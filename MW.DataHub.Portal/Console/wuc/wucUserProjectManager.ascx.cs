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
    public partial class wucUserProjectManager : wucMasterControl
    {
        BO.Entity.EntityBPUser entity = new BO.Entity.EntityBPUser();
        IBPUser User = BOFactory.GetBPUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkOnline();
            if (!IsPostBack)
            {
                BindUserManager();
            }
        }

        void BindUserManager()
        {
            string UserID = txtUserID.Text.Trim();
            string UserName = txtName.Text.Trim();
            string Status = ddlStatus.SelectedValue;
            DataTable dt = User.getBPUserList(UserID,UserName,Status);
            rptUserManager.DataSource = dt;
            rptUserManager.DataBind();
        }

        protected void btSearch_OnClick(object sender, EventArgs e)
        {
            BindUserManager();
        }

        protected void btSave_OnClick(object sender, EventArgs e)
        {
            entity.ID =hfID.Value==""?0:Convert.ToInt32( hfID.Value);
            entity.UserID = textuserID.Text.Trim();
            entity.FullName = textuserName.Text.Trim();
            entity.Password = txtPassword.Text.Trim();
            entity.Comments = txtDesc.Text.Trim();
            entity.Status = rbStatusList.SelectedValue;
            User.SaveSQLData(entity,"");
            BindUserManager();
        }

        protected void rptUserManager_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "Delete":
                    User.RemoveEntityByPK(Convert.ToInt32(e.CommandArgument.ToString()));
                    BindUserManager();
                    break;
            }
        }
    }
}