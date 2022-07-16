using System.Collections.Generic;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Xml;
using System;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json.Converters;

namespace Level_Editor
{
    class LevelSerialize
    {
        public const string planeHeights = "planeHeightsR";
        public const string planeDirections = "planeDirectionsR";
        public const string planeTextures = "planeTexturesR";
        public const string planePermissions = "PlanePermissionsR";
        public static void CreateLevelFile(uint width, uint height, uint id, int originX, int originZ, int terrainHeight, string path)
        {
            dynamic level = new ExpandoObject();

            level.levelID = id;
            level.levelWidth = width; level.levelHeight = height;
            level.levelOriginX = originX; level.levelOriginZ = originZ;
            level.worldLevels = ""+terrainHeight;

            //Add rows, columns, etc
            var dictionary = (IDictionary<string, object>)level;
            for (int y = 0; y < height; y++)
            {
                //Row name
                string pHeights = planeHeights + y;
                string pDirections = planeDirections + y;
                string pTextures = planeTextures + y;
                string pPermissions = "l" + terrainHeight + planePermissions + y;

                //Strings to push data in
                string pHeightsData = "";
                string pDirectionsData = "";
                string pTexturesData = "";
                string pPermissionsData = "";

                for (int x = 0; x < width; x++)
                {
                    //If not first, add deliminator
                    if (x != 0)
                    {
                        pHeightsData += '|';
                        pDirectionsData += '|';
                        pTexturesData += '|'; 
                        pPermissionsData += '|';
                    }
                    pHeightsData += '0';
                    pDirectionsData += '0';
                    pTexturesData += "0-0";
                    pPermissionsData += '0';
                }

                //Add to dictionary
                dictionary.Add(pHeights, pHeightsData);
                dictionary.Add(pDirections, pDirectionsData);
                dictionary.Add(pTextures, pTexturesData);
                dictionary.Add(pPermissions, pPermissionsData);
            }
            level.endTag = "end";

            string json = JsonConvert.SerializeObject(level, Newtonsoft.Json.Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(path + "//level" + id + ".json" , true)) //// true to append data to the file
            {
                writer.WriteLine(json);
            }
        }

        public static void CreateObjectFile(uint id, string path)
        {
            //Decalre a new XMLDocument object
            XmlDocument doc = new XmlDocument();

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //string.Empty makes cleaner code
            XmlElement element = doc.CreateElement(string.Empty, "Level", string.Empty);
            doc.AppendChild(element);

            doc.Save(path + "//level" + id + "_obj.xml");
        }

        public static void LoadLevelFromFile(string path)
        {
            EditorData.currentLevelPath = path;

            //Get level data
            dynamic levelData = JsonConvert.DeserializeObject<ExpandoObject>(File.ReadAllText(path), new ExpandoObjectConverter());

            //Load core data
            EditorData.currentLevelID = (int)levelData.levelID;
            EditorData.currentLevelWidth = (uint)levelData.levelWidth; EditorData.currentLevelHeight = (uint)levelData.levelHeight;
            EditorData.currentLevelOriginX = (int)levelData.levelOriginX; EditorData.currentLevelOriginZ = (int)levelData.levelOriginZ;
            string[] heightsTemp = ((string)levelData.worldLevels).Split('|');
            int[] heights = new int[heightsTemp.Length];
            for (int i = 0; i < heightsTemp.Length; i++)
            {
                heights[i] = Int32.Parse(heightsTemp[i]);
            }
            EditorData.currentLevelWorldHeights = heights;
            StoreLevelContent(levelData);
        }

        public static void LoadObjectsFromFile(string path)
        {
            //Append path to be to current level xml
            string objectsPath = path.Split('.')[0] + "_obj.xml";

            //Load in the file
            XmlDocument doc = new XmlDocument();
            doc.Load(objectsPath);

            //Get root
            XmlElement root = doc.DocumentElement;

            //Get next nodes
            XmlNodeList nodes = root.ChildNodes;
            foreach(XmlElement element in nodes)
            {
                if (element.Name == "Objects")
                {
                    StoreLevelObjects(element);
                }
            }

        }

