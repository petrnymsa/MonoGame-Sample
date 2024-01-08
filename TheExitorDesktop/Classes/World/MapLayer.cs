using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Třída představující jendu vrstvu mapu
    /// </summary>
    public class MapLayer
    {
        /// <summary>
        /// Veškeré LigtObject ve vrstvě
        /// </summary>
        public List<GameObject> Tiles { get; private set; }

        

        /// <summary>
        /// Index vrstvy
        /// </summary>
        public sbyte LayerOffset { get; set; }

        public MapLayer(sbyte layerIndex)
        {
            this.Tiles = new List<GameObject>();
            this.LayerOffset = layerIndex;
        }

        public MapLayer(List<GameObject> tiles, sbyte layerIndex)
        {
            this.Tiles = new List<GameObject>();

            foreach (GameObject tile in tiles)
            {
                this.Tiles.Add(tile);
            }

            this.LayerOffset = layerIndex;
        }



        /// <summary>
        /// Přidá tile
        /// </summary>
        /// <param name="tile"></param>
        public virtual void Add(GameObject tile)
        {
            Tiles.Add(tile);
        }

        /// <summary>
        /// Vymaže tile
        /// </summary>
        /// <param name="tile"></param>
        public virtual void Remove(GameObject tile)
        {
            if (Tiles.Contains(tile))
                Tiles.Remove(tile);
        }

        public virtual void Draw(BetterSpriteBatch spriteBatch)
        {
            foreach (GameObject p in Tiles)
            {
                p.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Zkontroluje kolizi a v případě kolize vrátí TRUE
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public virtual bool CheckCollision(Rectangle rectangle)
        {
            foreach (GameObject tile in Tiles)
            {
                if(tile.Solid && tile.CheckCollision(rectangle))              
                    return true;                
            }
            return false;
        }

        /// <summary>
        /// Zkontroluje kolizi a vrátí kolidovaný objekt ve vstvě
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="onlySolid"></param>
        /// <returns></returns>
        public virtual GameObject CheckCollisionObject(Rectangle rectangle,bool onlySolid)
        {
            foreach (GameObject tile in Tiles)
            {
                if (tile.CheckCollision(rectangle) && (onlySolid ? tile.Solid: true))
                    return tile;
            }

            return null;
        }
    }
}
