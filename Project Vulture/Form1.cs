using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.Cache;
using System.Net;
using System.IO;
using System.Management;
using System.Security.Cryptography;

namespace Project_Vulture
{
    public partial class Form1 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public static Encoding Encoding = Encoding.UTF8;

        public Form1()
        {
            InitializeComponent();
        }

        #region Strings
        private string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        #endregion


        private void LoginBTN_Click(object sender, EventArgs e)
        {
            Success choose1 = new Success();
            choose1.Show();
        }

        private void GetMyHWID_Click(object sender, EventArgs e)
        {
            HWID hardware1 = new HWID();
            hardware1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("Reflector");
            if (pname.Length == 1)
            {
                MessageBox.Show("An Error Has Occured");
                Close();
            }

            Process[] pname1 = Process.GetProcessesByName("DotNetResolver");
            if (pname1.Length == 1)
            {
                MessageBox.Show("An Error Has Occured");
                Close();
            }

            Process[] pname2 = Process.GetProcessesByName("OLLYDBG");
            if (pname2.Length == 1)
            {
                MessageBox.Show("An Error Has Occured");
                Close();
            }

        }



    }
}