        private static void StoreLevelObjects(XmlElement objects)
        {
            XmlNodeList objectsList = objects.ChildNodes;
            foreach(XmlElement element in objectsList)
            {
                Entity npc = new Entity(EntityType.NPC, new Tile(), "");
                Entity.CreateNPC(ref npc, element);
                EditorData.currentLevelObjects.Add(npc);
            }
        }

        private static void StoreLevelContent(dynamic levelData)
        {
            var dictionary = (IDictionary<string, object>)levelData;

            //Init all arrays
            int[,] heights = new int[EditorData.currentLevelWidth, EditorData.currentLevelHeight];
            int[,] directions = new int[EditorData.currentLevelWidth, EditorData.currentLevelHeight];
            IDictionary<int, int[,]> permissions = new Dictionary<int, int[,]>();
            Tile[,] textures = new Tile[EditorData.currentLevelWidth, EditorData.currentLevelHeight];

            //Read all lines
            string[] textureBuffer = new string[2];

            //2d data
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                //Row name
                string pHeights = LevelSerialize.planeHeights + y;
                string pDirections = LevelSerialize.planeDirections + y;
                string pTextures = LevelSerialize.planeTextures + y;

                string[] readInHeights = ((string)dictionary[pHeights]).Split('|');
                string[] readInDirections = ((string)dictionary[pDirections]).Split('|');
                string[] readInTextures = ((string)dictionary[pTextures]).Split('|');

                //Convert
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    heights[x, y] = Int32.Parse(readInHeights[x]);
                    directions[x, y] = Int32.Parse(readInDirections[x]);

                    textureBuffer = readInTextures[x].Split('-');
                    Tile tile = new Tile();
                    tile.x = UInt32.Parse(textureBuffer[0]);
                    tile.y = UInt32.Parse(textureBuffer[1]);
                    textures[x, y] = tile;
                }
            }
            EditorData.currentLevelHeights = heights; EditorData.currentLevelDirections = directions;
            EditorData.currentLevelTextures = textures;

