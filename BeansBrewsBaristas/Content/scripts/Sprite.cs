using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BeansBrewsBaristas.Content.scripts
{
    public class Sprite : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get => Global.SpriteBatch; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Color? SpriteColor { get; set; }

        #region CONSTRUCTORS
        public Sprite(Vector2 position, 
            Texture2D texture,
            Color? color) : base(Global.GameManager)
        {
            Position = position;
            Texture = texture;
            SpriteColor = color;
        }
        #endregion

        /// <summary>
        /// Draws the Sprite to the Game Window
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (Texture != null)
            {
                SpriteBatch.Begin();
                SpriteBatch.Draw(Texture, Position, SpriteColor ?? Color.White);
                SpriteBatch.End();
            }

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

        public static Vector2 GetPosFromVertex(Global.Vertex origin)
        {
            switch (origin)
            {
                case Global.Vertex.TOP:
                    return new Vector2(0, Global.Stage.Y / 2);
                case Global.Vertex.TOP_RIGHT:
                    return new Vector2(Global.Stage.X, 0);
                case Global.Vertex.RIGHT:
                    return new Vector2(Global.Stage.X, Global.Stage.Y / 2);
                case Global.Vertex.BOTTOM_RIGHT:
                    return new Vector2(Global.Stage.X, Global.Stage.Y);
                case Global.Vertex.BOTTOM:
                    return new Vector2(Global.Stage.X / 2, Global.Stage.Y);
                case Global.Vertex.BOTTOM_LEFT:
                    return new Vector2(0, Global.Stage.Y);
                case Global.Vertex.LEFT:
                    return new Vector2(0, Global.Stage.Y / 2);
                case Global.Vertex.TOP_LEFT:
                    return Vector2.Zero;
                case Global.Vertex.CENTER:
                    return new Vector2(Global.Stage.X / 2, Global.Stage.Y / 2);
                default:
                case Global.Vertex.NONE:
                    throw new Exception("No vertex selected. Use Vector2 or change vertex type.");
            }
        }
    }
}
