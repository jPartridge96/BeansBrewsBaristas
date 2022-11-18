using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System.Collections.Generic;


namespace BeansBrewsBaristas.Content.scripts
{
    
    public class AnimatedSprite : Sprite
    {
        const int ROWS = 4;
        const int COLS = 4;

        public Vector2 Dimension { get; set; } // 48*48

        public AnimatedSprite(Vector2 position,
            Texture2D texture,
            Color? color,
            int delay) : base(position, texture, color)
        {
            Dimension = new Vector2(texture.Width / COLS, texture.Height / ROWS);
            Position = position;
            _delay = delay;

            // Hide();
            CreateFrames();
        }

        private void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        private void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public void Restart()
        {
            _frameIndex = 0;
            _delayCounter = 0;
            Show();
        }

        private void CreateFrames()
        {
            _frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)Dimension.X;
                    int y = i * (int)Dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)Dimension.X, (int)Dimension.Y);
                    _frames.Add(r);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            _delayCounter++;
            if (_isPaused == false)
            {
                if (_delayCounter > _delay)
                {
                    _frameIndex++;

                    // Animation ended
                    if (_frameIndex > ROWS * COLS - 1)
                    {
                        // _frameIndex = -1;
                        // Hide();
                        Restart();
                    }
                    _delayCounter = 0;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            //dropdown 4
            if (_frameIndex >= 0)
                SpriteBatch.Draw(Texture, Position, _frames[_frameIndex], Color.White);

            SpriteBatch.End();
        }

        protected List<Rectangle> _frames;
        protected int _frameIndex = -1;
        protected bool _isPaused = false;
        private int _delay { get; set; }
        private float _delayCounter { get; set; }
    }
}
