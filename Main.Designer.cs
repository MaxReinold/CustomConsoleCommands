namespace CustomConsoleCommands
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.refresh = new System.Windows.Forms.Timer(this.components);
            this.refreshRateLbl = new System.Windows.Forms.Label();
            this.consoleBox = new System.Windows.Forms.RichTextBox();
            this.devOptionsEnabled = new System.Windows.Forms.CheckBox();
            this.blacklistWords = new System.Windows.Forms.RichTextBox();
            this.submitBlacklistBtn = new System.Windows.Forms.Button();
            this.resetLogFileBtn = new System.Windows.Forms.Button();
            this.devInput = new System.Windows.Forms.RichTextBox();
            this.devInputBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // refresh
            // 
            this.refresh.Interval = 7;
            this.refresh.Tick += new System.EventHandler(this.refresh_Tick);
            // 
            // refreshRateLbl
            // 
            this.refreshRateLbl.AutoSize = true;
            this.refreshRateLbl.Location = new System.Drawing.Point(14, 454);
            this.refreshRateLbl.Name = "refreshRateLbl";
            this.refreshRateLbl.Size = new System.Drawing.Size(85, 16);
            this.refreshRateLbl.TabIndex = 0;
            this.refreshRateLbl.Text = "RefreshRate: ";
            // 
            // consoleBox
            // 
            this.consoleBox.AcceptsTab = true;
            this.consoleBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.consoleBox.DetectUrls = false;
            this.consoleBox.Location = new System.Drawing.Point(15, 30);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.Size = new System.Drawing.Size(645, 209);
            this.consoleBox.TabIndex = 1;
            this.consoleBox.Text = "";
            // 
            // devOptionsEnabled
            // 
            this.devOptionsEnabled.AutoSize = true;
            this.devOptionsEnabled.Location = new System.Drawing.Point(14, 3);
            this.devOptionsEnabled.Name = "devOptionsEnabled";
            this.devOptionsEnabled.Size = new System.Drawing.Size(185, 20);
            this.devOptionsEnabled.TabIndex = 2;
            this.devOptionsEnabled.Text = "Developer Options Enabled";
            this.devOptionsEnabled.UseVisualStyleBackColor = true;
            this.devOptionsEnabled.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // blacklistWords
            // 
            this.blacklistWords.DetectUrls = false;
            this.blacklistWords.Location = new System.Drawing.Point(669, 30);
            this.blacklistWords.Name = "blacklistWords";
            this.blacklistWords.Size = new System.Drawing.Size(231, 421);
            this.blacklistWords.TabIndex = 5;
            this.blacklistWords.Text = "";
            // 
            // submitBlacklistBtn
            // 
            this.submitBlacklistBtn.Location = new System.Drawing.Point(669, 454);
            this.submitBlacklistBtn.Name = "submitBlacklistBtn";
            this.submitBlacklistBtn.Size = new System.Drawing.Size(232, 25);
            this.submitBlacklistBtn.TabIndex = 6;
            this.submitBlacklistBtn.Text = "Save Blacklist";
            this.submitBlacklistBtn.UseVisualStyleBackColor = true;
            this.submitBlacklistBtn.Click += new System.EventHandler(this.submitBlacklistBtn_Click);
            // 
            // resetLogFileBtn
            // 
            this.resetLogFileBtn.Location = new System.Drawing.Point(174, 451);
            this.resetLogFileBtn.Name = "resetLogFileBtn";
            this.resetLogFileBtn.Size = new System.Drawing.Size(339, 23);
            this.resetLogFileBtn.TabIndex = 7;
            this.resetLogFileBtn.Text = "Reset Log File to Improve FPS (Game must be closed)";
            this.resetLogFileBtn.UseVisualStyleBackColor = true;
            this.resetLogFileBtn.Click += new System.EventHandler(this.resetLogFileBtn_Click);
            // 
            // devInput
            // 
            this.devInput.AcceptsTab = true;
            this.devInput.Cursor = System.Windows.Forms.Cursors.Default;
            this.devInput.DetectUrls = false;
            this.devInput.Location = new System.Drawing.Point(14, 242);
            this.devInput.Name = "devInput";
            this.devInput.Size = new System.Drawing.Size(645, 209);
            this.devInput.TabIndex = 1;
            this.devInput.Text = "";
            // 
            // devInputBtn
            // 
            this.devInputBtn.Location = new System.Drawing.Point(564, 451);
            this.devInputBtn.Name = "devInputBtn";
            this.devInputBtn.Size = new System.Drawing.Size(75, 23);
            this.devInputBtn.TabIndex = 8;
            this.devInputBtn.Text = "Enter";
            this.devInputBtn.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 480);
            this.Controls.Add(this.devInputBtn);
            this.Controls.Add(this.devInput);
            this.Controls.Add(this.resetLogFileBtn);
            this.Controls.Add(this.submitBlacklistBtn);
            this.Controls.Add(this.blacklistWords);
            this.Controls.Add(this.devOptionsEnabled);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.refreshRateLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "CS:GO Advanced Console";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer refresh;
        private System.Windows.Forms.Label refreshRateLbl;
        public System.Windows.Forms.RichTextBox consoleBox;
        private System.Windows.Forms.CheckBox devOptionsEnabled;
        private System.Windows.Forms.RichTextBox blacklistWords;
        private System.Windows.Forms.Button submitBlacklistBtn;
        private System.Windows.Forms.Button resetLogFileBtn;
        private System.Windows.Forms.RichTextBox devInput;
        private System.Windows.Forms.Button devInputBtn;
    }
}

