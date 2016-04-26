using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Text;
using System.Collections.Generic;

namespace MW.DataHub.BO.Entity
{
	public partial class EntityBPProjectTaskRepository
	{
		RepositoryBase _db = new RepositoryBase();
		public RepositoryBase DB
		{
		    get { return _db; }
		    set { _db = value; }
		}
		public EntityBPProjectTaskRepository(){}
		public EntityBPProjectTaskRepository(string datbaseName){DB.DatabaseName = datbaseName;}

		public void Add(EntityBPProjectTask entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @TaskName=convert(nvarchar(100),@TaskName)";
			strSQL +=" Set @Protocol=convert(varchar(100),@Protocol)";
			strSQL +=" Set @IO=convert(varchar(100),@IO)";
			strSQL +=" Set @MsgHandler=convert(nvarchar(200),@MsgHandler)";
			strSQL +=" Set @BizHandler=convert(nvarchar(200),@BizHandler)";
			strSQL +=" Set @RServer=convert(varchar(100),@RServer)";
			strSQL +=" Set @RFolder=convert(varchar(100),@RFolder)";
			strSQL +=" Set @RUID=convert(nvarchar(40),@RUID)";
			strSQL +=" Set @RPWD=convert(nvarchar(40),@RPWD)";
			strSQL +=" Set @RCert=convert(varchar(100),@RCert)";
			strSQL +=" Set @FileExt=convert(nvarchar(50),@FileExt)";
			strSQL +=" Set @LFolder=convert(nvarchar(100),@LFolder)";
			strSQL +=" Set @ScheduleType=convert(nvarchar(50),@ScheduleType)";
			strSQL +=" Set @ScheduleTime=convert(nvarchar(15),@ScheduleTime)";
			strSQL +=" Set @Status=convert(varchar(50),@Status)";
			strSQL +=" Set @ScheduleWeekDay=convert(nvarchar(15),@ScheduleWeekDay)";
			strSQL +=" Insert into BPProjectTask([ProjectID],[Running],[TaskName],[TaskDesc],[Protocol],[IO],[Sequence],[MsgHandler],[BizHandler],[RuntimeParas],[RServer],[RFolder],[RPort],[RUID],[RPWD],[RCert],[FileExt],[LFolder],[ScheduleType],[ScheduleMonth],[ScheduleTime],[LastRunTime],[Status],[CreatedDT],[UpdatedDT],[CreatedBy],[UpdatedBy],[ScheduleWeekDay],[ScheduleInterval],[NextRunTime])values(@ProjectID,@Running,@TaskName,@TaskDesc,@Protocol,@IO,@Sequence,@MsgHandler,@BizHandler,@RuntimeParas,@RServer,@RFolder,@RPort,@RUID,@RPWD,@RCert,@FileExt,@LFolder,@ScheduleType,@ScheduleMonth,@ScheduleTime,@LastRunTime,@Status,@CreatedDT,@UpdatedDT,@CreatedBy,@UpdatedBy,@ScheduleWeekDay,@ScheduleInterval,@NextRunTime)";
			strSQL +=" Set @ID=SCOPE_IDENTITY()";
			strSQL += " END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildAddMap(command, entity);
			this.DB.ExecuteNonQuery(command);
			Int32 hqid = (System.Int32)this.DB.GetParameterValue(command, "ID");
			entity.ID = hqid;
		}

