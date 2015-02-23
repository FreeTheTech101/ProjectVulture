namespace Project_Vulture
{
    partial class GameRunning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameRunning));
            this.OKBTN = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.OKBTN2 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // OKBTN
            // 
            this.OKBTN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OKBTN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.OKBTN.Location = new System.Drawing.Point(13, 58);
            this.OKBTN.Name = "OKBTN";
            this.OKBTN.Size = new System.Drawing.Size(0, 0);
            this.OKBTN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OKBTN.TabIndex = 25;
            this.OKBTN.Text = "OK";
            this.OKBTN.Click += new System.EventHandler(this.OKBTN_Click);
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.ForeColor = System.Drawing.Color.White;
            this.labelX4.Location = new System.Drawing.Point(12, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(27, 36);
            this.labelX4.Symbol = "";
            this.labelX4.SymbolColor = System.Drawing.Color.Red;
            this.labelX4.TabIndex = 24;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.White;
            this.labelX3.Location = new System.Drawing.Point(131, 47);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(0, 0);
            this.labelX3.Symbol = "";
            this.labelX3.TabIndex = 23;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(15, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(248, 36);
            this.labelX1.TabIndex = 22;
            this.labelX1.Text = "Please Make Sure The Game Is Running \r\nBefore Launching The Program.";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // OKBTN2
            // 
            this.OKBTN2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.OKBTN2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.OKBTN2.Location = new System.Drawing.Point(185, 66);
            this.OKBTN2.Name = "OKBTN2";
            this.OKBTN2.Size = new System.Drawing.Size(75, 20);
            this.OKBTN2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.OKBTN2.TabIndex = 26;
            this.OKBTN2.Text = "OK";
            this.OKBTN2.Click += new System.EventHandler(this.OKBTN2_Click);
            // 
            // GameRunning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 98);
            this.Controls.Add(this.OKBTN2);
            this.Controls.Add(this.OKBTN);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameRunning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Vulture | Game Is Not Running";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX OKBTN;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX OKBTN2;
    }
}