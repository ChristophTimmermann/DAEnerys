namespace DAEnerys
{
    partial class UpdateWindow
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
            this.pictureInfo = new System.Windows.Forms.PictureBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelCurrentBuildTitle = new System.Windows.Forms.Label();
            this.labelCurrentBuild = new System.Windows.Forms.Label();
            this.labelLatestBuild = new System.Windows.Forms.Label();
            this.labelLatestBuildTitle = new System.Windows.Forms.Label();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.checkNeverAskAgain = new System.Windows.Forms.CheckBox();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureInfo
            // 
            this.pictureInfo.Location = new System.Drawing.Point(12, 12);
            this.pictureInfo.Name = "pictureInfo";
            this.pictureInfo.Size = new System.Drawing.Size(35, 35);
            this.pictureInfo.TabIndex = 0;
            this.pictureInfo.TabStop = false;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(53, 12);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(224, 13);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "There is a new version of DAEnerys available.";
            // 
            // labelCurrentBuildTitle
            // 
            this.labelCurrentBuildTitle.AutoSize = true;
            this.labelCurrentBuildTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentBuildTitle.Location = new System.Drawing.Point(53, 46);
            this.labelCurrentBuildTitle.Name = "labelCurrentBuildTitle";
            this.labelCurrentBuildTitle.Size = new System.Drawing.Size(69, 13);
            this.labelCurrentBuildTitle.TabIndex = 2;
            this.labelCurrentBuildTitle.Text = "Current build:";
            // 
            // labelCurrentBuild
            // 
            this.labelCurrentBuild.AutoSize = true;
            this.labelCurrentBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentBuild.Location = new System.Drawing.Point(128, 46);
            this.labelCurrentBuild.Name = "labelCurrentBuild";
            this.labelCurrentBuild.Size = new System.Drawing.Size(39, 13);
            this.labelCurrentBuild.TabIndex = 3;
            this.labelCurrentBuild.Text = "XXXX";
            // 
            // labelLatestBuild
            // 
            this.labelLatestBuild.AutoSize = true;
            this.labelLatestBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLatestBuild.Location = new System.Drawing.Point(128, 68);
            this.labelLatestBuild.Name = "labelLatestBuild";
            this.labelLatestBuild.Size = new System.Drawing.Size(39, 13);
            this.labelLatestBuild.TabIndex = 5;
            this.labelLatestBuild.Text = "XXXX";
            // 
            // labelLatestBuildTitle
            // 
            this.labelLatestBuildTitle.AutoSize = true;
            this.labelLatestBuildTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLatestBuildTitle.Location = new System.Drawing.Point(53, 68);
            this.labelLatestBuildTitle.Name = "labelLatestBuildTitle";
            this.labelLatestBuildTitle.Size = new System.Drawing.Size(64, 13);
            this.labelLatestBuildTitle.TabIndex = 4;
            this.labelLatestBuildTitle.Text = "Latest build:";
            // 
            // labelQuestion
            // 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.Location = new System.Drawing.Point(9, 102);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(227, 13);
            this.labelQuestion.TabIndex = 6;
            this.labelQuestion.Text = "Would you like to download the latest version?";
            // 
            // checkNeverAskAgain
            // 
            this.checkNeverAskAgain.AutoSize = true;
            this.checkNeverAskAgain.Location = new System.Drawing.Point(12, 122);
            this.checkNeverAskAgain.Name = "checkNeverAskAgain";
            this.checkNeverAskAgain.Size = new System.Drawing.Size(104, 17);
            this.checkNeverAskAgain.TabIndex = 7;
            this.checkNeverAskAgain.Text = "Never ask again";
            this.checkNeverAskAgain.UseVisualStyleBackColor = true;
            this.checkNeverAskAgain.CheckedChanged += new System.EventHandler(this.checkNeverAskAgain_CheckedChanged);
            // 
            // buttonYes
            // 
            this.buttonYes.Location = new System.Drawing.Point(122, 118);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(107, 23);
            this.buttonYes.TabIndex = 8;
            this.buttonYes.Text = "Yes";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.Location = new System.Drawing.Point(235, 118);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(107, 23);
            this.buttonNo.TabIndex = 9;
            this.buttonNo.Text = "No";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            // 
            // UpdateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 148);
            this.ControlBox = false;
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.checkNeverAskAgain);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.labelLatestBuild);
            this.Controls.Add(this.labelLatestBuildTitle);
            this.Controls.Add(this.labelCurrentBuild);
            this.Controls.Add(this.labelCurrentBuildTitle);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.pictureInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New version available";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UpdateWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureInfo;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelCurrentBuildTitle;
        private System.Windows.Forms.Label labelLatestBuildTitle;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.CheckBox checkNeverAskAgain;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
        public System.Windows.Forms.Label labelCurrentBuild;
        public System.Windows.Forms.Label labelLatestBuild;
    }
}