using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    /// <summary>
    /// Komponenta představující button
    /// </summary>
    class Button : UserComponent
    {
        Vector2 textPosition;
        Vector2 fontSize;
        SpriteFont font;
        Color color = Color.White;
        Color colorHover = Color.WhiteSmoke;
        Color colorShadow = Color.Black;
        string text;
        bool hover; 
        int space; // mezera

        public byte ID { get; private set; }
        public event EventHandler Click;
        public event EventHandler Hover;

        public Button(Rectangle rectangle, SpriteFont font, string text, byte id,int sizeShadow)
            : base(rectangle)
        {
          
            this.font = font;
            this.text = text;
            this.ID = id;
            this.space=sizeShadow;

            fontSize = font.MeasureString(text);

            int size = (int)(fontSize.X);
            this.textPosition = new Vector2(rectangle.X + text.Length, rectangle.Y);
            this.Bound = new Rectangle((int)textPosition.X, (int)textPosition.Y, size, font.LineSpacing);
        }

        public Button(Rectangle rectangle, SpriteFont font, string text, byte id, int sizeShadow, Color colorText, Color colorHover, Color colorShadow)
            : base(rectangle)
        {
            this.text = text;
            this.font = font;
            this.ID = id;
            this.space=sizeShadow;
            this.color = colorText;
            this.colorShadow = colorShadow;
            this.colorHover = colorHover;
           

            Vector2 fontSize = font.MeasureString(text);

            int size = (int)(fontSize.X);
            this.textPosition = new Vector2(rectangle.X + text.Length, rectangle.Y);
            this.Bound = new Rectangle((int)textPosition.X, (int)textPosition.Y, size, font.LineSpacing);
        }

        
        public override void Update()
        {
            hover = false;

            if (CheckCollision(UserInput.MouseRectangle) && UserInput.MouseLeftButtonPressed())
            {
                if (Click != null)
                    Click(this, EventArgs.Empty);

                
            }
            else if (CheckCollision(UserInput.MouseRectangle))
            {
                hover = true;
                if (Hover != null)
                    Hover(null, EventArgs.Empty);
            }
        }

        public override void Draw(BetterSpriteBatch spriteBatch)
        {
            int shadow = space - 1;
            if (shadow < 1)
                shadow = 1;
       
            if (hover)
                spriteBatch.DrawTextShadow(font, text, new Vector2(textPosition.X + space, textPosition.Y + space), color, colorHover,shadow);
            else
                spriteBatch.DrawTextShadow(font, text, new Vector2(textPosition.X + space, textPosition.Y + space), color, colorShadow,shadow);
        }
    }
}
