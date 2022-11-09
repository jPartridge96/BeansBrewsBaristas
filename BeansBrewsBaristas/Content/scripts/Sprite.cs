using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeansBrewsBaristas.Content.scripts
{
    public class Sprite : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }

        public Sprite(Game game,
            SpriteBatch spriteBatch,
            Vector2 position,
            Texture2D texture = null) : 
            base(game)
        {
            SpriteBatch = spriteBatch;
            Position = position;
            Texture = texture;
        }

        /// <summary>
        /// Draws the Sprite to the Game Window
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the perimeter of the Sprite as a Rectangle
        /// </summary>
        /// <returns>A Rectangle of the Sprite boundary</returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}
