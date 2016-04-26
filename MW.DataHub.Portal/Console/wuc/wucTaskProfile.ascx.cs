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
using MW.DataHub.BO.Entity;

namespace MW.DataHub.Portal.Console.wuc
{
    public partial class wucTaskProfile : wucMasterControl
    {
        int TaskID = -1;
        int ProjectID = -1;
        EntityBPProjectTask entityTask = new EntityBPProjectTask();
        IBPProjectTask  task=BOFactory.GetBPProjectTask();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkOnline();
            TaskID = string.IsNullOrEmpty(Request.QueryString["ID"]) ? -1 : Convert.ToInt32(Request.QueryString["ID"]);
            ProjectID = string.IsNullOrEmpty(Request.QueryString["ProjectID"]) ? -1 : Convert.ToInt32(Request.QueryString["ProjectID"]);
            if (!IsPostBack)
            {
                BindDDlMonth();
                if (TaskID > 0)
                {
                    tbSave.Text = "Save Change";
                    BtSave1.Text = "Save Change";
                    BindTaskData();
                }
                else
                {
                    tbSave.Text = "Create Task";
                    BtSave1.Text = "Create Task";
                }
            }
        }

        void BindTaskData()
        {
            entityTask.ID = TaskID;
            entityTask = task.getTaskListByTaskID(entityTask);
            txtSequence.Text = entityTask.Sequence.ToString();
            txtTaskName.Text = entityTask.TaskName;
            txtDesc.Text = entityTask.TaskDesc;
            radioList.SelectedValue = entityTask.Status;
            ddlProtocol.SelectedValue = entityTask.Protocol;
            radioListProtocol.SelectedValue = entityTask.IO;
            txtFileType.Text = entityTask.FileExt;
            txtRunTimeParas.Text = entityTask.RuntimeParas;
            txtMHandl.Text = entityTask.MsgHandler;
            txtBHandler.Text = entityTask.BizHandler;
            txtServer.Text = entityTask.RServer;
            txtFolder.Text = entityTask.RFolder;
            txtLFolder.Text = entityTask.LFolder;
            txtUserID.Text = entityTask.RUID;
            txtPort.Text = entityTask.RPort.ToString();
            txtPassword.Text = entityTask.RPWD;
            txtCertificate.Text = entityTask.RCert;
            radioListTimeType.SelectedValue = entityTask.ScheduleType;
            txtDaily.Text=entityTask.ScheduleTime;
            ddlMonth.SelectedValue = entityTask.ScheduleMonth.ToString();
            ddlWeek.SelectedValue =entityTask.ScheduleWeekDay;
            txtEvery.Text= entityTask.ScheduleInterval.ToString();
            txtMonthly.Text = entityTask.ScheduleTime;
            txtWeek.Text = entityTask.ScheduleTime;
        }

        void BindDDlMonth()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("day", typeof(int));
            for (int i = 1; i <= 31; i++)
            {
                DataRow dr = dt.NewRow();
                dr["day"] = i;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ddlMonth.DataSource = dt;
            ddlMonth.DataTextField = "day";
            ddlMonth.DataValueField = "day";
            ddlMonth.DataBind();
        }

        protected void BtSave_OnClick(object sender, EventArgs e)
        {
            entityTask.ID = TaskID;
            entityTask.ProjectID = ProjectID;
            entityTask.Sequence = Convert.ToInt32(txtSequence.Text.Trim());
            entityTask.Status = radioList.SelectedValue;
            entityTask.TaskName = txtTaskName.Text.Trim();
            entityTask.TaskDesc = txtDesc.Text.Trim();
            entityTask.IO = radioListProtocol.SelectedValue;
            entityTask.Protocol = ddlProtocol.SelectedValue;
            entityTask.FileExt = txtFileType.Text.Trim();
            entityTask.RuntimeParas = txtRunTimeParas.Text.Trim();
            entityTask.MsgHandler = txtMHandl.Text.Trim();
            entityTask.BizHandler = txtBHandler.Text.Trim();
            entityTask.IO = radioListProtocol.SelectedValue;
            entityTask.CreatedDT = DateTime.Now;
            if (entityTask.Protocol == "FTP")
            {
                entityTask.RServer = txtServer.Text.Trim();
                entityTask.RFolder = txtFolder.Text.Trim();
                entityTask.RPort = Convert.ToInt32(txtPort.Text.Trim());
                entityTask.RUID = txtUserID.Text.Trim();
                entityTask.RPWD = txtPassword.Text.Trim();
            }
            else if (entityTask.Protocol == "FTPs")
            {
                entityTask.RServer = txtServer.Text.Trim();
                entityTask.RFolder = txtFolder.Text.Trim();
                entityTask.RPort = Convert.ToInt32(txtPort.Text.Trim());
                entityTask.RUID = txtUserID.Text.Trim();
                entityTask.RPWD = txtPassword.Text.Trim();
                entityTask.RCert = txtCertificate.Text.Trim();
            }
            else if (entityTask.Protocol == "sFTP")
            {
                entityTask.RServer = txtServer.Text.Trim();
                entityTask.RFolder = txtFolder.Text.Trim();
                entityTask.RPort = Convert.ToInt32(txtPort.Text.Trim());
                entityTask.RUID = txtUserID.Text.Trim();
                entityTask.RPWD = txtPassword.Text.Trim();
                entityTask.RCert = txtCertificate.Text.Trim();
            }
            else if (entityTask.Protocol == "Local")
            {
                entityTask.RFolder = txtFolder.Text.Trim();
                entityTask.RPWD = txtPassword.Text.Trim();
            }
            else if (entityTask.Protocol == "HTTP")
            {
                entityTask.RServer = txtServer.Text.Trim();
                entityTask.RPort = Convert.ToInt32(txtPort.Text.Trim());
                entityTask.RUID = txtUserID.Text.Trim();
                entityTask.RPWD = txtPassword.Text.Trim();
            }
            else if (entityTask.Protocol == "HTTPs")
            {
                entityTask.RServer = txtServer.Text.Trim();
                entityTask.RPort = Convert.ToInt32(txtPort.Text.Trim());
                entityTask.RUID = txtUserID.Text.Trim();
                entityTask.RPWD = txtPassword.Text.Trim();
                entityTask.RCert = txtCertificate.Text.Trim();
            }
            entityTask.LFolder = txtLFolder.Text.Trim();
            entityTask.ScheduleType = radioListTimeType.SelectedValue;
            if (entityTask.ScheduleType == "Every")
            {

                entityTask.ScheduleInterval = Convert.ToInt32(txtEvery.Text.Trim());
            }
            else if (entityTask.ScheduleType == "Daily")
            {
                entityTask.ScheduleTime = txtDaily.Text.Trim();

            }
            else if (entityTask.ScheduleType == "Week")
            {
                entityTask.ScheduleTime = txtWeek.Text.Trim();
                entityTask.ScheduleWeekDay =ddlWeek.SelectedValue;

            }
            else if (entityTask.ScheduleType == "Monthly")
            {
                entityTask.ScheduleTime = txtMonthly.Text.Trim();
                entityTask.ScheduleMonth =Convert.ToInt32(ddlMonth.SelectedValue);
            }
            task.SaveSQLData(entityTask, UserID.ToString());
            this.windowhref1("../../Console/TasksManager.aspx?ProjectID="+ProjectID);
        }
    }
}