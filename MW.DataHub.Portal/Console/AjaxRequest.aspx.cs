using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MW.DataHub.BO;

namespace MW.DataHub.Portal.Console
{
    public partial class AjaxRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = "";
            string funtion = Request.QueryString["funtion"];
            switch (funtion)
            {
                case "CheckLog":
                    string UserName = Request.Form["UserName"];
                    string PassWord = Request.Form["PassWord"];
                    string UserIP = getIp();
                    Response.Write(CheckLog(UserName, PassWord,UserIP));
                    break;
                case "GetLogDetail":
                    ID = Request.Form["ID"];
                    string ProjectID = Request.Form["ProjectID"];
                    Response.Write(GetLogDetail(ID,ProjectID));
                    break;
                case "GetUserListByID":
                    string User_ID = Request.Form["ID"];
                    Response.Write(GetUserListByID(User_ID));
                    break;
                case "GetProjectDetail":
                    ID = Request.Form["ID"];
                    Response.Write(GetProjectDetail(ID));
                    break;
                    
                case "GetProjectTaskDetail":
                    ID = Request.Form["ID"];
                    Response.Write(GetProjectTaskDetail(ID));
                    break;
                case "GetProjectChangeDetail":
                    ID = Request.Form["ID"];
                    Response.Write(GetProjectChangeDetail(ID));
                    break;
            }
            Response.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            Response.End();
        }

        #region ProjectLog
        string GetLogDetail(string ID,string ProjectID)
        {
            IBPProjectLog ProjectLog = BOFactory.GetBPProjectLog();
            BO.Entity.EntityBPProjectLog entityProjectLog = new BO.Entity.EntityBPProjectLog();
            string responseStr = "";
            try
            {
                entityProjectLog = ProjectLog.GetEntityByPK(int.Parse(ID), int.Parse(ProjectID));
                responseStr = "{";
                responseStr += "\"DateTime\":\"" + entityProjectLog.LogTime.ToString("yyyy/MM/dd hh:mm:ss") + "\",";
                responseStr += "\"Type\":\"" + entityProjectLog.LogType + "\",";
                responseStr += "\"Key\":\"" + entityProjectLog.KeyValue + "\",";
                responseStr += "\"Content\":\"" +StrReplace(entityProjectLog.Log) + "\"";
                responseStr += "}";
            }
            catch(Exception ex)
            {
                responseStr = ex.ToString();
            }
            return responseStr;
        }
        #endregion

        #region User
        string CheckLog(string UserName, string PassWord, string UserIP)
        {
            IBPUser BPUser = BOFactory.GetBPUser();
            BO.Entity.EntityBPUser entityUser = new BO.Entity.EntityBPUser();
            int LogState = -1;
            string passWord = "";
            string UserID = "";
            DataTable dt = BPUser.getBPUser(UserName, UserIP);
            if (dt.Rows.Count > 0)
            {
                passWord = dt.Rows[0]["Password"].ToString();
                UserID = dt.Rows[0]["ID"].ToString();
                if (passWord == PassWord)
                {
                    LogState = 1;
                    Session["UserID"] = UserID;
                    Session["UserName"] = dt.Rows[0]["FullName"] is DBNull ? "" : dt.Rows[0]["FullName"].ToString(); ;
                }
                else
                {
                    LogState = 3;
                }
            }
            else
            {
                LogState = 2;
            }
            return "{\"state\":\"" + LogState + "\"}";
        }

        private static string getIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        string GetUserListByID(string ID)
        {
            IBPUser BPUser = BOFactory.GetBPUser();
            BO.Entity.EntityBPUser entityUser = new BO.Entity.EntityBPUser();
            entityUser=BPUser.GetEntityByPK(int.Parse(ID));
            string responseStr = "{";
            responseStr += "\"UserID\":\"" + entityUser.UserID + "\",";
            responseStr += "\"UserName\":\"" + entityUser.FullName + "\",";
            responseStr += "\"Password\":\"" + entityUser.Password + "\",";
            responseStr += "\"Desc\":\"" + entityUser.Comments + "\",";
            responseStr += "\"Status\":\"" + entityUser.Status + "\"";
            responseStr += "}";
            return responseStr;
        }
        #endregion

        #region Project
        string GetProjectDetail(string ID)
        {
            string responseStr = "";
            BO.Entity.EntityBPProject entityProject = new BO.Entity.EntityBPProject();
            IBPProject project = BOFactory.GetBPProject();
            try
            {
                entityProject = project.GetEntityByPK(int.Parse(ID));
                responseStr = "{";
                responseStr += "\"Seq\":\"" + entityProject.Sequence + "\",";
                responseStr += "\"ProjectName\":\"" + entityProject.ProjectName + "\",";
                responseStr += "\"HostMachine\":\"" + entityProject.HostMachineID + "\",";
                responseStr += "\"ProcessID\":\"" + entityProject.ProcessID + "\",";
                responseStr += "\"Owner\":\"" + entityProject.Owner + "\",";
                responseStr += "\"UpdateDT\":\"" +( entityProject.UpdatedDT.ToString()==""?"": Convert.ToDateTime(entityProject.UpdatedDT).ToString("yyyy/MM/dd hh:mm:ss")) + "\",";
                responseStr += "\"Status\":\"" + entityProject.Status + "\",";
                responseStr += "\"AdminMail\":\"" + entityProject.AdminMail + "\",";
                responseStr += "\"UserMail\":\"" + entityProject.UserMail + "\",";
                responseStr += "\"Parameters\":\"" +StrReplace(entityProject.RuntimeParas) + "\",";
                responseStr += "\"Desc\":\"" +StrReplace( entityProject.ProjectDesc) + "\"";
                responseStr += "}";
            }
            catch (Exception ex)
            {
                responseStr = ex.ToString();
            }
            return responseStr;
        }
        #endregion 

        #region ProjrctTask
        string GetProjectTaskDetail(string ID)
        {
            string responseStr = "";
            BO.Entity.EntityBPProjectTask entityProjectTask = new BO.Entity.EntityBPProjectTask();
            IBPProjectTask projectTask = BOFactory.GetBPProjectTask();
            try
            {
                entityProjectTask = projectTask.GetEntityByPK(int.Parse(ID));
                responseStr = "{";
                responseStr += "\"Seq\":\"" + entityProjectTask.Sequence + "\",";
                responseStr += "\"TaskName\":\"" + entityProjectTask.TaskName + "\",";
                responseStr += "\"Protocol\":\"" + entityProjectTask.Protocol + "\",";
                responseStr += "\"IO\":\"" + entityProjectTask.IO + "\",";
                responseStr += "\"FileType\":\"" + entityProjectTask.FileExt + "\",";
                responseStr += "\"MsgHandler\":\"" + StrReplace(entityProjectTask.MsgHandler) + "\",";
                responseStr += "\"Status\":\"" + entityProjectTask.Status + "\",";
                responseStr += "\"BizHandler\":\"" + StrReplace(entityProjectTask.BizHandler) + "\",";
                responseStr += "\"RServer\":\"" + entityProjectTask.RServer + "\",";
                responseStr += "\"RFolder\":\"" + StrReplace(entityProjectTask.RFolder) + "\",";
                responseStr += "\"RPort\":\"" + entityProjectTask.RPort + "\",";
                responseStr += "\"RUID\":\"" + entityProjectTask.RUID + "\",";
                responseStr += "\"RPWD\":\"" + entityProjectTask.RPWD + "\",";
                responseStr += "\"RCert\":\"" + entityProjectTask.RCert + "\",";
                responseStr += "\"LFolder\":\"" +StrReplace( entityProjectTask.LFolder) + "\",";
                responseStr += "\"ScheduleType\":\"" + entityProjectTask.ScheduleType + "\",";
                responseStr += "\"ScheduleInterval\":\"" + entityProjectTask.ScheduleInterval + "\",";
                responseStr += "\"ScheduleMonth\":\"" + entityProjectTask.ScheduleMonth + "\",";
                responseStr += "\"ScheduleTime\":\"" + entityProjectTask.ScheduleTime + "\",";
                responseStr += "\"ScheduleWeekDay\":\"" + entityProjectTask.ScheduleWeekDay + "\",";
                responseStr += "\"Parameters\":\"" +StrReplace(entityProjectTask.RuntimeParas) + "\",";
                responseStr += "\"Desc\":\"" +StrReplace( entityProjectTask.TaskDesc)+"\"";;
                responseStr += "}";
            }
            catch (Exception ex)
            {
                responseStr = ex.ToString();
            }
            return responseStr;
        }
        #endregion

        #region ProjectChange
        string GetProjectChangeDetail(string ID)
        {
            string responseStr = "";
            BO.Entity.EntityBPProjectChange entityProjectChange = new BO.Entity.EntityBPProjectChange();
            IBPProjectChange ProjectChange = BOFactory.GetBPProjectChange();
            try
            {
                entityProjectChange = ProjectChange.GetEntityByPK(int.Parse(ID));
                responseStr = "{";
                responseStr += "\"Subject\":\"" + entityProjectChange.ChangeTitle + "\",";
                responseStr += "\"Owner\":\"" + entityProjectChange.Owner + "\",";
                responseStr += "\"Suorce\":\"" + StrReplace(entityProjectChange.ChangeSuorce) + "\",";
                responseStr += "\"Content\":\"" + StrReplace(entityProjectChange.ChangeContent) + "\",";
                responseStr += "\"StartDT\":\"" + (entityProjectChange.StartDT.ToString()==""?"":Convert.ToDateTime(entityProjectChange.StartDT).ToString("yyyy/MM/dd")) + "\",";
                responseStr += "\"TargetDT\":\"" +(entityProjectChange.TargetDT.ToString()==""?"": Convert.ToDateTime(entityProjectChange.TargetDT).ToString("yyyy/MM/dd")) + "\",";
                responseStr += "\"CompleteDT\":\"" +(entityProjectChange.CompleteDT.ToString()==""?"": Convert.ToDateTime(entityProjectChange.CompleteDT).ToString("yyyy/MM/dd")) + "\",";
                responseStr += "\"OnlineDT\":\"" +(entityProjectChange.OnlineDT.ToString()==""?"": Convert.ToDateTime(entityProjectChange.OnlineDT).ToString("yyyy/MM/dd")) + "\",";
                responseStr += "\"SourceCode\":\"" + StrReplace(entityProjectChange.SourceCode) + "\"";
                responseStr += "}";
            }
            catch (Exception ex)
            {
                responseStr = ex.ToString();
            }
            return responseStr;
        }
        #endregion

        private string StrReplace(string Str)
        {
            return Str.Replace("\r\n", "-flag01-").Replace("\n", "-flag02-").Replace("\t", "-flag03-").Replace("\r", "-flag04-").Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

    }
}
