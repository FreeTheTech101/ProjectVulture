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
    public partial class MW2NonHostWindow : DevComponents.DotNetBar.Metro.MetroForm
    {
        public MW2NonHostWindow()
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
            InGame = 0x0633C8D7,
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
            RecoilOFF = 0x007A3228,
            RecoilOFF1 = 0x007F5B74,
            RecoilOFF2 = 0x007F8EEC,
            RecoilOFF3 = 0x0082C8D8,
            RecoilOFF4 = 0x00B33ACC,
            RecoilOFF5 = 0x00B3C2A0,
            RecoilOFF6 = 0x00B3F3EC,
            RecoilOFF7 = 0x00B42538,
            RecoilOFF8 = 0x00B42538,
            RecoilOFF9 = 0x00B487D0,
            RecoilOFF10 = 0x00B4B91C,
            RecoilOFF11 = 0x00B4EA68,
            RecoilOFF12 = 0x00B51BB4,
            RecoilOFF13 = 0x00B54D00,
            RecoilOFF14 = 0x00B54D00,
            RecoilOFF15 = 0x00B57E4C,
            RecoilOFF16 = 0x00B5AF98,
            RecoilOFF17 = 0x00B5E0E4,
            RecoilOFF18 = 0x00B61230,
            RecoilOFF19 = 0x00B6437C,
            RecoilOFF20 = 0x00B674C8,
            RecoilOFF21 = 0x00B6A614,
            RecoilOFF22 = 0x00B6D760,
            RecoilOFF23 = 0x00B708AC,
            RecoilOFF24 = 0x00B739F8,
            RecoilOFF25 = 0x00B76B44,
            RecoilOFF26 = 0x00B79C90,
            RecoilOFF27 = 0x00B7CDDC,
            RecoilOFF28 = 0x00B7FF28,
            RecoilOFF29 = 0x00B83074,
            RecoilOFF30 = 0x00B861C0,
            RecoilOFF31 = 0x00B8930C,
            RecoilOFF32 = 0x00B8C458,
            RecoilOFF33 = 0x00B8F5A4,
            RecoilOFF34 = 0x00B926F0,
            RecoilOFF35 = 0x00B9583C,
            RecoilOFF36 = 0x00B98988,
            RecoilOFF37 = 0x00B9BAD4,
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
            RedBoxOFF37 = 0x00B9B828;
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

        private void RecoilOFFHost0(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost1(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF1);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost2(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF2);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost3(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF3);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost4(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF4);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost5(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF5);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost6(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF6);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost7(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF7);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost8(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF8);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost9(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF9);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost10(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF10);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost11(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF11);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost12(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF12);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost13(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF13);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost14(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF14);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost15(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF15);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost16(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF16);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost17(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF17);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost18(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF18);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost19(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF19);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost20(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF20);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost21(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF21);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost22(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF22);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost23(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF23);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost24(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF24);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost25(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF25);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost26(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF26);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost27(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF27);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost28(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF28);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost29(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF29);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost30(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF30);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost31(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF31);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost32(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF32);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost33(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF33);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost34(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF34);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost35(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF35);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost36(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF36);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void RecoilOFFHost37(string Recoil)
        {
            uint Steady = Convert.ToUInt32(GameAddresses.RecoilOFF37);
            Memorys mem = new Memorys("iw4mp");
            uint AmmoTest = mem.baseaddress("iw4mp") + Steady;
            mem.Write(AmmoTest, int.Parse(Recoil));
        }

        private void MW2NonHostWindow_Load(object sender, EventArgs e)
        {
            MP.Start();
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
                ThermalSwitch.Enabled = true;
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
                RedBoxSwitch.Enabled = true;
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
                RedBoxSwitch.Enabled = false;
            }
        }

        private void RecoilSwitch_Click(object sender, EventArgs e)
        {
            if (RecoilSwitch.Value == true)
            {
                RecoilOFFHost0("0");
                RecoilOFFHost1("0");
                RecoilOFFHost2("0");
                RecoilOFFHost3("0");
                RecoilOFFHost4("0");
                RecoilOFFHost5("0");
                RecoilOFFHost6("0");
                RecoilOFFHost7("0");
                RecoilOFFHost8("0");
                RecoilOFFHost9("0");
                RecoilOFFHost10("0");
                RecoilOFFHost11("0");
                RecoilOFFHost12("0");
                RecoilOFFHost13("0");
                RecoilOFFHost14("0");
                RecoilOFFHost15("0");
                RecoilOFFHost16("0");
                RecoilOFFHost17("0");
                RecoilOFFHost18("0");
                RecoilOFFHost19("0");
                RecoilOFFHost20("0");
                RecoilOFFHost21("0");
                RecoilOFFHost22("0");
                RecoilOFFHost23("0");
                RecoilOFFHost24("0");
                RecoilOFFHost25("0");
                RecoilOFFHost26("0");
                RecoilOFFHost27("0");
                RecoilOFFHost29("0");
                RecoilOFFHost30("0");
                RecoilOFFHost31("0");
                RecoilOFFHost32("0");
                RecoilOFFHost33("0");
                RecoilOFFHost34("0");
                RecoilOFFHost35("0");
                RecoilOFFHost36("0");
                RecoilOFFHost37("0");
            }

            if (RecoilSwitch.Value == false)
            {
                RecoilOFFHost0("1024");
                RecoilOFFHost1("1024");
                RecoilOFFHost2("1024");
                RecoilOFFHost3("1024");
                RecoilOFFHost4("1024");
                RecoilOFFHost5("1024");
                RecoilOFFHost6("1024");
                RecoilOFFHost7("1024");
                RecoilOFFHost8("1024");
                RecoilOFFHost9("1024");
                RecoilOFFHost10("1024");
                RecoilOFFHost11("1024");
                RecoilOFFHost12("1024");
                RecoilOFFHost13("1024");
                RecoilOFFHost14("1024");
                RecoilOFFHost15("1024");
                RecoilOFFHost16("1024");
                RecoilOFFHost17("1024");
                RecoilOFFHost18("1024");
                RecoilOFFHost19("1024");
                RecoilOFFHost20("1024");
                RecoilOFFHost21("1024");
                RecoilOFFHost22("1024");
                RecoilOFFHost23("1024");
                RecoilOFFHost24("1024");
                RecoilOFFHost25("1024");
                RecoilOFFHost26("1024");
                RecoilOFFHost27("1024");
                RecoilOFFHost29("1024");
                RecoilOFFHost30("1024");
                RecoilOFFHost31("1024");
                RecoilOFFHost32("1024");
                RecoilOFFHost33("1024");
                RecoilOFFHost34("1024");
                RecoilOFFHost35("1024");
                RecoilOFFHost36("1024");
                RecoilOFFHost37("1024");
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

        private void MP_Tick(object sender, EventArgs e)
        {
            if (Process_Handle("iw4mp"))
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
            Memorys mem = new Memorys("iw4mp");
            uint ClientButton = mem.baseaddress("iw4mp") + Value;
            uint address0 = (uint)mem.ReadPointer(ClientButton);
            labelX1.Text = address0.ToString();

            if (labelX1.Text == "0")
            {
                RedBoxSwitch.Value = false;
                ThermalSwitch.Value = false;
                RecoilSwitch.Value = false;
                SteadySwitch.Value = false;
                RedBoxSwitch.Enabled = true;
                ThermalSwitch.Enabled = true;
            }
        }


    }
}
