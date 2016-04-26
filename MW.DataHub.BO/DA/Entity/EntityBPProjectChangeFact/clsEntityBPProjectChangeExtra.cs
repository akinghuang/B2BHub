using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace MW.DataHub.BO
{
	public partial interface IBPProjectChange : IBase<Entity.EntityBPProjectChange>
	{
        System.Data.DataTable getProjectChangeByProjectID(int ProjectID);
        System.Collections.Generic.List<Entity.EntityBPProjectChange> getEntityBPProjectChange(int ProjectID);
        void DeleteProjectChangeByProjectChangeID(int ProjectChangeID);
	}

	partial class clsBPProjectChange : clsBase<Entity.EntityBPProjectChange>, IBPProjectChange
	{

        protected override void SaveSQLDataAdd(Entity.EntityBPProjectChange entity, string LogonID)
        {
            entity.CreatedBy = Convert.ToInt32(LogonID);
            entity.CreatedDT = this.CurrentDateTime;
            entity.UpdatedBy = entity.CreatedBy;
            entity.UpdatedDT = entity.CreatedDT;
        }
        protected override void SaveSQLDataSave(Entity.EntityBPProjectChange entity, string LogonID)
        {
            entity.UpdatedBy =Convert.ToInt32(LogonID);
            entity.UpdatedDT = this.CurrentDateTime;
        }

        public System.Data.DataTable getProjectChangeByProjectID(int ProjectID)
        {
            DbCommand Command = objRep.DB.GetSqlStringCommand("select * from BPProjectChange where ProjectID=@ProjectID");
            try
            {
                objRep.DB.AddInParameter(Command, "@ProjectID", DbType.String, ProjectID);
                DataSet ds = objRep.DB.ExecuteDataSet(Command);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("getProjectChangeByProjectID Error:" + ex.ToString());
            }
        }

        public System.Collections.Generic.List<Entity.EntityBPProjectChange> getEntityBPProjectChange(int ProjectID)
        {
            return GetSQLEntitiesByDatatable(getProjectChangeByProjectID(ProjectID));
        }

        public void DeleteProjectChangeByProjectChangeID(int ProjectChangeID)
        {
            DbCommand Command = objRep.DB.GetSqlStringCommand("Delete BPProjectChange where ID=@ProjectChangeID");
            try
            {
                objRep.DB.AddInParameter(Command, "@ProjectChangeID", DbType.String, ProjectChangeID);
                objRep.DB.ExecuteNonQuery(Command);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("DeleteProjectChangeByProjectID Error:" + ex.ToString());
            }
        }
	}
}
