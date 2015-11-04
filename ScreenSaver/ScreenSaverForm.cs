using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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


        private Point mouseLocation;
        private bool previewMode = false;
        private Random rand = new Random();

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
            TopMost = true;
            
            // ex: http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/b2-1.mov
            this.axWindowsMediaPlayer1.settings.autoStart = true;
            this.axWindowsMediaPlayer1.settings.enableErrorDialogs = true;
            this.axWindowsMediaPlayer1.uiMode = "none";
            this.axWindowsMediaPlayer1.enableContextMenu = false;
            Application.AddMessageFilter(new IgnoreMouseClickMessageFilter(this, axWindowsMediaPlayer1));

            // Hack to eliminate black bars around videos
            // * Get the aspect ratio of the videos
            // * Use that to calculate the target width of the form necessary for the video to fill the height
            // * Finally adjust the position of the form to the left the required amount to centre the video (half the difference between the target width and the actual screen width)
            float videoAspectRatio = (float)1920/(float)1080;
            float targetWidth = (float)this.Size.Height * videoAspectRatio;
            Size tempSize = this.Size;
            tempSize.Width = Convert.ToInt32(Math.Ceiling(targetWidth));
            this.axWindowsMediaPlayer1.Size = tempSize;
            this.axWindowsMediaPlayer1.Top = 0;
            this.axWindowsMediaPlayer1.Left = -(tempSize.Width - this.Size.Width) / 2;
            this.axWindowsMediaPlayer1.settings.setMode("loop", true);
            this.axWindowsMediaPlayer1.MouseMoveEvent += AxWindowsMediaPlayer1_MouseMoveEvent;
            this.axWindowsMediaPlayer1.KeyPressEvent += AxWindowsMediaPlayer1_KeyPressEvent;


            var list = axWindowsMediaPlayer1.playlistCollection.newPlaylist("Aerial");

            var movies = new AerialContext().GetMovies();
            foreach (var item in movies)
            {
                var m = axWindowsMediaPlayer1.newMedia(item.url);
                list.appendItem(m);
            }

            axWindowsMediaPlayer1.currentPlaylist = list;

            //this.axWindowsMediaPlayer1.URL = @"http://a1.phobos.apple.com/us/r1000/000/Features/atv/AutumnResources/videos/b2-1.mov";
            this.axWindowsMediaPlayer1.Ctlcontrols.play();
            
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
