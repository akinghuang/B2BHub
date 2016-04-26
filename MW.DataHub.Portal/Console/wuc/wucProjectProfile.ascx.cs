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
    public partial class wucProjectProfile : wucMasterControl
    {
        int ProjectID = -1;
        string ChangeModule = "";
        BO.Entity.EntityBPProject entityBPProject = new BO.Entity.EntityBPProject();
        BO.IBPProject BPProject = BOFactory.GetBPProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkOnline();
            ProjectID = string.IsNullOrEmpty(Request.QueryString["ProjectID"]) ? -1 : Convert.ToInt32(Request.QueryString["ProjectID"]);
            GetChangeModule();
            if (!IsPostBack)
            {
                if (ProjectID > 0)
                {
                    BindProjectDate();
                    btSave.Text = "Save Change";
                    btSave1.Text = "Save Change";
                }
                else
                {
                    txtSequence.Text = null;
                    txtProjectName.Text = null;
                    txtOwner.Text = null;
                    txtProcessID.Text = null;
                    txtHostMachineID.Text = null;
                    txtDescription.Text = ChangeModule;
                    txtParameters.Text = null;
                    txtAdminstrator.Text = null;
                    txtUser.Text = null;
                    radioList.SelectedValue = "Active";
                    btSave.Text = "Create Project";
                    btSave1.Text = "Create Project";
                }
            }
        }

        void GetChangeModule()
        {
            string path = Server.MapPath("../BP_ProjectRequirement.htm");
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
                ChangeModule = sr.ReadToEnd();
                sr.Close();
            }
        }

        void BindProjectDate()
        {
            entityBPProject.ID = ProjectID;
            entityBPProject = BPProject.getProjectByID(entityBPProject);
            txtSequence.Text = entityBPProject.Sequence.ToString();
            radioList.SelectedValue = entityBPProject.Status;
            txtProjectName.Text = entityBPProject.ProjectName;
            txtOwner.Text = entityBPProject.Owner;
            txtProcessID.Text = entityBPProject.ProcessID;
            txtHostMachineID.Text = entityBPProject.HostMachineID;
            txtDescription.Text = entityBPProject.ProjectDesc;
            txtParameters.Text = entityBPProject.RuntimeParas;
            txtAdminstrator.Text = entityBPProject.AdminMail;
            
            txtUser.Text = entityBPProject.UserMail;
        }

        protected void btSave_OnClick(object sender, EventArgs e)
        {
            if (ProjectID > 0)
            {
                entityBPProject.ID = ProjectID;
                entityBPProject.Sequence = Convert.ToInt32(txtSequence.Text.Trim());
                entityBPProject.Status = radioList.SelectedValue;
                entityBPProject.ProjectName = txtProjectName.Text.Trim();
                entityBPProject.Owner = txtOwner.Text.Trim();
                entityBPProject.ProjectDesc = txtDescription.Text.Trim();
                entityBPProject.HostMachineID = txtHostMachineID.Text.Trim();
                entityBPProject.ProcessID = txtProcessID.Text.Trim();
                entityBPProject.RuntimeParas = txtParameters.Text.Trim();
                entityBPProject.AdminMail = txtAdminstrator.Text.Trim();
                entityBPProject.UserMail = txtUser.Text.Trim();
                BPProject.updateProjectList(entityBPProject, UserID);
            }
            else
            {
                entityBPProject.Sequence = Convert.ToInt32(txtSequence.Text.Trim());
                entityBPProject.Status = radioList.SelectedValue;
                entityBPProject.ProjectName = txtProjectName.Text.Trim();
                entityBPProject.Owner = txtOwner.Text.Trim();
                entityBPProject.ProjectDesc = txtDescription.Text.Trim();
                entityBPProject.HostMachineID = txtHostMachineID.Text.Trim();
                entityBPProject .ProcessID= txtProcessID.Text.Trim();
                entityBPProject.RuntimeParas = txtParameters.Text.Trim();
                entityBPProject.AdminMail = txtAdminstrator.Text.Trim();
                entityBPProject.UserMail = txtUser.Text.Trim();

                BPProject.AddProjectList(entityBPProject, UserID);
            }
            
            Response.Write("<script>var wind=window.parent;while(wind){if(wind.logClick){wind.urlChange('Console/ProjectManager.aspx');break;}else{wind=wind.parent;}}</script>");
        }

        protected void btReloadTemp_OnClick(object sender, EventArgs e)
        {
            txtDescription.Text = ChangeModule;
        }
    }
}