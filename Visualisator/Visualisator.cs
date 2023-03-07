using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using System.IO;

namespace Maneger
{
    internal class Visualisator : GameWindow
    {
        Comand comand = new Comand();
        

        private int Pole_vboVertex = 0;
        private int Block_vboVertex = 0;
        private int Block_vboColor = 0;
        private int BackGround_vboVertex = 0;
        private int BackGround_vboColor = 0;
        private int Charecter_vboVertex = 0;
        private int Pole_Color_VBO = 0;
        private int vboColor = 0;
        private int Block_VAO = 0;
        int Scene_VAO = 0;
        float[] Pole_Vertex;
        float[] Pole_Color;
        float[] Block_Vertex;
        float[] Block_Color;
        float[] Charecters_Vertex;
        float[] BackGround_Vertex =
        {
            -1f, 1f, -1f, 0f, 0f,
            -1f, -1f, -1f, 0f, 1f,
            1f, -1f, -1f, 1f, 1f,
            1f, 1f, -1f, 1f, 0f
        };
        int[] BackGroundIdexes =
        {
            0, 1, 2, 3
        };
        public int[] TexturesIndex;
        string _command = "Pole";
        string _Tool_Chapter = "Tool_Block";
        int _Pole_Width;
        int _Pole_Height;
        Camera camera;
        Block[,] _blocks;
        Tools _tool;
        Scene[] _scene;
        int[,] Pole_Blocks;
        Vector2 Cursor;
        Vector2 SelectedBlock;
        int Selected_Scene = 1;
        Texture2D _texture = new Texture2D();
        Block PaternBlock;
        private Shader shader_tool;
        private Charecter MainCharecter;
        private Shader shader_scene;
        private Shader shader_animation;
        private int Charecter_VAO;
        Game testgame;





        public Visualisator(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, float[] Block_VBOs, float[] Charecter_VBOs, float[] Pole_VBOs, float[] Pole_Colors_VBOs, float[] block_Color, Block[,] blocks_bl, Scene[] Scenes, Charecter person) : base(gameWindowSettings, nativeWindowSettings)
        {
            VSync = VSyncMode.On;
            Pole_Vertex = Pole_VBOs;
            Pole_Color = Pole_Colors_VBOs;
            
            Block_Vertex = Block_VBOs;
            Charecters_Vertex = Charecter_VBOs;
            camera = new Camera(Vector2.Zero, 1.0, 0.0);
            _Pole_Width = Scenes[0].Width + 1;
            _Pole_Height = Scenes[0].Height + 1;
            Pole_Blocks = new int[_Pole_Width, _Pole_Height];
            Block_Color = block_Color;
            //blocks = new Block[_Pole_Width, _Pole_Height];
            _blocks = blocks_bl;
            _scene = Scenes;
            MainCharecter= person;
        }



        Vector2 Mouse_Location(Vector2 Cam_Position, double Cam_Zoom, Vector2 Cursor_Position)
        {
            Cursor_Position.X = (float)(((Cursor_Position.X * 2 / Size.X) - 1) / Cam_Zoom) + Cam_Position.X;
            Cursor_Position.Y = (float)(((Cursor_Position.Y * 2 / Size.Y) - 1) / Cam_Zoom) * (-1) + Cam_Position.Y;

            return Cursor_Position;
        }





        private int Pole_CreateVBOs(float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return vbo;
        }
        private void Pole_DrawVBOs()
        {

            GL.EnableClientState(ArrayCap.VertexArray);
          //  GL.EnableClientState(ArrayCap.ColorArray);



            GL.BindBuffer(BufferTarget.ArrayBuffer, Pole_vboVertex);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);

           // GL.BindBuffer(BufferTarget.ArrayBuffer, Pole_Color_VBO);
            GL.ColorPointer(4, ColorPointerType.Float, 0, 0);


