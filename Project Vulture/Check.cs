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

namespace Project_Vulture
{
    public partial class Check : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Check()
        {
            InitializeComponent();
        }

        private void Check_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://projectvulture.co.uk/program/check/checkforupdates");
            WebResponse response = request.GetResponse(); // getting server response

            System.IO.StreamReader reader = new //stuff
            System.IO.StreamReader(response.GetResponseStream()); //stuff

            check1.Text = reader.ReadToEnd(); //reading data

            if (check1.Text == "No Update")
            {
                MessageBox.Show("There Are No Updates Currently Available");
            }

            else
            {
                MessageBox.Show("There Is An Update Available!");
                Start startupd = new Start();
                startupd.ShowDialog();
            }
            
        }
    }
}
