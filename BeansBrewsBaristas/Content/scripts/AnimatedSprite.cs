using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeansBrewsBaristas.Content.scripts
{
    public class AnimatedSprite : Sprite
    {
        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; } = -1;
        public int delay { get; set; }
        public int delayCounter { get; set; }

        private const int ROWS = 1;
        private const int COLS = 4;

        public AnimatedSprite(Vector2 position,
            Texture2D texture,
            Color? color,
            int delay) : base(position, texture, color)
        {
            this.delay = delay;
            dimension = new Vector2(Texture.Width / COLS, Texture.Height / ROWS);
            createFrames();
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y,
                        (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public void restart()
        {
            frameIndex = -1;
            delayCounter = 0;
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            if(frameIndex >= 0)
            {
                //dropdown 4
                SpriteBatch.Draw(Texture, Position, frames[frameIndex], Color.White);
            }
            SpriteBatch.End();
        }


    }
}
