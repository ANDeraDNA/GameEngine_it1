using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace Maneger
{
    internal class Comand
    { 
        public float[] DrawPole(int Width, int Heigh)
        {   
            float[] pole_vbo = new float[0];
            for (int i = 0; i <= Width + 1; i++)
            {
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = (float)(2.0 / (Width + 1) * i - 1);
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 1f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = -0.1f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = (float)(2.0 / (Width + 1) * i - 1);
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = -1f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = -0.1f;

            }
            for (int i = 0; i <= Heigh + 1; i++)
            {
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 1f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = (float)(2.0 / (Heigh + 1) * i - 1);
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = -0.1f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = -1f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = (float)(2.0 / (Heigh + 1) * i - 1);
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = -0.1f;
            }
            return pole_vbo;
        }
        public float[] FillPole(int Width, int Heigh)
        {
            float[] pole_vbo = new float[0];
            for (int i = 0; i <= Width + 1; i++)
            {
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;

            }
            for (int i = 0; i <= Heigh + 1; i++)
            {
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
                Array.Resize(ref pole_vbo, pole_vbo.Length + 1);
                pole_vbo[pole_vbo.Length - 1] = 0.7f;
            }
            return pole_vbo;
        }
        public Scene[] Creating_New_Scene(Scene[] _scene)
        {
            for (int i = 0; i < _scene.Length; i++)
            {
                if (_scene[i] == null)
                {
                    _scene[i] = new Scene();
                }
            }
            return _scene;
        }
        public float[] Draw_Block_VBO(int[,] pole_block, int width, int heigh, ref float[] Block_Color, Vector2 SelectedBlock, Block[,] block)
        {
            Block_Color = new float[0];
            float[] vbo = new float[0];
            for (int i = 0; i < heigh; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (pole_block[j, i]!= 0)
                    {
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)(j * 2.0 / (width)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)(i * 2.0 / (heigh)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length - 1] = 0.2f;

                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)((j+1) * 2.0 / (width)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)(i * 2.0 / (heigh)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length - 1] = 0.2f;


                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)((j+1) * 2.0 / (width)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)((i+1) * 2.0 / (heigh)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length - 1] = 0.2f;


                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)(j * 2.0 / (width)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length-1] = (float)((i+1) * 2.0 / (heigh)) - 1;
                        Array.Resize(ref vbo, vbo.Length + 1);
                        vbo[vbo.Length - 1] = 0.2f;

                        if(SelectedBlock.X ==j && SelectedBlock.Y == i)
                        {
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                            Array.Resize(ref Block_Color, Block_Color.Length + 1);
                            Block_Color[Block_Color.Length - 1] = 0.5f;
                        }
                        else
                        {
                            Block_Color = block[j,i].Set_Color(Block_Color);
                        }
         
                    }
                    else
                    {
                       // Block_Color = block[0].Set_Color(Block_Color);
                    }
                }
            }
            return vbo;
        }
    }
}