            GL.DrawArrays(PrimitiveType.Lines, 0, Pole_Vertex.Length);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
          //  GL.DisableClientState(ArrayCap.ColorArray);
        }
        private void Pole_InitVBOs()
        {
            Pole_vboVertex = Pole_CreateVBOs(Pole_Vertex);
          //  Pole_Color_VBO = Pole_CreateVBOs(Pole_Color);
        }
        private void Pole_DeleteVBOs()
        {
            GL.DeleteBuffer(Pole_vboVertex);
            GL.DeleteBuffer(Pole_Color_VBO);
        }
        private void Pole_Update()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Pole_vboVertex);
            GL.BufferData(BufferTarget.ArrayBuffer, Pole_Vertex.Length * sizeof(float), Pole_Vertex, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private int Block_CreateVBOs(float[] data)
        {
            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return vbo;
        }
        private void Block_DrawVBOs()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);



            GL.BindBuffer(BufferTarget.ArrayBuffer, Block_vboVertex);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);


            GL.BindBuffer(BufferTarget.ArrayBuffer, Block_vboColor);
            GL.ColorPointer(4, ColorPointerType.Float, 0, 0);


            GL.DrawArrays(PrimitiveType.Quads, 0, Block_Vertex.Length);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
        }
        private void Block_InitVBOs()
        {
            Block_vboVertex = Block_CreateVBOs(Block_Vertex);
            Block_vboColor = Block_CreateVBOs(Block_Color);
        }
        private void Block_DeleteVBOs()
        {
            GL.DeleteBuffer(Block_vboVertex);
            GL.DeleteBuffer(Block_vboColor);
        }
        private void Update_Block_VBO(float[] vertex_data, float[] color_data)
        {           
            GL.BindBuffer(BufferTarget.ArrayBuffer, Block_vboVertex);
            GL.BufferData(BufferTarget.ArrayBuffer, vertex_data.Length * sizeof(float), vertex_data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Block_vboColor);
            GL.BufferData(BufferTarget.ArrayBuffer, color_data.Length * sizeof(float), color_data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private int Scene_CreateVAOs()
        {

            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, BackGround_Vertex.Length * sizeof(float), BackGround_Vertex, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, BackGroundIdexes.Length * sizeof(uint), BackGroundIdexes, BufferUsageHint.StaticDraw);

            shader_scene.ActiveProgram();

            var VertexLocation = shader_scene.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader_scene.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture.BindTexture(_scene[Selected_Scene - 1].TextureID);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader_scene.DiactiveProgram();
            GL.DeleteBuffer(vbo);
            return vao;
        }
        private void Draw_Scene()
        {
            GL.BindVertexArray(Scene_VAO);
            _texture.BindTexture(_scene[Selected_Scene - 1].TextureID);
            shader_scene.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, BackGroundIdexes.Length, DrawElementsType.UnsignedInt, 0);
            shader_scene.DiactiveProgram();
            GL.BindVertexArray(0);
        }
        private void Delete_SceneVAO()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(Scene_VAO);
        }


        void Draw_Selected_Block(float x, float y)
        {      
                     
            Pole_Blocks[Convert.ToInt32(Math.Round((((x+1) / 2) * (_Pole_Width-1)), 0)), Convert.ToInt32(Math.Round((((y+1) / 2) * (_Pole_Height-1)), 0))] = 1;
            Block_Vertex = comand.Draw_Block_VBO(Pole_Blocks, _Pole_Width, _Pole_Height, ref Block_Color, SelectedBlock, _blocks);
            Update_Block_VBO(Block_Vertex, Block_Color);
        
                   
        }
        void Delete_Selected_Block(float x, float y)
        { 
            Pole_Blocks[Convert.ToInt32(Math.Round((((x + 1) / 2) * (_Pole_Width - 1)), 0)), Convert.ToInt32(Math.Round((((y + 1) / 2) * (_Pole_Height - 1)), 0))] = 0;
            Block_Vertex = comand.Draw_Block_VBO(Pole_Blocks, _Pole_Width, _Pole_Height, ref Block_Color, SelectedBlock, _blocks);
            Update_Block_VBO(Block_Vertex, Block_Color);
            
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            Pole_InitVBOs();
            
            Block_InitVBOs();                       

            shader_tool = new Shader(@"Data\Shader\Tool_Shader.vert", @"Data\Shader\Tool_Shader.frag");
            shader_scene = new Shader(@"Data\Shader\Scene_Shader.vert", @"Data\Shader\Scene_Shader.frag");
            shader_animation = new Shader(@"Data\Shader\Animation_Shader.vert", @"Data\Shader\Animation_Shader.frag");
            _tool = new Tools(shader_tool);
            _scene[Selected_Scene - 1].TextureID = _texture.CreateTexture(_scene[Selected_Scene - 1].TexturePath);
            MainCharecter.AnimatioID = _texture.CreateTexture(MainCharecter.AnimationPath);
            Scene_VAO = Scene_CreateVAOs();
            _scene[Selected_Scene - 1].CreateSceneBackGround();
        } 
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var keys = KeyboardState;
            if (keys.IsKeyDown(Keys.I))
            {
                _command = "Tools";
            }
            if (keys.IsKeyDown(Keys.U))
            {
                _command = "Pole";
            }
            if(keys.IsKeyDown(Keys.F5))
            {
                _command = "Game";
                _scene[Selected_Scene - 1] = _tool.Save_Scene(_blocks, Pole_Blocks, _scene[Selected_Scene - 1]);
                testgame = new Game(MainCharecter, shader_animation,_scene[Selected_Scene - 1], camera);
            }
            if(_command != "Tools")
                 camera.Moves(keys);
            if(_command == "Game")
            {
                if (keys.IsKeyDown(Keys.Right))
                    testgame.MoveRight();
                if (keys.IsKeyDown(Keys.Left))
                    testgame.MoveLeft();
                if (keys.IsKeyDown(Keys.Up))
                    testgame.MoveUP(); 
                if (keys.IsKeyDown(Keys.Down))
                    testgame.MoveDown();
            }
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.LoadIdentity();
            camera.ApplyTransform();
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (_command == "Pole")
            {
                Draw_Scene();
                Pole_DrawVBOs();
                Block_DrawVBOs();
  
            }
            if(_command ==  "Game")
            {
               
                GameTest_Draw();

            } 
            if (_command == "Tools")
            {
                camera.position.X = 0;
                camera.position.Y = 0;
                camera.zoom = 1;
                if(_Tool_Chapter == "Tool_Block")
                    if(SelectedBlock.X!=-1||SelectedBlock.Y!=-1)
                     _tool.Draw_Tool_Block(_blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y], SelectedBlock);
                if (_Tool_Chapter == "Tool_Scene")
                    _tool.Draw_Tool_Scene();
                if(_Tool_Chapter == "Tool_Charecter")
                    _tool.Draw_Tool_Charecter(MainCharecter);
                if (_Tool_Chapter == "Tool_Menu")
                    _tool.Draw_Tool_Menu();
                if (_Tool_Chapter == "Tool_Scene_Redactor")
                    _tool.Draw_Tool_SceneRedactor(_scene[Selected_Scene - 1], Selected_Scene);
            }
               


            SwapBuffers();
        }
        protected override void OnUnload()
        {
            Pole_DeleteVBOs();
            Block_DeleteVBOs();
            Delete_SceneVAO();
            _texture.DeleteTexture(_tool.Texture_ID);
            base.OnUnload();

        }



        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = Mouse_Location(camera.position, camera.zoom, e.Position);

        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (_command == "Pole")
            {
                if (e.Button == MouseButton.Left)
                {
                    SelectedBlock.X = Convert.ToInt32(Math.Round((((Cursor.X + 1) / 2) * (_Pole_Width - 1)), 0));
                    SelectedBlock.Y = Convert.ToInt32(Math.Round((((Cursor.Y + 1) / 2) * (_Pole_Height - 1)), 0));
                    
                    Draw_Selected_Block(Cursor.X, Cursor.Y);
                    if (PaternBlock != null)
                    {
                        Block localblock = new Block(PaternBlock.IsStatic, PaternBlock.IsMaterial, PaternBlock.Texture_Path, PaternBlock.IsDamage, PaternBlock.Damage, PaternBlock.TextureID, PaternBlock.Color);
                        _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] = localblock;
                    }                
                }
                if (e.Button == MouseButton.Right)
                {
                    Delete_Selected_Block(Cursor.X, Cursor.Y);
                }
            }
            if(_command == "Tools")
            {
                if(e.Button == MouseButton.Left)
                {
                    Choose_Tool(Cursor.X, Cursor.Y);
                }
            }


        }



        public void Choose_Tool(float X_Click, float Y_Click)
        {
            if(_Tool_Chapter == "Tool_Scene_Redactor")
            {
                if (X_Click > -1f && X_Click < -0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Block";
                }
                if (X_Click > -0.5f && X_Click < 0f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Scene";
                }
                if (X_Click > 0f && X_Click < 0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Charecter";
                }
                if (X_Click > 0.5f && X_Click < 1f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Menu";
                }
                if(X_Click> -0.5f && X_Click < -0.3f && Y_Click > 0f && Y_Click < 0.2f)
                {
                    _scene[Selected_Scene-1] = _tool.GetWidth(_scene[Selected_Scene - 1]);
                }
                if (X_Click > -0.5f && X_Click < -0.3f && Y_Click > -0.4f && Y_Click < -0.2f)
                {
                    _scene[Selected_Scene-1] = _tool.GetHeight(_scene[Selected_Scene - 1]);
                }
                if(X_Click > 0.5f && X_Click < 0.8f && Y_Click > -0.4f && Y_Click < -0.2f)
                {
                    _tool.Choose_Scene(ref Pole_Vertex, ref Pole_Color, ref _blocks, ref Pole_Blocks, ref _Pole_Width, ref _Pole_Height, ref SelectedBlock, _scene[Selected_Scene - 1], ref Block_Vertex, ref Block_Color);
                    GL.ClearColor(Color4.Black);
                    GL.Clear(ClearBufferMask.ColorBufferBit);
                    Pole_Update();
                    Block_Vertex = comand.Draw_Block_VBO(Pole_Blocks, _Pole_Width, _Pole_Height, ref Block_Color, SelectedBlock, _blocks);
                    Update_Block_VBO(Block_Vertex, Block_Color);                
                    //Block_DrawVBOs();
                }
                if(X_Click > -0.5f && X_Click < 0.2f && Y_Click > -0.8f && Y_Click < -0.6f)
                {
                    _scene[Selected_Scene - 1] = _tool.TexturePath(_scene[Selected_Scene - 1]);
                    if(File.Exists(_scene[Selected_Scene - 1].TexturePath))
                    {
                        _texture.UpDate_Texture(_scene[Selected_Scene - 1].TextureID, _scene[Selected_Scene - 1].TexturePath);
                    }
                    else
                    {
                        _texture.UpDate_Texture(_scene[Selected_Scene-1].TextureID, "Data/Texts/Null_Texture.png");
                    }
                    
                }
                if(X_Click > 0.3f && X_Click < 0.5f && Y_Click > 0.4f && Y_Click < 0.6f)
                {
                    _scene[Selected_Scene - 1] = _tool.CheckPointX(_scene[Selected_Scene - 1]);
                }
                if (X_Click > 0.3f && X_Click < 0.5f && Y_Click > 0f && Y_Click < 0.2f)
                {
                    _scene[Selected_Scene - 1] = _tool.CheckPointY(_scene[Selected_Scene - 1]);
                }
                if (X_Click > 0.5f && X_Click < 0.8f && Y_Click > -0.8f && Y_Click < -0.6f)
                {
                    _scene[Selected_Scene - 1] = _tool.ClearScene(_scene[Selected_Scene - 1]);
                    GL.ClearColor(Color4.Black);
                    GL.Clear(ClearBufferMask.ColorBufferBit);
                }
            }
            if (_Tool_Chapter == "Tool_Block")
            {
                if(X_Click > -1f && X_Click < -0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Block";
                }
                if(X_Click > -0.5f && X_Click < 0f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Scene";
                }
                if(X_Click > 0f && X_Click < 0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Charecter";
                }
                if(X_Click > 0.5f && X_Click < 1f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Menu";
                }
                if(SelectedBlock.X != -1 && SelectedBlock.Y != -1)
                {
                    if (Pole_Blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] != 0 && X_Click > -0.5f && X_Click < -0.4f && Y_Click > 0.4f && Y_Click < 0.6f)
                    {
                        _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] = _tool.IsMaterial(_blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y]);
                    }
                    if (Pole_Blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] != 0 && X_Click > -0.5f && X_Click < -0.4f && Y_Click > 0f && Y_Click < 0.2f)
                    {
                        _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] = _tool.IsStatic(_blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y]);
                    }
                    if (Pole_Blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] != 0 && X_Click > -0.5f && X_Click < -0.4f && Y_Click > -0.4f && Y_Click < -0.2f)
                    {
                        _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] = _tool.IsDamage(_blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y]);
                    }
                    if (Pole_Blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] != 0 && X_Click > -0.3f && X_Click < 0f && Y_Click > -0.4f && Y_Click < -0.2f)
                    {
                        _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] = _tool.Damage(_blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y]);
                        this.Focus();
                    }
                    if (Pole_Blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] != 0 && X_Click > -0.5f && X_Click < 0f && Y_Click > -0.8f && Y_Click < -0.6f)
                    {
                        _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] = _tool.TexturePath(_blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y]);
                        this.Focus();
                    }
                    if(Pole_Blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y] != 0 && X_Click > 0.6f && X_Click < 0.9f && Y_Click > 0.2f && Y_Click < 0.4f)
                    {
                        PaternBlock = _blocks[(int)SelectedBlock.X, (int)SelectedBlock.Y];
                    }
                }
            }
            if (_Tool_Chapter == "Tool_Scene")
            {
                if (X_Click > -1f && X_Click < -0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Block";
                }
                if (X_Click > -0.5f && X_Click < 0f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Scene";
                }
                if (X_Click > 0f && X_Click < 0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Charecter";
                }
                if (X_Click > 0.5f && X_Click < 1f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Menu";
                }
                int scene_nymber = 0;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        scene_nymber++;
                        if(X_Click>-0.85f + 0.3*j && X_Click<-0.65f + 0.3 * j && Y_Click>0.2f - 1f*i && Y_Click<0.6f - 1f * i)
                            {                            
                            _scene[Selected_Scene - 1] = _tool.Save_Scene(_blocks, Pole_Blocks, _scene[Selected_Scene - 1]);
                            Selected_Scene = scene_nymber;
                            if(Selected_Scene > _scene.Length)
                            {
                                Array.Resize(ref _scene, Selected_Scene);
                                comand.Creating_New_Scene(_scene);
                                _scene[Selected_Scene - 1].CreateSceneBackGround();
                            }
                            _Tool_Chapter = "Tool_Scene_Redactor";
                            break;
                        }
           
                    }
                }
            }
            if (_Tool_Chapter == "Tool_Charecter")
            {
                if (X_Click > -1f && X_Click < -0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Block";
                }
                if (X_Click > -0.5f && X_Click < 0f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Scene";
                }
                if (X_Click > 0f && X_Click < 0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Charecter";
                }
                if (X_Click > 0.5f && X_Click < 1f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Menu";
                }
                if (X_Click > -0.5f && X_Click < -0.3f && Y_Click > 0.4f && Y_Click < 0.6f)
                {
                    MainCharecter = _tool.GetCharecterWidth(MainCharecter);
                }
                if (X_Click > 0.3f && X_Click < 0.5f && Y_Click > 0.4f && Y_Click < 0.6f)
                {
                    MainCharecter = _tool.GetCharecterHeight(MainCharecter);
                }
                if (X_Click > -0.5f && X_Click < -0.3f && Y_Click > 0f && Y_Click < 0.2f)
                {
                    MainCharecter = _tool.GetCharecterHitPoints(MainCharecter);
                }
                if (X_Click > -0.5f && X_Click < -0.3f && Y_Click > -0.4f && Y_Click < -0.2f)
                {
                    MainCharecter = _tool.GetCharecterAnimationWidth(MainCharecter);
                }
                if (X_Click > -0.5f && X_Click < 0.2f && Y_Click > -0.8f && Y_Click < -0.6f)
                {
                    MainCharecter = _tool.GetCharecterAnimationPath(MainCharecter);
                    try
                    {
                    File.Delete("Data/Texts/Tool_Texture/52.png");
                    File.Copy(MainCharecter.AnimationPath, "Data/Texts/Tool_Texture/52.png"); 
                    }
                    catch(Exception ex)
                    {

                    }
                                   
                }
            }
            if (_Tool_Chapter == "Tool_Menu")
            {
                if (X_Click > -1f && X_Click < -0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Block";
                }
                if (X_Click > -0.5f && X_Click < 0f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Scene";
                }
                if (X_Click > 0f && X_Click < 0.5f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Charecter";
                }
                if (X_Click > 0.5f && X_Click < 1f && Y_Click > 0.8f && Y_Click < 1f)
                {
                    _Tool_Chapter = "Tool_Menu";
                }
                if (X_Click > -0.9f && X_Click < 0.9f && Y_Click > -0.4f && Y_Click < -0.2f)
                {
                    this.Close();
                }
            }
            

        }


        private void GameTest_Draw()
        {
            SelectedBlock.X = -1;
            SelectedBlock.Y = -1;
            Block_Vertex = comand.Draw_Block_VBO(Pole_Blocks, _Pole_Width, _Pole_Height, ref Block_Color, SelectedBlock, _blocks);
            Update_Block_VBO(Block_Vertex, Block_Color);
            Draw_Scene();
            Block_DrawVBOs();
            testgame.Draw_Charecter();
        }
    }
}
