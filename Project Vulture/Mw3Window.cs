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
    public partial class Mw3Window : DevComponents.DotNetBar.Metro.MetroForm
    {
        public static Encoding Encoding = Encoding.UTF8;

        public Mw3Window()
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

        private void Mw3Window_Load(object sender, EventArgs e)
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
            
            MP.Start();
            MapName.Start();
            RedBoxCheck.Start();
            WebRequest request = WebRequest.Create("http://projectvulture.co.uk/program/updates/MW3"); //reading update log.
            WebResponse response = request.GetResponse(); // getting server response

            System.IO.StreamReader reader = new //stuff
            System.IO.StreamReader(response.GetResponseStream()); //stuff

            richTextBox1.Text = reader.ReadToEnd(); //reading data
        }

        public class GameAddresses
        {
            public static int
            PlayerState1 = 0x38EC,
            PlayerState2 = 0x000,
            ClassPointer = 0x62,
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
            RedBoxCheck = 0x017C0A24,
            PrimaryInGunAmmo = 0x01D08CE8,
            PrimaryLeftAmmo = 0x01D08C68,
            Flashbangs = 0x01D08CF4,
            Flashbangsv2 = 0x01D08D00,
            Frags = 0x01D08CDC,
            SecondaryInGunAmmo = 0x01D08CD0,
            SecondaryLeftAmmo = 0x01D08C58,
            SecondaryAkimboInGun = 0x01D08CD4,
            GrenadeLauncher = 0x01D08CF4,
            NameOffset = 0x01D0BCD0,
            VisionsOffset = 0x1D088D0,
            mFlagOffset = 0x01D0BE8C,
            HealthOffset = 0x01B42444,
            CustomClass = 0x01DC0878,
            MapName = 0x059CA9B0,
            PrimaryWeapon = 0x01D08B3C,
            PrimaryWeaponAmmo = 0x01D08CE4,
            PrimaryWeaponAmmoLeft = 0x01D08C64,
            SecondaryWeapon = 0x01D08B34,
            SecondaryWeaponAmmo = 0x01D08CCC,
            SecondaryWeaponAmmoLeft = 0x01D08C54,
            SwitchWeapon = 0x01D08C30,
            XAxis = 0x01D088E0,
            YAxis = 0x01D088E4,
            ZAxis = 0x01D088DC,
            Prestige = 0x01DC04C8,
            XP = 0x01DC02B8,
            Score = 0x01DC04D0,
            Wins = 0x01DC052C,
            Losses = 0x01DC0530,
            Ties = 0x01DC0534,
            WinStreak = 0x01DC0538,
            Kills = 0x01DC04F8,
            Deaths = 0x01DC0500,
            Headshots = 0x01DC050C,
            Assists = 0x01DC0508,
            Killstreak = 0x01DC04FC,
            Tokens = 0x01DC2327,
            TimePlayed = 0x01DC0518,
            DoubleXP = 0x01DC237C,
            DoubleWeaponXP = 0x01DC2384,
            Client0Button = 0x01D0BC00,
            Client1Button = 0x01D0F4EC,
            Client2Button = 0x01D12DD8,
            Client3Button = 0x01D166C4,
            Client4Button = 0x01D19FB0,
            Client5Button = 0x01D1D89C,
            Client6Button = 0x01D21188,
            Client7Button = 0x01D24A74,
            Client8Button = 0x01D28360,
            Client9Button = 0x01D2BC4C,
            Client10Button = 0x01D2F538,
            Client11Button = 0x01D32E24,
            Client12Button = 0x01D36710,
            Client13Button = 0x01D39FFC,
            Client14Button = 0x01D3D8E8,
            Client15Button = 0x01D411D4,
            Client16Button = 0x01D44AC0,
            Client17Button = 0x01D483AC,
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
            Client17Offset = 0x01D4847C;
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

        private void Teleport(int ClientID, float XAxis, float YAxis, float ZAxis)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.XAxis;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            long Client0Offset1 = GameAddresses.YAxis;
            uint AllClientBaby1 = Convert.ToUInt32(Client0Offset1 + (GameAddresses.PlayerState1 * Client));
            long Client0Offset2 = GameAddresses.ZAxis;
            uint AllClientBaby2 = Convert.ToUInt32(Client0Offset2 + (GameAddresses.PlayerState1 * Client));
            Float(AllClientBaby, XAxis);
            Float(AllClientBaby1, YAxis);
            Float(AllClientBaby2, ZAxis);
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

        private void Health(int ClientID, string HealthPercentage)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.HealthOffset;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (0x34C * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(HealthPercentage));
        } 

        private void GivePrimaryWeapon(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryWeapon;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        } 

        private void GivePrimaryWeaponAmmo(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryWeaponAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void GivePrimaryWeaponAmmoLeft(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryWeaponAmmoLeft;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        } 

        private void GiveSecondaryWeaponAmmo(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryWeaponAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void GiveSecondaryWeaponAmmoLeft(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryWeaponAmmoLeft;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void GiveSecondaryWeapon(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryWeapon;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        } 

        private void PlayerSwitchToWeapon(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SwitchWeapon;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void GivePrimaryAmmo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryInGunAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GivePrimaryAmmoLeft(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryLeftAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveSecondaryAmmoLeft(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryLeftAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveSecondaryAmmo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryInGunAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveFlashbangs(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Flashbangs;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveFrags(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Frags;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveSecondaryAkimbo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryAkimboInGun;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveGrenade(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.GrenadeLauncher;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveFlashbangs2(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Flashbangsv2;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void mFlag(int ClientID, string mFlag)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.mFlagOffset;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(mFlag));
        } 

        private void Visions(int ClientID, string VisionID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.VisionsOffset;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(VisionID));
        }

        public void Map()
        {
            if (Process_Handle("iw5mp"))
            {
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + GameAddresses.MapName;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                MapNameTXT.Text = nickz.ToString();
            }
        }

        public void WriteName(int ClientID, string Name)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.NameOffset;
            int AllClientBaby = Convert.ToInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            byte[] nickbyte = Encoding.GetBytes(Name + "\0");
            Memorys mem = new Memorys("iw5mp");
            int address = (int)mem.baseaddress("iw5mp") + AllClientBaby;
            WriteBytes(address, nickbyte);
        }

        public void CustomClassNames(int ClassID, string Name)
        {
            if (Process_Handle("iw5mp"))
            {
                int Class = ClassID;
                long Class0Offset = GameAddresses.CustomClass;
                int AllClassesBaby = Convert.ToInt32(Class0Offset + (GameAddresses.ClassPointer * Class));
                byte[] nickbyte = Encoding.GetBytes(Name + "\0");
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + AllClassesBaby;
                WriteBytes(address, nickbyte);
            }
        }

        public void PreGameName(string Name)
        {
            if (Process_Handle("iw5mp"))
            {
                byte[] nickbyte = Encoding.GetBytes(Name + "\0");
                Memorys mem = new Memorys("iw5mp");
                int address = (int)mem.baseaddress("iw5mp") + 0x0385892C;
                WriteBytes(address, nickbyte);
            }
        }

        private void XP_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.XP);
            string ValueID = Convert.ToString(XPValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Score_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Score);
            string ValueID = Convert.ToString(ScoreValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Wins_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Wins);
            string ValueID = Convert.ToString(WinsValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Losses_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Losses);
            string ValueID = Convert.ToString(LossesValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Ties_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Ties);
            string ValueID = Convert.ToString(TiesValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void WinStreak_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.WinStreak);
            string ValueID = Convert.ToString(WinsStreakValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Kills_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Kills);
            string ValueID = Convert.ToString(KillsValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Deaths_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Deaths);
            string ValueID = Convert.ToString(DeathsValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Headshots_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Headshots);
            string ValueID = Convert.ToString(HeadshotsValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Assists_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Assists);
            string ValueID = Convert.ToString(AssistsValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void KillStreak_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Killstreak);
            string ValueID = Convert.ToString(KillStreakValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void TimePlayed_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.TimePlayed);
            string ValueID = Convert.ToString(TimePlayedValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void PrestigeTokens_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Tokens);
            string ValueID = Convert.ToString(PrestigeTokensValue.Value);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void CustomClassNamesBtn_Click(object sender, EventArgs e)
        {
            CustomClassNames(0, "^1Custom Class 1");
            CustomClassNames(1, "^2Custom Class 2");
            CustomClassNames(2, "^3Custom Class 3");
            CustomClassNames(3, "^5Custom Class 4");
            CustomClassNames(4, "^6Custom Class 5");
            CustomClassNames(5, "^1Custom Class 6");
            CustomClassNames(6, "^2Custom Class 7");
            CustomClassNames(7, "^3Custom Class 8");
            CustomClassNames(8, "^5Custom Class 9");
            CustomClassNames(9, "^6Custom Class 10");
            CustomClassNames(0, "^1Custom Class 1");
            CustomClassNames(1, "^2Custom Class 2");
            CustomClassNames(2, "^3Custom Class 3");
            CustomClassNames(3, "^5Custom Class 4");
            CustomClassNames(4, "^6Custom Class 5");
            CustomClassNames(5, "^1Custom Class 6");
            CustomClassNames(6, "^2Custom Class 7");
            CustomClassNames(7, "^3Custom Class 8");
            CustomClassNames(8, "^5Custom Class 9");
            CustomClassNames(9, "^6Custom Class 10");
            CustomClassNames(0, "^1Custom Class 1");
            CustomClassNames(1, "^2Custom Class 2");
            CustomClassNames(2, "^3Custom Class 3");
            CustomClassNames(3, "^5Custom Class 4");
            CustomClassNames(4, "^6Custom Class 5");
            CustomClassNames(5, "^1Custom Class 6");
            CustomClassNames(6, "^2Custom Class 7");
            CustomClassNames(7, "^3Custom Class 8");
            CustomClassNames(8, "^5Custom Class 9");
            CustomClassNames(9, "^6Custom Class 10");
        }

        private void Prestige0_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("0"));
        }

        private void Prestige1_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("1"));
        }

        private void Prestige2_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("2"));
        }

        private void Prestige3_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("3"));
        }

        private void Prestige4_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("4"));
        }

        private void Prestige5_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("5"));
        }

        private void Prestige6_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("6"));
        }

        private void Prestige7_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("7"));
        }

        private void Prestige8_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("8"));
        }

        private void Prestige9_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("9"));
        }

        private void Prestige10_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("10"));
        }

        private void RefreshClient_Click(object sender, EventArgs e)
        {
            ReadClient0();
            ReadClient1();
            ReadClient2();
            ReadClient3();
            ReadClient4();
            ReadClient5();
            ReadClient6();
            ReadClient7();
            ReadClient8();
            ReadClient9();
            ReadClient10();
            ReadClient11();
            ReadClient12();
            ReadClient13();
            ReadClient14();
            ReadClient15();
            ReadClient16();
            ReadClient17();
            ReadClient0();
            ReadClient1();
            ReadClient2();
            ReadClient3();
            ReadClient4();
            ReadClient5();
            ReadClient6();
            ReadClient7();
            ReadClient8();
            ReadClient9();
            ReadClient10();
            ReadClient11();
            ReadClient12();
            ReadClient13();
            ReadClient14();
            ReadClient15();
            ReadClient16();
            ReadClient17();
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

        private void SendVision_Click(object sender, EventArgs e)
        {
            if (VisionOps.Text == "Red Boxes Vision")
            {
                int ClientID = Convert.ToInt32(VisionID.Value);
                Visions(ClientID, "16");
            }

            if (VisionOps.Text == "Red Boxes & Thermal Vision")
            {
                int ClientID = Convert.ToInt32(VisionID.Value);
                Visions(ClientID, "25");
            }

            if (VisionOps.Text == "Thermal Vision")
            {
                int ClientID = Convert.ToInt32(VisionID.Value);
                Visions(ClientID, "300");
            }

            if (VisionOps.Text == "Emp & Red Boxes Vision")
            {
                int ClientID = Convert.ToInt32(VisionID.Value);
                Visions(ClientID, "1337");
            }

            if (VisionOps.Text == "Normal Vision")
            {
                int ClientID = Convert.ToInt32(VisionID.Value);
                Visions(ClientID, "0");
            }
        }

        private void NameBtn_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(NameID.Value);
            WriteName(ClientID, NameTxt.Text);
            WriteName(ClientID, NameTxt.Text);
            WriteName(ClientID, NameTxt.Text);
        }

        private void SendProjectile_Click(object sender, EventArgs e)
        {
            if (ProjectileOps.Text == "harrier_rockets_projectile" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(ProjectileID.Value);
                GivePrimaryWeapon(ClientID, "0");
                GiveSecondaryWeapon(ClientID, "0");
                GivePrimaryWeapon(ClientID, "113");
                GivePrimaryWeaponAmmo(ClientID, "113");
                GivePrimaryWeaponAmmoLeft(ClientID, "113");
                PlayerSwitchToWeapon(ClientID, "113");
            }

            if (ProjectileOps.Text == "harrier_rockets_projectile" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(ProjectileID.Value);
                GivePrimaryWeapon(ClientID, "0");
                GiveSecondaryWeapon(ClientID, "0");
                GivePrimaryWeapon(ClientID, "112");
                GivePrimaryWeaponAmmo(ClientID, "112");
                GivePrimaryWeaponAmmoLeft(ClientID, "112");
                PlayerSwitchToWeapon(ClientID, "112");
            }
        }

        private void GiveGod_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveGodID.Value);
            Health(ClientID, "0");
        }

        private void TakeGod_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TakeGodID.Value);
            Health(ClientID, "100");
        }

        private void GiveFuckedUp_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveFuckedUpID.Value);
            Visions(ClientID, "434");
        }

        private void TakeFuckedUp_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TakeFuckedUpID.Value);
            Visions(ClientID, "0");
        }

        private void GiveIMS_Click(object sender, EventArgs e)
        {
            if (MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveIMSID.Value);
                GivePrimaryWeapon(ClientID, "0");
                GiveSecondaryWeapon(ClientID, "0");
                GivePrimaryWeapon(ClientID, "130");
                GivePrimaryWeaponAmmo(ClientID, "130");
                GivePrimaryWeaponAmmoLeft(ClientID, "130");
                PlayerSwitchToWeapon(ClientID, "130");
            }

            if (MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveIMSID.Value);
                GivePrimaryWeapon(ClientID, "0");
                GiveSecondaryWeapon(ClientID, "0");
                GivePrimaryWeapon(ClientID, "129");
                GivePrimaryWeaponAmmo(ClientID, "129");
                GivePrimaryWeaponAmmoLeft(ClientID, "129");
                PlayerSwitchToWeapon(ClientID, "129");
            }
        }

        private void TakeIMS_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveIMSID.Value);
            GivePrimaryWeapon(ClientID, "0");
            GiveSecondaryWeapon(ClientID, "0");
            GivePrimaryWeapon(ClientID, "8");
            GivePrimaryWeaponAmmo(ClientID, "8");
            PlayerSwitchToWeapon(ClientID, "8");
        }

        private void TeleportBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TeleportID.Value);
            float XText = Convert.ToSingle(XAxisTXT.Text);
            float YText = Convert.ToSingle(YAxisTXT.Text);
            float ZText = Convert.ToSingle(ZAxisTXT.Text);
            Teleport(ClientID, XText, YText, ZText);
        }

        private void TeleLoop_Tick(object sender, EventArgs e)
        {
            float XText = Convert.ToSingle(XAxisTXT.Text);
            float YText = Convert.ToSingle(YAxisTXT.Text);
            float ZText = Convert.ToSingle(ZAxisTXT.Text);

            if (TeleportID.Value == 0)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 1)
            {
                Teleport(0, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 2)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 3)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 4)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 5)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 6)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 7)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 8)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 9)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 10)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 11)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 12)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 13)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 14)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 15)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 16)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
                Teleport(17, XText, YText, ZText);
            }

            if (TeleportID.Value == 17)
            {
                Teleport(1, XText, YText, ZText);
                Teleport(2, XText, YText, ZText);
                Teleport(3, XText, YText, ZText);
                Teleport(4, XText, YText, ZText);
                Teleport(5, XText, YText, ZText);
                Teleport(6, XText, YText, ZText);
                Teleport(7, XText, YText, ZText);
                Teleport(8, XText, YText, ZText);
                Teleport(9, XText, YText, ZText);
                Teleport(10, XText, YText, ZText);
                Teleport(11, XText, YText, ZText);
                Teleport(12, XText, YText, ZText);
                Teleport(13, XText, YText, ZText);
                Teleport(14, XText, YText, ZText);
                Teleport(15, XText, YText, ZText);
                Teleport(16, XText, YText, ZText);
                Teleport(0, XText, YText, ZText);
            }
        }

        private void TeleportLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (TeleportLoop.Checked == true)
            {
                TeleLoop.Start();
            }
            else
            {
                TeleLoop.Stop();
            }
        }

        private void MP_Tick(object sender, EventArgs e)
        {
            if (Process_Handle("iw5mp"))
            {

            }
            else
            {
                RedBoxCheck.Stop();
                MP.Stop();
                MessageBox.Show("Please Make Sure MW3 Is Open Before Launching The Tool!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
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
            }
        }

        private void GiveAmmo_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveAmmoID.Value);
            GivePrimaryAmmo(ClientID, "999999999");
            GivePrimaryAmmoLeft(ClientID, "999999999");
            GiveSecondaryAmmo(ClientID, "999999999");
            GiveSecondaryAkimbo(ClientID, "999999999");
            GiveSecondaryAmmoLeft(ClientID, "999999999");
            GiveFrags(ClientID, "999999999");
            GiveFlashbangs(ClientID, "999999999");
            GiveFlashbangs2(ClientID, "999999999");
            GiveGrenade(ClientID, "999999999");
        }

        private void TakeAmmo_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TakeAmmoID.Value);
            GivePrimaryAmmo(ClientID, "0");
            GivePrimaryAmmoLeft(ClientID, "300");
            GiveSecondaryAmmo(ClientID, "0");
            GiveSecondaryAkimbo(ClientID, "0");
            GiveSecondaryAmmoLeft(ClientID, "300");
            GiveFrags(ClientID, "1");
            GiveFlashbangs(ClientID, "2");
            GiveFlashbangs2(ClientID, "2");
            GiveGrenade(ClientID, "2");
        }

        private void GiveBindsBTN_Click(object sender, EventArgs e)
        {
            if (ClientList.Text == Client0.Text)
            {
                WriteName(0, "Binds: ^2Given");
                WriteName(0, "Binds: ^2Given");
                WriteName(0, "Binds: ^2Given");
                WriteName(0, "Binds: ^2Given");
                WriteName(0, "Binds: ^2Given");
                C0Bind.Start();
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }

            if (ClientList.Text == Client1.Text)
            {
                WriteName(1, "Binds: ^2Given");
                WriteName(1, "Binds: ^2Given");
                WriteName(1, "Binds: ^2Given");
                WriteName(1, "Binds: ^2Given");
                WriteName(1, "Binds: ^2Given");
                C1Bind.Start();
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }

            if (ClientList.Text == Client2.Text)
            {
                WriteName(2, "Binds: ^2Given");
                WriteName(2, "Binds: ^2Given");
                WriteName(2, "Binds: ^2Given");
                WriteName(2, "Binds: ^2Given");
                WriteName(2, "Binds: ^2Given");
                C2Bind.Start();
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }

            if (ClientList.Text == Client3.Text)
            {
                WriteName(3, "Binds: ^2Given");
                WriteName(3, "Binds: ^2Given");
                WriteName(3, "Binds: ^2Given");
                WriteName(3, "Binds: ^2Given");
                WriteName(3, "Binds: ^2Given");
                C3Bind.Start();
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }

            if (ClientList.Text == Client4.Text)
            {
                WriteName(4, "Binds: ^2Given");
                WriteName(4, "Binds: ^2Given");
                WriteName(4, "Binds: ^2Given");
                WriteName(4, "Binds: ^2Given");
                WriteName(4, "Binds: ^2Given");
                C4Bind.Start();
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }

            if (ClientList.Text == Client5.Text)
            {
                WriteName(5, "Binds: ^2Given");
                WriteName(5, "Binds: ^2Given");
                WriteName(5, "Binds: ^2Given");
                WriteName(5, "Binds: ^2Given");
                WriteName(5, "Binds: ^2Given");
                C5Bind.Start();
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }

            if (ClientList.Text == Client6.Text)
            {
                WriteName(6, "Binds: ^2Given");
                WriteName(6, "Binds: ^2Given");
                WriteName(6, "Binds: ^2Given");
                WriteName(6, "Binds: ^2Given");
                WriteName(6, "Binds: ^2Given");
                C6Bind.Start();
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }

            if (ClientList.Text == Client7.Text)
            {
                WriteName(7, "Binds: ^2Given");
                WriteName(7, "Binds: ^2Given");
                WriteName(7, "Binds: ^2Given");
                WriteName(7, "Binds: ^2Given");
                WriteName(7, "Binds: ^2Given");
                C7Bind.Start();
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }

            if (ClientList.Text == Client8.Text)
            {
                WriteName(8, "Binds: ^2Given");
                WriteName(8, "Binds: ^2Given");
                WriteName(8, "Binds: ^2Given");
                WriteName(8, "Binds: ^2Given");
                WriteName(8, "Binds: ^2Given");
                C8Bind.Start();
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }

            if (ClientList.Text == Client9.Text)
            {
                WriteName(9, "Binds: ^2Given");
                WriteName(9, "Binds: ^2Given");
                WriteName(9, "Binds: ^2Given");
                WriteName(9, "Binds: ^2Given");
                WriteName(9, "Binds: ^2Given");
                C9Bind.Start();
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }

            if (ClientList.Text == Client10.Text)
            {
                WriteName(10, "Binds: ^2Given");
                WriteName(10, "Binds: ^2Given");
                WriteName(10, "Binds: ^2Given");
                WriteName(10, "Binds: ^2Given");
                WriteName(10, "Binds: ^2Given");
                C10Bind.Start();
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }

            if (ClientList.Text == Client11.Text)
            {
                WriteName(11, "Binds: ^2Given");
                WriteName(11, "Binds: ^2Given");
                WriteName(11, "Binds: ^2Given");
                WriteName(11, "Binds: ^2Given");
                WriteName(11, "Binds: ^2Given");
                C11Bind.Start();
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }

            if (ClientList.Text == Client12.Text)
            {
                WriteName(12, "Binds: ^2Given");
                WriteName(12, "Binds: ^2Given");
                WriteName(12, "Binds: ^2Given");
                WriteName(12, "Binds: ^2Given");
                WriteName(12, "Binds: ^2Given");
                C12Bind.Start();
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }

            if (ClientList.Text == Client13.Text)
            {
                WriteName(13, "Binds: ^2Given");
                WriteName(13, "Binds: ^2Given");
                WriteName(13, "Binds: ^2Given");
                WriteName(13, "Binds: ^2Given");
                WriteName(13, "Binds: ^2Given");
                C13Bind.Start();
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }

            if (ClientList.Text == Client14.Text)
            {
                WriteName(14, "Binds: ^2Given");
                WriteName(14, "Binds: ^2Given");
                WriteName(14, "Binds: ^2Given");
                WriteName(14, "Binds: ^2Given");
                WriteName(14, "Binds: ^2Given");
                C14Bind.Start();
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }

            if (ClientList.Text == Client15.Text)
            {
                WriteName(15, "Binds: ^2Given");
                WriteName(15, "Binds: ^2Given");
                WriteName(15, "Binds: ^2Given");
                WriteName(15, "Binds: ^2Given");
                WriteName(15, "Binds: ^2Given");
                C15Bind.Start();
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }

            if (ClientList.Text == Client16.Text)
            {
                WriteName(16, "Binds: ^2Given");
                WriteName(16, "Binds: ^2Given");
                WriteName(16, "Binds: ^2Given");
                WriteName(16, "Binds: ^2Given");
                WriteName(16, "Binds: ^2Given");
                C16Bind.Start();
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }

            if (ClientList.Text == Client17.Text)
            {
                WriteName(17, "Binds: ^2Given");
                WriteName(17, "Binds: ^2Given");
                WriteName(17, "Binds: ^2Given");
                WriteName(17, "Binds: ^2Given");
                WriteName(17, "Binds: ^2Given");
                C17Bind.Start();
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }
        }

        private void ClientList_Click(object sender, EventArgs e)
        {
            ClientList.Items.Clear();
            ClientList.Items.Insert(0, Client0.Text);
            ClientList.Items.Insert(1, Client1.Text);
            ClientList.Items.Insert(2, Client2.Text);
            ClientList.Items.Insert(3, Client3.Text);
            ClientList.Items.Insert(4, Client4.Text);
            ClientList.Items.Insert(5, Client5.Text);
            ClientList.Items.Insert(6, Client6.Text);
            ClientList.Items.Insert(7, Client7.Text);
            ClientList.Items.Insert(8, Client8.Text);
            ClientList.Items.Insert(9, Client9.Text);
            ClientList.Items.Insert(10, Client10.Text);
            ClientList.Items.Insert(11, Client11.Text);
            ClientList.Items.Insert(12, Client12.Text);
            ClientList.Items.Insert(13, Client13.Text);
            ClientList.Items.Insert(14, Client14.Text);
            ClientList.Items.Insert(15, Client15.Text);
            ClientList.Items.Insert(16, Client16.Text);
            ClientList.Items.Insert(17, Client17.Text);
        }

        private void TakeBindsBTN_Click(object sender, EventArgs e)
        {
            if (ClientList.Text == Client0.Text)
            {
                WriteName(0, "Binds: ^1Taken");
                WriteName(0, "Binds: ^1Taken");
                WriteName(0, "Binds: ^1Taken");
                WriteName(0, "Binds: ^1Taken");
                WriteName(0, "Binds: ^1Taken");
                C0Bind.Stop();
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }

            if (ClientList.Text == Client1.Text)
            {
                WriteName(1, "Binds: ^1Taken");
                WriteName(1, "Binds: ^1Taken");
                WriteName(1, "Binds: ^1Taken");
                WriteName(1, "Binds: ^1Taken");
                WriteName(1, "Binds: ^1Taken");
                C1Bind.Stop();
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }

            if (ClientList.Text == Client2.Text)
            {
                WriteName(2, "Binds: ^1Taken");
                WriteName(2, "Binds: ^1Taken");
                WriteName(2, "Binds: ^1Taken");
                WriteName(2, "Binds: ^1Taken");
                WriteName(2, "Binds: ^1Taken");
                C2Bind.Stop();
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }

            if (ClientList.Text == Client3.Text)
            {
                WriteName(3, "Binds: ^1Taken");
                WriteName(3, "Binds: ^1Taken");
                WriteName(3, "Binds: ^1Taken");
                WriteName(3, "Binds: ^1Taken");
                WriteName(3, "Binds: ^1Taken");
                C3Bind.Stop();
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }

            if (ClientList.Text == Client4.Text)
            {
                WriteName(4, "Binds: ^1Taken");
                WriteName(4, "Binds: ^1Taken");
                WriteName(4, "Binds: ^1Taken");
                WriteName(4, "Binds: ^1Taken");
                WriteName(4, "Binds: ^1Taken");
                C4Bind.Stop();
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }

            if (ClientList.Text == Client5.Text)
            {
                WriteName(5, "Binds: ^1Taken");
                WriteName(5, "Binds: ^1Taken");
                WriteName(5, "Binds: ^1Taken");
                WriteName(5, "Binds: ^1Taken");
                WriteName(5, "Binds: ^1Taken");
                C5Bind.Stop();
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }

            if (ClientList.Text == Client6.Text)
            {
                WriteName(6, "Binds: ^1Taken");
                WriteName(6, "Binds: ^1Taken");
                WriteName(6, "Binds: ^1Taken");
                WriteName(6, "Binds: ^1Taken");
                WriteName(6, "Binds: ^1Taken");
                C6Bind.Stop();
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }

            if (ClientList.Text == Client7.Text)
            {
                WriteName(7, "Binds: ^1Taken");
                WriteName(7, "Binds: ^1Taken");
                WriteName(7, "Binds: ^1Taken");
                WriteName(7, "Binds: ^1Taken");
                WriteName(7, "Binds: ^1Taken");
                C7Bind.Stop();
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }

            if (ClientList.Text == Client8.Text)
            {
                WriteName(8, "Binds: ^1Taken");
                WriteName(8, "Binds: ^1Taken");
                WriteName(8, "Binds: ^1Taken");
                WriteName(8, "Binds: ^1Taken");
                WriteName(8, "Binds: ^1Taken");
                C8Bind.Stop();
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }

            if (ClientList.Text == Client9.Text)
            {
                WriteName(9, "Binds: ^1Taken");
                WriteName(9, "Binds: ^1Taken");
                WriteName(9, "Binds: ^1Taken");
                WriteName(9, "Binds: ^1Taken");
                WriteName(9, "Binds: ^1Taken");
                C9Bind.Stop();
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }

            if (ClientList.Text == Client10.Text)
            {
                WriteName(10, "Binds: ^1Taken");
                WriteName(10, "Binds: ^1Taken");
                WriteName(10, "Binds: ^1Taken");
                WriteName(10, "Binds: ^1Taken");
                WriteName(10, "Binds: ^1Taken");
                C10Bind.Stop();
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }

            if (ClientList.Text == Client11.Text)
            {
                WriteName(11, "Binds: ^1Taken");
                WriteName(11, "Binds: ^1Taken");
                WriteName(11, "Binds: ^1Taken");
                WriteName(11, "Binds: ^1Taken");
                WriteName(11, "Binds: ^1Taken");
                C11Bind.Stop();
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }

            if (ClientList.Text == Client12.Text)
            {
                WriteName(12, "Binds: ^1Taken");
                WriteName(12, "Binds: ^1Taken");
                WriteName(12, "Binds: ^1Taken");
                WriteName(12, "Binds: ^1Taken");
                WriteName(12, "Binds: ^1Taken");
                C12Bind.Stop();
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }

            if (ClientList.Text == Client13.Text)
            {
                WriteName(13, "Binds: ^1Taken");
                WriteName(13, "Binds: ^1Taken");
                WriteName(13, "Binds: ^1Taken");
                WriteName(13, "Binds: ^1Taken");
                WriteName(13, "Binds: ^1Taken");
                C13Bind.Stop();
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }

            if (ClientList.Text == Client14.Text)
            {
                WriteName(14, "Binds: ^1Taken");
                WriteName(14, "Binds: ^1Taken");
                WriteName(14, "Binds: ^1Taken");
                WriteName(14, "Binds: ^1Taken");
                WriteName(14, "Binds: ^1Taken");
                C14Bind.Stop();
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }

            if (ClientList.Text == Client15.Text)
            {
                WriteName(15, "Binds: ^1Taken");
                WriteName(15, "Binds: ^1Taken");
                WriteName(15, "Binds: ^1Taken");
                WriteName(15, "Binds: ^1Taken");
                WriteName(15, "Binds: ^1Taken");
                C15Bind.Stop();
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }

            if (ClientList.Text == Client16.Text)
            {
                WriteName(16, "Binds: ^1Taken");
                WriteName(16, "Binds: ^1Taken");
                WriteName(16, "Binds: ^1Taken");
                WriteName(16, "Binds: ^1Taken");
                WriteName(16, "Binds: ^1Taken");
                C16Bind.Stop();
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }

            if (ClientList.Text == Client17.Text)
            {
                WriteName(17, "Binds: ^1Taken");
                WriteName(17, "Binds: ^1Taken");
                WriteName(17, "Binds: ^1Taken");
                WriteName(17, "Binds: ^1Taken");
                WriteName(17, "Binds: ^1Taken");
                C17Bind.Stop();
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }
        }

        private void C0Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client0Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client0BTN.Text = address0.ToString();

            #region Toggle God
            if (Client0BTN.Text == "67108868")
            {
                WriteName(0, "God Mode: ^2ON");
                WriteName(0, "God Mode: ^2ON");
                WriteName(0, "God Mode: ^2ON");
                WriteName(0, "God Mode: ^2ON");
                WriteName(0, "God Mode: ^2ON");
                Health(0, "0");
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }

            if (Client0BTN.Text == "67109124")
            {
                WriteName(0, "God Mode: ^1OFF");
                WriteName(0, "God Mode: ^1OFF");
                WriteName(0, "God Mode: ^1OFF");
                WriteName(0, "God Mode: ^1OFF");
                WriteName(0, "God Mode: ^1OFF");
                Health(0, "100");
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }
            #endregion

            #region Noclip
            if (Client0BTN.Text == "32768")
            {
                WriteName(0, "Noclip: ^2ON");
                WriteName(0, "Noclip: ^2ON");
                WriteName(0, "Noclip: ^2ON");
                WriteName(0, "Noclip: ^2ON");
                WriteName(0, "Noclip: ^2ON");
                mFlag(0, "1");
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }

            if (Client0BTN.Text == "16384")
            {
                WriteName(0, "Noclip: ^1OFF");
                WriteName(0, "Noclip: ^1OFF");
                WriteName(0, "Noclip: ^1OFF");
                WriteName(0, "Noclip: ^1OFF");
                WriteName(0, "Noclip: ^1OFF");
                mFlag(0, "0");
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }
            #endregion

            #region Package
            if (Client0BTN.Text == "67635460")
            {
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                Visions(0, "4112");
                GiveSecondaryWeapon(0, "1");
                GiveSecondaryWeaponAmmo(0, "1");
                GivePrimaryAmmo(0, "999999999");
                GivePrimaryAmmoLeft(0, "999999999");
                GiveSecondaryAmmo(0, "999999999");
                GiveSecondaryAkimbo(0, "999999999");
                GiveSecondaryAmmoLeft(0, "999999999");
                GiveFrags(0, "999999999");
                GiveFlashbangs(0, "999999999");
                GiveFlashbangs2(0, "999999999");
                GiveGrenade(0, "999999999");
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }

            if (Client0BTN.Text == "67635204")
            {
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                Visions(0, "4096");
                GiveSecondaryWeapon(0, "4");
                GiveSecondaryWeaponAmmo(0, "4");
                GivePrimaryAmmo(0, "0");
                GivePrimaryAmmoLeft(0, "300");
                GiveSecondaryAmmo(0, "0");
                GiveSecondaryAkimbo(0, "0");
                GiveSecondaryAmmoLeft(0, "300");
                GiveFrags(0, "1");
                GiveFlashbangs(0, "2");
                GiveFlashbangs2(0, "2");
                GiveGrenade(0, "2");
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
                WriteName(0, Client0.Text);
            }
            #endregion
        }

        private void C1Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client1Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client1BTN.Text = address0.ToString();

            #region Toggle God
            if (Client1BTN.Text == "67108868")
            {
                WriteName(1, "God Mode: ^2ON");
                WriteName(1, "God Mode: ^2ON");
                WriteName(1, "God Mode: ^2ON");
                WriteName(1, "God Mode: ^2ON");
                WriteName(1, "God Mode: ^2ON");
                Health(1, "0");
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }

            if (Client1BTN.Text == "67109124")
            {
                WriteName(1, "God Mode: ^1OFF");
                WriteName(1, "God Mode: ^1OFF");
                WriteName(1, "God Mode: ^1OFF");
                WriteName(1, "God Mode: ^1OFF");
                WriteName(1, "God Mode: ^1OFF");
                Health(1, "100");
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }
            #endregion

            #region Noclip
            if (Client1BTN.Text == "32768")
            {
                WriteName(1, "Noclip: ^2ON");
                WriteName(1, "Noclip: ^2ON");
                WriteName(1, "Noclip: ^2ON");
                WriteName(1, "Noclip: ^2ON");
                WriteName(1, "Noclip: ^2ON");
                mFlag(1, "1");
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }

            if (Client1BTN.Text == "16384")
            {
                WriteName(1, "Noclip: ^1OFF");
                WriteName(1, "Noclip: ^1OFF");
                WriteName(1, "Noclip: ^1OFF");
                WriteName(1, "Noclip: ^1OFF");
                WriteName(1, "Noclip: ^1OFF");
                mFlag(1, "0");
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }
            #endregion

            #region Package
            if (Client1BTN.Text == "67635460")
            {
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                Visions(1, "4112");
                GiveSecondaryWeapon(1, "1");
                GiveSecondaryWeaponAmmo(1, "1");
                GivePrimaryAmmo(1, "999999999");
                GivePrimaryAmmoLeft(1, "999999999");
                GiveSecondaryAmmo(1, "999999999");
                GiveSecondaryAkimbo(1, "999999999");
                GiveSecondaryAmmoLeft(1, "999999999");
                GiveFrags(1, "999999999");
                GiveFlashbangs(1, "999999999");
                GiveFlashbangs2(1, "999999999");
                GiveGrenade(1, "999999999");
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }

            if (Client1BTN.Text == "67635204")
            {
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                Visions(1, "4096");
                GiveSecondaryWeapon(1, "4");
                GiveSecondaryWeaponAmmo(1, "4");
                GivePrimaryAmmo(1, "0");
                GivePrimaryAmmoLeft(1, "300");
                GiveSecondaryAmmo(1, "0");
                GiveSecondaryAkimbo(1, "0");
                GiveSecondaryAmmoLeft(1, "300");
                GiveFrags(1, "1");
                GiveFlashbangs(1, "2");
                GiveFlashbangs2(1, "2");
                GiveGrenade(1, "2");
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
                WriteName(1, Client1.Text);
            }
            #endregion
        }

        private void C2Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client2Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client2BTN.Text = address0.ToString();

            #region Toggle God
            if (Client2BTN.Text == "67108868")
            {
                WriteName(2, "God Mode: ^2ON");
                WriteName(2, "God Mode: ^2ON");
                WriteName(2, "God Mode: ^2ON");
                WriteName(2, "God Mode: ^2ON");
                WriteName(2, "God Mode: ^2ON");
                Health(2, "0");
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }

            if (Client2BTN.Text == "67109124")
            {
                WriteName(2, "God Mode: ^1OFF");
                WriteName(2, "God Mode: ^1OFF");
                WriteName(2, "God Mode: ^1OFF");
                WriteName(2, "God Mode: ^1OFF");
                WriteName(2, "God Mode: ^1OFF");
                Health(2, "100");
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }
            #endregion

            #region Noclip
            if (Client2BTN.Text == "32768")
            {
                WriteName(2, "Noclip: ^2ON");
                WriteName(2, "Noclip: ^2ON");
                WriteName(2, "Noclip: ^2ON");
                WriteName(2, "Noclip: ^2ON");
                WriteName(2, "Noclip: ^2ON");
                mFlag(2, "1");
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }

            if (Client2BTN.Text == "16384")
            {
                WriteName(2, "Noclip: ^1OFF");
                WriteName(2, "Noclip: ^1OFF");
                WriteName(2, "Noclip: ^1OFF");
                WriteName(2, "Noclip: ^1OFF");
                WriteName(2, "Noclip: ^1OFF");
                mFlag(2, "0");
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }
            #endregion

            #region Package
            if (Client2BTN.Text == "67635460")
            {
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                Visions(2, "4112");
                GiveSecondaryWeapon(2, "1");
                GiveSecondaryWeaponAmmo(2, "1");
                GivePrimaryAmmo(2, "999999999");
                GivePrimaryAmmoLeft(2, "999999999");
                GiveSecondaryAmmo(2, "999999999");
                GiveSecondaryAkimbo(2, "999999999");
                GiveSecondaryAmmoLeft(2, "999999999");
                GiveFrags(2, "999999999");
                GiveFlashbangs(2, "999999999");
                GiveFlashbangs2(2, "999999999");
                GiveGrenade(2, "999999999");
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }

            if (Client2BTN.Text == "67635204")
            {
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                Visions(2, "4096");
                GiveSecondaryWeapon(2, "4");
                GiveSecondaryWeaponAmmo(2, "4");
                GivePrimaryAmmo(2, "0");
                GivePrimaryAmmoLeft(2, "300");
                GiveSecondaryAmmo(2, "0");
                GiveSecondaryAkimbo(2, "0");
                GiveSecondaryAmmoLeft(2, "300");
                GiveFrags(2, "1");
                GiveFlashbangs(2, "2");
                GiveFlashbangs2(2, "2");
                GiveGrenade(2, "2");
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
                WriteName(2, Client2.Text);
            }
            #endregion
        }

        private void C3Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client3Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client3BTN.Text = address0.ToString();

            #region Toggle God
            if (Client3BTN.Text == "67108868")
            {
                WriteName(3, "God Mode: ^2ON");
                WriteName(3, "God Mode: ^2ON");
                WriteName(3, "God Mode: ^2ON");
                WriteName(3, "God Mode: ^2ON");
                WriteName(3, "God Mode: ^2ON");
                Health(3, "0");
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }

            if (Client3BTN.Text == "67109124")
            {
                WriteName(3, "God Mode: ^1OFF");
                WriteName(3, "God Mode: ^1OFF");
                WriteName(3, "God Mode: ^1OFF");
                WriteName(3, "God Mode: ^1OFF");
                WriteName(3, "God Mode: ^1OFF");
                Health(3, "100");
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }
            #endregion

            #region Noclip
            if (Client3BTN.Text == "32768")
            {
                WriteName(3, "Noclip: ^2ON");
                WriteName(3, "Noclip: ^2ON");
                WriteName(3, "Noclip: ^2ON");
                WriteName(3, "Noclip: ^2ON");
                WriteName(3, "Noclip: ^2ON");
                mFlag(3, "1");
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }

            if (Client3BTN.Text == "16384")
            {
                WriteName(3, "Noclip: ^1OFF");
                WriteName(3, "Noclip: ^1OFF");
                WriteName(3, "Noclip: ^1OFF");
                WriteName(3, "Noclip: ^1OFF");
                WriteName(3, "Noclip: ^1OFF");
                mFlag(3, "0");
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }
            #endregion

            #region Package
            if (Client3BTN.Text == "67635460")
            {
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                Visions(3, "4112");
                GiveSecondaryWeapon(3, "1");
                GiveSecondaryWeaponAmmo(3, "1");
                GivePrimaryAmmo(3, "999999999");
                GivePrimaryAmmoLeft(3, "999999999");
                GiveSecondaryAmmo(3, "999999999");
                GiveSecondaryAkimbo(3, "999999999");
                GiveSecondaryAmmoLeft(3, "999999999");
                GiveFrags(3, "999999999");
                GiveFlashbangs(3, "999999999");
                GiveFlashbangs2(3, "999999999");
                GiveGrenade(3, "999999999");
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }

            if (Client3BTN.Text == "67635204")
            {
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                Visions(3, "4096");
                GiveSecondaryWeapon(3, "4");
                GiveSecondaryWeaponAmmo(3, "4");
                GivePrimaryAmmo(3, "0");
                GivePrimaryAmmoLeft(3, "300");
                GiveSecondaryAmmo(3, "0");
                GiveSecondaryAkimbo(3, "0");
                GiveSecondaryAmmoLeft(3, "300");
                GiveFrags(3, "1");
                GiveFlashbangs(3, "2");
                GiveFlashbangs2(3, "2");
                GiveGrenade(3, "2");
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
                WriteName(3, Client3.Text);
            }
            #endregion
        }

        private void RedBoxON_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.RedBoxCheck);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            RedCheck.Text = address0.ToString();

            if (RedCheck.Text == "0")
            {
                RedBoxSwitch.Value = false;
            }
        }

        private void SendWeapon_Click(object sender, EventArgs e)
        {
            if (WeaponOps.Text == "M4A1" & Attach1Ops.Text == "Red Dot Sight")
            {
                string M4_RED_DOT_SIGHT = "12288";
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "26" + M4_RED_DOT_SIGHT);
                GiveSecondaryWeaponAmmo(ClientID, "26");
                GiveSecondaryWeaponAmmoLeft(ClientID, "26");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (WeaponOps.Text == "M4A1" && Attach1Ops.Text == "Red Dot Sight")
            {
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "12314");
                GiveSecondaryWeaponAmmo(ClientID, "12314");
                GiveSecondaryWeaponAmmoLeft(ClientID, "12314");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (WeaponOps.Text == "M4A1" && Attach1Ops.Text == "ACOG Scope")
            {
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "4122");
                GiveSecondaryWeaponAmmo(ClientID, "4122");
                GiveSecondaryWeaponAmmoLeft(ClientID, "4122");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (WeaponOps.Text == "M4A1" && Attach1Ops.Text == "Holographic Sight")
            {
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "8218");
                GiveSecondaryWeaponAmmo(ClientID, "8218");
                GiveSecondaryWeaponAmmoLeft(ClientID, "8218");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (WeaponOps.Text == "M4A1" && Attach1Ops.Text == "Hybrid Sight")
            {
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "98330");
                GiveSecondaryWeaponAmmo(ClientID, "98330");
                GiveSecondaryWeaponAmmoLeft(ClientID, "98330");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (WeaponOps.Text == "M4A1" && Attach1Ops.Text == "Hybrid Sight")
            {
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "98330");
                GiveSecondaryWeaponAmmo(ClientID, "98330");
                GiveSecondaryWeaponAmmoLeft(ClientID, "98330");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (WeaponOps.Text == "M4A1" && Attach1Ops.Text == "Thermal Scope")
            {
                int ClientID = Convert.ToInt32(CustomWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "16410");
                GiveSecondaryWeaponAmmo(ClientID, "16410");
                GiveSecondaryWeaponAmmoLeft(ClientID, "16410");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }
        }

        private void C4Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client4Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client4BTN.Text = address0.ToString();

            #region Toggle God
            if (Client4BTN.Text == "67108868")
            {
                WriteName(4, "God Mode: ^2ON");
                WriteName(4, "God Mode: ^2ON");
                WriteName(4, "God Mode: ^2ON");
                WriteName(4, "God Mode: ^2ON");
                WriteName(4, "God Mode: ^2ON");
                Health(4, "0");
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }

            if (Client4BTN.Text == "67109124")
            {
                WriteName(4, "God Mode: ^1OFF");
                WriteName(4, "God Mode: ^1OFF");
                WriteName(4, "God Mode: ^1OFF");
                WriteName(4, "God Mode: ^1OFF");
                WriteName(4, "God Mode: ^1OFF");
                Health(4, "100");
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }
            #endregion

            #region Noclip
            if (Client4BTN.Text == "32768")
            {
                WriteName(4, "Noclip: ^2ON");
                WriteName(4, "Noclip: ^2ON");
                WriteName(4, "Noclip: ^2ON");
                WriteName(4, "Noclip: ^2ON");
                WriteName(4, "Noclip: ^2ON");
                mFlag(4, "1");
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }

            if (Client4BTN.Text == "16384")
            {
                WriteName(4, "Noclip: ^1OFF");
                WriteName(4, "Noclip: ^1OFF");
                WriteName(4, "Noclip: ^1OFF");
                WriteName(4, "Noclip: ^1OFF");
                WriteName(4, "Noclip: ^1OFF");
                mFlag(4, "0");
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }
            #endregion

            #region Package
            if (Client4BTN.Text == "67635460")
            {
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                Visions(4, "4112");
                GiveSecondaryWeapon(4, "1");
                GiveSecondaryWeaponAmmo(4, "1");
                GivePrimaryAmmo(4, "999999999");
                GivePrimaryAmmoLeft(4, "999999999");
                GiveSecondaryAmmo(4, "999999999");
                GiveSecondaryAkimbo(4, "999999999");
                GiveSecondaryAmmoLeft(4, "999999999");
                GiveFrags(4, "999999999");
                GiveFlashbangs(4, "999999999");
                GiveFlashbangs2(4, "999999999");
                GiveGrenade(4, "999999999");
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }

            if (Client4BTN.Text == "67635204")
            {
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                Visions(4, "4096");
                GiveSecondaryWeapon(4, "4");
                GiveSecondaryWeaponAmmo(4, "4");
                GivePrimaryAmmo(4, "0");
                GivePrimaryAmmoLeft(4, "300");
                GiveSecondaryAmmo(4, "0");
                GiveSecondaryAkimbo(4, "0");
                GiveSecondaryAmmoLeft(4, "300");
                GiveFrags(4, "1");
                GiveFlashbangs(4, "2");
                GiveFlashbangs2(4, "2");
                GiveGrenade(4, "2");
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
                WriteName(4, Client4.Text);
            }
            #endregion
        }

        private void C5Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client5Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client5BTN.Text = address0.ToString();

            #region Toggle God
            if (Client5BTN.Text == "67108868")
            {
                WriteName(5, "God Mode: ^2ON");
                WriteName(5, "God Mode: ^2ON");
                WriteName(5, "God Mode: ^2ON");
                WriteName(5, "God Mode: ^2ON");
                WriteName(5, "God Mode: ^2ON");
                Health(5, "0");
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }

            if (Client5BTN.Text == "67109124")
            {
                WriteName(5, "God Mode: ^1OFF");
                WriteName(5, "God Mode: ^1OFF");
                WriteName(5, "God Mode: ^1OFF");
                WriteName(5, "God Mode: ^1OFF");
                WriteName(5, "God Mode: ^1OFF");
                Health(5, "100");
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }
            #endregion

            #region Noclip
            if (Client5BTN.Text == "32768")
            {
                WriteName(5, "Noclip: ^2ON");
                WriteName(5, "Noclip: ^2ON");
                WriteName(5, "Noclip: ^2ON");
                WriteName(5, "Noclip: ^2ON");
                WriteName(5, "Noclip: ^2ON");
                mFlag(5, "1");
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }

            if (Client5BTN.Text == "16384")
            {
                WriteName(5, "Noclip: ^1OFF");
                WriteName(5, "Noclip: ^1OFF");
                WriteName(5, "Noclip: ^1OFF");
                WriteName(5, "Noclip: ^1OFF");
                WriteName(5, "Noclip: ^1OFF");
                mFlag(5, "0");
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }
            #endregion

            #region Package
            if (Client5BTN.Text == "67635460")
            {
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                Visions(5, "4112");
                GiveSecondaryWeapon(5, "1");
                GiveSecondaryWeaponAmmo(5, "1");
                GivePrimaryAmmo(5, "999999999");
                GivePrimaryAmmoLeft(5, "999999999");
                GiveSecondaryAmmo(5, "999999999");
                GiveSecondaryAkimbo(5, "999999999");
                GiveSecondaryAmmoLeft(5, "999999999");
                GiveFrags(5, "999999999");
                GiveFlashbangs(5, "999999999");
                GiveFlashbangs2(5, "999999999");
                GiveGrenade(5, "999999999");
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }

            if (Client5BTN.Text == "67635204")
            {
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                Visions(5, "4096");
                GiveSecondaryWeapon(5, "4");
                GiveSecondaryWeaponAmmo(5, "4");
                GivePrimaryAmmo(5, "0");
                GivePrimaryAmmoLeft(5, "300");
                GiveSecondaryAmmo(5, "0");
                GiveSecondaryAkimbo(5, "0");
                GiveSecondaryAmmoLeft(5, "300");
                GiveFrags(5, "1");
                GiveFlashbangs(5, "2");
                GiveFlashbangs2(5, "2");
                GiveGrenade(5, "2");
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
                WriteName(5, Client5.Text);
            }
            #endregion
        }

        private void C6Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client6Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client6BTN.Text = address0.ToString();

            #region Toggle God
            if (Client6BTN.Text == "67108868")
            {
                WriteName(6, "God Mode: ^2ON");
                WriteName(6, "God Mode: ^2ON");
                WriteName(6, "God Mode: ^2ON");
                WriteName(6, "God Mode: ^2ON");
                WriteName(6, "God Mode: ^2ON");
                Health(6, "0");
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }

            if (Client6BTN.Text == "67109124")
            {
                WriteName(6, "God Mode: ^1OFF");
                WriteName(6, "God Mode: ^1OFF");
                WriteName(6, "God Mode: ^1OFF");
                WriteName(6, "God Mode: ^1OFF");
                WriteName(6, "God Mode: ^1OFF");
                Health(6, "100");
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }
            #endregion

            #region Noclip
            if (Client6BTN.Text == "32768")
            {
                WriteName(6, "Noclip: ^2ON");
                WriteName(6, "Noclip: ^2ON");
                WriteName(6, "Noclip: ^2ON");
                WriteName(6, "Noclip: ^2ON");
                WriteName(6, "Noclip: ^2ON");
                mFlag(6, "1");
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }

            if (Client6BTN.Text == "16384")
            {
                WriteName(6, "Noclip: ^1OFF");
                WriteName(6, "Noclip: ^1OFF");
                WriteName(6, "Noclip: ^1OFF");
                WriteName(6, "Noclip: ^1OFF");
                WriteName(6, "Noclip: ^1OFF");
                mFlag(6, "0");
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }
            #endregion

            #region Package
            if (Client6BTN.Text == "67635460")
            {
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                Visions(6, "4112");
                GiveSecondaryWeapon(6, "1");
                GiveSecondaryWeaponAmmo(6, "1");
                GivePrimaryAmmo(6, "999999999");
                GivePrimaryAmmoLeft(6, "999999999");
                GiveSecondaryAmmo(6, "999999999");
                GiveSecondaryAkimbo(6, "999999999");
                GiveSecondaryAmmoLeft(6, "999999999");
                GiveFrags(6, "999999999");
                GiveFlashbangs(6, "999999999");
                GiveFlashbangs2(6, "999999999");
                GiveGrenade(6, "999999999");
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }

            if (Client6BTN.Text == "67635204")
            {
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                Visions(6, "4096");
                GiveSecondaryWeapon(6, "4");
                GiveSecondaryWeaponAmmo(6, "4");
                GivePrimaryAmmo(6, "0");
                GivePrimaryAmmoLeft(6, "300");
                GiveSecondaryAmmo(6, "0");
                GiveSecondaryAkimbo(6, "0");
                GiveSecondaryAmmoLeft(6, "300");
                GiveFrags(6, "1");
                GiveFlashbangs(6, "2");
                GiveFlashbangs2(6, "2");
                GiveGrenade(6, "2");
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
                WriteName(6, Client6.Text);
            }
            #endregion
        }

        private void C7Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client7Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client7BTN.Text = address0.ToString();

            #region Toggle God
            if (Client7BTN.Text == "67108868")
            {
                WriteName(7, "God Mode: ^2ON");
                WriteName(7, "God Mode: ^2ON");
                WriteName(7, "God Mode: ^2ON");
                WriteName(7, "God Mode: ^2ON");
                WriteName(7, "God Mode: ^2ON");
                Health(7, "0");
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }

            if (Client7BTN.Text == "67109124")
            {
                WriteName(7, "God Mode: ^1OFF");
                WriteName(7, "God Mode: ^1OFF");
                WriteName(7, "God Mode: ^1OFF");
                WriteName(7, "God Mode: ^1OFF");
                WriteName(7, "God Mode: ^1OFF");
                Health(7, "100");
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }
            #endregion

            #region Noclip
            if (Client7BTN.Text == "32768")
            {
                WriteName(7, "Noclip: ^2ON");
                WriteName(7, "Noclip: ^2ON");
                WriteName(7, "Noclip: ^2ON");
                WriteName(7, "Noclip: ^2ON");
                WriteName(7, "Noclip: ^2ON");
                mFlag(7, "1");
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }

            if (Client7BTN.Text == "16384")
            {
                WriteName(7, "Noclip: ^1OFF");
                WriteName(7, "Noclip: ^1OFF");
                WriteName(7, "Noclip: ^1OFF");
                WriteName(7, "Noclip: ^1OFF");
                WriteName(7, "Noclip: ^1OFF");
                mFlag(7, "0");
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }
            #endregion

            #region Package
            if (Client7BTN.Text == "67635460")
            {
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                Visions(7, "4112");
                GiveSecondaryWeapon(7, "1");
                GiveSecondaryWeaponAmmo(7, "1");
                GivePrimaryAmmo(7, "999999999");
                GivePrimaryAmmoLeft(7, "999999999");
                GiveSecondaryAmmo(7, "999999999");
                GiveSecondaryAkimbo(7, "999999999");
                GiveSecondaryAmmoLeft(7, "999999999");
                GiveFrags(7, "999999999");
                GiveFlashbangs(7, "999999999");
                GiveFlashbangs2(7, "999999999");
                GiveGrenade(7, "999999999");
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }

            if (Client7BTN.Text == "67635204")
            {
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                Visions(7, "4096");
                GiveSecondaryWeapon(7, "4");
                GiveSecondaryWeaponAmmo(7, "4");
                GivePrimaryAmmo(7, "0");
                GivePrimaryAmmoLeft(7, "300");
                GiveSecondaryAmmo(7, "0");
                GiveSecondaryAkimbo(7, "0");
                GiveSecondaryAmmoLeft(7, "300");
                GiveFrags(7, "1");
                GiveFlashbangs(7, "2");
                GiveFlashbangs2(7, "2");
                GiveGrenade(7, "2");
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
                WriteName(7, Client7.Text);
            }
            #endregion
        }

        private void C8Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client8Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client8BTN.Text = address0.ToString();

            #region Toggle God
            if (Client8BTN.Text == "67108868")
            {
                WriteName(8, "God Mode: ^2ON");
                WriteName(8, "God Mode: ^2ON");
                WriteName(8, "God Mode: ^2ON");
                WriteName(8, "God Mode: ^2ON");
                WriteName(8, "God Mode: ^2ON");
                Health(8, "0");
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }

            if (Client8BTN.Text == "67109124")
            {
                WriteName(8, "God Mode: ^1OFF");
                WriteName(8, "God Mode: ^1OFF");
                WriteName(8, "God Mode: ^1OFF");
                WriteName(8, "God Mode: ^1OFF");
                WriteName(8, "God Mode: ^1OFF");
                Health(8, "100");
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }
            #endregion

            #region Noclip
            if (Client8BTN.Text == "32768")
            {
                WriteName(8, "Noclip: ^2ON");
                WriteName(8, "Noclip: ^2ON");
                WriteName(8, "Noclip: ^2ON");
                WriteName(8, "Noclip: ^2ON");
                WriteName(8, "Noclip: ^2ON");
                mFlag(8, "1");
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }

            if (Client8BTN.Text == "16384")
            {
                WriteName(8, "Noclip: ^1OFF");
                WriteName(8, "Noclip: ^1OFF");
                WriteName(8, "Noclip: ^1OFF");
                WriteName(8, "Noclip: ^1OFF");
                WriteName(8, "Noclip: ^1OFF");
                mFlag(8, "0");
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }
            #endregion

            #region Package
            if (Client8BTN.Text == "67635460")
            {
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                Visions(8, "4112");
                GiveSecondaryWeapon(8, "1");
                GiveSecondaryWeaponAmmo(8, "1");
                GivePrimaryAmmo(8, "999999999");
                GivePrimaryAmmoLeft(8, "999999999");
                GiveSecondaryAmmo(8, "999999999");
                GiveSecondaryAkimbo(8, "999999999");
                GiveSecondaryAmmoLeft(8, "999999999");
                GiveFrags(8, "999999999");
                GiveFlashbangs(8, "999999999");
                GiveFlashbangs2(8, "999999999");
                GiveGrenade(8, "999999999");
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }

            if (Client8BTN.Text == "67635204")
            {
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                Visions(8, "4096");
                GiveSecondaryWeapon(8, "4");
                GiveSecondaryWeaponAmmo(8, "4");
                GivePrimaryAmmo(8, "0");
                GivePrimaryAmmoLeft(8, "300");
                GiveSecondaryAmmo(8, "0");
                GiveSecondaryAkimbo(8, "0");
                GiveSecondaryAmmoLeft(8, "300");
                GiveFrags(8, "1");
                GiveFlashbangs(8, "2");
                GiveFlashbangs2(8, "2");
                GiveGrenade(8, "2");
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
                WriteName(8, Client8.Text);
            }
            #endregion
        }

        private void C9Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client9Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client9BTN.Text = address0.ToString();

            #region Toggle God
            if (Client9BTN.Text == "67108868")
            {
                WriteName(9, "God Mode: ^2ON");
                WriteName(9, "God Mode: ^2ON");
                WriteName(9, "God Mode: ^2ON");
                WriteName(9, "God Mode: ^2ON");
                WriteName(9, "God Mode: ^2ON");
                Health(9, "0");
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }

            if (Client9BTN.Text == "67109124")
            {
                WriteName(9, "God Mode: ^1OFF");
                WriteName(9, "God Mode: ^1OFF");
                WriteName(9, "God Mode: ^1OFF");
                WriteName(9, "God Mode: ^1OFF");
                WriteName(9, "God Mode: ^1OFF");
                Health(9, "100");
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }
            #endregion

            #region Noclip
            if (Client9BTN.Text == "32768")
            {
                WriteName(9, "Noclip: ^2ON");
                WriteName(9, "Noclip: ^2ON");
                WriteName(9, "Noclip: ^2ON");
                WriteName(9, "Noclip: ^2ON");
                WriteName(9, "Noclip: ^2ON");
                mFlag(9, "1");
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }

            if (Client9BTN.Text == "16384")
            {
                WriteName(9, "Noclip: ^1OFF");
                WriteName(9, "Noclip: ^1OFF");
                WriteName(9, "Noclip: ^1OFF");
                WriteName(9, "Noclip: ^1OFF");
                WriteName(9, "Noclip: ^1OFF");
                mFlag(9, "0");
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }
            #endregion

            #region Package
            if (Client9BTN.Text == "67635460")
            {
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                Visions(9, "4112");
                GiveSecondaryWeapon(9, "1");
                GiveSecondaryWeaponAmmo(9, "1");
                GivePrimaryAmmo(9, "999999999");
                GivePrimaryAmmoLeft(9, "999999999");
                GiveSecondaryAmmo(9, "999999999");
                GiveSecondaryAkimbo(9, "999999999");
                GiveSecondaryAmmoLeft(9, "999999999");
                GiveFrags(9, "999999999");
                GiveFlashbangs(9, "999999999");
                GiveFlashbangs2(9, "999999999");
                GiveGrenade(9, "999999999");
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }

            if (Client9BTN.Text == "67635204")
            {
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                Visions(9, "4096");
                GiveSecondaryWeapon(9, "4");
                GiveSecondaryWeaponAmmo(9, "4");
                GivePrimaryAmmo(9, "0");
                GivePrimaryAmmoLeft(9, "300");
                GiveSecondaryAmmo(9, "0");
                GiveSecondaryAkimbo(9, "0");
                GiveSecondaryAmmoLeft(9, "300");
                GiveFrags(9, "1");
                GiveFlashbangs(9, "2");
                GiveFlashbangs2(9, "2");
                GiveGrenade(9, "2");
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
                WriteName(9, Client9.Text);
            }
            #endregion
        }

        private void C10Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client10Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client10BTN.Text = address0.ToString();

            #region Toggle God
            if (Client10BTN.Text == "67108868")
            {
                WriteName(10, "God Mode: ^2ON");
                WriteName(10, "God Mode: ^2ON");
                WriteName(10, "God Mode: ^2ON");
                WriteName(10, "God Mode: ^2ON");
                WriteName(10, "God Mode: ^2ON");
                Health(10, "0");
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }

            if (Client10BTN.Text == "67109124")
            {
                WriteName(10, "God Mode: ^1OFF");
                WriteName(10, "God Mode: ^1OFF");
                WriteName(10, "God Mode: ^1OFF");
                WriteName(10, "God Mode: ^1OFF");
                WriteName(10, "God Mode: ^1OFF");
                Health(10, "100");
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }
            #endregion

            #region Noclip
            if (Client10BTN.Text == "32768")
            {
                WriteName(10, "Noclip: ^2ON");
                WriteName(10, "Noclip: ^2ON");
                WriteName(10, "Noclip: ^2ON");
                WriteName(10, "Noclip: ^2ON");
                WriteName(10, "Noclip: ^2ON");
                mFlag(10, "1");
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }

            if (Client10BTN.Text == "16384")
            {
                WriteName(10, "Noclip: ^1OFF");
                WriteName(10, "Noclip: ^1OFF");
                WriteName(10, "Noclip: ^1OFF");
                WriteName(10, "Noclip: ^1OFF");
                WriteName(10, "Noclip: ^1OFF");
                mFlag(10, "0");
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }
            #endregion

            #region Package
            if (Client10BTN.Text == "67635460")
            {
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                Visions(10, "4112");
                GiveSecondaryWeapon(10, "1");
                GiveSecondaryWeaponAmmo(10, "1");
                GivePrimaryAmmo(10, "999999999");
                GivePrimaryAmmoLeft(10, "999999999");
                GiveSecondaryAmmo(10, "999999999");
                GiveSecondaryAkimbo(10, "999999999");
                GiveSecondaryAmmoLeft(10, "999999999");
                GiveFrags(10, "999999999");
                GiveFlashbangs(10, "999999999");
                GiveFlashbangs2(10, "999999999");
                GiveGrenade(10, "999999999");
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }

            if (Client10BTN.Text == "67635204")
            {
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                Visions(10, "4096");
                GiveSecondaryWeapon(10, "4");
                GiveSecondaryWeaponAmmo(10, "4");
                GivePrimaryAmmo(10, "0");
                GivePrimaryAmmoLeft(10, "300");
                GiveSecondaryAmmo(10, "0");
                GiveSecondaryAkimbo(10, "0");
                GiveSecondaryAmmoLeft(10, "300");
                GiveFrags(10, "1");
                GiveFlashbangs(10, "2");
                GiveFlashbangs2(10, "2");
                GiveGrenade(10, "2");
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
                WriteName(10, Client10.Text);
            }
            #endregion
        }

        private void C11Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client11Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client11BTN.Text = address0.ToString();

            #region Toggle God
            if (Client11BTN.Text == "67108868")
            {
                WriteName(11, "God Mode: ^2ON");
                WriteName(11, "God Mode: ^2ON");
                WriteName(11, "God Mode: ^2ON");
                WriteName(11, "God Mode: ^2ON");
                WriteName(11, "God Mode: ^2ON");
                Health(11, "0");
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }

            if (Client11BTN.Text == "67109124")
            {
                WriteName(11, "God Mode: ^1OFF");
                WriteName(11, "God Mode: ^1OFF");
                WriteName(11, "God Mode: ^1OFF");
                WriteName(11, "God Mode: ^1OFF");
                WriteName(11, "God Mode: ^1OFF");
                Health(11, "100");
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }
            #endregion

            #region Noclip
            if (Client11BTN.Text == "32768")
            {
                WriteName(11, "Noclip: ^2ON");
                WriteName(11, "Noclip: ^2ON");
                WriteName(11, "Noclip: ^2ON");
                WriteName(11, "Noclip: ^2ON");
                WriteName(11, "Noclip: ^2ON");
                mFlag(11, "1");
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }

            if (Client11BTN.Text == "16384")
            {
                WriteName(11, "Noclip: ^1OFF");
                WriteName(11, "Noclip: ^1OFF");
                WriteName(11, "Noclip: ^1OFF");
                WriteName(11, "Noclip: ^1OFF");
                WriteName(11, "Noclip: ^1OFF");
                mFlag(11, "0");
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }
            #endregion

            #region Package
            if (Client11BTN.Text == "67635460")
            {
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                Visions(11, "4112");
                GiveSecondaryWeapon(11, "1");
                GiveSecondaryWeaponAmmo(11, "1");
                GivePrimaryAmmo(11, "999999999");
                GivePrimaryAmmoLeft(11, "999999999");
                GiveSecondaryAmmo(11, "999999999");
                GiveSecondaryAkimbo(11, "999999999");
                GiveSecondaryAmmoLeft(11, "999999999");
                GiveFrags(11, "999999999");
                GiveFlashbangs(11, "999999999");
                GiveFlashbangs2(11, "999999999");
                GiveGrenade(11, "999999999");
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }

            if (Client11BTN.Text == "67635204")
            {
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                Visions(11, "4096");
                GiveSecondaryWeapon(11, "4");
                GiveSecondaryWeaponAmmo(11, "4");
                GivePrimaryAmmo(11, "0");
                GivePrimaryAmmoLeft(11, "300");
                GiveSecondaryAmmo(11, "0");
                GiveSecondaryAkimbo(11, "0");
                GiveSecondaryAmmoLeft(11, "300");
                GiveFrags(11, "1");
                GiveFlashbangs(11, "2");
                GiveFlashbangs2(11, "2");
                GiveGrenade(11, "2");
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
                WriteName(11, Client11.Text);
            }
            #endregion
        }

        private void C12Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client12Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client12BTN.Text = address0.ToString();

            #region Toggle God
            if (Client12BTN.Text == "67108868")
            {
                WriteName(12, "God Mode: ^2ON");
                WriteName(12, "God Mode: ^2ON");
                WriteName(12, "God Mode: ^2ON");
                WriteName(12, "God Mode: ^2ON");
                WriteName(12, "God Mode: ^2ON");
                Health(12, "0");
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }

            if (Client12BTN.Text == "67109124")
            {
                WriteName(12, "God Mode: ^1OFF");
                WriteName(12, "God Mode: ^1OFF");
                WriteName(12, "God Mode: ^1OFF");
                WriteName(12, "God Mode: ^1OFF");
                WriteName(12, "God Mode: ^1OFF");
                Health(12, "100");
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }
            #endregion

            #region Noclip
            if (Client12BTN.Text == "32768")
            {
                WriteName(12, "Noclip: ^2ON");
                WriteName(12, "Noclip: ^2ON");
                WriteName(12, "Noclip: ^2ON");
                WriteName(12, "Noclip: ^2ON");
                WriteName(12, "Noclip: ^2ON");
                mFlag(12, "1");
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }

            if (Client12BTN.Text == "16384")
            {
                WriteName(12, "Noclip: ^1OFF");
                WriteName(12, "Noclip: ^1OFF");
                WriteName(12, "Noclip: ^1OFF");
                WriteName(12, "Noclip: ^1OFF");
                WriteName(12, "Noclip: ^1OFF");
                mFlag(12, "0");
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }
            #endregion

            #region Package
            if (Client12BTN.Text == "67635460")
            {
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                Visions(12, "4112");
                GiveSecondaryWeapon(12, "1");
                GiveSecondaryWeaponAmmo(12, "1");
                GivePrimaryAmmo(12, "999999999");
                GivePrimaryAmmoLeft(12, "999999999");
                GiveSecondaryAmmo(12, "999999999");
                GiveSecondaryAkimbo(12, "999999999");
                GiveSecondaryAmmoLeft(12, "999999999");
                GiveFrags(12, "999999999");
                GiveFlashbangs(12, "999999999");
                GiveFlashbangs2(12, "999999999");
                GiveGrenade(12, "999999999");
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }

            if (Client12BTN.Text == "67635204")
            {
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                Visions(12, "4096");
                GiveSecondaryWeapon(12, "4");
                GiveSecondaryWeaponAmmo(12, "4");
                GivePrimaryAmmo(12, "0");
                GivePrimaryAmmoLeft(12, "300");
                GiveSecondaryAmmo(12, "0");
                GiveSecondaryAkimbo(12, "0");
                GiveSecondaryAmmoLeft(12, "300");
                GiveFrags(12, "1");
                GiveFlashbangs(12, "2");
                GiveFlashbangs2(12, "2");
                GiveGrenade(12, "2");
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
                WriteName(12, Client12.Text);
            }
            #endregion
        }

        private void C13Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client13Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client13BTN.Text = address0.ToString();

            #region Toggle God
            if (Client13BTN.Text == "67108868")
            {
                WriteName(13, "God Mode: ^2ON");
                WriteName(13, "God Mode: ^2ON");
                WriteName(13, "God Mode: ^2ON");
                WriteName(13, "God Mode: ^2ON");
                WriteName(13, "God Mode: ^2ON");
                Health(13, "0");
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }

            if (Client13BTN.Text == "67109124")
            {
                WriteName(13, "God Mode: ^1OFF");
                WriteName(13, "God Mode: ^1OFF");
                WriteName(13, "God Mode: ^1OFF");
                WriteName(13, "God Mode: ^1OFF");
                WriteName(13, "God Mode: ^1OFF");
                Health(13, "100");
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }
            #endregion

            #region Noclip
            if (Client13BTN.Text == "32768")
            {
                WriteName(13, "Noclip: ^2ON");
                WriteName(13, "Noclip: ^2ON");
                WriteName(13, "Noclip: ^2ON");
                WriteName(13, "Noclip: ^2ON");
                WriteName(13, "Noclip: ^2ON");
                mFlag(13, "1");
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }

            if (Client13BTN.Text == "16384")
            {
                WriteName(13, "Noclip: ^1OFF");
                WriteName(13, "Noclip: ^1OFF");
                WriteName(13, "Noclip: ^1OFF");
                WriteName(13, "Noclip: ^1OFF");
                WriteName(13, "Noclip: ^1OFF");
                mFlag(13, "0");
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }
            #endregion

            #region Package
            if (Client13BTN.Text == "67635460")
            {
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                Visions(13, "4112");
                GiveSecondaryWeapon(13, "1");
                GiveSecondaryWeaponAmmo(13, "1");
                GivePrimaryAmmo(13, "999999999");
                GivePrimaryAmmoLeft(13, "999999999");
                GiveSecondaryAmmo(13, "999999999");
                GiveSecondaryAkimbo(13, "999999999");
                GiveSecondaryAmmoLeft(13, "999999999");
                GiveFrags(13, "999999999");
                GiveFlashbangs(13, "999999999");
                GiveFlashbangs2(13, "999999999");
                GiveGrenade(13, "999999999");
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }

            if (Client13BTN.Text == "67635204")
            {
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                Visions(13, "4096");
                GiveSecondaryWeapon(13, "4");
                GiveSecondaryWeaponAmmo(13, "4");
                GivePrimaryAmmo(13, "0");
                GivePrimaryAmmoLeft(13, "300");
                GiveSecondaryAmmo(13, "0");
                GiveSecondaryAkimbo(13, "0");
                GiveSecondaryAmmoLeft(13, "300");
                GiveFrags(13, "1");
                GiveFlashbangs(13, "2");
                GiveFlashbangs2(13, "2");
                GiveGrenade(13, "2");
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
                WriteName(13, Client13.Text);
            }
            #endregion
        }

        private void C14Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client14Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client14BTN.Text = address0.ToString();

            #region Toggle God
            if (Client14BTN.Text == "67108868")
            {
                WriteName(14, "God Mode: ^2ON");
                WriteName(14, "God Mode: ^2ON");
                WriteName(14, "God Mode: ^2ON");
                WriteName(14, "God Mode: ^2ON");
                WriteName(14, "God Mode: ^2ON");
                Health(14, "0");
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }

            if (Client14BTN.Text == "67109124")
            {
                WriteName(14, "God Mode: ^1OFF");
                WriteName(14, "God Mode: ^1OFF");
                WriteName(14, "God Mode: ^1OFF");
                WriteName(14, "God Mode: ^1OFF");
                WriteName(14, "God Mode: ^1OFF");
                Health(14, "100");
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }
            #endregion

            #region Noclip
            if (Client14BTN.Text == "32768")
            {
                WriteName(14, "Noclip: ^2ON");
                WriteName(14, "Noclip: ^2ON");
                WriteName(14, "Noclip: ^2ON");
                WriteName(14, "Noclip: ^2ON");
                WriteName(14, "Noclip: ^2ON");
                mFlag(14, "1");
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }

            if (Client14BTN.Text == "16384")
            {
                WriteName(14, "Noclip: ^1OFF");
                WriteName(14, "Noclip: ^1OFF");
                WriteName(14, "Noclip: ^1OFF");
                WriteName(14, "Noclip: ^1OFF");
                WriteName(14, "Noclip: ^1OFF");
                mFlag(14, "0");
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }
            #endregion

            #region Package
            if (Client14BTN.Text == "67635460")
            {
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                Visions(14, "4112");
                GiveSecondaryWeapon(14, "1");
                GiveSecondaryWeaponAmmo(14, "1");
                GivePrimaryAmmo(14, "999999999");
                GivePrimaryAmmoLeft(14, "999999999");
                GiveSecondaryAmmo(14, "999999999");
                GiveSecondaryAkimbo(14, "999999999");
                GiveSecondaryAmmoLeft(14, "999999999");
                GiveFrags(14, "999999999");
                GiveFlashbangs(14, "999999999");
                GiveFlashbangs2(14, "999999999");
                GiveGrenade(14, "999999999");
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }

            if (Client14BTN.Text == "67635204")
            {
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                Visions(14, "4096");
                GiveSecondaryWeapon(14, "4");
                GiveSecondaryWeaponAmmo(14, "4");
                GivePrimaryAmmo(14, "0");
                GivePrimaryAmmoLeft(14, "300");
                GiveSecondaryAmmo(14, "0");
                GiveSecondaryAkimbo(14, "0");
                GiveSecondaryAmmoLeft(14, "300");
                GiveFrags(14, "1");
                GiveFlashbangs(14, "2");
                GiveFlashbangs2(14, "2");
                GiveGrenade(14, "2");
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
                WriteName(14, Client14.Text);
            }
            #endregion
        }

        private void C15Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client15Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client15BTN.Text = address0.ToString();

            #region Toggle God
            if (Client15BTN.Text == "67108868")
            {
                WriteName(15, "God Mode: ^2ON");
                WriteName(15, "God Mode: ^2ON");
                WriteName(15, "God Mode: ^2ON");
                WriteName(15, "God Mode: ^2ON");
                WriteName(15, "God Mode: ^2ON");
                Health(15, "0");
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }

            if (Client15BTN.Text == "67109124")
            {
                WriteName(15, "God Mode: ^1OFF");
                WriteName(15, "God Mode: ^1OFF");
                WriteName(15, "God Mode: ^1OFF");
                WriteName(15, "God Mode: ^1OFF");
                WriteName(15, "God Mode: ^1OFF");
                Health(15, "100");
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }
            #endregion

            #region Noclip
            if (Client15BTN.Text == "32768")
            {
                WriteName(15, "Noclip: ^2ON");
                WriteName(15, "Noclip: ^2ON");
                WriteName(15, "Noclip: ^2ON");
                WriteName(15, "Noclip: ^2ON");
                WriteName(15, "Noclip: ^2ON");
                mFlag(15, "1");
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }

            if (Client15BTN.Text == "16384")
            {
                WriteName(15, "Noclip: ^1OFF");
                WriteName(15, "Noclip: ^1OFF");
                WriteName(15, "Noclip: ^1OFF");
                WriteName(15, "Noclip: ^1OFF");
                WriteName(15, "Noclip: ^1OFF");
                mFlag(15, "0");
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }
            #endregion

            #region Package
            if (Client15BTN.Text == "67635460")
            {
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                Visions(15, "4112");
                GiveSecondaryWeapon(15, "1");
                GiveSecondaryWeaponAmmo(15, "1");
                GivePrimaryAmmo(15, "999999999");
                GivePrimaryAmmoLeft(15, "999999999");
                GiveSecondaryAmmo(15, "999999999");
                GiveSecondaryAkimbo(15, "999999999");
                GiveSecondaryAmmoLeft(15, "999999999");
                GiveFrags(15, "999999999");
                GiveFlashbangs(15, "999999999");
                GiveFlashbangs2(15, "999999999");
                GiveGrenade(15, "999999999");
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }

            if (Client15BTN.Text == "67635204")
            {
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                Visions(15, "4096");
                GiveSecondaryWeapon(15, "4");
                GiveSecondaryWeaponAmmo(15, "4");
                GivePrimaryAmmo(15, "0");
                GivePrimaryAmmoLeft(15, "300");
                GiveSecondaryAmmo(15, "0");
                GiveSecondaryAkimbo(15, "0");
                GiveSecondaryAmmoLeft(15, "300");
                GiveFrags(15, "1");
                GiveFlashbangs(15, "2");
                GiveFlashbangs2(15, "2");
                GiveGrenade(15, "2");
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
                WriteName(15, Client15.Text);
            }
            #endregion
        }

        private void C16Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client16Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client16BTN.Text = address0.ToString();

            #region Toggle God
            if (Client16BTN.Text == "67108868")
            {
                WriteName(16, "God Mode: ^2ON");
                WriteName(16, "God Mode: ^2ON");
                WriteName(16, "God Mode: ^2ON");
                WriteName(16, "God Mode: ^2ON");
                WriteName(16, "God Mode: ^2ON");
                Health(16, "0");
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }

            if (Client16BTN.Text == "67109124")
            {
                WriteName(16, "God Mode: ^1OFF");
                WriteName(16, "God Mode: ^1OFF");
                WriteName(16, "God Mode: ^1OFF");
                WriteName(16, "God Mode: ^1OFF");
                WriteName(16, "God Mode: ^1OFF");
                Health(16, "100");
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }
            #endregion

            #region Noclip
            if (Client16BTN.Text == "32768")
            {
                WriteName(16, "Noclip: ^2ON");
                WriteName(16, "Noclip: ^2ON");
                WriteName(16, "Noclip: ^2ON");
                WriteName(16, "Noclip: ^2ON");
                WriteName(16, "Noclip: ^2ON");
                mFlag(16, "1");
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }

            if (Client16BTN.Text == "16384")
            {
                WriteName(16, "Noclip: ^1OFF");
                WriteName(16, "Noclip: ^1OFF");
                WriteName(16, "Noclip: ^1OFF");
                WriteName(16, "Noclip: ^1OFF");
                WriteName(16, "Noclip: ^1OFF");
                mFlag(16, "0");
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }
            #endregion

            #region Package
            if (Client16BTN.Text == "67635460")
            {
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                Visions(16, "4112");
                GiveSecondaryWeapon(16, "1");
                GiveSecondaryWeaponAmmo(16, "1");
                GivePrimaryAmmo(16, "999999999");
                GivePrimaryAmmoLeft(16, "999999999");
                GiveSecondaryAmmo(16, "999999999");
                GiveSecondaryAkimbo(16, "999999999");
                GiveSecondaryAmmoLeft(16, "999999999");
                GiveFrags(16, "999999999");
                GiveFlashbangs(16, "999999999");
                GiveFlashbangs2(16, "999999999");
                GiveGrenade(16, "999999999");
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }

            if (Client16BTN.Text == "67635204")
            {
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                Visions(16, "4096");
                GiveSecondaryWeapon(16, "4");
                GiveSecondaryWeaponAmmo(16, "4");
                GivePrimaryAmmo(16, "0");
                GivePrimaryAmmoLeft(16, "300");
                GiveSecondaryAmmo(16, "0");
                GiveSecondaryAkimbo(16, "0");
                GiveSecondaryAmmoLeft(16, "300");
                GiveFrags(16, "1");
                GiveFlashbangs(16, "2");
                GiveFlashbangs2(16, "2");
                GiveGrenade(16, "2");
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
                WriteName(16, Client16.Text);
            }
            #endregion
        }

        private void C17Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client17Button);
            Memorys mem = new Memorys("iw5mp");
            uint ClientButton = mem.baseaddress("iw5mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client17BTN.Text = address0.ToString();

            #region Toggle God
            if (Client17BTN.Text == "67108868")
            {
                WriteName(17, "God Mode: ^2ON");
                WriteName(17, "God Mode: ^2ON");
                WriteName(17, "God Mode: ^2ON");
                WriteName(17, "God Mode: ^2ON");
                WriteName(17, "God Mode: ^2ON");
                Health(17, "0");
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }

            if (Client17BTN.Text == "67109124")
            {
                WriteName(17, "God Mode: ^1OFF");
                WriteName(17, "God Mode: ^1OFF");
                WriteName(17, "God Mode: ^1OFF");
                WriteName(17, "God Mode: ^1OFF");
                WriteName(17, "God Mode: ^1OFF");
                Health(17, "100");
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }
            #endregion

            #region Noclip
            if (Client17BTN.Text == "32768")
            {
                WriteName(17, "Noclip: ^2ON");
                WriteName(17, "Noclip: ^2ON");
                WriteName(17, "Noclip: ^2ON");
                WriteName(17, "Noclip: ^2ON");
                WriteName(17, "Noclip: ^2ON");
                mFlag(17, "1");
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }

            if (Client17BTN.Text == "16384")
            {
                WriteName(17, "Noclip: ^1OFF");
                WriteName(17, "Noclip: ^1OFF");
                WriteName(17, "Noclip: ^1OFF");
                WriteName(17, "Noclip: ^1OFF");
                WriteName(17, "Noclip: ^1OFF");
                mFlag(17, "0");
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }
            #endregion

            #region Package
            if (Client17BTN.Text == "67635460")
            {
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                Visions(17, "4112");
                GiveSecondaryWeapon(17, "1");
                GiveSecondaryWeaponAmmo(17, "1");
                GivePrimaryAmmo(17, "999999999");
                GivePrimaryAmmoLeft(17, "999999999");
                GiveSecondaryAmmo(17, "999999999");
                GiveSecondaryAkimbo(17, "999999999");
                GiveSecondaryAmmoLeft(17, "999999999");
                GiveFrags(17, "999999999");
                GiveFlashbangs(17, "999999999");
                GiveFlashbangs2(17, "999999999");
                GiveGrenade(17, "999999999");
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }

            if (Client17BTN.Text == "67635204")
            {
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                Visions(17, "4096");
                GiveSecondaryWeapon(17, "4");
                GiveSecondaryWeaponAmmo(17, "4");
                GivePrimaryAmmo(17, "0");
                GivePrimaryAmmoLeft(17, "300");
                GiveSecondaryAmmo(17, "0");
                GiveSecondaryAkimbo(17, "0");
                GiveSecondaryAmmoLeft(17, "300");
                GiveFrags(17, "1");
                GiveFlashbangs(17, "2");
                GiveFlashbangs2(17, "2");
                GiveGrenade(17, "2");
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
                WriteName(17, Client17.Text);
            }
            #endregion
        }

        private void Prestige11_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("11"));
        }

        private void Prestige12_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("12"));
        }

        private void Prestige13_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("13"));
        }

        private void Prestige14_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("14"));
        }

        private void Prestige15_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("15"));
        }

        private void Prestige16_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("16"));
        }

        private void Prestige17_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("17"));
        }

        private void Prestige18_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("18"));
        }

        private void Prestige19_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("19"));
        }

        private void Prestige20_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("20"));
        }

        private void DoubleXP_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.DoubleXP);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("11059200"));
        }

        private void DoubleWeaponXP_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.DoubleWeaponXP);
            Memorys mem = new Memorys("iw5mp");
            uint AmmoTest = mem.baseaddress("iw5mp") + Value;
            mem.Write(AmmoTest, int.Parse("11059200"));
        }

        private void MapName_Tick(object sender, EventArgs e)
        {
            Map();

            if (MapNameTXT.Text == "mp_dome")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_mogadishu")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_carbon")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_hardhat")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_lambeth")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_radar")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_interchange")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_terminal_ss")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_seatown")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_plaza2")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_paris")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_exchange")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_bootleg")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_alpha")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_village")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_underground")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_bravo")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_courtyard_ss")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_aground_ss")
            {
                MapSide.Text = "Map Side B";
            }
        }


    }
}
