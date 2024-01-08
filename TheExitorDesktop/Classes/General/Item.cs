using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    class Item : LightableGameObject
    {
        public Item(Texture2D texture, Rectangle rectangle, int cost)
            : base(texture, rectangle, false)
        {
            this.Tag = cost;
        }
    }
}
