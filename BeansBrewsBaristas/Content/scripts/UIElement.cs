﻿using Microsoft.Xna.Framework;
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

        public UIElement(Vector2 position, Texture2D texture = null) : base(position, texture) { }

        public UIElement(Vertex origin, Texture2D texture = null) : base(GetPosFromOrigin(origin), texture) { }


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
