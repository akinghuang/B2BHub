using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace MW.DataHub.BO
{
	public partial interface IBPActivitiyLog : IBase<Entity.EntityBPActivitiyLog>
	{
        DataTable GetActivitiesLogByProjectID(int ProjectID, DateTime DateTStart, DateTime DateTEnd, ref int PageCount, int PageSize, int PageIndex);
        DataTable GetActivitiesLogByTaskID(int TaskID, DateTime DateTStart, DateTime DateTEnd, ref int PageCount, int PageSize, int PageIndex);
	}

	partial class clsBPActivitiyLog : clsBase<Entity.EntityBPActivitiyLog>, IBPActivitiyLog
	{
        public DataTable GetActivitiesLogByProjectID(int ProjectID, DateTime DateTStart, DateTime DateTEnd, ref int PageCount, int PageSize, int PageIndex)
        {
            DbCommand command = DB.GetStoredProcCommand("spGetActivitiesLogByProjectID");
            try
            {
                DB.AddInParameter(command, "@ProjectID", DbType.Int32, ProjectID);
                DB.AddInParameter(command, "@DateTStart", DbType.DateTime, DateTStart);
                DB.AddInParameter(command, "@DateTEnd", DbType.DateTime, DateTEnd);
                DB.AddOutParameter(command, "@PageCount", DbType.Int32, PageCount);
                DB.AddInParameter(command, "@pageSize", DbType.Int32, PageSize);
                DB.AddInParameter(command, "@PageIndex", DbType.Int32, PageIndex);
                DataSet ds = DB.ExecuteDataSet(command);
                PageCount = Convert.ToInt32(command.Parameters["@PageCount"].Value);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spGetActivitiesLogByProjectID Error:" + ex.ToString());
            }

        }

        public DataTable GetActivitiesLogByTaskID(int TaskID, DateTime DateTStart, DateTime DateTEnd, ref int PageCount, int PageSize, int PageIndex)
        {
            DbCommand command = DB.GetStoredProcCommand("spGetActivitiesLogByTaskID");
            try
            {
                DB.AddInParameter(command, "@TaskID", DbType.Int32, TaskID);
                DB.AddInParameter(command, "@DateTStart", DbType.DateTime, DateTStart);
                DB.AddInParameter(command, "@DateTEnd", DbType.DateTime, DateTEnd);
                DB.AddOutParameter(command, "@PageCount", DbType.Int32, PageCount);
                DB.AddInParameter(command, "@pageSize", DbType.Int32, PageSize);
                DB.AddInParameter(command, "@PageIndex", DbType.Int32, PageIndex);
                DataSet ds = DB.ExecuteDataSet(command);
                PageCount = Convert.ToInt32(command.Parameters["@PageCount"].Value);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spGetActivitiesLogByTaskID Error:" + ex.ToString());
            }

        }

	}
}
