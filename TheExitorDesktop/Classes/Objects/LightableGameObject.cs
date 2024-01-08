using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Objekt který lze ovlivnit světlem
    /// </summary>
    public class LightableGameObject : GameObject, ILightableObject
    {
        private TileVisibility visibility;
        public TileVisibility Visibility
        {
            get
            { return visibility; }
            set
            {
                visibility = value;
                if (visibility == TileVisibility.Visible || visibility==TileVisibility.Explored)
                    IsExplored = true;
            }
        }

        public bool IsExplored { get; set; }

        public LightableGameObject(Texture2D texture, Rectangle rectangle, bool isSolid)
            : base(texture, rectangle, isSolid)
        {
        }       

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Draw(BetterSpriteBatch spriteBatch)
        {
            if (Visibility == TileVisibility.Visible)
                spriteBatch.Draw(Texture, Bound, Color.White);
            else if (Visibility == TileVisibility.Explored)
                spriteBatch.Draw(Texture, Bound, Color.Gray);
           
        }


        public void RefreshLight(Rectangle bound)
        {
            if (CheckCollision(bound))
            {               
                Visibility = TileVisibility.Visible;
            }
            else if (IsExplored)
                Visibility = TileVisibility.Explored;
           // else Visibility = TileVisibility.NoVisible;
        }
    }
}
