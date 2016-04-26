using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPProjectRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPProjectRepository(){}
		public EntityBPProjectRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPProject entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @ProjectName=convert(nvarchar(40),@ProjectName)";
			strSQL +=" Set @Owner=convert(nvarchar(100),@Owner)";
			strSQL +=" Set @Status=convert(nvarchar(10),@Status)";
			strSQL +=" Set @HostMachineID=convert(nvarchar(20),@HostMachineID)";
			strSQL +=" Set @ProcessID=convert(nvarchar(50),@ProcessID)";
			strSQL +=" Insert into BPProject([Sequence],[ProjectName],[ProjectDesc],[Owner],[AdminMail],[UserMail],[RuntimeParas],[Status],[HostMachineID],[ProcessID],[CreatedDT],[UpdatedDT],[CreatedBy],[UpdateBy])values(@Sequence,@ProjectName,@ProjectDesc,@Owner,@AdminMail,@UserMail,@RuntimeParas,@Status,@HostMachineID,@ProcessID,@CreatedDT,@UpdatedDT,@CreatedBy,@UpdateBy)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPProject entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @ProjectName=convert(nvarchar(40),@ProjectName)";
			strSQL +=" Set @Owner=convert(nvarchar(100),@Owner)";
			strSQL +=" Set @Status=convert(nvarchar(10),@Status)";
			strSQL +=" Set @HostMachineID=convert(nvarchar(20),@HostMachineID)";
			strSQL +=" Set @ProcessID=convert(nvarchar(50),@ProcessID)";
			strSQL +=" Update BPProject Set [Sequence]=@Sequence,[ProjectName]=@ProjectName,[ProjectDesc]=@ProjectDesc,[Owner]=@Owner,[AdminMail]=@AdminMail,[UserMail]=@UserMail,[RuntimeParas]=@RuntimeParas,[Status]=@Status,[HostMachineID]=@HostMachineID,[ProcessID]=@ProcessID,[CreatedDT]=@CreatedDT,[UpdatedDT]=@UpdatedDT,[CreatedBy]=@CreatedBy,[UpdateBy]=@UpdateBy where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPProject entity = new EntityBPProject();
			string strSQL="Delete from BPProject";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPProject entity)
		{
			string strSQL="Delete from BPProject";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPProject GetEntityByID(int iHQID)
		{
			EntityBPProject entity = new EntityBPProject();
			string strSQL="Select * from BPProject";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPProject GetEntityByPK(EntityBPProject entity)
		{
			string strSQL="Select * from BPProject";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPProject GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPProject entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPProject> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPProject> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPProject entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPProject entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPProject entity)
		{
			this.DB.AddInParameter(command, "Sequence", DbType.Int32, entity.Sequence);
			this.DB.AddInParameter(command, "ProjectName", DbType.String, entity.ProjectName);
			this.DB.AddInParameter(command, "ProjectDesc", DbType.String, entity.ProjectDesc);
			this.DB.AddInParameter(command, "Owner", DbType.String, entity.Owner);
			this.DB.AddInParameter(command, "AdminMail", DbType.String, entity.AdminMail);
			this.DB.AddInParameter(command, "UserMail", DbType.String, entity.UserMail);
			this.DB.AddInParameter(command, "RuntimeParas", DbType.String, entity.RuntimeParas);
			this.DB.AddInParameter(command, "Status", DbType.String, entity.Status);
			this.DB.AddInParameter(command, "HostMachineID", DbType.String, entity.HostMachineID);
			this.DB.AddInParameter(command, "ProcessID", DbType.String, entity.ProcessID);
			this.DB.AddInParameter(command, "CreatedDT", DbType.DateTime, entity.CreatedDT);
			this.DB.AddInParameter(command, "UpdatedDT", DbType.DateTime, entity.UpdatedDT);
			this.DB.AddInParameter(command, "CreatedBy", DbType.Int32, entity.CreatedBy);
			this.DB.AddInParameter(command, "UpdateBy", DbType.Int32, entity.UpdateBy);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPProject entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPProject BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPProject> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPProject> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPProject> lists = new List<EntityBPProject>();
			while (idr.Read())
			{
				EntityBPProject entity = new EntityBPProject();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("Sequence");
				if (!idr.IsDBNull(index))
				{
					entity.Sequence = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("ProjectName");
				if (!idr.IsDBNull(index))
				{
					entity.ProjectName = idr.GetString(index);
				}

				index = idr.GetOrdinal("ProjectDesc");
				if (!idr.IsDBNull(index))
				{
					entity.ProjectDesc = idr.GetString(index);
				}

				index = idr.GetOrdinal("Owner");
				if (!idr.IsDBNull(index))
				{
					entity.Owner = idr.GetString(index);
				}

				index = idr.GetOrdinal("AdminMail");
				if (!idr.IsDBNull(index))
				{
					entity.AdminMail = idr.GetString(index);
				}

				index = idr.GetOrdinal("UserMail");
				if (!idr.IsDBNull(index))
				{
					entity.UserMail = idr.GetString(index);
				}

				index = idr.GetOrdinal("RuntimeParas");
				if (!idr.IsDBNull(index))
				{
					entity.RuntimeParas = idr.GetString(index);
				}

				index = idr.GetOrdinal("Status");
				if (!idr.IsDBNull(index))
				{
					entity.Status = idr.GetString(index);
				}

				index = idr.GetOrdinal("HostMachineID");
				if (!idr.IsDBNull(index))
				{
					entity.HostMachineID = idr.GetString(index);
				}

				index = idr.GetOrdinal("ProcessID");
				if (!idr.IsDBNull(index))
				{
					entity.ProcessID = idr.GetString(index);
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

				index = idr.GetOrdinal("UpdateBy");
				if (!idr.IsDBNull(index))
				{
					entity.UpdateBy = idr.GetInt32(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
