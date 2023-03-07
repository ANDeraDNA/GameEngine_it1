using System;
using System.Collections.Generic;
using System.Text;

namespace Maneger
{
     public class Block
    {
        public Block()
        {
            _IsMaterial = false;
            _IsStatic = false;
            _IsDamage = false;
            Damage = 0;
            Texture_Path = "";
            TextureID = -1;
        }
        public Block(bool isStatic, bool isMaterial, string texture_Path, bool isDamage, int damage, int textureID, float[] vboColor)
        {
            IsStatic = isStatic;
            IsMaterial = isMaterial;
            Texture_Path = texture_Path;
            IsDamage = isDamage;
            Damage = damage;
            TextureID = textureID;
            Texture_Path = texture_Path;
            VBO_Color = vboColor;
        }


        float[] VBO_Color = { 0.2f, 0.2f, 0.2f, 0.2f};
        private bool _IsStatic;
        private bool _IsMaterial;
        private string _Texture_Path;
        private bool _IsDamage;
        private int _Damage;
        private int _TextureID;
        public bool IsStatic
        {
            get { return _IsStatic; }
            set { _IsStatic = value; }
        }
        public bool IsMaterial
        {
            get { return _IsMaterial; }
            set { _IsMaterial = value; }
        }
        public int TextureID
        {
            get { return _TextureID; }
            set { _TextureID = value; }
        }
        public bool IsDamage
        {
            get { return _IsDamage; }
            set { _IsDamage = value; }
        }
        public int Damage
        {
            get { return _Damage; }
            set { _Damage = value; }
        }
        public string Texture_Path
        {
            get { return _Texture_Path; }
            set { _Texture_Path = value; }
        }
        public float[] Color
        {
            get { return VBO_Color; }
            set { VBO_Color = value; }
        }
        public float[] Set_Color(float[] Block_Color)
        {
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[0];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[1];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[2];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[3];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[0];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[1];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[2];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[3];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[0];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[1];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[2];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[3];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[0];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[1];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[2];
            Array.Resize(ref Block_Color, Block_Color.Length + 1);
            Block_Color[Block_Color.Length - 1] = VBO_Color[3];
            return Block_Color;
        }
    }

}
