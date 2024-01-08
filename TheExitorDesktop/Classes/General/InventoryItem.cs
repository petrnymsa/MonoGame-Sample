using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    public class InventoryItem : LightableGameObject, IEquatable<InventoryItem>
    {
        int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                if (MaxCount > 0 && count >= MaxCount)
                    count = MaxCount;
            }
        }

        public int MaxCount { get; set; }
        public SpriteFont Font { get; private set; }     

        public InventoryItem(SpriteFont font, Texture2D texture, Rectangle rectangle, int maxCount)
            : base(texture, rectangle, true)
        {
            Count = 1;
            this.Font = font;
            this.MaxCount = maxCount;

           
        }


        public bool Equals(InventoryItem other)
        {
            if (other != null)
            {
                return this.Tag == other.Tag && this.Texture == other.Texture;

            }
            return false;

        }

        public override void Draw(BetterSpriteBatch spriteBatch)
        {
            Vector2 size = Font.MeasureString(Count.ToString());
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Font, Count.ToString(), new Vector2(Bound.Right - size.X - 2f, Bound.Bottom), Color.White);
        }

       
    }
}
