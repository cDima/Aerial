﻿using ScreenSaver;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Aerial
{
    static class Program
    {
        /// <summary>
        /// Arguments for any Windows 98+ screensaver:
        /// 
        ///   ScreenSaver.scr           - Show the Settings dialog box.
        ///   ScreenSaver.scr /c        - Show the Settings dialog box, modal to the foreground window.
        ///   ScreenSaver.scr /p <HWND> - Preview Screen Saver as child of window <HWND>.
        ///   ScreenSaver.scr /s        - Run the Screen Saver.
        /// 
        /// Custom arguments:
        /// 
        ///   ScreenSaver.scr /w        - Run in normal resizable window mode.
        ///   ScreenSaver.exe           - Run in normal resizable window mode.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, dll) =>
            {
                var resName = "Aerial.libs." + dll.Name.Split(',')[0] + ".dll";
                var thisAssembly = Assembly.GetExecutingAssembly();
                using (var input = thisAssembly.GetManifestResourceStream(resName))
                {
                    return input != null
                         ? Assembly.Load(StreamToBytes(input))
                         : null;
                }
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Caching.Setup();


            if (args.Length > 0)
            {
                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                // Handle cases where arguments are separated by colon. 
                // Examples: /c:1234567 or /P:1234567
                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length > 1)
                    secondArgument = args[1];
                
                if (firstArgument == "/c")           // Configuration mode
                {
                    var settings = new SettingsForm();
                    settings.StartPosition = FormStartPosition.CenterScreen;
                    Application.Run(settings);
                }
                else if (firstArgument == "/p")      // Preview mode
                {
                    if (secondArgument == null)
                    {
                        MessageBox.Show("Sorry, but the expected window handle was not provided.",
                            "ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                    IntPtr previewWndHandle = new IntPtr(long.Parse(secondArgument));
                    Application.Run(new ScreenSaverForm(previewWndHandle));
                }
                else if (firstArgument == "/s")      // Full-screen mode
                {
                    ShowScreenSaver();
                    Application.Run();
                }  else if (firstArgument == "/w") // if executable, windowed mode.
                {
                    Application.Run(new ScreenSaverForm(WindowMode: true));
                }
                else    // Undefined argument
                {
                    MessageBox.Show("Sorry, but the command line argument \"" + firstArgument +
                        "\" is not valid.", "ScreenSaver",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else    
            {
                if (System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.EndsWith("exe")) // treat like /w
                {
                    Application.Run(new ScreenSaverForm(WindowMode: true));
                }
                else // No arguments - treat like /c
                {
                    Application.Run(new SettingsForm());
                }
            }            
        }

        static byte[] StreamToBytes(Stream input)
        {
            var capacity = input.CanSeek ? (int)input.Length : 0;
            using (var output = new MemoryStream(capacity))
            {
                int readLength;
                var buffer = new byte[4096];

                do
                {
                    readLength = input.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, readLength);
                }
                while (readLength != 0);

                return output.ToArray();
            }
        }

        /// <summary>
        /// Display the form on each of the computer's monitors.
        /// </summary>
        static void ShowScreenSaver()
        {
            var multiMonitorMode = new RegSettings().MultiMonitorMode;

            switch (multiMonitorMode)
            {
                case RegSettings.MultiMonitorModeEnum.SameOnEach:
                case RegSettings.MultiMonitorModeEnum.DifferentVideos:
                    {
                        foreach (var screen in Screen.AllScreens)
                        {
                            new ScreenSaverForm(screen.Bounds, shouldCache: screen.Primary, showVideo: true).Show();
                        }
                        break;
                    }
                case RegSettings.MultiMonitorModeEnum.SpanAll:
                    {
                        new ScreenSaverForm(Screen.AllScreens.GetBounds(), shouldCache: true, showVideo: true).Show();
                        break;
                    }
                case RegSettings.MultiMonitorModeEnum.MainOnly:
                default:
                    {
                        foreach (var screen in Screen.AllScreens)
                        {
                            new ScreenSaverForm(screen.Bounds, shouldCache: screen.Primary, showVideo: screen.Primary).Show();
                        }
                        break;
                    }
            }
        }
    }
}
