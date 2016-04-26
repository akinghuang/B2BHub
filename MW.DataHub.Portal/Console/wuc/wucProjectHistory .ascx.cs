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
    public partial class wucProjectHistory : wucMasterControl
    {
        int ProjectID = -1;
        IBPActivitiyLog ActivitiyLog = BOFactory.GetIBPActivitiesLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            if (!IsPostBack)
            {
                BindPageSize();
                BindProjectHistory();
            }
        }

        void BindProjectHistory()
        {
            int PageCount = 0;
            int PageSize = int.Parse(ddlPageSize.SelectedValue);
            int PageIndex = int.Parse(ddlPageIndex.SelectedValue == "" ? "1" : ddlPageIndex.SelectedValue);
            DateTime DTStart = Convert.ToDateTime(txtDateTStart.Text == "" ? "2000-01-01" : txtDateTStart.Text.Trim());
            DateTime DTEnd = Convert.ToDateTime(txtDateTEnd.Text == "" ? "3000-01-01" : txtDateTEnd.Text.Trim()).AddDays(1);
            DataTable dt = ActivitiyLog.GetActivitiesLogByProjectID(ProjectID, DTStart, DTEnd, ref PageCount, PageSize, PageIndex);
            rptProjectHistory.DataSource = dt;
            rptProjectHistory.DataBind();
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
            BindProjectHistory();
        }

        protected void ddlPageIndex_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjectHistory();
        }

        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjectHistory();
        }
    }
}