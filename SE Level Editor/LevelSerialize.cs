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
        public static void CreateLevel(uint width, uint height, uint id, int originX, int originZ, int terrainHeight, string path)
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

            //Get data
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
    }
}
