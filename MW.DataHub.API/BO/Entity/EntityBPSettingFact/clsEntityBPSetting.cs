using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.API.BO
{
	public partial interface IBPSetting : IBase<Entity.EntityBPSetting>
	{
		Entity.EntityBPSetting GetEntityByPK(int ProjectID,string SettingName);
		void RemoveEntityByPK(int ProjectID,string SettingName);
	}

	partial class clsBPSetting : clsBase<Entity.EntityBPSetting>, IBPSetting
	{
		Entity.EntityBPSettingRepository objRep = new Entity.EntityBPSettingRepository();
		public clsBPSetting()
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
		public override Entity.EntityBPSetting GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPSetting> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPSetting> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
		public override Entity.EntityBPSetting GetSQLEntityByPK(Entity.EntityBPSetting objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPSetting entity = objEntity;
			return objRep.GetEntityByPK(entity);
		}
		public Entity.EntityBPSetting GetEntityByPK(int vProjectID,string vSettingName)
		{
			Entity.EntityBPSetting entity = new Entity.EntityBPSetting();
			entity.ProjectID = vProjectID;
			entity.SettingName = vSettingName;
			return this.GetSQLEntityByPK(entity);
		}
		public override void RemoveSQLEntityByPK(Entity.EntityBPSetting objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPSetting entity = objEntity;
			objRep.Remove(entity);
		}
		public void RemoveEntityByPK(int vProjectID,string vSettingName)
		{
			Entity.EntityBPSetting entity = new Entity.EntityBPSetting();
			entity.ProjectID = vProjectID;
			entity.SettingName = vSettingName;
			objRep.Remove(entity);
		}
		//protected override void SaveSQLDataAdd(Entity.EntityBPSetting entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPSetting entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}
		public override int SaveSQLData(Entity.EntityBPSetting objEntity, string LogonID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			string strMSG = "";
			if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
			{
				throw new Exception(strMSG);
			}
			Entity.EntityBPSetting entity = objEntity;
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
