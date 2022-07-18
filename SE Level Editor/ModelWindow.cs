using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using techdump.opengl.Components;
using techdump.opengl.Components.Renderables;

namespace Level_Editor
{
    class ModelWindow : GameWindow
    {
        private readonly List<ARenderable> _renderObjects = new List<ARenderable>();
        private readonly Color4 _backColor = new Color4(0.1f, 0.1f, 0.3f, 1.0f);
        private Matrix4 _projectionMatrix;
        private float _fov = 60f;
        private ShaderProgram _texturedProgram;
        private ShaderProgram _solidProgram;
        private uint _levelWidth;
        private uint _levelHeight;
        private Vector3 _lightDir;

        //Camera move
        private Matrix4 _camera;
        private Matrix4 _left;
        private Matrix4 _right;
        private Matrix4 _fwrd;
        private Matrix4 _back;
        private Matrix4 _up;
        private Matrix4 _down;

        public ModelWindow(int width, int height, string title, uint levelWidth, uint levelHeight) : base(width, height, GraphicsMode.Default, title) 
        {
            _levelWidth = levelWidth;
            _levelHeight = levelHeight;
            Title = "3D view";
            _lightDir = new Vector3(0.0f, 1.0f, 1.0f);
        }

        public static void RunWindow()
        {
            using (ModelWindow window = new ModelWindow(400, 200, "Model View", EditorData.currentLevelWidth, EditorData.currentLevelHeight))
            {
                window.Run(8.0);
            }
        }

        // Now, we start initializing OpenGL.
        protected override void OnLoad(EventArgs e)
        {
            CreateProjection();
            _solidProgram = new ShaderProgram();
            _solidProgram.AddShader(ShaderType.VertexShader, @"Shaders\1Vert\simplePipeVert.c");
            _solidProgram.AddShader(ShaderType.FragmentShader, @"Shaders\5Frag\simplePipeFrag.c");
            _solidProgram.Link();

            _texturedProgram = new ShaderProgram();
            _texturedProgram.AddShader(ShaderType.VertexShader, @"Shaders\1Vert\simplePipeTexVert.c");
            _texturedProgram.AddShader(ShaderType.FragmentShader, @"Shaders\5Frag\simplePipeTexFrag.c");
            _texturedProgram.Link();

            //Create level base mesh
            TexturedQuad[] mesh = ObjectFactory.CreateTexturedMesh(32f,_levelWidth,_levelHeight);
            uint[] indices = ObjectFactory.CreateMeshIndices(_levelWidth, _levelHeight);
            if (mesh.Length > 0)
            {
                GenerateTerrain(ref mesh);
                MapWorldUV(ref mesh);
                _renderObjects.Add(new TexturedRenderObject(mesh, indices, _texturedProgram.Id, EditorData.tilesetPath));
            }

            //Camera
            Matrix4 initialTranslation = Matrix4.CreateTranslation(new Vector3(32*(-_levelWidth/2), -180, -300));
            Matrix4 initialRotation = Matrix4.CreateRotationX(((float)Math.PI / 180f) * 30.0f);
            _camera = Matrix4.Identity;
            _camera *= initialTranslation * initialRotation;

            _left = Matrix4.CreateTranslation(new Vector3(5, 0, 0));
            _right = Matrix4.CreateTranslation(new Vector3(-5, 0, 0));
            _up = Matrix4.CreateTranslation(new Vector3(0, -5, 0));
            _down = Matrix4.CreateTranslation(new Vector3(0, 5, 0));
            _fwrd = Matrix4.CreateTranslation(new Vector3(0, 0, 5));
            _back = Matrix4.CreateTranslation(new Vector3(0, 0, -5));

            CursorVisible = true;

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            GL.PointSize(3);
            GL.Enable(EnableCap.DepthTest);
        }

        // Now that initialization is done, let's create our render loop.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(_backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelView = Matrix4.Identity;
            GL.UniformMatrix4(20, false, ref _projectionMatrix);
            GL.UniformMatrix4(21, false, ref modelView);
            GL.UniformMatrix4(22, false, ref _camera);
            GL.Uniform3(30, _lightDir);

            foreach(var obj in _renderObjects)
            {
                obj.Bind();
                obj.Render();
            }

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            CreateProjection();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Exit();
        }

