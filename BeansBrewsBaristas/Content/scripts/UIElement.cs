using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Content.scripts
{
    public class UIElement : Sprite
    {
        public Vector2 Origin; // Relative to Local Pos
        public Vector2 Anchor; // Relative to Screen Pos

        #region CONSTRUCTORS
        public UIElement(
            Vector2 position,
            Color? color,
            Texture2D texture = null) :
            base(position, texture, color)
        { }

        public UIElement(
            Global.Vertex origin,
            Color? color,
            Texture2D texture = null) :
            base(GetPosFromVertex(origin), texture, color)
        { }
        #endregion
    }
}
