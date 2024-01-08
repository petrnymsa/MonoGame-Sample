using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    public class GameScreenManager
    {
        GameComponentCollection gameComponents;

        public GameScreen CurrentScreen { get; private set; }
        public List<GameScreen> Screens { get; private set; }
        public GameScreenManager(GameComponentCollection components)
        {
            this.gameComponents = components;
            Screens = new List<GameScreen>();
            CurrentScreen = null;
            foreach (GameComponent p in gameComponents)
            {
                ChangeComponent(p, false);
            }
        }

        public void AddScreen(GameScreen screen)
        {
            //if (!Screens.Contains(screen))
            Screens.Add(screen);
        }

        public void ChangeScreen(string screenKey)
        {
            GameScreen screen = Screens.Where((x) => x.Key == screenKey).First();
            if (screen == null)
                throw new Exception(string.Format("Screen with key {0} not exists", screenKey));

            foreach (GameComponent gm in gameComponents)
            {
                bool enable = screen.Components.Contains(gm);
                ChangeComponent(gm, enable);
            }

            CurrentScreen = screen;

        }

        private void ChangeComponent(GameComponent component, bool enable)
        {
            component.Enabled = enable;
            if (component is DrawableGameComponent)
                ((DrawableGameComponent)component).Visible = enable;
        }


    }
}
