using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.BO
{
	public partial interface IBPProjectLog : IBase<Entity.EntityBPProjectLog>
	{
		
	}

	partial class clsBPProjectLog : clsBase<Entity.EntityBPProjectLog>, IBPProjectLog
	{
		Entity.EntityBPProjectLogRepository objRep = new Entity.EntityBPProjectLogRepository();
		public clsBPProjectLog()
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
		public override Entity.EntityBPProjectLog GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPProjectLog> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPProjectLog> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
        //public override Entity.EntityBPProjectLog GetSQLEntityByPK(Entity.EntityBPProjectLog objEntity)
        //{
        //    objRep.DB.Transaction = this.DB.Transaction;
        //    Entity.EntityBPProjectLog entity = objEntity;
        //    return objRep.GetEntityByPK(entity);
        //}
        //public Entity.EntityBPProjectLog GetEntityByPK(int vID)
        //{
        //    Entity.EntityBPProjectLog entity = new Entity.EntityBPProjectLog();
        //    entity.ID = vID;
        //    return this.GetSQLEntityByPK(entity);
        //}
        //public override void RemoveSQLEntityByPK(Entity.EntityBPProjectLog objEntity)
        //{
        //    objRep.DB.Transaction = this.DB.Transaction;
        //    Entity.EntityBPProjectLog entity = objEntity;
        //    objRep.Remove(entity);
        //}
        //public void RemoveEntityByPK(int vID)
        //{
        //    Entity.EntityBPProjectLog entity = new Entity.EntityBPProjectLog();
        //    entity.ID = vID;
        //    objRep.Remove(entity);
        //}
		//protected override void SaveSQLDataAdd(Entity.EntityBPProjectLog entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPProjectLog entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}
        //public override int SaveSQLData(Entity.EntityBPProjectLog objEntity, string LogonID)
        //{
        //    objRep.DB.Transaction = this.DB.Transaction;
        //    string strMSG = "";
        //    if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
        //    {
        //        throw new Exception(strMSG);
        //    }
        //    Entity.EntityBPProjectLog entity = objEntity;
        //    if (entity.ID > 0)
        //    {
        //        SaveSQLDataSave(entity, LogonID);
        //        objRep.Save(entity);
        //    }
        //    else
        //    {
        //        SaveSQLDataAdd(entity, LogonID);
        //        objRep.Add(entity);
        //    }
        //    return entity.ID;
        //}

	}
}
