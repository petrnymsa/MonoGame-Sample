using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Třída představující část mapy
    /// </summary>
    public class MapTile : LightableGameObject, IEquatable<MapTile>
    {

        public int Width { get; set; }
        public int Height { get; set; }

        public MapTile(Texture2D texture, Rectangle rectangle, bool solid, sbyte layerOffset)
            : base(texture, rectangle, solid)
        {

            //this.IsExplored = false;
            this.LayerOffset = layerOffset;

        }

        public bool Equals(MapTile other)
        {
            if (other != null)
            {
                return other.Position == this.Position && other.Solid == this.Solid && other.Texture == this.Texture &&
                    other.Height == this.Height && other.Width == this.Width && other.Visibility == this.Visibility && other.IsExplored == this.IsExplored;
            }

            return false;
        }
    }
}
