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
    public partial class Start : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://projectvulture.co.uk/program/check/doupdate");
            WebResponse response = request.GetResponse(); // getting server response

            System.IO.StreamReader reader = new //stuff
            System.IO.StreamReader(response.GetResponseStream()); //stuff

            check1.Text = reader.ReadToEnd(); //reading data

            webBrowser1.Navigate(check1.Text);
        }
    }
}
