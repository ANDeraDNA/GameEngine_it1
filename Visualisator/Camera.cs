using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace Maneger
{
    internal class Camera
    {
        public Vector2 position;
        public double Rotation;
        public double zoom;
        public Camera(Vector2 startposition, double startzoom = 1.0, double startrotation = 0.0)
        {
            this.position = startposition;
            this.Rotation = startrotation;
            this.zoom = startzoom;
        }
        public void Update(Vector2 newposition, double newzoom, double newrotation)
        {
            this.position = newposition;
            Rotation = newrotation;
            this.zoom = newzoom;
        }
        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;
            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-(float)Rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));

            GL.MultMatrix(ref transform);
        }   
        public void Moves(KeyboardState keys)
        {
            
            if (keys.IsKeyDown(Keys.W))
            {
                position.Y += (float)(0.01/zoom);
            }
            if (keys.IsKeyDown(Keys.S))
            {
                position.Y -= (float)(0.01f/zoom);
            }
            if (keys.IsKeyDown(Keys.A))
            {
                position.X -= (float)(0.01f / zoom);
            }
            if (keys.IsKeyDown(Keys.D))
            {
                position.X += (float)(0.01f / zoom);
            }
            if (keys.IsKeyDown(Keys.Q))
            {
                zoom -= 0.01;
            }
            if (keys.IsKeyDown(Keys.E))
            {
                zoom += 0.01;
            }
        }
    }
}
