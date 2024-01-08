//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace TheExitorDesktop
//{

   
//    class PopUpMessage : DrawableGameComponent
//    {
//        Game1 g;
//        string text;
//        Rectangle rectangle;
//        Vector2 textPos;
//        Button btnOk;
//        Button btnNo;


//        Texture2D background;
       

//        public event PopUpEventHandler Closing;

//        public PopUpMessage(Game1 game, string text,PopUpButtons type)
//            : base(game)
//        {
          
//            this.g = game;
//            this.text = text;
//             Vector2 sizeYes = g.FontStencilMiddle.MeasureString("YES");
//                Vector2 sizeNo = g.FontStencilMiddle.MeasureString("NO");
//            Vector2 textSize = g.FontStencilMiddle.MeasureString(text);
//             int btnSpace = 15;
//             int halfWidth = g.Display.Width / 2; // polovina šířky obrazovky
//             int halfHeight = g.Display.Height / 2; // poloviny šířky obrazovky
//             int width = (int)textSize.X+50; // šířka rectanglu
//             int height = (int)textSize.Y + btnSpace + 40; // výška rectanglu

//             if (width >= g.Display.Width)
//                 width = g.Display.Width - 20;
           
//            this.rectangle = new Rectangle(halfWidth - width/2 , halfHeight- height/2, width, height);    
//            this.textPos = new Vector2(rectangle.X + (rectangle.Width / 2 - textSize.X / 2), rectangle.Y + 10);
//           //  this.textPos = new Vector2(rectangle.X, rectangle.Y + 10);
           
//            int posY=(int)(rectangle.Y + rectangle.Height); // pozice tlačítek

//            if (textSize.X > rectangle.Width - 10)
//            {
//                string[] arr = text.Split(' ');

//                int index = 0;
//                string[] n = new string[arr.Length];
//                string words = "";

//                foreach (string p in arr)
//                {
//                    words += p;
//                    Vector2 size = g.FontStencilMiddle.MeasureString(words);
//                    if (size.X > rectangle.Width - 10)
//                    {
//                        n[index] = words + "\n";
//                        index++;
//                        words = "";
//                    }
//                    else words += " ";

//                }
//                if (words != string.Empty)
//                    n[index] = words;

//                this.text = "";
//                foreach (string p in n)
//                    this.text += p;
//            }


//            if (type == PopUpButtons.Ok)
//            {
//                Vector2 sizeOk=g.FontStencilSmall.MeasureString("OK");
//                Rectangle rectOk=new Rectangle((int)(rectangle.X+rectangle.Width/2 - sizeOk.X/2),(int)(posY - sizeOk.Y),(int)sizeOk.X,(int)sizeOk.Y);
//                btnOk = new Button(rectOk, g.FontStencilSmall, "OK", 0,1);
                
//            }
//            else
//            {
//                Rectangle rectYes = new Rectangle((int)(rectangle.X + (rectangle.Width / 2) / 2 - sizeYes.X / 2),(int)(posY - sizeYes.Y), (int)sizeYes.X, (int)sizeYes.Y);
//                Rectangle rectNo = new Rectangle((int)(rectangle.X + rectangle.Width - (rectangle.Width / 2) / 2 - sizeNo.X / 2),(int)(posY - sizeNo.Y), (int)sizeNo.X, (int)sizeNo.Y);
//                btnOk = new Button(rectYes, g.FontStencilMiddle, "YES", 0, 1);
//                btnNo = new Button(rectNo, g.FontStencilMiddle, "NO", 1, 1);

//                btnNo.Click += btn_Click;
//            }


//            btnOk.Click += btn_Click;
          
//        }

//        void btn_Click(object sender, EventArgs e)
//        {
//            Button k = (Button)sender;

//            if (k.ID == 0)
//            {
//                Closing(this, new PopUpEventArgs(PopUpStatus.Yes));
//            }
//            else
//            {
//                Closing(this, new PopUpEventArgs(PopUpStatus.No));
//            }
           
//        }
     
//        public override void Initialize()
//        {
            

//            base.Initialize();
//        }

//        protected override void LoadContent()
//        {
//            background = g.Textures["PopUpBackground"];
//            base.LoadContent();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            btnOk.Update();
//            if (btnNo != null)
//                btnNo.Update();
//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            g.spriteBatch.Begin();            
//            g.spriteBatch.DrawTextureWithBorder(background, rectangle, Color.Black, 3);
//            g.spriteBatch.DrawString(g.FontStencilMiddle, text, textPos, Color.White);          
//            btnOk.Draw(g.spriteBatch);
//            if (btnNo != null)
//                btnNo.Draw(g.spriteBatch);

//            g.spriteBatch.End();
//            base.Draw(gameTime);
//        }
//    }
//}
