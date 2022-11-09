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
        public enum Vertex
        {
            NONE,
            TOP,
            TOP_RIGHT,
            RIGHT,
            BOTTOM_RIGHT,
            BOTTOM,
            BOTTOM_LEFT,
            LEFT,
            TOP_LEFT,
            CENTER
        }
        public Vertex Origin; // Relative to Local Pos
        public Vertex Anchor; // Relative to Screen Pos

        public UIElement(Game game,
            SpriteBatch spriteBatch,
            Vector2 position) : 
            base(game, spriteBatch, position)
        {
        }
    }
}
