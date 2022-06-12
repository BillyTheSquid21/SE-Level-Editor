using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Level_Editor
{
    class TilesetLoad
    {
        public const string FuncDLL = @"LoadingFuncs.dll";
        [DllImport(FuncDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LoadTileset(string path, [In, Out][MarshalAs(UnmanagedType.LPArray)] byte[] data, int size, ref ImageData dim);
        [DllImport(FuncDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LoadTile([In, Out][MarshalAs(UnmanagedType.LPArray)] byte[] tilesetData, int tilesetSize, ref ImageData dim, [In, Out][MarshalAs(UnmanagedType.LPArray)] byte[] iconData, int iconSize, Tile tile);

        public static unsafe void LoadTilesetFromFile(string path, ref byte[] data, int size, ref ImageData info)
        {
            //Pin Memory
            fixed (byte* p = data)
            {
                LoadTileset(path, data, size, ref info);
            }
        }

        public static unsafe void ExtractTileFromImage(ref byte[] tilesetData, int tilesetSize, ref ImageData info, ref byte[] iconData, int tileSize, Tile tile)
        {
            //Pin Memory
            fixed (byte* p1 = tilesetData)
            {
                fixed (byte* p2 = iconData)
                {
                    LoadTile(tilesetData, tilesetSize, ref info, iconData, tileSize, tile);
                }
            }
        }
    }
}
