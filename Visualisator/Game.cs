using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
//using OpenTK.Graphics.ES20;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Threading;

namespace Maneger
{
    internal class Game
    {
        private static Vector2 CharecterPosition;
        int _AnimationID;
        int HitPoints;
        int Width;
        int Height;
        int Animation_Width;
        Shader Animation_Shader;
        int SceneWidht;
        int SceneHeight;
        int Slide = 0;
        Texture2D _texture = new Texture2D();
        Block[,] blocks;
        int[,] IntBlocks;
        Camera camera;
        private static Vector2 StartCharecterPosition;
        int StartHitPoints;
        public Game(Charecter charecter, Shader animation_Shader, Scene scene, Camera camera)
        {
            CharecterPosition = new Vector2();
            _AnimationID = charecter.AnimatioID;
            Animation_Shader = animation_Shader;
            HitPoints = charecter.HitPoints;
            StartHitPoints = charecter.HitPoints;
            Width = charecter.Width;
            Height = charecter.Height;
            Animation_Width = charecter.AnimationWidth;
            SceneWidht = scene.Width;
            SceneHeight = scene.Height;
            CharecterPosition.X = (float)(scene.CheckPointX * 2.0 / SceneWidht - 1);
            CharecterPosition.Y = (float)(scene.CheckPointY * 2.0 / SceneHeight - 1);
            StartCharecterPosition = CharecterPosition;

            blocks = scene.blocks;
            IntBlocks = scene.IntBlock;
            camera.position = CharecterPosition;

            Character_Animation_vert[0] = 0f;
            Character_Animation_vert[1] = (float)(Height * 2.0 / 20 * charecter.Height);
            Character_Animation_vert[5] = 0f;
            Character_Animation_vert[6] = 0f;
            Character_Animation_vert[10] = (float)(Width * 2.0 / 10 * charecter.Width);
            Character_Animation_vert[11] = 0;
            Character_Animation_vert[15] = (float)(Width * 2.0 / 10 * charecter.Width);
            Character_Animation_vert[16] = (float)(Height * 2.0 / 20 * charecter.Height);

            _texture.AddAnimation(charecter.AnimationPath);
            _texture.UpDateAnimation(0, 0, Animation_Width, _AnimationID);
            AnimationVAO = Charecter_CreateVAOs();
            this.camera = camera;
            //camera.zoom = ((10 / SceneWidht)+(10 / SceneHeight))/2;
            //camera.position = CharecterPosition;

        }

        int AnimationVAO;

        private float[] Character_Animation_vert =
        {
            0.1f, 0.2f, 0f, 0f, 0f,
            0.1f, 0f, 0f, 0f, 1f,
            0.2f, 0f, 0f, 1f, 1f,
            0.2f, 0.2f, 0f, 1f, 0f
        };
        private int[] Character_Animation_index =
        {
            0, 1, 2, 3
        };

        private int Charecter_CreateVAOs()
        {

            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Character_Animation_vert.Length * sizeof(float), Character_Animation_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Character_Animation_index.Length * sizeof(uint), Character_Animation_index, BufferUsageHint.StaticDraw);

            Animation_Shader.ActiveProgram();

