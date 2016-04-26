using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace DIMERCO.B2B.BO
{
	public partial interface IBPUser : IBase<Entity.EntityBPUser>
	{
        DataTable getBPUser(string UserID);
	}

	partial class clsBPUser : clsBase<Entity.EntityBPUser>, IBPUser
	{
        public DataTable getBPUser(string UserID)
        {
            DataTable dt = new DataTable();
            DbCommand Command = DB.GetStoredProcCommand("spBPGetBPUSer");
            try
            {
                DB.AddInParameter(Command, "@UserID", DbType.String, UserID);
                DataSet ds = DB.ExecuteDataSet(Command);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPGetBPUSer Error:" + ex.ToString());
            }
        }
	}
}
