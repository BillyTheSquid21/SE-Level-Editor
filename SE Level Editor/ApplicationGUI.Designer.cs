
namespace Level_Editor
{
    partial class ApplicationGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationGUI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolstripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageVegetationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLoadingZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeCurrentLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBrushModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.permissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tilesetLabel = new System.Windows.Forms.Label();
            this.texturePanel1 = new Level_Editor.TexturePanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.helpfulInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heightPanel1 = new Level_Editor.HeightPanel();
            this.workspace1 = new Level_Editor.Workspace();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1384, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolstripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolstripMenuItem
            // 
            this.exitToolstripMenuItem.Name = "exitToolstripMenuItem";
            this.exitToolstripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolstripMenuItem.Text = "Exit";
            this.exitToolstripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullscreenToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.fullscreenToolStripMenuItem.Text = "Exit fullscreen";
            this.fullscreenToolStripMenuItem.Click += new System.EventHandler(this.fullscreenToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectsToolStripMenuItem,
            this.globalsToolStripMenuItem,
            this.writeCurrentLevelToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // objectsToolStripMenuItem
            // 
            this.objectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageVegetationToolStripMenuItem});
            this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            this.objectsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.objectsToolStripMenuItem.Text = "Objects";
            // 
            // manageVegetationToolStripMenuItem
            // 
            this.manageVegetationToolStripMenuItem.Name = "manageVegetationToolStripMenuItem";
            this.manageVegetationToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.manageVegetationToolStripMenuItem.Text = "Manage Vegetation";
            this.manageVegetationToolStripMenuItem.Click += new System.EventHandler(this.manageVegetationToolStripMenuItem_Click);
            // 
            // globalsToolStripMenuItem
            // 
            this.globalsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLoadingZoneToolStripMenuItem});
            this.globalsToolStripMenuItem.Name = "globalsToolStripMenuItem";
            this.globalsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.globalsToolStripMenuItem.Text = "Globals";
            // 
            // addLoadingZoneToolStripMenuItem
            // 
            this.addLoadingZoneToolStripMenuItem.Name = "addLoadingZoneToolStripMenuItem";
            this.addLoadingZoneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addLoadingZoneToolStripMenuItem.Text = "Loading Zones";
            this.addLoadingZoneToolStripMenuItem.Click += new System.EventHandler(this.addLoadingZoneToolStripMenuItem_Click);
            // 
            // writeCurrentLevelToolStripMenuItem
            // 
            this.writeCurrentLevelToolStripMenuItem.Name = "writeCurrentLevelToolStripMenuItem";
            this.writeCurrentLevelToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.writeCurrentLevelToolStripMenuItem.Text = "Write Current Level";
            this.writeCurrentLevelToolStripMenuItem.Click += new System.EventHandler(this.writeCurrentLevelToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setBrushModeToolStripMenuItem,
            this.dViewerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // setBrushModeToolStripMenuItem
            // 
            this.setBrushModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textureToolStripMenuItem,
            this.permissionToolStripMenuItem,
            this.directionToolStripMenuItem,
            this.heightToolStripMenuItem});
            this.setBrushModeToolStripMenuItem.Name = "setBrushModeToolStripMenuItem";
            this.setBrushModeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setBrushModeToolStripMenuItem.Text = "Set brush mode";
            // 
            // textureToolStripMenuItem
            // 
            this.textureToolStripMenuItem.Name = "textureToolStripMenuItem";
            this.textureToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.textureToolStripMenuItem.Text = "Texture";
            this.textureToolStripMenuItem.Click += new System.EventHandler(this.textureToolStripMenuItem_Click);
            // 
            // permissionToolStripMenuItem
            // 
            this.permissionToolStripMenuItem.Name = "permissionToolStripMenuItem";
            this.permissionToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.permissionToolStripMenuItem.Text = "Permission";
            this.permissionToolStripMenuItem.Click += new System.EventHandler(this.permissionToolStripMenuItem_Click);
            // 
            // directionToolStripMenuItem
            // 
            this.directionToolStripMenuItem.Name = "directionToolStripMenuItem";
            this.directionToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.directionToolStripMenuItem.Text = "Direction";
            this.directionToolStripMenuItem.Click += new System.EventHandler(this.directionToolStripMenuItem_Click);
            // 
            // heightToolStripMenuItem
            // 
            this.heightToolStripMenuItem.Name = "heightToolStripMenuItem";
            this.heightToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.heightToolStripMenuItem.Text = "Height";
            this.heightToolStripMenuItem.Click += new System.EventHandler(this.heightToolStripMenuItem_Click);
            // 
            // dViewerToolStripMenuItem
            // 
            this.dViewerToolStripMenuItem.Name = "dViewerToolStripMenuItem";
            this.dViewerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dViewerToolStripMenuItem.Text = "3D Viewer";
            this.dViewerToolStripMenuItem.Click += new System.EventHandler(this.dViewerToolStripMenuItem_Click);
            // 
            // textureButton
            // 
            this.textureButton.Location = new System.Drawing.Point(6, 5);
            this.textureButton.Name = "textureButton";
            this.textureButton.Size = new System.Drawing.Size(227, 23);
            this.textureButton.TabIndex = 0;
            this.textureButton.Text = "Choose Tileset";
            this.textureButton.UseVisualStyleBackColor = true;
            this.textureButton.Click += new System.EventHandler(this.TextureButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tilesetLabel);
            this.panel2.Controls.Add(this.texturePanel1);
            this.panel2.Controls.Add(this.textureButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1148, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 857);
            this.panel2.TabIndex = 2;
            // 
            // tilesetLabel
            // 
            this.tilesetLabel.AutoSize = true;
            this.tilesetLabel.Location = new System.Drawing.Point(3, 805);
            this.tilesetLabel.Name = "tilesetLabel";
            this.tilesetLabel.Size = new System.Drawing.Size(69, 13);
            this.tilesetLabel.TabIndex = 2;
            this.tilesetLabel.Text = "Tileset Name";
            // 
            // texturePanel1
            // 
            this.texturePanel1.AutoScroll = true;
            this.texturePanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.texturePanel1.Location = new System.Drawing.Point(6, 34);
            this.texturePanel1.Name = "texturePanel1";
            this.texturePanel1.Size = new System.Drawing.Size(227, 768);
            this.texturePanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 857);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 534);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(322, 319);
            this.panel3.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(104, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(104, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(104, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(104, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "N/A";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Offset Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Offset X:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Height:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Width:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Current Level Info:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(194, 504);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Generate Project";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Change Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(322, 498);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(187, 498);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick_1);
            this.treeView1.Resize += new System.EventHandler(this.treeView1_Resize);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "level.png");
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(131, 498);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(326, 791);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select working height";
            // 
            // helpfulInfoToolStripMenuItem
            // 
            this.helpfulInfoToolStripMenuItem.Name = "helpfulInfoToolStripMenuItem";
            this.helpfulInfoToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.helpfulInfoToolStripMenuItem.Text = "Helpful Info";
            // 
            // heightPanel1
            // 
            this.heightPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.heightPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.heightPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.heightPanel1.Location = new System.Drawing.Point(326, 688);
            this.heightPanel1.Name = "heightPanel1";
            this.heightPanel1.Size = new System.Drawing.Size(822, 103);
            this.heightPanel1.TabIndex = 5;
            // 
            // workspace1
            // 
            this.workspace1.AutoScroll = true;
            this.workspace1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.workspace1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.workspace1.Dock = System.Windows.Forms.DockStyle.Top;
            this.workspace1.Location = new System.Drawing.Point(326, 24);
            this.workspace1.Name = "workspace1";
            this.workspace1.Size = new System.Drawing.Size(822, 664);
            this.workspace1.TabIndex = 4;
            // 
            // ApplicationGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1384, 881);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.heightPanel1);
            this.Controls.Add(this.workspace1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ApplicationGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SE Level Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolstripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.Button textureButton;
        private System.Windows.Forms.Panel panel2;
        private TexturePanel texturePanel1;
        private System.Windows.Forms.Label tilesetLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeCurrentLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBrushModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heightToolStripMenuItem;
        private Workspace workspace1;
        private HeightPanel heightPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem helpfulInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageVegetationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLoadingZoneToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem dViewerToolStripMenuItem;
    }
}

