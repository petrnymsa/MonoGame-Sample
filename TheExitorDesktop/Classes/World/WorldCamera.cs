using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// 2D kamera
    /// </summary>
    public class WorldCamera
    {
        private Vector2 center; // centrování kamery       
        private Vector2 position; // pozice kamery
        public Viewport View { get; private set; } // view kamery
        public Matrix Transform { get; private set; } // Transformace
        public Vector2 Position { get { return position; } }  // pozice kamery

       

        public Rectangle CameraBound // Rectangle kamery
        {
            get
            {
                
                return new Rectangle((int)Position.X - View.Width/2,(int)Position.Y-View.Height/2, View.Width, View.Height);
            }
           
        }

        public float Zoom { get; set; }
        public float Rotation { get; set; }
     

        public WorldCamera(Viewport view)
        {
            this.View = view;          
            this.center = new Vector2(view.Width / 2, view.Height / 2);
            
            this.Zoom = 1;
            this.Rotation = 0f;
        }

        public void Update(Vector2 focus, int width, int height)
        {
            Viewport view = View;
            position.X = focus.X;
            position.Y = focus.Y;

            Transform = Matrix.Identity *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateTranslation(-position.X, -position.Y, 0) *
                Matrix.CreateTranslation(center.X, center.Y, 0);
        }

        public Vector2 GetScreenPosition(Vector2 worldPosition)
        {
            return worldPosition - position;
        }

        public Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            return screenPosition + position;
        } 
    }
}
