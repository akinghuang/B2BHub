using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.API.BO
{
	public partial interface IBPMailLog : IBase<Entity.EntityBPMailLog>
	{
		Entity.EntityBPMailLog GetEntityByPK(int ID);
		void RemoveEntityByPK(int ID);
	}

	partial class clsBPMailLog : clsBase<Entity.EntityBPMailLog>, IBPMailLog
	{
		Entity.EntityBPMailLogRepository objRep = new Entity.EntityBPMailLogRepository();
		public clsBPMailLog()
		{
			if (DB.TrustSQL)
			{
				objRep.DB.ServerIP = this.DB.ServerIP;
				objRep.DB.DBName = this.DB.DBName;
				objRep.DB.TrustSQL = DB.TrustSQL;
			}
			else if (!(string.IsNullOrEmpty(DB.ServerIP) || string.IsNullOrEmpty(DB.DBName) || string.IsNullOrEmpty(DB.UserID)))
			{
				objRep.DB.ServerIP = this.DB.ServerIP;
				objRep.DB.DBName = this.DB.DBName;
				objRep.DB.UserID = this.DB.UserID;
				objRep.DB.UserPassword = this.DB.UserPassword;
			}
			else if (!string.IsNullOrEmpty(DB.DatabaseName))
				objRep.DB.DatabaseName = DB.DatabaseName;
			else
				objRep.DB = this.DB;
		}
		public override Entity.EntityBPMailLog GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPMailLog> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPMailLog> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
		public override Entity.EntityBPMailLog GetSQLEntityByPK(Entity.EntityBPMailLog objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPMailLog entity = objEntity;
			return objRep.GetEntityByPK(entity);
		}
		public Entity.EntityBPMailLog GetEntityByPK(int vID)
		{
			Entity.EntityBPMailLog entity = new Entity.EntityBPMailLog();
			entity.ID = vID;
			return this.GetSQLEntityByPK(entity);
		}
		public override void RemoveSQLEntityByPK(Entity.EntityBPMailLog objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPMailLog entity = objEntity;
			objRep.Remove(entity);
		}
		public void RemoveEntityByPK(int vID)
		{
			Entity.EntityBPMailLog entity = new Entity.EntityBPMailLog();
			entity.ID = vID;
			objRep.Remove(entity);
		}
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
		public override int SaveSQLData(Entity.EntityBPMailLog objEntity, string LogonID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			string strMSG = "";
			if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
			{
				throw new Exception(strMSG);
			}
			Entity.EntityBPMailLog entity = objEntity;
			if (entity.ID > 0)
			{
				SaveSQLDataSave(entity, LogonID);
				objRep.Save(entity);
			}
			else
			{
				SaveSQLDataAdd(entity, LogonID);
				objRep.Add(entity);
			}
			return entity.ID;
		}

	}
}
