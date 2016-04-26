using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace DIMERCO.B2B.BO
{
    public partial interface IBPProjectTask : IBase<Entity.EntityBPProjectTask>
    {
        DataTable getTaskListByProjectID(int ProjectID);
        void updateTaskListByID(int ID);
        Entity.EntityBPProjectTask getTaskListByTaskID(Entity.EntityBPProjectTask entityTask);
        void AddTaskList(Entity.EntityBPProjectTask entityTask, string UserID);


        System.Collections.Generic.List<Entity.EntityBPProjectTask> getTaskEntityListByProjectID(int ProjectID);
    }

	partial class clsBPProjectTask : clsBase<Entity.EntityBPProjectTask>, IBPProjectTask
	{
        public DataTable getTaskListByProjectID(int ProjectID)
        {
            DataTable dt = new DataTable();
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
            return GetEntityById(entityTask.ID);
        }

        public void AddTaskList(Entity.EntityBPProjectTask entityTask,string UserID)
        {
            this.SaveSQLDataAdd(entityTask, UserID);
        }

	}
}
