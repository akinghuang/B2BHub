using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MW.DataHub.BO.Entity;
using System.Threading;
using System.Configuration;

namespace EDIPlatformDev
{
    public partial class frmDev : Form
    {
        public frmDev()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            String MachineID = ConfigurationSettings.AppSettings["MachineID"].ToString().Trim();
            String ProcessID = ConfigurationSettings.AppSettings["ProcessID"].ToString().Trim();
            String Query = " [Status]='Active' AND HostMachineID='"+MachineID+"' ";
            if(ProcessID.Trim()!="")
                Query+="AND ProcessID='"+ProcessID+"' ";
           // Query+="AND ProjectName='project' ";
            Query+="ORDER BY Sequence asc ";

            MW.DataHub.BO.IBPProject ProjectMgr = MW.DataHub.BO.BOFactory.GetBPProject();
            List<EntityBPProject> Projects = ProjectMgr.getEntityBPProject(Query);
            foreach (EntityBPProject project in Projects)
            {
                MW.DataHub.BO.Service.clsEDIService Svr = new MW.DataHub.BO.Service.clsEDIService(project);
                
                Thread worker = new Thread(new ThreadStart(Svr.ProjectStart));
                worker.Start();
                Thread.Sleep(100);
                //Svr.ProjectStart();
            }
        }

        private void tmrAutoRun_Tick(object sender, EventArgs e)
        {
            btnRun_Click(null, null);
        }

        private void btnAutoRun_Click(object sender, EventArgs e)
        {
            if (btnAutoRun.Text == "Auto Run")
            {
                btnAutoRun.Text = "Stop";
                tmrAutoRun.Start();
            }
            else
            {
                btnAutoRun.Text = "Auto Run";
                tmrAutoRun.Stop();
            }
        }
    }
}
