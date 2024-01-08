using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    class PopUpHint : GameObject
    {
        public bool IsOpen { get; set; }
        public PopUpHint(Texture2D texture, Rectangle rectangle, string text)
            : base(texture, rectangle,false)
        { 
        }

        public override void Draw(BetterSpriteBatch spriteBatch)
        {
            if (IsOpen)
            {
                spriteBatch.Draw(Texture, Bound, Color.White);
            }
        }
    }
}
