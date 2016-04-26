using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPUserProjectRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPUserProjectRepository(){}
		public EntityBPUserProjectRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPUserProject entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Insert into BPUserProject([UserID],[ProjectID])values(@UserID,@ProjectID)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPUserProject entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Update BPUserProject Set [UserID]=@UserID,[ProjectID]=@ProjectID where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPUserProject entity = new EntityBPUserProject();
			string strSQL="Delete from BPUserProject";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPUserProject entity)
		{
			string strSQL="Delete from BPUserProject";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPUserProject GetEntityByID(int iHQID)
		{
			EntityBPUserProject entity = new EntityBPUserProject();
			string strSQL="Select * from BPUserProject";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPUserProject GetEntityByPK(EntityBPUserProject entity)
		{
			string strSQL="Select * from BPUserProject";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPUserProject GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPUserProject entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPUserProject> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPUserProject> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPUserProject entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPUserProject entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPUserProject entity)
		{
			this.DB.AddInParameter(command, "UserID", DbType.Int32, entity.UserID);
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPUserProject entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPUserProject BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPUserProject> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPUserProject> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPUserProject> lists = new List<EntityBPUserProject>();
			while (idr.Read())
			{
				EntityBPUserProject entity = new EntityBPUserProject();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("UserID");
				if (!idr.IsDBNull(index))
				{
					entity.UserID = idr.GetInt32(index);
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
