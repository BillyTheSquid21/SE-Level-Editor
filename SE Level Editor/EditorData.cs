using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    //Stores data used in level editing between objects
    static class EditorData
    {
        static public Tile s_CurrentTextureTile = new Tile();

    }

    
}
