namespace Project_Vulture
{
    partial class Start
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
            this.check1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // check1
            // 
            this.check1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.check1.Border.Class = "TextBoxBorder";
            this.check1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.check1.ForeColor = System.Drawing.Color.White;
            this.check1.Location = new System.Drawing.Point(12, 12);
            this.check1.Name = "check1";
            this.check1.Size = new System.Drawing.Size(478, 20);
            this.check1.TabIndex = 5;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 38);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(478, 276);
            this.webBrowser1.TabIndex = 7;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 329);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.check1);
            this.DoubleBuffered = true;
            this.Name = "Start";
            this.Text = "Start";
            this.Load += new System.EventHandler(this.Start_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX check1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}