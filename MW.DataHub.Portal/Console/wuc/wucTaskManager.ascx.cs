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
    public partial class wucTaskManager : wucMasterControl
    {
        protected int ProjectID = 0;
        IBPProjectTask taskManager = BOFactory.GetBPProjectTask();
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            if (!IsPostBack)
            {
                BindTaskList();
            }
        }
        void BindTaskList()
        {
            DataTable dt = taskManager.getTaskListByProjectID(ProjectID);
            rptTaskList.DataSource = dt;
            rptTaskList.DataBind();
        }


        protected void rptTaskList_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "Delete":
                    int TaskID = Convert.ToInt32(e.CommandArgument.ToString());
                    taskManager.updateTaskListByID(TaskID);
                    BindTaskList();
                    break;
            }
        }
    }
}