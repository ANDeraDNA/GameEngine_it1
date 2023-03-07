using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Runtime.CompilerServices;

using WpfLibrary1;

namespace Maneger
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Game Engine\r\nAuthor: Soloviov Andrii\r\nCurator: Entin Yosif Abramovicich");
            Scene[] scene = new Scene[1];
            scene[0] = new Scene();
            Console.WriteLine("Enter the width: ");
            scene[0].Width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the height: ");
            scene[0].Height = Convert.ToInt32(Console.ReadLine());
            Comand comand = new Comand();
            float[] Block_VBOs = new float[0];
            float[] Charecter_VBOs = new float[1];
            float[] Pole_Vertex = comand.DrawPole(scene[0].Width, scene[0].Height);
            float[] Pole_Color = comand.DrawPole(scene[0].Width, scene[0].Height);
            Charecter MainCharecter = new Charecter();
            Block[,] blocks = new Block[scene[0].Width + 1, scene[0].Height + 1];
            float[] Block_Color = new float[0];
            for (int i = 0; i < scene[0].Width+1; i++)
            {
                for (int j = 0; j < scene[0].Height + 1; j++)
                {
                    blocks[i, j] = new Block();
                }
            }
            NativeWindowSettings nativeWinSettings = new NativeWindowSettings()
            { 
            
            //Work with the OpenGL form

            Size = new Vector2i(3200, 1800),
                Location = new Vector2i(0, 0),
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,
                StartVisible = true,
                StartFocused = true,
                Title = "GameEngeen",

                APIVersion = new Version(3, 3),
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Compatability,
                API = ContextAPI.OpenGL,


                NumberOfSamples = 0

            };
            using (Visualisator game = new Visualisator(GameWindowSettings.Default, nativeWinSettings, Block_VBOs, Charecter_VBOs, Pole_Vertex, Pole_Color, Block_Color, blocks, scene, MainCharecter))
            {
                game.Run();
            }

        }

    }
}
