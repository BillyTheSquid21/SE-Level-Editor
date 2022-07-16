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
        public static void CreateGlobalsFile(string path)
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

            XmlElement objectElement = doc.CreateElement(string.Empty, "Objects", string.Empty);
            element.AppendChild(objectElement);

            doc.Save(path + "//global.xml");
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

            //Check if are in a different project to previous global
            string objectsPath = Path.GetDirectoryName(path) + "//global.xml";
            if (objectsPath != EditorData.globalPath)
            {
                EditorData.globalPath = null;
                EditorData.currentLevelGlobalEntities = null;
            }

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

        public static void LoadGlobalObjectsFromFile(string path)
        {
            string objectsPath = Path.GetDirectoryName(path) + "//global.xml";
            EditorData.globalPath = objectsPath;
            EditorData.currentLevelGlobalEntities = new List<BatchEntity>();

            //Load in
            XmlDocument doc = new XmlDocument();
            doc.Load(objectsPath);

            //Get root
            XmlElement root = doc.DocumentElement;

            //Get next node
            XmlElement objects = (XmlElement)root.FirstChild;

            //Get next nodes
            foreach (XmlElement element in objects.ChildNodes)
            {
                if (element.Name == "LoadingZones")
                {
                    StoreLoadingZones(element);
                }
            }
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
                else if (element.Name == "TallGrass")
                {
                    StoreLevelGrass(element);
                }
                else if (element.Name == "Trees")
                {
                    StoreLevelTrees(element);
                }
            }
        }

        private static void StoreLoadingZones(XmlElement loadingZone)
        {
            BatchEntity loadingZoneEntity = new BatchEntity(BatchEntityType.LoadingZone, "LoadingZones");
            foreach (XmlElement element in loadingZone.ChildNodes)
            {
                LoadingZone zone = new LoadingZone();
                foreach(XmlElement property in element.ChildNodes)
                {
                    if (property.Name == "X")
                    {
                        zone.xOff = Int32.Parse(property.InnerText);
                    }
                    else if (property.Name == "Z")
                    {
                        zone.zOff = Int32.Parse(property.InnerText);
                    }
                    else if (property.Name == "Width")
                    {
                        zone.width = UInt32.Parse(property.InnerText);
                    }
                    else if (property.Name == "Height")
                    {
                        zone.height = UInt32.Parse(property.InnerText);
                    }
                    else if (property.Name == "L1ID")
                    {
                        zone.level1ID = Int32.Parse(property.InnerText);
                    }
                    else if (property.Name == "L2ID")
                    {
                        zone.level2ID = Int32.Parse(property.InnerText);
                    }
                }
                loadingZoneEntity.instances.Add(zone);
            }
            EditorData.currentLevelGlobalEntities.Add(loadingZoneEntity);
        }

        private static void StoreLevelTrees(XmlElement trees)
        {
            XmlNodeList treeList = trees.ChildNodes;
            BatchEntity treeEntity = new BatchEntity(BatchEntityType.Trees, trees.GetAttribute("name"));
            foreach(XmlElement element in treeList)
            {
                if (element.Name == "Count")
                {
                    treeEntity.properties.Add(Int32.Parse(element.InnerText));
                }
                else if (element.Name == "Tree")
                {
                    Tree treeInstance = new Tree();
                    foreach (XmlElement treeElement in element.ChildNodes)
                    {
                        if (treeElement.Name == "TileX")
                        {
                            treeInstance.tile.x = UInt32.Parse(treeElement.InnerText);
                        }
                        else if (treeElement.Name == "TileZ")
                        {
                            treeInstance.tile.y = (uint)InvertZTile(Int32.Parse(treeElement.InnerText));
                        }
                        else if (treeElement.Name == "TX1")
                        {
                            treeInstance.firstHalfTexture.x = UInt32.Parse(treeElement.InnerText);
                        }
                        else if (treeElement.Name == "TY1")
                        {
                            treeInstance.firstHalfTexture.y = UInt32.Parse(treeElement.InnerText);
                        }
                        else if (treeElement.Name == "TX2")
                        {
                            treeInstance.secondHalfTexture.x = UInt32.Parse(treeElement.InnerText);
                        }
                        else if (treeElement.Name == "TY2")
                        {
                            treeInstance.secondHalfTexture.y = UInt32.Parse(treeElement.InnerText);
                        }
                        else if (treeElement.Name == "TW")
                        {
                            treeInstance.textureWidth = UInt32.Parse(treeElement.InnerText);
                        }
                        else if (treeElement.Name == "TH")
                        {
                            treeInstance.textureWidth = UInt32.Parse(treeElement.InnerText);
                        }
                    }
                    treeEntity.instances.Add(treeInstance);
                }
            }
            EditorData.currentLevelBatchEntities.Add(treeEntity);
        }

        private static void StoreLevelGrass(XmlElement grass)
        {
            XmlNodeList grassList = grass.ChildNodes;
            BatchEntity grassEntity = new BatchEntity(BatchEntityType.Grasses, grass.GetAttribute("name"));
            grassEntity.properties = new List<object>(new object[4]); //Ensures placing at indexes

            Tile frame1 = new Tile();
            Tile frame2 = new Tile();
            Tile frame3 = new Tile();

            foreach(XmlElement element in grassList)
            {
                if (element.Name == "Count")
                {
                    grassEntity.properties[0] = Int32.Parse(element.InnerText);
                }
                else if (element.Name == "TXFrame1")
                {
                    frame1.x = UInt32.Parse(element.InnerText);
                }
                else if (element.Name == "TYFrame1")
                {
                    frame1.y = UInt32.Parse(element.InnerText);
                }
                else if (element.Name == "TXFrame2")
                {
                    frame2.x = UInt32.Parse(element.InnerText);
                }
                else if (element.Name == "TYFrame2")
                {
                    frame2.y = UInt32.Parse(element.InnerText);
                }
                else if (element.Name == "TXFrame3")
                {
                    frame3.x = UInt32.Parse(element.InnerText);
                }
                else if (element.Name == "TYFrame3")
                {
                    frame3.y = UInt32.Parse(element.InnerText);
                }
                else if (element.Name == "Grass")
                {
                    Grass grassInstance = new Grass();
                    foreach(XmlElement grassElement in element.ChildNodes)
                    {
                        if (grassElement.Name == "TileX")
                        {
                            grassInstance.tile.x = UInt32.Parse(grassElement.InnerText);
                        }
                        else if (grassElement.Name == "TileZ")
                        {
                            grassInstance.tile.y = UInt32.Parse(grassElement.InnerText);
                            grassInstance.tile.y = (uint)InvertZTile((int)grassInstance.tile.y);
                        }
                        else if (grassElement.Name == "WLevel")
                        {
                            grassInstance.height = Int32.Parse(grassElement.InnerText);
                        }
                    }
                    grassEntity.instances.Add(grassInstance);
                }
            }
            grassEntity.properties[1] = frame1;
            grassEntity.properties[2] = frame2;
            grassEntity.properties[3] = frame3;

            EditorData.currentLevelBatchEntities.Add(grassEntity);
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

        private static void WriteCurrentTreeData(ref XmlDocument doc, ref XmlElement root)
        {
            bool present = false;
            if (EditorData.currentLevelBatchEntities.Count == 0)
            {
                return;
            }
            BatchEntity entity = EditorData.currentLevelBatchEntities[0];
            for (int i = 0; i < EditorData.currentLevelBatchEntities.Count; i++)
            {
                entity = EditorData.currentLevelBatchEntities[i];
                if (entity.type == BatchEntityType.Trees)
                {
                    present = true;
                    break;
                }
            }
            if (!present || (int)entity.properties[0] <= 0)
            {
                return;
            }

            XmlElement trees = doc.CreateElement(string.Empty, "Trees", string.Empty);
            trees.SetAttribute("name", entity.tag);
            root.AppendChild(trees);

            //Add head info
            int count = (int)entity.properties[0];
            XmlElement countElement = doc.CreateElement(string.Empty, "Count", string.Empty);
            countElement.InnerText = count.ToString();
            trees.AppendChild(countElement);

            //Add each tree
            //Add each grass instance
            foreach (object treeInstance in entity.instances)
            {
                XmlElement treeElement = doc.CreateElement(string.Empty, "Tree", string.Empty);
                XmlElement tileX = doc.CreateElement(string.Empty, "TileX", string.Empty);
                XmlElement tileZ = doc.CreateElement(string.Empty, "TileZ", string.Empty);
                XmlElement wLevel = doc.CreateElement(string.Empty, "WLevel", string.Empty);
                treeElement.AppendChild(tileX); treeElement.AppendChild(tileZ); treeElement.AppendChild(wLevel);
                tileX.InnerText = ((Tree)treeInstance).tile.x.ToString();
                tileZ.InnerText = (InvertZTile((int)((Tree)treeInstance).tile.y)).ToString();
                wLevel.InnerText = ((Tree)treeInstance).height.ToString();

                //Texture
                XmlElement tx1 = doc.CreateElement(string.Empty, "TX1", string.Empty);
                tx1.InnerText = ((Tree)treeInstance).firstHalfTexture.x.ToString();
                XmlElement ty1 = doc.CreateElement(string.Empty, "TY1", string.Empty);
                ty1.InnerText = ((Tree)treeInstance).firstHalfTexture.y.ToString();
                XmlElement tx2 = doc.CreateElement(string.Empty, "TX2", string.Empty);
                tx2.InnerText = ((Tree)treeInstance).secondHalfTexture.x.ToString();
                XmlElement ty2 = doc.CreateElement(string.Empty, "TY2", string.Empty);
                ty2.InnerText = ((Tree)treeInstance).secondHalfTexture.y.ToString();
                XmlElement tw = doc.CreateElement(string.Empty, "TW", string.Empty);
                tw.InnerText = ((Tree)treeInstance).textureWidth.ToString();
                XmlElement th = doc.CreateElement(string.Empty, "TH", string.Empty);
                th.InnerText = ((Tree)treeInstance).textureHeight.ToString();
                treeElement.AppendChild(tx1); treeElement.AppendChild(ty1); 
                treeElement.AppendChild(tx2); treeElement.AppendChild(ty2);
                treeElement.AppendChild(tw); treeElement.AppendChild(th);

                trees.AppendChild(treeElement);
            }
        }

        private static void WriteCurrentGrassData(ref XmlDocument doc, ref XmlElement root)
        {
            bool present = false;
            if (EditorData.currentLevelBatchEntities.Count == 0)
            {
                return;
            }
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
            if (!present || (int)entity.properties[0] <= 0)
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
                tileZ.InnerText = (InvertZTile((int)((Grass)grassInstance).tile.y)).ToString();
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

            //If any trees, write to xml
            WriteCurrentTreeData(ref doc, ref element);

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

        public static void WriteCurrentGlobalData()
        {
            //Decalre a new XMLDocument object
            if (EditorData.globalPath == null)
            {
                LevelEditorCommands.ErrorMessage("No globals loaded!");
                return;
            }
            XmlDocument doc = new XmlDocument();
            File.Delete(EditorData.globalPath);

            //xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //create the root element
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //string.Empty makes cleaner code
            XmlElement element = doc.CreateElement(string.Empty, "Level", string.Empty);
            doc.AppendChild(element);

            XmlElement objElement = doc.CreateElement(string.Empty, "Objects", string.Empty);
            element.AppendChild(objElement);

            foreach (BatchEntity entity in EditorData.currentLevelGlobalEntities)
            {
                switch (entity.type)
                {
                    case BatchEntityType.LoadingZone:
                        WriteLoadingZones(entity, ref doc, ref objElement);
                        break;
                    default:
                        break;
                }
            }
            doc.Save(EditorData.globalPath);
        }

        private static void WriteLoadingZones(BatchEntity ent, ref XmlDocument doc, ref XmlElement objElement)
        {
            XmlElement loadingZone = doc.CreateElement(string.Empty, "LoadingZones", string.Empty);
            objElement.AppendChild(loadingZone);
            loadingZone.SetAttribute("name", "LoadingZones");

            LoadingZone zone;
            foreach (object obj in ent.instances)
            {
                XmlElement zoneElement = doc.CreateElement(string.Empty, "Zone", string.Empty);
                loadingZone.AppendChild(zoneElement);

                zone = (LoadingZone)obj;

                //Create element for each section
                XmlElement x = doc.CreateElement(string.Empty, "X", string.Empty);
                XmlElement z = doc.CreateElement(string.Empty, "Z", string.Empty);
                XmlElement w = doc.CreateElement(string.Empty, "Width", string.Empty);
                XmlElement h = doc.CreateElement(string.Empty, "Height", string.Empty);
                XmlElement id1 = doc.CreateElement(string.Empty, "L1ID", string.Empty);
                XmlElement id2 = doc.CreateElement(string.Empty, "L2ID", string.Empty);
                x.InnerText = zone.xOff.ToString(); z.InnerText = zone.zOff.ToString();
                w.InnerText = zone.width.ToString(); h.InnerText = zone.height.ToString();
                id1.InnerText = zone.level1ID.ToString(); id2.InnerText = zone.level2ID.ToString();
                zoneElement.AppendChild(x); zoneElement.AppendChild(z);
                zoneElement.AppendChild(w); zoneElement.AppendChild(h);
                zoneElement.AppendChild(id1); zoneElement.AppendChild(id2);
            }
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
            tileZ.InnerText = (InvertZTile((int)ent.tile.y).ToString()); //Offset to make compatible with engine
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

        public static int InvertZTile(int originalZ)
        {
            return (int)EditorData.currentLevelHeight - originalZ - 1;
        }
    }
}
