﻿namespace MW.DataHub.Service
{
    partial class EDIServiceInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MWDataHubServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // MWDataHubServiceInstaller
            // 
            this.MWDataHubServiceInstaller.Description = "MW DataHub Service";
            this.MWDataHubServiceInstaller.DisplayName = "MW DataHub Service";
            this.MWDataHubServiceInstaller.ServiceName = "MW DataHub Service";
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // EDIServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MWDataHubServiceInstaller,
            this.serviceProcessInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceInstaller MWDataHubServiceInstaller;
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;

    }
}