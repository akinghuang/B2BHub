using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPMailLogRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPMailLogRepository(){}
		public EntityBPMailLogRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPMailLog entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @MailStatus=convert(varchar(50),@MailStatus)";
			strSQL +=" Set @MailSender=convert(nvarchar(50),@MailSender)";
			strSQL +=" Set @MailTo=convert(nvarchar(500),@MailTo)";
			strSQL +=" Set @MailCC=convert(nvarchar(500),@MailCC)";
			strSQL +=" Set @MailBCC=convert(nvarchar(500),@MailBCC)";
			strSQL +=" Set @Subject=convert(nvarchar(500),@Subject)";
			strSQL +=" Set @MailBodyFormat=convert(varchar(50),@MailBodyFormat)";
			strSQL +=" Set @CreatedBy=convert(varchar(50),@CreatedBy)";
			strSQL +=" Set @SendBy=convert(varchar(50),@SendBy)";
			strSQL +=" Set @MailResult=convert(nvarchar(500),@MailResult)";
			strSQL +=" Insert into BPMailLog([ProjectID],[MailGUID],[MailStatus],[MailSender],[MailTo],[MailCC],[MailBCC],[Subject],[MailBody],[MailBodyFormat],[CreatedDate],[CreatedBy],[SendDate],[SendBy],[MailResult])values(@ProjectID,@MailGUID,@MailStatus,@MailSender,@MailTo,@MailCC,@MailBCC,@Subject,@MailBody,@MailBodyFormat,@CreatedDate,@CreatedBy,@SendDate,@SendBy,@MailResult)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPMailLog entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @MailStatus=convert(varchar(50),@MailStatus)";
			strSQL +=" Set @MailSender=convert(nvarchar(50),@MailSender)";
			strSQL +=" Set @MailTo=convert(nvarchar(500),@MailTo)";
			strSQL +=" Set @MailCC=convert(nvarchar(500),@MailCC)";
			strSQL +=" Set @MailBCC=convert(nvarchar(500),@MailBCC)";
			strSQL +=" Set @Subject=convert(nvarchar(500),@Subject)";
			strSQL +=" Set @MailBodyFormat=convert(varchar(50),@MailBodyFormat)";
			strSQL +=" Set @CreatedBy=convert(varchar(50),@CreatedBy)";
			strSQL +=" Set @SendBy=convert(varchar(50),@SendBy)";
			strSQL +=" Set @MailResult=convert(nvarchar(500),@MailResult)";
			strSQL +=" Update BPMailLog Set [ProjectID]=@ProjectID,[MailGUID]=@MailGUID,[MailStatus]=@MailStatus,[MailSender]=@MailSender,[MailTo]=@MailTo,[MailCC]=@MailCC,[MailBCC]=@MailBCC,[Subject]=@Subject,[MailBody]=@MailBody,[MailBodyFormat]=@MailBodyFormat,[CreatedDate]=@CreatedDate,[CreatedBy]=@CreatedBy,[SendDate]=@SendDate,[SendBy]=@SendBy,[MailResult]=@MailResult where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPMailLog entity = new EntityBPMailLog();
			string strSQL="Delete from BPMailLog";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPMailLog entity)
		{
			string strSQL="Delete from BPMailLog";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPMailLog GetEntityByID(int iHQID)
		{
			EntityBPMailLog entity = new EntityBPMailLog();
			string strSQL="Select * from BPMailLog";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPMailLog GetEntityByPK(EntityBPMailLog entity)
		{
			string strSQL="Select * from BPMailLog";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPMailLog GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPMailLog entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPMailLog> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPMailLog> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPMailLog entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPMailLog entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPMailLog entity)
		{
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "MailGUID", DbType.Guid, entity.MailGUID);
			this.DB.AddInParameter(command, "MailStatus", DbType.String, entity.MailStatus);
			this.DB.AddInParameter(command, "MailSender", DbType.String, entity.MailSender);
			this.DB.AddInParameter(command, "MailTo", DbType.String, entity.MailTo);
			this.DB.AddInParameter(command, "MailCC", DbType.String, entity.MailCC);
			this.DB.AddInParameter(command, "MailBCC", DbType.String, entity.MailBCC);
			this.DB.AddInParameter(command, "Subject", DbType.String, entity.Subject);
			this.DB.AddInParameter(command, "MailBody", DbType.String, entity.MailBody);
			this.DB.AddInParameter(command, "MailBodyFormat", DbType.String, entity.MailBodyFormat);
			this.DB.AddInParameter(command, "CreatedDate", DbType.DateTime, entity.CreatedDate);
			this.DB.AddInParameter(command, "CreatedBy", DbType.String, entity.CreatedBy);
			this.DB.AddInParameter(command, "SendDate", DbType.DateTime, entity.SendDate);
			this.DB.AddInParameter(command, "SendBy", DbType.String, entity.SendBy);
			this.DB.AddInParameter(command, "MailResult", DbType.String, entity.MailResult);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPMailLog entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPMailLog BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPMailLog> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPMailLog> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPMailLog> lists = new List<EntityBPMailLog>();
			while (idr.Read())
			{
				EntityBPMailLog entity = new EntityBPMailLog();
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

				index = idr.GetOrdinal("MailGUID");
				if (!idr.IsDBNull(index))
				{
					entity.MailGUID = idr.GetGuid(index);
				}

				index = idr.GetOrdinal("MailStatus");
				if (!idr.IsDBNull(index))
				{
					entity.MailStatus = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailSender");
				if (!idr.IsDBNull(index))
				{
					entity.MailSender = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailTo");
				if (!idr.IsDBNull(index))
				{
					entity.MailTo = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailCC");
				if (!idr.IsDBNull(index))
				{
					entity.MailCC = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailBCC");
				if (!idr.IsDBNull(index))
				{
					entity.MailBCC = idr.GetString(index);
				}

				index = idr.GetOrdinal("Subject");
				if (!idr.IsDBNull(index))
				{
					entity.Subject = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailBody");
				if (!idr.IsDBNull(index))
				{
					entity.MailBody = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailBodyFormat");
				if (!idr.IsDBNull(index))
				{
					entity.MailBodyFormat = idr.GetString(index);
				}

				index = idr.GetOrdinal("CreatedDate");
				if (!idr.IsDBNull(index))
				{
					entity.CreatedDate = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("CreatedBy");
				if (!idr.IsDBNull(index))
				{
					entity.CreatedBy = idr.GetString(index);
				}

				index = idr.GetOrdinal("SendDate");
				if (!idr.IsDBNull(index))
				{
					entity.SendDate = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("SendBy");
				if (!idr.IsDBNull(index))
				{
					entity.SendBy = idr.GetString(index);
				}

				index = idr.GetOrdinal("MailResult");
				if (!idr.IsDBNull(index))
				{
					entity.MailResult = idr.GetString(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
