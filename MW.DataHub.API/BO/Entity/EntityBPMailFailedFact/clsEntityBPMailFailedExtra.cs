using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.API.BO
{
	public partial interface IBPMailFailed : IBase<Entity.EntityBPMailFailed>
	{
		
	}

	partial class clsBPMailFailed : clsBase<Entity.EntityBPMailFailed>, IBPMailFailed
	{

        protected override void SaveSQLDataAdd(Entity.EntityBPMailFailed entity, string LogonID)
        {
            entity.CreatedBy = LogonID;
            entity.CreatedDate = this.CurrentDateTime;
            entity.SendBy = entity.CreatedBy;
            entity.SendDate = entity.CreatedDate;
        }
        protected override void SaveSQLDataSave(Entity.EntityBPMailFailed entity, string LogonID)
        {
            entity.SendBy = LogonID;
            entity.SendDate= this.CurrentDateTime;
        }

        protected override string ProcessSQLString(DIMERCO.SDK2.Base.ProcessCommandTypes commandTypes, MW.DataHub.API.BO.Entity.EntityBPMailFailed objEntity)
        {
            StringBuilder lsb = new StringBuilder();
            switch (commandTypes)
            {
                case DIMERCO.SDK2.Base.ProcessCommandTypes.BySearchFunction:
                    lsb.Remove(0, lsb.Length);
                    lsb.Append("Select * from BPMailFailed");
                    break;
            }

            return lsb.ToString();
        }
		

	}
}
