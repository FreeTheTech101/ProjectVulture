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
    public partial class News : DevComponents.DotNetBar.Metro.MetroForm
    {
        public News()
        {
            InitializeComponent();
        }

        private void News_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://projectvulture.co.uk/program/updates/News"); //reading update log.
            WebResponse response = request.GetResponse(); // getting server response

            System.IO.StreamReader reader = new //stuff
            System.IO.StreamReader(response.GetResponseStream()); //stuff

            richTextBox1.Text = reader.ReadToEnd(); //reading data
        }
    }
}
