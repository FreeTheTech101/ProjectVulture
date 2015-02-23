using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using memory_control;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;

namespace Project_Vulture
{
    public partial class GTA3Sp : DevComponents.DotNetBar.Metro.MetroForm
    {
        public GTA3Sp()
        {
            InitializeComponent();
        }

        #region Basic Stuff
        [DllImport("kernel32.dll")]
        private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        private static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
        IntPtr pHandel;
        public bool Process_Handle(string ProcessName)
        {
            try
            {
                Process[] ProcList = Process.GetProcessesByName(ProcessName);
                if (ProcList.Length == 0)
                    return false;
                else
                {
                    pHandel = ProcList[0].Handle;
                    return true;
                }
            }
            catch (Exception ex)
            { Console.Beep(); Console.WriteLine("Process_Handle - " + ex.Message); return false; }
        }

        private byte[] Read(int Address, int Length)
        {
            byte[] Buffer = new byte[Length];
            IntPtr Zero = IntPtr.Zero;
            ReadProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
            return Buffer;
        }

        private void Write(int Address, int Value)
        {
            byte[] Buffer = BitConverter.GetBytes(Value);
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
        }
        #endregion

        #region Write Functions (Integer & String)
        public void WriteInteger(int Address, int Value)
        {
            Write(Address, Value);
        }

        public void WriteString(int Address, string Text)
        {
            byte[] Buffer = new ASCIIEncoding().GetBytes(Text);
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
        }

        public void Float(Int64 Address, Single Float)
        {
            IntPtr retByt;
            WriteProcessMemory(pHandel, new IntPtr(Address), BitConverter.GetBytes(Float), sizeof(Single), out retByt);
        }

        public void WriteBytes(int Address, byte[] Bytes)
        {
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Bytes, (uint)Bytes.Length, out Zero);
        }

        public void WriteNOP(int Address)
        {
            byte[] Buffer = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
            IntPtr Zero = IntPtr.Zero;
            WriteProcessMemory(pHandel, (IntPtr)Address, Buffer, (UInt32)Buffer.Length, out Zero);
        }
        #endregion

        #region Read Functions (Integer & String)
        public int ReadInteger(int Address, int Length = 4)
        {
            return BitConverter.ToInt32(Read(Address, Length), 0);
        }
        public string ReadString(int Address, int Length = 4)
        {
            return new ASCIIEncoding().GetString(Read(Address, Length));
        }
        public byte[] ReadBytes(int Address, int Length)
        {
            return Read(Address, Length);
        }
        #endregion

        public class GameAddresses
        {
            public static int
            HealthPointer = 0x7e0,
            HealthPointer1 = 0x1f0,
            HealthPointer2 = 0x4f4,
            HealthPointer3 = 0x1f0,
            HealthPointer4 = 0x2c0,
            Health = 0x0030B308;
        }

        private void SP_Tick(object sender, EventArgs e)
        {
            if (Process_Handle("gta3"))
            {
            }
            else
            {
                SP.Stop();
                GameRunning GameRunningCheck = new GameRunning();
                GameRunningCheck.ShowDialog();
                Close();
            }
        }

        private void GTA3Sp_Load(object sender, EventArgs e)
        {
            SP.Start();
        }

        private void Health(int ClientID, float HealthValue)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Health;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.HealthPointer + GameAddresses.HealthPointer1 + GameAddresses.HealthPointer2 + GameAddresses.HealthPointer3 + GameAddresses.HealthPointer4 * Client));
            Float(AllClientBaby, HealthValue);
        }

        private void GiveGod_Click(object sender, EventArgs e)
        {
            Health(1, 1000);
        }

        private void CommitSuicideBTN_Click(object sender, EventArgs e)
        {
            Health(1, 0);
        }

    }
}
