using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.API.BO.Entity
{
	public partial class EntityBPMailAttachRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPMailAttachRepository(){}
		public EntityBPMailAttachRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPMailAttach entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @FilePath=convert(nvarchar(500),@FilePath)";
			strSQL +=" Set @FileName=convert(nvarchar(100),@FileName)";
			strSQL +=" Set @FileContentType=convert(varchar(50),@FileContentType)";
			strSQL +=" Insert into BPMailAttach([MailGUID],[FilePath],[FileName],[FileContentType],[FileLength],[FileContent],[CreatedDate])values(@MailGUID,@FilePath,@FileName,@FileContentType,@FileLength,@FileContent,@CreatedDate)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPMailAttach entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @FilePath=convert(nvarchar(500),@FilePath)";
			strSQL +=" Set @FileName=convert(nvarchar(100),@FileName)";
			strSQL +=" Set @FileContentType=convert(varchar(50),@FileContentType)";
			strSQL +=" Update BPMailAttach Set [MailGUID]=@MailGUID,[FilePath]=@FilePath,[FileName]=@FileName,[FileContentType]=@FileContentType,[FileLength]=@FileLength,[FileContent]=@FileContent,[CreatedDate]=@CreatedDate where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPMailAttach entity = new EntityBPMailAttach();
			string strSQL="Delete from BPMailAttach";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPMailAttach entity)
		{
			string strSQL="Delete from BPMailAttach";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPMailAttach GetEntityByID(int iHQID)
		{
			EntityBPMailAttach entity = new EntityBPMailAttach();
			string strSQL="Select * from BPMailAttach";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPMailAttach GetEntityByPK(EntityBPMailAttach entity)
		{
			string strSQL="Select * from BPMailAttach";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPMailAttach GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPMailAttach entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPMailAttach> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPMailAttach> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPMailAttach entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPMailAttach entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPMailAttach entity)
		{
			this.DB.AddInParameter(command, "MailGUID", DbType.Guid, entity.MailGUID);
			this.DB.AddInParameter(command, "FilePath", DbType.String, entity.FilePath);
			this.DB.AddInParameter(command, "FileName", DbType.String, entity.FileName);
			this.DB.AddInParameter(command, "FileContentType", DbType.String, entity.FileContentType);
			this.DB.AddInParameter(command, "FileLength", DbType.Int32, entity.FileLength);
			this.DB.AddInParameter(command, "FileContent", DbType.Binary, entity.FileContent);
			this.DB.AddInParameter(command, "CreatedDate", DbType.DateTime, entity.CreatedDate);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPMailAttach entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPMailAttach BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPMailAttach> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPMailAttach> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPMailAttach> lists = new List<EntityBPMailAttach>();
			while (idr.Read())
			{
				EntityBPMailAttach entity = new EntityBPMailAttach();
				int index = 0;

				index = idr.GetOrdinal("ID");
				if (!idr.IsDBNull(index))
				{
					entity.ID = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("MailGUID");
				if (!idr.IsDBNull(index))
				{
					entity.MailGUID = idr.GetGuid(index);
				}

				index = idr.GetOrdinal("FilePath");
				if (!idr.IsDBNull(index))
				{
					entity.FilePath = idr.GetString(index);
				}

				index = idr.GetOrdinal("FileName");
				if (!idr.IsDBNull(index))
				{
					entity.FileName = idr.GetString(index);
				}

				index = idr.GetOrdinal("FileContentType");
				if (!idr.IsDBNull(index))
				{
					entity.FileContentType = idr.GetString(index);
				}

				index = idr.GetOrdinal("FileLength");
				if (!idr.IsDBNull(index))
				{
					entity.FileLength = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("FileContent");
				if (!idr.IsDBNull(index))
				{
					entity.FileContent = new Byte[(idr.GetBytes(index, 0, null, 0, int.MaxValue))];
					idr.GetBytes(index, 0, entity.FileContent, 0, entity.FileContent.Length);
				}

				index = idr.GetOrdinal("CreatedDate");
				if (!idr.IsDBNull(index))
				{
					entity.CreatedDate = idr.GetDateTime(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
