using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.API.BO
{
	public partial interface IBPMailFailed : IBase<Entity.EntityBPMailFailed>
	{
		Entity.EntityBPMailFailed GetEntityByPK(Guid MailGUID,int ProjectID);
		void RemoveEntityByPK(Guid MailGUID,int ProjectID);
	}

	partial class clsBPMailFailed : clsBase<Entity.EntityBPMailFailed>, IBPMailFailed
	{
		Entity.EntityBPMailFailedRepository objRep = new Entity.EntityBPMailFailedRepository();
		public clsBPMailFailed()
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
		public override Entity.EntityBPMailFailed GetSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			return objRep.GetEntityByID(vHQID);
		}
		public override void RemoveSQLEntityByHQID(int vHQID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			objRep.Remove(vHQID);
		}
		public override List<Entity.EntityBPMailFailed> GetSQLEntitiesByDatatable(DataTable ldt)
		{
			List<Entity.EntityBPMailFailed> lists = objRep.GetEntityListByDataTable(ldt);
			return lists;
		}
		public override Entity.EntityBPMailFailed GetSQLEntityByPK(Entity.EntityBPMailFailed objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPMailFailed entity = objEntity;
			return objRep.GetEntityByPK(entity);
		}
		public Entity.EntityBPMailFailed GetEntityByPK(Guid vMailGUID,int vProjectID)
		{
			Entity.EntityBPMailFailed entity = new Entity.EntityBPMailFailed();
			entity.MailGUID = vMailGUID;
			entity.ProjectID = vProjectID;
			return this.GetSQLEntityByPK(entity);
		}
		public override void RemoveSQLEntityByPK(Entity.EntityBPMailFailed objEntity)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			Entity.EntityBPMailFailed entity = objEntity;
			objRep.Remove(entity);
		}
		public void RemoveEntityByPK(Guid vMailGUID,int vProjectID)
		{
			Entity.EntityBPMailFailed entity = new Entity.EntityBPMailFailed();
			entity.MailGUID = vMailGUID;
			entity.ProjectID = vProjectID;
			objRep.Remove(entity);
		}
		//protected override void SaveSQLDataAdd(Entity.EntityBPMailFailed entity, string LogonID)
		//{
		//	entity.CreatedBy = LogonID;
		//	entity.CreatedDate = this.CurrentDateTime;
		//	entity.UpdatedBy = entity.CreatedBy;
		//	entity.UpdatedDate = entity.CreatedDate;
		//}
		//protected override void SaveSQLDataSave(Entity.EntityBPMailFailed entity, string LogonID)
		//{
		//	entity.UpdatedBy = LogonID;
		//	entity.UpdatedDate = this.CurrentDateTime;
		//}
		public override int SaveSQLData(Entity.EntityBPMailFailed objEntity, string LogonID)
		{
			objRep.DB.Transaction = this.DB.Transaction;
			string strMSG = "";
			if (this.SaveSQLDataCheckPrimaryKey(objEntity, ref strMSG))
			{
				throw new Exception(strMSG);
			}
			Entity.EntityBPMailFailed entity = objEntity;
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
