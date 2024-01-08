using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    /// <summary>
    /// Obsluha uživatelského vstupu
    /// </summary>
    static class UserInput
    {

        public static MouseState MouseInput { get; private set; }
        public static Rectangle MouseRectangle { get; private set; }
        public static Vector2 MouseVector { get { return new Vector2(MouseInput.X, MouseInput.Y); } }
      
        static MouseState lastM;
        

        static KeyboardState keyInput;
        static KeyboardState lastKey;
       
        public static void Update()
        {
            lastM = MouseInput;
            MouseInput = Mouse.GetState();
            lastKey = keyInput;
            keyInput = Keyboard.GetState();

            MouseRectangle = new Rectangle(MouseInput.X, MouseInput.Y, 1, 1);
        }

        /// <summary>
        /// Return True / False is Key Pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyPress(Keys key)
        {
            return keyInput.IsKeyDown(key) && lastKey.IsKeyUp(key);
        }

        /// <summary>
        /// Key is UP
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyUp(Keys key)
        {
            return keyInput.IsKeyUp(key);
        }

        /// <summary>
        /// Key is Down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDown(Keys key)
        {
            return keyInput.IsKeyDown(key);
        }

        public static bool MouseLeftButtonPressed()
        {
            return lastM.LeftButton == ButtonState.Pressed && MouseInput.LeftButton == ButtonState.Released;
        }

        public static bool MouseRightButtonPressed()
        {
            return lastM.RightButton == ButtonState.Pressed && MouseInput.RightButton == ButtonState.Released;
        }

    }
}
