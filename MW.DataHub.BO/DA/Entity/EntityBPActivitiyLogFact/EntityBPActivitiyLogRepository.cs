using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPActivitiyLogRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPActivitiyLogRepository(){}
		public EntityBPActivitiyLogRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPActivitiyLog entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Insert into BPActivitiyLog([ProjectID],[TaskID],[RunStartDT],[RunEndDT],[RunStatus],[RunResult])values(@ProjectID,@TaskID,@RunStartDT,@RunEndDT,@RunStatus,@RunResult)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPActivitiyLog entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Update BPActivitiyLog Set [ProjectID]=@ProjectID,[TaskID]=@TaskID,[RunStartDT]=@RunStartDT,[RunEndDT]=@RunEndDT,[RunStatus]=@RunStatus,[RunResult]=@RunResult where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPActivitiyLog entity = new EntityBPActivitiyLog();
			string strSQL="Delete from BPActivitiyLog";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPActivitiyLog entity)
		{
			string strSQL="Delete from BPActivitiyLog";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPActivitiyLog GetEntityByID(int iHQID)
		{
			EntityBPActivitiyLog entity = new EntityBPActivitiyLog();
			string strSQL="Select * from BPActivitiyLog";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPActivitiyLog GetEntityByPK(EntityBPActivitiyLog entity)
		{
			string strSQL="Select * from BPActivitiyLog";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPActivitiyLog GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPActivitiyLog entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPActivitiyLog> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPActivitiyLog> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPActivitiyLog entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPActivitiyLog entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPActivitiyLog entity)
		{
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "TaskID", DbType.Int32, entity.TaskID);
			this.DB.AddInParameter(command, "RunStartDT", DbType.DateTime, entity.RunStartDT);
			this.DB.AddInParameter(command, "RunEndDT", DbType.DateTime, entity.RunEndDT);
			this.DB.AddInParameter(command, "RunStatus", DbType.Boolean, entity.RunStatus);
			this.DB.AddInParameter(command, "RunResult", DbType.String, entity.RunResult);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPActivitiyLog entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPActivitiyLog BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPActivitiyLog> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPActivitiyLog> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPActivitiyLog> lists = new List<EntityBPActivitiyLog>();
			while (idr.Read())
			{
				EntityBPActivitiyLog entity = new EntityBPActivitiyLog();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("ProjectID");
				if (!idr.IsDBNull(index))
				{
					entity.ProjectID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("TaskID");
				if (!idr.IsDBNull(index))
				{
					entity.TaskID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("RunStartDT");
				if (!idr.IsDBNull(index))
				{
					entity.RunStartDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("RunEndDT");
				if (!idr.IsDBNull(index))
				{
					entity.RunEndDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("RunStatus");
				if (!idr.IsDBNull(index))
				{
					entity.RunStatus = idr.GetBoolean(index);
				}

				index = idr.GetOrdinal("RunResult");
				if (!idr.IsDBNull(index))
				{
					entity.RunResult = idr.GetString(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
