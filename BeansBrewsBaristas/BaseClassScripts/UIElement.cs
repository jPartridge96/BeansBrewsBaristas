using BeansBrewsBaristas.BaseClassScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Content.scripts
{
    /// <summary>
    /// An element to display on the Game Screen
    /// </summary>
    public class UIElement : Sprite
    {
        public Vector2 Origin; // Relative to Local Pos
        public Vector2 Anchor; // Relative to Screen Pos

        #region CONSTRUCTORS
        /// <summary>
        /// Generates UI element with parameters passed
        /// </summary>
        /// <param name="position">Position of Element</param>
        /// <param name="color">Color of UI</param>
        /// <param name="texture">Optional texture</param>
        public UIElement(
            Vector2 position,
            Color? color,
            Texture2D texture = null) :
            base(position, texture, color)
        { }

        /// <summary>
        /// Generates UI element with parameters passed
        /// </summary>
        /// <param name="origin">Position of Element</param>
        /// <param name="color">Color of UI</param>
        /// <param name="texture">Optional texture</param>
        public UIElement(
            Global.Vertex origin,
            Color? color,
            Texture2D texture = null) :
            base(GetPosFromVertex(origin), texture, color)
        { }
        #endregion
    }
}
