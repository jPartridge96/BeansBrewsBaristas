using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using SharpDX.DXGI;
using BeansBrewsBaristas.Content.scripts;
using System.Linq;
using BeansBrewsBaristas.Managers;
using System.Linq.Expressions;
using System;
using SharpDX.MediaFoundation;
using Microsoft.Xna.Framework.Media;

namespace BeansBrewsBaristas
{
    public class GameManager : Game
    {
        private const string GAME_TITLE = "Brews, Beans, Baristas!";
        private const string GAME_VER = "0.01";
        public Point mousePosition;
        private GraphicsDeviceManager _graphics;
        public static string volume = "Volume 25%";

        public GameManager()
        {
            Window.Title = $"{GAME_TITLE} | v{GAME_VER}";

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Define globals
            Global.Stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Global.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Global.GameManager = this;

            // Init Singletons
            AudioManager.GetInstance();
            SceneManager.GetInstance();
            CustomerManager.GetInstance();

            SceneManager.LoadScene("Menu");

            this.Components.Add(Global.InputManager = new InputManager(this));
            this.Components.Add(new Debug(this));
        }

        protected override void Update(GameTime gameTime)
        {
            mousePosition = new Point(InputManager._msState.X, InputManager._msState.Y);
            Rectangle rectangle = new Rectangle((int)SceneManager.testVolumeSprite.Position.X, (int)SceneManager.testVolumeSprite.Position.Y, SceneManager.VolumeTex.Width, SceneManager.VolumeTex.Height);
            //check to see if inside the rectangle for the volume slider then get the mouse position relative to screen (not sure how to get relative to the box ask Jordan)
            if (SceneManager.ActiveScene == "Options")
            {
                if (rectangle.Contains(mousePosition))
                {
                    //180 is the total difference 
                    //need to change this into a switch (these are super magic numbers. oof.
                    if (mousePosition.X < 395)
                    {
                        volume = "Volume 0%";
                        SceneManager.banana.Text = "Volume 0%";
                        MediaPlayer.Volume = 0f;
                    }
                    if (mousePosition.X < 440 && mousePosition.X > 395)
                    {
                        volume = "Volume 25%";

                        MediaPlayer.Volume = .25f;
                    }
                    if (mousePosition.X < 485 && mousePosition.X > 440)
                    {
                        volume = "Volume 50%";
                        MediaPlayer.Volume = .50f;
                    }
                    if (mousePosition.X < 525 && mousePosition.X > 485)
                    {
                        volume = "Volume 75%";
                        MediaPlayer.Volume = .75f;
                    }
                    else if (mousePosition.X > 525)
                    {
                        volume = "Volume 100%";
                        MediaPlayer.Volume = 1;
                    }
                }
                SceneManager.banana.Text = volume;
            }




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Global.SpriteBatch.Begin();

            #region DEBUG RECTANGLES

            //Debug.DrawRectangle(
            //    (int)(Global.Stage.X / 8) * 2, 0,
            //    (int)(Global.Stage.X / 8),
            //    (int)Global.Stage.Y
            //);

            //Debug.DrawRectangle(
            //    (int)(Global.Stage.X / 8) * 5, 0,
            //    (int)(Global.Stage.X / 8),
            //    (int)Global.Stage.Y
            //);

            #endregion

            Global.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}