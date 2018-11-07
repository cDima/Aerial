using Aerial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        private int currentVideoIndex = 0;
        private DateTime lastInteraction = DateTime.Now;
        private Point mouseLocation = Point.Empty;
        private List<Asset> Movies;
        private Timer NextVideoTimer = new Timer();
        private bool previewMode = false;
        private SettingsForm settingsFrm = null;
        private bool shouldCache = false;
        private bool showVideo = true;
        private bool windowMode = false;
        
        public ScreenSaverForm()
        {
            InitializeComponent();

#if !DEBUG
            TopMost = true;
#endif

            RegisterEvents();
        }

        /// <summary>
        /// Initiate the form inside window's screen saver settings screen
        /// </summary>
        /// <param name="PreviewWndHandle"></param>
        public ScreenSaverForm(IntPtr PreviewWndHandle) : this()
        {
            previewMode = true;

            // Set the preview window as the parent of this window
            NativeMethods.SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            NativeMethods.SetWindowLong(this.Handle, -16, new IntPtr(NativeMethods.GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            NativeMethods.GetClientRect(PreviewWndHandle, out ParentRect);
            Size = new Size(ParentRect.Size.Width + 1, ParentRect.Size.Height + 1);
            Location = new Point(-1, -1);
        }

        public ScreenSaverForm(bool WindowMode = false) : this()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
            windowMode = WindowMode;
            MaximizeVideo();

            ShowButtons();
        }

        public ScreenSaverForm(Rectangle Bounds, bool shouldCache, bool showVideo) : this()
        {
            this.Bounds = Bounds;
            this.shouldCache = shouldCache;
            this.showVideo = showVideo;
        }
        
        void RegisterEvents()
        {
            this.player.MouseDownEvent += Player_MouseDownEvent;
            this.player.KeyPressEvent += player_KeyPressEvent;
            this.player.PlayStateChange += player_PlayStateChange;
            this.player.MouseMoveEvent += player_MouseMoveEvent;

            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnClose.MouseMove += new MouseEventHandler(this.btnClose_MouseMove);
            this.btnSettings.Click += new EventHandler(this.btnSettings_Click);
            this.btnSettings.MouseMove += new MouseEventHandler(this.btnClose_MouseMove);

            this.KeyPress += new KeyPressEventHandler(this.ScreenSaverForm_KeyPress);
            this.MouseDown += DoMouseDown;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScreenSaverForm_MouseUp);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!previewMode && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Form
        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            if (!previewMode && !windowMode) Cursor.Hide();

            LayoutPlayer();
            
            this.BackgroundImageLayout = ImageLayout.None;

            if (showVideo) // testing preview video speed didn't work well && !previewMode
            {
                Movies = AerialContext.GetMovies();

#if DEBUG && false
                Movies = new List<Asset>
                {
                    new Asset { url = @"http://18292-presscdn-0-89.pagely.netdna-cdn.com/wp-content/uploads/2015/07/stripe-checkout.mp4?_=1" },
                    new Asset {url = @"http://18292-presscdn-0-89.pagely.netdna-cdn.com/wp-content/uploads/2015/07/stripe-shake.mp4?_=3" },
                };
#endif

                NextVideoTimer.Tick += NextVideoTimer_Tick;
                NextVideoTimer.Interval = 1000;
                NextVideoTimer.Enabled = true;
                    
                SetNextVideo();
            } else if (previewMode)
            {
                // on preview - hide player.
                ShowButtons(false);

                // show picture preview in the windows screensaver dialog inside the 1980s CRT monitor with that CD rom drive at it's bottom

                var pictureBox1 = new PictureBox();
                pictureBox1.Image = global::Aerial.Properties.Resources.bridgeSm3;
                pictureBox1.Location = new System.Drawing.Point(0,0);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(166, 130);
                pictureBox1.TabIndex = 3;
                pictureBox1.TabStop = false;
                this.Controls.Add(pictureBox1);
            }
        }

        private void ScreenSaverForm_Resize(object sender, EventArgs e)
        {
            Trace.WriteLine("ScreenSaverForm_Resize()");
            if (windowMode) ResizePlayer();
        }

        private void ScreenSaverForm_Shown(object sender, EventArgs e)
        {
            this.Resize += ScreenSaverForm_Resize;
        }

        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'n')
                SetNextVideo();
            else
                ShouldExit();
        }
        private void player_KeyPressEvent(object sender, AxWMPLib._WMPOCXEvents_KeyPressEvent e)
        {
            ScreenSaverForm_KeyPress(sender, new KeyPressEventArgs((char)e.nKeyAscii));
        }
