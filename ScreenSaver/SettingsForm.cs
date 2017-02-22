using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Aerial;
using System.Diagnostics;
using System.Collections.Generic;

namespace ScreenSaver
{
    public partial class SettingsForm : Form
    {
        private Dictionary<string, Asset> Movies;
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        /// <summary>
        /// Load display text from the Registry
        /// </summary>
        private void LoadSettings()
        {
            var settings = new RegSettings();
            chkDifferentMonitorMovies.Checked = settings.DifferentMoviesOnDual;
            chkUseTimeOfDay.Checked = settings.UseTimeOfDay;
            chkMultiscreenDisabled.Checked = settings.MultiscreenDisabled;
            chkCacheVideos.Checked = settings.CacheVideos;

            if(settings.CacheLocation == null || settings.CacheLocation == "")
            {
                txtCacheFolderPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aerial").ToString();
            }
            else
            {
                txtCacheFolderPath.Text = settings.CacheLocation;
            }
            
            changeCacheLocationButton.Enabled = settings.CacheVideos;

            ShowSpace();

            PopulateChosenVideoGroup();
            //HideChosenVideoGroup();

            InitPlayer();

            StartPlayer();
        }

        private void InitPlayer()
        {
            this.player.enableContextMenu = false;
            this.player.settings.autoStart = true;
            this.player.settings.enableErrorDialogs = true;
            this.player.stretchToFit = true;
            this.player.uiMode = "none";
        }

        private void StartPlayer()
        {
            tvChosen.SelectedNode = tvChosen.Nodes[0].Nodes[0];
            tvChosen.Select();
            tvChosen.TopNode.EnsureVisible();
            tvChosen.Nodes[0].EnsureVisible();
        }

        private void PopulateChosenVideoGroup()
        {
            var movies = AerialContext.GetAllMovies();
            movies.Sort();
            if (movies.Count == 0) return; // error

            AddHumanNumbers(movies);
            BuildTree(movies);
            tvChosen.BuildTree(movies);
            tvMovies.AfterCheck += new TreeViewEventHandler(tvMovies_AfterCheck);

            tvMovies.ExpandAll();
            tvMovies.CheckBoxes = true;
        }

        private void BuildTree(List<Asset> movies)
        {
            var selected = new RegSettings().ChosenMovies.Split(';').ToList();
            TreeNode root = new TreeNode(movies[0].accessibilityLabel);
            tvMovies.Nodes.Add(root);
            Movies = new Dictionary<string, Asset>();
            bool allChecked = true;
            foreach (var m in movies)
            {
                if (m.accessibilityLabel != root.Text)
                {
                    // checked root
                    if (allChecked) root.Checked = true;
                    // new root
                    root = new TreeNode(m.accessibilityLabel);
                    tvMovies.Nodes.Add(root);
                }
                // add node
                var newNode = new TreeNode(m.TimeNumbered());
                root.Nodes.Add(newNode);
                newNode.Checked = selected.Contains(newNode.FullPath);
                allChecked = allChecked && newNode.Checked;
                Movies.Add(root.Nodes[root.Nodes.Count - 1].FullPath, m);
            }
        }

        private string ConcatChosenEntities()
        {
            var selected = "";
            foreach (TreeNode root in tvMovies.Nodes)
                foreach(TreeNode n in root.Nodes)
                    if (n.Checked)
                        selected += ";" + n.FullPath;
            
            return selected + ";";
        }

        private static void AddHumanNumbers(List<Asset> movies)
        {
            int n = 1;
            for (int i = 1; i < movies.Count; i++)
            {
                if (movies[i - 1].ShortName() == movies[i].ShortName())
                {
                    movies[i - 1].numeric = n;
                    n++;
                    movies[i].numeric = n;
                }
                else
                {
                    if (n != 1)
                    {
                        movies[i - 1].numeric = n;
                        n = 1;
                        movies[i].numeric = n;
                    }
                    else
                    {
                        movies[i - 1].numeric = 0;
                        movies[i].numeric = n;
                    }
                }
            }
            if (movies.Count > 0 && movies.Last().numeric == 1) movies.Last().numeric = 0;
        }

        void HideChosenVideoGroup()
        {
            // while developing
            tabs.TabPages.Remove(tabAbout);
            grpChosenVideos.Hide();
        }

        private void ShowSpace()
        {
            var cacheSize = NativeMethods.GetExplorerFileSize(Caching.GetDirectorySize());
            lblCacheSize.Text = "Current Cache Size: " + cacheSize;

            var cacheFree = NativeMethods.GetExplorerFileSize(Caching.CacheSpace());
            lblFreeSpace.Text = "Free Space Available on drive: " + cacheFree;

        }

        /// <summary>
        /// Save text into the Registry.
        /// </summary>
        private void SaveSettings()
        {
            var settings = new RegSettings();
            settings.DifferentMoviesOnDual = chkDifferentMonitorMovies.Checked;
            settings.UseTimeOfDay = chkUseTimeOfDay.Checked;
            settings.MultiscreenDisabled = chkMultiscreenDisabled.Checked;
            settings.CacheVideos = chkCacheVideos.Checked;

            string oldCacheDirectory = settings.CacheLocation;
            settings.CacheLocation = txtCacheFolderPath.Text;

            settings.ChosenMovies = ConcatChosenEntities();

            settings.SaveSettings();

            Caching.UpdateCachePath(oldCacheDirectory, settings.CacheLocation);
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void changeCacheLocationButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtCacheFolderPath.Text;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCacheFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
            ShowSpace();
        }

        private void chkCacheVideos_CheckedChanged(object sender, EventArgs e)
        {
            changeCacheLocationButton.Enabled = chkCacheVideos.Checked;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.lblVersion.Text = "Current Version " + AssemblyVersion.ExecutingAssemblyVersion + " (" + AssemblyVersion.CompileDate + ")";
        }

        private void lblVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // todo get latest builds from json interface: https://api.github.com/repos/cdima/aerial/releases/latest
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/cDima/Aerial/releases");
            Process.Start(sInfo);
        }

        private void btnOpenCache_Click(object sender, EventArgs e)
        {
            Process.Start(Caching.CacheFolder);
        }

        private void btnPurgeCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all cached files?", "Delete Cache?") == DialogResult.OK)
            {
                Caching.DeleteCache();
            }
            ShowSpace();
        }

        private void tvMovies_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Trace.WriteLine("Selected tree element " + e.Node.FullPath);
            if (cbLivePreview.Checked && e.Node.FullPath.Contains("\\"))
            {
                string url = Movies[e.Node.FullPath].url;
                player.URL = Caching.TryHit(url);
            }
        }

        private void timerDiskUpdate_Tick(object sender, EventArgs e)
        {
            ShowSpace();
        }

        bool updatingChecked = false;
        private void tvMovies_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (updatingChecked) return;

            updatingChecked = true;

            if (!e.Node.FullPath.Contains("\\"))
            {
                foreach (TreeNode node in e.Node.Nodes)
                    node.Checked = e.Node.Checked;
            }
            else
            {
                foreach(TreeNode n in e.Node.Parent.Nodes)
                {
                    if (!n.Checked)
                    {
                        e.Node.Parent.Checked = false;
                        return;
                    }
                }
                e.Node.Parent.Checked = true;
            }

            updatingChecked = false;
        }
    }
}
