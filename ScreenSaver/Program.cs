using System;
using System.Windows.Forms;

namespace ScreenSaver
{
    static class Program
    {
        /// <summary>
        /// Arguments for any Windows 98+ screensaver:
        /// 
        ///   ScreenSaver           - Show the Settings dialog box.
        ///   ScreenSaver /c        - Show the Settings dialog box, modal to the foreground window.
        ///   ScreenSaver /p <HWND> - Preview Screen Saver as child of window <HWND>.
        ///   ScreenSaver /s        - Run the Screen Saver.
        /// 
        /// Custom arguments:
        /// 
        ///   ScreenSaver /w        - Run in normal resizable window mode.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                    Application.Run(new SettingsForm());
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
                }  else if (firstArgument == "/w")
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
            else    // No arguments - treat like /c
            {
                Application.Run(new SettingsForm());
            }            
        }

        /// <summary>
        /// Display the form on each of the computer's monitors.
        /// </summary>
        static void ShowScreenSaver()
        {
            int i = 0;
            var multiscreenDisabled = new RegSettings().MultiscreenDisabled;
            foreach (Screen screen in Screen.AllScreens)
            {
                ScreenSaverForm screensaver = new ScreenSaverForm(screen.Bounds);

                // disable video on multi-displays (3+) except the first
                if (Screen.AllScreens.Length > 2 && i != 0 && multiscreenDisabled)
                    screensaver.ShowVideo = false;

                i++;
                screensaver.Show();
            }
        }

    }
}