            var VertexLocation = Animation_Shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = Animation_Shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture.BindTexture(_AnimationID);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Animation_Shader.DiactiveProgram();
            return vao;
        }
        private void UpDateVAO()
        {     
            GL.BindVertexArray(AnimationVAO);

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, Character_Animation_vert.Length * sizeof(float), Character_Animation_vert, BufferUsageHint.StaticDraw);

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Character_Animation_index.Length * sizeof(uint), Character_Animation_index, BufferUsageHint.StaticDraw);

            Animation_Shader.ActiveProgram();

            var VertexLocation = Animation_Shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(VertexLocation);
            GL.VertexAttribPointer(VertexLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);

            var textcordloc = Animation_Shader.GetAttribLocation("aTexCoord"); ;
            GL.EnableVertexAttribArray(textcordloc);
            GL.VertexAttribPointer(textcordloc, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            _texture.BindTexture(_AnimationID);

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Animation_Shader.DiactiveProgram();
            GL.DeleteBuffer(vbo);
        }
        public void Draw_Charecter()
        {
            GL.BindVertexArray(AnimationVAO);
            _texture.BindTexture(_AnimationID);
            Animation_Shader.ActiveProgram();
            GL.DrawElements(PrimitiveType.Quads, Character_Animation_index.Length, DrawElementsType.UnsignedInt, 0);
            Animation_Shader.DiactiveProgram();
            GL.BindVertexArray(0);
        }
             
        public void Delete_CharecterVAO()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(AnimationVAO);
        }

        public void MoveRight()
        {
            
            if(!RightChecker())
            {
                CharecterPosition.X += (float)(2.0 / SceneWidht / 16);
                camera.position = CharecterPosition;
            }  
            
            
            Slide++;
            if((Slide + 1) == Animation_Width)
            {
                Slide = 0;
            }
            _texture.UpDateAnimation( 2, Slide, Animation_Width, _AnimationID);
            UpDateVAO();
            Thread.Sleep(75);
        }
        public void MoveLeft()
        {
            if(!LeftChecker())
            {
                CharecterPosition.X -= (float)(2.0 / SceneWidht / 16);
                camera.position = CharecterPosition;
            }
            
            Slide++;
            if ((Slide + 1) == Animation_Width)
            {
                Slide = 0;
            }
            _texture.UpDateAnimation(1, Slide, Animation_Width, _AnimationID);
            UpDateVAO();
            Thread.Sleep(75);
        }
        public void MoveDown()
        {
            if(!DownChecker())
            {
                CharecterPosition.Y -= (float)(2.0 / SceneHeight / 16);
                camera.position = CharecterPosition;
            }     
            Slide++;
            if ((Slide + 1) == Animation_Width)
            {
                Slide = 0;
            }
            _texture.UpDateAnimation(0, Slide, Animation_Width, _AnimationID);
            UpDateVAO();
            Thread.Sleep(75);
        }
        public void MoveUP()
        {
            if(!UPChecker())
            {
                CharecterPosition.Y += (float)(2.0 / SceneHeight / 16);
                camera.position = CharecterPosition;
            }
           
            Slide++;
            if ((Slide + 1) == Animation_Width)
            {
                Slide = 0;
            }
            _texture.UpDateAnimation(3, Slide, Animation_Width, _AnimationID);
            UpDateVAO();
            Thread.Sleep(75);         

        }
        
        private bool RightChecker()
        {
           if(Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht+1)))) + 1) >= blocks.GetLength(0))
            {
                return true;
            }
            if (IntBlocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))) + 1, Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight+1)))))] == 1)
            {
                if(blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))) + 1, Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].IsDamage)
                {
                    HitPoints -= blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))) + 1, Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].Damage;
                    if(HitPoints <= 0)
                    {
                        CharecterPosition = StartCharecterPosition;
                        HitPoints = StartHitPoints;
                        return false;
                    }
                }
                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))) + 1, Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].IsMaterial)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private bool LeftChecker()
        {
            if(Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1)))))<0)
            {
                return true;
            }
            if (IntBlocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))] == 1)
            {
                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].IsDamage)
                {
                    HitPoints -= blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].Damage;
                    if (HitPoints <= 0)
                    {
                        CharecterPosition = StartCharecterPosition;
                        HitPoints = StartHitPoints;
                        return false;
                    }
                }

                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].IsMaterial)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private bool UPChecker()
        {
            if(Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))) + 1)>=blocks.GetLength(1))
            {
                return true;
            }
            if(IntBlocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1))))) + 1] == 1)
            {
                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor(((CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1))))) + 1].IsDamage)
                {
                    HitPoints -= blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1))))) + 1].Damage;
                    if (HitPoints <= 0)
                    {
                        CharecterPosition = StartCharecterPosition;
                        HitPoints = StartHitPoints;
                        return false;
                    }
                }

                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1))))) + 1].IsMaterial)
                     return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool DownChecker()
        {
            if(Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1))))) < 0)
            {
                return true;
            }
            if(IntBlocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1)/ (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1)/ (2.0/ (SceneHeight + 1)))))] == 1)
            {
                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].IsDamage)
                {
                    HitPoints -= blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight+1)))))].Damage;
                    if (HitPoints <= 0)
                    {
                        CharecterPosition = StartCharecterPosition;
                        HitPoints = StartHitPoints; 
                        return false;
                    }
                }

                if (blocks[Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.X + 1) / (2.0 / (SceneWidht + 1))))), Convert.ToInt32(Math.Floor((Convert.ToDouble(CharecterPosition.Y + 1) / (2.0 / (SceneHeight + 1)))))].IsMaterial)
                     return true;
                 else
                      return false;
            }
            else
            {
                return false;
            }
        }


    }
}