		public void Save(EntityBPProjectTask entity)
		{
			string strSQL="SET NOCOUNT ON BEGIN TRY";
			strSQL +=" Set @TaskName=convert(nvarchar(100),@TaskName)";
			strSQL +=" Set @Protocol=convert(varchar(100),@Protocol)";
			strSQL +=" Set @IO=convert(varchar(100),@IO)";
			strSQL +=" Set @MsgHandler=convert(nvarchar(200),@MsgHandler)";
			strSQL +=" Set @BizHandler=convert(nvarchar(200),@BizHandler)";
			strSQL +=" Set @RServer=convert(varchar(100),@RServer)";
			strSQL +=" Set @RFolder=convert(varchar(100),@RFolder)";
			strSQL +=" Set @RUID=convert(nvarchar(40),@RUID)";
			strSQL +=" Set @RPWD=convert(nvarchar(40),@RPWD)";
			strSQL +=" Set @RCert=convert(varchar(100),@RCert)";
			strSQL +=" Set @FileExt=convert(nvarchar(50),@FileExt)";
			strSQL +=" Set @LFolder=convert(nvarchar(100),@LFolder)";
			strSQL +=" Set @ScheduleType=convert(nvarchar(50),@ScheduleType)";
			strSQL +=" Set @ScheduleTime=convert(nvarchar(15),@ScheduleTime)";
			strSQL +=" Set @Status=convert(varchar(50),@Status)";
			strSQL +=" Set @ScheduleWeekDay=convert(nvarchar(15),@ScheduleWeekDay)";
			strSQL +=" Update BPProjectTask Set [ProjectID]=@ProjectID,[Running]=@Running,[TaskName]=@TaskName,[TaskDesc]=@TaskDesc,[Protocol]=@Protocol,[IO]=@IO,[Sequence]=@Sequence,[MsgHandler]=@MsgHandler,[BizHandler]=@BizHandler,[RuntimeParas]=@RuntimeParas,[RServer]=@RServer,[RFolder]=@RFolder,[RPort]=@RPort,[RUID]=@RUID,[RPWD]=@RPWD,[RCert]=@RCert,[FileExt]=@FileExt,[LFolder]=@LFolder,[ScheduleType]=@ScheduleType,[ScheduleMonth]=@ScheduleMonth,[ScheduleTime]=@ScheduleTime,[LastRunTime]=@LastRunTime,[Status]=@Status,[CreatedDT]=@CreatedDT,[UpdatedDT]=@UpdatedDT,[CreatedBy]=@CreatedBy,[UpdatedBy]=@UpdatedBy,[ScheduleWeekDay]=@ScheduleWeekDay,[ScheduleInterval]=@ScheduleInterval,[NextRunTime]=@NextRunTime where ID=@ID";
			strSQL += " IF @@ROWCOUNT = 0 	BEGIN RAISERROR('Concurrent update error. Updated aborted.', 16, 2)	END   END TRY";
			strSQL += " BEGIN CATCH EXEC RethrowError END CATCH SET NOCOUNT OFF";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildUpdateMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public void Remove(int iHQID)
		{
			EntityBPProjectTask entity = new EntityBPProjectTask();
			string strSQL="Delete from BPProjectTask";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}
		public void Remove(EntityBPProjectTask entity)
		{
			string strSQL="Delete from BPProjectTask";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			this.DB.ExecuteNonQuery(command);
		}

		public EntityBPProjectTask GetEntityByID(int iHQID)
		{
			EntityBPProjectTask entity = new EntityBPProjectTask();
			string strSQL="Select * from BPProjectTask";
			strSQL += " where ID=@ID";
			entity.ID=iHQID;

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}

		public EntityBPProjectTask GetEntityByPK(EntityBPProjectTask entity)
		{
			string strSQL="Select * from BPProjectTask";
			strSQL +=" where ID=@ID";

			DbCommand command = this.DB.GetSqlStringCommand(strSQL);
			this.BuildDeleteMap(command, entity);
			IDataReader idr = this.DB.ExecuteReader(command);
			entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public Entity.EntityBPProjectTask GetEntityByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			Entity.EntityBPProjectTask entity = this.BuildSelectConstruct(idr);
			idr.Close();
			return entity;
		}
		public List<Entity.EntityBPProjectTask> GetEntityListByDataTable(DataTable ldt)
		{
			IDataReader idr = (IDataReader)ldt.CreateDataReader();
			List<Entity.EntityBPProjectTask> lists = this.BuildSelectListConstruct(idr);
			idr.Close();
			return lists;
		}

		private void BuildAddMap(DbCommand command,EntityBPProjectTask entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddOutParameter(command, "ID", DbType.Int32, 4);
		}

		private void BuildUpdateMap(DbCommand command,EntityBPProjectTask entity)
		{
			BuildAddUpdateMap(command,entity);
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}

		private void BuildAddUpdateMap(DbCommand command,EntityBPProjectTask entity)
		{
			this.DB.AddInParameter(command, "ProjectID", DbType.Int32, entity.ProjectID);
			this.DB.AddInParameter(command, "Running", DbType.Boolean, entity.Running);
			this.DB.AddInParameter(command, "TaskName", DbType.String, entity.TaskName);
			this.DB.AddInParameter(command, "TaskDesc", DbType.String, entity.TaskDesc);
			this.DB.AddInParameter(command, "Protocol", DbType.String, entity.Protocol);
			this.DB.AddInParameter(command, "IO", DbType.String, entity.IO);
			this.DB.AddInParameter(command, "Sequence", DbType.Int32, entity.Sequence);
			this.DB.AddInParameter(command, "MsgHandler", DbType.String, entity.MsgHandler);
			this.DB.AddInParameter(command, "BizHandler", DbType.String, entity.BizHandler);
			this.DB.AddInParameter(command, "RuntimeParas", DbType.String, entity.RuntimeParas);
			this.DB.AddInParameter(command, "RServer", DbType.String, entity.RServer);
			this.DB.AddInParameter(command, "RFolder", DbType.String, entity.RFolder);
			this.DB.AddInParameter(command, "RPort", DbType.Int32, entity.RPort);
			this.DB.AddInParameter(command, "RUID", DbType.String, entity.RUID);
			this.DB.AddInParameter(command, "RPWD", DbType.String, entity.RPWD);
			this.DB.AddInParameter(command, "RCert", DbType.String, entity.RCert);
			this.DB.AddInParameter(command, "FileExt", DbType.String, entity.FileExt);
			this.DB.AddInParameter(command, "LFolder", DbType.String, entity.LFolder);
			this.DB.AddInParameter(command, "ScheduleType", DbType.String, entity.ScheduleType);
			this.DB.AddInParameter(command, "ScheduleMonth", DbType.Int32, entity.ScheduleMonth);
			this.DB.AddInParameter(command, "ScheduleTime", DbType.String, entity.ScheduleTime);
			this.DB.AddInParameter(command, "LastRunTime", DbType.DateTime, entity.LastRunTime);
			this.DB.AddInParameter(command, "Status", DbType.String, entity.Status);
			this.DB.AddInParameter(command, "CreatedDT", DbType.DateTime, entity.CreatedDT);
			this.DB.AddInParameter(command, "UpdatedDT", DbType.DateTime, entity.UpdatedDT);
			this.DB.AddInParameter(command, "CreatedBy", DbType.Int32, entity.CreatedBy);
			this.DB.AddInParameter(command, "UpdatedBy", DbType.Int32, entity.UpdatedBy);
			this.DB.AddInParameter(command, "ScheduleWeekDay", DbType.String, entity.ScheduleWeekDay);
			this.DB.AddInParameter(command, "ScheduleInterval", DbType.Int32, entity.ScheduleInterval);
			this.DB.AddInParameter(command, "NextRunTime", DbType.DateTime, entity.NextRunTime);
		}
		private void BuildDeleteMap(DbCommand command,EntityBPProjectTask entity)
		{
			this.DB.AddInParameter(command, "ID", DbType.Int32, entity.ID);
		}
		private EntityBPProjectTask BuildSelectConstruct(IDataReader idr)
		{
			List<EntityBPProjectTask> lists = BuildSelectListConstruct(idr);
			if (lists != null && lists.Count > 0)
				return lists[0];
			else
				return null;
		}
		private List<EntityBPProjectTask> BuildSelectListConstruct(IDataReader idr)
		{
			List<EntityBPProjectTask> lists = new List<EntityBPProjectTask>();
			while (idr.Read())
			{
				EntityBPProjectTask entity = new EntityBPProjectTask();
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

				index = idr.GetOrdinal("Running");
				if (!idr.IsDBNull(index))
				{
					entity.Running = idr.GetBoolean(index);
				}

				index = idr.GetOrdinal("TaskName");
				if (!idr.IsDBNull(index))
				{
					entity.TaskName = idr.GetString(index);
				}

				index = idr.GetOrdinal("TaskDesc");
				if (!idr.IsDBNull(index))
				{
					entity.TaskDesc = idr.GetString(index);
				}

				index = idr.GetOrdinal("Protocol");
				if (!idr.IsDBNull(index))
				{
					entity.Protocol = idr.GetString(index);
				}

				index = idr.GetOrdinal("IO");
				if (!idr.IsDBNull(index))
				{
					entity.IO = idr.GetString(index);
				}

				index = idr.GetOrdinal("Sequence");
				if (!idr.IsDBNull(index))
				{
					entity.Sequence = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("MsgHandler");
				if (!idr.IsDBNull(index))
				{
					entity.MsgHandler = idr.GetString(index);
				}

				index = idr.GetOrdinal("BizHandler");
				if (!idr.IsDBNull(index))
				{
					entity.BizHandler = idr.GetString(index);
				}

				index = idr.GetOrdinal("RuntimeParas");
				if (!idr.IsDBNull(index))
				{
					entity.RuntimeParas = idr.GetString(index);
				}

				index = idr.GetOrdinal("RServer");
				if (!idr.IsDBNull(index))
				{
					entity.RServer = idr.GetString(index);
				}

				index = idr.GetOrdinal("RFolder");
				if (!idr.IsDBNull(index))
				{
					entity.RFolder = idr.GetString(index);
				}

				index = idr.GetOrdinal("RPort");
				if (!idr.IsDBNull(index))
				{
					entity.RPort = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("RUID");
				if (!idr.IsDBNull(index))
				{
					entity.RUID = idr.GetString(index);
				}

				index = idr.GetOrdinal("RPWD");
				if (!idr.IsDBNull(index))
				{
					entity.RPWD = idr.GetString(index);
				}

				index = idr.GetOrdinal("RCert");
				if (!idr.IsDBNull(index))
				{
					entity.RCert = idr.GetString(index);
				}

				index = idr.GetOrdinal("FileExt");
				if (!idr.IsDBNull(index))
				{
					entity.FileExt = idr.GetString(index);
				}

				index = idr.GetOrdinal("LFolder");
				if (!idr.IsDBNull(index))
				{
					entity.LFolder = idr.GetString(index);
				}

				index = idr.GetOrdinal("ScheduleType");
				if (!idr.IsDBNull(index))
				{
					entity.ScheduleType = idr.GetString(index);
				}

				index = idr.GetOrdinal("ScheduleMonth");
				if (!idr.IsDBNull(index))
				{
					entity.ScheduleMonth = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("ScheduleTime");
				if (!idr.IsDBNull(index))
				{
					entity.ScheduleTime = idr.GetString(index);
				}

				index = idr.GetOrdinal("LastRunTime");
				if (!idr.IsDBNull(index))
				{
					entity.LastRunTime = idr.GetDateTime(index);
				}

				index = idr.GetOrdinal("Status");
				if (!idr.IsDBNull(index))
				{
					entity.Status = idr.GetString(index);
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

				index = idr.GetOrdinal("ScheduleWeekDay");
				if (!idr.IsDBNull(index))
				{
					entity.ScheduleWeekDay = idr.GetString(index);
				}

				index = idr.GetOrdinal("ScheduleInterval");
				if (!idr.IsDBNull(index))
				{
					entity.ScheduleInterval = idr.GetInt32(index);
				}

				index = idr.GetOrdinal("NextRunTime");
				if (!idr.IsDBNull(index))
				{
					entity.NextRunTime = idr.GetDateTime(index);
				}

				lists.Add(entity);
			}
			return lists;
		}

	}
}
