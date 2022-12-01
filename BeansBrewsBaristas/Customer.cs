using BeansBrewsBaristas.Content.scripts;
using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

            //customer travels to this position (need to update to reflect a queue in an array)
            TravelToPos(line1.Dequeue());

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
        /// new position for customer to travel to specified in their update call
        /// </summary>
        /// <param name="pos">position in the line will be assigned by their index in the queue</param>
        public void TravelToPos(Vector2 pos)
        {
            //we want the customer to go in a straight line across the screen queue will have to be horizontal
            while(Position.X != pos.X)
            {
                Position = new Vector2(Position.X + 1, Position.Y);
                break;
            }

            //// Aync? Hopefully code will execute while this processes
            //Task.Run(() =>
            //{
            //    while (Position != pos)
            //    {
            //        // y than x
            //        // Travels to Vector2 position passed into method.
            //    }
            //});
        }


    }
}
