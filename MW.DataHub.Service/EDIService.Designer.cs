namespace MW.DataHub.Service
{
    partial class DimercoEDIService
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
            this.tmMainProcess = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.tmMainProcess)).BeginInit();
            // 
            // tmMainProcess
            // 
            this.tmMainProcess.Enabled = true;
            this.tmMainProcess.Interval = 1000;
            this.tmMainProcess.Elapsed += new System.Timers.ElapsedEventHandler(this.tmMainProcess_Elapsed);
            // 
            // DimercoEDIService
            // 
            this.CanPauseAndContinue = true;
            this.ServiceName = "Dimerco EDI Service";
            ((System.ComponentModel.ISupportInitialize)(this.tmMainProcess)).EndInit();

        }

        #endregion

        private System.Timers.Timer tmMainProcess;

    }
}
