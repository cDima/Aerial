using Aerial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        public bool ShowVideo = true;

        private Point mouseLocation;
        private bool previewMode = false;
        private bool windowMode = false;
        int currentVideoIndex = 0;
        List<Asset> Movies;
        DateTime lastInteraction = DateTime.Now;

        string cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aerial");
        string tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");

        public ScreenSaverForm()
        {
            InitializeComponent();

            TopMost = true;
        }

        public ScreenSaverForm(bool WindowMode = false) : this()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
            
            windowMode = true;
            MaximizeVideo();
            
            this.MouseDown += ScreenSaverForm_MouseDown;
            this.player.MouseDownEvent += Player_MouseDownEvent;
            btnClose.Visible = true;
        }
        
        public ScreenSaverForm(Rectangle Bounds) : this()
        {
            this.Bounds = Bounds;
        }

        public ScreenSaverForm(IntPtr PreviewWndHandle) : this()
        {
         
            // Set the preview window as the parent of this window
            NativeMethods.SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            NativeMethods.SetWindowLong(this.Handle, -16, new IntPtr(NativeMethods.GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            NativeMethods.GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);

            previewMode = true;
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            if (!previewMode && !windowMode) Cursor.Hide();

            LayoutPlayer();

            var nextVideoTimer = new System.Windows.Forms.Timer();
            nextVideoTimer.Tick += NextVideoTimer_Tick;
            nextVideoTimer.Interval = 1000;
            nextVideoTimer.Enabled = true;

            var cacheVideos = new RegSettings().CacheVideos;
            if (cacheVideos) {
                DirectoryInfo directory = Directory.CreateDirectory(cacheFolder);
            }

            if (ShowVideo)
            {
                Movies = new AerialContext().GetMovies();

#if DEBUG
                Movies = new List<Asset>
                {
                    new Asset {url = @"http://blog.luxisinteractive.com/wp-content/uploads/2015/08/depth.mp4" },
                    new Asset {url = @"http://blog.luxisinteractive.com/wp-content/uploads/2015/08/animation.mp4" },
                };

                //this.player.URL = @"http://blog.luxisinteractive.com/wp-content/uploads/2015/08/depth.mp4";
#endif

                SetNextVideo();

            }
        }

        private void MaximizeVideo()
        {
            var screenArea = Screen.FromControl(this).WorkingArea;
            var videoSize = this.Size;
            if (screenArea.Size.Width > videoSize.Width && screenArea.Height > videoSize.Height)
            {
                videoSize = new Size(1920, 1080);
            }

            this.SetBounds(
                (screenArea.Width - videoSize.Width) / 2,
                (screenArea.Height - videoSize.Height) / 2,
                videoSize.Width,
                videoSize.Height);
        }

        private void Player_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            ScreenSaverForm_MouseDown(null, new MouseEventArgs(e.nButton == 1 ? MouseButtons.Left : MouseButtons.Right, 0, e.fX, e.fY, 0));
        }

        private void ScreenSaverForm_MouseDown(object sender, MouseEventArgs e)
        {
            Point m = PointToClient(Cursor.Position);
            var drag = 12;
            bool? toTop = m.Y < drag ? true : (m.Y > (Size.Height - drag) ? false : (bool?)null);
            bool? toLeft = m.X < drag ? true : (m.X > (Size.Width - drag) ? false : (bool?)null);

            if (e.Button == MouseButtons.Left)
            {
                if (toTop == null && toLeft == null)
                    NativeMethods.DragWindow(Handle);
                else
                    NativeMethods.ResizeWindow(Handle, toTop, toLeft);
            }
        }

        private void OnDownloadFileComplete(object sender, AsyncCompletedEventArgs e)
        {
			if (e.Cancelled == false && e.Error == null) {
	            Directory.Move(Path.Combine(tempFolder, e.UserState.ToString()), Path.Combine(cacheFolder, e.UserState.ToString()));
	        }
        }

        private void SetNextVideo()
        {
            Trace.WriteLine("SetNextVideo()");
            var cacheVideos = new RegSettings().CacheVideos;
            if (ShowVideo)
            {
                string tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");
                string filename = Path.GetFileName(Movies[currentVideoIndex].url);

                if (File.Exists(Path.Combine(cacheFolder, filename)))
                {
                    player.URL = Path.Combine(cacheFolder, filename);
                }
                else
                {
                    player.URL = Movies[currentVideoIndex].url;
                    if (cacheVideos) {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileComplete);
                            client.DownloadFileAsync(new System.Uri(Movies[currentVideoIndex].url), Path.Combine(tempFolder, filename), filename);
                        }
                    }
                }
                currentVideoIndex++;
                if (currentVideoIndex >= Movies.Count)
                    currentVideoIndex = 0;
            }
        }

        private void NextVideoTimer_Tick(object sender, EventArgs e)
        {
            var state = this.player.playState;
            Trace.WriteLine("Timer: " + state);
            if (state == WMPLib.WMPPlayState.wmppsReady ||
                state == WMPLib.WMPPlayState.wmppsUndefined ||
                state == WMPLib.WMPPlayState.wmppsStopped)
            {
                SetNextVideo();
            }

            if (lastInteraction.AddSeconds(-1) < DateTime.Now)
            {
                this.btnClose.Visible = false;
            }
        }

        private void LayoutPlayer()
        {
            this.player.settings.autoStart = true;
            this.player.settings.enableErrorDialogs = true;
            this.player.uiMode = "none";
            this.player.enableContextMenu = false;
            Application.AddMessageFilter(new IgnoreMouseClickMessageFilter(this, player));

            ResizePlayer();

            this.player.MouseMoveEvent += player_MouseMoveEvent;
            this.player.KeyPressEvent += player_KeyPressEvent;
            this.player.PlayStateChange += player_PlayStateChange;
        }
        
        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            NativeMethods.EnableMonitorSleep();
        }

        /// <summary>
        /// Resize & center player
        /// </summary>
        private void ResizePlayer()
        {
            this.player.Size = CalculateVideoFillSize(this.Size);
            this.player.stretchToFit = true;
            this.player.Top = (this.Size.Height / 2) - (this.player.Size.Height / 2);
            this.player.Left = (this.Size.Width / 2) - (this.player.Size.Width / 2);
        }

        /// <summary>
        /// Algoirthm for calculating video fill size to fill available screensize on different resolutions.
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

        private void player_KeyPressEvent(object sender, AxWMPLib._WMPOCXEvents_KeyPressEvent e)
        {
            ScreenSaverForm_KeyPress(sender, new KeyPressEventArgs((char)e.nKeyAscii));
        }

        private void player_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            ScreenSaverForm_MouseMove(sender, new MouseEventArgs(MouseButtons.None, 0, e.fX, e.fY, 0));
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            Point m = PointToClient(Cursor.Position);
            bool? toTop = m.Y < 10 ? true : (m.Y > (Size.Height - 10) ? false : (bool?)null);
            bool? toLeft = m.X < 10 ? true : (m.X > (Size.Width - 10) ? false : (bool?)null);

            if (toTop == true && toLeft == true) Cursor = Cursors.SizeNWSE;
            if (toTop == true && toLeft == null) Cursor = Cursors.SizeNS;
            if (toTop == true && toLeft == false) Cursor = Cursors.SizeNESW;

            if (toTop == false && toLeft == true) Cursor = Cursors.SizeNESW;
            if (toTop == false && toLeft == null) Cursor = Cursors.SizeNS;
            if (toTop == false && toLeft == false) Cursor = Cursors.SizeNWSE;

            if (toTop == null && toLeft == true) Cursor = Cursors.SizeWE;
            if (toTop == null && toLeft == null) Cursor = Cursors.Default;
            if (toTop == null && toLeft == false) Cursor = Cursors.SizeWE;

            if (!mouseLocation.IsEmpty)
            {
                // Terminate if mouse is moved a significant distance
                if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                    Math.Abs(mouseLocation.Y - e.Y) > 5)
                    ShouldExit();
            }

            // Update current mouse location
            mouseLocation = e.Location;
            if (windowMode) this.btnClose.Visible = true;
            
        }

        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ShouldExit();
        }

        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            ShouldExit();
        }

        /// <summary>
        /// Exits if not in windowed or preview mode.
        /// </summary>
        void ShouldExit()
        {
            if (!previewMode && !windowMode)
                Application.Exit();
        }

        private void ScreenSaverForm_Resize(object sender, EventArgs e)
        {
            if (windowMode) ResizePlayer();
        }

        private void ScreenSaverForm_Shown(object sender, EventArgs e)
        {
            this.Resize += ScreenSaverForm_Resize;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
    }
}
