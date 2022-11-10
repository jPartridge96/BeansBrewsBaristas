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

        public TextElement(
            string text,
            Vector2 position,
            Color? color = null,
            Texture2D texture = null) :
            base(position, color, texture)
        {
            Text = text;
            SpriteColor = color;
        }

        public TextElement(
            string text,
            Vertex origin,
            Color? color = null,
            Texture2D texture = null) : 
            base(GetPosFromOrigin(origin), color, texture)
        {
            Text = text;
            SpriteColor = color;
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(textFont, Text, Position, SpriteColor ?? Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public static Vector2 GetPosFromOrigin()
        {
            return Vector2.Zero;
        }
    }
}
