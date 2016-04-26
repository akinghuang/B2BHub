using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Data.SqlClient;

namespace DIMERCO.B2B.BO
{
	public partial interface IBPProject : IBase<Entity.EntityBPProject>
	{
        DataTable getProjectList(string ProjectCode, string Status);
        Entity.EntityBPProject getProjectByID(BO.Entity.EntityBPProject entityBPProject);
        void updateProjectList(int ID);
        void updateProjectList(BO.Entity.EntityBPProject entityBPProject, int LogUser);
        int AddProjectList(BO.Entity.EntityBPProject entityBPProject, int LogUser);
	}

	partial class clsBPProject : clsBase<Entity.EntityBPProject>, IBPProject
	{
		
        public DataTable getProjectList(string ProjectCode, string Status)
        {
            DataTable dt = new DataTable();
            DbCommand Command = objRep.DB.GetStoredProcCommand("spBPGetProjectList");
            try
            {
                objRep.DB.AddInParameter(Command, "@ProjectCode", DbType.String, ProjectCode);
                objRep.DB.AddInParameter(Command, "@Status", DbType.String, Status);
                DataSet ds = objRep.DB.ExecuteDataSet(Command);
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPGetBPUSer Error:" + ex.ToString());
            }
        }

        public Entity.EntityBPProject getProjectByID(BO.Entity.EntityBPProject entityBPProject)
        {
            return objRep.GetEntityByID(entityBPProject.ID);
        }

        public void updateProjectList(int ID)
        {
            string query = "update BPProject set Status='Delete' where ID=@ID";
            DbCommand Command = objRep.DB.GetSqlStringCommand(query);
            try
            {
                objRep.DB.AddInParameter(Command, "@ID", DbType.Int32, ID);
                objRep.DB.ExecuteNonQuery(Command);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("updateProjectList Error:" + ex.ToString());
            }
        }

        public void updateProjectList(BO.Entity.EntityBPProject entityBPProject, int LogUser)
        {
            DbCommand Command = objRep.DB.GetStoredProcCommand("spBPUpdateProjectList");
            try
            {
                objRep.DB.AddInParameter(Command, "@ID", DbType.Int32, entityBPProject.ID);
                objRep.DB.AddInParameter(Command, "@Sequence", DbType.Int32, entityBPProject.Sequence);
                objRep.DB.AddInParameter(Command, "@Status", DbType.String, entityBPProject.Status);
                objRep.DB.AddInParameter(Command, "@ProjectName", DbType.String, entityBPProject.ProjectName);
                objRep.DB.AddInParameter(Command, "@Owner", DbType.String, entityBPProject.Owner);
                objRep.DB.AddInParameter(Command, "@Description", DbType.String, entityBPProject.ProjectDesc);
                objRep.DB.AddInParameter(Command, "@HostMachineID", DbType.String, entityBPProject.HostMachineID);
                objRep.DB.AddInParameter(Command, "@ProcessID", DbType.String, entityBPProject.ProcessID);
                objRep.DB.AddInParameter(Command, "@Parameters", DbType.String, entityBPProject.RuntimeParas);
                objRep.DB.AddInParameter(Command, "@Adminstrator", DbType.String, entityBPProject.AdminMail);
                objRep.DB.AddInParameter(Command, "@User", DbType.String, entityBPProject.UserMail);
                objRep.DB.AddInParameter(Command, "@LogUser", DbType.Int32, LogUser);
                objRep.DB.ExecuteNonQuery(Command);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPUpdateProjectList Error:" + ex.ToString());
            }
        }

        public int AddProjectList(BO.Entity.EntityBPProject entityBPProject, int LogUser)
        {
            DbCommand Command = objRep.DB.GetStoredProcCommand("spBPAddProjectList");
            int ID = 0;
            try
            {
                objRep.DB.AddInParameter(Command, "@Sequence", DbType.Int32, entityBPProject.Sequence);
                objRep.DB.AddInParameter(Command, "@Status", DbType.String, entityBPProject.Status);
                objRep.DB.AddInParameter(Command, "@ProjectName", DbType.String, entityBPProject.ProjectName);
                objRep.DB.AddInParameter(Command, "@Owner", DbType.String, entityBPProject.Owner);
                objRep.DB.AddInParameter(Command, "@Description", DbType.String, entityBPProject.ProjectDesc);
                objRep.DB.AddInParameter(Command, "@HostMachineID", DbType.String, entityBPProject.HostMachineID);
                objRep.DB.AddInParameter(Command, "@ProcessID", DbType.String, entityBPProject.ProcessID);
                objRep.DB.AddInParameter(Command, "@Parameters", DbType.String, entityBPProject.RuntimeParas);
                objRep.DB.AddInParameter(Command, "@Adminstrator", DbType.String, entityBPProject.AdminMail);
                objRep.DB.AddInParameter(Command, "@User", DbType.String, entityBPProject.UserMail);
                objRep.DB.AddInParameter(Command, "@LogUser", DbType.Int32, LogUser);
                objRep.DB.ExecuteNonQuery(Command);
                ID=Convert.ToInt32(DB.GetParameterValue(Command, "@ID"));
                return ID;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("spBPAddProjectList Error:" + ex.ToString());
            }
        }

	}
}
