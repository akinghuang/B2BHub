using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.API.BO
{
	public partial interface IBPMailAttach : IBase<Entity.EntityBPMailAttach>
	{
        List<MW.DataHub.API.BO.Entity.EntityBPMailAttach> GetSQLEntityListByGUID(Guid GUID);
	}

	partial class clsBPMailAttach : clsBase<Entity.EntityBPMailAttach>, IBPMailAttach
	{

        protected override void SaveSQLDataAdd(Entity.EntityBPMailAttach entity, string LogonID)
        {
            //entity = LogonID;
            entity.CreatedDate = this.CurrentDateTime;
            //entity.UpdatedBy = entity.CreatedBy;
            //entity.UpdatedDate = entity.CreatedDate;
        }
        protected override void SaveSQLDataSave(Entity.EntityBPMailAttach entity, string LogonID)
        {
            //entity.UpdatedBy = LogonID;
            //entity.UpdatedDate = this.CurrentDateTime;
        }

        protected override string ProcessSQLString(DIMERCO.SDK2.Base.ProcessCommandTypes commandTypes, MW.DataHub.API.BO.Entity.EntityBPMailAttach objEntity)
        {
            string strSQL = "";
            switch (commandTypes)
            {
                case DIMERCO.SDK2.Base.ProcessCommandTypes.ByParentID:
                    strSQL = string.Format("Select * from BPMailAttach Where MailGUID='{0}'", objEntity.MailGUID);
                    break;
            }
            return strSQL;
        }

        public List<MW.DataHub.API.BO.Entity.EntityBPMailAttach> GetSQLEntityListByGUID(Guid GUID)
        {
            MW.DataHub.API.BO.Entity.EntityBPMailAttach entity = new MW.DataHub.API.BO.Entity.EntityBPMailAttach();
            entity.MailGUID = GUID;
            DataTable ldt = this.GetSQLDataSetByParentID(entity).Tables[0];

            //string strSQL = string.Format("Select * from BPMailAttach Where MailGUID='{0}'", GUID);
            //DataTable ldt = this.DB.ExecuteDataSet(strSQL).Tables[0];

            return this.GetSQLEntitiesByDatatable(ldt);
        }

	}
}
