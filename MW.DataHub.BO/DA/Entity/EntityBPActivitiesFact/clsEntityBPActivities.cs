using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.BO
{
	public partial interface IBPActivities : IBase<Entity.EntityBPActivities>
	{
		Entity.EntityBPActivities GetEntityByPK(int ProjectID,int TaskID);
		void RemoveEntityByPK(int ProjectID,int TaskID);
	}

	partial class clsBPActivities : clsBase<Entity.EntityBPActivities>, IBPActivities
	{
		Entity.EntityBPActivitiesRepository objRep = new Entity.EntityBPActivitiesRepository();
		public clsBPActivities()
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
		public override Entity.EntityBPActivities GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPActivities> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPActivities> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
		public override Entity.EntityBPActivities GetSQLEntityByPK(Entity.EntityBPActivities objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPActivities entity = objEntity;
			return objRep.GetEntityByPK(entity);
		}
		public Entity.EntityBPActivities GetEntityByPK(int vProjectID,int vTaskID)
		{
			Entity.EntityBPActivities entity = new Entity.EntityBPActivities();
			entity.ProjectID = vProjectID;
			entity.TaskID = vTaskID;
			return this.GetSQLEntityByPK(entity);
		}
		public override void RemoveSQLEntityByPK(Entity.EntityBPActivities objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPActivities entity = objEntity;
			objRep.Remove(entity);
		}
		public void RemoveEntityByPK(int vProjectID,int vTaskID)
		{
			Entity.EntityBPActivities entity = new Entity.EntityBPActivities();
			entity.ProjectID = vProjectID;
			entity.TaskID = vTaskID;
			objRep.Remove(entity);
		}
		//protected override void SaveSQLDataAdd(Entity.EntityBPActivities entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPActivities entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}

        //public override int SaveSQLData(Entity.EntityBPActivities objEntity, string LogonID)
        //{
        //    objRep.DB.Transaction = this.DB.Transaction;
        //    string strMSG = "";
        //    if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
        //    {
        //        throw new Exception(strMSG);
        //    }
        //    Entity.EntityBPActivities entity = objEntity;
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
