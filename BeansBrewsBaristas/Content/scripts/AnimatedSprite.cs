using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Content.scripts
{
    public class AnimatedSprite : Sprite
    {
        public Texture2D[] Frames { get; set; } // Sprite atlas
        public int SampleRate { get; set; } // How fast should frames be switching?
        public bool IsAnimating { get; set; } // Currently animating?

        public AnimatedSprite(Vector2 position, Texture2D texture, Color color) : base(position, texture, color)
        {
        }

        public override void Update(GameTime gameTime)
        {
            _sampleCounter++;
            if(_sampleCounter > SampleRate)
            {
                if (_frameCounter >= Frames.Length)
                    _frameCounter = 0;
                else
                    _frameCounter++;

                Texture = Frames[_frameCounter];
                _sampleCounter = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(IsAnimating)
                base.Draw(gameTime);
        }

        public int _frameCounter = 0;
        private int _sampleCounter = 0;


    }
}
