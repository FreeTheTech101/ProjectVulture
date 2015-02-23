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

namespace Project_Vulture
{
    public partial class Mw3 : DevComponents.DotNetBar.Metro.MetroForm
    {
        public static Encoding Encoding = Encoding.UTF8;

        public Mw3()
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

        public void WriteFloat(int Address, float Value)
        {
            float myFloat = Value;
            int myInt = Convert.ToInt32(myFloat);
            Write(Address, myInt);
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
            PlayerState1 = 0x38EC,
            PlayerState2 = 0x000,
            ClassPointer = 0x62,
            NameOffset = 0x01D0BCD0,
            VisionsOffset = 0x1D088D0,
            mFlagOffset = 0x01D0BE8C,
            HealthOffset = 0x01B42444,
            CustomClass = 0x01DC0878,
            PrimaryWeapon = 0x01D08B3C,
            PrimaryWeaponAmmo = 0x01D08CE4,
            SecondaryWeapon = 0x01D08B34,
            SecondaryWeaponAmmo = 0x01D08CCC,
            SwitchWeapon = 0x01D08C30,
            XP = 0x01DC02B8,
            Score = 0x01DC04D0,
            Wins = 0x01DC052C,
            Losses = 0x01DC0530,
            Ties = 0x01D,
            WinStreak = 0x01DC0538,
            Kills = 0x01DC04F8,
            Deaths = 0x01DC0500,
            Headshots = 0x01DC050C,
            Assists = 0x01DC0508,
            Killstreak = 0x01DC04FC,
            Tokens = 0x01DC2327,
            TimePlayed = 0x01DC0518,
            Client0Offset = 0x01D0BCD0,
            Client1Offset = 0x01D0F5BC,
            Client2Offset = 0x01D12EA8,
            Client3Offset = 0x01D16794,
            Client4Offset = 0x01D1A080,
            Client5Offset = 0x01D1D96C,
            Client6Offset = 0x01D21258,
            Client7Offset = 0x01D24B44,
            Client8Offset = 0x01D28430,
            Client9Offset = 0x01D2BD1C,
            Client10Offset = 0x01D2F608,
            Client11Offset = 0x01D32EF4,
            Client12Offset = 0x01D367E0,
            Client13Offset = 0x01D3A0CC,
            Client14Offset = 0x01D3D9B8,
            Client15Offset = 0x01D412A4,
            Client16Offset = 0x01D44B90,
            Client17Offset = 0x01D4847C,
            alignOffset = 0x30;
        }

        private void ReadClient0()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client0Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client0.Text = nickz.ToString();
            }
        }

        private void ReadClient1()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client1Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client1.Text = nickz.ToString();
            }
        }

        private void ReadClient2()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client2Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client2.Text = nickz.ToString();
            }
        }

        private void ReadClient3()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client3Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client3.Text = nickz.ToString();
            }
        }

        private void ReadClient4()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client4Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client4.Text = nickz.ToString();
            }
        }

        private void ReadClient5()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client5Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client5.Text = nickz.ToString();
            }
        }

        private void ReadClient6()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client6Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client6.Text = nickz.ToString();
            }
        }

        private void ReadClient7()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client7Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client7.Text = nickz.ToString();
            }
        }

        private void ReadClient8()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client8Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client8.Text = nickz.ToString();
            }
        }

        private void ReadClient9()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client9Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client9.Text = nickz.ToString();
            }
        }

        private void ReadClient10()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client10Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client10.Text = nickz.ToString();
            }
        }

        private void ReadClient11()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client11Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client11.Text = nickz.ToString();
            }
        }

        private void ReadClient12()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client12Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client12.Text = nickz.ToString();
            }
        }

        private void ReadClient13()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client13Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client13.Text = nickz.ToString();
            }
        }

        private void ReadClient14()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client14Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client14.Text = nickz.ToString();
            }
        }

        private void ReadClient15()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client15Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client15.Text = nickz.ToString();
            }
        }

        private void ReadClient16()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client16Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client16.Text = nickz.ToString();
            }
        }

        private void ReadClient17()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.Client17Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client17.Text = nickz.ToString();
            }
        }

    }
}
