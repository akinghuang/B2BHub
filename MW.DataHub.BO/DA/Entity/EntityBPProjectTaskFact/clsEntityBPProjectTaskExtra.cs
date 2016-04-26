using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace MW.DataHub.BO
{
    public partial interface IBPProjectTask : IBase<Entity.EntityBPProjectTask>
    {
        DataTable getTaskListByProjectID(int ProjectID);
        void updateTaskListByID(int ID);
        Entity.EntityBPProjectTask getTaskListByTaskID(Entity.EntityBPProjectTask entityTask);

        System.Collections.Generic.List<Entity.EntityBPProjectTask> getTaskEntityListByProjectID(int ProjectID);
    }

	partial class clsBPProjectTask : clsBase<Entity.EntityBPProjectTask>, IBPProjectTask
	{

        protected override void SaveSQLDataAdd(Entity.EntityBPProjectTask entity, string LogonID)
        {
            //entity.CreatedBy = Convert.ToInt32(LogonID);
            entity.CreatedDT = this.CurrentDateTime;
            //entity.UpdatedBy = entity.CreatedBy;
            entity.UpdatedDT = entity.CreatedDT;
        }
        protected override void SaveSQLDataSave(Entity.EntityBPProjectTask entity, string LogonID)
        {
            //entity.UpdatedBy = Convert.ToInt32(LogonID);
            entity.UpdatedDT = this.CurrentDateTime;
        }


        public DataTable getTaskListByProjectID(int ProjectID)
        {
            DbCommand Command = DB.GetStoredProcCommand("spBPGetTaskListByProjectID");
            try
            {
                DB.AddInParameter(Command, "@ProjectID", DbType.Int32, ProjectID);
                DataSet ds = DB.ExecuteDataSet(Command);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPGetTaskListByProjectID Error:" + ex.ToString());
            }
        }

        public System.Collections.Generic.List<Entity.EntityBPProjectTask> getTaskEntityListByProjectID(int ProjectID)
        {
            try
            {
                return this.GetSQLEntitiesByDatatable(getTaskListByProjectID(ProjectID));
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPGetTaskListByProjectID Error:" + ex.ToString());
            }
        }

        public void updateTaskListByID(int ID)
        {
            DbCommand Command = DB.GetStoredProcCommand("spBPUpdateTaskListByID");
            try
            {
                DB.AddInParameter(Command, "@ID", DbType.Int32, ID);
                DB.ExecuteNonQuery(Command);

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPUpdateTaskListByID Error:" + ex.ToString());
            }
        }

        public Entity.EntityBPProjectTask getTaskListByTaskID(Entity.EntityBPProjectTask entityTask)
        {
            return GetEntityByPK(entityTask.ID);
            
        }

	}
}
