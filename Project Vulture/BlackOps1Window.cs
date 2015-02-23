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
    public partial class BlackOps1Window : DevComponents.DotNetBar.Metro.MetroForm
    {
        public static Encoding Encoding = Encoding.UTF8;

        public BlackOps1Window()
        {
            InitializeComponent();
        }

        public class GameAddresses
        {
            public static int
            PlayerState1 = 0x1D28,
            PlayerState2 = 0x34C,
            mFlag = 0x01C0A74C,
            hFlag = 0x01A7987C,
            Money = 0x01C0A6C8,
            IngameName = 0x01C0A678,
            ClassPointer = 0x40;
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

        private void ReadClient0Name()
        {
            if (Process_Handle("BlackOps"))
            {
                Memorys mem = new Memorys("BlackOps");
                int address = (int)mem.baseaddress("BlackOps") + 0x01C0A678;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client0.Text = nickz.ToString();
            }
        }

        private void ReadClient1Name()
        {
            if (Process_Handle("BlackOps"))
            {
                Memorys mem = new Memorys("BlackOps");
                int address = (int)mem.baseaddress("BlackOps") + 0x01C0C3A0;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client1.Text = nickz.ToString();
            }
        }

        private void ReadClient2Name()
        {
            if (Process_Handle("BlackOps"))
            {
                Memorys mem = new Memorys("BlackOps");
                int address = (int)mem.baseaddress("BlackOps") + 0x01C0E0C8;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client2.Text = nickz.ToString();
            }
        }

        private void ReadClient3Name()
        {
            if (Process_Handle("BlackOps"))
            {
                Memorys mem = new Memorys("BlackOps");
                int address = (int)mem.baseaddress("BlackOps") + 0x01C0FDF0;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client3.Text = nickz.ToString();
            }
        }

        private void Health(int ClientID, string HealthPercentage)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.hFlag;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState2 * Client));
            Memorys mem = new Memorys("BlackOps");
            uint AmmoTest = mem.baseaddress("BlackOps") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(HealthPercentage));
        }

        private void mFlag(int ClientID, string mFlag)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.mFlag;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("BlackOps");
            uint AmmoTest = mem.baseaddress("BlackOps") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(mFlag));
        }

        private void InclipPrimary(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = 0x01C08F00;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (0x1D28 * Client));
            Memorys mem = new Memorys("BlackOps");
            uint AmmoTest = mem.baseaddress("BlackOps") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void InclipSecondary(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = 0x01C08F10;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (0x1D28 * Client));
            Memorys mem = new Memorys("BlackOps");
            uint AmmoTest = mem.baseaddress("BlackOps") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void InclipThird(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = 0x01C08F18;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (0x1D28 * Client));
            Memorys mem = new Memorys("BlackOps");
            uint AmmoTest = mem.baseaddress("BlackOps") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void Money(int ClientID, string MoneyValue)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Money;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("BlackOps");
            uint AmmoTest = mem.baseaddress("BlackOps") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(MoneyValue));
        }

        public void WriteName(int ClientID, string Name)
        {
            if (Process_Handle("BlackOps"))
            {
                int Client = ClientID;
                long Client0Offset = GameAddresses.IngameName;
                int AllClientBaby = Convert.ToInt32(Client0Offset + (0x1D28 * Client));
                byte[] nickbyte = Encoding.GetBytes(Name + "\0");
                Memorys mem = new Memorys("BlackOps");
                int address = (int)mem.baseaddress("BlackOps") + AllClientBaby;
                WriteBytes(address, nickbyte);
            }
        }

        public void PreGameName(string Name)
        {
            if (Process_Handle("BlackOps"))
            {
                byte[] nickbyte = Encoding.GetBytes(Name + "\0");
                Memorys mem = new Memorys("BlackOps");
                int address = (int)mem.baseaddress("BlackOps") + 0x0385892C;
                WriteBytes(address, nickbyte);
            }
        }

        private void BlackOps1Window_Load(object sender, EventArgs e)
        {

        }

        private void NameChangerBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(NameChangerID.Value);
            WriteName(ClientID, NameChangerTXT.Text);
            WriteName(ClientID, NameChangerTXT.Text);
        }

        private void SendMoneyBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(SendMoneyID.Value);
            Money(ClientID, MoneyTXT.Text);
        }

        private void SendmFlag_Click(object sender, EventArgs e)
        {
            if (mFlagOps.Text == "Noclip")
            {
                int ClientID = Convert.ToInt32(mFlagID.Value);
                mFlag(ClientID, "1");
            }

            if (mFlagOps.Text == "UFO")
            {
                int ClientID = Convert.ToInt32(mFlagID.Value);
                mFlag(ClientID, "2");
            }

            if (mFlagOps.Text == "Freeze")
            {
                int ClientID = Convert.ToInt32(mFlagID.Value);
                mFlag(ClientID, "4");
            }

            if (mFlagOps.Text == "Normal")
            {
                int ClientID = Convert.ToInt32(mFlagID.Value);
                mFlag(ClientID, "0");
            }
        }

    }
}
