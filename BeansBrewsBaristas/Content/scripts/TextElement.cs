using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Content.scripts
{
    public class TextElement : UIElement
    {
        SpriteFont textFont = Global.GameManager.Content.Load<SpriteFont>("fonts/DebugFont");

        public string Text { get; set; }
        public Color Color { get; set; } 

        public TextElement(
            string text,
            Color color,
            Vector2 position,
            Texture2D texture = null) : 
            base(position, texture)
        {
            Text = text;
            Color = color;
        }

        public TextElement(
            string text,
            Color color,
            Vertex origin,
            Texture2D texture = null) :
            base(GetPosFromOrigin(origin), texture)
        {
            Text = text;
            Color = color;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(textFont, Text, Position, Color);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public static Vector2 GetPosFromOrigin()
        {
            return Vector2.Zero;
        }
    }
}
