using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
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

        public UIElement(Vector2 position, 
            Color? color, 
            Texture2D texture = null) : base(position, texture, color) { }

        public UIElement(Vertex origin,
            Color? color,
            Texture2D texture = null) : base(GetPosFromOrigin(origin), texture, color) { }

        public static Vector2 GetPosFromOrigin(Vertex origin)
        {
            switch (origin)
            {
                case Vertex.NONE:
                    break;
                case Vertex.TOP:
                    break;
                case Vertex.TOP_RIGHT:
                    break;
                case Vertex.RIGHT:
                    break;
                case Vertex.BOTTOM_RIGHT:
                    break;
                case Vertex.BOTTOM:
                    break;
                case Vertex.BOTTOM_LEFT:
                    break;
                case Vertex.LEFT:
                    break;
                case Vertex.TOP_LEFT:
                    break;
                case Vertex.CENTER:
                    break;
            }
            return new Vector2(0, 0);
        }
    }
}
