using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Třída držící informace o aktuální mapě
    /// </summary>
    class Map
    {
        MazeGenerator maze;              
        Random rnd;

        Game1 g;

      //  ContentManager content;           

        /// <summary>
        /// RAW loaded map, not changing by re-moving / adding tiles
        /// </summary>
        public int[,] ArrayMap { get; set; }


        public int TileSize { get; set; } // velikost Tiles
      //  public List<MapTile> Tiles { get; private set; }

        /// <summary>
        /// Jednotlivé vrstvy mapy
        /// </summary>
        public List<MapLayer> Layers { get;private set; }

        public int CountOfLayers { get { return Layers.Count; } }

        public List<InventoryItem> Diamonds { get;private set; }

        /// <summary>
        /// Viditelné tile
        /// </summary>
        public List<MapLayer> VisibleLayers { get; private set; }

        public int RelativeWidth { get { return ArrayMap.GetLength(0); } }
        public int RelativeHeight { get { return ArrayMap.GetLength(1); } }
        public int AbsoluteWidth { get { return ArrayMap.GetLength(0) * TileSize; } }
        public int AbsoluteHeight { get { return ArrayMap.GetLength(1) * TileSize; } }


        public Map(Game1 game, int tileSize)
        {
            this.g = game;
            this.maze = new MazeGenerator();
            this.rnd = new Random();
            this.TileSize = tileSize;
            this.Layers = new List<MapLayer>();
            this.VisibleLayers = new List<MapLayer>();
            this.Diamonds = new List<InventoryItem>();


        }

        public void GeneratedMap(int width,int height)
        {
            ArrayMap = maze.Generate(width, height);

            ReInitTiles();

            PlaceDiamonds();
        }

        public void PlaceDiamonds()
        {
          //  List<Point> whitePlaces = new List<Point>();

         //   List<InventoryItem> diamonds = new List<InventoryItem>();

            MapLayer layer = new MapLayer(1);
            int absX = 0;
            int absY=0;
            Texture2D dia;
            Item item;

            for (int x = 0; x < RelativeWidth; x++)
            {
                for (int y = 0; y < RelativeHeight; y++)
                {
                    absX = x*TileSize;
                    absY = y*TileSize;
                    if (ArrayMap[x, y] == 0)
                    {
                        if (rnd.Next(0, 101) <= 70)
                        {
                            dia = g.Textures["BlueDiamond"];
                            item = new Item(dia,new Rectangle(absX + dia.Width / 2, absY + dia.Height / 2, dia.Width, dia.Height), 1);
                            item.LayerOffset = 1;
                            layer.Add(item);
                        }
                        else
                        {
                            dia = g.Textures["RedDiamond"];
                            item = new Item(dia, new Rectangle(absX + dia.Width / 2, absY + dia.Height / 2, dia.Width, dia.Height), 3);
                            item.LayerOffset = 1;
                            layer.Add(item);
                        }
                    }
                }
            }

           // Layers.Add(new MapLayer(diamonds, 1));
            Layers.Add(layer);

           
        }

        /// <summary>
        /// Navrátí list specifických objektů
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="onlyVisible"></param>
        /// <returns></returns>
        public List<TOut> RetrieveObjectList<TOut>(bool onlyVisible)
        {
            List<TOut> list = new List<TOut>();

            foreach (MapLayer layer in onlyVisible ? VisibleLayers : Layers)
            {
                var ret = layer.Tiles.ReturnType<GameObject, TOut>();

                foreach(TOut p in ret.ToList())
                {
                    list.Add(p);
                }

            }

            return list;
        }

        /// <summary>
        /// /Refreshne objekty pro vykreslení
        /// </summary>
        /// <param name="boundOfCamera"></param>
        public void RefreshCameraVisibility(Rectangle boundOfCamera)
        {
            sbyte index = 0;
            VisibleLayers.Clear();
            List<GameObject> tiles  =new List<GameObject>();
            foreach (MapLayer layer in Layers)
            {
                foreach (GameObject tile in layer.Tiles)
                {
                    if(tile.CheckCollision(boundOfCamera))
                    {
                        tiles.Add(tile);                        
                    }
                }
                VisibleLayers.Add(new MapLayer(tiles, index));
                index++;
                tiles.Clear();
            }
        }


        /// <summary>
        /// Refreshne světla
        /// </summary>
        /// <param name="lightBound"></param>
        public void RefreshLights(Rectangle lightBound)
        {
            foreach (MapLayer layer in Layers)
            {
                foreach (LightableGameObject tile in layer.Tiles)
                {
                    tile.RefreshLight(lightBound);
                }
            }
        }

        /// <summary>
        /// Zkontroluje kolizi s objekty v mapě
        /// </summary>
        /// <param name="layerIndex">-1 Pro všechny vrstvy</param>
        public bool CheckCollision(sbyte layerIndex, Rectangle rectangle)
        {
            if (layerIndex == -1)
            {
                foreach (MapLayer layer in VisibleLayers)
                {
                    if (layer.CheckCollision(rectangle))
                        return true;
                }               
            }
            else
            {
                try
                {
                    return Layers[layerIndex].CheckCollision(rectangle);
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Zkontroluje kolizi s objekty a vrátí kolidovaný objekt
        /// </summary>
        /// <param name="layerIndex">Index vrstvy / -1 = všechny vrstvy </param>
        /// <param name="rectangle"></param>
        /// <param name="onlySolid"></param>
        /// <returns></returns>
        public virtual GameObject CheckCollisionObject(sbyte layerIndex, Rectangle rectangle,bool onlySolid)
        {
            if (layerIndex == -1)
            {
                try
                {
                    foreach (MapLayer layer in Layers)
                    {
                        GameObject obj = layer.CheckCollisionObject(rectangle, onlySolid);
                        if (obj != null)
                            return obj;
                    }
                }
                catch { return null; }

            }
            else
            {
                try
                {
                    foreach (GameObject tile in Layers[layerIndex].Tiles)
                    {

                        if (tile.CheckCollision(rectangle) && onlySolid ? tile.Solid : true)
                            return tile;
                    }

                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Zkontroluje kolizi s mlhou
        /// </summary>
        /// <param name="layerIndex"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public virtual bool CheckCollisionWithFog(sbyte layerIndex, Rectangle rectangle)
        {
            if (layerIndex == -1)
            {
                try
                {
                    foreach (MapLayer layer in VisibleLayers)
                    {
                        foreach (LightableGameObject tile in layer.Tiles)
                        {
                            if (tile.CheckCollision(rectangle) && tile.Visibility == TileVisibility.NoVisible)
                            {
                                return true;
                            }
                        }
                    }
                }
                catch { return false; }
            }
            else
            {
                try
                {
                    foreach (LightableGameObject tile in Layers[layerIndex].Tiles)
                    {
                        if (tile.CheckCollision(rectangle) && tile.Visibility == TileVisibility.NoVisible)
                        {
                            return true;
                        }
                    }

                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Vykreslí všechny vrstvy postupně za sebou
        /// </summary>
        /// <param name="sort"></param>
        public virtual void DrawAllLayersSorted(LayerSorting sort)
        {
            if (sort == LayerSorting.BackToFront)
            {
                foreach (MapLayer layer in VisibleLayers)
                {
                    layer.Draw(g.spriteBatch);
                }
            }
            else
            {
                for (int i = VisibleLayers.Count - 1; i >= 0; i--)
                {
                    VisibleLayers[i].Draw(g.spriteBatch);
                }
            }
        }

        /// <summary>
        /// Změní tile ve specifické vrstvě
        /// </summary>
        /// <param name="layerIndex"></param>
        /// <param name="oldTile"></param>
        /// <param name="newTile"></param>
        public virtual void ChangeTile(byte layerIndex, MapTile oldTile, MapTile newTile)
        {
            //try
            //{
                this.Layers[layerIndex].Tiles.Remove(newTile);

                this.Layers[layerIndex].Tiles.Add(newTile);
              //  this.Tiles.Remove(oldTile);
              //  this.Tiles.Add(newTile);
            //}
            //catch
            //{
            //    ErrorLogger.Log("Cannot reach layer with " + layerIndex.ToString());
            //} 
         
       
        }

        /// <summary>
        /// Vymaže objekt ve specifické vrstvě
        /// </summary>
        /// <param name="layerIndex"></param>
        /// <param name="objectToRemove"></param>
        public virtual void RemoveObject(sbyte layerIndex, GameObject objectToRemove)
        {
            try
            {
                Layers[layerIndex].Tiles.Remove(objectToRemove);
            }
            catch
            {

            }
        }


        public virtual void LoadMapFromFile(string fileName)
        {          
            string[] lines = File.ReadAllLines(string.Format("Maps/{0}.txt", fileName));

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');

                if (i == 0)
                    ArrayMap = new int[line.Length, lines.Length];

                for (int j = 0; j < line.Length; j++)
                {
                    ArrayMap[j, i] = int.Parse(line[j]);
                }
            }

            ReInitTiles();
        }

        public Point prePoint;
        public Point absPoint;

        /// <summary>
        /// Vrátí náhodné volné místo v mapě. Pouze z array RAW !
        /// </summary>
        /// <returns></returns>
        public virtual Point GetRandomWhiteSpace()
        {
            List<Point> spaces = new List<Point>();

            for (int x = 0; x < RelativeWidth; x++)
            {
                for (int y = 0; y < RelativeHeight; y++)
                {
                    if (ArrayMap[x, y] == 0)
                        spaces.Add(new Point(x, y));

                }
            }

            prePoint = spaces[rnd.Next(0, spaces.Count)];
            absPoint = new Point(prePoint.X * TileSize, prePoint.Y * TileSize);

            return absPoint;
        }



        protected virtual void ReInitTiles()
        {           
            MapLayer layer = new MapLayer(0);

            for (int x = 0; x < RelativeWidth; x++)
            {
                for (int y = 0; y < RelativeHeight; y++)
                {
                    switch (ArrayMap[x, y])
                    {
                        case 0:
                            MapTile t = new MapTile(g.Textures["StoneDarkFloor"],
                            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), false, 0);
                            t.Tag = "Floor";
                            layer.Add(t);
                            
                            break;
                        case -1: layer.Add(new MapTile(g.Textures["StoneWall"],
                            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), true, 0));

                            break;
                    }
                }
            }

            Layers.Clear();
            Layers.Add(layer);

            
        }

    }
}
