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


namespace MW.DataHub.Portal.Console.wuc
{
    public partial class wucProjectManager : wucMasterControl
    {
        
        BO.IBPProject BPProject = BOFactory.GetBPProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDgList();
            }
        }

        void BindDgList()
        {
            string ProjectCode = tetProjectCode.Text.Trim();
            string Status = ddlProjectStatus.SelectedValue;
            DataTable dt = BPProject.getProjectList(ProjectCode, Status); 
            //System.Collections.Generic.List<MW.DataHub.BO.Entity.EntityBPProject> dt = BPProject.getEntityBPProject(ProjectCode, Status);
            rptProjectList.DataSource = dt;
            rptProjectList.DataBind();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            BindDgList();
        }

        protected void rptProjectList_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    int ID = Convert.ToInt32(e.CommandArgument.ToString());
                    BPProject.updateProjectList(ID);
                    BindDgList();
                    break;
            }
        }
    }
}