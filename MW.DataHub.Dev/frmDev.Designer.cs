namespace EDIPlatformDev
{
    partial class frmDev
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRun = new System.Windows.Forms.Button();
            this.lstMsg = new System.Windows.Forms.ListBox();
            this.btnAutoRun = new System.Windows.Forms.Button();
            this.tmrAutoRun = new System.Windows.Forms.Timer(this.components);
            this.btnProcessRevFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run one time";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lstMsg
            // 
            this.lstMsg.FormattingEnabled = true;
            this.lstMsg.ItemHeight = 12;
            this.lstMsg.Location = new System.Drawing.Point(12, 65);
            this.lstMsg.Name = "lstMsg";
            this.lstMsg.Size = new System.Drawing.Size(389, 148);
            this.lstMsg.TabIndex = 1;
            // 
            // btnAutoRun
            // 
            this.btnAutoRun.Location = new System.Drawing.Point(114, 12);
            this.btnAutoRun.Name = "btnAutoRun";
            this.btnAutoRun.Size = new System.Drawing.Size(75, 23);
            this.btnAutoRun.TabIndex = 2;
            this.btnAutoRun.Text = "Auto Run";
            this.btnAutoRun.UseVisualStyleBackColor = true;
            this.btnAutoRun.Click += new System.EventHandler(this.btnAutoRun_Click);
            // 
            // tmrAutoRun
            // 
            this.tmrAutoRun.Interval = 60000;
            this.tmrAutoRun.Tick += new System.EventHandler(this.tmrAutoRun_Tick);
            // 
            // btnProcessRevFile
            // 
            this.btnProcessRevFile.Location = new System.Drawing.Point(213, 337);
            this.btnProcessRevFile.Name = "btnProcessRevFile";
            this.btnProcessRevFile.Size = new System.Drawing.Size(75, 23);
            this.btnProcessRevFile.TabIndex = 3;
            this.btnProcessRevFile.Text = "Process rev file";
            this.btnProcessRevFile.UseVisualStyleBackColor = true;
            // 
            // frmDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 405);
            this.Controls.Add(this.btnProcessRevFile);
            this.Controls.Add(this.btnAutoRun);
            this.Controls.Add(this.lstMsg);
            this.Controls.Add(this.btnRun);
            this.Name = "frmDev";
            this.Text = "EDI Platform Development mode";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ListBox lstMsg;
        private System.Windows.Forms.Button btnAutoRun;
        private System.Windows.Forms.Timer tmrAutoRun;
        private System.Windows.Forms.Button btnProcessRevFile;
    }
}

