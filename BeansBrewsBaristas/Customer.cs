using BeansBrewsBaristas.Content.scripts;
using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas
{
    public class Customer : AnimatedSprite
    {
        public int PatienceTimer { get; set; }
        public int WaitTimer { get; set; }

        public Queue<Vector2> linePosition;
        public Vector2[] linePositions = new Vector2[] { new Vector2(450, 300), new Vector2(450, 350) };
        Queue<Vector2> line1;

        #region CONSTRUCTORS
        public Customer(Vector2 position,
            Texture2D texture,
            Color color,
            int patienceTimer,
            int waitTimer) :
            base(position, texture, color, 2)
        {
            PatienceTimer = patienceTimer;
            WaitTimer = waitTimer;
        }
        #endregion
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            line1 = new Queue<Vector2>(linePositions);
            base.Update(gameTime);

            //unused code region.
            #region Unused Code for right now.
            // else _isPaused = true;

            //if (_frameIndex >= 0)
            //{
            //    if (Position.Y < Global.Stage.Y - _frames[_frameIndex].Height && WaitTimer <= 0)
            //        Position += new Vector2(Position.X, 2);
            //    else if (Position.Y >= Global.Stage.Y - _frames[_frameIndex].Height)
            //        _isPaused = true;
            //}
            //this switch can be used to make patience indicator above customers head... however, it is not useful right now.
            //switch (PatienceTimer)
            //{
            //    case > 500:
            //        SpriteColor = Color.Green;
            //        break;
            //    case > 250:
            //        SpriteColor = Color.Yellow;
            //        break;
            //    case < 100:
            //        SpriteColor = Color.Red;
            //        break;
            //    default:
            //        break;
            //}
            //WaitTimer--;
            //PatienceTimer--; 
            #endregion
        }

        public override void Draw(GameTime gameTime)
        {

            SpriteBatch.Begin();

            if (_frameIndex >= 0) // v4
                SpriteBatch.Draw(Texture, Position, _frames[_frameIndex], Color.White);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Customer travels async towards a specified coordinate
        /// </summary>
        /// <param name="orderPos">Position for Customer to travel towards</param>
        /// <returns>Completed Async Task</returns>
        public async Task TravelToPos(Vector2 orderPos)
        {
            while (Position.X <= orderPos.X)
            {
                Position = new Vector2(Position.X + 1, Position.Y);
                await Task.Delay(1);
            }
        }
    }
}
