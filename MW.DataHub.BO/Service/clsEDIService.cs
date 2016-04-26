
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using MW.DataHub.BO.Entity;
using MW.DataHub.API.Net;
using MW.DataHub.EDIInterface;

namespace MW.DataHub.BO.Service
{
    public class clsEDIService
    {
        public EntityBPProject _CurrProject;
        public IBPProjectTask TaskMgr = BOFactory.GetBPProjectTask();
        MW.DataHub.API.Net.clsMailHandler mail = new clsMailHandler();
                
        public clsEDIService(Entity.EntityBPProject project)
        {
            _CurrProject = project;
            mail.OnMessageChanged += new MailMessageChangedHandler(mail_OnMessageChanged);
        }
        public void ProjectStart()
        {
            List<EntityBPProjectTask> Tasks = TaskMgr.getTaskEntityListByProjectID(this._CurrProject.ID);
            foreach (EntityBPProjectTask Task in Tasks)
            {
                if (TaskTrigger(Task))
                {
                    try
                    {
                        Task.Running = true;
                        TaskMgr.SaveData(Task);
                        this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + Task.TaskName, "Set Task to running.");
                    }
                    catch (Exception e)
                    {
                        this.RecordLog("HostService", "Error", this._CurrProject.ProjectName + "_" + Task.TaskName, "Update task status failed." + e.ToString());
                    }

                    bool _RunResult = true;
                    
                    EntityBPActivities Activity = new EntityBPActivities();
                    Activity.ProjectID = this._CurrProject.ID;
                    Activity.TaskID = Task.ID;
                    Activity.LastRunStartDT = DateTime.Now;
                    try
                    {
                        #region Start process
                        if (Task.IO.ToUpper().Trim() == "OUTGOING")
                        {
                            ProcessCommon(Task);

                            switch (Task.Protocol.ToUpper())
                            {
                                case "FTP":
                                    _RunResult = ExecuteFTP(Task, false);
                                    break;
                                case "FTPS":
                                    _RunResult = ExecuteFTP(Task, false);
                                    break;
                                case "SFTP":
                                    _RunResult = ExecuteSFTP(Task, false);
                                    break;
                                case "LOCAL":
                                    _RunResult = ExecuteLocal(Task, false);
                                    break;
                                default:
                                    _RunResult = ExecuteLocal(Task, false);
                                    break;
                            }
                        }
                        else//Default INCOMING
                        {
                            switch (Task.Protocol.ToUpper())
                            {
                                case "FTP":
                                    _RunResult = ExecuteFTP(Task, true);
                                    break;
                                case "FTPS":
                                    _RunResult = ExecuteFTP(Task, true);
                                    break;
                                case "SFTP":
                                    _RunResult = ExecuteSFTP(Task, true);
                                    break;
                                case "LOCAL":
                                    _RunResult = ExecuteLocal(Task, true);
                                    break;
                                default:
                                    _RunResult = ExecuteLocal(Task, true);
                                    break;
                            }
                            ProcessCommon(Task);
                        }
                        #endregion
                    }
                    catch (Exception e)
                    {
                        _RunResult = false;
                        this.RecordLog("HostService", "Error", this._CurrProject.ProjectName + "_" + Task.TaskName, "Process EDI Task failed." + e.ToString());
                    }
                    //Update task activity
                    try
                    {
                        Activity.LastRunStatus = _RunResult;
                        Activity.LastRunEndDT = DateTime.Now;
                        Activity.LastRunResult = _RunResult?"Success":"Fail";
                        if (_RunResult)
                        {
                            Activity.LastSuccessDT = DateTime.Now;
                            Activity.RunFailTimes = 0;
                        }
                        else
                            Activity.RunFailTimes = (Activity.RunFailTimes.HasValue ? Activity.RunFailTimes.Value : 0) + 1;
                        IBPActivities ActivityMgr = BOFactory.GetBPActivities();
                        ActivityMgr.SaveData(Activity);
                    }
                    catch (Exception e)
                    {
                        this.RecordLog("HostService", "Error", this._CurrProject.ProjectName + "_" + Task.TaskName, "Update Activity failed." + e.ToString());
                    }

                    //Update task status
                    try
                    {
                        Task.LastRunTime = DateTime.Now;
                        Task.Running = false;
                        Task.NextRunTime = getNextRunTime(Task);
                        TaskMgr.SaveSQLData(Task);
                        this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + Task.TaskName, "Set Task to idle.");
                    }
                    catch (Exception e)
                    {
                        this.RecordLog("HostService", "Error", this._CurrProject.ProjectName + "_" + Task.TaskName, "Update task status failed."+e.ToString());
                    }                   
                    
                }
            }
        }
        public bool ExecuteFTP(EntityBPProjectTask task,bool download)
        {
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Execute FTP "+(download?"Download":"Upload")+" start");
            if (!download)
                ProcessOutgoing(task);
            FtpLogin flogin = new FtpLogin();
            flogin.Server = task.RServer;
            flogin.Port=task.RPort.HasValue?task.RPort.Value:21;
            if(task.RUID.Trim()!="")
                flogin.UserName=task.RUID;
            if(task.RPWD.Trim()!="")
                flogin.Password=task.RPWD.Trim();
            flogin.Direction= download?FtpDirection.Download: FtpDirection.Upload;
            flogin.FileExtension=task.FileExt;
            flogin.RemoteFolder=task.RFolder;
            flogin.LocalFolder=task.LFolder;
            flogin.FTPTool = FtpTools.FTPAPI;

            clsFTPHandler ftp = new clsFTPHandler();
            ftp.MessageChanged += new MessageChangedHandler(ftp_MessageChanged);
            try
            {
                ftp.doFTP(flogin);
            }
            catch(Exception e)
            {
                this.RecordLog("HostService", "Error", this._CurrProject.ProjectName + "_" + task.TaskName, "Execute FTP error "+e.ToString());
                return false;
            }
            if (download)
                ProcessIncoming(task);

            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Execute FTP " + (download ? "Download" : "Upload") + " completed");
            return true;
        }
        public bool ExecuteSFTP(EntityBPProjectTask task, bool download)
        {
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Execute sFTP " + (download ? "Download" : "Upload") + " start");
            if (!download)
                ProcessOutgoing(task);
            SFTP sFtp = new SFTP(task.RServer, task.RUID, task.RPWD,task.RPort.HasValue?task.RPort.Value:22);
            sFtp.Connect();
            if (download)
                sFtp.DownloadData(task.LFolder, task.FileExt, task.RFolder);
            else
                sFtp.UploadData(task.LFolder, task.FileExt, task.RFolder);            

            sFtp.Disconnect();
            
            if (download)
                ProcessIncoming(task);

            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Execute sFTP " + (download ? "Download" : "Upload") + " completed");
            return true;
        }
        public bool ExecuteLocal(EntityBPProjectTask task, bool download)
        {            
            if (Directory.Exists(task.RFolder) && Directory.Exists(task.LFolder))
            {
                if (!download)
                {
                    if (!ProcessOutgoing(task))
                        return false;
                    foreach (string file in Directory.GetFiles(task.LFolder, task.FileExt))
                    {
                        string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                        
                        File.Move(file, task.RFolder + "\\" + fileName);
                        this.RecordLog("HostService", "Log", fileName, "File movement from [" + file + "] to [" + task.RFolder + "\\" + fileName + "]");
                    }
                    return true;
                }
                else
                {
                    foreach (string file in Directory.GetFiles(task.RFolder, task.FileExt))
                    {
                        string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                        string fileNameNew = fileName.Substring(0, fileName.LastIndexOf(".")) + "_" + Guid.NewGuid().ToString() + fileName.Substring(fileName.LastIndexOf("."));
                        if (!File.Exists(task.LFolder + "\\" + fileName))
                        {
                            File.Move(file, task.LFolder + "\\" + fileName);
                            this.RecordLog("HostService", "Log", fileName, "File movement from [" + file + "] to [" + task.RFolder + "\\" + fileName + "]");
                        }
                        else
                        {
                            File.Move(file, task.LFolder + "\\" + fileNameNew);
                            this.RecordLog("HostService", "Log", fileName, "File movement from [" + file + "] to [" + task.RFolder + "\\" + fileNameNew + "]");
                        }
                        
                    }
                    if (!ProcessIncoming(task))
                        return false;
                    return true;
                }
            }
            else
            {
                if (!Directory.Exists(task.RFolder))
                    
                    this.RecordLog("HostSerivce", "Error", task.TaskName, "Remote folder not exist:" + task.RFolder);

                if (!Directory.Exists(task.LFolder))
                    this.RecordLog("HostSerivce", "Error", task.TaskName, "Local folder not exist:" + task.LFolder);
                return false;
            }
        }
        public bool ProcessIncoming(EntityBPProjectTask task)
        {
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName+"_"+task.TaskName, "Process incoming start");

            System.Reflection.Assembly asmMsg = System.Reflection.Assembly.LoadFrom(task.MsgHandler);
            MW.DataHub.EDIInterface.IMsgHandler msgParser = (MW.DataHub.EDIInterface.IMsgHandler)System.Activator.CreateInstance(asmMsg.GetType(System.IO.Path.GetFileNameWithoutExtension(task.MsgHandler) + ".MsgParser"));
            msgParser.OnError += new OnError(msg_OnError);

            System.Reflection.Assembly asmBiz = System.Reflection.Assembly.LoadFrom(task.BizHandler);
            MW.DataHub.EDIInterface.IBizHandler Biz = (MW.DataHub.EDIInterface.IBizHandler)System.Activator.CreateInstance(asmBiz.GetType(System.IO.Path.GetFileNameWithoutExtension(task.BizHandler) + ".BizHandler"));
            Biz.OnError += new OnError(Biz_OnError);

            StringBuilder MailBody = new StringBuilder("<Table style=\"border:solid 1pt black\"><tr><td colspan=3>Process incoming history</td></tr><tr><td><b>File</b></td><td><b>Status</b></td><td><b>Time</b></td></tr>");
            bool hasIncoming = false;
            foreach (string file in Directory.GetFiles(task.LFolder, task.FileExt))
            {
                string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                this.RecordLog("HostService", "Log", fileName, "Process received message [" + fileName + "] start");
                string outMsg ="";
                //try
                //{
                    DataSet dsInData = msgParser.ParseMessage(this._CurrProject.RuntimeParas, task.RuntimeParas, file, ref outMsg);
                    if (dsInData != null)
                    {
                        string outMsgBiz = "";
                        Biz.ImportData(this._CurrProject.RuntimeParas, task.RuntimeParas, dsInData, ref outMsgBiz);
                        MailBody.Append(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", fileName, "Success", DateTime.Now.ToString()));
                    }
                    else
                        MailBody.Append(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", fileName, "Fail[No data]", DateTime.Now.ToString()));
                //}
                //catch (Exception exp) { SendMail(this._CurrProject.ProjectName + " - " + task.TaskName + " - Incoming record", true, exp.ToString()); }
                this.RecordLog("HostService", "Log", fileName, "Process received message [" + fileName + "] complete");
                hasIncoming = true;
            }
            MailBody.Append("</table>");
            if(hasIncoming)
                SendMail(this._CurrProject.ProjectName+" - "+task.TaskName+" - Incoming record", true, MailBody.ToString());//Send mail to admin for the processed files
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Process incoming complete");
            return true;
        }
        public bool ProcessOutgoing(EntityBPProjectTask task)
        {
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Process outgoing start");

            System.Reflection.Assembly asmBiz = System.Reflection.Assembly.LoadFrom(task.BizHandler);
            MW.DataHub.EDIInterface.IBizHandler Biz = (MW.DataHub.EDIInterface.IBizHandler)System.Activator.CreateInstance(asmBiz.GetType(System.IO.Path.GetFileNameWithoutExtension(task.BizHandler) + ".BizHandler"));
            Biz.OnError += new OnError(Biz_OnError);
            System.Reflection.Assembly asmMsg = System.Reflection.Assembly.LoadFrom(task.MsgHandler);
            MW.DataHub.EDIInterface.IMsgHandler msg = (MW.DataHub.EDIInterface.IMsgHandler)System.Activator.CreateInstance(asmMsg.GetType(System.IO.Path.GetFileNameWithoutExtension(task.MsgHandler) + ".MsgParser"));
            msg.OnError += new OnError(msg_OnError);
          
            string outMsgBiz="";
            DataSet dsBizData = Biz.ExtractData(this._CurrProject.RuntimeParas, task.RuntimeParas, ref outMsgBiz);

            String outMsg="";
            msg.PackMessage(this._CurrProject.RuntimeParas, task.RuntimeParas, dsBizData, ref outMsg);
            StringBuilder MailBody = new StringBuilder("<Table style=\"border:solid 1pt black\"><tr><td colspan=3>Process outgoing history</td></tr><tr><td><b>File</b></td><td><b>Status</b></td><td><b>Time</b></td></tr>");
            MailBody.Append("<tr><td colspan=3>" + outMsg + "</td></tr>");
            MailBody.Append("</table>");
            if(outMsg.Trim()!="")
                SendMail(this._CurrProject.ProjectName+" - "+task.TaskName+" - Outgoing record", true, MailBody.ToString());//Send mail to admin for the processed files

            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Process outgoing complete");
            return true;
        }
        public void ProcessCommon(EntityBPProjectTask task)
        {
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Process common start");

            System.Reflection.Assembly asmBiz = System.Reflection.Assembly.LoadFrom(task.BizHandler);
            MW.DataHub.EDIInterface.IBizHandler Biz = (MW.DataHub.EDIInterface.IBizHandler)System.Activator.CreateInstance(asmBiz.GetType(System.IO.Path.GetFileNameWithoutExtension(task.BizHandler) + ".BizHandler"));
            Biz.OnError += new OnError(Biz_OnError);
            String outMsg = "";
            if (Biz.CommonFun(this._CurrProject.RuntimeParas, task.RuntimeParas, ref outMsg))
            {
                this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, outMsg);
            }
            else
                this.RecordLog("HostService", "Error", this._CurrProject.ProjectName + "_" + task.TaskName, outMsg);
            
            this.RecordLog("HostService", "Log", this._CurrProject.ProjectName + "_" + task.TaskName, "Process common complete");
        }
        void msg_OnError(string ErrorType, string KeyValue, string ErrorMessage, bool TriggerNotification, bool SendToAdminOnly)
        {
            RecordLog("MsgParser", ErrorType, KeyValue, ErrorMessage);
            if (TriggerNotification)
            {

                MailEntity mailInfo = new MailEntity();
                mailInfo.MailBodyFormat = "HTML";
                mailInfo.Subject = this._CurrProject.ProjectName;
                mailInfo.ProjectID = this._CurrProject.ID;
                if (SendToAdminOnly)
                    mailInfo.MailTo = this._CurrProject.AdminMail;
                else
                {
                    mailInfo.MailTo = this._CurrProject.UserMail;
                    mailInfo.MailCC = this._CurrProject.AdminMail;
                }
                mailInfo.MailBody = String.Format("<B>Notification type</B>:{0}<BR><B>Key words:</B>:{1}<BR><B>Details</B>:{2}<BR>This is an auto mail from Dimerco B2B,please do not reply.", ErrorType, KeyValue, ErrorMessage);
                mail.SendMail(mailInfo);
            }
        }
        void Biz_OnError(string ErrorType, string KeyValue, string ErrorMessage, bool TriggerNotification, bool SendToAdminOnly)
        {
            RecordLog("BizHandler", ErrorType, KeyValue, ErrorMessage);
            if (TriggerNotification)
            {
                
                MailEntity mailInfo = new MailEntity();
                mailInfo.MailBodyFormat ="HTML";
                mailInfo.Subject = this._CurrProject.ProjectName;
                mailInfo.ProjectID = this._CurrProject.ID;
                if (SendToAdminOnly)
                    mailInfo.MailTo = this._CurrProject.AdminMail;
                else
                {
                    mailInfo.MailTo = this._CurrProject.UserMail;
                    mailInfo.MailCC = this._CurrProject.AdminMail;
                }
                mailInfo.MailBody = String.Format("<B>Notification type</B>:{0}<BR><B>Key words:</B>:{1}<BR><B>Details</B>:{2}<BR>This is an auto mail from Dimerco B2B,please do not reply.", ErrorType, KeyValue, ErrorMessage);
                mail.SendMail(mailInfo);
            }
        }
        void mail_OnMessageChanged(MailMessageType msgType, string msg)
        {
            if(msgType== MailMessageType.Start||msgType== MailMessageType.End)
                RecordLog("MailSender", "Log", msgType.ToString(), msg);
        }

        void ftp_MessageChanged(FtpMessageType msgType, string msg)
        {
            switch (msgType)
            {
                case FtpMessageType.End:                    
                    break;
                case FtpMessageType.Message:
                    break;
                case FtpMessageType.Start:
                    break;
                case FtpMessageType.Success:
                    break;
                case FtpMessageType.Fail:
                    RecordLog("eMailSender", "Error", msgType.ToString(), msg);
                    break;
            }
            //throw new NotImplementedException();
        }
        void RecordLog(string Source,string ErrorType,string KeyValue,string LogContent)
        {
            clsBPProjectLog Log = new clsBPProjectLog();
            EntityBPProjectLog LogInfo = new EntityBPProjectLog();
            LogInfo.LogType = ErrorType;
            LogInfo.LogTime = DateTime.Now;
            LogInfo.KeyValue = KeyValue;
            LogInfo.Source = Source;
            LogInfo.Log = LogContent;
            LogInfo.ProjectID = this._CurrProject.ID;
            if (ErrorType.ToLower().Trim() == "error")
            {
                MailEntity mailInfo = new MailEntity();
                mailInfo.MailBodyFormat = "HTML";
                mailInfo.Subject = this._CurrProject.ProjectName;
                mailInfo.ProjectID = this._CurrProject.ID;
                mailInfo.MailTo = this._CurrProject.AdminMail;
                mailInfo.MailBody = String.Format("<B>Notification type</B>:{0}<BR><B>Key words:</B>:{1}<BR><B>Details</B>:{2}<BR>This is an auto mail from Dimerco B2B,please do not reply.", ErrorType, KeyValue, Source + ":<br>" + LogContent);
                mail.SendMail(mailInfo);
            }
            Log.SaveData(LogInfo);
        }
        void SendMail(string Subject, bool toAdminOnly, string body)
        {
            MailEntity mailInfo = new MailEntity();
            mailInfo.MailBodyFormat = "HTML";
            mailInfo.Subject = Subject.Trim() == "" ? (this._CurrProject.ProjectName) : Subject;
            mailInfo.ProjectID = this._CurrProject.ID;
            if (!toAdminOnly)
            {
                mailInfo.MailTo = this._CurrProject.UserMail;
                mailInfo.MailCC = this._CurrProject.AdminMail;
            }
            else
                mailInfo.MailTo = this._CurrProject.AdminMail;

            mailInfo.MailBody = String.Format("{0}<BR>This is an auto mail from Dimerco B2B,please do not reply.", body);
            mail.SendMail(mailInfo);
        }
        public bool TaskTrigger(EntityBPProjectTask Task)
        {
            if (Task.Status.ToUpper().Trim() != "ACTIVE"||(Task.Running.HasValue?Task.Running.Value:false))
                return false;

            TimeSpan Curr = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan LastRun = new TimeSpan(Task.LastRunTime.HasValue?Task.LastRunTime.Value.Ticks:DateTime.Now.Ticks);

            switch (Task.ScheduleType.ToUpper().Trim())
            {
                case "EVERY":
                    if (!Task.LastRunTime.HasValue)
                        return true;
                    return Curr.Subtract(LastRun).Minutes >= Task.ScheduleInterval.Value?true:false;
                case "DAILY":
                    return (!checkHasRunInMin(Task)&& DateTime.Now.ToString("HH:mm") == Task.ScheduleTime) ? true : false;
                case "WEEKLY":
                    return (!checkHasRunInMin(Task) && DateTime.Now.DayOfWeek.ToString() == Task.ScheduleWeekDay && DateTime.Now.ToString("HH:mm") == Task.ScheduleTime) ? true : false;
                case "MONTHLY":
                    return (!checkHasRunInMin(Task) && DateTime.Now.Day == Task.ScheduleMonth.Value && DateTime.Now.ToString("HH:mm") == Task.ScheduleTime) ? true : false;
                default:
                    return Curr.Subtract(LastRun).Minutes >= Task.ScheduleInterval.Value ? true : false;
            }
        }
        private bool checkHasRunInMin(EntityBPProjectTask Task)
        {
            if (!Task.LastRunTime.HasValue)
                return false;
            if (Task.LastRunTime.Value.ToString("yyyyMMdd HH:mm") == DateTime.Now.ToString("yyyyMMdd HH:mm"))
                return true;
            else
                return false;
        }
        public DateTime getNextRunTime(EntityBPProjectTask Task)
        {
            switch (Task.ScheduleType.ToUpper().Trim())
            {
                case "EVERY":
                    return DateTime.Now.AddMinutes(Task.ScheduleInterval.Value);
                case "DAILY":
                    return DateTime.Now.AddDays(1);
                case "WEEKLY":
                    return DateTime.Now.AddDays(7);
                case "MONTHLY":
                    return DateTime.Now.AddMonths(1);
                default:
                    return DateTime.Now.AddMinutes(Task.ScheduleInterval.Value);
            }
        }
    }
}
