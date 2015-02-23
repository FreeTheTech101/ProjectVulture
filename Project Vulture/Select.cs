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
    public partial class Select : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Select()
        {
            InitializeComponent();
        }

        private void MW2_Click(object sender, EventArgs e)
        {
            MW2Window MW2 = new MW2Window();
            MW2.ShowDialog();
        }

        private void MW3_Click(object sender, EventArgs e)
        {
            Mw3Window MW3 = new Mw3Window();
            MW3.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            News shownews = new News();
            shownews.ShowDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Updates showupdates = new Updates();
            showupdates.ShowDialog();
        }

        private void Select_Load(object sender, EventArgs e)
        {

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.projectvulture.co.uk/forum/index.php?forums/software-versions-and-updates.8/");
        }

        private void BlackOps1_Click(object sender, EventArgs e)
        {
            BlackOps1Window showupdates = new BlackOps1Window();
            showupdates.ShowDialog();
        }
    }
}
