using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MW.DataHub.BO
{
	public partial interface IBPMailLog : IBase<Entity.EntityBPMailLog>
	{
        DataTable GetProjectNtfList(int ProjectID, DateTime DateTStart, DateTime DateTEnd, string Subject, string Content, ref int PageCount, int PageIndex, int PageSize);
	}

	partial class clsBPMailLog : clsBase<Entity.EntityBPMailLog>, IBPMailLog
	{
        public DataTable GetProjectNtfList(int ProjectID, DateTime DateTStart, DateTime DateTEnd, string Subject, string Content, ref int PageCount, int PageIndex, int PageSize)
        {
            DbCommand command = objRep.DB.GetStoredProcCommand("spGetProjectNtfList");
            try
            {
                DB.AddInParameter(command, "@ProjectID", DbType.Int32, ProjectID);
                DB.AddInParameter(command, "@DateTStart", DbType.DateTime, DateTStart);
                DB.AddInParameter(command, "@DateTEnd", DbType.DateTime, DateTEnd);
                DB.AddInParameter(command, "@Subject", DbType.String, Subject);
                DB.AddInParameter(command, "@Content", DbType.String, Content);
                DB.AddOutParameter(command, "@PageCount", DbType.Int32, PageCount);
                DB.AddInParameter(command, "@PageIndex", DbType.Int32, PageIndex);
                DB.AddInParameter(command, "@PageSize", DbType.Int32, PageSize);
                DataSet ds = DB.ExecuteDataSet(command);
                PageCount = Convert.ToInt32(command.Parameters["@PageCount"].Value);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spGetProjectNtfList error:" + ex.ToString());
            }
        }

	}
}
