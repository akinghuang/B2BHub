using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPUserRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPUserRepository(){}
		public EntityBPUserRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPUser entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @UserID=convert(varchar(20),@UserID)";
			strSQL +=" Set @FullName=convert(nvarchar(50),@FullName)";
			strSQL +=" Set @Password=convert(nvarchar(50),@Password)";
			strSQL +=" Set @Status=convert(varchar(50),@Status)";
			strSQL +=" Set @LastLoginIP=convert(varchar(50),@LastLoginIP)";
			strSQL +=" Insert into BPUser([UserID],[FullName],[Password],[Comments],[LastLoginDT],[TotalLoginCount],[Status],[LastLoginIP])values(@UserID,@FullName,@Password,@Comments,@LastLoginDT,@TotalLoginCount,@Status,@LastLoginIP)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPUser entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @UserID=convert(varchar(20),@UserID)";
			strSQL +=" Set @FullName=convert(nvarchar(50),@FullName)";
			strSQL +=" Set @Password=convert(nvarchar(50),@Password)";
			strSQL +=" Set @Status=convert(varchar(50),@Status)";
			strSQL +=" Set @LastLoginIP=convert(varchar(50),@LastLoginIP)";
			strSQL +=" Update BPUser Set [UserID]=@UserID,[FullName]=@FullName,[Password]=@Password,[Comments]=@Comments,[LastLoginDT]=@LastLoginDT,[TotalLoginCount]=@TotalLoginCount,[Status]=@Status,[LastLoginIP]=@LastLoginIP where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPUser entity = new EntityBPUser();
			string strSQL="Delete from BPUser";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPUser entity)
		{
			string strSQL="Delete from BPUser";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPUser GetEntityByID(int iHQID)
		{
			EntityBPUser entity = new EntityBPUser();
			string strSQL="Select * from BPUser";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPUser GetEntityByPK(EntityBPUser entity)
		{
			string strSQL="Select * from BPUser";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPUser GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPUser entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPUser> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPUser> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPUser entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPUser entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPUser entity)
		{
			this.DB.AddInParameter(command, "UserID", DbType.String, entity.UserID);
			this.DB.AddInParameter(command, "FullName", DbType.String, entity.FullName);
			this.DB.AddInParameter(command, "Password", DbType.String, entity.Password);
			this.DB.AddInParameter(command, "Comments", DbType.String, entity.Comments);
			this.DB.AddInParameter(command, "LastLoginDT", DbType.DateTime, entity.LastLoginDT);
			this.DB.AddInParameter(command, "TotalLoginCount", DbType.Int32, entity.TotalLoginCount);
			this.DB.AddInParameter(command, "Status", DbType.String, entity.Status);
			this.DB.AddInParameter(command, "LastLoginIP", DbType.String, entity.LastLoginIP);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPUser entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPUser BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPUser> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPUser> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPUser> lists = new List<EntityBPUser>();
			while (idr.Read())
			{
				EntityBPUser entity = new EntityBPUser();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("UserID");
				if (!idr.IsDBNull(index))
				{
					entity.UserID = idr.GetString(index);
				}

				index = idr.GetOrdinal("FullName");
				if (!idr.IsDBNull(index))
				{
					entity.FullName = idr.GetString(index);
				}

				index = idr.GetOrdinal("Password");
				if (!idr.IsDBNull(index))
				{
					entity.Password = idr.GetString(index);
				}

				index = idr.GetOrdinal("Comments");
				if (!idr.IsDBNull(index))
				{
					entity.Comments = idr.GetString(index);
				}

				index = idr.GetOrdinal("LastLoginDT");
				if (!idr.IsDBNull(index))
				{
					entity.LastLoginDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("TotalLoginCount");
				if (!idr.IsDBNull(index))
				{
					entity.TotalLoginCount = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("Status");
				if (!idr.IsDBNull(index))
				{
					entity.Status = idr.GetString(index);
				}

				index = idr.GetOrdinal("LastLoginIP");
				if (!idr.IsDBNull(index))
				{
					entity.LastLoginIP = idr.GetString(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
