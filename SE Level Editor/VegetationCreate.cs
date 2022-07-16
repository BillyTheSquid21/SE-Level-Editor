using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Level_Editor
{
    class VegetationCreate : Form
    {
        private Label label1;
        private ComboBox comboBox1;
        private BatchEntityType selectedType = BatchEntityType.NULL;

        //Data cache
        List<object> properties;
        private Button button1;
        private Panel panel2;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private NumericUpDown numericUpDown7;
        private NumericUpDown numericUpDown6;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown4;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private NumericUpDown numericUpDown2;
        private Label label3;
        private NumericUpDown numericUpDown3;
        private Label label4;
        private Panel panel1;
        private GridControl gridControl1;
        private Label label10;
        private CheckBox checkBox1;
        private NumericUpDown numericUpDown8;
        private Label label9;
        List<object> instances;

        public VegetationCreate()
        {
            this.InitializeComponent();
            this.gridControl1.SetCallback(clickCallback);
        }

        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl1 = new Level_Editor.GridControl();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Grass",
            "Trees"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 531);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.numericUpDown7);
            this.panel2.Controls.Add(this.numericUpDown6);
            this.panel2.Controls.Add(this.numericUpDown5);
            this.panel2.Controls.Add(this.numericUpDown4);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(2, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(418, 206);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(344, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Right Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(344, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Right X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(344, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Left Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(344, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Left X";
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(218, 85);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown7.TabIndex = 4;
            this.numericUpDown7.ValueChanged += new System.EventHandler(this.numericUpDown7_ValueChanged);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(218, 58);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown6.TabIndex = 3;
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(218, 31);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown5.TabIndex = 2;
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(218, 3);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown4.TabIndex = 1;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(11, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(192, 192);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(11, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(192, 192);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(218, 4);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Frame";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(218, 31);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Texture X";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(218, 58);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 5;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Texture Y";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.numericUpDown3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(2, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 198);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.AutoScroll = true;
            this.gridControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.gridControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.gridControl1.Location = new System.Drawing.Point(426, 0);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(622, 566);
            this.gridControl1.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 273);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(335, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Values should be changed before placing and cannot be viewed later";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 253);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(135, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Align with terrain height";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Location = new System.Drawing.Point(166, 252);
            this.numericUpDown8.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown8.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(292, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Height";
            // 
            // VegetationCreate
            // 
            this.ClientSize = new System.Drawing.Size(1048, 566);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDown8);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "VegetationCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private bool CheckIfEntityExists(BatchEntityType type)
        {
            foreach (BatchEntity entity in EditorData.currentLevelBatchEntities)
            {
                if (entity.type == type)
                {
                    return true;
                }
            }
            return false;
        }

        private void LoadGrassIcon()
        {
            //Load icon
            Bitmap bmp = (Bitmap)EditorData.currentTilesetImages[(int)this.numericUpDown2.Value, (int)this.numericUpDown3.Value];
            Bitmap resizedBmp = new Bitmap(192, 192);
            Graphics g = Graphics.FromImage((Image)resizedBmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImage(bmp, 0, 0, 192, 192);
            this.pictureBox1.Image = resizedBmp;
        }

        private void LoadTreeIcon()
        {
            //Create merged bmp
            Bitmap lowerLeft = (Bitmap)EditorData.currentTilesetImages[(int)this.numericUpDown4.Value, (int)this.numericUpDown5.Value];
            Bitmap upperLeft = (Bitmap)EditorData.currentTilesetImages[(int)this.numericUpDown4.Value, (int)this.numericUpDown5.Value + 1];
            Bitmap lowerRight = (Bitmap)EditorData.currentTilesetImages[(int)this.numericUpDown6.Value, (int)this.numericUpDown7.Value];
            Bitmap upperRight = (Bitmap)EditorData.currentTilesetImages[(int)this.numericUpDown6.Value, (int)this.numericUpDown7.Value + 1];

            Bitmap bmp = new Bitmap(lowerLeft.Width + lowerRight.Width, lowerLeft.Height + upperLeft.Height);
            using (Graphics graphic = Graphics.FromImage(bmp))
            {
                graphic.DrawImage(upperLeft, 0, 0);
                graphic.DrawImage(lowerLeft, 0, lowerLeft.Height);
                graphic.DrawImage(upperRight, lowerLeft.Width, 0);
                graphic.DrawImage(lowerRight, lowerLeft.Width, lowerRight.Height);
            }

            //Load icon
            Bitmap resizedBmp = new Bitmap(192, 192);
            Graphics resizedGraphic = Graphics.FromImage((Image)resizedBmp);
            resizedGraphic.InterpolationMode = InterpolationMode.NearestNeighbor;
            resizedGraphic.DrawImage(bmp, 0, 0, 191, 192);
            this.pictureBox2.Image = resizedBmp;
        }

        private void LoadGrass()
        {
            foreach (BatchEntity entity in EditorData.currentLevelBatchEntities)
            {
                if (entity.type == BatchEntityType.Grasses)
                {
                    //Load first frame
                    this.numericUpDown1.Value = 1;
                    Tile frame1 = (Tile)entity.properties[1];
                    this.numericUpDown2.Value = frame1.x;
                    this.numericUpDown2.Value = frame1.y;
                    this.properties = entity.properties;
                    this.instances = entity.instances;
                    this.LoadGrassIcon();
                }
            }
        }

        private void LoadTrees()
        {
            foreach (BatchEntity entity in EditorData.currentLevelBatchEntities)
            {
                if (entity.type == BatchEntityType.Trees)
                {
                    this.properties = entity.properties;
                    this.instances = entity.instances;
                }
            }
            this.LoadTreeIcon();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedType = BatchEntity.GetEntityType((string)((ComboBox)sender).SelectedItem);
            string tag = "Level" + EditorData.currentLevelID;
            switch (this.selectedType)
            {
                case BatchEntityType.Grasses:
                    if (!CheckIfEntityExists(BatchEntityType.Grasses))
                    {
                        if (!LevelEditorCommands.ConfirmMessage("Add grass to level?"))
                        {
                            break;
                        }
                        tag += "_grass";
                        BatchEntity grass = new BatchEntity(BatchEntityType.Grasses, tag);
                        BatchEntity.CreateDefaultBatchEntity(ref grass);
                        EditorData.currentLevelBatchEntities.Add(grass);
                    }
                    this.LoadGrass();
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    this.gridControl1.SetGrid(EditorData.currentLevelWidth, EditorData.currentLevelHeight);
                    this.gridControl1.PaintGrid(EditorData.currentTilesetImages, EditorData.currentLevelTextures);
                    foreach(object entity in this.instances)
                    {
                        this.gridControl1.ColorPictureBoxAt(((Grass)entity).tile, Color.FromArgb(55, 210, 55));
                    }
                    break;
                case BatchEntityType.Trees:
                    if (!CheckIfEntityExists(BatchEntityType.Trees))
                    {
                        if (!LevelEditorCommands.ConfirmMessage("Add trees to level?"))
                        {
                            break;
                        }
                        tag += "_trees";
                        BatchEntity tree = new BatchEntity(BatchEntityType.Trees, tag);
                        BatchEntity.CreateDefaultBatchEntity(ref tree);
                        EditorData.currentLevelBatchEntities.Add(tree);
                    }
                    this.LoadTrees();
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    this.gridControl1.SetGrid(EditorData.currentLevelWidth, EditorData.currentLevelHeight);
                    this.gridControl1.PaintGrid(EditorData.currentTilesetImages, EditorData.currentLevelTextures);
                    foreach (object entity in this.instances)
                    {
                        this.gridControl1.ColorPictureBoxAt(((Tree)entity).tile, Color.FromArgb(55, 210, 55));
                    }
                    break;
                default:
                    break;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (this.properties == null)
            {
                return;
            }
            this.LoadGrassIcon();
            Tile tile = (Tile)this.properties[(int)this.numericUpDown1.Value];
            tile.x = (uint)this.numericUpDown2.Value;
            this.properties[(int)this.numericUpDown1.Value] = tile;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (this.properties == null)
            {
                return;
            }
            this.LoadGrassIcon();
            Tile tile = (Tile)this.properties[(int)this.numericUpDown1.Value];
            tile.y = (uint)this.numericUpDown3.Value;
            this.properties[(int)this.numericUpDown1.Value] = tile;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(BatchEntity entity in EditorData.currentLevelBatchEntities)
            {
                if (entity.type == this.selectedType)
                {
                    entity.properties = this.properties;
                    entity.instances = this.instances;
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (this.properties == null)
            {
                return;
            }
            Tile tile = (Tile)this.properties[(int)this.numericUpDown1.Value];
            this.numericUpDown2.Value = tile.x;
            this.numericUpDown3.Value = tile.y;
            this.LoadGrassIcon();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            this.LoadTreeIcon();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            this.LoadTreeIcon();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            this.LoadTreeIcon();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            this.LoadTreeIcon();
        }

        private void clickCallback()
        {
            InteractionType selection = gridControl1.selection.type;
            Tile tile = gridControl1.selection.tile;
            if (selection == InteractionType.NULL)
            {
                return;
            }
            else if (selection == InteractionType.LeftClick && this.selectedType == BatchEntityType.Grasses)
            {
                //Check if there is a grass at that location already
                for (int i = 0; i < this.instances.Count; i++)
                {
                    Tile grassTile = ((Grass)this.instances[i]).tile;
                    if (grassTile.x == tile.x && grassTile.y == tile.y)
                    {
                        return;
                    }
                }
                //If not, add grass with values from numeric boxes
                Grass grass = new Grass();
                grass.tile = tile;
                if (this.checkBox1.Checked)
                {
                    grass.height = EditorData.currentLevelHeights[tile.x, tile.y];
                }
                else
                {
                    grass.height = (int)this.numericUpDown8.Value;
                }
                this.gridControl1.ColorPictureBoxAt(tile, Color.FromArgb(55, 210, 55));
                this.instances.Add(grass);
                int count = (int)this.properties[0];
                count++;
                this.properties[0] = count;
            }
            else if (selection == InteractionType.RightClick && this.selectedType == BatchEntityType.Grasses)
            {
                //Check if there is a grass at that location already
                bool found = false;
                for (int i = 0; i < this.instances.Count; i++)
                {
                    Tile grassTile = ((Grass)this.instances[i]).tile;
                    if (grassTile.x == tile.x && grassTile.y == tile.y)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
                for(int i = 0; i < this.instances.Count; i++)
                {
                    Tile instanceTile = ((Grass)this.instances[i]).tile;
                    if (instanceTile.x == tile.x && instanceTile.y == tile.y)
                    {
                        this.instances.RemoveAt(i);
                        int count = (int)this.properties[0];
                        count--;
                        this.properties[0] = count;
                        break;
                    }
                }
                this.gridControl1.ColorPictureBoxAt(tile, Color.FromArgb(255, 155, 155));
            }
            else if (selection == InteractionType.LeftClick && this.selectedType == BatchEntityType.Trees)
            {
                //Check if there is a grass at that location already
                for (int i = 0; i < this.instances.Count; i++)
                {
                    Tile treeTile = ((Tree)this.instances[i]).tile;
                    if (treeTile.x == tile.x && treeTile.y == tile.y)
                    {
                        return;
                    }
                }
                //If not, add grass with values from numeric boxes
                Tree tree = new Tree();
                tree.tile = tile;
                tree.firstHalfTexture.x = (uint)this.numericUpDown4.Value; tree.firstHalfTexture.y = (uint)this.numericUpDown5.Value;
                tree.secondHalfTexture.x = (uint)this.numericUpDown6.Value; tree.secondHalfTexture.y = (uint)this.numericUpDown7.Value;
                tree.textureWidth = 32; tree.textureHeight = 64;
                if (this.checkBox1.Checked)
                {
                    tree.height = EditorData.currentLevelHeights[tile.x, tile.y];
                }
                else
                {
                    tree.height = (int)this.numericUpDown8.Value;
                }
                this.gridControl1.ColorPictureBoxAt(tile, Color.FromArgb(55, 210, 55));
                this.instances.Add(tree);
                int count = (int)this.properties[0];
                count++;
                this.properties[0] = count;
            }
            else if (selection == InteractionType.RightClick && this.selectedType == BatchEntityType.Trees)
            {
                //Check if there is a grass at that location already
                bool found = false;
                for (int i = 0; i < this.instances.Count; i++)
                {
                    Tile treeTile = ((Tree)this.instances[i]).tile;
                    if (treeTile.x == tile.x && treeTile.y == tile.y)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return;
                }
                for (int i = 0; i < this.instances.Count; i++)
                {
                    Tile instanceTile = ((Tree)this.instances[i]).tile;
                    if (instanceTile.x == tile.x && instanceTile.y == tile.y)
                    {
                        this.instances.RemoveAt(i);
                        int count = (int)this.properties[0];
                        count--;
                        this.properties[0] = count;
                        break;
                    }
                }
                this.gridControl1.ColorPictureBoxAt(tile, Color.FromArgb(255, 155, 155));
            }
        }
    }
}
