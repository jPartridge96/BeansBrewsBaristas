﻿using BeansBrewsBaristas.Content.scripts;
using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.ComponentScripts
{
    /// <summary>
    /// Carries a drink and will be served when becoming active
    /// </summary>
    public class Customer : Sprite
    {
        public string Name { get; }
        public int PatienceTimer { get; set; }
        public int WaitTimer { get; set; }
        public Order Order { get; set; }

        private Vector2 PosToTravelTo;

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a customer and generates a name and order for them
        /// </summary>
        /// <param name="position">Position on screen</param>
        /// <param name="texture">Skin of customer</param>
        /// <param name="color">Color of customer</param>
        /// <param name="patienceTimer">How much patience they have</param>
        /// <param name="waitTimer">How long theyve waited</param>
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
        /// <param name="travelPos">Position for Customer to travel towards</param>
        /// <returns>Completed Async Task</returns>
        public async Task TravelToPos(Vector2 travelPos)
        {
            PosToTravelTo = travelPos;
            // Move left or right?
            if (Position.X > PosToTravelTo.X)
            {
                // Repeat until reached position
                while (Position.X >= PosToTravelTo.X)
                {
                    Position = new Vector2(Position.X - 1, Position.Y);
                    await Task.Delay(1);
                }
            }
            else
            {
                // Repeat until reached position
                while (Position.X <= PosToTravelTo.X)
                {
                    Position = new Vector2(Position.X + 1, Position.Y);
                    await Task.Delay(1);
                }
            }
            
            // Once reached position, check for action
            if (Position.X >= PosToTravelTo.X)
                CheckForAction();
        }

        /// <summary>
        /// Customer checks if it is available to complete a task
        /// </summary>
        public async void CheckForAction()
        {
            // If Customer is next in line to order, wait 5 seconds and take the order
            if(CustomerManager.GetQueueIndex(this, CustomerManager.OrderQueue) == 0)
            {
                Random rand = new Random();
                await Task.Delay(rand.Next(3000, 5000));
                if(CustomerManager.PickupQueue.Count <= CustomerManager.QUEUE_LIMIT)
                CustomerManager.TakeNextOrder();
            }
        }

        /// <summary>
        /// Overridden string used when the customer is active
        /// </summary>
        /// <returns>Formated string including order</returns>
        public override string ToString()
        {
            return $"Item 1 of 1\n" +
                $"Items in order: 1\n" +
                $"*{Name}*\n" +
                $"{Order}";
        }
    }
}
