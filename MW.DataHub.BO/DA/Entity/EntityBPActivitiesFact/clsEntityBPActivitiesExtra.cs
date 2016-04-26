using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace MW.DataHub.BO
{
	public partial interface IBPActivities : IBase<Entity.EntityBPActivities>
	{
        DataTable GetActivitiesList(string ProjectName, string Status, bool Success, ref int PageCount, int PageSize, int PageIndex);
	}

    partial class clsBPActivities : clsBase<Entity.EntityBPActivities>, IBPActivities
    {
        public DataTable GetActivitiesList(string ProjectName, string Status, bool Success,ref int PageCount,int PageSize,int PageIndex)
        {
            DbCommand command = DB.GetStoredProcCommand("spGetActivitiesList");
            try
            {
                DB.AddInParameter(command, "@ProjectName", DbType.String, ProjectName);
                DB.AddInParameter(command, "@Status", DbType.String, Status);
                DB.AddInParameter(command, "@Success", DbType.Boolean, Success);
                DB.AddOutParameter(command, "@PageCount", DbType.Int32, PageCount);
                DB.AddInParameter(command, "@pageSize", DbType.Int32, PageSize);
                DB.AddInParameter(command, "@PageIndex", DbType.Int32, PageIndex);
                DataSet ds= DB.ExecuteDataSet(command);
                PageCount =Convert.ToInt32( command.Parameters["@PageCount"].Value);
                return ds.Tables[0];
            }
            catch(SqlException ex)
            {
                throw new ApplicationException("spGetActivitiesList error:"+ex.ToString());
            }
        }

        public override int SaveSQLData(MW.DataHub.BO.Entity.EntityBPActivities objEntity, string LoginID)
        {
            if (objEntity.ProjectID <= 0 || objEntity.TaskID <= 0)
                throw new Exception("Activity's ProjectID and TaskID should be more than 0");
            if (objEntity.LastRunResult == null)
                objEntity.LastRunStatus = false;
            DateTime _dt = new DateTime();
            if (objEntity.LastRunStartDT == _dt || objEntity.LastRunEndDT == _dt)
                throw new Exception("Activity's Run Date did not exists.");

            string strSQL = @"
--declare @ProjectID int, @TaskID int,@Status nvarchar(20)
--declare @StartDT datetime, @EndDT datetime
--declare @RunStatus bit,@RunResult ntext

Insert into BPActivitiyLog([ProjectID],[TaskID]
	,[RunStartDT],[RunEndDT],[RunStatus],[RunResult])
	values(@ProjectID,@TaskID
	,@StartDT,@EndDT,@RunStatus,@RunResult)

if exists(select 1 from BPActivities where ProjectID=@ProjectID and TaskID=@TaskID)
	update BPActivities set [Status]=@Status
	,[LastRunStartDT]=@StartDT,[LastRunEndDT]=@EndDT
	,[LastRunStatus]=@RunStatus,[LastRunResult]=@RunResult
	,[LastSuccessDT]=case when @RunStatus=1 then @EndDT else [LastSuccessDT] end
	,[RunFailTimes]=case when @RunStatus=0 then [RunFailTimes]+1 else 0 end
	where [ProjectID]=@ProjectID and [TaskID]=@TaskID
else
	Insert into BPActivities([ProjectID],[TaskID],[Status]
	,[LastRunStartDT],[LastRunEndDT],[LastRunStatus],[LastRunResult]
	,[LastSuccessDT],[RunFailTimes])
	values(@ProjectID,@TaskID,@Status
	,@StartDT,@EndDT,@RunStatus,@RunResult
	,case when @RunStatus=1 then @EndDT else null end
	,case when @RunStatus=0 then 1 else 0 end)";

            DbCommand command = this.DB.GetSqlStringCommand(strSQL);
            this.DB.AddInParameter(command, "ProjectID", DbType.Int32, objEntity.ProjectID);
            this.DB.AddInParameter(command, "TaskID", DbType.Int32, objEntity.TaskID);
            this.DB.AddInParameter(command, "Status", DbType.String, objEntity.Status);
            this.DB.AddInParameter(command, "StartDT", DbType.DateTime, objEntity.LastRunStartDT);
            this.DB.AddInParameter(command, "EndDT", DbType.DateTime, objEntity.LastRunEndDT);
            this.DB.AddInParameter(command, "RunStatus", DbType.Boolean, objEntity.LastRunStatus);
            this.DB.AddInParameter(command, "RunResult", DbType.String, objEntity.LastRunResult);
            if (this.DB.ExecuteNonQuery(command) > 0)
                return 1;
            else
                return 0;
        }
    }
}
