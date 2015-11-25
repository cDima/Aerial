
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Aerial
{
    internal static class NativeMethods
    {
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

        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;

        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        
        internal static void EnableMonitorSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS);
        }

        internal static void DragWindow(IntPtr hangle)
        {
            ReleaseCapture();
            SendMessage(hangle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        internal static void ResizeWindow(IntPtr handle, bool? toTop, bool? toLeft)
        {
            var SC_SIZE = 0xF000;
            var WM_SYSCOMMAND = 0x0112;

            //var directions = new List<Tuple<bool?,bool?,SysCommandSize>>();
            var enumName = "SC_SIZE_HT" + 
                (toTop == true ? "TOP" : toTop == false? "BOTTOM" : "") +
                (toLeft == true ? "LEFT" : toLeft == false ? "RIGHT" : "");
            SysCommandSize direction = (SysCommandSize) Enum.Parse(typeof(SysCommandSize), enumName);
            
            SendMessage(handle, WM_SYSCOMMAND, SC_SIZE + (int)direction, 0);
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
