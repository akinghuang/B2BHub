using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPActivitiesRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPActivitiesRepository(){}
		public EntityBPActivitiesRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPActivities entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @Status=convert(nvarchar(20),@Status)";
			strSQL +=" Insert into BPActivities([ProjectID],[TaskID],[Status],[LastRunStartDT],[LastRunEndDT],[LastRunStatus],[LastRunResult],[LastSuccessDT],[RunFailTimes])values(@ProjectID,@TaskID,@Status,@LastRunStartDT,@LastRunEndDT,@LastRunStatus,@LastRunResult,@LastSuccessDT,@RunFailTimes)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPActivities entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @Status=convert(nvarchar(20),@Status)";
			strSQL +=" Update BPActivities Set [ProjectID]=@ProjectID,[TaskID]=@TaskID,[Status]=@Status,[LastRunStartDT]=@LastRunStartDT,[LastRunEndDT]=@LastRunEndDT,[LastRunStatus]=@LastRunStatus,[LastRunResult]=@LastRunResult,[LastSuccessDT]=@LastSuccessDT,[RunFailTimes]=@RunFailTimes where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPActivities entity = new EntityBPActivities();
			string strSQL="Delete from BPActivities";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPActivities entity)
		{
			string strSQL="Delete from BPActivities";
			strSQL +=" where ProjectID=@ProjectID and TaskID=@TaskID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPActivities GetEntityByID(int iHQID)
		{
			EntityBPActivities entity = new EntityBPActivities();
			string strSQL="Select * from BPActivities";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPActivities GetEntityByPK(EntityBPActivities entity)
		{
			string strSQL="Select * from BPActivities";
			strSQL +=" where ProjectID=@ProjectID and TaskID=@TaskID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPActivities GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPActivities entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPActivities> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPActivities> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPActivities entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPActivities entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPActivities entity)
		{
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "TaskID", DbType.Int32, entity.TaskID);
			this.DB.AddInParameter(command, "Status", DbType.String, entity.Status);
			this.DB.AddInParameter(command, "LastRunStartDT", DbType.DateTime, entity.LastRunStartDT);
			this.DB.AddInParameter(command, "LastRunEndDT", DbType.DateTime, entity.LastRunEndDT);
			this.DB.AddInParameter(command, "LastRunStatus", DbType.Boolean, entity.LastRunStatus);
			this.DB.AddInParameter(command, "LastRunResult", DbType.String, entity.LastRunResult);
			this.DB.AddInParameter(command, "LastSuccessDT", DbType.DateTime, entity.LastSuccessDT);
			this.DB.AddInParameter(command, "RunFailTimes", DbType.Int32, entity.RunFailTimes);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPActivities entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "TaskID", DbType.Int32, entity.TaskID);
		}
		private EntityBPActivities BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPActivities> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPActivities> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPActivities> lists = new List<EntityBPActivities>();
			while (idr.Read())
			{
				EntityBPActivities entity = new EntityBPActivities();
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

				index = idr.GetOrdinal("Status");
				if (!idr.IsDBNull(index))
				{
					entity.Status = idr.GetString(index);
				}

				index = idr.GetOrdinal("LastRunStartDT");
				if (!idr.IsDBNull(index))
				{
					entity.LastRunStartDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("LastRunEndDT");
				if (!idr.IsDBNull(index))
				{
					entity.LastRunEndDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("LastRunStatus");
				if (!idr.IsDBNull(index))
				{
					entity.LastRunStatus = idr.GetBoolean(index);
				}

				index = idr.GetOrdinal("LastRunResult");
				if (!idr.IsDBNull(index))
				{
					entity.LastRunResult = idr.GetString(index);
				}

				index = idr.GetOrdinal("LastSuccessDT");
				if (!idr.IsDBNull(index))
				{
					entity.LastSuccessDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("RunFailTimes");
				if (!idr.IsDBNull(index))
				{
					entity.RunFailTimes = idr.GetInt32(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
