using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    public class Inventory : HitableObject
    {
        /// <summary>
        /// Itemy v inventáři
        /// </summary>
        public List<InventoryItem> Items { get; set; }
       
        public Texture2D TextureBackground { get; set; }

        int actualPositionX;
        public Inventory(Rectangle rectangle,Texture2D textureBackground)
            : base(rectangle)
        {
            Items = new List<InventoryItem>();
            TextureBackground = textureBackground;

            actualPositionX = 5;

        }

        public void AddItem(SpriteFont font, Texture2D texture, int maxCount)
        {
            //vypočítaná pozice
            Vector2 position = new Vector2(actualPositionX,this.Bound.Y + this.Bound.Height / 2 - texture.Height / 2);
           
            //vytvořený item
            InventoryItem item = new InventoryItem(font, texture, 
                new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), maxCount);
            
            int index = Items.IndexOf(item);

            if (index != -1)
            {
                Items[index].Count++;
            }
            else
            {
                actualPositionX += 3 + texture.Width;
                Items.Add(item);
            }
        }

        public void RemoveItem(SpriteFont font, Texture2D texture, int maxCount)
        {
            Vector2 position = new Vector2(actualPositionX, this.Bound.Height / 2 - texture.Height / 2);
            InventoryItem item = new InventoryItem(font, texture,
                new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), maxCount);

            int index = Items.IndexOf(item);

            if (index != -1)
            {
                Items[index].Count--;
            }
            else
            {
                actualPositionX += 5 + texture.Width;
                Items.Add(item);
            }
        }      

        public void IncreaseCountOfItem(InventoryItem item, int count)
        {
            int index = Items.IndexOf(item);

            if (index != -1)
                Items[index].Count += count;
        }

        public void DeCreaseCountOfItem(InventoryItem item, int count)
        {
            int index = Items.IndexOf(item);

            if (index != -1)
                Items[index].Count -= count;
        }

        public void Draw(BetterSpriteBatch spriteBatch)
        {

            spriteBatch.Draw(TextureBackground, Bound, Color.White);

            foreach (InventoryItem item in Items)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
