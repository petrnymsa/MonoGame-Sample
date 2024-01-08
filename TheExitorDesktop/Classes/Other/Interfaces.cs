using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{
    public interface ILightableObject
    {
        TileVisibility Visibility { get; set; }
        bool IsExplored { get; set; }

        void RefreshLight(Rectangle bound);
       
    }
}
