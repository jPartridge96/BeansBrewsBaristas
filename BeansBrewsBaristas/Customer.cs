using BeansBrewsBaristas.Content.scripts;
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


        public int patienceTimer { get; set; }
        public int waitTimer { get; set; }
        public Customer(Vector2 position, Texture2D texture, Color color, int patienceTimer, int waitTimer) : base(position, texture, color)
        {
            this.patienceTimer = patienceTimer;
            this.waitTimer = waitTimer;
        }

        public override void Update(GameTime gameTime)
        {
            

            if(this.Position.Y < GameManager.customerSpawn.Bottom && this.waitTimer <= 0)
            {
                this.Position += new Vector2(this.Position.X, 2);
            }
            switch (this.patienceTimer)
            {
                case > 500:
                    this.color = Color.Green;
                    break;
                case > 250:
                    this.color = Color.Yellow;
                    break;
                case < 100:
                    this.color = Color.Red;
                    break;
                default:
                    break;
            }
            this.waitTimer--;
            this.patienceTimer--;

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {

            base.LoadContent();
        }
    }
}
