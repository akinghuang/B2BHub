using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.BO
{
	public partial interface IBPProjectTask : IBase<Entity.EntityBPProjectTask>
	{
		Entity.EntityBPProjectTask GetEntityByPK(int ID);
		void RemoveEntityByPK(int ID);
	}

	partial class clsBPProjectTask : clsBase<Entity.EntityBPProjectTask>, IBPProjectTask
	{
		Entity.EntityBPProjectTaskRepository objRep = new Entity.EntityBPProjectTaskRepository();
		public clsBPProjectTask()
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
		public override Entity.EntityBPProjectTask GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPProjectTask> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPProjectTask> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
		public override Entity.EntityBPProjectTask GetSQLEntityByPK(Entity.EntityBPProjectTask objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPProjectTask entity = objEntity;
			return objRep.GetEntityByPK(entity);
		}
		public Entity.EntityBPProjectTask GetEntityByPK(int vID)
		{
			Entity.EntityBPProjectTask entity = new Entity.EntityBPProjectTask();
			entity.ID = vID;
			return this.GetSQLEntityByPK(entity);
		}
		public override void RemoveSQLEntityByPK(Entity.EntityBPProjectTask objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPProjectTask entity = objEntity;
			objRep.Remove(entity);
		}
		public void RemoveEntityByPK(int vID)
		{
			Entity.EntityBPProjectTask entity = new Entity.EntityBPProjectTask();
			entity.ID = vID;
			objRep.Remove(entity);
		}
		//protected override void SaveSQLDataAdd(Entity.EntityBPProjectTask entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPProjectTask entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}
		public override int SaveSQLData(Entity.EntityBPProjectTask objEntity, string LogonID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			string strMSG = "";
			if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
			{
				throw new Exception(strMSG);
			}
			Entity.EntityBPProjectTask entity = objEntity;
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
