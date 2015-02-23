namespace Project_Vulture
{
    partial class Check
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
            this.start1 = new DevComponents.DotNetBar.Controls.TextBoxX();
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
            this.check1.TabIndex = 3;
            // 
            // start1
            // 
            this.start1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.start1.Border.Class = "TextBoxBorder";
            this.start1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.start1.ForeColor = System.Drawing.Color.White;
            this.start1.Location = new System.Drawing.Point(12, 38);
            this.start1.Name = "start1";
            this.start1.Size = new System.Drawing.Size(478, 20);
            this.start1.TabIndex = 4;
            // 
            // Check
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 0);
            this.Controls.Add(this.start1);
            this.Controls.Add(this.check1);
            this.DoubleBuffered = true;
            this.Name = "Check";
            this.Text = "Check";
            this.Load += new System.EventHandler(this.Check_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX check1;
        private DevComponents.DotNetBar.Controls.TextBoxX start1;
    }
}