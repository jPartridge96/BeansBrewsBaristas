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
        public int PatienceTimer { get; set; }
        public int WaitTimer { get; set; }

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
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if(Position.Y < CustomerManager.SpawnPoint.Bottom && WaitTimer <= 0)
            {
                Position += new Vector2(Position.X, 2);
            }
            switch (PatienceTimer)
            {
                case > 500:
                    SpriteColor = Color.Green;
                    break;
                case > 250:
                    SpriteColor = Color.Yellow;
                    break;
                case < 100:
                    SpriteColor = Color.Red;
                    break;
                default:
                    break;
            }
            WaitTimer--;
            PatienceTimer--;

            base.Update(gameTime);
        }

        public void TravelToPos(Vector2 pos)
        {
            // Aync? Hopefully code will execute while this processes
            Task.Run(() =>
            {
                while (Position != pos)
                {
                    // Travels to Vector2 position passed into method.
                }
            });
        }
    }
}
