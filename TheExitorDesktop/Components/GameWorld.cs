using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    public class GameWorld : DrawableGameComponent
    {
      
        Game1 g;       
        Player player;
        public WorldCamera Camera { get; private set; }

        Map map;        
    
        bool debug;

        int score;

      
        
        public GameWorld(Game1 game)
            : base(game)
        {
            this.g = game;
            
            
          
        }

        public override void Initialize()
        {
            map = new Map(g, 64);
            map.GeneratedMap(60, 60);

           this.Camera = new WorldCamera(new Viewport(0,0,g.Display.Width,g.Display.Height));
           Point playerPos = map.GetRandomWhiteSpace();
           player = new Player(g, new Vector2(playerPos.X + 37, playerPos.Y + 37));
            base.Initialize();
        }

        protected override void LoadContent()
        {
           
    
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (UserInput.KeyPress(Keys.Escape))
            { Reset(); g.ScreenManager.ChangeScreen("MainMenu"); }
            if (UserInput.KeyPress(Keys.F1))
                debug = !debug;

            player.Update(gameTime);
            Camera.Update(player.Position, player.Bound.Width, player.Bound.Height);
         
            map.RefreshCameraVisibility(Camera.CameraBound); // Refreshne viditelné objekty
            map.RefreshLights(new Rectangle(player.Bound.X - 120, player.Bound.Y - 120, 240, 240)); // refreshne světlo          

            // Checkne kolizi mezi hráčem a objekty v mapě  
            if (map.CheckCollision(-1, player.CollisionRectangle))
            {
                if(!debug)
               player.FireUpCollision();
            }

            //Check kolize s diamanty
            List<Item> items = map.RetrieveObjectList<Item>(false);
            for (int i = 0; i < items.Count; i++)
            {
                if (player.CheckCollision(items[i].Bound))
                {
                    map.RemoveObject(items[i].LayerOffset, items[i]);
                    score += (int)items[i].Tag;
                    player.PlayerInventory.AddItem(g.Fonts["Stencil10"], items[i].Texture, -1);
                }
            }

            //Check kolize z Bullet hráče a okolním světem
            List<MovableGameObject> bullets = player.Bullets;
            for (int i = 0; i < bullets.Count; i++)
            {
                GameObject gm= map.CheckCollisionObject(-1, bullets[i].Bound, true);
                if (gm != null || map.CheckCollisionWithFog(-1, bullets[i].Bound) || !bullets[i].CheckCollision(Camera.CameraBound))
                {
                    bullets.RemoveAt(i);
                    if (gm != null)
                        map.RemoveObject(gm.LayerOffset, gm);
                    i--;
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {            
           
            g.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);
            map.DrawAllLayersSorted(LayerSorting.BackToFront);          
            player.Draw(g.spriteBatch);     
            g.spriteBatch.End();

            
            g.spriteBatch.Begin();
            player.PlayerInventory.Draw(g.spriteBatch); // vykreslení hráčova inventáře
            g.spriteBatch.DrawString(g.FontStencilMiddle, string.Format("Score: {0}", score), new Vector2(15, 15), Color.Yellow);
         //   g.spriteBatch.DrawString(g.FontStencilSmall, string.Format("PRE: X= {0} Y= {1}", map.prePoint.X, map.prePoint.Y), new Vector2(15, 80), Color.Yellow);
          //  g.spriteBatch.DrawString(g.FontStencilSmall, string.Format("ABS: X= {0} Y= {1}", map.absPoint.X, map.absPoint.Y), new Vector2(15, 120), Color.Yellow);
            g.spriteBatch.End();
          
            if (debug)
            { 
                g.spriteBatch.Begin();         
                g.spriteBatch.DrawString(g.FontStencilSmall, string.Format("Bullets: {0}", player.Bullets.Count), new Vector2(0, 30), Color.Red);
                g.spriteBatch.DrawString(g.FontStencilSmall, string.Format("FPS: {0}",1000 / gameTime.ElapsedGameTime.TotalMilliseconds), new Vector2(0, 60), Color.Red);
                g.spriteBatch.DrawString(g.FontStencilSmall, string.Format("Player Bounds - Width: {0}, Height: {1}, X: {2}, Y: {3}", player.Bound.Width, player.Bound.Height, player.Bound.X,player.Bound.Y), new Vector2(0, 90), Color.Red);            
             
                g.spriteBatch.DrawString(g.FontStencilSmall, string.Format("{0},{1}",Camera.CameraBound.Width/map.TileSize,Camera.CameraBound.Height/map.TileSize), new Vector2(0, 120), Color.Red);           
                g.spriteBatch.End();
            }
           
            base.Draw(gameTime);
        }

        public void Reset()
        {
            map = new Map(g, 64);
            map.GeneratedMap(60, 60);

            this.Camera = new WorldCamera(new Viewport(0, 0, g.Display.Width, g.Display.Height));
            Point playerPos = map.GetRandomWhiteSpace();
            player = new Player(g, new Vector2(playerPos.X + 37, playerPos.Y + 37));
        }


    }


 
}
