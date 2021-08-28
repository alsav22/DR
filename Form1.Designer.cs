namespace DR
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.portName = new System.Windows.Forms.Label();
            this.id_vid = new System.Windows.Forms.TextBox();
            this.id_pid = new System.Windows.Forms.TextBox();
            this.vid = new System.Windows.Forms.Label();
            this.pid = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.port = new System.Windows.Forms.Label();
            this.process = new System.Windows.Forms.Label();
            this.processName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonSettings = new System.Windows.Forms.Button();
            this.labelPreset = new System.Windows.Forms.Label();
            this.presetName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // portName
            // 
            this.portName.Location = new System.Drawing.Point(61, 57);
            this.portName.Name = "portName";
            this.portName.Size = new System.Drawing.Size(112, 27);
            this.portName.TabIndex = 0;
            this.portName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // id_vid
            // 
            this.id_vid.Location = new System.Drawing.Point(73, 170);
            this.id_vid.Name = "id_vid";
            this.id_vid.Size = new System.Drawing.Size(100, 22);
            this.id_vid.TabIndex = 1;
            // 
            // id_pid
            // 
            this.id_pid.Location = new System.Drawing.Point(73, 218);
            this.id_pid.Name = "id_pid";
            this.id_pid.Size = new System.Drawing.Size(100, 22);
            this.id_pid.TabIndex = 2;
            // 
            // vid
            // 
            this.vid.AutoSize = true;
            this.vid.Location = new System.Drawing.Point(30, 173);
            this.vid.Name = "vid";
            this.vid.Size = new System.Drawing.Size(37, 16);
            this.vid.TabIndex = 3;
            this.vid.Text = "VID_";
            // 
            // pid
            // 
            this.pid.AutoSize = true;
            this.pid.Location = new System.Drawing.Point(30, 221);
            this.pid.Name = "pid";
            this.pid.Size = new System.Drawing.Size(37, 16);
            this.pid.TabIndex = 4;
            this.pid.Text = "PID_";
            // 
            // ok
            // 
            this.ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok.Location = new System.Drawing.Point(95, 258);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(54, 32);
            this.ok.TabIndex = 5;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.OK_Click);
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(30, 29);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(35, 16);
            this.port.TabIndex = 6;
            this.port.Text = "Port:";
            // 
            // process
            // 
            this.process.AutoSize = true;
            this.process.Location = new System.Drawing.Point(220, 29);
            this.process.Name = "process";
            this.process.Size = new System.Drawing.Size(61, 16);
            this.process.TabIndex = 7;
            this.process.Text = "Process:";
            // 
            // processName
            // 
            this.processName.Location = new System.Drawing.Point(223, 57);
            this.processName.Name = "processName";
            this.processName.Size = new System.Drawing.Size(175, 22);
            this.processName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Device ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Remote Control";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSettings.Location = new System.Drawing.Point(308, 260);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(90, 32);
            this.buttonSettings.TabIndex = 10;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // labelPreset
            // 
            this.labelPreset.AutoSize = true;
            this.labelPreset.Location = new System.Drawing.Point(223, 107);
            this.labelPreset.Name = "labelPreset";
            this.labelPreset.Size = new System.Drawing.Size(50, 16);
            this.labelPreset.TabIndex = 12;
            this.labelPreset.Text = "Preset:";
            // 
            // presetName
            // 
            this.presetName.Location = new System.Drawing.Point(226, 135);
            this.presetName.Name = "presetName";
            this.presetName.Size = new System.Drawing.Size(172, 22);
            this.presetName.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 322);
            this.Controls.Add(this.presetName);
            this.Controls.Add(this.labelPreset);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processName);
            this.Controls.Add(this.process);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.pid);
            this.Controls.Add(this.vid);
            this.Controls.Add(this.id_pid);
            this.Controls.Add(this.id_vid);
            this.Controls.Add(this.portName);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remote Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label portName;
        private System.Windows.Forms.TextBox id_vid;
        private System.Windows.Forms.TextBox id_pid;
        private System.Windows.Forms.Label vid;
        private System.Windows.Forms.Label pid;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label port;
        private System.Windows.Forms.Label process;
        private System.Windows.Forms.TextBox processName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label labelPreset;
        private System.Windows.Forms.TextBox presetName;
    }
}