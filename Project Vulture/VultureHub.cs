using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
//using DevComponents

namespace Project_Vulture
{
    public partial class VultureHub : DevComponents.DotNetBar.Metro.MetroForm
    {
        public VultureHub()
        {
            InitializeComponent();
        }

        private void CheckUpdates_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            var UI = wc.DownloadString("https://projectvulture.co.uk/updates/check");
            if (UI.Contains("Version1"))
            {
                UpdateAvailable Update = new UpdateAvailable();
                Update.ShowDialog();
            }
            else
            {
                NoUpdate NoUpdate = new NoUpdate();
                NoUpdate.ShowDialog();
            }
        }

        private void Information_Click(object sender, EventArgs e)
        {
            Process.Start("https://projectvulture.co.uk/forum/index.php?forums/project-vulture-information.12/");
        }

        private void ReportBug_Click(object sender, EventArgs e)
        {
            Process.Start("https://projectvulture.co.uk/forum/index.php?forums/report-project-vulture-bugs-here.11/");
        }

        private void Help_Click(object sender, EventArgs e)
        {
            Process.Start("https://projectvulture.co.uk/forum/index.php?forums/help.14/");
        }

        private void MW2Host_Click(object sender, EventArgs e)
        {
            MW2HostWindow MW2Host = new MW2HostWindow();
            MW2Host.ShowDialog();
        }

        private void MW2NonHost_Click(object sender, EventArgs e)
        {
            MW2NonHostWindow MW2NonHost = new MW2NonHostWindow();
            MW2NonHost.ShowDialog();
        }

        private void MW2Profile_Click(object sender, EventArgs e)
        {
            MW2ProfileWindow MW2Profile = new MW2ProfileWindow();
            MW2Profile.ShowDialog();
        }

        private void MW3Host_Click(object sender, EventArgs e)
        {
            MW3HostWindow MW3Host = new MW3HostWindow();
            MW3Host.ShowDialog();
        }

        private void MW3NonHost_Click(object sender, EventArgs e)
        {
            MW3NonHostWindow MW3NonHost = new MW3NonHostWindow();
            MW3NonHost.ShowDialog();
        }
    }
}
