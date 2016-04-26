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

namespace MW.DataHub.Portal.Console.wuc
{
    public partial class wucChangeManager : wucMasterControl
    {
        protected int ProjectID = 0;
        BO.Entity.EntityBPProjectChange entity = new BO.Entity.EntityBPProjectChange();
        BO.IBPProjectChange projectChange = BOFactory.GetBPProjectChange();
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            if (!IsPostBack)
            {
                BindProjectChange();
            }
        }

        void BindProjectChange()
        {
            entity.ProjectID = ProjectID;
            DataTable dt = projectChange.getProjectChangeByProjectID(ProjectID);
            RptProjectChange.DataSource = dt;
            RptProjectChange.DataBind();
        }

        protected void RptProjectChange_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    int ProjectChangeID = Convert.ToInt32(e.CommandArgument.ToString());
                    projectChange.DeleteProjectChangeByProjectChangeID(ProjectChangeID);
                    BindProjectChange();
                    break;
                case "Download":
                    try
                    {
                        Response.ContentType = "application/x-zip-compressed";
                        Response.AddHeader("Content-Disposition", "attachment;filename="+System.IO.Path.GetFileName(e.CommandArgument.ToString()));
                        string filename = Server.MapPath(e.CommandArgument.ToString());
                        Response.TransmitFile(filename);
                    }
                    catch(Exception ex)
                    {
                        
                    }
                    break;
            }
        }

        protected void RptProjectChange_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton ibtDownload = (ImageButton)e.Item.FindControl("ibtDownload");
            try
            {
                string path = Server.MapPath(ibtDownload.CommandArgument.ToString());
                if (File.Exists(path))
                {
                    ibtDownload.Visible = true;
                }
                else
                {
                    ibtDownload.Visible = false;
                }
            }
            catch(Exception ex)
            {
                ibtDownload.Visible = false;
            }
        }
    }
}