using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace techdump.opengl.Components.Renderables
{
    public class TexturedRenderObject : ARenderable
    {
        private readonly int _texture;

        public TexturedRenderObject(TexturedQuad[] vertices, uint[] indices, int program, string filename)
            : base(program, vertices.Length, (uint)indices.Length)
        {
            // create first buffer: vertex
            GL.NamedBufferStorage(
                Buffer,
                TexturedVertex.Size * vertices.Length * 4,        // the size needed by this buffer
                vertices,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer

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
                2,                      // size of attribute, vec2
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                     // relative offset after a vec4

            GL.VertexArrayAttribBinding(VertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 2);
            GL.VertexArrayAttribFormat(
                VertexArray,
                2,                      // attribute index, from the shader location = 2
                3,                      // size of attribute, vec3
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                24);                    // relative offset after a vec4

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, TexturedVertex.Size);

            _texture = InitTextures(filename);
        }
        public TexturedRenderObject(TexturedVertex[] vertices, uint[] indices, int program, string filename)
            : base(program, vertices.Length, (uint)indices.Length)
        {
            // create first buffer: vertex
            GL.NamedBufferStorage(
                Buffer,
                TexturedVertex.Size * vertices.Length,        // the size needed by this buffer
                vertices,                           // data to initialize with
                BufferStorageFlags.MapWriteBit);    // at this point we will only write to the buffer

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
                2,                      // size of attribute, vec2
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                16);                     // relative offset after a vec4

            GL.VertexArrayAttribBinding(VertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 2);
            GL.VertexArrayAttribFormat(
                VertexArray,
                2,                      // attribute index, from the shader location = 2
                3,                      // size of attribute, vec3
                VertexAttribType.Float, // contains floats
                false,                  // does not need to be normalized as it is already, floats ignore this flag anyway
                24);                    // relative offset after a vec4

            // link the vertex array and buffer and provide the stride as size of Vertex
            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, TexturedVertex.Size);

            _texture = InitTextures(filename);
        }

        private int InitTextures(string filename)
        {
            int width, height;
            var data = LoadTexture(filename, out width, out height);
            int texture;
            GL.CreateTextures(TextureTarget.Texture2D, 1, out texture);
            GL.TextureStorage2D(
                texture,
                1,                           // levels of mipmapping
                SizedInternalFormat.Rgba32f, // format of texture
                width,
                height);

            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TextureSubImage2D(texture,
                0,                  // this is level 0
                0,                  // x offset
                0,                  // y offset
                width,
                height,
                PixelFormat.Rgba,
                PixelType.Float,
                data);
            return texture;
            // data not needed from here on, OpenGL has the data
        }

        private float[] LoadTexture(string filename, out int width, out int height)
        {
            float[] r;
            using (var bmp = (Bitmap)Image.FromFile(filename))
            {
                width = bmp.Width;
                height = bmp.Height;
                r = new float[width * height * 4];
                int index = 0;
                BitmapData data = null;
                try
                {
                    data = bmp.LockBits(
                        new Rectangle(0, 0, bmp.Width, bmp.Height),
                        ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    unsafe
                    {
                        var ptr = (byte*)data.Scan0;
                        int remain = data.Stride - data.Width * 3;
                        for (int i = 0; i < data.Height; i++)
                        {
                            for (int j = 0; j < data.Width; j++)
                            {
                                r[index++] = ptr[2] / 255f;
                                r[index++] = ptr[1] / 255f;
                                r[index++] = ptr[0] / 255f;
                                r[index++] = 1f;
                                ptr += 3;
                            }
                            ptr += remain;
                        }
                    }
                }
                finally
                {
                    bmp.UnlockBits(data);
                }
            }
            return r;
        }

        public override void Bind()
        {
            base.Bind();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteTexture(_texture);
            }
            base.Dispose(disposing);
        }
    }
}