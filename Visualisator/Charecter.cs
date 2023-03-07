using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Maneger
{
     class Charecter
    {
        private int _Width;
        private int _Height;
        private string _AnimationPath;
        private int _AnimationID;
        private int _HitPoints;
        private int _AnimationWidth;
        public int Width 
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        public string AnimationPath
        {
            get { return _AnimationPath; }
            set { _AnimationPath = value;}
        }
        public int AnimatioID
        {
            get { return _AnimationID; }
            set
            {
                _AnimationID = value;
            }
        }
        public int HitPoints
        {
            get { return _HitPoints; }
            set
            {
                _HitPoints = value;
            }
        }
        public int AnimationWidth
        {
            get { return _AnimationWidth; }
            set { _AnimationWidth = value; }
        }
        public Charecter()
        {
            _Width = 1;
            _Height = 2;
            _AnimationPath = "Data/Texts/Null_Texture.png";
            _HitPoints = 1;
            AnimationWidth = 1;

        }
            


    }
}
