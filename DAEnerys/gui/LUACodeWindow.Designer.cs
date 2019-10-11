namespace DAEnerys
{
    partial class LUACodeWindow
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
            this.boxCode = new ScintillaNET.Scintilla();
            this.SuspendLayout();
            // 
            // boxCode
            // 
            this.boxCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxCode.Location = new System.Drawing.Point(0, 0);
            this.boxCode.Name = "boxCode";
            this.boxCode.Size = new System.Drawing.Size(800, 450);
            this.boxCode.TabIndex = 0;
            this.boxCode.Text = "scintilla1";
            // 
            // LUACodeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.boxCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LUACodeWindow";
            this.Text = "LUA code";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla boxCode;
    }
}