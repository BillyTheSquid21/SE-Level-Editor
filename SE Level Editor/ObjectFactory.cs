using System;
using OpenTK;
using OpenTK.Graphics;
using techdump.opengl.Components.Renderables;

namespace techdump.opengl.Components
{
    public struct ColoredQuad
    {
        public ColoredVertex v1;
        public ColoredVertex v2;
        public ColoredVertex v3;
        public ColoredVertex v4;
    }

    public struct TexturedQuad
    {
        public TexturedVertex v1;
        public TexturedVertex v2;
        public TexturedVertex v3;
        public TexturedVertex v4;
    }
    public class ObjectFactory
    {
        public static TexturedQuad[] CreateTexturedMesh(float quadSize, uint width, uint height)
        {
            TexturedQuad[] vertices = new TexturedQuad[width * height];
            int index = 0;
            for (float y = 0; y > -height * quadSize; y -= quadSize)
            {
                for (float x = 0; x < width * quadSize; x += quadSize)
                {
                    var v1 = new TexturedVertex(new Vector4(x, 0.0f, y, 1.0f), new Vector2(0.0f,0.8f), new Vector3(1.0f, 0.0f, 0.0f));
                    var v2 = new TexturedVertex(new Vector4(x + quadSize, 0.0f, y, 1.0f), new Vector2(0.2f, 0.8f), new Vector3(1.0f, 0.0f, 0.0f));
                    var v3 = new TexturedVertex(new Vector4(x + quadSize, 0.0f, y + quadSize, 1.0f), new Vector2(0.2f, 1), new Vector3(1.0f, 0.0f, 0.0f));
                    var v4 = new TexturedVertex(new Vector4(x, 0.0f, y + quadSize, 1.0f), new Vector2(0.0f, 1), new Vector3(1.0f, 0.0f, 0.0f));

                    vertices[index].v1 = v1;
                    vertices[index].v2 = v2;
                    vertices[index].v3 = v3;
                    vertices[index].v4 = v4;
                    index++;
                }
            }
            return vertices;
        }
        public static ColoredQuad[] CreateSolidMesh(float quadSize, uint width, uint height, Color4 color)
        {
            ColoredQuad[] vertices = new ColoredQuad[width*height];
            int index = 0;
            for (float y = 0; y > -height*quadSize; y-=quadSize)
            {
                for (float x = 0; x < width*quadSize; x+=quadSize)
                {
                    var v1 = new ColoredVertex( new Vector4(x, 0.0f, y, 1.0f), Color4.HotPink, new Vector3(1.0f,0.0f,0.0f));
                    var v2 = new ColoredVertex(new Vector4(x+quadSize, 0.0f, y, 1.0f), Color4.HotPink, new Vector3(1.0f, 0.0f, 0.0f));
                    var v3 = new ColoredVertex(new Vector4(x+quadSize, 0.0f, y+quadSize, 1.0f), Color4.HotPink, new Vector3(1.0f, 0.0f, 0.0f));
                    var v4 = new ColoredVertex(new Vector4(x, 0.0f, y+quadSize, 1.0f), Color4.HotPink, new Vector3(1.0f, 0.0f, 0.0f));

                    vertices[index].v1 = v1;
                    vertices[index].v2 = v2;
                    vertices[index].v3 = v3;
                    vertices[index].v4 = v4;
                    index++;
                }
            }
            return vertices;
        }

        public static uint[] CreateMeshIndices(uint width, uint height)
        {
            //Find total ints needed
            uint quadCount = width * height;
            uint quadIntCount = quadCount * 6;
            uint[] indices = new uint[quadIntCount];

            uint indicesIndex = 0;

            int lastLargest = -1;

            for (uint i = 0; i < quadCount; i++)
            {
                //Increment all by last largest - set temp to base
                uint[] indicesTemp = {
                    0,1,2,0,2,3
                };
                for (int j = 0; j < 6; j++)
                {
                    indicesTemp[j] += (uint)(lastLargest + 1);
                }
                //Set last largest
                lastLargest = (int)indicesTemp[6 - 1];
                for (int k = 0; k < 6; k++)
                {
                    indices[indicesIndex] = indicesTemp[k];
                    indicesIndex++;
                }
            }
            return indices;
        }

        private static Vector3 Vec4ToVec3(Vector4 vec4)
        {
            return new Vector3(vec4.X, vec4.Y, vec4.Z);
        }
        public static void GenerateMeshNormals(ref TexturedQuad[] mesh)
        {
            int total = mesh.Length;
            //set all normals to 0
            for (int i = 0; i < total; i++)
            {
                mesh[i].v1._normal = new Vector3(0.0f, 0.0f, 0.0f);
                mesh[i].v2._normal = new Vector3(0.0f, 0.0f, 0.0f);
                mesh[i].v3._normal = new Vector3(0.0f, 0.0f, 0.0f);
                mesh[i].v4._normal = new Vector3(0.0f, 0.0f, 0.0f);
            }
            //calc each triangle normal
            for (int i = 0; i < total; i++)
            {
                //Tri 1 - 0,1,2
                Vector4 tri1Edge1 = mesh[i].v2._position - mesh[i].v1._position;
                Vector4 tri1Edge2 = mesh[i].v1._position - mesh[i].v3._position;
                Vector3 tri1Norm = Vector3.Normalize(Vector3.Cross(Vec4ToVec3(tri1Edge1), Vec4ToVec3(tri1Edge2)));

                //Set norms 1
                mesh[i].v1._normal += tri1Norm;
                mesh[i].v2._normal += tri1Norm;
                mesh[i].v3._normal += tri1Norm;

                //Tri 2 - 0,2,3
                Vector4 tri2Edge1 = mesh[i].v3._position - mesh[i].v1._position;
                Vector4 tri2Edge2 = mesh[i].v1._position - mesh[i].v4._position;
                Vector3 tri2Norm = Vector3.Normalize(Vector3.Cross(Vec4ToVec3(tri2Edge1), Vec4ToVec3(tri2Edge2)));

                //Set norms 2
                mesh[i].v1._normal += tri2Norm;
                mesh[i].v3._normal += tri2Norm;
                mesh[i].v4._normal += tri2Norm;

                //Normalise all
                mesh[i].v1._normal = Vector3.Normalize(mesh[i].v1._normal);
                mesh[i].v2._normal = Vector3.Normalize(mesh[i].v2._normal);
                mesh[i].v3._normal = Vector3.Normalize(mesh[i].v3._normal);
                mesh[i].v4._normal = Vector3.Normalize(mesh[i].v4._normal);
            }
        }

