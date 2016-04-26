using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace MW.DataHub.Portal
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected int UserID = -1;
        protected string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UserID = int.Parse(Session["UserID"].ToString());
                UserName = Session["UserName"].ToString();
            }
            catch (Exception ex)
            {
                UserID = -1;
            }
        }
    }
}
