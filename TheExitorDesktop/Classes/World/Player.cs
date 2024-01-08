using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Třída představující objekt ovladatelný hráčem
    /// </summary>  
    public class Player : GameObject
    {      
        float rotation;
        Vector2 position; //  aktulní pozice
        Vector2 lastPosition; // předchozí pozice
        Texture2D txBullet;
     //   Rectangle collision; // rectangle pro kolizi

        float sprintInterval;
        float sprintAfterInterval;

        Game1 g;

        public List<MovableGameObject> Bullets { get; private set; }
        public byte Live { get; set; } // životy
        public Inventory PlayerInventory { get; private set; }
        public float Speed { get; private set; }

        /// <summary>
        /// Rectangle pro kolize
        /// </summary>
        public Rectangle CollisionRectangle { get { return new Rectangle(Bound.X - Texture.Width/2, Bound.Y - Texture.Height/2, Bound.Width, Bound.Height); } }
        

        public Player(Game1 game,Vector2 position)
            : base(game.Textures["Player"], new Rectangle((int)position.X, (int)position.Y, game.Textures["Player"].Width, game.Textures["Player"].Height), true)
        {

            this.g = game;
            this.position = position;        
            this.txBullet = g.Textures["Bullet"];
            this.Live = 100;
            this.Speed = 4f;
            this.Bullets = new List<MovableGameObject>();
            this.PlayerInventory = new Inventory(new Rectangle(0, g.Display.TitleSafeArea.Bottom - 120, g.Display.Width, 120),
                g.spriteBatch.GetColorTexture(g.Display.Width, 250, Color.FromNonPremultiplied(80, 15, 15, 125)));

            sprintAfterInterval = 35;
            sprintInterval = sprintAfterInterval;
          
        }


        public override void Update(GameTime gameTime)
        {
            Rectangle rectangle = Bound;
            lastPosition = position;


            // rotace
            Vector2 distance = new Vector2(g.Display.Width / 2, g.Display.Height / 2) - UserInput.MouseVector;

            rotation = (float)(Math.Atan2(distance.Y, distance.X) - Math.PI / 2);

            if (UserInput.KeyDown(Keys.A)) position.X -= Speed;
            if (UserInput.KeyDown(Keys.D)) position.X += Speed;
            if (UserInput.KeyDown(Keys.W)) position.Y -= Speed;
            if (UserInput.KeyDown(Keys.S)) position.Y += Speed;

            if (UserInput.MouseLeftButtonPressed()) // střelba      
            {
                Bullets.Add(new MovableGameObject(txBullet, new Rectangle((int)position.X,
                    (int)position.Y, 5, 5), rotation, 8f));
            }


          //  rectangle = GetRectangle();
            //collision = new Rectangle((int)position.X-Texture.Width, (int)position.Y-Texture.Height, Texture.Width, Texture.Height);

            // Update střel
            foreach (MovableGameObject p in Bullets)
                p.Update(gameTime);


            Bound = GetRectangle();


        }

        public override void Draw(BetterSpriteBatch spriteBatch)
        {
            foreach (MovableGameObject p in Bullets)
            {
                spriteBatch.Draw(txBullet, p.Bound, Color.White);
            }
            spriteBatch.Draw(Texture, Bound, null, Color.White, rotation, new Vector2(Bound.Width / 2, Bound.Height / 2), SpriteEffects.None, 0f);



            //spriteBatch.Draw(spriteBatch.GetColorTexture(Bound.Width, Bound.Height, Color.Red), Bound, Color.White);
            // spriteBatch.Draw(spriteBatch.GetColorTexture(collision.Width, collision.Height, Color.Red), Bound, Color.White);
        }
        /// <summary>
        /// Vyvolá kolizi
        /// </summary>
        public void FireUpCollision()
        {
            position = lastPosition;
            Bound = GetRectangle();
        }

        /// <summary>
        /// Zkontroluje kolizi
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public override bool CheckCollision(Rectangle rect)
        {            
            if (CollisionRectangle.Intersects(rect))
            {               
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Vezme střední projekt
        /// </summary>
        /// <returns></returns>
        private Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
        }

        /// <summary>
        /// Nastaví novou pozici hráče
        /// </summary>
        /// <param name="vector"></param>
        public void SetNewPosition(Vector2 vector)
        {
            this.position = vector;
            lastPosition = position;
            this.Position = position;
            Bound = GetRectangle();
        }
    
    
    }
}
