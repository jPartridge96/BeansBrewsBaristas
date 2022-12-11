using BeansBrewsBaristas.Content.scripts;
using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.BaseClassScripts
{

    public class Debug : DrawableGameComponent
    {
        public static SpriteFont Font = Global.GameManager.Content.Load<SpriteFont>("fonts/DebugFont");

        public Debug(Game game) : base(game)
        {
            // Used by Debug.Output();
            Global.GameManager.Components.Add(
                _debugMessage = new TextElement(
                    "", Global.Vertex.TOP_LEFT,
                    Color.Red
            ));
        }

        public override void Draw(GameTime gameTime)
        {
            Global.SpriteBatch.Begin();

            // DrawRectangle(CustomerManager.SpawnPoint);

            Global.SpriteBatch.End();

            base.Draw(gameTime);
        }

        #region public static void DrawRectangle()
        /// <summary>
        /// Draws a colored Rectangle at the position passed
        /// </summary>
        /// <param name="x">X position of the Rectangle</param>
        /// <param name="y">Y position of the Rectangle</param>
        /// <param name="xLen">Width of the Rectangle</param>
        /// <param name="yLen">Height of the Rectangle</param>
        /// <param name="color">Color of the Rectangle</param>
        public static void DrawRectangle(int x, int y, int xLen, int yLen, Color? color = null)
        {
            Rectangle rect = new Rectangle(x, y, xLen, yLen);
            Texture2D tex = new Texture2D(Global.GameManager.GraphicsDevice, 1, 1);
            tex.SetData(new[] { Color.White });

            Global.SpriteBatch.Draw(tex, rect, color ?? Color.White);
        }

        /// <summary>
        /// Draws a colored Rectangle at the position passed
        /// </summary>
        /// <param name="rect">The position of the Rectangle</param>
        /// <param name="color">The color of the Rectangle</param>
        public static void DrawRectangle(Rectangle rect, Color? color = null)
        {
            Texture2D tex = new Texture2D(Global.GameManager.GraphicsDevice, 1, 1);
            tex.SetData(new[] { Color.White });

            Global.SpriteBatch.Draw(tex, rect, color ?? Color.White);
        }
        #endregion

        /// <summary>
        /// Outputs text to the game window
        /// </summary>
        /// <param name="text">The text to be displayed</param>
        public static void Output(string text)
        {
            _debugMessage.Text = text;
        }
        static TextElement _debugMessage;

        public static void WriteLine(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }
    }
}
