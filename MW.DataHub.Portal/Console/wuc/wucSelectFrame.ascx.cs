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
    public partial class wucSelectFrame : wucMasterControl
    {
        protected int ID = -1;
        protected string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ID = string.IsNullOrEmpty(Request.QueryString["ProjectID"]) ? -1 : Convert.ToInt32(Request.QueryString["ProjectID"]);
            UserName = Request.QueryString["UserName"];
        }
    }
}