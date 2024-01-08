using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    /// <summary>
    /// Abstract. Base třída pro User-Komponenty
    /// </summary>
    abstract class UserComponent : HitableObject
    {
      
        public UserComponent(Rectangle rectangle)
            : base(rectangle)
        {
            
        }

        public abstract void Update();
        
        public abstract void Draw(BetterSpriteBatch spriteBatch);
        

        
    }
}
