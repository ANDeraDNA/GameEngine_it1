using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.PixelFormats;
using IronSoftware.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;

//using OpenTK.Graphics.ES11;

namespace Maneger
{
    
    class Texture2D
    {
        public int CreateTexture(string TexturePath)
        {
            Image<Rgba32> texture;
            try
            {
                texture = Image.Load<Rgba32>(TexturePath);
            }
            catch
            {
                texture = Image.Load<Rgba32>("Data/Texts/Null_Texture.png");
            }
            var pixels = new byte[4 * texture.Width * texture.Height];
            texture.CopyPixelDataTo(pixels);
            int TextureID = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                (int)texture.Width, (int)texture.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixels
            );
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            return TextureID;
        }
        public void BindTexture(int ID)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }
        public void DeleteTexture(int[] _indeces)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            for (int i = 0; i < _indeces.Length; i++)
            {
                GL.DeleteTexture(_indeces[i]);
            }

        }
        public void Create_Text(string text, string imagepath)
        {
            try
            {
            Image image = Image.Load(imagepath);
            image.Mutate(x => x.Clear(SixLabors.ImageSharp.Color.Grey));
            Font font = new Font("Times New Roman", FontStyle.Italic, 28);
            image.Mutate(x => x.DrawText(text, font, SixLabors.ImageSharp.Color.Yellow, new PointF(10, 10)));
            image.Save(imagepath);
            image.Dispose(); 
            }
            catch(Exception ex)
            {

            }
        }
        public void UpDate_Texture(int ID, string texturepath)
        {
            Image<Rgba32> texture;
            try
            {
                texture = Image.Load<Rgba32>(texturepath);
            }
            catch
            {
                texture = Image.Load<Rgba32>("Data/Texts/Null_Texture.png");
            }
            var pixels = new byte[4 * texture.Width * texture.Height];
            texture.CopyPixelDataTo(pixels);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                (int)texture.Width, (int)texture.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixels
            );
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }
        Image<Rgba32> Animation;
        public void AddAnimation(string path)
        {
            try
            {
                Animation = Image.Load<Rgba32>(path);
            }
            catch
            {
                Animation = Image.Load<Rgba32>("Data/Texts/Null_Texture.png");
            }
        }
        public void UpDateAnimation(int Derection, int AnimationNumber, int AnWidth, int AnimationID)
        {
            int imgWidht = Animation.Width;
            int imgHeight = Animation.Height;
            var cadr = Animation.Clone();
            cadr.Mutate<Rgba32>(i => i.Crop(new Rectangle(imgWidht / AnWidth * AnimationNumber, imgHeight / 4 * Derection, imgWidht / AnWidth, imgHeight / 4)));
            cadr.Mutate<Rgba32>(i => i.Resize(imgWidht / AnWidth, imgHeight / 4));
            var pixels = new byte[4 * cadr.Width * cadr.Height];
            cadr.CopyPixelDataTo(pixels);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, AnimationID);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                (int)cadr.Width, (int)cadr.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                pixels
            );
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
    }
}
