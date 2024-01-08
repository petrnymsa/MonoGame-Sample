using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Obecný herní objekt s texturou
    /// </summary>
    public class GameObject : HitableObject,IEquatable<GameObject>
    {
        public Texture2D Texture { get; set; }
        public bool Solid { get; set; }
        public object Tag { get; set; }
        public sbyte LayerOffset { get; set; }

        public GameObject(Texture2D texture, Rectangle rectangle,bool solid)
            : base(rectangle)
        {
            this.Texture=texture;
            this.Solid = solid;
            this.LayerOffset = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Draw(BetterSpriteBatch spriteBatch)
        {
            if(Texture!=null)
            spriteBatch.Draw(Texture,Bound, Color.White);        
        }

        public bool Equals(GameObject other)
        {
            if (other != null)
            {
                return this.Bound == other.Bound && this.Solid==other.Solid && this.Texture == other.Texture;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Bound.GetHashCode();
        }
    }
}
