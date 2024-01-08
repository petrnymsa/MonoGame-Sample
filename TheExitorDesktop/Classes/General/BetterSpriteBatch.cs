using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    public class BetterSpriteBatch : SpriteBatch
    {

        public Color BlackFog { get; private set; }

        public BetterSpriteBatch(GraphicsDevice device)
            : base(device)
        {
            this.BlackFog = Color.FromNonPremultiplied(0, 0, 0, 126);
        }

        /// <summary>
        /// Vykreslí text se stínem v -45 stupních
        /// </summary>
        /// <param name="font">Font písma</param>
        /// <param name="text">Text</param>
        /// <param name="position">Pozice</param>
        /// <param name="colorText">Barva textu</param>
        /// <param name="colorShadow">Barva stínu</param>
        /// <param name="size">Velikost stínu</param>
        public void DrawTextShadow(SpriteFont font, string text, Vector2 position, Color colorText, Color colorShadow, int size)
        {
            int k = 2;
            for (int i = 0; i < size; i++)
            {
                DrawString(font, text, new Vector2(position.X + k, position.Y + k), colorShadow);
                k += 1;
            }
            DrawString(font, text, position, colorText);

            
        }

        /// <summary>
        /// Vykreslí Texturu se stínem
        /// </summary>
        /// <param name="texture">Textura</param>
        /// <param name="rectangle">Pozice</param>
        /// <param name="shadow">Barva stínu</param>
        /// <param name="space">Odsazení stínu</param>
        public void DrawTextureShadow(Texture2D texture, Rectangle rectangle, Color shadow, int space)
        {
            Draw(texture, rectangle, Color.White);
            Draw(texture, new Rectangle(rectangle.X+space,rectangle.Y+space,texture.Width,texture.Height), shadow);
        }

        /// <summary>
        /// Vykreslí barevný rectangle s ohraničením. Výpočetní náročnost !
        /// </summary>
        /// <param name="rectangle">Rectangle k vykreslení</param>
        /// <param name="colorRectangle">Barva rectanglu</param>
        /// <param name="border">Barva ohraníčení</param>
        /// <param name="sizeBorder">Velikost ohraničení</param>
        public void DrawRectangleWithBorder(Rectangle rectangle, Color colorRectangle, Color border, int sizeBorder)
        {
            Draw(GetColorTexture(rectangle.Width, rectangle.Height, border), 
                new Rectangle(rectangle.X - sizeBorder, rectangle.Y - sizeBorder, rectangle.Width + sizeBorder*2, rectangle.Height + sizeBorder*2), Color.White);
            Draw(GetColorTexture(rectangle.Width, rectangle.Height, colorRectangle), rectangle, Color.White);
        }

        /// <summary>
        /// Vykreslí Texturu s rámečkem
        /// </summary>
        /// <param name="texture">Textura k vykreslení</param>
        /// <param name="rectangle">Pozice</param>
        /// <param name="colorBorder">Barva rámečku</param>
        /// <param name="sizeBorder">Velikost rámečku</param>
        public void DrawTextureWithBorder(Texture2D texture,Rectangle rectangle, Color colorBorder, int sizeBorder)
        {
            Draw(GetColorTexture(texture.Width, texture.Height, Color.White),
                new Rectangle(rectangle.X - sizeBorder, rectangle.Y - sizeBorder, rectangle.Width + sizeBorder * 2, rectangle.Height + sizeBorder * 2), colorBorder);
            Draw(texture, rectangle, Color.White);
        }

        /// <summary>
        /// Vykreslní barevný obdélník
        /// </summary>
        /// <param name="rectangle">Obdélník k vykreslení</param>
        /// <param name="color">Barva obdélníku</param>
        public void DrawNewTextureColor(Rectangle rectangle, Color color)
        {
            Draw(GetColorTexture(rectangle.Width, rectangle.Height, color), rectangle, Color.White);
        }


        /// <summary>
        /// Vrátí barevnou jedno-barevnou texturu
        /// </summary>
        /// <param name="width">Šířka textury</param>
        /// <param name="height">Výška textury</param>
        /// <param name="color">Barva textury</param>
        /// <returns></returns>
        public Texture2D GetColorTexture(int width, int height, Color color)
        {
            Texture2D tx = new Texture2D(this.GraphicsDevice, width, height);
            Color[] data = new Color[tx.Width * tx.Height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            tx.SetData(data);
            return tx;
        }       
    }
}
