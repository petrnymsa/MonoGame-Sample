using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{  
    /// <summary>
    /// Obecný objekt s Rectanglem
    /// </summary>
    public abstract class HitableObject
    {
        /// <summary>
        /// Rectangle objektu
        /// </summary>
        public Rectangle Bound { get; protected set; }

        /// <summary>
        /// Pozice objektu, resp. rectanglu
        /// </summary>
        public Vector2 Position 
        {
            get
            { 
                return new Vector2(Bound.X, Bound.Y);
            }

            set
            {
                Vector2 p = value;

                Bound = new Rectangle((int)p.X, (int)p.Y, Bound.Width, Bound.Height);
            }
        }

        public HitableObject(Rectangle rectangle)
        {
            this.Bound = rectangle;
        }

        /// <summary>
        /// Zkontroluje kolizi mezi tímto objektem a objektem jiným => Rectangle vs Rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public virtual bool CheckCollision(Rectangle rect)
        { return this.Bound.Intersects(rect); }        
    }
}
