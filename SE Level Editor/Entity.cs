using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level_Editor
{
    public enum EntityType
    { 
        NULL, Item, NPC, Script, Model
    }

    public enum SpriteType
    { 
        Sprite, DirectionalSprite, WalkingSprite, RunningSprite
    }

    class Entity
    {
        public EntityType type;
        public List<object> properties; //Stored as list that are interpreted based on entity type
        public Tile tile;
        public string tag = "";

        public Entity(EntityType type, Tile location, string tag)
        {
            this.type = type;

            //Tiles when explicitly stated are reversed from in the editor - z = 0 is at BOTTOM
            Tile tile = new Tile
            {
                x = location.x,
                y = location.y
            };
            this.tile = tile;

            this.properties = new List<object>();
            this.tag = tag;
        }

        public static void CreateDefaultEntity(ref Entity entity)
        {
            switch (entity.type)
            {
                case EntityType.NPC:
                    CreateDefaultNPC(ref entity);
                    break;
            }
        }

        private static void CreateDefaultNPC(ref Entity entity)
        {
            //Set sprite type
            entity.properties.Add(SpriteType.DirectionalSprite);
            //Set world level
            entity.properties.Add(0);
            //Set texture location
            entity.properties.Add(0); //X Tex
            entity.properties.Add(0); //Y Tex
            //Set facing direction
            entity.properties.Add(1);
            //Set random walk
            entity.properties.Add(0);
            //Set script - "NULL" if no script
            entity.properties.Add("NULL");
        }

        public static void CreateNPC(ref Entity entity, XmlElement element)
        {
            entity.tag = element.Attributes[0].Value.Split('_')[1];
            entity.properties.Add(Entity.GetSpriteFromString(element.FirstChild.Name));

            XmlNodeList properties = element.FirstChild.ChildNodes;

            //Needs to be in order so run through all elements to get desired - should only take one loop for much of the data if arranged right
            Tile tile = new Tile();
            foreach (XmlElement property in properties)
            {
                if (property.Name == "TileX")
                {
                    tile.x = (uint)Int32.Parse(property.InnerText);
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "TileZ")
                {
                    tile.y = (uint)(EditorData.currentLevelHeight - Int32.Parse(property.InnerText) - 1); //Offset to make compatible with engine
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "WLevel")
                {
                    entity.properties.Add(Int32.Parse(property.InnerText));
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "TX")
                {
                    entity.properties.Add(Int32.Parse(property.InnerText));
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "TY")
                {
                    entity.properties.Add(Int32.Parse(property.InnerText));
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "Dir")
                {
                    entity.properties.Add(Int32.Parse(property.InnerText));
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "RandWalk")
                {
                    entity.properties.Add(Int32.Parse(property.InnerText));
                    break;
                }
            }
            foreach (XmlElement property in properties)
            {
                if (property.Name == "NPCScript")
                {
                    entity.properties.Add(property.InnerText);
                    break;
                }
            }
            entity.tile = tile;
        }

        public static EntityType GetEntityType(string type)
        {
            if (type == "Item")
            {
                return EntityType.Item;
            }
            else if (type == "NPC")
            {
                return EntityType.NPC;
            }
            else if (type == "Script")
            {
                return EntityType.Script;
            }
            else if (type == "Model")
            {
                return EntityType.Model;
            }
            return EntityType.NULL;
        }

        public static string GetSpriteString(SpriteType type)
        {
            switch (type)
            {
                case SpriteType.DirectionalSprite:
                    return "DirSprite";
                case SpriteType.RunningSprite:
                    return "RunSprite";
                case SpriteType.Sprite:
                    return "Sprite";
                case SpriteType.WalkingSprite:
                    return "WalkSprite";
            }
            return "Sprite";
        }

        public static SpriteType GetSpriteFromString(string type)
        {
            if (type == "Sprite")
            {
                return SpriteType.Sprite;
            }
            else if (type == "DirSprite")
            {
                return SpriteType.DirectionalSprite;
            }
            else if (type == "WalkSprite")
            {
                return SpriteType.WalkingSprite;
            }
            else if (type == "RunSprite")
            {
                return SpriteType.RunningSprite;
            }
            return SpriteType.Sprite;
        }
    }
}
