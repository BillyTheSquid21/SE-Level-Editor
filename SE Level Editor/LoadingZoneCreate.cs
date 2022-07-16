using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Level_Editor
{
    class LoadingZoneCreate : Form
    {
        private Label label1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button button1;
        private GridControl gridControl1;

        struct ZoneTileStruct
        {
            public Tile tile;
            public int index;
        }

        private List<ZoneTileStruct> occupiedTiles;
        private Button button2;
        private LoadingZone selectedZone;
        private Button button3;
        private int selectedZoneIndex = -1;

        public LoadingZoneCreate()
        {
            if (EditorData.currentLevelPath == null)
            {
                LevelEditorCommands.ErrorMessage("No levels loaded, could not deduce desired global path!");
                return;
            }
            this.InitializeComponent();
            this.gridControl1.SetGrid(EditorData.currentLevelWidth, EditorData.currentLevelHeight);
            this.gridControl1.PaintGrid(EditorData.currentTilesetImages, EditorData.currentLevelTextures);
            this.gridControl1.SetCallback(clickCallback);
            this.occupiedTiles = new List<ZoneTileStruct>();
            if (EditorData.globalPath != null)
            {
                this.loadLoadingZones();
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.gridControl1 = new Level_Editor.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selected Zone Properties:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(16, 47);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(16, 82);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 7;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown3.Location = new System.Drawing.Point(16, 118);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 8;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown4.Location = new System.Drawing.Point(16, 158);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown4.TabIndex = 9;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(16, 199);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown5.TabIndex = 10;
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(16, 238);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown6.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown6.TabIndex = 11;
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Position X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Position Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(142, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Level ID 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Level ID 2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 274);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Load Relevant Zones";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 304);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Save Current Zone";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(16, 334);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(180, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Write Loading Zone Changes";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.AutoScroll = true;
            this.gridControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.gridControl1.Location = new System.Drawing.Point(224, 0);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(810, 582);
            this.gridControl1.TabIndex = 0;
            // 
            // LoadingZoneCreate
            // 
            this.ClientSize = new System.Drawing.Size(1034, 582);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown6);
            this.Controls.Add(this.numericUpDown5);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridControl1);
            this.Name = "LoadingZoneCreate";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EditorData.currentLevelGlobalEntities != null)
            {
                LevelEditorCommands.ErrorMessage("Globals already loaded!");
                return;
            }
            LevelSerialize.LoadGlobalObjectsFromFile(EditorData.currentLevelPath);
            this.loadLoadingZones();
        }

        public void loadLoadingZones()
        {
            this.occupiedTiles = new List<ZoneTileStruct>();
            this.gridControl1.SetGrid(EditorData.currentLevelWidth, EditorData.currentLevelHeight);
            this.gridControl1.PaintGrid(EditorData.currentTilesetImages, EditorData.currentLevelTextures);
            foreach (BatchEntity entity in EditorData.currentLevelGlobalEntities)
            {
                if (entity.type == BatchEntityType.LoadingZone)
                {
                    //Get data of any zone with current level id
                    //Take current level offset away from absolute position
                    //Convert absolute position to tile with inv z
                    int index = 0;
                    foreach(object zoneObject in entity.instances)
                    {
                        LoadingZone zone = (LoadingZone)zoneObject;
                        if (zone.level1ID != EditorData.currentLevelID && zone.level2ID != EditorData.currentLevelID)
                        {
                            continue;
                        }
                        zone.xOff -= EditorData.currentLevelOriginX;
                        zone.zOff -= EditorData.currentLevelOriginZ;

                        int tileX = zone.xOff / (int)Constants.TILE_SIZE;
                        int tileZ = LevelSerialize.InvertZTile(zone.zOff / (int)Constants.TILE_SIZE);

                        //Go along every tile it covers, if exists color
                        int xCount = (int)zone.width / (int)Constants.TILE_SIZE;
                        int zCount = (int)zone.height / (int)Constants.TILE_SIZE;

                        for (int z = 0; z < zCount; z++)
                        {
                            for (int x = 0; x < xCount; x++)
                            {
                                int xPos = tileX + x;
                                int zPos = tileZ - z;
                                Console.WriteLine(xPos);
                                Console.WriteLine(zPos);
                                if (xPos >= EditorData.currentLevelWidth || xPos < 0 
                                    || zPos >= EditorData.currentLevelHeight || zPos < 0)
                                {
                                    continue;
                                }
                                //If in bounds, convert to tile
                                Tile tile = new Tile();
                                tile.x = (uint)xPos;
                                tile.y = (uint)zPos;

                                //Color
                                this.gridControl1.ColorPictureBoxAt(tile, Color.FromArgb(55, 40, 240));

                                //Log tile location and index of zone instance linked to
                                ZoneTileStruct zts = new ZoneTileStruct();
                                zts.tile = tile;
                                zts.index = index;
                                this.occupiedTiles.Add(zts);
                            }
                        }
                        index++;
                    }
                }
            }
        }

        public void clickCallback()
        {
            if (EditorData.globalPath == null)
            {
                return;
            }
            GridSelection select = this.gridControl1.selection;
            if (loadZoneFromTile(select.tile))
            {
                return;
            }
            //Otherwise create new zone, occupying just current tile (selected index => -2 to show this
            if (!LevelEditorCommands.ConfirmMessage("Create new zone?"))
            {
                return;
            }
            LoadingZone zone = new LoadingZone();
            zone.xOff = ((int)select.tile.x * (int)Constants.TILE_SIZE) + EditorData.currentLevelOriginX;
            zone.zOff = LevelSerialize.InvertZTile((int)select.tile.y) * (int)Constants.TILE_SIZE + EditorData.currentLevelOriginZ;
            zone.width = Constants.TILE_SIZE * 2;
            zone.height = Constants.TILE_SIZE * 2;
            zone.level1ID = EditorData.currentLevelID;
            zone.level2ID = -1;
            this.selectedZone = zone;
            foreach (BatchEntity entity in EditorData.currentLevelGlobalEntities)
            {
                if (entity.type == BatchEntityType.LoadingZone)
                {
                    this.selectedZoneIndex = entity.instances.Count;
                    entity.instances.Add(zone);
                }
            }
            this.loadLoadingZones();
        }

        public bool loadZoneFromTile(Tile tile)
        {
            foreach(ZoneTileStruct zts in this.occupiedTiles)
            {
                if (tile.x == zts.tile.x && tile.y == zts.tile.y)
                {
                    foreach (BatchEntity entity in EditorData.currentLevelGlobalEntities)
                    {
                        if (entity.type == BatchEntityType.LoadingZone)
                        {
                            LoadingZone zone = (LoadingZone)entity.instances[zts.index];
                            this.numericUpDown1.Value = zone.xOff;
                            this.numericUpDown2.Value = zone.zOff;
                            this.numericUpDown3.Value = zone.width;
                            this.numericUpDown4.Value = zone.height;
                            this.numericUpDown5.Value = zone.level1ID;
                            this.numericUpDown6.Value = zone.level2ID;
                            this.selectedZone = zone;
                            this.selectedZoneIndex = zts.index;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.selectedZoneIndex == -1)
            {
                LevelEditorCommands.ErrorMessage("No zone selected!");
                return;
            }
            for (int i = 0; i < EditorData.currentLevelGlobalEntities.Count; i++)
            {
                if (EditorData.currentLevelGlobalEntities[i].type == BatchEntityType.LoadingZone)
                {
                    EditorData.currentLevelGlobalEntities[i].instances[selectedZoneIndex] = this.selectedZone;
                    break;
                }
            }
            this.loadLoadingZones();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.selectedZone.xOff = (int)this.numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.selectedZone.zOff = (int)this.numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            this.selectedZone.width = (uint)this.numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            this.selectedZone.height = (uint)this.numericUpDown4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            this.selectedZone.level1ID = (int)this.numericUpDown5.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            this.selectedZone.level2ID = (int)this.numericUpDown6.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LevelSerialize.WriteCurrentGlobalData();
        }
    }
}
