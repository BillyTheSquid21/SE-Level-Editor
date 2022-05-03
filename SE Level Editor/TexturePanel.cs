using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Level_Editor
{
    public struct Tile
    {
        public uint x;
        public uint y;
    }

    public struct ImageData
    {
        public int width;
        public int height;
        public int bpp;
    };

    //Panel that displays a grid of availible tiles
    class TexturePanel : Panel
    {
        public const string FuncDLL = @"..\..\LoadingFuncs.dll";
        [DllImport(FuncDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LoadTileset(string path, [In, Out][MarshalAs(UnmanagedType.LPArray)] byte[] data, int length, ref ImageData dim);

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
            LoadTilesetFromFile(path, ref bytes, bytes.Length, ref data);

            //Works out how many tiles there are
            const int TILE_SIZE = 32;
            uint tilesX = (uint)data.width / TILE_SIZE;
            uint tilesY = (uint)data.height / TILE_SIZE;
            uint currentTileX = 0; uint currentTileY = 0;

            for (int y = 0; y < this.Height; y += 64)
            {
                for (int x = 0; x < this.Width * 0.8f; x += 64)
                {
                    //Go along all of tiles x, then y, then stop
                    TextureSelect select = new TextureSelect();
                    var panelLoc = this.Location;
                    select.Location = new System.Drawing.Point(panelLoc.X + x, panelLoc.Y + y);
                    select.Name = "textureButton" + x + y;
                    select.Size = new System.Drawing.Size(64, 64);
                    select.TabIndex = 0;
                    select.Text = "" + data.width + " " + data.height;

                    //Test image
                    using (var stream = new MemoryStream(bytes))
                    using (var bmp = new Bitmap(data.width, data.height, pixelFormat))
                    {
                        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                        IntPtr pNative = bmpData.Scan0;
                        Marshal.Copy(bytes, 0, pNative, bytes.Length);

                        bmp.UnlockBits(bmpData);

                        bmp.Save("test.bmp");
                    }

                    select.Click += new System.EventHandler(this.Select_Click);
                    this.Controls.Add(select);

                    //Increment tiles
                    if (currentTileX >= tilesX)
                    {
                        if (currentTileY >= tilesY)
                        {
                            return;
                        }
                        currentTileY++;
                        currentTileX = 0;
                        continue;
                    }
                    currentTileX++;
                }
            }
        }

        public unsafe void LoadTilesetFromFile(string path, ref byte[] data, int size, ref ImageData info)
        {
            //Pin Memory
            fixed (byte* p = data)
            {
                LoadTileset(path, data, size, ref info);
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            
        }
    }

    class TextureSelect : Button
    { 
        
    }

}
