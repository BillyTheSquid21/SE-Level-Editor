using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;

namespace Level_Editor
{
    public struct Tile
    {
        public uint x;
        public uint y;
    }

    public enum FileType
    {
        LEVEL, SCRIPT, DELETE, NULL
    }

    public enum BrushMode
    {
        TEXTURE, HEIGHT, PERMISSION, DIRECTION
    }

    //Stores data used in level editing between objects
    static class EditorData
    {
        //Brush
        static public Tile brushTextureTile = new Tile();
        static public Image brushTextureImage = null;
        static public int brushHeight = 0;
        static public int brushDirection = 0;
        static public int brushWorldHeight = 0;
        static public int brushPermission = 0;
        static public BrushMode brushMode = BrushMode.TEXTURE;

        //System
        static public string selectedListPath = null;

        //Tileset
        static public Image[,] currentTilesetImages = null;

        //current level data
        static public string currentLevelPath = null;
        static public uint currentLevelWidth = 0; static public uint currentLevelHeight = 0;
        static public int currentLevelID = -1;
        static public int currentLevelOriginX = 0; static public int currentLevelOriginZ = 0;
        static public int[] currentLevelWorldHeights = { 0 };

        static public int[,] currentLevelHeights = null; static public int[,] currentLevelDirections = null;
        static public IDictionary<int, int[,]> currentLevelPermissions = null; static public Tile[,] currentLevelTextures = null;

        //Add new layer
        public static void AddHeightLayer(int height)
        {
            //If exists, return
            foreach(int i in currentLevelWorldHeights)
            {
                if (i == height)
                {
                    LevelEditorCommands.ErrorMessage("Height already has layer attached!");
                    return;
                }
            }

            int[] newWorldHeights = new int[currentLevelWorldHeights.Length + 1];
            currentLevelWorldHeights.CopyTo(newWorldHeights, 0);
            //Add at end
            newWorldHeights[currentLevelWorldHeights.Length] = height;
            currentLevelWorldHeights = newWorldHeights;

            //Add to 3d data dictionaries
            currentLevelPermissions[height] = new int[currentLevelWidth,currentLevelHeight];
        }
    }

    
}
