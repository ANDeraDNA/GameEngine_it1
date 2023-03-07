using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WpfLibrary1;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Windows.Media.TextFormatting;
using OpenTK.Compute.OpenCL;
using System.Windows.Documents;


namespace Maneger
{
    internal class Tools
    {
    
        private int[] Tool_Textures_ID = new int[0];
        private int[] Tool_VAO = new int[59];
        public int[] Texture_ID
        {
            get { return Tool_Textures_ID; }
        }
        Shader shader;
        Texture2D texture;
        public int[] Tool_Textures()
        {
            
            int[] indices = new int[59];
            for (int i = 0; i < 59; i++)
            {
                if(File.Exists($"Data/Texts/Tool_Texture/{i}.png"))
                {
                indices[i] = texture.CreateTexture($"Data/Texts/Tool_Texture/{i}.png"); 
                }
                else
                {
                    indices[i] = texture.CreateTexture($"Data/Texts/Null_Texture.png");
                }
            }
            return indices;
        }

        public Tools(Shader _shd)
        {
            shader = _shd;
            texture = new Texture2D();
            Tool_Textures_ID = Tool_Textures();
            Generate_Tool();

        }
  

        //Block Method
        public Block IsMaterial(Block block)
        {
            if(block.IsMaterial == true)
            {
                block.IsMaterial = false;
            }
            else
            {
                block.IsMaterial = true;
            }
            return block;
        }
        public Block IsStatic(Block block)
        {
            if(block.IsStatic == true)
            {
                block.IsStatic = false;
            }
            else
            {
                block.IsStatic = true;
            }
            return block;
        }
        public Block IsDamage(Block block)
        {
            if(block.IsDamage == true)
            {
                block.IsDamage = false;
            }
            else
            {
                block.IsDamage = true;
            }
            return block;
        }
        public Block Damage(Block block)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            block.Damage = Convert.ToInt32(menu_widow.Data);
            return block;
        }
        public Block TexturePath(Block block)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            string Color = menu_widow.Data;
            string[] VBO_Color_string = Color.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
            float[] VBO_color = new float[VBO_Color_string.Length];
            for (int i = 0; i < VBO_color.Length; i++)
            {
                VBO_color[i] = (float)Convert.ToDouble(VBO_Color_string[i]);
            }
            block.Color = VBO_color;
            return block;
        }

        //Scene Method

        public Scene GetWidth(Scene scene)
        {
            Window1 menu_widow = new Window1();
            int width = scene.Width;
            menu_widow.Activate();
            menu_widow.ShowDialog();
            scene.Width = Convert.ToInt32(menu_widow.Data);
            Block[,] _block = scene.blocks;
            int[,] _IntBlocks = scene.IntBlock;
            scene.blocks = new Block[scene.Width + 1,scene.Height + 1];
            scene.IntBlock = new int[scene.Width + 1,scene.Height + 1];
            for (int i = 0; i < scene.Width+1; i++)
            {
                for (int j = 0; j < scene.Height+1; j++)
                {
                    if(i < width)
                    {
                        scene.blocks[i, j] = _block[i, j];
                        scene.IntBlock[i, j] = _IntBlocks[i, j];
                    }
                    else
                    {
                        scene.blocks[i, j] = new Block();
                        scene.IntBlock[i, j] = 0;
                    }
                }
            }
            return scene;
        }
        public Scene GetHeight(Scene scene)
        {
            Window1 menu_widow = new Window1();
            int height = scene.Height;
            menu_widow.Activate();
            menu_widow.ShowDialog();
            scene.Height = Convert.ToInt32(menu_widow.Data);
            Block[,] _blocks = scene.blocks;
            int[,] _IntBlocks = scene.IntBlock;
            scene.blocks = new Block[scene.Width + 1, scene.Height + 1];
            scene.IntBlock = new int[scene.Width + 1, scene.Height + 1];
            for (int i = 0; i < scene.Width + 1; i++)
            {
                for (int j = 0; j < scene.Height + 1; j++)
                {
                    if(j < height)
                    {
                        scene.blocks[i, j] = _blocks[i, j];
                        scene.IntBlock[i, j] = _IntBlocks[i, j];
                    }
                    else
                    {
                        scene.blocks[i, j] = new Block();
                        scene.IntBlock[i, j] = 0;
                    }
                }
            }
            return scene;
        }
        public void Choose_Scene(ref float[] Pole_Vertex, ref float[] Pole_Color, ref Block[,] blocks, ref int[,] IntBlocks, ref int Pole_Width, ref int Pole_Height, ref Vector2 Selected_Block, Scene scen, ref float[] Block_Vertex, ref float[] Block_Color)
        {
            Comand comand = new Comand();
            Pole_Vertex = comand.DrawPole(scen.Width, scen.Height);
            Pole_Color = comand.FillPole(scen.Width, scen.Height);
            blocks = scen.blocks;
            IntBlocks = scen.IntBlock;
            Pole_Width = scen.Width + 1;
            Pole_Height = scen.Height +1;
            Selected_Block.X = -1;
            Selected_Block.Y = -1;
            Block_Vertex = new float[0];
            Block_Color = new float[0];
        }
        public Scene Save_Scene(Block[,] bloks, int[,] IntBlocks, Scene scene)
        {
            scene.blocks = bloks;
            scene.IntBlock = IntBlocks;
            return scene;
        }
        public Scene TexturePath(Scene scene)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            string TexturePath = menu_widow.Data;
            scene.TexturePath = TexturePath;
            return scene;
        }
        public Scene CheckPointX(Scene scene)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            scene.CheckPointX = Convert.ToInt32(menu_widow.Data);
            return scene;
        }
        public Scene CheckPointY(Scene scene)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            scene.CheckPointY = Convert.ToInt32(menu_widow.Data);
            return scene;
        }
        public Scene ClearScene(Scene scene)
        {
            Scene scene_window = new Scene();
            scene_window.Width = scene.Width;
            scene_window.Height = scene.Height;
            scene_window.blocks = new Block[scene_window.Width+1, scene_window.Height+1];
            for (int i = 0; i < scene.Width+1; i++)
            {
                for (int j = 0; j < scene.Height+1; j++)
                {
                    scene_window.blocks[i, j] = new Block();
                }
            }
            scene_window.IntBlock = new int[scene_window.Width+1, scene_window.Height+1];
            return scene_window;
        }

        //Charecter Method

        public Charecter GetCharecterWidth(Charecter person)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            person.Width = Convert.ToInt32(menu_widow.Data);
            return person;
        }
        public Charecter GetCharecterHeight(Charecter person)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            person.Height = Convert.ToInt32(menu_widow.Data);
            return person;
        }
        public Charecter GetCharecterHitPoints(Charecter person)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            person.HitPoints = Convert.ToInt32(menu_widow.Data);
            return person;
        }
        public Charecter GetCharecterAnimationPath(Charecter person)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            person.AnimationPath = menu_widow.Data;
            return person;
        }
        public Charecter GetCharecterAnimationWidth(Charecter person)
        {
            Window1 menu_widow = new Window1();
            menu_widow.Activate();
            menu_widow.ShowDialog();
            person.AnimationWidth = Convert.ToInt32(menu_widow.Data);
            return person;
        }


        //Tool Draw
        //
        //
        //
        public void Generate_Tool(/*Charecter person*/)
        {
            Tool_VAO[0] = Create_Block_Select_VAO();
            Tool_VAO[1] = Create_Scene_Select_VAO();
            Tool_VAO[2] = Create_Charecter_Select_VAO();
            Tool_VAO[3] = Create_Menu_Select_VAO();
            Tool_VAO[4] = Create_IsMaterial_TextBox_VAO();
            Tool_VAO[5] = Create_IsStatic_TextBox_VAO();
            Tool_VAO[6] = Create_IsDamage_TextBox_VAO();
            Tool_VAO[7] = Create_Texture_TextBox_VAO();
            Tool_VAO[8] = Create_BlockCoord_TextBox_VAO();
            Tool_VAO[9] = Create_MakePater_Select_VAO();
            Tool_VAO[10] = Create_Scene1_Select_VAO();
            Tool_VAO[11] = Create_Scene2_Select_VAO();
            Tool_VAO[12] = Create_Scene3_Select_VAO();
            Tool_VAO[13] = Create_Scene4_Select_VAO();
            Tool_VAO[14] = Create_Scene5_Select_VAO();
            Tool_VAO[15] = Create_Scene6_Select_VAO();
            Tool_VAO[16] = Create_Scene7_Select_VAO();
            Tool_VAO[17] = Create_Scene8_Select_VAO();
            Tool_VAO[18] = Create_Scene9_Select_VAO();
            Tool_VAO[19] = Create_Scene10_Select_VAO();
            Tool_VAO[20] = Create_Scene11_Select_VAO();
            Tool_VAO[21] = Create_Scene12_Select_VAO();
            Tool_VAO[22] = Create_CoordX_TextBox_VAO();
            Tool_VAO[23] = Create_CharecterWidth_TextBox_VAO();
            Tool_VAO[24] = Create_HitPoint_TextBox_VAO();
            Tool_VAO[25] = Create_Animation_TextBox_VAO();
            Tool_VAO[26] = Create_CoordY_TextBox_VAO();
            Tool_VAO[27] = Create_CharecterHeight_TextBox_VAO();
            Tool_VAO[28] = Create_Save_Select_VAO();
            Tool_VAO[29] = Create_Load_Select_VAO();
            Tool_VAO[30] = Create_Exit_Select_VAO();
            Tool_VAO[31] = Create_SceneNomerv_TextBox_VAO();
            Tool_VAO[32] = Create_SceneWidth_TextBox_VAO();
            Tool_VAO[33] = Create_HeightScene_TextBox_VAO();
            Tool_VAO[34] = Create_BackGroundTexture_TextBox_VAO();
            Tool_VAO[35] = Create_SelectScene_Select_VAO();
            Tool_VAO[36] = Create_ClearScene_Select_VAO();
            Tool_VAO[37] = Create_IsMaterial_CheckBox_NoCheck_VAO();
            Tool_VAO[38] = Create_IsMaterial_CheckBox_Checked_VAO();
            Tool_VAO[39] = Create_IsStatic_CheckBox_NoCheck_VAO();
            Tool_VAO[40] = Create_IsStatic_CheckBox_Checked_VAO();
            Tool_VAO[41] = Create_IsDamage_CheckBox_NoCheck_VAO();
            Tool_VAO[42] = Create_IsDamage_CheckBox_Checked_VAO();
            Tool_VAO[43] = Create_Damage_Text_VAO();
            Tool_VAO[44] = Create_Texture_Text_VAO();
            Tool_VAO[45] = Create_BlockCoords_Text_VAO();
            Tool_VAO[46] = Create_AnimationWidth_TextBox_VAO();
            Tool_VAO[47] = Create_CharecterWidth_Text_VAO();
            Tool_VAO[48] = Create_CharecterHeight_Text_VAO();
            Tool_VAO[49] = Create_HitPoint_Text_VAO();
            Tool_VAO[50] = Create_AnimationWidth_Text_VAO();
            Tool_VAO[51] = Create_Animation_Text_VAO();
            Tool_VAO[52] = Create_AnimationShow_Text_VAO();
            Tool_VAO[53] = Create_SceneNumber_Text_VAO();
            Tool_VAO[54] = Create_SceneWidth_Text_VAO();
            Tool_VAO[55] = Create_SceneHeight_Text_VAO();
            Tool_VAO[56] = Create_SceneBackGround_Text_VAO();
            Tool_VAO[57] = Create_CheckPointX_Text_VAO();
            Tool_VAO[58] = Create_CheckPointY_Text_VAO();
        }
        //
        //
        //
        //
        public void Draw_Tool_Block(Block block, Vector2 selected_Block)
        {
            Draw_Block_Select();
            Draw_Scene_Select();
            Draw_Charecter_Select();
            Draw_Menu_Select();
            Draw_IsMaterial_TextBox();
            Draw_IsStatic_TextBox();
            Draw_IsDamage_TextBox();
            Draw_Texture_TextBox();
            Draw_BlockCoord_TextBox();
            Draw_MakePater_TextBox();
            Create_BlockExamp_Show_VBO(block.Color);
            Draw_BlockExamp_TextBox();
            if(block.IsMaterial)
            {
                Draw_IsMaterial_CheckBox_Checked_TextBox();
            }
            else
            {
                Draw_IsMaterial_CheckBox_NoCheck_TextBox();
            }
            if (block.IsStatic)
            {
                Draw_IsStatic_CheckBox_Checked_TextBox();
            }
            else
            {
                Draw_IsStatic_CheckBox_NoCheck_TextBox();
            }
            if (block.IsDamage)
            {
                Draw_IsDamage_CheckBox_Checked_TextBox();
            }
            else
            {
                Draw_IsDamage_CheckBox_NoCheck_TextBox();
            }
            texture.Create_Text(Convert.ToString(block.Damage), "Data/Texts/Tool_Texture/43.png");
            texture.UpDate_Texture(Tool_Textures_ID[43], "Data/Texts/Tool_Texture/43.png");
            Draw_Damage_Text();
            texture.Create_Text($"{block.Color[0]};{block.Color[1]};{block.Color[2]};{block.Color[3]}", "Data/Texts/Tool_Texture/44.png");
            texture.UpDate_Texture(Tool_Textures_ID[44], "Data/Texts/Tool_Texture/44.png");
            Draw_Texture_Text();

            texture.Create_Text($"{selected_Block.X};{selected_Block.Y};", "Data/Texts/Tool_Texture/45.png");
            texture.UpDate_Texture(Tool_Textures_ID[45], "Data/Texts/Tool_Texture/45.png");
            Draw_BlockCoords_Text();
        }
        public void Draw_Tool_Scene()
        {
            Draw_Block_Select();
            Draw_Scene_Select();
            Draw_Charecter_Select();
            Draw_Menu_Select();
            Draw_Scene1_Select();
            Draw_Scene2_Select();
            Draw_Scene3_Select();
            Draw_Scene4_Select();
            Draw_Scene5_Select();
            Draw_Scene6_Select();
            Draw_Scene7_Select();
            Draw_Scene8_Select();
            Draw_Scene9_Select();
            Draw_Scene10_Select();
            Draw_Scene11_Select();
            Draw_Scene12_Select();

        }
        public void Draw_Tool_Charecter(Charecter charecter)
        {
            Draw_Block_Select();
            Draw_Scene_Select();
            Draw_Charecter_Select();
            Draw_Menu_Select();
            Draw_CharecterWidth_TextBox();
            Draw_HitPoint_TextBox();
            Draw_Animation_TextBox();
            Draw_CharecterHeight_TextBox();
            Draw_AnimationWidth_TextBox();
            texture.Create_Text($"{charecter.Width}", "Data/Texts/Tool_Texture/47.png");
            texture.UpDate_Texture(Tool_Textures_ID[47], "Data/Texts/Tool_Texture/47.png");
            Draw_CharecterWidth_Text();
            texture.Create_Text($"{charecter.Height}", "Data/Texts/Tool_Texture/48.png");
            texture.UpDate_Texture(Tool_Textures_ID[48], "Data/Texts/Tool_Texture/48.png");
            Draw_CharecterHeight_Text();
            texture.Create_Text($"{charecter.HitPoints}", "Data/Texts/Tool_Texture/49.png");
            texture.UpDate_Texture(Tool_Textures_ID[49], "Data/Texts/Tool_Texture/49.png");
            Draw_HitPoint_Text();
            texture.Create_Text($"{charecter.AnimationWidth}", "Data/Texts/Tool_Texture/50.png");
            texture.UpDate_Texture(Tool_Textures_ID[50], "Data/Texts/Tool_Texture/50.png");
            Draw_AnimationWidth_Text();
            texture.Create_Text($"{charecter.AnimationPath}", "Data/Texts/Tool_Texture/51.png");
            texture.UpDate_Texture(Tool_Textures_ID[51], "Data/Texts/Tool_Texture/51.png");
            Draw_Animation_Text();
            texture.UpDate_Texture(Tool_Textures_ID[52], "Data/Texts/Tool_Texture/52.png");
            Draw_AnimationShow_Text();
        }
        public void Draw_Tool_Menu()
        {
            Draw_Block_Select();
            Draw_Scene_Select();
            Draw_Charecter_Select();
            Draw_Menu_Select();
           // Draw_Load_Select();
           // Draw_Save_Select();
            Draw_Exit_Select();
        }
        public void Draw_Tool_SceneRedactor(Scene scene, int scenenumber)
        {
            Draw_Block_Select();
            Draw_Scene_Select();
            Draw_Charecter_Select();
            Draw_Menu_Select();
            Draw_SceneNomer_TextBox();
            Draw_SceneWidth_TextBox();
            Draw_HeightScene_TextBox();
            Draw_BackGroundTexture_TextBox();
            Draw_SelectScene_Select();
            Draw_ClearScene_Select();
            Draw_CoordX_TextBox();
            Draw_CoordY_TextBox();
            texture.Create_Text($"{scenenumber}", "Data/Texts/Tool_Texture/53.png");
            texture.UpDate_Texture(Tool_Textures_ID[53], "Data/Texts/Tool_Texture/53.png");
            Draw_SceneNumber_Text();

            texture.Create_Text($"{scene.Width}", "Data/Texts/Tool_Texture/54.png");
            texture.UpDate_Texture(Tool_Textures_ID[54], "Data/Texts/Tool_Texture/54.png");
            Draw_SceneWidth_Text();

            texture.Create_Text($"{scene.Height}", "Data/Texts/Tool_Texture/55.png");
            texture.UpDate_Texture(Tool_Textures_ID[55], "Data/Texts/Tool_Texture/55.png");
            Draw_SceneHeight_Text();

            texture.Create_Text($"{scene.TexturePath}", "Data/Texts/Tool_Texture/56.png");
            texture.UpDate_Texture(Tool_Textures_ID[56], "Data/Texts/Tool_Texture/56.png");
            Draw_SceneBackGround_Text();

            texture.Create_Text($"{scene.CheckPointX}", "Data/Texts/Tool_Texture/57.png");
            texture.UpDate_Texture(Tool_Textures_ID[57], "Data/Texts/Tool_Texture/57.png");
            Draw_CheckPointX_Text();

            texture.Create_Text($"{scene.CheckPointY}", "Data/Texts/Tool_Texture/58.png");
            texture.UpDate_Texture(Tool_Textures_ID[58], "Data/Texts/Tool_Texture/58.png");
            Draw_CheckPointY_Text();
        }


        //Block_Tool
        private float[] Block_Select_vert =
        {
             -1f, 1f, 0f, 0f, 0f, 
            -1f, 0.8f, 0f, 0f, 1f,
            -0.5f, 0.8f, 0f, 1f, 1f,
            -0.5f, 1f, 0f, 1f, 0f
        };
        private int[] Block_select_index =
        {
           0, 1, 2, 3
        };

        private int Create_Block_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Block_Select_vert.Length * sizeof(float), Block_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Block_select_index.Length * sizeof(uint), Block_select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[0]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Block_Select()
        {
            GL.BindVertexArray(Tool_VAO[0]);
            texture.BindTexture(Tool_Textures_ID[0]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Block_select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene_Select_vert =
        {
            -0.5f, 1f, 0f, 0f, 0f,
            -0.5f, 0.8f, 0f, 0f, 1f,
            0f, 0.8f, 0f, 1f, 1f,
            0f, 1f, 0f, 1f, 0f
        };
        private int[] Scene_select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene_Select_vert.Length * sizeof(float), Scene_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene_select_index.Length * sizeof(uint), Scene_select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[1]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene_Select()
        {
            GL.BindVertexArray(Tool_VAO[1]);
            texture.BindTexture(Tool_Textures_ID[1]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene_select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Charecter_Select_vert =
       {
              0f, 1f, 0f, 0f, 0f,
             0f, 0.8f, 0f, 0f, 1f,
             0.5f, 0.8f, 0f, 1f, 1f,
             0.5f, 1f, 0f, 1f, 0f
         };
        private int[] Charecter_select_index =
        {
             0, 1, 2, 3
         };

        private int Create_Charecter_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Charecter_Select_vert.Length * sizeof(float), Charecter_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Charecter_select_index.Length * sizeof(uint), Charecter_select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[2]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Charecter_Select()
        {
            GL.BindVertexArray(Tool_VAO[2]);
            texture.BindTexture(Tool_Textures_ID[2]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Charecter_select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Menu_Select_vert =
        {
             0.5f, 1f, 0f, 0f, 0f,
            0.5f, 0.8f, 0f, 0f, 1f,
            1f, 0.8f, 0f, 1f, 1f,
            1f, 1f, 0f, 1f, 0f
        };
        private int[] Menu_select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Menu_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Menu_Select_vert.Length * sizeof(float), Menu_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Menu_select_index.Length * sizeof(uint), Menu_select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[3]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Menu_Select()
        {
            GL.BindVertexArray(Tool_VAO[3]);
            texture.BindTexture(Tool_Textures_ID[3]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Menu_select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }
        
        private float[] IsMaterial_TextBox_vert =
        {
            -0.9f, 0.6f, 0f, 0f, 0f,
            -0.9f, 0.4f, 0f, 0f, 1f,
            -0.6f, 0.4f, 0f, 1f, 1f,
            -0.6f, 0.6f, 0f, 1f, 0f
        };
        private int[] IsMaterial_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsMaterial_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsMaterial_TextBox_vert.Length * sizeof(float), IsMaterial_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsMaterial_TextBox_index.Length * sizeof(uint), IsMaterial_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[4]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsMaterial_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[4]);
            texture.BindTexture(Tool_Textures_ID[4]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsMaterial_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsStatic_TextBox_vert =
        {
            -0.9f, 0.2f, 0f, 0f, 0f,
            -0.9f, 0f, 0f, 0f, 1f,
            -0.6f, 0f, 0f, 1f, 1f,
            -0.6f, 0.2f, 0f, 1f, 0f
        };
        private int[] IsStatic_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsStatic_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsStatic_TextBox_vert.Length * sizeof(float), IsStatic_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsStatic_TextBox_index.Length * sizeof(uint), IsStatic_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[5]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsStatic_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[5]);
            texture.BindTexture(Tool_Textures_ID[5]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsStatic_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }
        private float[] _Block_Tool_VBO =
        {
           //CheckBox
            -0.5f, 0.6f, 0f, 
            -0.5f, 0.4f, 0f, 
            -0.4f, 0.4f, 0f, 
            -0.4f, 0.6f, 0f, 

            //CheckBox
            -0.5f, 0.2f, 0f, 
            -0.5f, 0f, 0f, 
            -0.4f, 0f, 0f, 
            -0.4f, 0.2f, 0f,

            //CheckBox
            -0.5f, -0.2f, 0f, 
            -0.5f, -0.4f, 0f, 
            -0.4f, -0.4f, 0f, 
            -0.4f, -0.2f, 0f, 
            //TextBox
            -0.3f, -0.2f, 0f, 
            -0.3f, -0.4f, 0f, 
            0f, -0.4f, 0f, 
            0f, -0.2f, 0f, 

            //TextBox 
            -0.5f, -0.6f, 0f, 
            -0.5f, -0.8f, 0f, 
            0f, -0.8f, 0f,
            0f, -0.6f, 0f, 

            //TextBox
            0.2f, 0.6f, 0f, 
            0.2f, 0.4f, 0f, 
            0.5f, 0.4f, 0f, 
            0.5f, 0.6f, 0f, 
            //BlockBox
            0.2f, 0f, 0f,
            0.2f, -0.8f, 0f,
            0.7f, -0.8f, 0f, 
            0.7f, 0f, 0f,

        };

        private float[] IsDamage_TextBox_vert =
        {
            -0.9f, -0.2f, 0f, 0f, 0f,
            -0.9f, -0.4f, 0f, 0f, 1f,
            -0.6f, -0.4f, 0f, 1f, 1f,
            -0.6f, -0.2f, 0f, 1f, 0f
        };
        private int[] IsDamage_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsDamage_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsDamage_TextBox_vert.Length * sizeof(float), IsDamage_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsDamage_TextBox_index.Length * sizeof(uint), IsDamage_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[6]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsDamage_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[6]);
            texture.BindTexture(Tool_Textures_ID[6]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsDamage_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Texture_TextBox_vert =
       {
            -0.9f, -0.6f, 0f, 0f, 0f,
            -0.9f, -0.8f, 0f, 0f, 1f,
            -0.6f, -0.8f, 0f, 1f, 1f,
            -0.6f, -0.6f, 0f, 1f, 0f
        };
        private int[] Texture_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_Texture_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Texture_TextBox_vert.Length * sizeof(float), Texture_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Texture_TextBox_index.Length * sizeof(uint), Texture_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[7]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Texture_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[7]);
            texture.BindTexture(Tool_Textures_ID[7]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Texture_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] BlockCoord_TextBox_vert =
        {
            -0.1f, 0.6f, 0f, 0f, 0f,
            -0.1f, 0.4f, 0f, 0f, 1f,
            0.2f, 0.4f, 0f, 1f, 1f,
            0.2f, 0.6f, 0f, 1f, 0f
        };
        private int[] BlockCoord_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_BlockCoord_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, BlockCoord_TextBox_vert.Length * sizeof(float), BlockCoord_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, BlockCoord_TextBox_index.Length * sizeof(uint), BlockCoord_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[8]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_BlockCoord_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[8]);
            texture.BindTexture(Tool_Textures_ID[8]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, BlockCoord_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] MakePater_Select_vert =
        {
            0.6f, 0.4f, 0f, 0f, 0f,
            0.6f, 0.2f, 0f, 0f, 1f,
            0.9f, 0.2f, 0f, 1f, 1f,
            0.9f, 0.4f, 0f, 1f, 0f
        };
        private int[] MakePater_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_MakePater_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, MakePater_Select_vert.Length * sizeof(float), MakePater_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, MakePater_Select_index.Length * sizeof(uint), MakePater_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[9]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_MakePater_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[9]);
            texture.BindTexture(Tool_Textures_ID[9]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, MakePater_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }


        private float[] BlockExamp_Select_vert =
        {
            0.2f, 0f, 0f,
            0.2f, -0.8f, 0f, 
            0.7f, -0.8f, 0f,
            0.7f, 0f, 0f
        };
        private int BlockExamp_VBO_vertex = 0;
        private int BlockExamp_VBO_color = 0;

        private void Create_BlockExamp_Show_VBO(float[] Color)
        {
            for (int i = 4; i < 16; i++)
            {
                Array.Resize(ref Color, Color.Length + 1);
                Color[Color.Length - 1] = Color[Color.Length-5]; 
            }

            BlockExamp_VBO_vertex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BlockExamp_VBO_vertex);
            GL.BufferData(BufferTarget.ArrayBuffer, BlockExamp_Select_vert.Length * sizeof(float), BlockExamp_Select_vert, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            BlockExamp_VBO_color = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, BlockExamp_VBO_color);
            GL.BufferData(BufferTarget.ArrayBuffer, Color.Length * sizeof(float), Color, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        private void Draw_BlockExamp_TextBox()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);



            GL.BindBuffer(BufferTarget.ArrayBuffer, BlockExamp_VBO_vertex);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);


            GL.BindBuffer(BufferTarget.ArrayBuffer, BlockExamp_VBO_color);
            GL.ColorPointer(4, ColorPointerType.Float, 0, 0);


            GL.DrawArrays(PrimitiveType.Quads, 0, BlockExamp_Select_vert.Length);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
        }

        //Scene_Select_Tool
        private float[] Scene1_Select_vert =
       {
            -0.85f, 0.6f, 0f, 0f, 0f,
            -0.85f, 0.2f, 0f, 0f, 1f,
            -0.65f, 0.2f, 0f, 1f, 1f,
            -0.65f, 0.6f, 0f, 1f, 0f
        };
        private int[] Scene1_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene1_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene1_Select_vert.Length * sizeof(float), Scene1_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene1_Select_index.Length * sizeof(uint), Scene1_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[10]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene1_Select()
        {
            GL.BindVertexArray(Tool_VAO[10]);
            texture.BindTexture(Tool_Textures_ID[10]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene1_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene2_Select_vert =
       {
            -0.55f, 0.6f, 0f, 0f, 0f,
            -0.55f, 0.2f, 0f, 0f, 1f,
            -0.35f, 0.2f, 0f, 1f, 1f,
            -0.35f, 0.6f, 0f, 1f, 0f
        };
        private int[] Scene2_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene2_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene2_Select_vert.Length * sizeof(float), Scene2_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene2_Select_index.Length * sizeof(uint), Scene2_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[11]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene2_Select()
        {
            GL.BindVertexArray(Tool_VAO[11]);
            texture.BindTexture(Tool_Textures_ID[11]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene2_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene3_Select_vert =
       {
            -0.25f, 0.6f, 0f, 0f, 0f,
            -0.25f, 0.2f, 0f, 0f, 1f,
            -0.05f, 0.2f, 0f, 1f, 1f,
            -0.05f, 0.6f, 0f, 1f, 0f
        };
        private int[] Scene3_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene3_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene3_Select_vert.Length * sizeof(float), Scene3_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene3_Select_index.Length * sizeof(uint), Scene3_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[12]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene3_Select()
        {
            GL.BindVertexArray(Tool_VAO[12]);
            texture.BindTexture(Tool_Textures_ID[12]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene3_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene4_Select_vert =
{
            0.05f, 0.6f, 0f, 0f, 0f,
            0.05f, 0.2f, 0f, 0f, 1f,
            0.25f, 0.2f, 0f, 1f, 1f,
            0.25f, 0.6f, 0f, 1f, 0f
        };
        private int[] Scene4_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene4_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene4_Select_vert.Length * sizeof(float), Scene4_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene4_Select_index.Length * sizeof(uint), Scene4_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[13]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene4_Select()
        {
            GL.BindVertexArray(Tool_VAO[13]);
            texture.BindTexture(Tool_Textures_ID[13]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene4_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene5_Select_vert =
{
            0.35f, 0.6f, 0f, 0f, 0f,
            0.35f, 0.2f, 0f, 0f, 1f,
            0.55f, 0.2f, 0f, 1f, 1f,
            0.55f, 0.6f, 0f, 1f, 0f
        };
        private int[] Scene5_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene5_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene5_Select_vert.Length * sizeof(float), Scene5_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene5_Select_index.Length * sizeof(uint), Scene5_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[14]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene5_Select()
        {
            GL.BindVertexArray(Tool_VAO[14]);
            texture.BindTexture(Tool_Textures_ID[14]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene5_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene6_Select_vert =
{
            0.65f, 0.6f, 0f, 0f, 0f,
            0.65f, 0.2f, 0f, 0f, 1f,
            0.85f, 0.2f, 0f, 1f, 1f,
            0.85f, 0.6f, 0f, 1f, 0f
        };
        private int[] Scene6_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene6_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene6_Select_vert.Length * sizeof(float), Scene6_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene6_Select_index.Length * sizeof(uint), Scene6_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[15]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene6_Select()
        {
            GL.BindVertexArray(Tool_VAO[15]);
            texture.BindTexture(Tool_Textures_ID[15]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene6_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene7_Select_vert =
{
            -0.85f, -0.4f, 0f, 0f, 0f,
            -0.85f, -0.8f, 0f, 0f, 1f,
            -0.65f, -0.8f, 0f, 1f, 1f,
            -0.65f, -0.4f, 0f, 1f, 0f
        };
        private int[] Scene7_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene7_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene7_Select_vert.Length * sizeof(float), Scene7_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene7_Select_index.Length * sizeof(uint), Scene7_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[16]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene7_Select()
        {
            GL.BindVertexArray(Tool_VAO[16]);
            texture.BindTexture(Tool_Textures_ID[16]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene7_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene8_Select_vert =
{
            -0.55f, -0.4f, 0f, 0f, 0f,
            -0.55f, -0.8f, 0f, 0f, 1f,
            -0.35f, -0.8f, 0f, 1f, 1f,
            -0.35f, -0.4f, 0f, 1f, 0f
        };
        private int[] Scene8_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene8_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene8_Select_vert.Length * sizeof(float), Scene8_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene8_Select_index.Length * sizeof(uint), Scene8_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[17]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene8_Select()
        {
            GL.BindVertexArray(Tool_VAO[17]);
            texture.BindTexture(Tool_Textures_ID[17]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene8_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene9_Select_vert =
{
            -0.25f, -0.4f, 0f, 0f, 0f,
            -0.25f, -0.8f, 0f, 0f, 1f,
            -0.05f, -0.8f, 0f, 1f, 1f,
            -0.05f, -0.4f, 0f, 1f, 0f
        };
        private int[] Scene9_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene9_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene9_Select_vert.Length * sizeof(float), Scene9_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene9_Select_index.Length * sizeof(uint), Scene9_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[18]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene9_Select()
        {
            GL.BindVertexArray(Tool_VAO[18]);
            texture.BindTexture(Tool_Textures_ID[18]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene9_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene10_Select_vert =
{
            0.05f, -0.4f, 0f, 0f, 0f,
            0.05f, -0.8f, 0f, 0f, 1f,
            0.25f, -0.8f, 0f, 1f, 1f,
            0.25f, -0.4f, 0f, 1f, 0f
        };
        private int[] Scene10_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene10_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene10_Select_vert.Length * sizeof(float), Scene10_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene10_Select_index.Length * sizeof(uint), Scene10_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[19]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene10_Select()
        {
            GL.BindVertexArray(Tool_VAO[19]);
            texture.BindTexture(Tool_Textures_ID[19]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene10_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene11_Select_vert =
{
            0.35f, -0.4f, 0f, 0f, 0f,
            0.35f, -0.8f, 0f, 0f, 1f,
            0.55f, -0.8f, 0f, 1f, 1f,
            0.55f, -0.4f, 0f, 1f, 0f
        };
        private int[] Scene11_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene11_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene11_Select_vert.Length * sizeof(float), Scene11_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene11_Select_index.Length * sizeof(uint), Scene11_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[20]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene11_Select()
        {
            GL.BindVertexArray(Tool_VAO[20]);
            texture.BindTexture(Tool_Textures_ID[20]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene11_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Scene12_Select_vert =
{
            0.65f, -0.4f, 0f, 0f, 0f,
            0.65f, -0.8f, 0f, 0f, 1f,
            0.85f, -0.8f, 0f, 1f, 1f,
            0.85f, -0.4f, 0f, 1f, 0f
        };
        private int[] Scene12_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Scene12_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Scene12_Select_vert.Length * sizeof(float), Scene12_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Scene12_Select_index.Length * sizeof(uint), Scene12_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[21]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Scene12_Select()
        {
            GL.BindVertexArray(Tool_VAO[21]);
            texture.BindTexture(Tool_Textures_ID[21]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Scene12_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        //Charecter

        private float[] CharecterWidth_TextBox_vert =
        {
            -0.9f, 0.6f, 0f, 0f, 0f,
            -0.9f, 0.4f, 0f, 0f, 1f,
            -0.6f, 0.4f, 0f, 1f, 1f,
            -0.6f, 0.6f, 0f, 1f, 0f
        };
        private int[] CharecterWidth_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_CharecterWidth_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CharecterWidth_TextBox_vert.Length * sizeof(float), CharecterWidth_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CharecterWidth_TextBox_index.Length * sizeof(uint), CharecterWidth_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[23]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CharecterWidth_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[23]);
            texture.BindTexture(Tool_Textures_ID[23]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CharecterWidth_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] HitPoint_TextBox_vert =
        {
            -0.9f, 0.2f, 0f, 0f, 0f,
            -0.9f, 0f, 0f, 0f, 1f,
            -0.6f, 0f, 0f, 1f, 1f,
            -0.6f, 0.2f, 0f, 1f, 0f
        };
        private int[] HitPoint_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_HitPoint_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, HitPoint_TextBox_vert.Length * sizeof(float), HitPoint_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, HitPoint_TextBox_index.Length * sizeof(uint), HitPoint_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[24]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_HitPoint_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[24]);
            texture.BindTexture(Tool_Textures_ID[24]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, HitPoint_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Animation_TextBox_vert =
       {
            -0.9f, -0.6f, 0f, 0f, 0f,
            -0.9f, -0.8f, 0f, 0f, 1f,
            -0.6f, -0.8f, 0f, 1f, 1f,
            -0.6f, -0.6f, 0f, 1f, 0f
        };
        private int[] Animation_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_Animation_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Animation_TextBox_vert.Length * sizeof(float), Animation_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Animation_TextBox_index.Length * sizeof(uint), Animation_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[25]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Animation_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[25]);
            texture.BindTexture(Tool_Textures_ID[25]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Animation_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }


        private float[] CharecterHeight_TextBox_vert =
        {
            -0.1f, 0.6f, 0f, 0f, 0f,
            -0.1f, 0.4f, 0f, 0f, 1f,
            0.2f, 0.4f, 0f, 1f, 1f,
            0.2f, 0.6f, 0f, 1f, 0f
        };
        private int[] CharecterHeight_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_CharecterHeight_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CharecterHeight_TextBox_vert.Length * sizeof(float), CharecterHeight_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CharecterHeight_TextBox_index.Length * sizeof(uint), CharecterHeight_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[27]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CharecterHeight_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[27]);
            texture.BindTexture(Tool_Textures_ID[27]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CharecterHeight_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] AnimationWidth_TextBox_vert =
        {
            -0.9f, -0.2f, 0f, 0f, 0f,
            -0.9f, -0.4f, 0f, 0f, 1f,
            -0.6f, -0.4f, 0f, 1f, 1f,
            -0.6f, -0.2f, 0f, 1f, 0f
        };
        private int[] AnimationWidth_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_AnimationWidth_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, AnimationWidth_TextBox_vert.Length * sizeof(float), AnimationWidth_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, AnimationWidth_TextBox_index.Length * sizeof(uint), AnimationWidth_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[46]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_AnimationWidth_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[46]);
            texture.BindTexture(Tool_Textures_ID[46]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, AnimationWidth_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        //Menu_Tool

        private float[] Save_Select_vert =
        {
            -0.9f, 0.6f, 0f, 0f, 0f,
            -0.9f, 0.4f, 0f, 0f, 1f,
            0.9f, 0.4f, 0f, 1f, 1f,
            0.9f, 0.6f, 0f, 1f, 0f
        };
        private int[] Save_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Save_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Save_Select_vert.Length * sizeof(float), Save_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Save_Select_index.Length * sizeof(uint), Save_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[28]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Save_Select()
        {
            GL.BindVertexArray(Tool_VAO[28]);
            texture.BindTexture(Tool_Textures_ID[28]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Save_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Load_Select_vert =
        {
            -0.9f, 0.2f, 0f, 0f, 0f,
            -0.9f, 0f, 0f, 0f, 1f,
            0.9f, 0f, 0f, 1f, 1f,
            0.9f, 0.2f, 0f, 1f, 0f
        };
        private int[] Load_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Load_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Load_Select_vert.Length * sizeof(float), Load_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Load_Select_index.Length * sizeof(uint), Load_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[29]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Load_Select()
        {
            GL.BindVertexArray(Tool_VAO[29]);
            texture.BindTexture(Tool_Textures_ID[29]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Load_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] Exit_Select_vert =
        {
            -0.9f, -0.2f, 0f, 0f, 0f,
            -0.9f, -0.4f, 0f, 0f, 1f,
            0.9f, -0.4f, 0f, 1f, 1f,
            0.9f, -0.2f, 0f, 1f, 0f
        };
        private int[] Exit_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_Exit_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Exit_Select_vert.Length * sizeof(float), Exit_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Exit_Select_index.Length * sizeof(uint), Exit_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[30]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Exit_Select()
        {
            GL.BindVertexArray(Tool_VAO[30]);
            texture.BindTexture(Tool_Textures_ID[30]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Exit_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        //Scene Redactor

        private float[] SceneNomer_TextBox_vert =
        {
            -0.9f, 0.6f, 0f, 0f, 0f,
            -0.9f, 0.4f, 0f, 0f, 1f,
            -0.5f, 0.4f, 0f, 1f, 1f,
            -0.5f, 0.6f, 0f, 1f, 0f
        };
        private int[] SceneNomer_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_SceneNomerv_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, SceneNomer_TextBox_vert.Length * sizeof(float), SceneNomer_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, SceneNomer_TextBox_index.Length * sizeof(uint), SceneNomer_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[31]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SceneNomer_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[31]);
            texture.BindTexture(Tool_Textures_ID[31]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, SceneNomer_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] SceneWidth_TextBox_vert =
        {
            -0.9f, 0.2f, 0f, 0f, 0f,
            -0.9f, 0f, 0f, 0f, 1f,
            -0.5f, 0f, 0f, 1f, 1f,
            -0.5f, 0.2f, 0f, 1f, 0f
        };
        private int[] SceneWidth_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_SceneWidth_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, SceneWidth_TextBox_vert.Length * sizeof(float), SceneWidth_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, SceneWidth_TextBox_index.Length * sizeof(uint), SceneWidth_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[32]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SceneWidth_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[32]);
            texture.BindTexture(Tool_Textures_ID[32]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, SceneWidth_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] HeightScene_TextBox_vert =
        {
            -0.9f, -0.2f, 0f, 0f, 0f,
            -0.9f, -0.4f, 0f, 0f, 1f,
            -0.5f, -0.4f, 0f, 1f, 1f,
            -0.5f, -0.2f, 0f, 1f, 0f
        };
        private int[] HeightScene_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_HeightScene_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, HeightScene_TextBox_vert.Length * sizeof(float), HeightScene_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, HeightScene_TextBox_index.Length * sizeof(uint), HeightScene_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[33]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_HeightScene_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[33]);
            texture.BindTexture(Tool_Textures_ID[33]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, HeightScene_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] BackGroundTexture_TextBox_vert =
       {
            -0.9f, -0.6f, 0f, 0f, 0f,
            -0.9f, -0.8f, 0f, 0f, 1f,
            -0.5f, -0.8f, 0f, 1f, 1f,
            -0.5f, -0.6f, 0f, 1f, 0f
        };
        private int[] BackGroundTexture_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_BackGroundTexture_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, BackGroundTexture_TextBox_vert.Length * sizeof(float), BackGroundTexture_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, BackGroundTexture_TextBox_index.Length * sizeof(uint), BackGroundTexture_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[34]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_BackGroundTexture_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[34]);
            texture.BindTexture(Tool_Textures_ID[34]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Texture_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] SelectScene_Select_vert =
       {
            0.5f, -0.2f, 0f, 0f, 0f,
            0.5f, -0.4f, 0f, 0f, 1f,
            0.8f, -0.4f, 0f, 1f, 1f,
            0.8f, -0.2f, 0f, 1f, 0f
        };
        private int[] SelectScene_Select_index =
        {
            0, 1, 2, 3
        };

        private int Create_SelectScene_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, SelectScene_Select_vert.Length * sizeof(float), SelectScene_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, SelectScene_Select_index.Length * sizeof(uint), SelectScene_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[35]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SelectScene_Select()
        {
            GL.BindVertexArray(Tool_VAO[35]);
            texture.BindTexture(Tool_Textures_ID[35]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, SelectScene_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] ClearScene_Select_vert =
       {
            0.5f, -0.6f, 0f, 0f, 0f,
            0.5f, -0.8f, 0f, 0f, 1f,
            0.8f, -0.8f, 0f, 1f, 1f,
            0.8f, -0.6f, 0f, 1f, 0f
        };
        private int[] ClearScene_Select_index =
        {
            0, 1, 2, 3
        };

        private float[] CoordY_TextBox_vert =
{
            -0.1f, 0.2f, 0f, 0f, 0f,
            -0.1f, 0f, 0f, 0f, 1f,
            0.2f, 0f, 0f, 1f, 1f,
            0.2f, 0.2f, 0f, 1f, 0f
        };
        private int[] CoordY_TextBox_index =
        {
            0, 1, 2, 3
        };

        private int Create_CoordY_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CoordY_TextBox_vert.Length * sizeof(float), CoordY_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CoordY_TextBox_index.Length * sizeof(uint), CoordY_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[26]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CoordY_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[26]);
            texture.BindTexture(Tool_Textures_ID[26]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CoordY_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] CoordX_TextBox_vert =
{
            -0.1f, 0.6f, 0f, 0f, 0f,
            -0.1f, 0.4f, 0f, 0f, 1f,
            0.2f, 0.4f, 0f, 1f, 1f,
            0.2f, 0.6f, 0f, 1f, 0f
        };
        private int[] CoordX_TextBox_index =
        {
            0, 1, 2, 3
        };
        private int Create_CoordX_TextBox_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CoordX_TextBox_vert.Length * sizeof(float), CoordX_TextBox_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CoordX_TextBox_index.Length * sizeof(uint), CoordX_TextBox_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[22]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CoordX_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[22]);
            texture.BindTexture(Tool_Textures_ID[22]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CoordX_TextBox_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }


        //Ative objects
        //CheckBox
        private int Create_ClearScene_Select_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, ClearScene_Select_vert.Length * sizeof(float), ClearScene_Select_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, ClearScene_Select_index.Length * sizeof(uint), ClearScene_Select_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[36]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_ClearScene_Select()
        {
            GL.BindVertexArray(Tool_VAO[36]);
            texture.BindTexture(Tool_Textures_ID[36]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, ClearScene_Select_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsMaterial_CheckBox_NoCheck_vert =
{
            -0.5f, 0.6f, 0f, 0f, 0f,
            -0.5f, 0.4f, 0f, 0f, 1f,
            -0.4f, 0.4f, 0f, 1f, 1f,
            -0.4f, 0.6f, 0f, 1f, 0f
        };
        private int[] IsMaterial_CheckBox_NoCheck_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsMaterial_CheckBox_NoCheck_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsMaterial_CheckBox_NoCheck_vert.Length * sizeof(float), IsMaterial_CheckBox_NoCheck_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsMaterial_CheckBox_NoCheck_index.Length * sizeof(uint), IsMaterial_CheckBox_NoCheck_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[37]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsMaterial_CheckBox_NoCheck_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[37]);
            texture.BindTexture(Tool_Textures_ID[37]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsMaterial_CheckBox_NoCheck_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsMaterial_CheckBox_Checked_vert =
{
            -0.5f, 0.6f, 0f, 0f, 0f,
            -0.5f, 0.4f, 0f, 0f, 1f,
            -0.4f, 0.4f, 0f, 1f, 1f,
            -0.4f, 0.6f, 0f, 1f, 0f
        };
        private int[] IsMaterial_CheckBox_Checked_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsMaterial_CheckBox_Checked_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsMaterial_CheckBox_Checked_vert.Length * sizeof(float), IsMaterial_CheckBox_Checked_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsMaterial_CheckBox_Checked_index.Length * sizeof(uint), IsMaterial_CheckBox_Checked_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[38]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsMaterial_CheckBox_Checked_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[38]);
            texture.BindTexture(Tool_Textures_ID[38]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsMaterial_CheckBox_Checked_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsStatic_CheckBox_NoCheck_vert =
{
            -0.5f, 0.2f, 0f, 0f, 0f,
            -0.5f, 0.0f, 0f, 0f, 1f,
            -0.4f, 0.0f, 0f, 1f, 1f,
            -0.4f, 0.2f, 0f, 1f, 0f
        };
        private int[] IsStatic_CheckBox_NoCheck_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsStatic_CheckBox_NoCheck_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsStatic_CheckBox_NoCheck_vert.Length * sizeof(float), IsStatic_CheckBox_NoCheck_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsStatic_CheckBox_NoCheck_index.Length * sizeof(uint), IsStatic_CheckBox_NoCheck_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[39]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsStatic_CheckBox_NoCheck_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[39]);
            texture.BindTexture(Tool_Textures_ID[39]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsStatic_CheckBox_NoCheck_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsStatic_CheckBox_Checked_vert =
{
            -0.5f, 0.2f, 0f, 0f, 0f,
            -0.5f, 0.0f, 0f, 0f, 1f,
            -0.4f, 0.0f, 0f, 1f, 1f,
            -0.4f, 0.2f, 0f, 1f, 0f
        };
        private int[] IsStatic_CheckBox_Checked_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsStatic_CheckBox_Checked_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsStatic_CheckBox_Checked_vert.Length * sizeof(float), IsStatic_CheckBox_Checked_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsStatic_CheckBox_Checked_index.Length * sizeof(uint), IsStatic_CheckBox_Checked_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[40]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsStatic_CheckBox_Checked_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[40]);
            texture.BindTexture(Tool_Textures_ID[40]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsStatic_CheckBox_Checked_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsDamage_CheckBox_NoCheck_vert =
        {
            -0.5f, -0.2f, 0f, 0f, 0f,
            -0.5f, -0.4f, 0f, 0f, 1f,
            -0.4f, -0.4f, 0f, 1f, 1f,
            -0.4f, -0.2f, 0f, 1f, 0f
        };
        private int[] IsDamage_CheckBox_NoCheck_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsDamage_CheckBox_NoCheck_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsDamage_CheckBox_NoCheck_vert.Length * sizeof(float), IsDamage_CheckBox_NoCheck_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsDamage_CheckBox_NoCheck_index.Length * sizeof(uint), IsDamage_CheckBox_NoCheck_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[41]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsDamage_CheckBox_NoCheck_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[41]);
            texture.BindTexture(Tool_Textures_ID[41]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsStatic_CheckBox_NoCheck_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] IsDamage_CheckBox_Checked_vert =
        {
            -0.5f, -0.2f, 0f, 0f, 0f,
            -0.5f, -0.4f, 0f, 0f, 1f,
            -0.4f, -0.4f, 0f, 1f, 1f,
            -0.4f, -0.2f, 0f, 1f, 0f
        };
        private int[] IsDamage_CheckBox_Checked_index =
        {
            0, 1, 2, 3
        };

        private int Create_IsDamage_CheckBox_Checked_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, IsDamage_CheckBox_Checked_vert.Length * sizeof(float), IsDamage_CheckBox_Checked_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, IsDamage_CheckBox_Checked_index.Length * sizeof(uint), IsDamage_CheckBox_Checked_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[42]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_IsDamage_CheckBox_Checked_TextBox()
        {
            GL.BindVertexArray(Tool_VAO[42]);
            texture.BindTexture(Tool_Textures_ID[42]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, IsDamage_CheckBox_Checked_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        //TextBox
        private float[] Damage_Text_vert =
        {
            -0.3f, -0.2f, 0f, 0f, 0f,
            -0.3f, -0.4f, 0f, 0f, 1f,
            -0.0f, -0.4f, 0f, 1f, 1f,
            -0.0f, -0.2f, 0f, 1f, 0f
        };
        private int[] Damage_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_Damage_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Damage_Text_vert.Length * sizeof(float), Damage_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Damage_Text_index.Length * sizeof(uint), Damage_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[43]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Damage_Text()
        {
            GL.BindVertexArray(Tool_VAO[43]);
            texture.BindTexture(Tool_Textures_ID[43]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Damage_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }


        private float[] Texture_Text_vert =
        {
            -0.5f, -0.6f, 0f, 0f, 0f,
            -0.5f, -0.8f, 0f, 0f, 1f,
            -0.0f, -0.8f, 0f, 1f, 1f,
            -0.0f, -0.6f, 0f, 1f, 0f
        };
        private int[] Texture_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_Texture_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Texture_Text_vert.Length * sizeof(float), Texture_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Texture_Text_index.Length * sizeof(uint), Texture_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[44]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Texture_Text()
        {
            GL.BindVertexArray(Tool_VAO[44]);
            texture.BindTexture(Tool_Textures_ID[44]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Texture_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] BlockCoords_Text_vert =
        {
            0.2f, 0.6f, 0f, 0f, 0f,
            0.2f, 0.4f, 0f, 0f, 1f,
            0.5f, 0.4f, 0f, 1f, 1f,
            0.5f, 0.6f, 0f, 1f, 0f
        };
        private int[] BlockCoords_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_BlockCoords_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, BlockCoords_Text_vert.Length * sizeof(float), BlockCoords_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, BlockCoords_Text_index.Length * sizeof(uint), BlockCoords_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[45]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_BlockCoords_Text()
        {
            GL.BindVertexArray(Tool_VAO[45]);
            texture.BindTexture(Tool_Textures_ID[45]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, BlockCoords_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] CharecterWidth_Text_vert =
        {
            -0.5f, 0.6f, 0f, 0f, 0f,
            -0.5f, 0.4f, 0f, 0f, 1f,
            -0.3f, 0.4f, 0f, 1f, 1f,
            -0.3f, 0.6f, 0f, 1f, 0f
        };
        private int[] CharecterWidth_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_CharecterWidth_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CharecterWidth_Text_vert.Length * sizeof(float), CharecterWidth_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CharecterWidth_Text_index.Length * sizeof(uint), CharecterWidth_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[47]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CharecterWidth_Text()
        {
            GL.BindVertexArray(Tool_VAO[47]);
            texture.BindTexture(Tool_Textures_ID[47]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CharecterWidth_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] CharecterHeight_Text_vert =
        {
            0.3f, 0.6f, 0f, 0f, 0f,
            0.3f, 0.4f, 0f, 0f, 1f,
            0.5f, 0.4f, 0f, 1f, 1f,
            0.5f, 0.6f, 0f, 1f, 0f
        };
        private int[] CharecterHeight_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_CharecterHeight_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CharecterHeight_Text_vert.Length * sizeof(float), CharecterHeight_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CharecterHeight_Text_index.Length * sizeof(uint), CharecterHeight_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[48]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CharecterHeight_Text()
        {
            GL.BindVertexArray(Tool_VAO[48]);
            texture.BindTexture(Tool_Textures_ID[48]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CharecterHeight_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] HitPoint_Text_vert =
   {
            -0.5f, 0.2f, 0f, 0f, 0f,
            -0.5f, 0f, 0f, 0f, 1f,
            -0.3f, 0f, 0f, 1f, 1f,
            -0.3f, 0.2f, 0f, 1f, 0f
        };
        private int[] HitPoint_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_HitPoint_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, HitPoint_Text_vert.Length * sizeof(float), HitPoint_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, HitPoint_Text_index.Length * sizeof(uint), HitPoint_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[49]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_HitPoint_Text()
        {
            GL.BindVertexArray(Tool_VAO[49]);
            texture.BindTexture(Tool_Textures_ID[49]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, HitPoint_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] AnimationWidth_Text_vert =
   {
            -0.5f, -0.2f, 0f, 0f, 0f,
            -0.5f, -0.4f, 0f, 0f, 1f,
            -0.3f, -0.4f, 0f, 1f, 1f,
            -0.3f, -0.2f, 0f, 1f, 0f
        };
        private int[] AnimationWidth_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_AnimationWidth_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, AnimationWidth_Text_vert.Length * sizeof(float), AnimationWidth_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, AnimationWidth_Text_index.Length * sizeof(uint), AnimationWidth_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[50]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_AnimationWidth_Text()
        {
            GL.BindVertexArray(Tool_VAO[50]);
            texture.BindTexture(Tool_Textures_ID[50]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, AnimationWidth_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }


        private float[] Animation_Text_vert =
   {
            -0.5f, -0.6f, 0f, 0f, 0f,
            -0.5f, -0.8f, 0f, 0f, 1f,
            0.2f, -0.8f, 0f, 1f, 1f,
            0.2f, -0.6f, 0f, 1f, 0f
        };
        private int[] Animation_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_Animation_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Animation_Text_vert.Length * sizeof(float), Animation_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Animation_Text_index.Length * sizeof(uint), Animation_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[51]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_Animation_Text()
        {
            GL.BindVertexArray(Tool_VAO[51]);
            texture.BindTexture(Tool_Textures_ID[51]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Animation_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] AnimationShow_Text_vert =
         {
            0.4f, 0.2f, 0f, 0f, 0f,
            0.4f, -0.8f, 0f, 0f, 1f,
            0.9f, -0.8f, 0f, 1f, 1f,
            0.9f, 0.2f, 0f, 1f, 0f
        };
        private int[] AnimationShow_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_AnimationShow_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, AnimationShow_Text_vert.Length * sizeof(float), AnimationShow_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, AnimationShow_Text_index.Length * sizeof(uint), AnimationShow_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[52]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_AnimationShow_Text()
        {
            GL.BindVertexArray(Tool_VAO[52]);
            texture.BindTexture(Tool_Textures_ID[52]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, AnimationShow_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }


        private float[] SceneNumber_Text_vert =
         {
            -0.5f, 0.6f, 0f, 0f, 0f,
            -0.5f, 0.4f, 0f, 0f, 1f,
            -0.3f, 0.4f, 0f, 1f, 1f,
            -0.3f, 0.6f, 0f, 1f, 0f
        };
        private int[] SceneNumber_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_SceneNumber_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, SceneNumber_Text_vert.Length * sizeof(float), SceneNumber_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, SceneNumber_Text_index.Length * sizeof(uint), SceneNumber_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[53]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SceneNumber_Text()
        {
            GL.BindVertexArray(Tool_VAO[53]);
            texture.BindTexture(Tool_Textures_ID[53]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, SceneNumber_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] ScenWidth_Text_vert =
         {
            -0.5f, 0.2f, 0f, 0f, 0f,
            -0.5f, 0f, 0f, 0f, 1f,
            -0.3f, 0f, 0f, 1f, 1f,
            -0.3f, 0.2f, 0f, 1f, 0f
        };
        private int[] ScenWidth_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_SceneWidth_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, ScenWidth_Text_vert.Length * sizeof(float), ScenWidth_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, ScenWidth_Text_index.Length * sizeof(uint), ScenWidth_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[54]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SceneWidth_Text()
        {
            GL.BindVertexArray(Tool_VAO[54]);
            texture.BindTexture(Tool_Textures_ID[54]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, ScenWidth_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] ScenHeight_Text_vert =
         {
            -0.5f, -0.2f, 0f, 0f, 0f,
            -0.5f, -0.4f, 0f, 0f, 1f,
            -0.3f, -0.4f, 0f, 1f, 1f,
            -0.3f, -0.2f, 0f, 1f, 0f
        };
        private int[] ScenHeight_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_SceneHeight_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, ScenHeight_Text_vert.Length * sizeof(float), ScenHeight_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, ScenHeight_Text_index.Length * sizeof(uint), ScenHeight_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[55]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SceneHeight_Text()
        {
            GL.BindVertexArray(Tool_VAO[55]);
            texture.BindTexture(Tool_Textures_ID[55]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, ScenHeight_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] SceneBackGround_Text_vert =
         {
            -0.5f, -0.6f, 0f, 0f, 0f,
            -0.5f, -0.8f, 0f, 0f, 1f,
            0.2f, -0.8f, 0f, 1f, 1f,
            0.2f, -0.6f, 0f, 1f, 0f
        };
        private int[] SceneBackGround_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_SceneBackGround_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, SceneBackGround_Text_vert.Length * sizeof(float), SceneBackGround_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, SceneBackGround_Text_index.Length * sizeof(uint), SceneBackGround_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[56]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_SceneBackGround_Text()
        {
            GL.BindVertexArray(Tool_VAO[56]);
            texture.BindTexture(Tool_Textures_ID[56]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, SceneBackGround_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] CheckPointX_Text_vert =
         {
            0.3f, 0.6f, 0f, 0f, 0f,
            0.3f, 0.4f, 0f, 0f, 1f,
            0.5f, 0.4f, 0f, 1f, 1f,
            0.5f, 0.6f, 0f, 1f, 0f
        };
        private int[] CheckPointX_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_CheckPointX_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CheckPointX_Text_vert.Length * sizeof(float), CheckPointX_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CheckPointX_Text_index.Length * sizeof(uint), CheckPointX_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[57]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CheckPointX_Text()
        {
            GL.BindVertexArray(Tool_VAO[57]);
            texture.BindTexture(Tool_Textures_ID[57]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CheckPointX_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }

        private float[] CheckPointY_Text_vert =
        {
            0.3f, 0.2f, 0f, 0f, 0f,
            0.3f, 0f, 0f, 0f, 1f,
            0.5f, 0f, 0f, 1f, 1f,
            0.5f, 0.2f, 0f, 1f, 0f
        };
        private int[] CheckPointY_Text_index =
        {
            0, 1, 2, 3
        };

        private int Create_CheckPointY_Text_VAO()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, CheckPointY_Text_vert.Length * sizeof(float), CheckPointY_Text_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, CheckPointY_Text_index.Length * sizeof(uint), CheckPointY_Text_index, BufferUsageHint.StaticDraw);

            shader.ActiveProgram();

            var VertexLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            texture.BindTexture(Tool_Textures_ID[58]);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            shader.DiactiveProgram();
            return vao;
        }
        private void Draw_CheckPointY_Text()
        {
            GL.BindVertexArray(Tool_VAO[58]);
            texture.BindTexture(Tool_Textures_ID[58]);
            shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, CheckPointY_Text_index.Length, DrawElementsType.UnsignedInt, 0);
            shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }
    }

}
