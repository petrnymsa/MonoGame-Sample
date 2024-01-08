using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    class Menu : DrawableGameComponent
    {
        Texture2D background;
        Game1 g;     
        List<Button> buttons;      

        public Menu(Game1 game)
            : base(game)
        {
            this.g = game;        
            this.buttons = new List<Button>();
          
        }
     
        public override void Initialize()
        {
            buttons = new List<Button>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = g.Content.Load<Texture2D>("Background/menu");
            buttons.Clear();
            string[] names = new string[] { "New Game", "Load Game", "Settings","Exit" };
            for (byte i = 0; i < 4; i++)
            {
                Vector2 size = g.FontStencilBig.MeasureString(names[i]);
                buttons.Add(new Button(new Rectangle((int)(g.Display.Width / 2 - size.X / 2), (int)(250 + (i * size.Y + 25)), (int)size.X, (int)size.Y),
                    g.FontStencilBig, names[i], i,8, Color.FromNonPremultiplied(54,54,54,255), Color.FromNonPremultiplied(84,84,84,180), Color.Black));
                buttons[i].Click += Menu_Click;
            }

          

            base.LoadContent();
        }
        
        private void Menu_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

           
            switch (btn.ID)
            {
                case 0: // Nová hra                   
                    g.ScreenManager.ChangeScreen("World");
                    break;
                case 1: // Načíst hru 
                    break;
                case 2:
                     // Setting                   
                    break;
                case 3: 
                    g.Exit();
                        
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {            

            foreach (Button p in buttons)
                p.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            g.spriteBatch.Begin();          
            g.spriteBatch.Draw(background, new Rectangle(0, 0, (int)g.Display.Width, (int)g.Display.Height), Color.White);
            foreach (Button p in buttons)
                p.Draw(g.spriteBatch);         
            g.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