        public static void GenerateMeshNormals(ref ColoredQuad[] mesh)
        {
            int total = mesh.Length;
            //set all normals to 0
            for (int i = 0; i < total; i++)
            {
                mesh[i].v1._normal = new Vector3(0.0f, 0.0f, 0.0f);
                mesh[i].v2._normal = new Vector3(0.0f, 0.0f, 0.0f);
                mesh[i].v3._normal = new Vector3(0.0f, 0.0f, 0.0f);
                mesh[i].v4._normal = new Vector3(0.0f, 0.0f, 0.0f);
            }
            //calc each triangle normal
            for (int i = 0; i < total; i++)
            {
                //Tri 1 - 0,1,2
                Vector4 tri1Edge1 = mesh[i].v2._position - mesh[i].v1._position;
                Vector4 tri1Edge2 = mesh[i].v1._position - mesh[i].v3._position;
                Vector3 tri1Norm = Vector3.Normalize(Vector3.Cross(Vec4ToVec3(tri1Edge1), Vec4ToVec3(tri1Edge2)));

                //Set norms 1
                mesh[i].v1._normal += tri1Norm;
                mesh[i].v2._normal += tri1Norm;
                mesh[i].v3._normal += tri1Norm;

                //Tri 2 - 0,2,3
                Vector4 tri2Edge1 = mesh[i].v3._position - mesh[i].v1._position;
                Vector4 tri2Edge2 = mesh[i].v1._position - mesh[i].v4._position;
                Vector3 tri2Norm = Vector3.Normalize(Vector3.Cross(Vec4ToVec3(tri2Edge1), Vec4ToVec3(tri2Edge2)));

                //Set norms 2
                mesh[i].v1._normal += tri2Norm;
                mesh[i].v3._normal += tri2Norm;
                mesh[i].v4._normal += tri2Norm;

                //Normalise all
                mesh[i].v1._normal = Vector3.Normalize(mesh[i].v1._normal);
                mesh[i].v2._normal = Vector3.Normalize(mesh[i].v2._normal);
                mesh[i].v3._normal = Vector3.Normalize(mesh[i].v3._normal);
                mesh[i].v4._normal = Vector3.Normalize(mesh[i].v4._normal);
            }
        }

        public static void RotateQuadY(ref ColoredQuad quad, int xValue, int yValue, float quadWidth, float angle)
        {
            Vector3 cOfRotation = new Vector3();

            float x = quad.v1._position.X + (quad.v2._position.X - quad.v1._position.X) / 2.0f;
            float z = quad.v1._position.Z + (quad.v3._position.Z - quad.v1._position.Z) / 2.0f;
            float y = quad.v1._position.Y;
            cOfRotation.X = x; cOfRotation.Z = z; cOfRotation.Y = y;

            Matrix4 rotate = Matrix4.CreateRotationY(angle);
            Matrix4 translateIn = Matrix4.CreateTranslation(-cOfRotation);
            Matrix4 translateOut = Matrix4.CreateTranslation(cOfRotation);

            //Pull to centre
            quad.v1._position *= translateIn;
            quad.v2._position *= translateIn;
            quad.v3._position *= translateIn;
            quad.v4._position *= translateIn;

            //Rotate
            quad.v1._position *= rotate;
            quad.v2._position *= rotate;
            quad.v3._position *= rotate;
            quad.v4._position *= rotate;

            //Return
            quad.v1._position *= translateOut;
            quad.v2._position *= translateOut;
            quad.v3._position *= translateOut;
            quad.v4._position *= translateOut;
        }

        public static void RotateQuadY(ref TexturedQuad quad, int xValue, int yValue, float quadWidth, float angle)
        {
            Vector3 cOfRotation = new Vector3();

            float x = quad.v1._position.X + (quad.v2._position.X - quad.v1._position.X) / 2.0f;
            float z = quad.v1._position.Z + (quad.v3._position.Z - quad.v1._position.Z) / 2.0f;
            float y = quad.v1._position.Y;
            cOfRotation.X = x; cOfRotation.Z = z; cOfRotation.Y = y;

            Matrix4 rotate = Matrix4.CreateRotationY(angle);
            Matrix4 translateIn = Matrix4.CreateTranslation(-cOfRotation);
            Matrix4 translateOut = Matrix4.CreateTranslation(cOfRotation);

            //Pull to centre
            quad.v1._position *= translateIn;
            quad.v2._position *= translateIn;
            quad.v3._position *= translateIn;
            quad.v4._position *= translateIn;

            //Rotate
            quad.v1._position *= rotate;
            quad.v2._position *= rotate;
            quad.v3._position *= rotate;
            quad.v4._position *= rotate;

            //Return
            quad.v1._position *= translateOut;
            quad.v2._position *= translateOut;
            quad.v3._position *= translateOut;
            quad.v4._position *= translateOut;
        }
    }
}