        public override void Exit()
        {
            foreach (var obj in _renderObjects)
            {
                obj.Dispose();
            }

            base.Exit();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.W)
            {
                _camera *= _fwrd;
            }
            if (e.Key == Key.S)
            {
                _camera *= _back;
            }
            if (e.Key == Key.A)
            {
                _camera *= _left;
            }
            if (e.Key == Key.D)
            {
                _camera *= _right;
            }
            if (e.Key == Key.Q)
            {
                _camera *= _up;
            }
            if (e.Key == Key.E)
            {
                _camera *= _down;
            }
            if (e.Key == Key.F)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
            if (e.Key == Key.G)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
        }

        private void CreateProjection()
        {

            var aspectRatio = (float)Width / Height;
            _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                _fov * ((float)Math.PI / 180f), // field of view angle, in radians
                aspectRatio,                    // current window aspect ratio
                0.1f,                           // near plane
                4000f);                         // far plane
        }

        private void GenerateTerrain(ref TexturedQuad[] mesh)
        {
            //Raise each square by its world height
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    int index = (int)((y * EditorData.currentLevelWidth) + x);

                    //height
                    int height = EditorData.currentLevelHeights[x, LevelSerialize.InvertZTile(y)];
                    if (height != 0)
                    {
                        mesh[index].v1._position.Y = GetHeight(height);
                        mesh[index].v2._position.Y = GetHeight(height);
                        mesh[index].v3._position.Y = GetHeight(height);
                        mesh[index].v4._position.Y = GetHeight(height);
                    }

                    //slope
                    int direction = EditorData.currentLevelDirections[x, LevelSerialize.InvertZTile(y)];
                    if (direction != 0)
                    {
                        PositionVertices(ref mesh[index], x, y, height, direction);
                    }
                }
            }
            ObjectFactory.GenerateMeshNormals(ref mesh);
        }

        private float GetHeight(int height)
        {
            return (float)((float)(height * 32.0) * 1.0 / Math.Sqrt(2));
        }

        private void PositionVertices(ref TexturedQuad quad, int x, int y, int height, int direction)
        {
            //Get direction of tile
            switch (direction)
            {
                case 1:
                    quad.v1._position.Y = GetHeight(height+1);
                    quad.v4._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -90.0f);
                    break;
                case 2:
                    quad.v1._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f)*-90.0f);
                    break;
                case 3:
                    quad.v1._position.Y = GetHeight(height + 1);
                    break;
                case 4:
                    quad.v1._position.Y = GetHeight(height + 1);
                    quad.v4._position.Y = GetHeight(height + 1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -270.0f);
                    break;
                case 5:
                    quad.v1._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -180.0f);
                    break;
                case 6:
                    quad.v1._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -270.0f);
                    break;
                case 7:
                    quad.v1._position.Y = GetHeight(height + 1);
                    quad.v4._position.Y = GetHeight(height + 1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -180.0f);
                    break;
                case 8:
                    quad.v1._position.Y = GetHeight(height + 1);
                    quad.v4._position.Y = GetHeight(height + 1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * 0.0f);
                    break;
                case 9:
                    quad.v1._position.Y = GetHeight(height+1);
                    quad.v2._position.Y = GetHeight(height+1);
                    quad.v4._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -90.0f);
                    break;
                case 10:
                    quad.v1._position.Y = GetHeight(height+1);
                    quad.v2._position.Y = GetHeight(height+1);
                    quad.v4._position.Y = GetHeight(height+1);
                    break;
                case 11:
                    quad.v1._position.Y = GetHeight(height+1);
                    quad.v2._position.Y = GetHeight(height+1);
                    quad.v4._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -180.0f);
                    break;
                case 12:
                    quad.v1._position.Y = GetHeight(height+1);
                    quad.v2._position.Y = GetHeight(height+1);
                    quad.v4._position.Y = GetHeight(height+1);
                    ObjectFactory.RotateQuadY(ref quad, x, y, 32, ((float)Math.PI / 180f) * -270.0f);
                    break;
            }
        }

        public static void MapWorldUV(ref TexturedQuad[] quads)
        {
            float tileSize = (float)EditorData.tilesetImageWidth / (float)EditorData.currentTilesetImages.GetLength(0);
            float tilewidth = 32.0f/(float)EditorData.tilesetImageWidth;
            float tileheight = 32.0f/(float)EditorData.tilesetImageHeight;
            for (int y = 0; y < EditorData.currentLevelHeight; y++)
            {
                for (int x = 0; x < EditorData.currentLevelWidth; x++)
                {
                    int index = (int)((y * EditorData.currentLevelWidth) + x);

                    //Get texture tile
                    Tile texLoc = EditorData.currentLevelTextures[x, LevelSerialize.InvertZTile(y)];
                    float xPos = (float)texLoc.x * tilewidth;
                    float yPos = (float)texLoc.y * tileheight;
                    quads[index].v2._textureCoordinate = new Vector2(xPos, 1.0f-yPos);
                    quads[index].v3._textureCoordinate = new Vector2(xPos+tilewidth, 1.0f-yPos);
                    quads[index].v4._textureCoordinate = new Vector2(xPos+tilewidth, 1.0f-(yPos+tileheight));
                    quads[index].v1._textureCoordinate = new Vector2(xPos, 1.0f - (yPos + tileheight));
                }
            }
        }
    }
}