            //3d data
            foreach (int i in EditorData.currentLevelWorldHeights)
            {
                permissions[i] = new int[EditorData.currentLevelWidth, EditorData.currentLevelHeight];
                int[,] permissionsTemp = new int[EditorData.currentLevelWidth, EditorData.currentLevelHeight];
                for (int y = 0; y < EditorData.currentLevelHeight; y++)
                {
                    string pPermissions = "l" + i + LevelSerialize.planePermissions + y;
                    string[] readInPermissions = ((string)dictionary[pPermissions]).Split('|');

                    for (int x = 0; x < EditorData.currentLevelWidth; x++)
                    {
                        permissionsTemp[x, y] = Int32.Parse(readInPermissions[x]);
                    }
                }
                permissions[i] = permissionsTemp;
            }
            EditorData.currentLevelPermissions = permissions;
            EditorData.brushWorldHeight = EditorData.currentLevelWorldHeights[0]; //Ensures the brush defaults to a good value
        }

        private static void WriteCurrentGrassData(ref XmlDocument doc, ref XmlElement root)
        {
            bool present = false;
            BatchEntity entity = EditorData.currentLevelBatchEntities[0];
            for(int i = 0; i < EditorData.currentLevelBatchEntities.Count; i++)
            {
                entity = EditorData.currentLevelBatchEntities[i];
                if (entity.type == BatchEntityType.Grasses)
                {
                    present = true;
                    break;
                }
            }
            if (!present)
            {
                return;
            }

            XmlElement grass = doc.CreateElement(string.Empty, "TallGrass", string.Empty);
            grass.SetAttribute("name", entity.tag);
            root.AppendChild(grass);

            //Add head info
            int count = (int)entity.properties[0];
            Tile frame1 = (Tile)entity.properties[1];
            Tile frame2 = (Tile)entity.properties[2];
            Tile frame3 = (Tile)entity.properties[3];

            XmlElement countElement = doc.CreateElement(string.Empty, "Count", string.Empty);
            countElement.InnerText = count.ToString();
            grass.AppendChild(countElement);

            XmlElement txFrame1 = doc.CreateElement(string.Empty, "TXFrame1", string.Empty);
            txFrame1.InnerText = frame1.x.ToString();
            grass.AppendChild(txFrame1);
            XmlElement tyFrame1 = doc.CreateElement(string.Empty, "TYFrame1", string.Empty);
            tyFrame1.InnerText = frame1.y.ToString();
            grass.AppendChild(tyFrame1);

            XmlElement txFrame2 = doc.CreateElement(string.Empty, "TXFrame2", string.Empty);
            txFrame2.InnerText = frame2.x.ToString();
            grass.AppendChild(txFrame2);
            XmlElement tyFrame2 = doc.CreateElement(string.Empty, "TYFrame2", string.Empty);
            tyFrame2.InnerText = frame2.y.ToString();
            grass.AppendChild(tyFrame2);

            XmlElement txFrame3 = doc.CreateElement(string.Empty, "TXFrame3", string.Empty);
            txFrame3.InnerText = frame3.x.ToString();
            grass.AppendChild(txFrame3);
            XmlElement tyFrame3 = doc.CreateElement(string.Empty, "TYFrame3", string.Empty);
            tyFrame3.InnerText = frame3.y.ToString();
            grass.AppendChild(tyFrame3);

            //Add each grass instance
            foreach(object grassInstance in entity.instances)
            {
                XmlElement grassElement = doc.CreateElement(string.Empty, "Grass", string.Empty);
                XmlElement tileX = doc.CreateElement(string.Empty, "TileX", string.Empty);
                XmlElement tileZ = doc.CreateElement(string.Empty, "TileZ", string.Empty);
                XmlElement wLevel = doc.CreateElement(string.Empty, "WLevel", string.Empty);
                grassElement.AppendChild(tileX); grassElement.AppendChild(tileZ); grassElement.AppendChild(wLevel);
                tileX.InnerText = ((Grass)grassInstance).tile.x.ToString();
                tileZ.InnerText = ((int)EditorData.currentLevelHeight - ((Grass)grassInstance).tile.y - 1).ToString();
                wLevel.InnerText = ((Grass)grassInstance).height.ToString();
                grass.AppendChild(grassElement);
            }
        }

        public static void WriteCurrentObjectData()
        {
            //Decalre a new XMLDocument object
            XmlDocument doc = new XmlDocument();
            File.Delete(EditorData.currentLevelPath.Split('.')[0] + "_obj.xml");

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //string.Empty makes cleaner code
            XmlElement element = doc.CreateElement(string.Empty, "Level", string.Empty);
            doc.AppendChild(element);

            //If any grass, write to xml
            WriteCurrentGrassData(ref doc, ref element);

            XmlElement objects = doc.CreateElement(string.Empty, "Objects", string.Empty);
            element.AppendChild(objects);
            
            //Write objects
            foreach(Entity ent in EditorData.currentLevelObjects)
            {
                switch (ent.type)
                {
                    case EntityType.NPC:
                        WriteNPC(ent, ref doc, ref objects);
                        break;
                }
            }

            doc.Save(EditorData.currentLevelPath.Split('.')[0] + "_obj.xml");
        }

        public static void WriteCurrentLevelData()
        {
            dynamic level = new ExpandoObject();

            //Add simple datas
            level.levelID = EditorData.currentLevelID;
            level.levelWidth = EditorData.currentLevelWidth; level.levelHeight = EditorData.currentLevelHeight;
            level.levelOriginX = EditorData.currentLevelOriginX; level.levelOriginZ = EditorData.currentLevelOriginZ;

            //Add world levels
            string worldLevels = "";
            for (int i = 0; i < EditorData.currentLevelWorldHeights.Length; i++)
            {
                if (i != 0)
                {
                    worldLevels += '|';
                }
                worldLevels += EditorData.currentLevelWorldHeights[i];
            }
            level.worldLevels = worldLevels;

            //Add 2d data
            //Add rows, columns, etc
            var dictionary = (IDictionary<string, object>)level;
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                //Row name
                string pHeights = planeHeights + y;
                string pDirections = planeDirections + y;
                string pTextures = planeTextures + y;

                //Strings to push data in
                string pHeightsData = "";
                string pDirectionsData = "";
                string pTexturesData = "";

                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    //If not first, add deliminator
                    if (x != 0)
                    {
                        pHeightsData += '|';
                        pDirectionsData += '|';
                        pTexturesData += '|';
                    }
                    pHeightsData += EditorData.currentLevelHeights[x,y];
                    pDirectionsData += EditorData.currentLevelDirections[x,y];
                    pTexturesData += EditorData.currentLevelTextures[x, y].x + "-" + EditorData.currentLevelTextures[x, y].y;
                }

                //Add to dictionary
                dictionary.Add(pHeights, pHeightsData);
                dictionary.Add(pDirections, pDirectionsData);
                dictionary.Add(pTextures, pTexturesData);
            }
            //Add 3d data
            foreach (int i in EditorData.currentLevelWorldHeights)
            {
                for (int y = 0; y < EditorData.currentLevelHeight; y++)
                {
                    //Row name
                    string pPermissions = "l" + i + planePermissions + y;

                    //Row data
                    string pPermissionsData = "";
                    for (int x = 0; x < EditorData.currentLevelWidth; x++)
                    {
                        if (x != 0)
                        {
                            pPermissionsData += '|';
                        }
                        pPermissionsData += EditorData.currentLevelPermissions[i][x, y];
                    }
                    dictionary.Add(pPermissions, pPermissionsData);
                }
            }
            level.endTag = "end";

            string json = JsonConvert.SerializeObject(level, Newtonsoft.Json.Formatting.Indented);

            //Erase data
            File.WriteAllText(EditorData.currentLevelPath, "");

            using (StreamWriter writer = new StreamWriter(EditorData.currentLevelPath, true)) //// true to append data to the file
            {
                writer.WriteLine(json);
            }
        }

        private static XmlElement WriteNPC(Entity ent, ref XmlDocument doc, ref XmlElement objects)
        {
            string objTag = "Level" + EditorData.currentLevelID + "_" + ent.tag;
            XmlElement objectElement = doc.CreateElement(string.Empty, "Object", string.Empty);
            objectElement.SetAttribute("name", objTag);

            //Go through and add data
            //Sprite type
            XmlElement spriteType = doc.CreateElement(string.Empty, Entity.GetSprite((SpriteType)ent.properties[0]), string.Empty);

            //Tile location
            XmlElement tileX = doc.CreateElement(string.Empty, "TileX", string.Empty);
            tileX.InnerText = ent.tile.x.ToString();
            spriteType.AppendChild(tileX);
            XmlElement tileZ = doc.CreateElement(string.Empty, "TileZ", string.Empty);
            tileZ.InnerText = ((int)EditorData.currentLevelHeight - (int)ent.tile.y - 1).ToString(); //Offset to make compatible with engine
            spriteType.AppendChild(tileZ);

            //World level
            XmlElement wLevel = doc.CreateElement(string.Empty, "WLevel", string.Empty);
            wLevel.InnerText = ent.properties[1].ToString();
            spriteType.AppendChild(wLevel);

            //Texture location
            XmlElement texX = doc.CreateElement(string.Empty, "TX", string.Empty);
            texX.InnerText = ent.properties[2].ToString();
            spriteType.AppendChild(texX);
            XmlElement texY = doc.CreateElement(string.Empty, "TY", string.Empty);
            texY.InnerText = ent.properties[3].ToString();
            spriteType.AppendChild(texY);

            //Facing dir
            XmlElement dir = doc.CreateElement(string.Empty, "Dir", string.Empty);
            dir.InnerText = ent.properties[4].ToString();
            spriteType.AppendChild(dir);

            //Random walk
            XmlElement randW = doc.CreateElement(string.Empty, "RandWalk", string.Empty);
            randW.InnerText = ent.properties[5].ToString();
            spriteType.AppendChild(randW);

            //Script
            if (ent.properties.Count > 6)
            {
                if (ent.properties[6].ToString() != "NULL")
                {
                    XmlElement script = doc.CreateElement(string.Empty, "NPCScript", string.Empty);
                    script.InnerText = ent.properties[6].ToString();
                    spriteType.AppendChild(script);
                }
            }

            //Append to node
            objectElement.AppendChild(spriteType);
            objects.AppendChild(objectElement);
            return objectElement;
        }
    }
}
