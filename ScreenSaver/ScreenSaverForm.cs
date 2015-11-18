using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Timers;

namespace ScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        #region Win32 API functions

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        public bool ShowVideo = true;

        private Point mouseLocation;
        private bool previewMode = false;
        int currentVideoIndex = 0;
        List<Asset> Movies;

        public ScreenSaverForm()
        {
            InitializeComponent();
        }

        public ScreenSaverForm(Rectangle Bounds)
        {
            InitializeComponent();
            this.Bounds = Bounds;
        }

        public ScreenSaverForm(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);

            previewMode = true;
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            if (!previewMode) Cursor.Hide();

            //TopMost = true;

            LayoutPlayer();


            System.Timers.Timer nextVideoTimer = new System.Timers.Timer();
            nextVideoTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            nextVideoTimer.Interval = 5000;
            nextVideoTimer.Enabled = true;

            if (ShowVideo)
            {
                //var list = axWindowsMediaPlayer1.playlistCollection.newPlaylist("Aerial");

                Movies = new AerialContext().GetMovies();
                
                /*
                Movies = new List<Asset>
                {
                    new Asset {url = @"http://blog.luxisinteractive.com/wp-content/uploads/2015/08/depth.mp4" },
                    new Asset {url = @"http://blog.luxisinteractive.com/wp-content/uploads/2015/08/depth.mp4" },
                };
                */

                //this.axWindowsMediaPlayer1.URL = @"http://blog.luxisinteractive.com/wp-content/uploads/2015/08/depth.mp4";
                SetNextVideo();
                
            }
        }

        private void SetNextVideo()
        {
            axWindowsMediaPlayer1.URL = Movies[currentVideoIndex].url;
            currentVideoIndex++;
            if (currentVideoIndex >= Movies.Count)
                currentVideoIndex = 0;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer: " + this.axWindowsMediaPlayer1.playState);
            if (this.axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsReady)
            {
                SetNextVideo();
            }
        }

        private void LayoutPlayer()
        {
            this.axWindowsMediaPlayer1.settings.autoStart = true;
            this.axWindowsMediaPlayer1.settings.enableErrorDialogs = true;
            this.axWindowsMediaPlayer1.uiMode = "none";
            this.axWindowsMediaPlayer1.enableContextMenu = false;
            Application.AddMessageFilter(new IgnoreMouseClickMessageFilter(this, axWindowsMediaPlayer1));

            this.axWindowsMediaPlayer1.Size = CalculateVideoFillSize(this.Size);
            this.axWindowsMediaPlayer1.stretchToFit = true;
            this.axWindowsMediaPlayer1.Top = 0;
            this.axWindowsMediaPlayer1.Left = 0;
            this.axWindowsMediaPlayer1.settings.setMode("loop", true);
            this.axWindowsMediaPlayer1.MouseMoveEvent += AxWindowsMediaPlayer1_MouseMoveEvent;
            this.axWindowsMediaPlayer1.KeyPressEvent += AxWindowsMediaPlayer1_KeyPressEvent;
            this.axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
        }

        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(uint esFlags);
        public const uint ES_CONTINUOUS = 0x80000000;

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            SetThreadExecutionState(ES_CONTINUOUS);
        }

        /// <summary>
        /// Algoirthm for calculating video fill size
        /// </summary>
        /// <param name="displaySize"></param>
        /// <param name="scaleMode"></param>
        /// <returns>The size of the canvas needed to fill the screen with the source width and height element</returns>
        private Size CalculateVideoFillSize(Size displaySize, double sourceHeight = 1080.0, double sourceWidth = 1920.0)
        {
            var screenHeight = (double)displaySize.Height;
            var screenWidth = (double)displaySize.Width;
            var screenRatio = screenWidth / screenHeight;

            var scale = Math.Max(screenWidth / sourceWidth, screenHeight / sourceHeight);

            return new Size()
            {
                Height = Convert.ToInt32(scale * sourceHeight),
                Width = Convert.ToInt32(scale * sourceWidth)
            };
        }

        private void AxWindowsMediaPlayer1_KeyPressEvent(object sender, AxWMPLib._WMPOCXEvents_KeyPressEvent e)
        {
            ScreenSaverForm_KeyPress(sender, new KeyPressEventArgs((char)e.nKeyAscii));
        }

        private void AxWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            ScreenSaverForm_MouseMove(sender, new MouseEventArgs(MouseButtons.None, 0, e.fX, e.fY, 0));
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!previewMode)
            {
                if (!mouseLocation.IsEmpty)
                {
                    // Terminate if mouse is moved a significant distance
                    if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                        Math.Abs(mouseLocation.Y - e.Y) > 5)
                        Application.Exit();
                }

                // Update current mouse location
                mouseLocation = e.Location;
            }
        }

        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }

        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }
    }
}
