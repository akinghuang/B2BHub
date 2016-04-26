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
    public partial class wucActivitiesMonitor : System.Web.UI.UserControl
    {
        IBPActivities Activities = BO.BOFactory.GetBPActivities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPageSize();
                BindActivities();
            }
            if (txtIsRefresh.Text == "IsRefresh")
            {
                BindActivities();
                txtIsRefresh.Text = "";
            }
        }

 
       

        void BindActivities()
        {
            int PageCount = 0;
            int PageSize = int.Parse(ddlPageSize.SelectedValue);
            int PageIndex = int.Parse(ddlPageIndex.SelectedValue == "" ? "1" : ddlPageIndex.SelectedValue);
            string ProjectName = txtProjectName.Text.Trim();
            string Status = ddlStatus.SelectedValue;
            bool Success = !cbShow.Checked;
            DataTable dt = Activities.GetActivitiesList(ProjectName, Status, Success,ref PageCount,PageSize,PageIndex); 
            rptactivities.DataSource = dt;
            rptactivities.DataBind();
            BindPageIndex(PageCount, PageIndex);
        }

        void BindPageSize()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageSize");
            for (int i = 1; i <= 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["PageSize"] = i * 10;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ddlPageSize.DataSource = dt;
            ddlPageSize.DataTextField = "PageSize";
            ddlPageSize.DataValueField = "PageSize";
            ddlPageSize.DataBind();
            ddlPageSize.SelectedValue = "50";
            ddlPageSize1.DataSource = dt;
            ddlPageSize1.DataTextField = "PageSize";
            ddlPageSize1.DataValueField = "PageSize";
            ddlPageSize1.DataBind();
            ddlPageSize1.SelectedValue = "50";
        }


        void BindPageIndex(int PageCount, int PageIndex)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PageIndex");
            for (int i = 1; i <= PageCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr["PageIndex"] = i;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ddlPageIndex.DataSource = dt;
            ddlPageIndex.DataTextField = "PageIndex";
            ddlPageIndex.DataValueField = "PageIndex";
            ddlPageIndex.DataBind();
            if (ddlPageIndex.Items.Count >= PageIndex)
            {
                ddlPageIndex.SelectedValue = PageIndex.ToString();
            }
            ddlPageIndex1.DataSource = dt;
            ddlPageIndex1.DataTextField = "PageIndex";
            ddlPageIndex1.DataValueField = "PageIndex";
            ddlPageIndex1.DataBind();
            if (ddlPageIndex1.Items.Count >= PageIndex)
            {
                ddlPageIndex1.SelectedValue = PageIndex.ToString();
            }
        }

        protected void btSearch_OnClick(object sender, EventArgs e)
        {
            BindActivities();
        }

        protected void ddlPageIndex_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindActivities();
        }

        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindActivities();
        }
    }
}