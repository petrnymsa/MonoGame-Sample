using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    public class GameScreen : IEquatable<GameScreen>
    {

        string key;
        public string Key { get { return key; } }
        /// <summary>
        /// Components of Screen
        /// </summary>
        public List<GameComponent> Components { get; private set; }

        Game1 g;
        public GameScreen(Game1 game, string key, params GameComponent[] newComponents)
        {
            this.g = game;
            this.key = key;
            this.Components = new List<GameComponent>();
            foreach (GameComponent p in newComponents)
            {
                AddComponent(p);
            }

        }

        /// <summary>
        /// Add new compnent to game screen
        /// </summary>
        /// <param name="component">Game component</param>
        public void AddComponent(GameComponent component)
        {
            this.Components.Add(component);
            if (!g.Components.Contains(component))
                g.Components.Add(component);
        }

        public bool Equals(GameScreen other)
        {
            foreach (GameComponent oG in other.Components)
            {
                if (!Components.Contains(oG))
                    return false;
            }

            return true;

        }

        public override string ToString()
        {
            return Key;
        }
    }
}
