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
        static EditorData()
        {
            currentLevelObjects = new List<Entity>();
            currentLevelBatchEntities = new List<BatchEntity>();
        }

        public static int GetObjectIndex(Tile tile)
        {
            for(int i = 0; i < currentLevelObjects.Count; i++)
            {
                if (currentLevelObjects[i].tile.x == tile.x && currentLevelObjects[i].tile.y == tile.y)
                {
                    return i;
                }
            }
            return -1; //if obj not found
        }

        public static int GetObjectIndex(string name)
        {
            for (int i = 0; i < currentLevelObjects.Count; i++)
            {
                if (name == currentLevelObjects[i].tag)
                {
                    return i;
                }
            }
            return -1; //if obj not found
        }
        public static Tile[] GetNonEmptyTiles()
        {
            Tile[] locations = new Tile[currentLevelObjects.Count];
            for (int i = 0; i < currentLevelObjects.Count; i++)
            {
                locations[i] = currentLevelObjects[i].tile;
            }
            return locations;
        }

        public static Entity[] GetObjectsAt(Tile tile)
        {
            int size = ObjectCountAt(tile);
            Entity[] entities = new Entity[size];
            int index = 0;
            foreach(Entity ent in currentLevelObjects)
            {
                if (ent.tile.x == tile.x && ent.tile.y == tile.y)
                {
                    entities[index] = ent;
                    index++;
                }
            }
            return entities;
        }

        public static int ObjectCountAt(Tile tile)
        {
            int count = 0;
            foreach(Entity ent in currentLevelObjects)
            {
                if (ent.tile.x == tile.x && ent.tile.y == tile.y)
                {
                    count++;
                }
            }
            return count;
        }

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

        //current level objects
        static public List<Entity> currentLevelObjects;
        static public List<BatchEntity> currentLevelBatchEntities;
        static public List<BatchEntity> currentLevelGlobalEntities = null; static public string globalPath = null;

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
