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
    public partial class wucChangeProfile : wucMasterControl
    {
        int ProjectID = 0;
        int ProjectChangeID = 0;
        string ChangeModule = "";
        BO.Entity.EntityBPProjectChange entity = new BO.Entity.EntityBPProjectChange();
        IBPProjectChange projectChange = BOFactory.GetBPProjectChange();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkOnline();
            ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            ProjectChangeID = Convert.ToInt32(Request.QueryString["ProjectChangeID"]);
            GetChangeModule();
            if (!IsPostBack)
            {
                if (ProjectChangeID > 0)
                {
                    btSave.Text = "Save Change";
                    btSave1.Text = "Save Change";
                    BindProjectChange();
                }
                else
                {
                    btSave.Text = "Create Project Change";
                    btSave1.Text = "Create Project Change";
                    txtContent.Text = ChangeModule;
                }
            }
        }

        void GetChangeModule()
        {
            string path = Server.MapPath("../BP_ProjectChange.htm");
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
                ChangeModule = sr.ReadToEnd();
                sr.Close();
            }
        }

        void BindProjectChange()
        {
            entity = projectChange.GetEntityByPK(ProjectChangeID);
            txtSubject.Text = entity.ChangeTitle;
            txtOwner.Text = entity.Owner;
            txtContent.Text = entity.ChangeContent;
            txtSource.Text = entity.ChangeSuorce;
            txtStartDate.Text = Convert.ToDateTime(entity.StartDT.ToString()).ToString("yyyy/MM/dd");
            txtTargetDate.Text = Convert.ToDateTime(entity.TargetDT.ToString()).ToString("yyyy/MM/dd");
            txtCompleteDate.Text = Convert.ToDateTime(entity.CompleteDT.ToString()).ToString("yyyy/MM/dd");
            txtOnlineDate.Text = Convert.ToDateTime(entity.OnlineDT.ToString()).ToString("yyyy/MM/dd");
            txtSourceCodePath.Text = entity.SourceCode;
        }

        protected void btSave_OnClick(object sender,EventArgs e)
        {
            entity.ID = ProjectChangeID;
            entity.ProjectID = ProjectID;
            entity.ChangeTitle = txtSubject.Text.Trim();
            entity.Owner = txtOwner.Text.Trim();
            entity.ChangeSuorce = txtSource.Text.Trim();
            entity.ChangeContent = txtContent.Text.Trim();
            entity.StartDT =Convert.ToDateTime(txtStartDate.Text.Trim());
            entity.TargetDT = Convert.ToDateTime(txtTargetDate.Text.Trim());
            entity.CompleteDT = Convert.ToDateTime(txtCompleteDate.Text.Trim());
            entity.OnlineDT = Convert.ToDateTime(txtOnlineDate.Text.Trim());
            entity.CreatedDT = DateTime.Now;
            entity.SourceCode =HttpUtility.UrlDecode(txtSourceCodePath.Text.Trim());
            projectChange.SaveSQLData(entity, UserID.ToString());
            this.windowhref1("../../Console/ChangeManager.aspx?ProjectID=" + ProjectID);
        }

        protected void btReloadTemp_OnClick(object sender, EventArgs e)
        {
            txtContent.Text = ChangeModule;
        }
    }
}