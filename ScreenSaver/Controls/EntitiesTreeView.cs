using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Aerial.Controls
{
    public class EntitiesTreeView : TreeView
    {
        private Dictionary<string, Asset> Movies;

        protected override void WndProc(ref Message m)
        {
            // Filter WM_LBUTTONDBLCLK
            if (m.Msg != 0x203) base.WndProc(ref m);
        }

        public EntitiesTreeView()
        {
            CheckBoxes = true;
            FullRowSelect = true;
            Location = new System.Drawing.Point(7, 19);
            Name = "tvMovies";
            ShowLines = false;
            ShowPlusMinus = false;
            Size = new System.Drawing.Size(138, 229);
            TabIndex = 15;
            //AfterSelect += new TreeViewEventHandler(AfterSelect);
            //AfterCheck += new TreeViewEventHandler(tvMovies_AfterCheck);

        }

        public void BuildTree(List<Asset> movies)
        {
            var selected = new RegSettings().ChosenMovies.Split(';').ToList();
            TreeNode root = new TreeNode(movies[0].accessibilityLabel);
            Nodes.Add(root);
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
                    Nodes.Add(root);
                }
                // add node
                var newNode = new TreeNode(m.TimeNumbered());
                root.Nodes.Add(newNode);
                newNode.Checked = selected.Contains(newNode.FullPath);
                allChecked = allChecked && newNode.Checked;
                Movies.Add(root.Nodes[root.Nodes.Count - 1].FullPath, m);
            }

            ExpandAll();
        }
    }
}
