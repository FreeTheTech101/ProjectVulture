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
    public partial class MW2Window : DevComponents.DotNetBar.Metro.MetroForm
    {

        public static Encoding Encoding = Encoding.UTF8;
        
        public MW2Window()
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
            PlayerState1 = 0x366C,
            PlayerState2 = 0x274,
            ClassPointer = 0x40,
            NameOffset = 0x01AA1A2C,
            mFlagOffset = 0x01AA1BA4,
            HealthOffset = 0x018DC174,
            SteadyAim = 0x01A9EAE0,
            Vision = 0x01A9E820,
            MapName = 0x014C8680,
            SteadyOFF = 0x007F5B88,
            SteadyOFF1 = 0x007F8F00,
            SteadyOFF2 = 0x007F8C40,
            SteadyOFF3 = 0x0082C8EC,
            SteadyOFF4 = 0x00B33AE0,
            SteadyOFF5 = 0x00B3C2B4,
            SteadyOFF6 = 0x00B3F400,
            SteadyOFF7 = 0x00B4254C,
            SteadyOFF8 = 0x00B45698,
            SteadyOFF9 = 0x00B487E4,
            SteadyOFF10 = 0x00B4B930,
            SteadyOFF11 = 0x00B51BC8,
            SteadyOFF12 = 0x00B54D14,
            SteadyOFF13 = 0x00B57E60,
            SteadyOFF14 = 0x00B5AFAC,
            SteadyOFF15 = 0x00B5E0F8,
            SteadyOFF16 = 0x00B61244,
            SteadyOFF17 = 0x00B64390,
            SteadyOFF18 = 0x00B674DC,
            SteadyOFF19 = 0x00B6A628,
            SteadyOFF20 = 0x00B6D774,
            SteadyOFF21 = 0x00B708C0,
            SteadyOFF22 = 0x00B73A0C,
            SteadyOFF23 = 0x00B76B58,
            SteadyOFF24 = 0x00B79CA4,
            SteadyOFF25 = 0x00B7CDF0,
            SteadyOFF26 = 0x00B7FF3C,
            SteadyOFF27 = 0x00B83088,
            SteadyOFF28 = 0x00B861D4,
            SteadyOFF29 = 0x00B89320,
            SteadyOFF30 = 0x00B8C46C,
            SteadyOFF31 = 0x00B8F5B8,
            SteadyOFF32 = 0x00B92704,
            SteadyOFF33 = 0x00B95850,
            SteadyOFF34 = 0x00B9899C,
            SteadyOFF35 = 0x00B9BAE8,
            SteadyOFF36 = 0x00B4EA7C,
            RedBoxOFF = 0x009DC3B0,
            RedBoxOFF1 = 0x007F58C8,
            RedBoxOFF2 = 0x007F8C40,
            RedBoxOFF3 = 0x0082C62C,
            RedBoxOFF4 = 0x008F2BDC,
            RedBoxOFF5 = 0x00B33820,
            RedBoxOFF6 = 0x00B3BFF4,
            RedBoxOFF7 = 0x00B3F140,
            RedBoxOFF8 = 0x00B4228C,
            RedBoxOFF9 = 0x00B453D8,
            RedBoxOFF10 = 0x00B48524,
            RedBoxOFF11 = 0x00B4B670,
            RedBoxOFF12 = 0x00B4E7BC,
            RedBoxOFF13 = 0x00B51908,
            RedBoxOFF14 = 0x00B54A54,
            RedBoxOFF15 = 0x00B57BA0,
            RedBoxOFF16 = 0x00B5ACEC,
            RedBoxOFF17 = 0x00B5DE38,
            RedBoxOFF18 = 0x00B60F84,
            RedBoxOFF19 = 0x00B640D0,
            RedBoxOFF20 = 0x00B6721C,
            RedBoxOFF21 = 0x00B6A368,
            RedBoxOFF22 = 0x00B6D4B4,
            RedBoxOFF23 = 0x00B7374C,
            RedBoxOFF24 = 0x00B70600,
            RedBoxOFF25 = 0x00B76898,
            RedBoxOFF26 = 0x00B799E4,
            RedBoxOFF27 = 0x00B7CB30,
            RedBoxOFF28 = 0x00B7FC7C,
            RedBoxOFF29 = 0x00B82DC8,
            RedBoxOFF30 = 0x00B85F14,
            RedBoxOFF31 = 0x00B89060,
            RedBoxOFF32 = 0x00B8C1AC,
            RedBoxOFF33 = 0x00B8F2F8,
            RedBoxOFF34 = 0x00B92444,
            RedBoxOFF35 = 0x00B95590,
            RedBoxOFF36 = 0x00B986DC,
            RedBoxOFF37 = 0x00B9B828,
            NightVision = 0x01A9EC6C,
            Recoil = 0x01A9EACC,
            Jump = 0x064720E0,
            Fall = 0x0646FD40,
            Gravity = 0x06471C30,
            TimeScale = 0x06454D60,
            Speed1 = 0x06476E10,
            Speed2 = 0x06476E20,
            Speed3 = 0x06476E30,
            XAxis = 0x01A9E82C,
            YAxis = 0x01A9E834,
            ZAxis = 0x01A9E830,
            GoldD = 0x01B2CC70,
            CustomClass = 0x01B2CC74,
            SecondaryWeapon = 0x01A9EA30,
            SecondaryWeaponAmmo = 0x01A9EAE8,
            SwitchWeapon = 0x00000000,
            PrimaryInGunAmmo = 0x01A9EB7C,
            PrimaryLeftAmmo = 0x01A9EAFC,
            PrimaryAkimboInGun = 0x01A9EB80,
            Flashbangs = 0x01A9EB88,
            Flashbangsv2 = 0x01A9EB94,
            Frags = 0x01A9EB70,
            SecondaryInGunAmmo = 0x01A9EB64,
            SecondaryLeftAmmo = 0x01A9EAEC,
            SecondaryAkimboInGun = 0x01A9EB68,
            GrenadeLauncher = 0x01A9EB88,
            Prestige = 0x01B2C8A4,
            XP = 0x01B2C89C,
            Score = 0x01B2C8AC,
            Wins = 0x01B2C8E4,
            Losses = 0x01B2C8E8,
            Ties = 0x01B2C8EC,
            WinStreak = 0x01B2C8F0,
            Kills = 0x01B2C8B0,
            Deaths = 0x01B2C8B8,
            Headshots = 0x01B2C8C4,
            Assists = 0x01B2C8C0,
            Killstreak = 0x01B2C8B4,
            TimePlayed = 0x01B2C8D0,
            UnlockAll = 0x01B2CED0,
            Client0Button = 0x01AA1960,
            Client1Button = 0x01AA4FCC,
            Client2Button = 0x01AA8638,
            Client3Button = 0x01AABCA4,
            Client4Button = 0x01AAF310,
            Client5Button = 0x01AB297C,
            Client6Button = 0x01AB5FE8,
            Client7Button = 0x01AB9654,
            Client8Button = 0x01ABCCC0,
            Client9Button = 0x01AC032C,
            Client10Button = 0x01AC3998,
            Client11Button = 0x01AC7004,
            Client12Button = 0x01ACA670,
            Client13Button = 0x01ACDCDC,
            Client14Button = 0x01AD1348,
            Client15Button = 0x01AD49B4,
            Client16Button = 0x01AD8020,
            Client17Button = 0x01ADB68C,
            Client0Offset = 0x01AA1A2C,
            Client1Offset = 0x01AA5098,
            Client2Offset = 0x01AA8704,
            Client3Offset = 0x01AABD70,
            Client4Offset = 0x01AAF3DC,
            Client5Offset = 0x01AB2A48,
            Client6Offset = 0x01AB60B4,
            Client7Offset = 0x01AB9720,
            Client8Offset = 0x01ABCD8C,
            Client9Offset = 0x01AC03F8,
            Client10Offset = 0x01AC3A64,
            Client11Offset = 0x01AC70D0,
            Client12Offset = 0x01ACA73C,
            Client13Offset = 0x01ACDDA8,
            Client14Offset = 0x01AD1414,
            Client15Offset = 0x01AD4A80,
            Client16Offset = 0x01AD80EC,
            Client17Offset = 0x01ADB758;
        }

        private void ReadClient0()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client0Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client0.Text = nickz.ToString();
            }
        }

        private void ReadClient1()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client1Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client1.Text = nickz.ToString();
            }
        }

        private void ReadClient2()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client2Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client2.Text = nickz.ToString();
            }
        }

        private void ReadClient3()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client3Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client3.Text = nickz.ToString();
            }
        }

        private void ReadClient4()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client4Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client4.Text = nickz.ToString();
            }
        }

        private void ReadClient5()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client5Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client5.Text = nickz.ToString();
            }
        }

        private void ReadClient6()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client6Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client6.Text = nickz.ToString();
            }
        }

        private void ReadClient7()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client7Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client7.Text = nickz.ToString();
            }
        }

        private void ReadClient8()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client8Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client8.Text = nickz.ToString();
            }
        }

        private void ReadClient9()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client9Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client9.Text = nickz.ToString();
            }
        }

        private void ReadClient10()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client10Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client10.Text = nickz.ToString();
            }
        }

        private void ReadClient11()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client11Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client11.Text = nickz.ToString();
            }
        }

        private void ReadClient12()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client12Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client12.Text = nickz.ToString();
            }
        }

        private void ReadClient13()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client13Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client13.Text = nickz.ToString();
            }
        }

        private void ReadClient14()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client14Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client14.Text = nickz.ToString();
            }
        }

        private void ReadClient15()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client15Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client15.Text = nickz.ToString();
            }
        }

        private void ReadClient16()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client16Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client16.Text = nickz.ToString();
            }
        }

        private void ReadClient17()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.Client17Offset;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                Client17.Text = nickz.ToString();
            }
        }

        private void Health(int ClientID, string HealthPercentage)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.HealthOffset;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState2 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(HealthPercentage));
        }

        private void Visions(int ClientID, string VisionValue)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Vision;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(VisionValue));
        }

        private void Speed(string SpeedValue)
        {
            uint Offset1 = Convert.ToUInt32(GameAddresses.Speed1);
            uint Offset2 = Convert.ToUInt32(GameAddresses.Speed2);
            uint Offset3 = Convert.ToUInt32(GameAddresses.Speed3);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Offset1;
            mem.Write(AmmoTest, int.Parse(SpeedValue));
            uint AmmoTest1 = mem.baseaddress("iw4mp") + Offset2;
            mem.Write(AmmoTest, int.Parse(SpeedValue));
            uint AmmoTest2 = mem.baseaddress("iw4mp") + Offset3;
            mem.Write(AmmoTest, int.Parse(SpeedValue));
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

        private void GiveSteadyAim(int ClientID, string AimValue)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SteadyAim;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(AimValue));
        }

        private void GiveNoRecoil(int ClientID, string RecoilValue)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Recoil;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(RecoilValue));
        }

        private void GiveNightVision(int ClientID, string GoggleValue)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.NightVision;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(GoggleValue));
        }

        private void GiveSecondaryWeaponAmmo(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryWeaponAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void GiveSecondaryWeapon(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryWeapon;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void PlayerSwitchToWeapon(int ClientID, string WeaponID)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SwitchWeapon;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(WeaponID));
        }

        private void GivePrimaryAmmo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryInGunAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GivePrimaryAmmoLeft(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryLeftAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveSecondaryAmmoLeft(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryLeftAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveSecondaryAmmo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryInGunAmmo;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveFlashbangs(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Flashbangs;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveFlashbangs2(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Flashbangsv2;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveFrags(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.Frags;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveSecondaryAkimbo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.SecondaryAkimboInGun;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GivePrimaryAkimbo(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.PrimaryAkimboInGun;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        private void GiveGrenade(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.GrenadeLauncher;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        public void Map()
        {
            if (Process_Handle("iw4mp"))
            {
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + GameAddresses.MapName;
                var nick = ReadBytes(address, 20);

                string nickz;
                nickz = Encoding.GetString(nick).Replace("\r", "");
                MapNameTXT.Text = nickz.ToString();
            }
        }

        private void mFlag(int ClientID, string mFlag)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.mFlagOffset;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(mFlag));
        }

        private void GiveGold(int ClientID, string Ammo)
        {
            int Client = ClientID;
            long Client0Offset = GameAddresses.GoldD;
            uint AllClientBaby = Convert.ToUInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + AllClientBaby;
            mem.Write(AmmoTest, int.Parse(Ammo));
        }

        public void WriteName(int ClientID, string Name)
        {
            if (Process_Handle("iw4mp"))
            {
                int Client = ClientID;
                long Client0Offset = GameAddresses.NameOffset;
                int AllClientBaby = Convert.ToInt32(Client0Offset + (GameAddresses.PlayerState1 * Client));
                byte[] nickbyte = Encoding.GetBytes(Name + "\0");
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + AllClientBaby;
                WriteBytes(address, nickbyte);
            }
        }

        public void CustomClassNames(int ClassID, string Name)
        {
            if (Process_Handle("iw4mp"))
            {
                int Class = ClassID;
                long Class0Offset = GameAddresses.CustomClass;
                int AllClassesBaby = Convert.ToInt32(Class0Offset + (GameAddresses.ClassPointer * Class));
                byte[] nickbyte = Encoding.GetBytes(Name + "\0");
                Memorys mem = new Memorys("iw4mp");
                int address = (int)mem.baseaddress("iw4mp") + AllClassesBaby;
                WriteBytes(address, nickbyte);
            }
        }

        public void JumpH(float Value)
        {
            if (Process_Handle("iw4mp"))
            {
                Float(GameAddresses.Jump, Value);
            }
        }

        public void FallDam(float Value)
        {
            if (Process_Handle("iw4mp"))
            {
                Float(GameAddresses.Fall, Value);
            }
        }

        public void GravityGame(float Value)
        {
            if (Process_Handle("iw4mp"))
            {
                Float(GameAddresses.Gravity, Value);
            }
        }

        public void TimeScale(float Value)
        {
            if (Process_Handle("iw4mp"))
            {
                Float(GameAddresses.TimeScale, Value);
            }
        }

        private void RedBoxOFFHost0(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost1(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF1);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost2(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF2);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost3(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF3);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost4(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF4);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost5(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF5);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost6(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF6);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost7(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF7);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost8(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF8);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost9(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF9);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost10(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF10);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost11(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF11);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost12(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF12);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost13(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF13);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost14(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF14);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost15(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF15);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost16(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF16);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost17(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF17);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost18(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF18);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost19(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF19);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost20(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF20);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost21(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF21);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost22(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF22);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost23(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF23);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost24(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF24);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost25(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF25);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost26(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF26);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost27(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF27);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost28(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF28);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost29(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF29);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost30(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF30);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost31(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF31);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost32(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF32);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost33(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF33);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost34(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF34);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost35(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF35);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost36(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF36);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void RedBoxOFFHost37(string RedBox)
        {
            uint RED = Convert.ToUInt32(GameAddresses.RedBoxOFF37);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + RED;
            mem.Write(AmmoTest, int.Parse(RedBox));
        }

        private void SteadyOFFHost0(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost1(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF1);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost2(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF2);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost3(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF3);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost4(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF4);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost5(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF5);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost6(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF6);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost7(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF7);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost8(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF8);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost9(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF9);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost10(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF10);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost11(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF11);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost12(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF12);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost13(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF13);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost14(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF14);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost15(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF15);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost16(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF16);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost17(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF17);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost18(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF18);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost19(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF19);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost20(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF20);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost21(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF21);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost22(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF22);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost23(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF23);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost24(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF24);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost25(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF25);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost26(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF26);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost27(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF27);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost28(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF28);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost29(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF29);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost30(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF30);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost31(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF31);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost32(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF32);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost33(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF33);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost34(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF34);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost35(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF35);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void SteadyOFFHost36(string SteadyAim)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.SteadyOFF36);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(SteadyAim));
        }

        private void XP_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.XP);
            string ValueID = Convert.ToString(XPValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Score_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Score);
            string ValueID = Convert.ToString(ScoreValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Wins_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Wins);
            string ValueID = Convert.ToString(WinsValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Losses_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Losses);
            string ValueID = Convert.ToString(LossesValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Ties_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Ties);
            string ValueID = Convert.ToString(TiesValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void WinStreak_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.WinStreak);
            string ValueID = Convert.ToString(WinsStreakValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Kills_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Kills);
            string ValueID = Convert.ToString(KillsValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Deaths_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Deaths);
            string ValueID = Convert.ToString(DeathsValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Headshots_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Headshots);
            string ValueID = Convert.ToString(HeadshotsValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void Assists_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Assists);
            string ValueID = Convert.ToString(AssistsValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void KillStreak_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.Killstreak);
            string ValueID = Convert.ToString(KillStreakValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void TimePlayed_Click(object sender, EventArgs e)
        {
            uint OFFSET = Convert.ToUInt32(GameAddresses.TimePlayed);
            string ValueID = Convert.ToString(TimePlayedValue.Value);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + OFFSET;
            mem.Write(AmmoTest, int.Parse(ValueID));
        }

        private void NameChangerBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(NameChangerID.Value);
            WriteName(ClientID, NameChangerTXT.Text);
            WriteName(ClientID, NameChangerTXT.Text);
        }

        private void RefreshClientsBTN_Click(object sender, EventArgs e)
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
        }

        private void MW2Window_Load(object sender, EventArgs e)
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
            WebRequest request = WebRequest.Create("http://projectvulture.co.uk/program/updates/MW2"); //reading update log.
            WebResponse response = request.GetResponse(); // getting server response

            System.IO.StreamReader reader = new //stuff
            System.IO.StreamReader(response.GetResponseStream()); //stuff

            richTextBox1.Text = reader.ReadToEnd(); //reading data
        }

        private void MP_Tick(object sender, EventArgs e)
        {
            if (Process_Handle("iw4mp"))
            {

            }
            else
            {
                MP.Stop();
                MapName.Stop();
                MessageBox.Show("Please Make Sure MW2 Is Open Before Launching The Tool!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void GiveSecondaryWeaponBTN_Click(object sender, EventArgs e)
        {
            if (GiveWeaponSelect.Text == "Default Weapon" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "1");
                GiveSecondaryWeaponAmmo(ClientID, "1");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "Default Weapon" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "1");
                GiveSecondaryWeaponAmmo(ClientID, "1");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "PP2000" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "88");
                GiveSecondaryWeaponAmmo(ClientID, "56");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "PP2000" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "87");
                GiveSecondaryWeaponAmmo(ClientID, "55");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "G18" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "50");
                GiveSecondaryWeaponAmmo(ClientID, "39");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "G18" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "49");
                GiveSecondaryWeaponAmmo(ClientID, "38");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "M93" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "69");
                GiveSecondaryWeaponAmmo(ClientID, "46");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "M93" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "68");
                GiveSecondaryWeaponAmmo(ClientID, "45");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "TMP" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "107");
                GiveSecondaryWeaponAmmo(ClientID, "63");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "TMP" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "106");
                GiveSecondaryWeaponAmmo(ClientID, "62");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "Spas-12" && MapSide.Text == "Map Side A")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "925");
                GiveSecondaryWeaponAmmo(ClientID, "446");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }

            if (GiveWeaponSelect.Text == "Spas-12" && MapSide.Text == "Map Side B")
            {
                int ClientID = Convert.ToInt32(GiveWeaponID.Value);
                GiveSecondaryWeapon(ClientID, "924");
                GiveSecondaryWeaponAmmo(ClientID, "445");
                GiveSecondaryAmmo(ClientID, "0");
                GiveSecondaryAkimbo(ClientID, "0");
                GiveSecondaryAmmoLeft(ClientID, "300");
            }
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

        private void GiveAmmo_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveAmmoID.Value);
            GivePrimaryAmmo(ClientID, "999999999");
            GivePrimaryAkimbo(ClientID, "999999999");
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
            GivePrimaryAkimbo(ClientID, "0");
            GivePrimaryAmmoLeft(ClientID, "300");
            GiveSecondaryAmmo(ClientID, "0");
            GiveSecondaryAkimbo(ClientID, "0");
            GiveSecondaryAmmoLeft(ClientID, "300");
            GiveFrags(ClientID, "1");
            GiveFlashbangs(ClientID, "2");
            GiveFlashbangs2(ClientID, "2");
            GiveGrenade(ClientID, "2");
        }

        private void GiveSteady_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveSteadyID.Value);
            GiveSteadyAim(ClientID, "2");
        }

        private void TakeSteady_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TakeSteadyID.Value);
            GiveSteadyAim(ClientID, "0");
        }

        private void GiveNight_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveNightID.Value);
            GiveNightVision(ClientID, "3");
        }

        private void TakeNight_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TakeNightID.Value);
            GiveNightVision(ClientID, "0");
        }

        private void GiveNoRecoilBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(GiveNoRecoilID.Value);
            GiveNoRecoil(ClientID, "1024");
        }

        private void TakeNoRecoilBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TakeNoRecoilID.Value);
            GiveNoRecoil(ClientID, "0");
        }

        private void SendVisionBTN_Click(object sender, EventArgs e)
        {
            if (SendVisionOPS.Text == "Red Boxes Vision")
            {
                int ClientID = Convert.ToInt32(SendVisionID.Value);
                Visions(ClientID, "4112");
            }

            if (SendVisionOPS.Text == "Red Boxes & Thermal Vision")
            {
                int ClientID = Convert.ToInt32(SendVisionID.Value);
                Visions(ClientID, "23498776");
            }

            if (SendVisionOPS.Text == "Thermal Vision")
            {
                int ClientID = Convert.ToInt32(SendVisionID.Value);
                Visions(ClientID, "5096");
            }

            if (SendVisionOPS.Text == "Emp & Red Boxes Vision")
            {
                int ClientID = Convert.ToInt32(SendVisionID.Value);
                Visions(ClientID, "54629980");
            }

            if (SendVisionOPS.Text == "Normal Vision")
            {
                int ClientID = Convert.ToInt32(SendVisionID.Value);
                Visions(ClientID, "4096");
            }
        }

        private void JumpHeight_ValueChanged(object sender, EventArgs e)
        {
            if (JumpHeight.Value == 0)
            {
                JumpH(0);
                FallDam(128);
            }

            if (JumpHeight.Value == 1)
            {
                JumpH(39);
                FallDam(128);
            }

            if (JumpHeight.Value == 2)
            {
                JumpH(100);
                FallDam(10000);
            }

            if (JumpHeight.Value == 3)
            {
                JumpH(200);
                FallDam(10000);
            }

            if (JumpHeight.Value == 4)
            {
                JumpH(300);
                FallDam(10000);
            }

            if (JumpHeight.Value == 5)
            {
                JumpH(400);
                FallDam(10000);
            }

            if (JumpHeight.Value == 6)
            {
                JumpH(500);
                FallDam(10000);
            }

            if (JumpHeight.Value == 7)
            {
                JumpH(600);
                FallDam(10000);
            }

            if (JumpHeight.Value == 8)
            {
                JumpH(700);
                FallDam(10000);
            }

            if (JumpHeight.Value == 9)
            {
                JumpH(800);
                FallDam(10000);
            }

            if (JumpHeight.Value == 10)
            {
                JumpH(900);
                FallDam(10000);
            }
        }

        private void TeleportBTN_Click(object sender, EventArgs e)
        {
            int ClientID = Convert.ToInt32(TeleportID.Value);
            float XText = Convert.ToSingle(XAxisTXT.Text);
            float YText = Convert.ToSingle(YAxisTXT.Text);
            float ZText = Convert.ToSingle(ZAxisTXT.Text);
            Teleport(ClientID, XText, YText, ZText);
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

        private void SpeedSlider_ValueChanged(object sender, EventArgs e)
        {
            if (SpeedSlider.Value == 0)
            {
                Speed("10");
            }

            if (SpeedSlider.Value == 1)
            {
                Speed("100");
            }

            if (SpeedSlider.Value == 2)
            {
                Speed("190");
            }

            if (SpeedSlider.Value == 3)
            {
                Speed("200");
            }

            if (SpeedSlider.Value == 4)
            {
                Speed("300");
            }

            if (SpeedSlider.Value == 5)
            {
                Speed("400");
            }

            if (SpeedSlider.Value == 6)
            {
                Speed("500");
            }

            if (SpeedSlider.Value == 7)
            {
                Speed("600");
            }

            if (SpeedSlider.Value == 8)
            {
                Speed("700");
            }

            if (SpeedSlider.Value == 9)
            {
                Speed("800");
            }

            if (SpeedSlider.Value == 10)
            {
                Speed("900");
            }
        }

        private void GravitySlider_ValueChanged(object sender, EventArgs e)
        {
            if (GravitySlider.Value == 0)
            {
                GravityGame(1);
            }

            if (GravitySlider.Value == 1)
            {
                GravityGame(100);
            }

            if (GravitySlider.Value == 2)
            {
                GravityGame(200);
            }

            if (GravitySlider.Value == 3)
            {
                GravityGame(300);
            }

            if (GravitySlider.Value == 4)
            {
                GravityGame(400);
            }

            if (GravitySlider.Value == 5)
            {
                GravityGame(500);
            }

            if (GravitySlider.Value == 6)
            {
                GravityGame(600);
            }

            if (GravitySlider.Value == 7)
            {
                GravityGame(700);
            }

            if (GravitySlider.Value == 8)
            {
                GravityGame(800);
            }

            if (GravitySlider.Value == 9)
            {
                GravityGame(900);
            }

            if (GravitySlider.Value == 10)
            {
                GravityGame(1000);
            }
        }

        private void TimeScaleSlider_ValueChanged(object sender, EventArgs e)
        {
            if (TimeScaleSlider.Value == 0)
            {
                float Value = Convert.ToSingle(0.2);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 1)
            {
                float Value = Convert.ToSingle(0.4);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 2)
            {
                float Value = Convert.ToSingle(0.6);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 3)
            {
                float Value = Convert.ToSingle(0.8);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 4)
            {
                float Value = Convert.ToSingle(0.9);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 5)
            {
                float Value = Convert.ToSingle(1);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 6)
            {
                float Value = Convert.ToSingle(1.2);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 7)
            {
                float Value = Convert.ToSingle(1.4);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 8)
            {
                float Value = Convert.ToSingle(1.6);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 9)
            {
                float Value = Convert.ToSingle(1.8);
                TimeScale(Value);
            }

            if (TimeScaleSlider.Value == 10)
            {
                float Value = Convert.ToSingle(2);
                TimeScale(Value);
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client0BTN.Text = address0.ToString();

            #region Toggle God
            if (Client0BTN.Text == "4")
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

            if (Client0BTN.Text == "260")
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
            if (Client0BTN.Text == "526596")
            {
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                WriteName(0, "Package: ^2ON");
                GiveNoRecoil(0, "1024");
                GiveNightVision(0, "3");
                GiveSteadyAim(0, "2");
                Visions(0, "4112");
                GiveSecondaryWeapon(0, "1");
                GiveSecondaryWeaponAmmo(0, "1");
                GivePrimaryAmmo(0, "999999999");
                GivePrimaryAkimbo(0, "999999999");
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

            if (Client0BTN.Text == "526340")
            {
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                WriteName(0, "Package: ^1OFF");
                GiveNoRecoil(0, "0");
                GiveNightVision(0, "0");
                GiveSteadyAim(0, "0");
                Visions(0, "4096");
                GiveSecondaryWeapon(0, "4");
                GiveSecondaryWeaponAmmo(0, "4");
                GivePrimaryAmmo(0, "0");
                GivePrimaryAkimbo(0, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client1BTN.Text = address0.ToString();

            #region Toggle God
            if (Client1BTN.Text == "4")
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

            if (Client1BTN.Text == "260")
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
            if (Client1BTN.Text == "526596")
            {
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                WriteName(1, "Package: ^2ON");
                GiveNoRecoil(1, "1024");
                GiveNightVision(1, "3");
                GiveSteadyAim(1, "2");
                Visions(1, "4112");
                GiveSecondaryWeapon(1, "1");
                GiveSecondaryWeaponAmmo(1, "1");
                GivePrimaryAmmo(1, "999999999");
                GivePrimaryAkimbo(1, "999999999");
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

            if (Client1BTN.Text == "526340")
            {
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                WriteName(1, "Package: ^1OFF");
                GiveNoRecoil(1, "0");
                GiveNightVision(1, "0");
                GiveSteadyAim(1, "0");
                Visions(1, "4096");
                GiveSecondaryWeapon(1, "4");
                GiveSecondaryWeaponAmmo(1, "4");
                GivePrimaryAmmo(1, "0");
                GivePrimaryAkimbo(1, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client2BTN.Text = address0.ToString();

            #region Toggle God
            if (Client2BTN.Text == "4")
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

            if (Client2BTN.Text == "260")
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
            if (Client2BTN.Text == "526596")
            {
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                WriteName(2, "Package: ^2ON");
                GiveNoRecoil(2, "1024");
                GiveNightVision(2, "3");
                GiveSteadyAim(2, "2");
                Visions(2, "4112");
                GiveSecondaryWeapon(2, "1");
                GiveSecondaryWeaponAmmo(2, "1");
                GivePrimaryAmmo(2, "999999999");
                GivePrimaryAkimbo(2, "999999999");
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

            if (Client2BTN.Text == "526340")
            {
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                WriteName(2, "Package: ^1OFF");
                GiveNoRecoil(2, "0");
                GiveNightVision(2, "0");
                GiveSteadyAim(2, "0");
                Visions(2, "4096");
                GiveSecondaryWeapon(2, "4");
                GiveSecondaryWeaponAmmo(2, "4");
                GivePrimaryAmmo(2, "0");
                GivePrimaryAkimbo(2, "0");
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

        private void Prestige0_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("0"));
        }

        private void Prestige1_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("1"));
        }

        private void Prestige2_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("2"));
        }

        private void Prestige3_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("3"));
        }

        private void Prestige4_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("4"));
        }

        private void Prestige5_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("5"));
        }

        private void Prestige6_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("6"));
        }

        private void Prestige7_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("7"));
        }

        private void Prestige8_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("8"));
        }

        private void Prestige9_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("9"));
        }

        private void Prestige10_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("10"));
        }

        private void Prestige11_Click(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Prestige);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Value;
            mem.Write(AmmoTest, int.Parse("11"));
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

        private void MapName_Tick(object sender, EventArgs e)
        {
            Map();

            if (MapNameTXT.Text == "mp_afghan")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_highrise")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_checkpoint")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_quarry")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_rundown")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_nightshift")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_terminal")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_brecourt")
            {
                MapSide.Text = "Map Side A";
            }

            if (MapNameTXT.Text == "mp_derail")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_estate")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_favela")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_invasion")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_rust")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_boneyard")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_underpass")
            {
                MapSide.Text = "Map Side B";
            }

            if (MapNameTXT.Text == "mp_subbase")
            {
                MapSide.Text = "Map Side B";
            }
        }

        private void UnlockAll_Click(object sender, EventArgs e)
        {
            WriteBytes(GameAddresses.UnlockAll, new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            });
        }

        private void GOLDEAGLE_Click(object sender, EventArgs e)
        {
            GiveGold(0, "333855");
        }

        private void C3Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client3Button);
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client3BTN.Text = address0.ToString();

            #region Toggle God
            if (Client3BTN.Text == "4")
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

            if (Client3BTN.Text == "260")
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
            if (Client3BTN.Text == "526596")
            {
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                WriteName(3, "Package: ^2ON");
                GiveNoRecoil(3, "1024");
                GiveNightVision(3, "3");
                GiveSteadyAim(3, "2");
                Visions(3, "4112");
                GiveSecondaryWeapon(3, "1");
                GiveSecondaryWeaponAmmo(3, "1");
                GivePrimaryAmmo(3, "999999999");
                GivePrimaryAkimbo(3, "999999999");
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

            if (Client3BTN.Text == "526340")
            {
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                WriteName(3, "Package: ^1OFF");
                GiveNoRecoil(3, "0");
                GiveNightVision(3, "0");
                GiveSteadyAim(3, "0");
                Visions(3, "4096");
                GiveSecondaryWeapon(3, "4");
                GiveSecondaryWeaponAmmo(3, "4");
                GivePrimaryAmmo(3, "0");
                GivePrimaryAkimbo(3, "0");
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

        private void C4Bind_Tick(object sender, EventArgs e)
        {
            uint Value = Convert.ToUInt32(GameAddresses.Client4Button);
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client4BTN.Text = address0.ToString();

            #region Toggle God
            if (Client4BTN.Text == "4")
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

            if (Client4BTN.Text == "260")
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
            if (Client4BTN.Text == "526596")
            {
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                WriteName(4, "Package: ^2ON");
                GiveNoRecoil(4, "1024");
                GiveNightVision(4, "3");
                GiveSteadyAim(4, "2");
                Visions(4, "4112");
                GiveSecondaryWeapon(4, "1");
                GiveSecondaryWeaponAmmo(4, "1");
                GivePrimaryAmmo(4, "999999999");
                GivePrimaryAkimbo(4, "999999999");
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

            if (Client4BTN.Text == "526340")
            {
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                WriteName(4, "Package: ^1OFF");
                GiveNoRecoil(4, "0");
                GiveNightVision(4, "0");
                GiveSteadyAim(4, "0");
                Visions(4, "4096");
                GiveSecondaryWeapon(4, "4");
                GiveSecondaryWeaponAmmo(4, "4");
                GivePrimaryAmmo(4, "0");
                GivePrimaryAkimbo(4, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client5BTN.Text = address0.ToString();

            #region Toggle God
            if (Client5BTN.Text == "4")
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

            if (Client5BTN.Text == "260")
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
            if (Client5BTN.Text == "526596")
            {
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                WriteName(5, "Package: ^2ON");
                GiveNoRecoil(5, "1024");
                GiveNightVision(5, "3");
                GiveSteadyAim(5, "2");
                Visions(5, "4112");
                GiveSecondaryWeapon(5, "1");
                GiveSecondaryWeaponAmmo(5, "1");
                GivePrimaryAmmo(5, "999999999");
                GivePrimaryAkimbo(5, "999999999");
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

            if (Client5BTN.Text == "526340")
            {
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                WriteName(5, "Package: ^1OFF");
                GiveNoRecoil(5, "0");
                GiveNightVision(5, "0");
                GiveSteadyAim(5, "0");
                Visions(5, "4096");
                GiveSecondaryWeapon(5, "4");
                GiveSecondaryWeaponAmmo(5, "4");
                GivePrimaryAmmo(5, "0");
                GivePrimaryAkimbo(5, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client6BTN.Text = address0.ToString();

            #region Toggle God
            if (Client6BTN.Text == "4")
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

            if (Client6BTN.Text == "260")
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
            if (Client6BTN.Text == "526596")
            {
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                WriteName(6, "Package: ^2ON");
                GiveNoRecoil(6, "1024");
                GiveNightVision(6, "3");
                GiveSteadyAim(6, "2");
                Visions(6, "4112");
                GiveSecondaryWeapon(6, "1");
                GiveSecondaryWeaponAmmo(6, "1");
                GivePrimaryAmmo(6, "999999999");
                GivePrimaryAkimbo(6, "999999999");
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

            if (Client6BTN.Text == "526340")
            {
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                WriteName(6, "Package: ^1OFF");
                GiveNoRecoil(6, "0");
                GiveNightVision(6, "0");
                GiveSteadyAim(6, "0");
                Visions(6, "4096");
                GiveSecondaryWeapon(6, "4");
                GiveSecondaryWeaponAmmo(6, "4");
                GivePrimaryAmmo(6, "0");
                GivePrimaryAkimbo(6, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client7BTN.Text = address0.ToString();

            #region Toggle God
            if (Client7BTN.Text == "4")
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

            if (Client7BTN.Text == "260")
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
            if (Client7BTN.Text == "526596")
            {
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                WriteName(7, "Package: ^2ON");
                GiveNoRecoil(7, "1024");
                GiveNightVision(7, "3");
                GiveSteadyAim(7, "2");
                Visions(7, "4112");
                GiveSecondaryWeapon(7, "1");
                GiveSecondaryWeaponAmmo(7, "1");
                GivePrimaryAmmo(7, "999999999");
                GivePrimaryAkimbo(7, "999999999");
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

            if (Client7BTN.Text == "526340")
            {
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                WriteName(7, "Package: ^1OFF");
                GiveNoRecoil(7, "0");
                GiveNightVision(7, "0");
                GiveSteadyAim(7, "0");
                Visions(7, "4096");
                GiveSecondaryWeapon(7, "4");
                GiveSecondaryWeaponAmmo(7, "4");
                GivePrimaryAmmo(7, "0");
                GivePrimaryAkimbo(7, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client8BTN.Text = address0.ToString();

            #region Toggle God
            if (Client8BTN.Text == "4")
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

            if (Client8BTN.Text == "260")
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
            if (Client8BTN.Text == "526596")
            {
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                WriteName(8, "Package: ^2ON");
                GiveNoRecoil(8, "1024");
                GiveNightVision(8, "3");
                GiveSteadyAim(8, "2");
                Visions(8, "4112");
                GiveSecondaryWeapon(8, "1");
                GiveSecondaryWeaponAmmo(8, "1");
                GivePrimaryAmmo(8, "999999999");
                GivePrimaryAkimbo(8, "999999999");
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

            if (Client8BTN.Text == "526340")
            {
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                WriteName(8, "Package: ^1OFF");
                GiveNoRecoil(8, "0");
                GiveNightVision(8, "0");
                GiveSteadyAim(8, "0");
                Visions(8, "4096");
                GiveSecondaryWeapon(8, "4");
                GiveSecondaryWeaponAmmo(8, "4");
                GivePrimaryAmmo(8, "0");
                GivePrimaryAkimbo(8, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client9BTN.Text = address0.ToString();

            #region Toggle God
            if (Client9BTN.Text == "4")
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

            if (Client9BTN.Text == "260")
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
            if (Client9BTN.Text == "526596")
            {
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                WriteName(9, "Package: ^2ON");
                GiveNoRecoil(9, "1024");
                GiveNightVision(9, "3");
                GiveSteadyAim(9, "2");
                Visions(9, "4112");
                GiveSecondaryWeapon(9, "1");
                GiveSecondaryWeaponAmmo(9, "1");
                GivePrimaryAmmo(9, "999999999");
                GivePrimaryAkimbo(9, "999999999");
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

            if (Client9BTN.Text == "526340")
            {
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                WriteName(9, "Package: ^1OFF");
                GiveNoRecoil(9, "0");
                GiveNightVision(9, "0");
                GiveSteadyAim(9, "0");
                Visions(9, "4096");
                GiveSecondaryWeapon(9, "4");
                GiveSecondaryWeaponAmmo(9, "4");
                GivePrimaryAmmo(9, "0");
                GivePrimaryAkimbo(9, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client10BTN.Text = address0.ToString();

            #region Toggle God
            if (Client10BTN.Text == "4")
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

            if (Client10BTN.Text == "260")
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
            if (Client10BTN.Text == "526596")
            {
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                WriteName(10, "Package: ^2ON");
                GiveNoRecoil(10, "1024");
                GiveNightVision(10, "3");
                GiveSteadyAim(10, "2");
                Visions(10, "4112");
                GiveSecondaryWeapon(10, "1");
                GiveSecondaryWeaponAmmo(10, "1");
                GivePrimaryAmmo(10, "999999999");
                GivePrimaryAkimbo(10, "999999999");
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

            if (Client10BTN.Text == "526340")
            {
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                WriteName(10, "Package: ^1OFF");
                GiveNoRecoil(10, "0");
                GiveNightVision(10, "0");
                GiveSteadyAim(10, "0");
                Visions(10, "4096");
                GiveSecondaryWeapon(10, "4");
                GiveSecondaryWeaponAmmo(10, "4");
                GivePrimaryAmmo(10, "0");
                GivePrimaryAkimbo(10, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client11BTN.Text = address0.ToString();

            #region Toggle God
            if (Client11BTN.Text == "4")
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

            if (Client11BTN.Text == "260")
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
            if (Client11BTN.Text == "526596")
            {
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                WriteName(11, "Package: ^2ON");
                GiveNoRecoil(11, "1024");
                GiveNightVision(11, "3");
                GiveSteadyAim(11, "2");
                Visions(11, "4112");
                GiveSecondaryWeapon(11, "1");
                GiveSecondaryWeaponAmmo(11, "1");
                GivePrimaryAmmo(11, "999999999");
                GivePrimaryAkimbo(11, "999999999");
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

            if (Client11BTN.Text == "526340")
            {
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                WriteName(11, "Package: ^1OFF");
                GiveNoRecoil(11, "0");
                GiveNightVision(11, "0");
                GiveSteadyAim(11, "0");
                Visions(11, "4096");
                GiveSecondaryWeapon(11, "4");
                GiveSecondaryWeaponAmmo(11, "4");
                GivePrimaryAmmo(11, "0");
                GivePrimaryAkimbo(11, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client12BTN.Text = address0.ToString();

            #region Toggle God
            if (Client12BTN.Text == "4")
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

            if (Client12BTN.Text == "260")
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
            if (Client12BTN.Text == "526596")
            {
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                WriteName(12, "Package: ^2ON");
                GiveNoRecoil(12, "1024");
                GiveNightVision(12, "3");
                GiveSteadyAim(12, "2");
                Visions(12, "4112");
                GiveSecondaryWeapon(12, "1");
                GiveSecondaryWeaponAmmo(12, "1");
                GivePrimaryAmmo(12, "999999999");
                GivePrimaryAkimbo(12, "999999999");
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

            if (Client12BTN.Text == "526340")
            {
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                WriteName(12, "Package: ^1OFF");
                GiveNoRecoil(12, "0");
                GiveNightVision(12, "0");
                GiveSteadyAim(12, "0");
                Visions(12, "4096");
                GiveSecondaryWeapon(12, "4");
                GiveSecondaryWeaponAmmo(12, "4");
                GivePrimaryAmmo(12, "0");
                GivePrimaryAkimbo(12, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client13BTN.Text = address0.ToString();

            #region Toggle God
            if (Client13BTN.Text == "4")
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

            if (Client13BTN.Text == "260")
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
            if (Client13BTN.Text == "526596")
            {
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                WriteName(13, "Package: ^2ON");
                GiveNoRecoil(13, "1024");
                GiveNightVision(13, "3");
                GiveSteadyAim(13, "2");
                Visions(13, "4112");
                GiveSecondaryWeapon(13, "1");
                GiveSecondaryWeaponAmmo(13, "1");
                GivePrimaryAmmo(13, "999999999");
                GivePrimaryAkimbo(13, "999999999");
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

            if (Client13BTN.Text == "526340")
            {
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                WriteName(13, "Package: ^1OFF");
                GiveNoRecoil(13, "0");
                GiveNightVision(13, "0");
                GiveSteadyAim(13, "0");
                Visions(13, "4096");
                GiveSecondaryWeapon(13, "4");
                GiveSecondaryWeaponAmmo(13, "4");
                GivePrimaryAmmo(13, "0");
                GivePrimaryAkimbo(13, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client14BTN.Text = address0.ToString();

            #region Toggle God
            if (Client14BTN.Text == "4")
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

            if (Client14BTN.Text == "260")
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
            if (Client14BTN.Text == "526596")
            {
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                WriteName(14, "Package: ^2ON");
                GiveNoRecoil(14, "1024");
                GiveNightVision(14, "3");
                GiveSteadyAim(14, "2");
                Visions(14, "4112");
                GiveSecondaryWeapon(14, "1");
                GiveSecondaryWeaponAmmo(14, "1");
                GivePrimaryAmmo(14, "999999999");
                GivePrimaryAkimbo(14, "999999999");
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

            if (Client14BTN.Text == "526340")
            {
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                WriteName(14, "Package: ^1OFF");
                GiveNoRecoil(14, "0");
                GiveNightVision(14, "0");
                GiveSteadyAim(14, "0");
                Visions(14, "4096");
                GiveSecondaryWeapon(14, "4");
                GiveSecondaryWeaponAmmo(14, "4");
                GivePrimaryAmmo(14, "0");
                GivePrimaryAkimbo(14, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client15BTN.Text = address0.ToString();

            #region Toggle God
            if (Client15BTN.Text == "4")
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

            if (Client15BTN.Text == "260")
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
            if (Client15BTN.Text == "526596")
            {
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                WriteName(15, "Package: ^2ON");
                GiveNoRecoil(15, "1024");
                GiveNightVision(15, "3");
                GiveSteadyAim(15, "2");
                Visions(15, "4112");
                GiveSecondaryWeapon(15, "1");
                GiveSecondaryWeaponAmmo(15, "1");
                GivePrimaryAmmo(15, "999999999");
                GivePrimaryAkimbo(15, "999999999");
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

            if (Client15BTN.Text == "526340")
            {
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                WriteName(15, "Package: ^1OFF");
                GiveNoRecoil(15, "0");
                GiveNightVision(15, "0");
                GiveSteadyAim(15, "0");
                Visions(15, "4096");
                GiveSecondaryWeapon(15, "4");
                GiveSecondaryWeaponAmmo(15, "4");
                GivePrimaryAmmo(15, "0");
                GivePrimaryAkimbo(15, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client16BTN.Text = address0.ToString();

            #region Toggle God
            if (Client16BTN.Text == "4")
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

            if (Client16BTN.Text == "260")
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
            if (Client16BTN.Text == "526596")
            {
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                WriteName(16, "Package: ^2ON");
                GiveNoRecoil(16, "1024");
                GiveNightVision(16, "3");
                GiveSteadyAim(16, "2");
                Visions(16, "4112");
                GiveSecondaryWeapon(16, "1");
                GiveSecondaryWeaponAmmo(16, "1");
                GivePrimaryAmmo(16, "999999999");
                GivePrimaryAkimbo(16, "999999999");
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

            if (Client16BTN.Text == "526340")
            {
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                WriteName(16, "Package: ^1OFF");
                GiveNoRecoil(16, "0");
                GiveNightVision(16, "0");
                GiveSteadyAim(16, "0");
                Visions(16, "4096");
                GiveSecondaryWeapon(16, "4");
                GiveSecondaryWeaponAmmo(16, "4");
                GivePrimaryAmmo(16, "0");
                GivePrimaryAkimbo(16, "0");
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            Client17BTN.Text = address0.ToString();

            #region Toggle God
            if (Client17BTN.Text == "4")
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

            if (Client17BTN.Text == "260")
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
            if (Client17BTN.Text == "526596")
            {
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                WriteName(17, "Package: ^2ON");
                GiveNoRecoil(17, "1024");
                GiveNightVision(17, "3");
                GiveSteadyAim(17, "2");
                Visions(17, "4112");
                GiveSecondaryWeapon(17, "1");
                GiveSecondaryWeaponAmmo(17, "1");
                GivePrimaryAmmo(17, "999999999");
                GivePrimaryAkimbo(17, "999999999");
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

            if (Client17BTN.Text == "526340")
            {
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                WriteName(17, "Package: ^1OFF");
                GiveNoRecoil(17, "0");
                GiveNightVision(17, "0");
                GiveSteadyAim(17, "0");
                Visions(17, "4096");
                GiveSecondaryWeapon(17, "4");
                GiveSecondaryWeaponAmmo(17, "4");
                GivePrimaryAmmo(17, "0");
                GivePrimaryAkimbo(17, "0");
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
                RedBoxOFFHost21("4096");
                RedBoxOFFHost22("4096");
                RedBoxOFFHost23("4096");
                RedBoxOFFHost24("4096");
                RedBoxOFFHost25("4096");
                RedBoxOFFHost26("4096");
                RedBoxOFFHost27("4096");
                RedBoxOFFHost28("4096");
                RedBoxOFFHost29("4096");
                RedBoxOFFHost30("4096");
                RedBoxOFFHost31("4096");
                RedBoxOFFHost32("4096");
                RedBoxOFFHost33("4096");
                RedBoxOFFHost34("4096");
                RedBoxOFFHost35("4096");
                RedBoxOFFHost36("4096");
                RedBoxOFFHost37("4096");
            }

            if (RedBoxSwitch.Value == false)
            {
                RedBoxOFFHost0("4112");
                RedBoxOFFHost1("4112");
                RedBoxOFFHost2("4112");
                RedBoxOFFHost3("4112");
                RedBoxOFFHost4("4112");
                RedBoxOFFHost5("4112");
                RedBoxOFFHost6("4112");
                RedBoxOFFHost7("4112");
                RedBoxOFFHost8("4112");
                RedBoxOFFHost9("4112");
                RedBoxOFFHost10("4112");
                RedBoxOFFHost11("4112");
                RedBoxOFFHost12("4112");
                RedBoxOFFHost13("4112");
                RedBoxOFFHost14("4112");
                RedBoxOFFHost15("4112");
                RedBoxOFFHost16("4112");
                RedBoxOFFHost17("4112");
                RedBoxOFFHost18("4112");
                RedBoxOFFHost19("4112");
                RedBoxOFFHost20("4112");
                RedBoxOFFHost21("4112");
                RedBoxOFFHost22("4112");
                RedBoxOFFHost23("4112");
                RedBoxOFFHost24("4112");
                RedBoxOFFHost25("4112");
                RedBoxOFFHost26("4112");
                RedBoxOFFHost27("4112");
                RedBoxOFFHost28("4112");
                RedBoxOFFHost29("4112");
                RedBoxOFFHost30("4112");
                RedBoxOFFHost31("4112");
                RedBoxOFFHost32("4112");
                RedBoxOFFHost33("4112");
                RedBoxOFFHost34("4112");
                RedBoxOFFHost35("4112");
                RedBoxOFFHost36("4112");
                RedBoxOFFHost37("4112");
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
                RedBoxOFFHost21("4096");
                RedBoxOFFHost22("4096");
                RedBoxOFFHost23("4096");
                RedBoxOFFHost24("4096");
                RedBoxOFFHost25("4096");
                RedBoxOFFHost26("4096");
                RedBoxOFFHost27("4096");
                RedBoxOFFHost28("4096");
                RedBoxOFFHost29("4096");
                RedBoxOFFHost30("4096");
                RedBoxOFFHost31("4096");
                RedBoxOFFHost32("4096");
                RedBoxOFFHost33("4096");
                RedBoxOFFHost34("4096");
                RedBoxOFFHost35("4096");
                RedBoxOFFHost36("4096");
                RedBoxOFFHost37("4096");
            }

            if (ThermalSwitch.Value == false)
            {
                RedBoxOFFHost0("5096");
                RedBoxOFFHost1("5096");
                RedBoxOFFHost2("5096");
                RedBoxOFFHost3("5096");
                RedBoxOFFHost4("5096");
                RedBoxOFFHost5("5096");
                RedBoxOFFHost6("5096");
                RedBoxOFFHost7("5096");
                RedBoxOFFHost8("5096");
                RedBoxOFFHost9("5096");
                RedBoxOFFHost10("5096");
                RedBoxOFFHost11("5096");
                RedBoxOFFHost12("5096");
                RedBoxOFFHost13("5096");
                RedBoxOFFHost14("5096");
                RedBoxOFFHost15("5096");
                RedBoxOFFHost16("5096");
                RedBoxOFFHost17("5096");
                RedBoxOFFHost18("5096");
                RedBoxOFFHost19("5096");
                RedBoxOFFHost20("5096");
                RedBoxOFFHost21("5096");
                RedBoxOFFHost22("5096");
                RedBoxOFFHost23("5096");
                RedBoxOFFHost24("5096");
                RedBoxOFFHost25("5096");
                RedBoxOFFHost26("5096");
                RedBoxOFFHost27("5096");
                RedBoxOFFHost28("5096");
                RedBoxOFFHost29("5096");
                RedBoxOFFHost30("5096");
                RedBoxOFFHost31("5096");
                RedBoxOFFHost32("5096");
                RedBoxOFFHost33("5096");
                RedBoxOFFHost34("5096");
                RedBoxOFFHost35("5096");
                RedBoxOFFHost36("5096");
                RedBoxOFFHost37("5096");
            }
        }

        private void SteadySwitch_Click(object sender, EventArgs e)
        {
            if (SteadySwitch.Value == true)
            {
                SteadyOFFHost0("0");
                SteadyOFFHost1("0");
                SteadyOFFHost2("0");
                SteadyOFFHost3("0");
                SteadyOFFHost4("0");
                SteadyOFFHost5("0");
                SteadyOFFHost6("0");
                SteadyOFFHost7("0");
                SteadyOFFHost8("0");
                SteadyOFFHost9("0");
                SteadyOFFHost11("0");
                SteadyOFFHost12("0");
                SteadyOFFHost13("0");
                SteadyOFFHost14("0");
                SteadyOFFHost15("0");
                SteadyOFFHost16("0");
                SteadyOFFHost17("0");
                SteadyOFFHost18("0");
                SteadyOFFHost19("0");
                SteadyOFFHost20("0");
                SteadyOFFHost21("0");
                SteadyOFFHost22("0");
                SteadyOFFHost23("0");
                SteadyOFFHost24("0");
                SteadyOFFHost25("0");
                SteadyOFFHost26("0");
                SteadyOFFHost27("0");
                SteadyOFFHost28("0");
                SteadyOFFHost29("0");
                SteadyOFFHost30("0");
                SteadyOFFHost31("0");
                SteadyOFFHost32("0");
                SteadyOFFHost33("0");
                SteadyOFFHost34("0");
                SteadyOFFHost35("0");
                SteadyOFFHost36("0");
            }

            if (SteadySwitch.Value == false)
            {
                SteadyOFFHost0("2");
                SteadyOFFHost1("2");
                SteadyOFFHost2("2");
                SteadyOFFHost3("2");
                SteadyOFFHost4("2");
                SteadyOFFHost5("2");
                SteadyOFFHost6("2");
                SteadyOFFHost7("2");
                SteadyOFFHost8("2");
                SteadyOFFHost9("2");
                SteadyOFFHost11("2");
                SteadyOFFHost12("2");
                SteadyOFFHost13("2");
                SteadyOFFHost14("2");
                SteadyOFFHost15("2");
                SteadyOFFHost16("2");
                SteadyOFFHost17("2");
                SteadyOFFHost18("2");
                SteadyOFFHost19("2");
                SteadyOFFHost20("2");
                SteadyOFFHost21("2");
                SteadyOFFHost22("2");
                SteadyOFFHost23("2");
                SteadyOFFHost24("2");
                SteadyOFFHost25("2");
                SteadyOFFHost26("2");
                SteadyOFFHost27("2");
                SteadyOFFHost28("2");
                SteadyOFFHost29("2");
                SteadyOFFHost30("2");
                SteadyOFFHost31("2");
                SteadyOFFHost32("2");
                SteadyOFFHost33("2");
                SteadyOFFHost34("2");
                SteadyOFFHost35("2");
                SteadyOFFHost36("2");
            }
        }


    }
}