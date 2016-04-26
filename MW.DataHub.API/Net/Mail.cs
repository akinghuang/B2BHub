using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

using MW.DataHub.API.BO;
using System.IO;
using System.Data;
namespace MW.DataHub.API.Net
{
    public enum MailMessageType
    {
        Start, End, Success, Fail, Message, Warning
    }
    public class MailEntity : MW.DataHub.API.BO.Entity.EntityBPMailFailed
    {
        private string _AttachFile;
        public string AttachFile
        {
            get { return this._AttachFile; }
            set { this._AttachFile = value; }
        }

        private bool _ReSend = true;
        internal bool ReSendForFailed
        {
            get { return this._ReSend; }
            set { this._ReSend = value; }
        }
    }
    public delegate void MailMessageChangedHandler(MailMessageType msgType, string msg);

    public class clsMailHandler
    {
        public event MailMessageChangedHandler OnMessageChanged = null;
        BO.Entity.EntityBPSetting eSetting;
        BO.IBPSetting mgrSetting = BOFactory.GetBPSetting();

        private bool IsMailAddress(string mailAddress)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(mailAddress
               , @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private void ErrorMailAddress(string MailAddress, string Type)
        {
            if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Warning, string.Format("Mail Address {0} of {1} is Error", MailAddress, Type));
        }

        /// <summary>
        /// Send Mail Function
        /// </summary>
        /// <param name="entity">定义的发送mail的内容</param>
        /// <returns>如果发送成功，返回0， 如果发送错误，返回大于0的数 </returns>
        public int SendMail(MailEntity entity)
        {
            return SendMail(entity, "MailSMTP1");
        }

        /// <summary>
        /// Re-send mail since last send error.
        /// </summary>
        /// <param name="FailID">The ID which saved in BPMailFailed. the record will be removed if send successfully</param>
        /// <param name="bSendTwice">true: will use MailSMTP2 as SMTPServer, false: will user MailSMTP1 as SMTP Server </param>
        /// <returns></returns>
        private int SendMailAgain(int FailID, bool bSendTwice)
        {
            IBPMailFailed mgrFail = BOFactory.GetBPMailFailed();
            BO.Entity.EntityBPMailFailed enFail = mgrFail.GetEntityById(FailID);

            if (enFail == null)
            {
                string strError = string.Format("Did not find the record which ID is {0}", FailID);
                return -1;
            }
            if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Message, string.Format("Start to Resend the Mail which key is {0}", enFail.MailGUID));
            System.Windows.Forms.Application.DoEvents();
            MailEntity entity = new MailEntity();
            entity.ID = enFail.ID;
            entity.CreatedBy = enFail.CreatedBy;
            entity.CreatedDate = enFail.CreatedDate;
            entity.FailTimes = enFail.FailTimes;
            entity.MailBCC = enFail.MailBCC;
            entity.MailBody = enFail.MailBody;
            entity.MailBodyFormat = enFail.MailBodyFormat;
            entity.MailCC = enFail.MailCC;
            entity.MailGUID = enFail.MailGUID;
            entity.MailResult = enFail.MailResult;
            entity.MailSender = enFail.MailSender;
            entity.MailStatus = enFail.MailStatus;
            entity.MailTo = enFail.MailTo;
            entity.ProjectID = enFail.ProjectID;
            entity.SendBy = enFail.SendBy;
            entity.Subject = enFail.Subject;
            return SendMail(entity, (bSendTwice ? "MailSMTP2" : "MailSMTP1"));
        }

