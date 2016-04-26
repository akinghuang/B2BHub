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

namespace MW.DataHub.Portal.Console.wuc
{
    public partial class wucMasterControl : System.Web.UI.UserControl
    {
        protected int UserID = 0;
        public wucMasterControl()
        {
            //try
            //{
            //    UserID = int.Parse(Session["UserID"].ToString());
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>var wind=window;while(wind){if(wind.logClick){wind.logClick();break;}else{wind=wind.parent;}}</script>");
            //}
        }

        protected void windowhref(string url)
        {
            Response.Write("<script>var wind=window;while(wind){if(wind.urlChange){wind.urlChange('" + url + "');break;}else{wind=wind.parent;}}</script>");
        }

        protected void windowhref1(string url)
        {
            Response.Write("<script>var wind=window;while(wind){if(wind.urlChange1){wind.urlChange1('" + url + "');break;}else{wind=wind.parent;}}</script>");
        }

        protected void checkOnline()
        {
            try
            {
                UserID = int.Parse(Session["UserID"].ToString());
            }
            catch (Exception ex)
            {
                Response.Write("<script>var wind=window;while(wind){if(wind.logClick){wind.logClick(wind);break;}else{wind=wind.parent;}}</script>");
            }
        }

        protected string StrJosnReplace(string StrJosn)
        {
            return StrJosn.Replace("\r\n", "-flag01-").Replace(":", "-flag02-").Replace(@"\", "-flag03-").Replace("\"", "-flag04-").Replace("\'", "-flag05-").Replace("<", "-flag06-").Replace(">", "-flag07-");
        }
    }
}