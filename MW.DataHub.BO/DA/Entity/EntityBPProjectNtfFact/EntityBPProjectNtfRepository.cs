using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPProjectNtfRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPProjectNtfRepository(){}
		public EntityBPProjectNtfRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPProjectNtf entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @Subject=convert(nvarchar(100),@Subject)";
			strSQL +=" Set @MailTo=convert(varchar(500),@MailTo)";
			strSQL +=" Set @MailCC=convert(varchar(500),@MailCC)";
			strSQL +=" Set @Attachments=convert(nvarchar(200),@Attachments)";
			strSQL +=" Set @SendStatus=convert(varchar(15),@SendStatus)";
			strSQL +=" Insert into BPProjectNtf([Subject],[MailTo],[MailCC],[MailBody],[Attachments],[SendStatus],[SendDT],[SendMsg],[ProjectID])values(@Subject,@MailTo,@MailCC,@MailBody,@Attachments,@SendStatus,@SendDT,@SendMsg,@ProjectID)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPProjectNtf entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @Subject=convert(nvarchar(100),@Subject)";
			strSQL +=" Set @MailTo=convert(varchar(500),@MailTo)";
			strSQL +=" Set @MailCC=convert(varchar(500),@MailCC)";
			strSQL +=" Set @Attachments=convert(nvarchar(200),@Attachments)";
			strSQL +=" Set @SendStatus=convert(varchar(15),@SendStatus)";
			strSQL +=" Update BPProjectNtf Set [Subject]=@Subject,[MailTo]=@MailTo,[MailCC]=@MailCC,[MailBody]=@MailBody,[Attachments]=@Attachments,[SendStatus]=@SendStatus,[SendDT]=@SendDT,[SendMsg]=@SendMsg,[ProjectID]=@ProjectID where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPProjectNtf entity = new EntityBPProjectNtf();
			string strSQL="Delete from BPProjectNtf";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPProjectNtf entity)
		{
			string strSQL="Delete from BPProjectNtf";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPProjectNtf GetEntityByID(int iHQID)
		{
			EntityBPProjectNtf entity = new EntityBPProjectNtf();
			string strSQL="Select * from BPProjectNtf";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPProjectNtf GetEntityByPK(EntityBPProjectNtf entity)
		{
			string strSQL="Select * from BPProjectNtf";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPProjectNtf GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPProjectNtf entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPProjectNtf> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPProjectNtf> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPProjectNtf entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPProjectNtf entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPProjectNtf entity)
		{
			this.DB.AddInParameter(command, "Subject", DbType.String, entity.Subject);
			this.DB.AddInParameter(command, "MailTo", DbType.String, entity.MailTo);
			this.DB.AddInParameter(command, "MailCC", DbType.String, entity.MailCC);
			this.DB.AddInParameter(command, "MailBody", DbType.String, entity.MailBody);
			this.DB.AddInParameter(command, "Attachments", DbType.String, entity.Attachments);
			this.DB.AddInParameter(command, "SendStatus", DbType.String, entity.SendStatus);
			this.DB.AddInParameter(command, "SendDT", DbType.DateTime, entity.SendDT);
			this.DB.AddInParameter(command, "SendMsg", DbType.String, entity.SendMsg);
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPProjectNtf entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPProjectNtf BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPProjectNtf> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPProjectNtf> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPProjectNtf> lists = new List<EntityBPProjectNtf>();
			while (idr.Read())
			{
				EntityBPProjectNtf entity = new EntityBPProjectNtf();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("Subject");
				if (!idr.IsDBNull(index))
				{
					entity.Subject = idr.GetString(index);
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

				index = idr.GetOrdinal("MailBody");
				if (!idr.IsDBNull(index))
				{
					entity.MailBody = idr.GetString(index);
				}

				index = idr.GetOrdinal("Attachments");
				if (!idr.IsDBNull(index))
				{
					entity.Attachments = idr.GetString(index);
				}

				index = idr.GetOrdinal("SendStatus");
				if (!idr.IsDBNull(index))
				{
					entity.SendStatus = idr.GetString(index);
				}

				index = idr.GetOrdinal("SendDT");
				if (!idr.IsDBNull(index))
				{
					entity.SendDT = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("SendMsg");
				if (!idr.IsDBNull(index))
				{
					entity.SendMsg = idr.GetString(index);
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
