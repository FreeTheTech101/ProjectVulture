using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Vulture
{
    public partial class Success : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Success()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            Select choose = new Select();
            choose.Show();
            Close();
        }

    }
}
