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

namespace Project_Vulture
{
    public partial class UpdateAvailable : DevComponents.DotNetBar.Metro.MetroForm
    {
        public UpdateAvailable()
        {
            InitializeComponent();
        }

        private void DownloadBTN_Click(object sender, EventArgs e)
        {
            WebClient ck = new WebClient();
            var CK1 = ck.DownloadString("https://projectvulture.co.uk/updates/dl-link");
            Process.Start(CK1);
            Close();
        }

    }
}
