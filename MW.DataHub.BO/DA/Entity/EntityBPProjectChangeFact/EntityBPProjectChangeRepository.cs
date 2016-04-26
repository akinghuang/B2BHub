using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPProjectChangeRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPProjectChangeRepository(){}
		public EntityBPProjectChangeRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPProjectChange entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @ChangeTitle=convert(nvarchar(100),@ChangeTitle)";
			strSQL +=" Set @ChangeSuorce=convert(nvarchar(100),@ChangeSuorce)";
			strSQL +=" Set @Owner=convert(nvarchar(50),@Owner)";
			strSQL +=" Set @SourceCode=convert(nvarchar(200),@SourceCode)";
			strSQL +=" Insert into BPProjectChange([ProjectID],[ChangeTitle],[ChangeContent],[ChangeSuorce],[Owner],[StartDT],[TargetDT],[CompleteDT],[OnlineDT],[SourceCode],[CreatedDT],[UpdatedDT],[CreatedBy],[UpdatedBy])values(@ProjectID,@ChangeTitle,@ChangeContent,@ChangeSuorce,@Owner,@StartDT,@TargetDT,@CompleteDT,@OnlineDT,@SourceCode,@CreatedDT,@UpdatedDT,@CreatedBy,@UpdatedBy)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPProjectChange entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @ChangeTitle=convert(nvarchar(100),@ChangeTitle)";
			strSQL +=" Set @ChangeSuorce=convert(nvarchar(100),@ChangeSuorce)";
			strSQL +=" Set @Owner=convert(nvarchar(50),@Owner)";
			strSQL +=" Set @SourceCode=convert(nvarchar(200),@SourceCode)";
			strSQL +=" Update BPProjectChange Set [ProjectID]=@ProjectID,[ChangeTitle]=@ChangeTitle,[ChangeContent]=@ChangeContent,[ChangeSuorce]=@ChangeSuorce,[Owner]=@Owner,[StartDT]=@StartDT,[TargetDT]=@TargetDT,[CompleteDT]=@CompleteDT,[OnlineDT]=@OnlineDT,[SourceCode]=@SourceCode,[UpdatedDT]=@UpdatedDT,[UpdatedBy]=@UpdatedBy where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPProjectChange entity = new EntityBPProjectChange();
			string strSQL="Delete from BPProjectChange";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPProjectChange entity)
		{
			string strSQL="Delete from BPProjectChange";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPProjectChange GetEntityByID(int iHQID)
		{
			EntityBPProjectChange entity = new EntityBPProjectChange();
			string strSQL="Select * from BPProjectChange";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPProjectChange GetEntityByPK(EntityBPProjectChange entity)
		{
			string strSQL="Select * from BPProjectChange";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPProjectChange GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPProjectChange entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPProjectChange> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPProjectChange> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPProjectChange entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPProjectChange entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPProjectChange entity)
		{
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "ChangeTitle", DbType.String, entity.ChangeTitle);
			this.DB.AddInParameter(command, "ChangeContent", DbType.String, entity.ChangeContent);
			this.DB.AddInParameter(command, "ChangeSuorce", DbType.String, entity.ChangeSuorce);
			this.DB.AddInParameter(command, "Owner", DbType.String, entity.Owner);
			this.DB.AddInParameter(command, "StartDT", DbType.DateTime, entity.StartDT);
			this.DB.AddInParameter(command, "TargetDT", DbType.DateTime, entity.TargetDT);
			this.DB.AddInParameter(command, "CompleteDT", DbType.DateTime, entity.CompleteDT);
			this.DB.AddInParameter(command, "OnlineDT", DbType.DateTime, entity.OnlineDT);
			this.DB.AddInParameter(command, "SourceCode", DbType.String, entity.SourceCode);
			this.DB.AddInParameter(command, "CreatedDT", DbType.DateTime, entity.CreatedDT);
			this.DB.AddInParameter(command, "UpdatedDT", DbType.DateTime, entity.UpdatedDT);
			this.DB.AddInParameter(command, "CreatedBy", DbType.Int32, entity.CreatedBy);
			this.DB.AddInParameter(command, "UpdatedBy", DbType.Int32, entity.UpdatedBy);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPProjectChange entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPProjectChange BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPProjectChange> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPProjectChange> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPProjectChange> lists = new List<EntityBPProjectChange>();
			while (idr.Read())
			{
				EntityBPProjectChange entity = new EntityBPProjectChange();
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

				index = idr.GetOrdinal("ChangeTitle");
				if (!idr.IsDBNull(index))
				{
					entity.ChangeTitle = idr.GetString(index);
				}

				index = idr.GetOrdinal("ChangeContent");
				if (!idr.IsDBNull(index))
				{
					entity.ChangeContent = idr.GetString(index);
				}

				index = idr.GetOrdinal("ChangeSuorce");
				if (!idr.IsDBNull(index))
				{
					entity.ChangeSuorce = idr.GetString(index);
				}

				index = idr.GetOrdinal("Owner");
				if (!idr.IsDBNull(index))
				{
					entity.Owner = idr.GetString(index);
				}

				index = idr.GetOrdinal("StartDT");
				if (!idr.IsDBNull(index))
				{
					entity.StartDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("TargetDT");
				if (!idr.IsDBNull(index))
				{
					entity.TargetDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("CompleteDT");
				if (!idr.IsDBNull(index))
				{
					entity.CompleteDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("OnlineDT");
				if (!idr.IsDBNull(index))
				{
					entity.OnlineDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("SourceCode");
				if (!idr.IsDBNull(index))
				{
					entity.SourceCode = idr.GetString(index);
				}

				index = idr.GetOrdinal("CreatedDT");
				if (!idr.IsDBNull(index))
				{
					entity.CreatedDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("UpdatedDT");
				if (!idr.IsDBNull(index))
				{
					entity.UpdatedDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("CreatedBy");
				if (!idr.IsDBNull(index))
				{
					entity.CreatedBy = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("UpdatedBy");
				if (!idr.IsDBNull(index))
				{
					entity.UpdatedBy = idr.GetInt32(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
