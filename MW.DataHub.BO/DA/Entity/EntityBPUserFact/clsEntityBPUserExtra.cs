using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace MW.DataHub.BO
{
	public partial interface IBPUser : IBase<Entity.EntityBPUser>
	{
        DataTable getBPUser(string UserID,string UserIP);
        DataTable getBPUserList(string UserID,string UserName,string Status);
	}

	partial class clsBPUser : clsBase<Entity.EntityBPUser>, IBPUser
	{
        public DataTable getBPUser(string UserID,string UserIP)
        {
            DataTable dt = new DataTable();
            DbCommand Command = DB.GetStoredProcCommand("spBPGetBPUSer");
            try
            {
                DB.AddInParameter(Command, "@UserID", DbType.String, UserID);
                DB.AddInParameter(Command, "@UserIP", DbType.String, UserIP);
                DataSet ds = DB.ExecuteDataSet(Command);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPGetBPUSer Error:" + ex.ToString());
            }
        }

        public DataTable getBPUserList(string UserID, string UserName, string Status)
        {
            DataTable dt = new DataTable();
            DbCommand Command = DB.GetStoredProcCommand("spBPGetBPUSerList");
            try
            {
                DB.AddInParameter(Command, "@UserID", DbType.String, UserID);
                DB.AddInParameter(Command, "@UserName", DbType.String, UserName);
                DB.AddInParameter(Command, "@Status", DbType.String, Status);
                DataSet ds = DB.ExecuteDataSet(Command);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPGetBPUSerList Error:" + ex.ToString());
            }
        }
	}
}
