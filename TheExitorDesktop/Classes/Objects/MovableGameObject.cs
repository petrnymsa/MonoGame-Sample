using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Třída představující pohybující se objekt
    /// </summary>
    public class MovableGameObject : GameObject
    {
        public float Alpha { get; set; }
        public float Speed { get; set; }  
        public MovableGameObject(Texture2D texture, Rectangle rectangle, float alpha,float speed)
            : base(texture, rectangle,true)
        {
            this.Alpha = alpha;
            this.Speed = speed;
        
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle rect = Bound;
            rect.X+= (int)(Math.Sin(Alpha) * Speed);
            rect.Y-= (int)(Math.Cos(Alpha) * Speed);

            Bound = rect;
        }
    }
}
