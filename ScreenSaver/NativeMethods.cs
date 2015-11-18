
using System;
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
        
        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;

        internal static void EnableMonitorSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS);
        }
    }
}
