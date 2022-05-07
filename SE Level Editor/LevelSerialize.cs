using System.Collections.Generic;
using Newtonsoft.Json;
using System.Dynamic;
using System.IO;
using System.Xml;

namespace Level_Editor
{
    class LevelSerialize
    {
        public static void CreateLevel(uint width, uint height, uint id, int originX, int originZ, string path)
        {
            dynamic level = new ExpandoObject();

            level.levelID = id;
            level.levelWidth = width; level.levelHeight = height;
            level.levelOriginX = originX; level.levelOriginZ = originZ;
            level.worldLevels = "0";

            //Add rows, columns, etc
            const string planeHeights = "planeHeightsR";
            const string planeDirections = "planeDirectionsR";
            const string planeTextures = "planeTexturesR";
            const string planePermissions = "l0PlanePermissionsR";
            var dictionary = (IDictionary<string, object>)level;
            for (int y = 0; y < height; y++)
            {
                //Row name
                string pHeights = planeHeights + y;
                string pDirections = planeDirections + y;
                string pTextures = planeTextures + y;
                string pPermissions = planePermissions + y;

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
    }
}
