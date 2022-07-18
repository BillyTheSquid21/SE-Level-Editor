using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace techdump.opengl.Components.Renderables
{
    public struct Vertex
    {
        public const int Size = (4 + 4) * 4; // size of struct in bytes

        public Vector4 position;
        public readonly Color4 color;

        public Vertex(Vector4 pos, Color4 col)
        {
            position = pos;
            color = col;
        }
    }

    public struct ColoredVertex
    {
        public const int Size = (4 + 4 + 3) * 4; // size of struct in bytes

        public Vector4 _position;
        public Color4 _color;
        public Vector3 _normal;

        public ColoredVertex(Vector4 position, Color4 color, Vector3 normal)
        {
            _position = position;
            _color = color;
            _normal = normal;
        }
    }

    public struct TexturedVertex
    {
        public const int Size = (4 + 2 + 3) * 4; // size of struct in bytes

        public Vector4 _position;
        public Vector2 _textureCoordinate;
        public Vector3 _normal;

        public TexturedVertex(Vector4 pos, Vector2 tex, Vector3 normal)
        {
            _position = pos;
            _textureCoordinate = tex;
            _normal = normal;
        }
    }
}
