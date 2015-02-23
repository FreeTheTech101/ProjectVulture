namespace Project_Vulture
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
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.info = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.loginusername = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.loginpassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.LoginBTN = new DevComponents.DotNetBar.ButtonX();
            this.GetMyHWID = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))), System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232))))));
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(12, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 15);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "Username:";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.White;
            this.labelX2.Location = new System.Drawing.Point(12, 52);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(54, 15);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Password:";
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.info.Border.Class = "TextBoxBorder";
            this.info.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.info.ForeColor = System.Drawing.Color.White;
            this.info.Location = new System.Drawing.Point(12, 251);
            this.info.Name = "info";
            this.info.ReadOnly = true;
            this.info.Size = new System.Drawing.Size(260, 20);
            this.info.TabIndex = 5;
            this.info.Visible = false;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(129, 235);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(0, 0);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 11;
            this.buttonX1.Text = "buttonX1";
            // 
            // loginusername
            // 
            this.loginusername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.loginusername.Border.Class = "TextBoxBorder";
            this.loginusername.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.loginusername.ForeColor = System.Drawing.Color.White;
            this.loginusername.Location = new System.Drawing.Point(12, 26);
            this.loginusername.Name = "loginusername";
            this.loginusername.Size = new System.Drawing.Size(260, 20);
            this.loginusername.TabIndex = 12;
            this.loginusername.WatermarkText = "Enter Username Here...";
            // 
            // loginpassword
            // 
            this.loginpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.loginpassword.Border.Class = "TextBoxBorder";
            this.loginpassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.loginpassword.ForeColor = System.Drawing.Color.White;
            this.loginpassword.Location = new System.Drawing.Point(12, 73);
            this.loginpassword.Name = "loginpassword";
            this.loginpassword.Size = new System.Drawing.Size(260, 20);
            this.loginpassword.TabIndex = 13;
            this.loginpassword.UseSystemPasswordChar = true;
            this.loginpassword.WatermarkText = "Enter Password Here...";
            // 
            // LoginBTN
            // 
            this.LoginBTN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.LoginBTN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.LoginBTN.Location = new System.Drawing.Point(12, 99);
            this.LoginBTN.Name = "LoginBTN";
            this.LoginBTN.Size = new System.Drawing.Size(127, 33);
            this.LoginBTN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.LoginBTN.Symbol = "";
            this.LoginBTN.TabIndex = 14;
            this.LoginBTN.Text = "Login";
            this.LoginBTN.Click += new System.EventHandler(this.LoginBTN_Click);
            // 
            // GetMyHWID
            // 
            this.GetMyHWID.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.GetMyHWID.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.GetMyHWID.Location = new System.Drawing.Point(145, 99);
            this.GetMyHWID.Name = "GetMyHWID";
            this.GetMyHWID.Size = new System.Drawing.Size(127, 33);
            this.GetMyHWID.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.GetMyHWID.Symbol = "";
            this.GetMyHWID.TabIndex = 15;
            this.GetMyHWID.Text = "Get My HWID";
            this.GetMyHWID.Click += new System.EventHandler(this.GetMyHWID_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.GetMyHWID);
            this.Controls.Add(this.LoginBTN);
            this.Controls.Add(this.loginpassword);
            this.Controls.Add(this.loginusername);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.info);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Vulture | Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX info;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX loginusername;
        private DevComponents.DotNetBar.Controls.TextBoxX loginpassword;
        private DevComponents.DotNetBar.ButtonX LoginBTN;
        private DevComponents.DotNetBar.ButtonX GetMyHWID;
    }
}