#endregion

        #region Mouse events
        
        private void Player_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            Trace.WriteLine("Player_MouseDownEvent() e.nButton=" + e.nButton);
            
            DoMouseDown(null, new MouseEventArgs(e.nButton == 1 ? MouseButtons.Left : MouseButtons.Right, 0, e.fX, e.fY, 0));
        }
        private void DoMouseDown(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("ScreenSaverForm_MouseDown()");
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

        private void ScreenSaverForm_MouseClick(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("ScreenSaverForm_MouseClick()");
            ShouldExit();
        }
        private void ScreenSaverForm_MouseUp(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("ScreenSaverForm_MouseUp()");
        }
        
        private void player_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            Trace.WriteLine("player_MouseMoveEvent()");
            ScreenSaverForm_MouseMove(sender, new MouseEventArgs(MouseButtons.None, 0, e.fX, e.fY, 0));
        }
        
        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("btnClose_MouseMove()");
            this.Cursor = Cursors.Default;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("btnClose_Click()");
            Application.Exit();
        }

        private void btnSettings_MouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("btnSettings_MouseMove()");
            this.Cursor = Cursors.Default;
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("btnSettings_Click()");
            if (settingsFrm == null)
            {
                settingsFrm = new SettingsForm();
                var result = settingsFrm.ShowDialog();
                DialogResult = DialogResult.Ignore;
                settingsFrm = null;
            }
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            Trace.WriteLine("ScreenSaverForm_MouseMove()");
            lastInteraction = DateTime.Now;

            if (!windowMode)
            {
                if (!mouseLocation.IsEmpty)
                {
                    // Terminate if mouse is moved a significant distance
                    if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                        Math.Abs(mouseLocation.Y - e.Y) > 5)
                        ShouldExit();
                }
                // Update current mouse location
                mouseLocation = e.Location;
            }
            else
            {
                Point m = PointToClient(Cursor.Position);
                int drag = 10;
                bool? toTop = m.Y < drag ? true : (m.Y > (Size.Height - drag) ? false : (bool?)null);
                bool? toLeft = m.X < drag ? true : (m.X > (Size.Width - drag) ? false : (bool?)null);

                if (toTop == true && toLeft == true) Cursor = Cursors.SizeNWSE;
                if (toTop == true && toLeft == null) Cursor = Cursors.SizeNS;
                if (toTop == true && toLeft == false) Cursor = Cursors.SizeNESW;

                if (toTop == false && toLeft == true) Cursor = Cursors.SizeNESW;
                if (toTop == false && toLeft == null) Cursor = Cursors.SizeNS;
                if (toTop == false && toLeft == false) Cursor = Cursors.SizeNWSE;

                if (toTop == null && toLeft == true) Cursor = Cursors.SizeWE;
                if (toTop == null && toLeft == null) Cursor = Cursors.Default;
                if (toTop == null && toLeft == false) Cursor = Cursors.SizeWE;

                ShowButtons();
            }

        }

        void ShowButtons(bool visibility = true)
        {
            btnClose.Visible = visibility;
            btnSettings.Visible = visibility;
        }


        #endregion

        #region Video player
        private void MaximizeVideo()
        {
            var screenArea = Screen.FromControl(this).WorkingArea;
            var videoSize = this.Size;
            if (screenArea.Size.Width > videoSize.Width && screenArea.Height > videoSize.Height)
            {
                videoSize = new Size(1920, 1080);
            }

            if (new RegSettings().MultiMonitorMode == RegSettings.MultiMonitorModeEnum.SpanAll)
            {
                // find edges of all monitors
                var topMost = Screen.AllScreens.Min(x => x.Bounds.Top);
                var leftMost = Screen.AllScreens.Min(x => x.Bounds.Left);
                var bottomMost = Screen.AllScreens.Max(x => x.Bounds.Bottom);
                var rightMost = Screen.AllScreens.Max(x => x.Bounds.Right);

                this.SetBounds(
                    leftMost, topMost,
                    rightMost - leftMost,
                    bottomMost - topMost);
            }
            else
            {
                this.SetBounds(
                    (screenArea.Width - videoSize.Width) / 2,
                    (screenArea.Height - videoSize.Height) / 2,
                    videoSize.Width,
                    videoSize.Height);
            }
        }


        private void SetNextVideo()
        {
            Trace.WriteLine("SetNextVideo()");
            var cacheEnabled = new RegSettings().CacheVideos;
            if (showVideo)
            {
                //If movies is null, when we tried to parse the JSON doc with the movies it
                //failed or it was empty.
                if (Movies == null || Movies.Count == 0)
                {
                    showVideo = false;
                    MessageBox.Show("Error finding the video locations.  Please confirm that the video source " +
                        "is a valid JSON document and can be reached.  Resart after fixing the video source");

                    return;
                }

                string url = Movies[currentVideoIndex].url;

                if (Caching.IsHit(url))
                {
                    player.URL = Caching.Get(url);
                }
                else
                {
                    player.URL = url;
                    if (cacheEnabled && shouldCache && 
                        !previewMode &&  !Caching.IsCaching(url)) {
                        Caching.StartDelayedCache(url);
                    }
                }
                currentVideoIndex++;
                if (currentVideoIndex >= Movies.Count)
                    currentVideoIndex = 0;
            }
        }

        private void NextVideoTimer_Tick(object sender, EventArgs e)
        {
            // Trace.WriteLine("Timer: " + state);
            var state = this.player.playState;
            if (state == WMPLib.WMPPlayState.wmppsReady ||
                state == WMPLib.WMPPlayState.wmppsUndefined ||
                state == WMPLib.WMPPlayState.wmppsStopped)
            {
                SetNextVideo();
            }
            if (lastInteraction.AddSeconds(1) < DateTime.Now)
            {
                ShowButtons(false);
            }
        }

        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            NativeMethods.EnableMonitorSleep();
        }

        private void LayoutPlayer()
        {
            this.player.enableContextMenu = false;
            this.player.settings.autoStart = true;
            this.player.settings.enableErrorDialogs = true;
            this.player.stretchToFit = true;
            this.player.uiMode = "none";
            Application.AddMessageFilter(new IgnoreMouseClickMessageFilter(this, player));

            ResizePlayer();
        }
        
        /// <summary>
        /// Resize & center player
        /// </summary>
        private void ResizePlayer()
        {
            this.player.Size = CalculateVideoFillSize(this.Size);
            this.player.Top = (this.Size.Height / 2) - (this.player.Size.Height / 2);
            this.player.Left =  (this.Size.Width / 2) - (this.player.Size.Width / 2);
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

#endregion


        /// <summary>
        /// Exits if not in windowed or preview mode.
        /// </summary>
        void ShouldExit()
        {
            if (!previewMode && !windowMode)
                Application.Exit();
        }

    }
}
