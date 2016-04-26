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
    public partial class wucNotification : wucMasterControl
    {
        int ProjectID = -1;
        IBPMailLog MailLog = BOFactory.GetBPProjectMailLog();
        BO.Entity.EntityBPMailAttach entityAttach = new BO.Entity.EntityBPMailAttach();
        IBPMailAttach MailAttach = BOFactory.GetBPMailAttach();
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            if (!IsPostBack)
            {
                BindPageSize();
                BindProjectNtf();
            }
            else
            {
                string Attach = hfAttach.Value;
                if (Attach != "")
                {
                    DownloadFild(Attach);
                }
                hfAttach.Value = "";
            }

        }

        void BindProjectNtf()
        {
            int PageCount = 0;
            int PageSize = int.Parse(ddlPageSize.SelectedValue);
            int PageIndex = int.Parse(ddlPageIndex.SelectedValue == "" ? "1" : ddlPageIndex.SelectedValue);
            DateTime DTStart = Convert.ToDateTime(txtDateTStart.Text == "" ? "2000-01-01" : txtDateTStart.Text.Trim());
            DateTime DTEnd = Convert.ToDateTime(txtDateEnd.Text == "" ? "3000-01-01" : txtDateEnd.Text.Trim()).AddDays(1);
            DateTime DTNowStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime DTNowEnd = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 0, 0, 0);
            string Subject = txtSubject.Text.Trim();
            if (cbIsToday.Checked)
            {
                DTStart = DTStart > DTNowStart ? DTStart : DTNowStart;
                DTEnd = DTEnd > DTNowEnd ? DTNowEnd : DTEnd;
            }
            string Content = txtContent.Text.Trim();
            DataTable dt = MailLog.GetProjectNtfList(ProjectID, DTStart, DTEnd, Subject, Content, ref PageCount, PageIndex, PageSize);
            rptProjectNtf.DataSource = dt;
            rptProjectNtf.DataBind();
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

        void DownloadFild(string Attach)
        {
            try
            {
                entityAttach = MailAttach.GetEntityByPK(Convert.ToInt32(Attach));
                string FileName = entityAttach.FileName;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(entityAttach.FileContent);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btSearch_OnClick(object sender, EventArgs e)
        {
            BindProjectNtf();
        }

        protected void ddlPageIndex_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjectNtf();
        }

        protected void ddlPageSize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjectNtf();
        }

    }
}