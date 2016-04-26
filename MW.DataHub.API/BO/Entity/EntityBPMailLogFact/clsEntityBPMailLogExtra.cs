using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.API.BO
{
	public partial interface IBPMailLog : IBase<Entity.EntityBPMailLog>
	{
		
	}

	partial class clsBPMailLog : clsBase<Entity.EntityBPMailLog>, IBPMailLog
	{
		
		//protected override void SaveSQLDataAdd(Entity.EntityBPMailLog entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPMailLog entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}
		

	}
}
