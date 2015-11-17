
using System;
using System.Runtime.InteropServices;

namespace Aerial
{
    internal static class NativeMethods
    {
        // Import SetThreadExecutionState Win32 API and necessary flags
        [DllImport("kernel32.dll")]
        static extern uint SetThreadExecutionState(uint esFlags);
        const uint ES_CONTINUOUS = 0x80000000;
        const uint ES_SYSTEM_REQUIRED = 0x00000001;

        internal static void EnableMonitorSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS);
        }
    }
}