        /// <summary>
        /// Send mail use current MailEntity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="SMTPServer">Distinguish the setting of SMTPServer</param>
        /// <returns></returns>
        private int SendMail(MailEntity entity, string SMTPServer)
        {
            //这个主要是在第一次发送失败后需要重新发送， 如果只是重新发送，就不要多次处理了。
            if (entity.ID > 0) entity.ReSendForFailed = false;
            eSetting = mgrSetting.GetEntityByPK(0, SMTPServer);
            if (eSetting == null||(string.IsNullOrEmpty(entity.MailTo)&&string.IsNullOrEmpty(entity.MailCC)))
            {
                if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Fail, string.Format("There did not have setting or MailTo '{0}' in BPSetting", SMTPServer));
                return 1;
                //throw new Exception("Did not find the Mail Setting  or MailTo, Please config it.");
            }
            if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Start, string.Format("Starting to Process the SMTP {0} which SMTP Server is {1}", SMTPServer, eSetting.SMTPServer));

            MailMessage mail = new MailMessage();
            //Encoding eEncode = Encoding.GetEncoding(eSetting.Encoding);
            //mail.BodyEncoding = eEncode;
            //mail.SubjectEncoding = eEncode;

            #region Mail Address
            string mailSender = entity.MailSender;
            string mailSenderName = entity.MailSender;

            if (string.IsNullOrEmpty(mailSender))
            {
                mailSender = eSetting.Sender;
                mailSenderName = eSetting.SenderName;
            }
            if (!IsMailAddress(mailSender))
            {
                if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Fail, string.Format("Did not have Send eMail Address, Program return."));
                throw new Exception("Sender address is not a mail address, Please config it.");
            }
            mail.From = new MailAddress(mailSender, mailSenderName);

            if (entity.MailTo != null)
                foreach (string add in entity.MailTo.Split(','))
                    if (IsMailAddress(add))
                        mail.To.Add(add);
                    else
                        ErrorMailAddress(add, "To");

            if (entity.MailCC != null)
                foreach (string add in entity.MailCC.Split(','))
                    if (IsMailAddress(add))
                        mail.CC.Add(add);
                    else
                        ErrorMailAddress(add, "CC");

            if (entity.MailBCC != null)
                foreach (string add in entity.MailBCC.Split(','))
                    if (IsMailAddress(add))
                        mail.Bcc.Add(add);
                    else
                        ErrorMailAddress(add, "BCC");

            //This is used for mail checking if lost
            if (eSetting.BCC != null)
                foreach (string add in eSetting.BCC.Split(','))
                    if (IsMailAddress(add))
                        mail.Bcc.Add(add);
                    else
                        ErrorMailAddress(add, "BCC");

            if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Message, string.Format("Complete the Mail Address Initial."));
            #endregion Mail Address

            if (string.IsNullOrEmpty(entity.Subject))
                entity.Subject = "DIMERCO B2B Mail Notification";
            mail.Subject = entity.Subject;
            mail.Body = entity.MailBody;
            mail.IsBodyHtml = true;//eSetting.BodyFormat;

            #region Attachment
            Guid mailGUID = entity.MailGUID;

            IBPMailAttach mgrAttach = BOFactory.GetBPMailAttach();
            if (entity.ID > 0)
            {
                //需要发送mail
                //System.Net.Mail.Attachment FAtta = null; // new Attachment(reader, attachFile.Substring(attachFile.LastIndexOf("\\") + 1));
                //mail.Attachments.Add(FAtta);

                List<BO.Entity.EntityBPMailAttach> lists = mgrAttach.GetSQLEntityListByGUID(mailGUID);
                foreach (BO.Entity.EntityBPMailAttach enAttach in lists)
                {
                    System.Net.Mime.ContentType content = new System.Net.Mime.ContentType();
                    content.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
                    content.Name = enAttach.FileName;
                    MemoryStream stream = new MemoryStream();
                    stream.Write(enAttach.FileContent, 0, enAttach.FileLength.Value);
                    stream.Position = 0;
                    System.Net.Mail.Attachment FAtta = new Attachment(stream, content);
                    mail.Attachments.Add(FAtta);
                }
            }
            else
            {
                mailGUID = Guid.NewGuid();
                if (entity.AttachFile != null)
                    foreach (string attachFile in entity.AttachFile.Split(','))
                    {
                        if (attachFile.Trim() != "")
                        {
                            if (File.Exists(attachFile))
                            {
                                FileStream reader = new FileStream(attachFile, FileMode.Open, FileAccess.Read);

                                BO.Entity.EntityBPMailAttach enAttach = new MW.DataHub.API.BO.Entity.EntityBPMailAttach();
                                enAttach.FilePath = attachFile;
                                enAttach.FileName = attachFile.Substring(attachFile.LastIndexOf("\\") + 1);
                                enAttach.FileLength = Convert.ToInt32(reader.Length);
                                enAttach.MailGUID = mailGUID;
                                //enAttach.FileContent = "";
                                byte[] fileByte = new byte[enAttach.FileLength.Value];
                                reader.Read(fileByte, 0, enAttach.FileLength.Value);
                                reader.Position = 0;
                                enAttach.FileContent = fileByte;
                                mgrAttach.SaveSQLData(enAttach, "");

                                System.Net.Mail.Attachment FAtta = new Attachment(reader, enAttach.FileName);
                                mail.Attachments.Add(FAtta);
                            }
                            else
                                ErrorMailAddress(attachFile, "Attach File");
                        }
                    }
            }
            #endregion Attachment

            SmtpClient smtp = new SmtpClient();
            smtp.Host = eSetting.SMTPServer;        // Load Mail Server
            smtp.Port = 25;
            if (!string.IsNullOrEmpty(eSetting.UserName))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(eSetting.UserName, eSetting.Password);
            }
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 600000;

            bool bSendResult = true;
            string SendResult = "Send Successfully.";
            int SendFailID = 0;
            if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Message, string.Format("Start to Send the mail."));
            try
            {
                System.Windows.Forms.Application.DoEvents();
                smtp.Send(mail);

                if (OnMessageChanged != null) OnMessageChanged(MailMessageType.Success, string.Format("Send Mail Successfully"));
                System.Windows.Forms.Application.DoEvents();
            }
            catch (SmtpException exp)
            {
                bSendResult = false;
                SendResult = string.Format("SMTP send fail. Reason:{0} \n{1}", exp.InnerException.Message
                    , exp.StackTrace);
            }
            catch (Exception exp)
            {
                bSendResult = false;
                SendResult = string.Format("Send fail. Reason:{0} \n{1}", exp.InnerException.Message
                    , exp.StackTrace);
            }
            finally
            {
                #region Log Mail
                IBPMailFailed mgrFail = BOFactory.GetBPMailFailed();
                BO.Entity.EntityBPMailFailed enFail = (MW.DataHub.API.BO.Entity.EntityBPMailFailed)entity;

                if (enFail.ID > 0 && bSendResult)
                {
                    mgrFail.RemoveEntityById(enFail.ID);
                }
                else if (!bSendResult)
                {
                    if (enFail.ID > 0)
                        enFail.FailTimes++;
                    else
                        enFail.FailTimes = 1;

                    enFail.MailStatus = "0";
                    enFail.SendDate = DateTime.Now;
                    enFail.MailGUID = mailGUID;
                    enFail.MailResult = SendResult;

                    SendFailID = mgrFail.SaveSQLData(enFail, "");
                }

                IBPMailLog mgrLog = BOFactory.GetBPMailLog();
                BO.Entity.EntityBPMailLog enLog = new MW.DataHub.API.BO.Entity.EntityBPMailLog();

                enLog.ProjectID = entity.ProjectID;
                enLog.MailSender = entity.MailSender;
                enLog.MailTo = entity.MailTo;
                enLog.MailCC = entity.MailCC;
                enLog.MailBCC = entity.MailBCC;
                enLog.Subject = entity.Subject;
                enLog.MailBody = entity.MailBody;
                enLog.MailBodyFormat = entity.MailBodyFormat;

                enLog.MailStatus = (bSendResult ? "1" : "0");
                enLog.SendDate = DateTime.Now;
                enLog.MailGUID = mailGUID;
                enLog.MailResult = SendResult;

                mgrLog.SaveSQLData(enLog, "");
                #endregion Log Mail
            }

            if (SendFailID > 0 && entity.ReSendForFailed)
                SendFailID = SendMailAgain(SendFailID, true);

            mail.Dispose();
            mail = null;
            smtp = null;

            if (OnMessageChanged != null) OnMessageChanged(MailMessageType.End, string.Format("Send Mail End Result {0}:{1} ", (SendFailID > 0 ? "Failed" : "Success"), SendFailID));

            return SendFailID;
        }

        public void ReSendMail()
        {
            IBPMailFailed mgrFail = BOFactory.GetBPMailFailed();
            DataSet lds = mgrFail.GetSQLSearchFunction(null);
            foreach (DataRow ldr in lds.Tables[0].Rows)
            {
                SendMailAgain(Convert.ToInt32(ldr["ID"]), false);
            }
        }

    }
}
