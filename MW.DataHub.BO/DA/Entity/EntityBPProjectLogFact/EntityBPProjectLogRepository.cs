using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPProjectLogRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPProjectLogRepository(){}
		public EntityBPProjectLogRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPProjectLog entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @LogType=convert(nvarchar(100),@LogType)";
			strSQL +=" Set @KeyValue=convert(nvarchar(200),@KeyValue)";
			strSQL +=" Set @Source=convert(nvarchar(100),@Source)";
			strSQL +=" Insert into BPProjectLog([LogTime],[LogType],[KeyValue],[Log],[Source],[ProjectID])values(@LogTime,@LogType,@KeyValue,@Log,@Source,@ProjectID)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPProjectLog entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @LogType=convert(nvarchar(100),@LogType)";
			strSQL +=" Set @KeyValue=convert(nvarchar(200),@KeyValue)";
			strSQL +=" Set @Source=convert(nvarchar(100),@Source)";
			strSQL +=" Update BPProjectLog Set [LogTime]=@LogTime,[LogType]=@LogType,[KeyValue]=@KeyValue,[Log]=@Log,[Source]=@Source,[ProjectID]=@ProjectID where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPProjectLog entity = new EntityBPProjectLog();
			string strSQL="Delete from BPProjectLog";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPProjectLog entity)
		{
			string strSQL="Delete from BPProjectLog";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPProjectLog GetEntityByID(int iHQID)
		{
			EntityBPProjectLog entity = new EntityBPProjectLog();
			string strSQL="Select * from BPProjectLog";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPProjectLog GetEntityByPK(EntityBPProjectLog entity)
		{
			string strSQL="Select * from BPProjectLog";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPProjectLog GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPProjectLog entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPProjectLog> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPProjectLog> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPProjectLog entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPProjectLog entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPProjectLog entity)
		{
			this.DB.AddInParameter(command, "LogTime", DbType.DateTime, entity.LogTime);
			this.DB.AddInParameter(command, "LogType", DbType.String, entity.LogType);
			this.DB.AddInParameter(command, "KeyValue", DbType.String, entity.KeyValue);
			this.DB.AddInParameter(command, "Log", DbType.String, entity.Log);
			this.DB.AddInParameter(command, "Source", DbType.String, entity.Source);
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPProjectLog entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPProjectLog BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPProjectLog> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPProjectLog> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPProjectLog> lists = new List<EntityBPProjectLog>();
			while (idr.Read())
			{
				EntityBPProjectLog entity = new EntityBPProjectLog();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("LogTime");
				if (!idr.IsDBNull(index))
				{
					entity.LogTime = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("LogType");
				if (!idr.IsDBNull(index))
				{
					entity.LogType = idr.GetString(index);
				}

				index = idr.GetOrdinal("KeyValue");
				if (!idr.IsDBNull(index))
				{
					entity.KeyValue = idr.GetString(index);
				}

				index = idr.GetOrdinal("Log");
				if (!idr.IsDBNull(index))
				{
					entity.Log = idr.GetString(index);
				}

				index = idr.GetOrdinal("Source");
				if (!idr.IsDBNull(index))
				{
					entity.Source = idr.GetString(index);
				}

				index = idr.GetOrdinal("ProjectID");
				if (!idr.IsDBNull(index))
				{
					entity.ProjectID = idr.GetInt32(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
