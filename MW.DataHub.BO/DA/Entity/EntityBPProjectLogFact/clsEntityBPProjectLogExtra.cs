using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace MW.DataHub.BO
{
	public partial interface IBPProjectLog : IBase<Entity.EntityBPProjectLog>
	{
        void RemoveEntityByPK(int ID,int ProjectID);
        Entity.EntityBPProjectLog GetEntityByPK(int ID,int ProjectID);
        DataTable getProjectLogList(string LogType, DateTime DTStart, DateTime DTEnd, string Content ,int PageSize,int PageIndex,ref int PageCount,int ProjectID,string Key);
	}

	partial class clsBPProjectLog : clsBase<Entity.EntityBPProjectLog>, IBPProjectLog
	{
        public DataTable getProjectLogList(string LogType, DateTime DTStart, DateTime DTEnd, string Content, int PageSize, int PageIndex, ref int PageCount,int ProjectID,string Key)
        {
            DbCommand command = objRep.DB.GetStoredProcCommand("spGetProjectLogList");
            try
            {
                objRep.DB.AddInParameter(command, "@LogType", DbType.String, LogType);
                objRep.DB.AddInParameter(command, "@DTStart", DbType.DateTime, DTStart);
                objRep.DB.AddInParameter(command, "@DTEnd", DbType.DateTime, DTEnd);
                objRep.DB.AddInParameter(command, "@Content", DbType.String, Content);
                objRep.DB.AddInParameter(command, "@PageSize", DbType.Int32, PageSize);
                objRep.DB.AddInParameter(command, "@PageIndex", DbType.Int32, PageIndex);
                objRep.DB.AddOutParameter(command, "@PageCount", DbType.Int32, PageCount);
                objRep.DB.AddInParameter(command, "@ProjectID", DbType.Int32, ProjectID);
                objRep.DB.AddInParameter(command, "@Key", DbType.String, Key);
                DataSet ds = objRep.DB.ExecuteDataSet(command);
                PageCount =Convert.ToInt32( command.Parameters["@PageCount"].Value);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spGetProjectLogList error:" + ex.ToString());
            }
        }

        public override int SaveSQLData(Entity.EntityBPProjectLog objEntity, string LogonID)
        {
            if (objEntity.ProjectID <= 0)
            {
                throw new Exception("ProjectID shuold be more than 0");
            }

            string sql = "exec spCreateTableProjectLogByProjectID "+objEntity.ProjectID;
            sql += " if exists(select 1 from [BPProjectLog_" + objEntity.ProjectID + "] where ID=@ID)";
            sql += " begin";
            sql += " update [BPProjectLog_" + objEntity.ProjectID + "] set [LogTime]=@LogTime,[LogType]=@LogType,[KeyValue]=@KeyValue,[Log]=@Log,Source=@Source ";
            sql += " where ID=@ID and ProjectID=@ProjectID";
            sql += " end";
            sql += " else";
            sql += " begin";
            sql += " insert into [BPProjectLog_" + objEntity.ProjectID + "] ([LogTime],[LogType],[KeyValue],[Log],[Source],[ProjectID]) values (@LogTime,@LogType,@KeyValue,@Log,@Source,@ProjectID)";
            sql += " end";
            DbCommand command = DB.GetSqlStringCommand(sql);
            DB.AddInParameter(command, "@LogTime", DbType.DateTime,objEntity.LogTime);
            DB.AddInParameter(command, "@LogType", DbType.String, objEntity.LogType);
            DB.AddInParameter(command, "@KeyValue", DbType.String, objEntity.KeyValue);
            DB.AddInParameter(command, "@Log", DbType.String, objEntity.Log);
            DB.AddInParameter(command, "@Source", DbType.String, objEntity.Source);
            DB.AddInParameter(command, "@ProjectID", DbType.Int32, objEntity.ProjectID);
            DB.AddInParameter(command, "@ID", DbType.Int32, objEntity.ID);
            if (DB.ExecuteNonQuery(command) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override Entity.EntityBPProjectLog GetSQLEntityByPK(Entity.EntityBPProjectLog objEntity)
        {
            objRep.DB.Transaction = this.DB.Transaction;
            if (objEntity.ProjectID <= 0)
            {
                throw new Exception("ProjectID shuold be more than 0");
            }
            string sql = "exec spCreateTableProjectLogByProjectID " + objEntity.ProjectID;
            sql += " select * from [BPProjectLog_" + objEntity.ProjectID + "] where ID=@ID and ProjectID=@ProjectID ";
            DbCommand command = DB.GetSqlStringCommand(sql);
            DB.AddInParameter(command, "@ProjectID", DbType.Int32, objEntity.ProjectID);
            DB.AddInParameter(command, "@ID", DbType.Int32, objEntity.ID);
            
            DataSet ds=DB.ExecuteDataSet(command);
            
            return this.BuildSelectListConstruct(ds)[0];
        }

        public Entity.EntityBPProjectLog GetEntityByPK(int vID,int ProjectID)
        {
            Entity.EntityBPProjectLog entity = new Entity.EntityBPProjectLog();
            entity.ID = vID;
            entity.ProjectID = ProjectID;
            return this.GetSQLEntityByPK(entity);
        }

        public override void RemoveSQLEntityByPK(Entity.EntityBPProjectLog objEntity)
        {
            objRep.DB.Transaction = this.DB.Transaction;
            if (objEntity.ProjectID <= 0)
            {
                throw new Exception("ProjectID shuold be more than 0");
            }
            string sql = "exec spCreateTableProjectLogByProjectID " + objEntity.ProjectID;
            sql += " delete [BPProjectLog_" + objEntity.ProjectID + "] where ID=@ID and ProjectID=@ProjectID ";
            DbCommand command = DB.GetSqlStringCommand(sql);
            DB.AddInParameter(command, "@ProjectID", DbType.Int32, objEntity.ProjectID);
            DB.AddInParameter(command, "@ID", DbType.Int32, objEntity.ID);
            DB.ExecuteNonQuery(command);
        }

        public void RemoveEntityByPK(int vID,int ProjectID)
        {
            Entity.EntityBPProjectLog entity = new Entity.EntityBPProjectLog();
            entity.ID = vID;
            entity.ProjectID = ProjectID;
            this.RemoveSQLEntityByPK(entity);
        }

        private System.Collections.Generic.List<Entity.EntityBPProjectLog> BuildSelectListConstruct(DataSet ds)
        {
            System.Collections.Generic.List<Entity.EntityBPProjectLog> lists = new System.Collections.Generic.List<Entity.EntityBPProjectLog>();
            lists = this.GetSQLEntitiesByDatatable(ds.Tables[0]);
            return lists;
        }
        
	}
}
