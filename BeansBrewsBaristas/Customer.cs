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
    public class Customer : Sprite
    {
        public string Name { get; }
        public int PatienceTimer { get; set; }
        public int WaitTimer { get; set; }
        public Order Order { get; set; }

        #region CONSTRUCTORS
        public Customer(Vector2 position,
            Texture2D texture,
            Color color,
            int patienceTimer,
            int waitTimer) :
            base(position, texture, color)
        {
            PatienceTimer = patienceTimer;
            WaitTimer = waitTimer;

            Name = CustomerManager.GetCustomerName();
            Order = new Order();

            // Eventually use this so only specifictoppings pair with their related drink types
            // Order = CustomerManager.GetCustomerOrder();

        }
        #endregion

        public override void Update(GameTime gameTime)
        {
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

            SpriteBatch.Draw(Texture, Position, Color.White);

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

        public override string ToString()
        {

            return $"{Name} {Order}";
        }
    }
}
