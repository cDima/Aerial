
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Aerial
{
    internal static class NativeMethods
    {
        public const int HT_CAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        
        [DllImport("kernel32.dll")]
        internal static extern uint SetThreadExecutionState(uint esFlags);
        
        [DllImport("user32.dll")]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);
        
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern bool SetCapture();

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern long StrFormatByteSize(
        long fileSize
        , [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer
        , int bufferSize);


        /// <summary>
        /// Converts a numeric value into a string that represents the number expressed as a size value in bytes, kilobytes, megabytes, or gigabytes, depending on the size.
        /// </summary>
        /// <param name="filelength">The numeric value to be converted.</param>
        /// <returns>the converted string</returns>
        public static string GetExplorerFileSize(long filesize)
        {
            StringBuilder sb = new StringBuilder(11);
            StrFormatByteSize(filesize, sb, sb.Capacity);
            return sb.ToString();
        }

        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;

        
        internal static void EnableMonitorSleep()
        {
            Trace.WriteLine("EnableMonitorSleep()");
            SetThreadExecutionState(ES_CONTINUOUS);
        }

        internal static void DragWindow(IntPtr handle)
        {
            Trace.WriteLine("DragWindow()");
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            CaptureMouseUpAndClick(handle);
        }

        internal static void ResizeWindow(IntPtr handle, bool? toTop, bool? toLeft)
        {
            Trace.WriteLine("ResizeWindow()");
            var SC_SIZE = 0xF000;
            var WM_SYSCOMMAND = 0x0112;

            //var directions = new List<Tuple<bool?,bool?,SysCommandSize>>();
            var enumName = "SC_SIZE_HT" + 
                (toTop == true ? "TOP" : toTop == false? "BOTTOM" : "") +
                (toLeft == true ? "LEFT" : toLeft == false ? "RIGHT" : "");
            SysCommandSize direction = (SysCommandSize) Enum.Parse(typeof(SysCommandSize), enumName);
            
            SendMessage(handle, WM_SYSCOMMAND, SC_SIZE + (int)direction, 0);

            CaptureMouseUpAndClick(handle);
        }

        internal static void CaptureMouseUpAndClick(IntPtr handle)
        {
            Trace.WriteLine("CaptureMouseUpAndClick()");
            // Set capture back to the form
            ReleaseCapture();
            // Send the form a MouseUp message
            SendMessage(handle, WM_LBUTTONUP, 0, 0);
        }

        enum SysCommandSize : int
        {
            SC_SIZE_HTLEFT = 1,
            SC_SIZE_HTRIGHT = 2,
            SC_SIZE_HTTOP = 3,
            SC_SIZE_HTTOPLEFT = 4,
            SC_SIZE_HTTOPRIGHT = 5,
            SC_SIZE_HTBOTTOM = 6,
            SC_SIZE_HTBOTTOMLEFT = 7,
            SC_SIZE_HTBOTTOMRIGHT = 8
        }
    }

}
