﻿namespace Project_Vulture
{
    partial class MW3NonHostWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MW3NonHostWindow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelX103 = new DevComponents.DotNetBar.LabelX();
            this.RecoilSwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX100 = new DevComponents.DotNetBar.LabelX();
            this.ThermalSwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX99 = new DevComponents.DotNetBar.LabelX();
            this.RedBoxSwitch = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.MP = new System.Windows.Forms.Timer(this.components);
            this.InGame = new System.Windows.Forms.Timer(this.components);
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelX103);
            this.groupBox1.Controls.Add(this.RecoilSwitch);
            this.groupBox1.Controls.Add(this.labelX100);
            this.groupBox1.Controls.Add(this.ThermalSwitch);
            this.groupBox1.Controls.Add(this.labelX99);
            this.groupBox1.Controls.Add(this.RedBoxSwitch);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // labelX103
            // 
            this.labelX103.AutoSize = true;
            this.labelX103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX103.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX103.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX103.ForeColor = System.Drawing.Color.White;
            this.labelX103.Location = new System.Drawing.Point(6, 77);
            this.labelX103.Name = "labelX103";
            this.labelX103.Size = new System.Drawing.Size(54, 15);
            this.labelX103.TabIndex = 24;
            this.labelX103.Text = "Advanced:";
            // 
            // RecoilSwitch
            // 
            this.RecoilSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.RecoilSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RecoilSwitch.ForeColor = System.Drawing.Color.White;
            this.RecoilSwitch.Location = new System.Drawing.Point(76, 74);
            this.RecoilSwitch.Name = "RecoilSwitch";
            this.RecoilSwitch.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.RecoilSwitch.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.RecoilSwitch.Size = new System.Drawing.Size(67, 22);
            this.RecoilSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RecoilSwitch.TabIndex = 23;
            // 
            // labelX100
            // 
            this.labelX100.AutoSize = true;
            this.labelX100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX100.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX100.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX100.ForeColor = System.Drawing.Color.White;
            this.labelX100.Location = new System.Drawing.Point(6, 49);
            this.labelX100.Name = "labelX100";
            this.labelX100.Size = new System.Drawing.Size(46, 15);
            this.labelX100.TabIndex = 20;
            this.labelX100.Text = "Thermal:";
            // 
            // ThermalSwitch
            // 
            this.ThermalSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.ThermalSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ThermalSwitch.ForeColor = System.Drawing.Color.White;
            this.ThermalSwitch.Location = new System.Drawing.Point(76, 46);
            this.ThermalSwitch.Name = "ThermalSwitch";
            this.ThermalSwitch.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.ThermalSwitch.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.ThermalSwitch.Size = new System.Drawing.Size(67, 22);
            this.ThermalSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ThermalSwitch.TabIndex = 19;
            this.ThermalSwitch.Click += new System.EventHandler(this.ThermalSwitch_Click);
            // 
            // labelX99
            // 
            this.labelX99.AutoSize = true;
            this.labelX99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.labelX99.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX99.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX99.ForeColor = System.Drawing.Color.White;
            this.labelX99.Location = new System.Drawing.Point(6, 21);
            this.labelX99.Name = "labelX99";
            this.labelX99.Size = new System.Drawing.Size(59, 15);
            this.labelX99.TabIndex = 18;
            this.labelX99.Text = "Red Boxes:";
            // 
            // RedBoxSwitch
            // 
            this.RedBoxSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // 
            // 
            this.RedBoxSwitch.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RedBoxSwitch.ForeColor = System.Drawing.Color.White;
            this.RedBoxSwitch.Location = new System.Drawing.Point(76, 18);
            this.RedBoxSwitch.Name = "RedBoxSwitch";
            this.RedBoxSwitch.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.RedBoxSwitch.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(232)))));
            this.RedBoxSwitch.Size = new System.Drawing.Size(67, 22);
            this.RedBoxSwitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.RedBoxSwitch.TabIndex = 17;
            this.RedBoxSwitch.Click += new System.EventHandler(this.RedBoxSwitch_Click);
            // 
            // MP
            // 
            this.MP.Interval = 1;
            this.MP.Tick += new System.EventHandler(this.MP_Tick);
            // 
            // InGame
            // 
            this.InGame.Interval = 1;
            this.InGame.Tick += new System.EventHandler(this.InGame_Tick);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(38, -8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 25;
            this.labelX1.Text = "labelX1";
            // 
            // MW3NonHostWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 116);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MW3NonHostWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Vulture | Modern Warfare 3 Non Host Mods | Program Version Beta | Title U" +
    "pdate 1.9.461";
            this.Load += new System.EventHandler(this.MW3NonHostWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX103;
        private DevComponents.DotNetBar.Controls.SwitchButton RecoilSwitch;
        private DevComponents.DotNetBar.LabelX labelX100;
        private DevComponents.DotNetBar.Controls.SwitchButton ThermalSwitch;
        private DevComponents.DotNetBar.LabelX labelX99;
        private DevComponents.DotNetBar.Controls.SwitchButton RedBoxSwitch;
        private System.Windows.Forms.Timer MP;
        private System.Windows.Forms.Timer InGame;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}