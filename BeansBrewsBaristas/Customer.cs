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

        public override void Update(GameTime gameTime)
        {
            if (_frameIndex >= 0)
            {
                if (Position.Y < Global.Stage.Y - _frames[_frameIndex].Height && WaitTimer <= 0)
                    Position += new Vector2(Position.X, 2);
                else if (Position.Y >= Global.Stage.Y - _frames[_frameIndex].Height)
                    _isPaused = true;
            }
            TravelToPos(new Vector2(450, 300));
            #region Unused Code for right now.
            // else _isPaused = true;

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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            if (_frameIndex >= 0) // v4
                SpriteBatch.Draw(Texture, Position, _frames[_frameIndex], Color.White);

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void TravelToPos(Vector2 pos)
        {
            //if(Position != pos)
            //{
            //    Position = new Vector2(pos.X, pos.Y);
            //}

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
