using System;
using OpenTK.Graphics.OpenGL4;

namespace techdump.opengl.Components.Renderables
{
    public abstract class ARenderable : IDisposable
    {
        protected readonly int Program;
        protected readonly int VertexArray;
        protected readonly int Buffer;
        protected readonly int ElementBuffer;
        protected readonly int VerticeCount;
        protected readonly uint IndiceCount;

        protected ARenderable(int program, int vertexCount, uint indiceCount)
        {
            Program = program;
            VerticeCount = vertexCount;
            IndiceCount = indiceCount;
            VertexArray = GL.GenVertexArray();
            Buffer = GL.GenBuffer();
            ElementBuffer = GL.GenBuffer();

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer);
        }
        public virtual void Bind()
        {
            GL.UseProgram(Program);
            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer);
        }
        public virtual void Render()
        {
            GL.DrawElements(BeginMode.Triangles, (int)IndiceCount, DrawElementsType.UnsignedInt, 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteVertexArray(VertexArray);
                GL.DeleteBuffer(Buffer);
            }
        }
    }
}