namespace DR
{
    partial class Settings
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
            this.code = new System.Windows.Forms.TextBox();
            this.hotkey = new System.Windows.Forms.TextBox();
            this.save = new System.Windows.Forms.Button();
            this.labelCode = new System.Windows.Forms.Label();
            this.labelHotkey = new System.Windows.Forms.Label();
            this.labelProcess = new System.Windows.Forms.Label();
            this.processName = new System.Windows.Forms.ComboBox();
            this.presetName = new System.Windows.Forms.ComboBox();
            this.labelPreset = new System.Windows.Forms.Label();
            this.addbinding = new System.Windows.Forms.Button();
            this.removebinding = new System.Windows.Forms.Button();
            this.addpreset = new System.Windows.Forms.Button();
            this.removepreset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // code
            // 
            this.code.Location = new System.Drawing.Point(36, 229);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(124, 22);
            this.code.TabIndex = 0;
            this.code.TextChanged += new System.EventHandler(this.code_TextChanged);
            // 
            // hotkey
            // 
            this.hotkey.Location = new System.Drawing.Point(181, 229);
            this.hotkey.Name = "hotkey";
            this.hotkey.Size = new System.Drawing.Size(133, 22);
            this.hotkey.TabIndex = 1;
            this.hotkey.TextChanged += new System.EventHandler(this.hotkey_TextChanged);
            this.hotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotkey_KeyDown);
            // 
            // save
            // 
            this.save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save.Location = new System.Drawing.Point(181, 348);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(137, 26);
            this.save.TabIndex = 2;
            this.save.Text = "Save changes";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Location = new System.Drawing.Point(33, 210);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(44, 16);
            this.labelCode.TabIndex = 3;
            this.labelCode.Text = "Code:";
            // 
            // labelHotkey
            // 
            this.labelHotkey.AutoSize = true;
            this.labelHotkey.Location = new System.Drawing.Point(178, 210);
            this.labelHotkey.Name = "labelHotkey";
            this.labelHotkey.Size = new System.Drawing.Size(54, 16);
            this.labelHotkey.TabIndex = 4;
            this.labelHotkey.Text = "Hotkey:";
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(35, 13);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(61, 16);
            this.labelProcess.TabIndex = 6;
            this.labelProcess.Text = "Process:";
            // 
            // processName
            // 
            this.processName.FormattingEnabled = true;
            this.processName.Location = new System.Drawing.Point(38, 32);
            this.processName.Name = "processName";
            this.processName.Size = new System.Drawing.Size(150, 24);
            this.processName.TabIndex = 7;
            // 
            // presetName
            // 
            this.presetName.FormattingEnabled = true;
            this.presetName.Location = new System.Drawing.Point(38, 97);
            this.presetName.Name = "presetName";
            this.presetName.Size = new System.Drawing.Size(150, 24);
            this.presetName.TabIndex = 8;
            this.presetName.SelectedIndexChanged += new System.EventHandler(this.presetName_SelectedIndexChanged);
            this.presetName.TextChanged += new System.EventHandler(this.presetName_TextChanged);
            this.presetName.Enter += new System.EventHandler(this.presetName_Enter);
            // 
            // labelPreset
            // 
            this.labelPreset.Location = new System.Drawing.Point(38, 71);
            this.labelPreset.Name = "labelPreset";
            this.labelPreset.Size = new System.Drawing.Size(150, 23);
            this.labelPreset.TabIndex = 9;
            this.labelPreset.Text = "Preset:";
            // 
            // addbinding
            // 
            this.addbinding.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addbinding.Location = new System.Drawing.Point(345, 229);
            this.addbinding.Name = "addbinding";
            this.addbinding.Size = new System.Drawing.Size(96, 27);
            this.addbinding.TabIndex = 11;
            this.addbinding.Text = "Add binding";
            this.addbinding.UseVisualStyleBackColor = true;
            this.addbinding.Click += new System.EventHandler(this.addbinding_Click);
            // 
            // removebinding
            // 
            this.removebinding.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removebinding.Location = new System.Drawing.Point(181, 271);
            this.removebinding.Name = "removebinding";
            this.removebinding.Size = new System.Drawing.Size(133, 27);
            this.removebinding.TabIndex = 12;
            this.removebinding.Text = "Remove binding";
            this.removebinding.UseVisualStyleBackColor = true;
            this.removebinding.Click += new System.EventHandler(this.removebinding_Click);
            // 
            // addpreset
            // 
            this.addpreset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addpreset.Location = new System.Drawing.Point(217, 93);
            this.addpreset.Name = "addpreset";
            this.addpreset.Size = new System.Drawing.Size(101, 31);
            this.addpreset.TabIndex = 13;
            this.addpreset.Text = "Add preset:";
            this.addpreset.UseVisualStyleBackColor = true;
            this.addpreset.Click += new System.EventHandler(this.addpreset_Click);
            // 
            // removepreset
            // 
            this.removepreset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removepreset.Location = new System.Drawing.Point(36, 136);
            this.removepreset.Name = "removepreset";
            this.removepreset.Size = new System.Drawing.Size(152, 33);
            this.removepreset.TabIndex = 14;
            this.removepreset.Text = "Remove preset";
            this.removepreset.UseVisualStyleBackColor = true;
            this.removepreset.Click += new System.EventHandler(this.removepreset_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 410);
            this.Controls.Add(this.removepreset);
            this.Controls.Add(this.addpreset);
            this.Controls.Add(this.removebinding);
            this.Controls.Add(this.addbinding);
            this.Controls.Add(this.labelPreset);
            this.Controls.Add(this.presetName);
            this.Controls.Add(this.processName);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.labelHotkey);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.save);
            this.Controls.Add(this.hotkey);
            this.Controls.Add(this.code);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Activated += new System.EventHandler(this.Settings_Activated);
            this.Deactivate += new System.EventHandler(this.Settings_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.TextBox code;
        public  System.Windows.Forms.TextBox hotkey;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.Label labelHotkey;
        private System.Windows.Forms.Label labelProcess;
        public  System.Windows.Forms.ComboBox processName;
        public  System.Windows.Forms.ComboBox presetName;
        private System.Windows.Forms.Label labelPreset;
        private System.Windows.Forms.Button addbinding;
        private System.Windows.Forms.Button removebinding;
        private System.Windows.Forms.Button addpreset;
        private System.Windows.Forms.Button removepreset;
    }
}