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
        public Customer(Vector2 position, Texture2D texture, int patienceTimer) : base(position, texture)
        {
            this.patienceTimer = patienceTimer;
        }

        public override void Update(GameTime gameTime)
        {

            if(this.Position.Y < GameManager.customerSpawn.Bottom && this.patienceTimer <= 0)
            {
                this.Position += new Vector2(this.Position.X, 2);
            }
            this.patienceTimer--;

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {

            base.LoadContent();
        }
    }
}
