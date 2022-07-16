using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level_Editor
{
    public enum BatchEntityType
    { 
        NULL, Trees, Grasses, LoadingZone
    }

    //Grass instance struct
    struct Grass
    {
        public Tile tile;
        public int height;
    }

    //Tree instance struct
    struct Tree
    {
        public Tile tile;
        public int height;

        //Tex
        public Tile firstHalfTexture;
        public Tile secondHalfTexture;
        public uint textureWidth;
        public uint textureHeight;
    }

    //Loading zone instance struct
    struct LoadingZone
    {
        public int xOff;
        public int zOff;
        public uint width;
        public uint height;
        public int level1ID;
        public int level2ID;
    }

    //Entities that are batched together (eg grass instances, tree instances etc)
    class BatchEntity
    {
        public BatchEntityType type;
        public List<object> properties;
        public List<object> instances;
        public string tag = "";

        public BatchEntity(BatchEntityType type, string tag)
        {
            this.type = type;
            this.properties = new List<object>();
            this.instances = new List<object>();
            this.tag = tag;
        }

        public static void CreateDefaultBatchEntity(ref BatchEntity entity)
        {
            switch (entity.type)
            {
                case BatchEntityType.Grasses:
                    CreateDefaultGrass(ref entity);
                    break;
                case BatchEntityType.Trees:
                    CreateDefaultTree(ref entity);
                    break;
                case BatchEntityType.LoadingZone:
                    CreateDefaultLoadingZone(ref entity);
                    break;
            }
        }

        //Loading zone
        private static void CreateDefaultLoadingZone(ref BatchEntity entity)
        {
            //Set tag
            entity.tag = "LoadingZones";
        }

        //Grass
        private static void CreateDefaultGrass(ref BatchEntity entity)
        {
            //Set count
            entity.properties.Add(0);
            //Set the 3 frames
            Tile tile = new Tile();
            tile.x = 0; tile.y = 0;
            entity.properties.Add(tile);
            entity.properties.Add(tile);
            entity.properties.Add(tile);
        }

        //Tree
        private static void CreateDefaultTree(ref BatchEntity entity)
        {
            //Set count
            entity.properties.Add(0);
        }

        public static void AddGrass(ref BatchEntity entity, Tile tile, int height)
        {
            if (entity.type != BatchEntityType.Grasses)
            {
                LevelEditorCommands.ErrorMessage("Can't add grass to this entity!");
                return;
            }
            Grass grass = new Grass();
            grass.tile = tile; grass.height = height;
            entity.instances.Add(grass);
        }

        public static void AddTree(ref BatchEntity entity, Tile tile, int height, Tile tex1, Tile tex2, uint texWidth, uint texHeight)
        {
            if (entity.type != BatchEntityType.Trees)
            {
                LevelEditorCommands.ErrorMessage("Can't add trees to this entity!");
                return;
            }
            Tree tree = new Tree();
            tree.tile = tile; tree.height = height; tree.firstHalfTexture = tex1; tree.secondHalfTexture = tex2; 
            tree.textureWidth = texWidth; tree.textureHeight = texHeight;
            entity.instances.Add(tree);
        }

        public static BatchEntityType GetEntityType(string type)
        {
            if (type == "Trees")
            {
                return BatchEntityType.Trees;
            }
            else if (type == "Grass")
            {
                return BatchEntityType.Grasses;
            }
            return BatchEntityType.NULL;
        }
    }
}
