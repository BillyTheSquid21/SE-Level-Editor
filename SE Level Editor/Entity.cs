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
            //Set height
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
            entity.properties.Add(Entity.GetSprite(element.FirstChild.Name));

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
                    tile.y = (uint)LevelSerialize.InvertZTile(Int32.Parse(property.InnerText)); //Offset to make compatible with engine
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
            bool hasScript = false;
            foreach (XmlElement property in properties)
            {
                if (property.Name == "NPCScript")
                {
                    entity.properties.Add(property.InnerText);
                    hasScript = true;
                    break;
                }
            }
            if (!hasScript)
            {
                entity.properties.Add("NULL");
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

        public static string GetSprite(SpriteType type)
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

        public static SpriteType GetSprite(string type)
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

        public static int GetDirection(string direction)
        {
            if (direction == "Null")
            {
                return 0;
            }
            if (direction == "North")
            {
                return 1;
            }
            else if (direction == "North-East")
            {
                return 2;
            }
            else if (direction == "North-West")
            {
                return 3;
            }
            else if (direction == "South")
            {
                return 4;
            }
            else if (direction == "South-East")
            {
                return 5;
            }
            else if (direction == "South-West")
            {
                return 6;
            }
            else if (direction == "East")
            {
                return 7;
            }
            else if (direction == "West")
            {
                return 8;
            }
            else if (direction == "North-East - Wrapped")
            {
                return 9;
            }
            else if (direction == "North-West - Wrapped")
            {
                return 10;
            }
            else if (direction == "South-East - Wrapped")
            {
                return 11;
            }
            else if (direction == "South-West - Wrapped")
            {
                return 12;
            }
            return 0;
        }

        public static string GetDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                    return "Null";
                case 1:
                    return "North";
                case 2:
                    return "North-East";
                case 3:
                    return "North-West";
                case 4:
                    return "South";
                case 5:
                    return "South-East";
                case 6:
                    return "South-West";
                case 7:
                    return "East";
                case 8:   
                    return "West";
                case 9:
                    return "North-East - Wrapped";
                case 10:
                    return "North-West - Wrapped";
                case 11:
                    return "South-East - Wrapped";
                case 12:
                    return "South-West - Wrapped";
                default:
                    return "Null";
            }
        }
    }
}
