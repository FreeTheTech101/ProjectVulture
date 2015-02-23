namespace Project_Vulture
{
    partial class GTA3Sp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GTA3Sp));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NameChangerTXT = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SendmFlag = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CommitSuicideBTN = new DevComponents.DotNetBar.ButtonX();
            this.TakeAmmo = new DevComponents.DotNetBar.ButtonX();
            this.GiveAmmo = new DevComponents.DotNetBar.ButtonX();
            this.TakeGod = new DevComponents.DotNetBar.ButtonX();
            this.GiveGod = new DevComponents.DotNetBar.ButtonX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SP = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox2.Controls.Add(this.NameChangerTXT);
            this.groupBox2.Controls.Add(this.SendmFlag);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(163, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 70);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Money";
            // 
            // NameChangerTXT
            // 
            this.NameChangerTXT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.NameChangerTXT.Border.Class = "TextBoxBorder";
            this.NameChangerTXT.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.NameChangerTXT.ForeColor = System.Drawing.Color.White;
            this.NameChangerTXT.Location = new System.Drawing.Point(6, 18);
            this.NameChangerTXT.MaxLength = 15;
            this.NameChangerTXT.Name = "NameChangerTXT";
            this.NameChangerTXT.Size = new System.Drawing.Size(170, 20);
            this.NameChangerTXT.TabIndex = 8;
            this.NameChangerTXT.WatermarkText = "Enter Money...";
            // 
            // SendmFlag
            // 
            this.SendmFlag.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.SendmFlag.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.SendmFlag.Location = new System.Drawing.Point(6, 44);
            this.SendmFlag.Name = "SendmFlag";
            this.SendmFlag.Size = new System.Drawing.Size(170, 20);
            this.SendmFlag.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.SendmFlag.TabIndex = 3;
            this.SendmFlag.Text = "Send Money";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Controls.Add(this.CommitSuicideBTN);
            this.groupBox1.Controls.Add(this.TakeAmmo);
            this.groupBox1.Controls.Add(this.GiveAmmo);
            this.groupBox1.Controls.Add(this.TakeGod);
            this.groupBox1.Controls.Add(this.GiveGod);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 148);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main Mods";
            // 
            // CommitSuicideBTN
            // 
            this.CommitSuicideBTN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.CommitSuicideBTN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.CommitSuicideBTN.Location = new System.Drawing.Point(6, 122);
            this.CommitSuicideBTN.Name = "CommitSuicideBTN";
            this.CommitSuicideBTN.Size = new System.Drawing.Size(139, 20);
            this.CommitSuicideBTN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CommitSuicideBTN.TabIndex = 8;
            this.CommitSuicideBTN.Text = "Commit Suicide";
            this.CommitSuicideBTN.Click += new System.EventHandler(this.CommitSuicideBTN_Click);
            // 
            // TakeAmmo
            // 
            this.TakeAmmo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.TakeAmmo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.TakeAmmo.Location = new System.Drawing.Point(6, 96);
            this.TakeAmmo.Name = "TakeAmmo";
            this.TakeAmmo.Size = new System.Drawing.Size(139, 20);
            this.TakeAmmo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.TakeAmmo.TabIndex = 6;
            this.TakeAmmo.Text = "Take Max Ammo";
            // 
            // GiveAmmo
            // 
            this.GiveAmmo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.GiveAmmo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.GiveAmmo.Location = new System.Drawing.Point(6, 70);
            this.GiveAmmo.Name = "GiveAmmo";
            this.GiveAmmo.Size = new System.Drawing.Size(139, 20);
            this.GiveAmmo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.GiveAmmo.TabIndex = 4;
            this.GiveAmmo.Text = "Give Max Ammo";
            // 
            // TakeGod
            // 
            this.TakeGod.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.TakeGod.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.TakeGod.Location = new System.Drawing.Point(6, 44);
            this.TakeGod.Name = "TakeGod";
            this.TakeGod.Size = new System.Drawing.Size(139, 20);
            this.TakeGod.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.TakeGod.TabIndex = 2;
            this.TakeGod.Text = "Take God Mode";
            // 
            // GiveGod
            // 
            this.GiveGod.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.GiveGod.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.GiveGod.Location = new System.Drawing.Point(6, 18);
            this.GiveGod.Name = "GiveGod";
            this.GiveGod.Size = new System.Drawing.Size(139, 20);
            this.GiveGod.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.GiveGod.TabIndex = 0;
            this.GiveGod.Text = "Give God Mode";
            this.GiveGod.Click += new System.EventHandler(this.GiveGod_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox3.Controls.Add(this.comboBoxEx1);
            this.groupBox3.Controls.Add(this.buttonX1);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(163, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 70);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "mFlags";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.ForeColor = System.Drawing.Color.White;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 14;
            this.comboBoxEx1.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.comboBoxEx1.Location = new System.Drawing.Point(6, 18);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(131, 20);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 4;
            this.comboBoxEx1.WatermarkText = "Select mFlag...";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "UFO";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Noclip";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "Freeze";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "Normal";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(6, 44);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(170, 20);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 3;
            this.buttonX1.Text = "Send mFlag";
            // 
            // SP
            // 
            this.SP.Interval = 1;
            this.SP.Tick += new System.EventHandler(this.SP_Tick);
            // 
            // GTA3Sp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 155);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GTA3Sp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Vulture | Grand Theft Auto 3 Single Player | Program Version Beta | Title" +
    " Update 1.1";
            this.Load += new System.EventHandler(this.GTA3Sp_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX SendmFlag;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX CommitSuicideBTN;
        private DevComponents.DotNetBar.ButtonX TakeAmmo;
        private DevComponents.DotNetBar.ButtonX GiveAmmo;
        private DevComponents.DotNetBar.ButtonX TakeGod;
        private DevComponents.DotNetBar.ButtonX GiveGod;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX NameChangerTXT;
        private System.Windows.Forms.Timer SP;
    }
}