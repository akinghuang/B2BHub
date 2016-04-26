using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.BO
{
	public partial interface IBPActivitiyLog : IBase<Entity.EntityBPActivitiyLog>
	{
		Entity.EntityBPActivitiyLog GetEntityByPK(int ID);
		void RemoveEntityByPK(int ID);
	}

	partial class clsBPActivitiyLog : clsBase<Entity.EntityBPActivitiyLog>, IBPActivitiyLog
	{
		Entity.EntityBPActivitiyLogRepository objRep = new Entity.EntityBPActivitiyLogRepository();
		public clsBPActivitiyLog()
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
		public override Entity.EntityBPActivitiyLog GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPActivitiyLog> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPActivitiyLog> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
		public override Entity.EntityBPActivitiyLog GetSQLEntityByPK(Entity.EntityBPActivitiyLog objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPActivitiyLog entity = objEntity;
			return objRep.GetEntityByPK(entity);
		}
		public Entity.EntityBPActivitiyLog GetEntityByPK(int vID)
		{
			Entity.EntityBPActivitiyLog entity = new Entity.EntityBPActivitiyLog();
			entity.ID = vID;
			return this.GetSQLEntityByPK(entity);
		}
		public override void RemoveSQLEntityByPK(Entity.EntityBPActivitiyLog objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPActivitiyLog entity = objEntity;
			objRep.Remove(entity);
		}
		public void RemoveEntityByPK(int vID)
		{
			Entity.EntityBPActivitiyLog entity = new Entity.EntityBPActivitiyLog();
			entity.ID = vID;
			objRep.Remove(entity);
		}
		//protected override void SaveSQLDataAdd(Entity.EntityBPActivitiyLog entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPActivitiyLog entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}
		public override int SaveSQLData(Entity.EntityBPActivitiyLog objEntity, string LogonID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			string strMSG = "";
			if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
			{
				throw new Exception(strMSG);
			}
			Entity.EntityBPActivitiyLog entity = objEntity;
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
