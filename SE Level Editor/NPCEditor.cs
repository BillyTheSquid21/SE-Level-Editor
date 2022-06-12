using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace Level_Editor
{
    class NPCEditor : Form
    {
        //Data
        public string npcTag;
        public List<object> data;
        public bool confirmed = false;

        //Sprite
        const byte BITS_PER_PIXEL = 4;
        byte[] bytes;
        ImageData imgData;
        float width; float height;
        PixelFormat pixelFormat;

        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private NumericUpDown numericUpDown1;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private ComboBox comboBox2;
        private Label label4;
        private TextBox textBox1;
        private Label label5;
        private Button button1;
        private Button button2;
        private Label label1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private Button button3;
        private Label label6;
        private Label label8;
        private Label label9;
        private Button button4;
        private Label label7;
        public NPCEditor(string npcTag, List<object> data) : base()
        {
            this.InitializeComponent();
            this.label1.Text = npcTag;
            this.npcTag = npcTag;
            this.data = data;

            //Init data shown
            this.comboBox2.SelectedItem = Entity.GetSprite((SpriteType)this.data[0]);
            this.numericUpDown1.Value = (int)this.data[1];
            this.comboBox1.SelectedItem = Entity.GetDirection((int)this.data[4]);
            this.textBox1.Text = (string)this.data[6];
            this.numericUpDown2.Value = (int)this.data[2];
            this.numericUpDown3.Value = (int)this.data[3];
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "NPC Tag";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 331);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Random Walk";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 249);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "North",
            "South",
            "East",
            "West"});
            this.comboBox1.Location = new System.Drawing.Point(12, 276);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Direction";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Sprite",
            "DirSprite",
            "WalkSprite",
            "RunSprite"});
            this.comboBox2.Location = new System.Drawing.Point(12, 304);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 10;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Sprite Type";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(223, 190);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(414, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Script";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(370, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(289, 325);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Confirm";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(223, 93);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 16;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(223, 120);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown3.TabIndex = 17;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(223, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "Load Sprite Sheet";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(349, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Texture X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(349, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Texture Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(211, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "- If sprite sheet arranged correctly first sprite";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(233, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(169, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "should be east facing neutral pose";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(370, 296);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "Delete Entity";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // NPCEditor
            // 
            this.ClientSize = new System.Drawing.Size(460, 355);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "NPCEditor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int randWalk = ((CheckBox)sender).Checked ? 1 : 0;
            this.data[5] = randWalk;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (text == "")
            {
                return;
            }
            this.data[6] = text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.data[1] = (int)((NumericUpDown)sender).Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.confirmed = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.data[4] = Entity.GetDirection((string)((ComboBox)sender).SelectedItem);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.data[0] = Entity.GetSprite((string)((ComboBox)sender).SelectedItem);
        }

        private void LoadIcon()
        {
            //Load icon
            const int DISPLAY_TILE_SIZE = 32;
            Tile tile = new Tile();
            tile.x = (uint)this.numericUpDown2.Value; tile.y = (uint)this.numericUpDown3.Value;
            byte[] iconBytes = new byte[(int)(DISPLAY_TILE_SIZE * DISPLAY_TILE_SIZE * BITS_PER_PIXEL)];
            TilesetLoad.ExtractTileFromImage(ref bytes, bytes.Length, ref imgData, ref iconBytes, iconBytes.Length, tile);
            using (var stream = new MemoryStream(iconBytes))
            using (var bmp = new Bitmap(DISPLAY_TILE_SIZE, DISPLAY_TILE_SIZE, pixelFormat))
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr pNative = bmpData.Scan0;
                Marshal.Copy(iconBytes, 0, pNative, iconBytes.Length);
                bmp.UnlockBits(bmpData);
                Bitmap resizedBmp = new Bitmap(192, 192);
                Graphics g = Graphics.FromImage((Image)resizedBmp);
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, 0, 0, 192, 192);
                this.pictureBox1.Image = resizedBmp;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = LevelEditorCommands.OpenFileDialog("PNG files (*.png*)|*.png*");
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (Image tif = Image.FromStream(stream: file, useEmbeddedColorManagement: false, validateImageData: false))
                {
                    width = tif.PhysicalDimension.Width;
                    height = tif.PhysicalDimension.Height;
                    pixelFormat = tif.PixelFormat;
                }
            }
            const byte BITS_PER_PIXEL = 4;
            bytes = new byte[(int)(width * height * BITS_PER_PIXEL)];
            imgData = new ImageData();
            TilesetLoad.LoadTilesetFromFile(path, ref bytes, bytes.Length, ref imgData);
            LoadIcon();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.data[2] = ((NumericUpDown)sender).Value;
            if (bytes != null)
            {
                LoadIcon();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            this.data[3] = ((NumericUpDown)sender).Value;
            if (bytes != null)
            {
                LoadIcon();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (LevelEditorCommands.ConfirmMessage("Confirm delete entity??"))
            {
                EditorData.currentLevelObjects.RemoveAt(EditorData.GetObjectIndex(this.npcTag));
                this.Close();
            }
        }
    }
}
