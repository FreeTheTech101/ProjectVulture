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
    public partial class GameRunning : DevComponents.DotNetBar.Metro.MetroForm
    {
        public GameRunning()
        {
            InitializeComponent();
        }

        private void OKBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OKBTN2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
