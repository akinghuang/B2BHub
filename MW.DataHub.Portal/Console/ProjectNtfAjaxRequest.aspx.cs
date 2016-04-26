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
using System.IO;

namespace MW.DataHub.Portal.Console
{
    public partial class ProjectNtfAjaxRequest : System.Web.UI.Page
    {
        BO.Entity.EntityBPMailLog entity = new BO.Entity.EntityBPMailLog();
        IBPMailLog MailLog = BOFactory.GetBPProjectMailLog();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string funtion = Request.QueryString["funtion"];
            switch (funtion)
            {
                case "GetNotificationDetail":
                    string ID = Request.Form["ID"];
                    Response.Write(GetNotificationDetail(ID));
                    break;
                
            }
            Response.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            Response.End();
        }

        string GetNotificationDetail(string ID)
        {
            entity = MailLog.GetEntityByPK(Convert.ToInt32(ID));
            string responseStr = "{";
            responseStr += "\"DateTime\":\"" + (entity.SendDate.ToString() == "" ? "" : Convert.ToDateTime(entity.SendDate).ToString("yyyy/MM/dd hh:mm:ss")) + "\",";
            responseStr += "\"MailTo\":\"" + entity.MailTo + "\",";
            responseStr += "\"MailCC\":\"" + entity.MailCC + "\",";
            responseStr += "\"Content\":\"" + StrReplace(entity.MailBody) + "\",";
            responseStr += "\"Subject\":\"" + entity.Subject + "\"";
            responseStr += "}";
            return responseStr;
        }

        private string StrReplace(string Str)
        {
            return Str.Replace("\r\n", "-flag01-").Replace("\n", "-flag02-").Replace("\t", "-flag03-").Replace("\r", "-flag04-").Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
