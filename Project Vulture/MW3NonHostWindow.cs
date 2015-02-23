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
    public partial class MW3NonHostWindow : DevComponents.DotNetBar.Metro.MetroForm
    {
        public MW3NonHostWindow()
        {
            InitializeComponent();
        }

        public class GameAddresses
        {
            public static int
            RedBoxOFF = 0x009DC3B0,
            RedBoxOFF1 = 0x009DF71C,
            RedBoxOFF2 = 0x00A13658,
            RedBoxOFF3 = 0x00AE0CF4,
            RedBoxOFF4 = 0x0113DBA8,
            RedBoxOFF5 = 0x0114AF78,
            RedBoxOFF6 = 0x0114E2B4,
            RedBoxOFF7 = 0x011515F0,
            RedBoxOFF8 = 0x0115492C,
            RedBoxOFF9 = 0x01157C68,
            RedBoxOFF10 = 0x0115AFA4,
            RedBoxOFF11 = 0x0116161C,
            RedBoxOFF12 = 0x01164958,
            RedBoxOFF13 = 0x01167C94,
            RedBoxOFF14 = 0x0116AFD0,
            RedBoxOFF15 = 0x0116E30C,
            RedBoxOFF16 = 0x01171648,
            RedBoxOFF17 = 0x01174984,
            RedBoxOFF18 = 0x01177CC0,
            RedBoxOFF19 = 0x0117AFFC,
            RedBoxOFF20 = 0x0115E2E0,
            InGame = 0x00AE0194;
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

        private void MW3NonHostWindow_Load(object sender, EventArgs e)
        {
            MP.Start();
        }

        private void MP_Tick(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp"))
            {
                InGame.Start();
            }
            else
            {
                MP.Stop();
                InGame.Stop();
                GameRunning GameRunningCheck = new GameRunning();
                GameRunningCheck.ShowDialog();
                Close();
            }
        }

        private void InGame_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.InGame);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            labelX1.Text = address0.ToString();

            if (labelX1.Text == "0")
            {
                RedBoxSwitch.Value = false;
                ThermalSwitch.Value = false;
                RecoilSwitch.Value = false;
                RedBoxSwitch.Enabled = true;
                ThermalSwitch.Enabled = true;
            }
        }

        private void RedBoxOFFHost0(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost1(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF1);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost2(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF2);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost3(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF3);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost4(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF4);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost5(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF5);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost6(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF6);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost7(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF7);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost8(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF8);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost9(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF9);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost10(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF10);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost11(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF11);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost12(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF12);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost13(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF13);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost14(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF14);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost15(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF15);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost16(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF16);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost17(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF17);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost18(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF18);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost19(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF19);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost20(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF20);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxSwitch_Click(object sender, EventArgs e)
        {
            if (RedBoxSwitch.Value == true)
            {
                RedBoxOFFHost0("4096");
                RedBoxOFFHost1("4096");
                RedBoxOFFHost2("4096");
                RedBoxOFFHost3("4096");
                RedBoxOFFHost4("4096");
                RedBoxOFFHost5("4096");
                RedBoxOFFHost6("4096");
                RedBoxOFFHost7("4096");
                RedBoxOFFHost8("4096");
                RedBoxOFFHost9("4096");
                RedBoxOFFHost10("4096");
                RedBoxOFFHost11("4096");
                RedBoxOFFHost12("4096");
                RedBoxOFFHost13("4096");
                RedBoxOFFHost14("4096");
                RedBoxOFFHost15("4096");
                RedBoxOFFHost16("4096");
                RedBoxOFFHost17("4096");
                RedBoxOFFHost18("4096");
                RedBoxOFFHost19("4096");
                RedBoxOFFHost20("4096");
                ThermalSwitch.Enabled = true;
            }

            if (RedBoxSwitch.Value == false)
            {
                RedBoxOFFHost0("70000");
                RedBoxOFFHost1("70000");
                RedBoxOFFHost2("70000");
                RedBoxOFFHost3("70000");
                RedBoxOFFHost4("70000");
                RedBoxOFFHost5("70000");
                RedBoxOFFHost6("70000");
                RedBoxOFFHost7("70000");
                RedBoxOFFHost8("70000");
                RedBoxOFFHost9("70000");
                RedBoxOFFHost10("70000");
                RedBoxOFFHost11("70000");
                RedBoxOFFHost12("70000");
                RedBoxOFFHost13("70000");
                RedBoxOFFHost14("70000");
                RedBoxOFFHost15("70000");
                RedBoxOFFHost16("70000");
                RedBoxOFFHost17("70000");
                RedBoxOFFHost18("70000");
                RedBoxOFFHost19("70000");
                RedBoxOFFHost20("70000");
                ThermalSwitch.Enabled = false;
            }
        }

        private void ThermalSwitch_Click(object sender, EventArgs e)
        {
            if (ThermalSwitch.Value == true)
            {
                RedBoxOFFHost0("4096");
                RedBoxOFFHost1("4096");
                RedBoxOFFHost2("4096");
                RedBoxOFFHost3("4096");
                RedBoxOFFHost4("4096");
                RedBoxOFFHost5("4096");
                RedBoxOFFHost6("4096");
                RedBoxOFFHost7("4096");
                RedBoxOFFHost8("4096");
                RedBoxOFFHost9("4096");
                RedBoxOFFHost10("4096");
                RedBoxOFFHost11("4096");
                RedBoxOFFHost12("4096");
                RedBoxOFFHost13("4096");
                RedBoxOFFHost14("4096");
                RedBoxOFFHost15("4096");
                RedBoxOFFHost16("4096");
                RedBoxOFFHost17("4096");
                RedBoxOFFHost18("4096");
                RedBoxOFFHost19("4096");
                RedBoxOFFHost20("4096");
                RedBoxSwitch.Enabled = true;
            }

            if (ThermalSwitch.Value == false)
            {
                RedBoxOFFHost0("300");
                RedBoxOFFHost1("300");
                RedBoxOFFHost2("300");
                RedBoxOFFHost3("300");
                RedBoxOFFHost4("300");
                RedBoxOFFHost5("300");
                RedBoxOFFHost6("300");
                RedBoxOFFHost7("300");
                RedBoxOFFHost8("300");
                RedBoxOFFHost9("300");
                RedBoxOFFHost10("300");
                RedBoxOFFHost11("300");
                RedBoxOFFHost12("300");
                RedBoxOFFHost13("300");
                RedBoxOFFHost14("300");
                RedBoxOFFHost15("300");
                RedBoxOFFHost16("300");
                RedBoxOFFHost17("300");
                RedBoxOFFHost18("300");
                RedBoxOFFHost19("300");
                RedBoxOFFHost20("300");
                RedBoxSwitch.Enabled = false;
            }
        }
    }
}
