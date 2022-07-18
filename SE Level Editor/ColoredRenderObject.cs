using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace techdump.opengl.Components.Renderables
{
    public class ColoredRenderObject : ARenderable
    {
        public ColoredRenderObject(ColoredVertex[] vertices, uint[] indices, int program)
            : base(program, vertices.Length, (uint)indices.Length)
        {
            // create first buffer: vertex
            GL.NamedBufferStorage(
                Buffer,
                ColoredVertex.Size * vertices.Length,   // the size needed by this buffer
                vertices,                               // data to initialize with
                BufferStorageFlags.MapWriteBit);        // at this point we will only write to the buffer

            //Create next buffer: indices
            GL.NamedBufferStorage(
                ElementBuffer, 
                sizeof(uint) * indices.Length, 
                indices, 
                BufferStorageFlags.MapWriteBit);


            GL.VertexArrayAttribBinding(VertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 0);
            GL.VertexArrayAttribFormat(
                VertexArray,
                0,                      // attribute index, from the shader location = 0
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item


            GL.VertexArrayAttribBinding(VertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 1);
            GL.VertexArrayAttribFormat(
                VertexArray,
                1,                      // attribute index, from the shader location = 1
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                    // relative offset after a vec4

            GL.VertexArrayAttribBinding(VertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 2);
            GL.VertexArrayAttribFormat(
                VertexArray,
                2,                      // attribute index, from the shader location = 2
                3,                      // size of attribute, vec3
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                32);                    // relative offset after a vec4

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, ColoredVertex.Size);
        }

        public ColoredRenderObject(ColoredQuad[] vertices, uint[] indices, int program)
            : base(program, vertices.Length, (uint)indices.Length)
        {
            // create first buffer: vertex
            GL.NamedBufferStorage(
                Buffer,
                ColoredVertex.Size * vertices.Length * 4,   // the size needed by this buffer (in this overload *4 for verts in quad)
                vertices,                                   // data to initialize with
                BufferStorageFlags.MapWriteBit);            // at this point we will only write to the buffer

            //Create next buffer: indices
            GL.NamedBufferStorage(
                ElementBuffer,
                sizeof(uint) * indices.Length,
                indices,
                BufferStorageFlags.MapWriteBit);


            GL.VertexArrayAttribBinding(VertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 0);
            GL.VertexArrayAttribFormat(
                VertexArray,
                0,                      // attribute index, from the shader location = 0
                4,          // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                0);                     // relative offset, first item


            GL.VertexArrayAttribBinding(VertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 1);
            GL.VertexArrayAttribFormat(
                VertexArray,
                1,                      // attribute index, from the shader location = 1
                4,                      // size of attribute, vec4
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                    // relative offset after a vec4

            GL.VertexArrayAttribBinding(VertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 2);
            GL.VertexArrayAttribFormat(
                VertexArray,
                2,                      // attribute index, from the shader location = 2
                3,                      // size of attribute, vec3
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                32);                    // relative offset after a vec4

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, ColoredVertex.Size);
        }
    }
}