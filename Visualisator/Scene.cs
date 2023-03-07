using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maneger
{
    public class Scene
    {
        public Scene()
        {
            _Width = 0;
            _Height = 0;
            _blocks = new Block[1, 1];
            _IntBlock = new int[1,1];
            _blocks[0, 0] = new Block();
            _IntBlock[0, 0] = 0;
            _CheckPoint.X = 0;
            _CheckPoint.Y = 0;
            _TexturePath = "Data/Texts/Null_Texture.png";
        }
        private int _Width;
        private int _Height;
        private string _TexturePath;
        private int _TextureID;
        private Block[,] _blocks;
        private int[,] _IntBlock;
        private Vector2 _CheckPoint;
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
        public string TexturePath
        {
            get { return _TexturePath; }
            set
            {
                _TexturePath = value;
            }
        }
        public int TextureID
        {
            get { return _TextureID; }
            set
            {
                _TextureID = value;
            }
        }
        public Block[,] blocks
        {
            get { return _blocks; }
            set
            {
                _blocks = value;
            }
        }
        public int[,] IntBlock
        {
            get { return _IntBlock; }
            set
            {
                _IntBlock = value;
            }
        }
        public int CheckPointX
        {
            get { return (int)_CheckPoint.X; }
            set { _CheckPoint.X = value; }
        }
        public int CheckPointY
        {
            get { return (int)_CheckPoint.Y; }
            set { _CheckPoint.Y = value; }
        }
        public void CreateSceneBackGround()
        {
            Texture2D texture = new Texture2D();
            _TextureID = texture.CreateTexture(_TexturePath);
        }
    }
}

