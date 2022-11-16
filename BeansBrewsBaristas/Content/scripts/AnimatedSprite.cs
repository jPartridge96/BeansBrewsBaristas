using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System.Collections.Generic;


namespace BeansBrewsBaristas.Content.scripts
{
    
    public class AnimatedSprite : Sprite
    {
        public Vector2 dimension { get; set; }
        public List<Rectangle> frames { get; set; }
        public int frameIndex { get; set; } = -1;
        public int delay { get; set; }
        public float delayCounter { get; set; }

        byte previousAnimation;
        byte currentAnimation;



        Dictionary<string, Rectangle[]> Animations = new Dictionary<string, Rectangle[]>();

        string walkDirection = "walk_up";


        public AnimatedSprite(Vector2 position,
            Texture2D texture,
            Color? color,
            int delay) : base(position, texture, color)
        {
            ///
            ///These will be the coordinates for the animation dictionary
            ///just need to set these to their kvp and they will be ready to rip.
            ///

            ////down coords
            //sourceRectangles = new Rectangle[4];
            //sourceRectangles[0] = new Rectangle(0, 0, 48, 48);
            //sourceRectangles[1] = new Rectangle(0, 48, 48, 48);
            //sourceRectangles[2] = new Rectangle(0, 96, 48, 48);
            //sourceRectangles[3] = new Rectangle(0, 142, 48, 48);
            #region Walking coordinates
            //right coords
            //sourceRectangles = new Rectangle[4];
            //sourceRectangles[0] = new Rectangle(142, 0, 48, 48);
            //sourceRectangles[1] = new Rectangle(142, 48, 48, 48);
            //sourceRectangles[2] = new Rectangle(142, 96, 48, 48);
            //sourceRectangles[3] = new Rectangle(142, 142, 48, 48); 
            ////Left coords
            //sourceRectangles = new Rectangle[4];
            //sourceRectangles[0] = new Rectangle(48, 0, 48, 48);
            //sourceRectangles[1] = new Rectangle(48, 48, 48, 48);
            //sourceRectangles[2] = new Rectangle(48, 96, 48, 48);
            //sourceRectangles[3] = new Rectangle(48, 142, 48, 48);
            ////up coords
            //sourceRectangles = new Rectangle[4];
            //sourceRectangles[0] = new Rectangle(96, 0, 48, 48);
            //sourceRectangles[1] = new Rectangle(96, 48, 48, 48);
            //sourceRectangles[2] = new Rectangle(96, 96, 48, 48);
            //sourceRectangles[3] = new Rectangle(96, 142, 48, 48);
            #endregion

            Animations["walk_right"] = new Rectangle[4] 
            {
                new Rectangle(142, 0, 48, 48),
                new Rectangle(142, 48, 48, 48),
                new Rectangle(142, 96, 48, 48),
                new Rectangle(142, 142, 48, 48) 
            };

            Animations["walk_left"] = new Rectangle[4]
            {
               new Rectangle(48, 0, 48, 48),
               new Rectangle(48, 48, 48, 48),
               new Rectangle(48, 96, 48, 48),
               new Rectangle(48, 142, 48, 48)
            };

            Animations["walk_up"] = new Rectangle[4]
            {
                new Rectangle(96, 0, 48, 48),
                new Rectangle(96, 48, 48, 48),
                new Rectangle(96, 96, 48, 48),
                new Rectangle(96, 142, 48, 48)
            };

            Animations["walk_down"] = new Rectangle[4]
            {
                new Rectangle(0, 0, 48, 48),
                new Rectangle(0, 48, 48, 48),
                new Rectangle(0, 96, 48, 48),
                new Rectangle(0, 142, 48, 48)
            };
            Animations["idle"] = new Rectangle[4]
            {
                new Rectangle(0, 0, 48, 48),
                new Rectangle(0, 0, 48, 48),
                new Rectangle(0, 0, 48, 48),
                new Rectangle(0, 0, 48, 48)
            };

            this.delay = delay;
            previousAnimation = 2;
            currentAnimation = 1;


        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            walkDirection = "idle";
            if (ks.IsKeyDown(Keys.S))
            {
                walkDirection = "walk_down";
            }
            if (ks.IsKeyDown(Keys.W))
            {
                walkDirection = "walk_up";
            }
            if (ks.IsKeyDown(Keys.D))
            {
                walkDirection = "walk_right";
            }
            if (ks.IsKeyDown(Keys.A))
            {
                walkDirection = "walk_left";
            }
            

            

            delayCounter++;
            if(delayCounter > delay)
            {
                if(currentAnimation == 1)
                {
                    if(previousAnimation == 0)
                    {
                        currentAnimation = 2;
                    }
                    else
                    {
                        currentAnimation = 0;
                    }
                    previousAnimation = currentAnimation;
                }
                else
                {
                    currentAnimation = 1;
                }
                delayCounter = 0;
            }
            else
            {
                delayCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            //dropdown 4
            SpriteBatch.Draw(Texture, new Vector2(150, 150), Animations[walkDirection][currentAnimation], Color.White);
            SpriteBatch.End();
        }


    }
}
