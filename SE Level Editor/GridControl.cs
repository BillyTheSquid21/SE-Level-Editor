using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Level_Editor
{
    public enum InteractionType
    {
        NULL, LeftClick, RightClick
    }

    struct GridSelection
    {
        public Tile tile;
        public InteractionType type;
    }

    class GridControl : Panel
    {
        public GridSelection selection;
        private Action clickCallback;
        private uint width; private uint height;

        public GridControl()
        {
            this.AutoScroll = true;
            this.selection = new GridSelection();
        }

        public void SetCallback(Action callback)
        {
            this.clickCallback = callback;
        }

        public void SetGrid(uint width, uint height)
        {
            this.Controls.Clear();
            this.width = width; this.height = height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.ClientSize = new Size((int)Constants.TILE_SIZE, (int)Constants.TILE_SIZE);
                    pictureBox.Padding = new System.Windows.Forms.Padding(1);

                    Tile currentTile = new Tile();
                    currentTile.x = (uint)x; currentTile.y = (uint)y; //Y is reversed for compatability with engine
                    pictureBox.Location = new Point(x * (int)Constants.TILE_SIZE, y * (int)Constants.TILE_SIZE);
                    pictureBox.MouseClick += new MouseEventHandler(tile_Click);
                    pictureBox.Tag = currentTile;
                    pictureBox.BackColor = Color.FromArgb(255, 155, 155);
                    this.Controls.Add(pictureBox);
                }
            }
        }
        //Gives grid an image array to paint (should be in x/y format)
        public void PaintGrid(Image[,] images, Tile[,] locations)
        {
            if (locations.Length < this.Controls.Count)
            {
                LevelEditorCommands.ErrorMessage("Image array length out of bounds for grid");
                return;
            }
            int index = 0;
            for (int y = 0; y < locations.GetLength(1); y++)
            {
                for (int x = 0; x < locations.GetLength(0); x++)
                {
                    Tile tileImage = locations[x, y];
                    ((PictureBox)this.Controls[index]).Image = images[tileImage.x,tileImage.y];
                    index++;
                }
            }
        }

        public int GetPictureBoxIndex(Tile tile)
        {
            int index = -1;
            foreach(Control control in this.Controls)
            {
                index++;
                if (((Tile)control.Tag).x == tile.x && ((Tile)control.Tag).y == tile.y)
                {
                    return index;
                }
            }
            return index;
        }

        public void ColorPictureBoxAt(Tile tile, Color color)
        {
            int index = GetPictureBoxIndex(tile);
            if (index >= this.Controls.Count)
            {
                LevelEditorCommands.ErrorMessage("Grid element out of range for coloring!");
                return;
            }
            ((PictureBox)this.Controls[index]).BackColor = color;
        }

        private void tile_Click(object sender, MouseEventArgs e)
        {
            Tile tile = (Tile)((PictureBox)sender).Tag;
            GridSelection sel = new GridSelection();
            sel.tile = tile;
            InteractionType type = InteractionType.NULL;
            if (e.Button == MouseButtons.Left)
            {
                type = InteractionType.LeftClick;
            }
            else if (e.Button == MouseButtons.Right)
            {
                type = InteractionType.RightClick;
            }
            sel.type = type;
            this.selection = sel;
            this.clickCallback();
        }

    }
}
