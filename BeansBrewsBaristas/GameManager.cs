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
        private const string GAME_VER = "0.02";
        public Point mousePosition;
        private GraphicsDeviceManager _graphics;
        public static string volume = "25%";

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
            Rectangle soundFxRect = new Rectangle((int)SceneManager.soundEffectSprite.Position.X, (int)SceneManager.soundEffectSprite.Position.Y, SceneManager.VolumeTex.Width, SceneManager.VolumeTex.Height);
            Rectangle musicVolumeRect = new Rectangle((int)SceneManager.volumeSprite.Position.X, (int)SceneManager.volumeSprite.Position.Y, SceneManager.VolumeTex.Width, SceneManager.VolumeTex.Height);
            //check to see if inside the rectangle for the volume slider then get the mouse position relative to screen (not sure how to get relative to the box ask Jordan)
            if (SceneManager.ActiveScene == "Options")
            {
                if (soundFxRect.Contains(mousePosition))
                {
                    //180 is the total difference 
                    switch (mousePosition.X)
                    {
                        case < 395:
                            volume = "0%";
                            MediaPlayer.Volume = 0f;
                            break;
                        case < 425:
                            volume = "25%";
                            MediaPlayer.Volume = .25f;
                            break;
                        case < 480:
                            volume = "50%";
                            MediaPlayer.Volume = .50f;
                            break;
                        case < 525:
                            volume = "75%";
                            MediaPlayer.Volume = .75f;
                            break;
                        case < 600:
                            volume = "100%";
                            MediaPlayer.Volume = 1;
                            break;
                        default:
                            break;
                    }
                    SceneManager.soundEffectTex.Text = $"Effects Volume: {volume}";


                }
                else if (musicVolumeRect.Contains(mousePosition))
                {
                    switch (mousePosition.X)
                    {
                        case < 395:
                            volume = "0%";
                            SoundEffect.MasterVolume = 0f;
                            break;
                        case < 425:
                            volume = "25%";
                            SoundEffect.MasterVolume = .25f;
                            break;
                        case < 480:
                            volume = "50%";
                            SoundEffect.MasterVolume = .50f;
                            break;
                        case < 525:
                            volume = "75%";
                            SoundEffect.MasterVolume = .75f;
                            break;
                        case < 600:
                            volume = "100%";
                            SoundEffect.MasterVolume = 1;
                            break;
                        default:
                            break;
                    }
                    SceneManager.musicVolumeTex.Text = $"Music Volume: {volume}";
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            Global.SpriteBatch.Begin();

            // Fixes layering for Customers if Customers are present
            if (CustomerManager.Customers.Count > 0)
            {
                for (int i = CustomerManager.Customers.Count - 1; i >= 0; i--)
                {
                    Customer cust = CustomerManager.Customers[i];
                    Global.SpriteBatch.Draw(cust.Texture, cust.Position, Color.White);
                }
            }

            Global.SpriteBatch.End();
        }
    }
}