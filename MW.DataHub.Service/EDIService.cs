using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using CFG = System.Configuration;
using System.Threading;

using MW.DataHub.BO.Entity;

namespace MW.DataHub.Service
{
    public partial class DimercoEDIService : ServiceBase
    {
        public DimercoEDIService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.tmMainProcess.Interval = CFG.ConfigurationSettings.AppSettings["ServiceInterval"].Trim() == "" ? 60 * 1000 : Convert.ToDouble(CFG.ConfigurationSettings.AppSettings["ServiceInterval"]) * 1000;
            this.tmMainProcess.Start();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            this.tmMainProcess.Stop();
            base.OnStop();
        }

        private void tmMainProcess_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            String LogFolder = CFG.ConfigurationSettings.AppSettings["LogFolder"].ToString().Trim();
            
            try
            {
                //log.wLog("Start process");
                StartProcess();
                //log.wLog("Finished process");
                
            }
            catch (Exception exg)
            {
                if (LogFolder.Trim() != "")
                {
                    if (System.IO.Directory.Exists(LogFolder))
                    {
                        MW.DataHub.BO.Common.LocalLog log = new MW.DataHub.BO.Common.LocalLog(LogFolder);
                        log.wLog("Errow at:" + DateTime.Now.ToString() + ",Desc:" + exg.ToString());
                    }
                }
            }
        }
        private void StartProcess()
        {
            try
            {
                String MachineID = CFG.ConfigurationSettings.AppSettings["MachineID"].ToString().Trim();
                String ProcessID = CFG.ConfigurationSettings.AppSettings["ProcessID"].ToString().Trim();
                String Query = " [Status]='Active' AND HostMachineID='" + MachineID + "' ";
                if (ProcessID.Trim() != "")
                    Query += "AND ProcessID='" + ProcessID + "' ";
                Query += "ORDER BY Sequence asc ";

                MW.DataHub.BO.IBPProject ProjectMgr = MW.DataHub.BO.BOFactory.GetBPProject();
                List<EntityBPProject> Projects = ProjectMgr.getEntityBPProject(Query);
                foreach (EntityBPProject project in Projects)
                {
                    MW.DataHub.BO.Service.clsEDIService Svr = new MW.DataHub.BO.Service.clsEDIService(project);

                    Thread worker = new Thread(new ThreadStart(Svr.ProjectStart));
                    worker.Start();
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Execute process error", e);
            }
        }
    }
}
