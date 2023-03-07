using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using OpenTK.Graphics.OpenGL;


namespace Maneger
{
    internal class Texture
    {
        int TextureID;
        int TextureWidth;
        int TextureHeight;
        public int ID
        {
            get { return TextureID; }
            set { TextureID = value; }
        }
        public int Width
        {
            get { return TextureWidth; }
            set { TextureWidth = value; }
        }
        public int Height
        {
            get { return TextureHeight; }
            set { TextureHeight = value; }
        }
        public int CreateTexture(string TexturePath)
        {
            Image<Rgba32> texture = Image.Load<Rgba32>(TexturePath);
            var pixels = new byte[4 * texture.Width * texture.Height];
            texture.CopyPixelDataTo(pixels);
            int TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                (int)texture.Width, (int)texture.Height,
                0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixels
            );
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            return TextureID;
        }
        public int CreateText(string Text, int width, int height)
        {
                                     
            int TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            return TextureID;
        }
    }
}
