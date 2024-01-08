//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace TheExitorDesktop
//{
//    class PopUpMenu : DrawableGameComponent
//    {
//        Game1 g;
//        List<Button> buttons;
//        Rectangle rectangle;
//        Texture2D background;

//        public event EventHandler Closing;

//        public PopUpMenu(Game1 game)
//            : base(game)
//        {
//            this.g = game;
//        }

//        public override void Initialize()
//        {
//            buttons = new List<Button>();
//            string[] names = new string[] { "Main menu" };

//            int width=350;
//            int height=600;
//            int halfWidth=g.Display.Width/2;
//            int halfHeight=g.Display.Height/2;
//            rectangle = new Rectangle(halfWidth - width/2, halfHeight - height/2, width, height);

//            buttons.Add(new Button(new Rectangle(rectangle.X + 5, rectangle.Y + 50, 120, 120), g.FontStencilSmall, "Main menu",0,3,Color.Black,Color.Gray,Color.White));
//            buttons[0].Click += PopUpMenu_Click;

//            base.Initialize();
//        }

//        void PopUpMenu_Click(object sender, EventArgs e)
//        {
//            Closing(sender, e);
//        }

//        protected override void LoadContent()
//        {
//            background = g.Textures["PopUpBackground"];
//            base.LoadContent();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            buttons[0].Update();

//            if (UserInput.KeyPress(Keys.Escape))
//                Closing(-1, EventArgs.Empty);

//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            g.spriteBatch.Begin();
//            g.spriteBatch.DrawTextureWithBorder(background, rectangle, Color.Black, 3);
            
//            buttons[0].Draw(g.spriteBatch);
//            g.spriteBatch.End();
//            base.Draw(gameTime);
//        }
//    }
//}
