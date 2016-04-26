using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.API.BO.Entity
{
	public partial class EntityBPSettingRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPSettingRepository(){}
		public EntityBPSettingRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPSetting entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @SettingName=convert(varchar(50),@SettingName)";
			strSQL +=" Set @SMTPServer=convert(varchar(50),@SMTPServer)";
			strSQL +=" Set @Sender=convert(nvarchar(50),@Sender)";
			strSQL +=" Set @SenderName=convert(nvarchar(50),@SenderName)";
			strSQL +=" Set @Encoding=convert(varchar(50),@Encoding)";
			strSQL +=" Set @UserName=convert(nvarchar(100),@UserName)";
			strSQL +=" Set @Password=convert(nvarchar(100),@Password)";
			strSQL +=" Set @BodyFormat=convert(varchar(50),@BodyFormat)";
			strSQL +=" Set @BCC=convert(varchar(50),@BCC)";
			strSQL +=" Insert into BPSetting([ProjectID],[SettingName],[SMTPServer],[Sender],[SenderName],[Encoding],[UserName],[Password],[BodyFormat],[BCC])values(@ProjectID,@SettingName,@SMTPServer,@Sender,@SenderName,@Encoding,@UserName,@Password,@BodyFormat,@BCC)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPSetting entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @SettingName=convert(varchar(50),@SettingName)";
			strSQL +=" Set @SMTPServer=convert(varchar(50),@SMTPServer)";
			strSQL +=" Set @Sender=convert(nvarchar(50),@Sender)";
			strSQL +=" Set @SenderName=convert(nvarchar(50),@SenderName)";
			strSQL +=" Set @Encoding=convert(varchar(50),@Encoding)";
			strSQL +=" Set @UserName=convert(nvarchar(100),@UserName)";
			strSQL +=" Set @Password=convert(nvarchar(100),@Password)";
			strSQL +=" Set @BodyFormat=convert(varchar(50),@BodyFormat)";
			strSQL +=" Set @BCC=convert(varchar(50),@BCC)";
			strSQL +=" Update BPSetting Set [ProjectID]=@ProjectID,[SettingName]=@SettingName,[SMTPServer]=@SMTPServer,[Sender]=@Sender,[SenderName]=@SenderName,[Encoding]=@Encoding,[UserName]=@UserName,[Password]=@Password,[BodyFormat]=@BodyFormat,[BCC]=@BCC where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPSetting entity = new EntityBPSetting();
			string strSQL="Delete from BPSetting";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPSetting entity)
		{
			string strSQL="Delete from BPSetting";
			strSQL +=" where ProjectID=@ProjectID and SettingName=@SettingName";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPSetting GetEntityByID(int iHQID)
		{
			EntityBPSetting entity = new EntityBPSetting();
			string strSQL="Select * from BPSetting";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPSetting GetEntityByPK(EntityBPSetting entity)
		{
			string strSQL="Select * from BPSetting";
			strSQL +=" where ProjectID=@ProjectID and SettingName=@SettingName";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPSetting GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPSetting entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPSetting> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPSetting> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPSetting entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPSetting entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPSetting entity)
		{
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "SettingName", DbType.String, entity.SettingName);
			this.DB.AddInParameter(command, "SMTPServer", DbType.String, entity.SMTPServer);
			this.DB.AddInParameter(command, "Sender", DbType.String, entity.Sender);
			this.DB.AddInParameter(command, "SenderName", DbType.String, entity.SenderName);
			this.DB.AddInParameter(command, "Encoding", DbType.String, entity.Encoding);
			this.DB.AddInParameter(command, "UserName", DbType.String, entity.UserName);
			this.DB.AddInParameter(command, "Password", DbType.String, entity.Password);
			this.DB.AddInParameter(command, "BodyFormat", DbType.String, entity.BodyFormat);
			this.DB.AddInParameter(command, "BCC", DbType.String, entity.BCC);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPSetting entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "SettingName", DbType.String, entity.SettingName);
		}
		private EntityBPSetting BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPSetting> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPSetting> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPSetting> lists = new List<EntityBPSetting>();
			while (idr.Read())
			{
				EntityBPSetting entity = new EntityBPSetting();
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

				index = idr.GetOrdinal("SettingName");
				if (!idr.IsDBNull(index))
				{
					entity.SettingName = idr.GetString(index);
				}

				index = idr.GetOrdinal("SMTPServer");
				if (!idr.IsDBNull(index))
				{
					entity.SMTPServer = idr.GetString(index);
				}

				index = idr.GetOrdinal("Sender");
				if (!idr.IsDBNull(index))
				{
					entity.Sender = idr.GetString(index);
				}

				index = idr.GetOrdinal("SenderName");
				if (!idr.IsDBNull(index))
				{
					entity.SenderName = idr.GetString(index);
				}

				index = idr.GetOrdinal("Encoding");
				if (!idr.IsDBNull(index))
				{
					entity.Encoding = idr.GetString(index);
				}

				index = idr.GetOrdinal("UserName");
				if (!idr.IsDBNull(index))
				{
					entity.UserName = idr.GetString(index);
				}

				index = idr.GetOrdinal("Password");
				if (!idr.IsDBNull(index))
				{
					entity.Password = idr.GetString(index);
				}

				index = idr.GetOrdinal("BodyFormat");
				if (!idr.IsDBNull(index))
				{
					entity.BodyFormat = idr.GetString(index);
				}

				index = idr.GetOrdinal("BCC");
				if (!idr.IsDBNull(index))
				{
					entity.BCC = idr.GetString(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
