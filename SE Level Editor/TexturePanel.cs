using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Level_Editor
{
    public struct ImageData
    {
        public int width;
        public int height;
        public int bpp;
    };

    //Panel that displays a grid of availible tiles
    class TexturePanel : Panel
    {
        public TexturePanel() : base()
        {
            this.AutoScroll = true;
        }

        public void DisplayTileset(string path)
        {
            //Makes path into a c++ friendly form
            path = path.Replace(@"\", @"\\");
            
            //Clears any existing controls
            this.Controls.Clear();

            //Loads tileset byte array using size in bytes from fileinfo
            float width; float height;
            PixelFormat pixelFormat;
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
            byte[] bytes = new byte[(int)(width*height*BITS_PER_PIXEL)];
            ImageData data = new ImageData();
            TilesetLoad.LoadTilesetFromFile(path, ref bytes, bytes.Length, ref data);
            EditorData.tilesetImageWidth = (uint)data.width; EditorData.tilesetImageHeight = (uint)data.height;

            //Works out how many tiles there are
            uint tilesX = (uint)data.width / Constants.TILE_SIZE;
            uint tilesY = (uint)data.height / Constants.TILE_SIZE;
            uint currentTileX = 0; uint currentTileY = 0;
            uint totalTiles = tilesX * tilesY;

            //Tracks positioning in panel
            int x = 0; int y = 0;

            //Image array
            Image[,] tileImages = new Image[tilesX, tilesY];

            for (int i = 0; i < totalTiles; i ++)
            {
                //Go along all of tiles x, then y, then stop
                TextureSelect select = new TextureSelect();
                var panelLoc = this.Location;
                select.Location = new System.Drawing.Point(panelLoc.X + x, panelLoc.Y + y);
                select.Name = "textureButton" + x + y;
                select.Size = new System.Drawing.Size(64, 64);
                select.TabIndex = 0;
                select.Text = "" + currentTileX + " " + currentTileY;

                const int DISPLAY_TILE_SIZE = 32;
                Tile tile = new Tile();
                tile.x = currentTileX; tile.y = currentTileY;
                byte[] iconBytes = new byte[(int)(DISPLAY_TILE_SIZE * DISPLAY_TILE_SIZE * BITS_PER_PIXEL)];
                TilesetLoad.ExtractTileFromImage(ref bytes, bytes.Length, ref data, ref iconBytes, iconBytes.Length, tile);
                using (var stream = new MemoryStream(iconBytes))
                using (var bmp = new Bitmap(DISPLAY_TILE_SIZE, DISPLAY_TILE_SIZE, pixelFormat))
                {
                    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                    IntPtr pNative = bmpData.Scan0;
                    Marshal.Copy(iconBytes, 0, pNative, iconBytes.Length);
                    bmp.UnlockBits(bmpData);
                    select.Image = (Bitmap)bmp.Clone();
                    tileImages[currentTileX, currentTileY] = (Image)bmp.Clone();
                }
                if (i == 0)
                {
                    EditorData.brushTextureImage = (Image)select.Image.Clone();
                }

                select.m_Tile = tile;
                select.Click += new System.EventHandler(this.Select_Click);
                this.Controls.Add(select);

                //Increment tiles
                if (currentTileX >= tilesX - 1)
                {
                    currentTileX = 0;
                    currentTileY++;
                }
                else
                {
                    currentTileX++;
                }

                //Increment position
                if (x >= 128)
                {
                    x = 0; y+=64;
                }
                else
                {
                    x += 64;
                }
            }
            EditorData.currentTilesetImages = tileImages;
        }

        private void Select_Click(object sender, EventArgs e)
        {
            TextureSelect select = (TextureSelect)sender;
            EditorData.brushTextureTile = select.m_Tile;
            EditorData.brushTextureImage = select.Image;
        }
    }

    class TextureSelect : Button
    {
        public Tile m_Tile;
    }

}
