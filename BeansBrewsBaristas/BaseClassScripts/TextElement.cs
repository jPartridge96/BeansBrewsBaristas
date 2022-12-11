using BeansBrewsBaristas.BaseClassScripts;
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
    public class TextElement : UIElement
    {
        public string Text { get; set; }
        SpriteFont Font = Global.GameManager.Content.Load<SpriteFont>("Fonts/Font");

        #region CONSTRUCTORS
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
            Global.Vertex origin,
            Color? color = null,
            Texture2D texture = null) : 
            base(GetPosFromVertex(origin), color, texture)
        {
            Text = text;
            SpriteColor = color;
        }
        #endregion

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(Font ?? Debug.Font, Text, Position, SpriteColor ?? Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the perimeter of the Text as a Rectangle
        /// </summary>
        /// <returns>A Rectangle of the Text boundary</returns>
        public Rectangle GetTextBounds()
        {
            Vector2 textLen = Debug.Font.MeasureString(Text);
            return new Rectangle(new Point((int)Position.X, (int)Position.Y), new Point((int)textLen.X, (int)textLen.Y));
        }
    }
}
