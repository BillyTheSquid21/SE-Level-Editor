using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic;

namespace Level_Editor
{
    public partial class ApplicationGUI : Form
    {

        public ApplicationGUI()
        {
            InitializeComponent();
            PopulateTreeView();
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.listView1.AllowColumnReorder = false;
            this.heightPanel1.LinkWorkspace(this.workspace1);
        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory()+"/Projects");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        public void RepopulateTreeView(string path)
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(path);
            if (info.Exists)
            {
                //Clear old nodes
                treeView1.Nodes.Clear();

                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs,
            TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory")};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File")};

                item.SubItems.AddRange(subItems);
                item.Tag = file.FullName;
                listView1.Items.Add(item);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Normal;
            return;
        }

        private void TextureButton_Click(object sender, EventArgs e)
        {
            string path = LevelEditorCommands.OpenFileDialog("PNG files (*.png*)|*.png*");
            EditorData.tilesetPath = path;
            this.texturePanel1.DisplayTileset(path);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listView1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                //Gets filepath of selected item
                EditorData.selectedListPath = (string)listView1.Items[intselectedindex].Tag;
                //TODO - make do more than select level
                if (EditorData.selectedListPath == null)
                {
                    return;
                }
                if (EditorData.selectedListPath.Contains(".json") && EditorData.selectedListPath.Contains("level"))
                {
                    //Check if tileset loaded
                    if (EditorData.currentTilesetImages == null)
                    {
                        LevelEditorCommands.ErrorMessage("No tileset loaded!");
                        return;
                    }
                    this.heightPanel1.ClearButtons();
                    LevelEditorCommands.LoadLevel(EditorData.selectedListPath);
                    this.label9.Text = EditorData.currentLevelID.ToString();
                    this.label10.Text = EditorData.currentLevelWidth.ToString();
                    this.label11.Text = EditorData.currentLevelHeight.ToString();
                    this.label12.Text = EditorData.currentLevelOriginX.ToString();
                    this.label13.Text = EditorData.currentLevelOriginZ.ToString();
                    this.workspace1.LoadLevel();
                    this.heightPanel1.AddHeight(EditorData.currentLevelWorldHeights);
                    EditorData.brushMode = BrushMode.TEXTURE;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = LevelEditorCommands.OpenDirectoryDialog();
            if (path != null)
            {
                RepopulateTreeView(path);
            }
        }

        private void treeView1_Resize(object sender, EventArgs e)
        {
            //Resizes button along with it
            TreeView tV = (TreeView)sender;
            button1.Width = tV.Width - 4;
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            //Resizes button along with it
            ListView tV = (ListView)sender;
            button2.Width = tV.Width - 4;
            button2.Location = new Point(tV.Location.X + treeView1.Width + 4,tV.Location.Y + tV.Height + 7);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string rootDir = LevelEditorCommands.OpenDirectoryDialog();
            if (rootDir != null)
            {
                string name = Interaction.InputBox("Project Name:", "", "", this.Location.X + this.Width/2, this.Location.Y + this.Height/2);
                if (name == null || name == "")
                {
                    LevelEditorCommands.ErrorMessage("Invalid project name!");
                    return;
                }
                LevelEditorCommands.GenerateProject(name, rootDir);
                this.RepopulateTreeView(Directory.GetCurrentDirectory());
            }
        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Get path of current dir
                if (treeView1.SelectedNode == null)
                {
                    return;
                }
                DirectoryInfo info = (DirectoryInfo)treeView1.SelectedNode.Tag;
                LevelEditorCommands.OpenCreateFileDialog(info.FullName);
                this.PopulateTreeView();
            }
        }

        private void writeCurrentLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditorData.currentLevelPath == null)
            {
                return;
            }
            LevelEditorCommands.WriteLevel();
        }

        private void textureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.workspace1.SetBrushMode(BrushMode.TEXTURE);
        }

        private void permissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.workspace1.SetBrushMode(BrushMode.PERMISSION);
        }

        private void directionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.workspace1.SetBrushMode(BrushMode.DIRECTION);
        }

        private void heightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.workspace1.SetBrushMode(BrushMode.HEIGHT);
        }

        private void manageVegetationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditorData.currentLevelID == -1)
            {
                LevelEditorCommands.ErrorMessage("Level not loaded!");
                return;
            }
            VegetationCreate vegetationCreate = new VegetationCreate();
            vegetationCreate.Show();
        }

        private void addLoadingZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadingZoneCreate loadingZoneCreate = new LoadingZoneCreate();
            loadingZoneCreate.Show();
        }

        private void dViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var numThreads = 20;
            var toProcess = numThreads;

            var resetEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(delegate (object state) {
                    ModelWindow.RunWindow();
                    if (Interlocked.Decrement(ref toProcess) == 0) resetEvent.Set();
                }), null);
        }
    }
}
