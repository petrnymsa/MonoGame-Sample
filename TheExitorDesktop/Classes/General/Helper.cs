using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    class Helper
    {
        static Random rnd = new Random();

        /// <summary>
        /// Vrátí náhodnou barvu s nastavenou Barvou
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color GetRandomColor(int alpha)
        {
            if(alpha>255)
                alpha=255;
            if(alpha<0)
                alpha=0;
            return Color.FromNonPremultiplied(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256), alpha);
        }

        /// <summary>
        /// Vrátí Barvu s nastavenou hodnotou Alpha
        /// </summary>
        /// <param name="color"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color GetColorWithAlpha(Color color,int alpha)
        {
            return Color.FromNonPremultiplied(color.R, color.G, color.B, alpha);
        }

        
    }
}
