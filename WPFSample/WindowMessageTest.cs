using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace WPFSample
{
    internal class WindowMessageTest
    {
        private const int WM_COPYDATA = 0x4A;
        private const int WM_USER = 0x0400;
        public static IntPtr HWND_BROADCAST = (IntPtr)0xFFFF;

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT2
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [DllImport("User32.dll")]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref COPYDATASTRUCT2 lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public static readonly int TEST_MESSAGE = RegisterWindowMessage("TEST_MESSAGE");
        public static readonly int TEST_MESSAGE2 = RegisterWindowMessage("TEST_MESSAGE2");

        public static void Execute(Window window)
        {
            RegisterWndProc(window);

            SendMessageToApp();
        }

        private static async void SendMessageToApp()
        {
            IntPtr hwnd = GetWindowHandle("MFCApp");

            if (hwnd == IntPtr.Zero)
            {
                RunMFCApp();

                await Task.Delay(2000);

                hwnd = GetWindowHandle("MFCApp");
            }

            SendMessage(hwnd, TEST_MESSAGE, 1, 0);
        }

        private static void RunMFCApp()
        {
            string mfcAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MFCApp.exe");

            FileInfo fi = new FileInfo(mfcAppPath);

            Process process = new Process();
            process.StartInfo.Arguments = string.Empty;
            process.StartInfo.FileName = mfcAppPath;

            //if (bRunAsAdmin)
            //{
            //    process.StartInfo.Verb = "runas";
            //}

            //if (bHidden)
            //{
            //    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //}

            process.Start();

            //if (bWaitForIdle)
            //{
            //    process.WaitForInputIdle();
            //}

            //IntPtr hWnd = process.MainWindowHandle;
        }

        private static IntPtr GetWindowHandle(string processName)
        {
            IntPtr hWnd = IntPtr.Zero;
            Process[] processList = Process.GetProcessesByName(processName);

            foreach (var process in processList)
            {
                hWnd = process.MainWindowHandle;

                if (hWnd.ToInt32() > 0) return hWnd;
            }

            return hWnd;
        }

        private static void RegisterWndProc(Window window)
        {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg.Equals(TEST_MESSAGE2))
            {
                MessageBox.Show("WPF Message");
            }

            return IntPtr.Zero;
        }
    }
}