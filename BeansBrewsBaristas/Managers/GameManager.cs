using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using SharpDX.DXGI;
using BeansBrewsBaristas.Content.scripts;
using System.Linq;
using System.Linq.Expressions;
using System;
using SharpDX.MediaFoundation;
using Microsoft.Xna.Framework.Media;
using BeansBrewsBaristas.ComponentScripts;
using BeansBrewsBaristas.BaseClassScripts;
using System.Threading;

namespace BeansBrewsBaristas.Managers
{
    public class GameManager : Game
    {
        private const string GAME_TITLE = "Brews, Beans, Baristas!";
        private const string GAME_VER = "0.02";
        public Point mousePosition;
        private GraphicsDeviceManager _graphics;
        public static string volume = "25%";
        public static Dictionary<Keys, object> modificationKeys;

        SpriteFont Font;
        Texture2D Instructions;
        Texture2D ReceiptBackground;

        public GameManager()
        {
            Window.Title = $"{GAME_TITLE} | v{GAME_VER}";

            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Dictionary<Keys, object> modificationKeys = new Dictionary<Keys, object>();

            foreach (var key in Enum.GetValues(typeof(ModificationManager.AddinControls)))
            {
                modificationKeys.Add((Keys)key, key);
            }
            foreach (var key in Enum.GetValues(typeof(ModificationManager.BaseControls)))
            {
                modificationKeys.Add((Keys)key, key);
            }

            foreach (var key in Enum.GetValues(typeof(ModificationManager.CupControls)))
            {
                modificationKeys.Add((Keys)key, key);
            }
            foreach (var key in Enum.GetValues(typeof(ModificationManager.TakeoutControls)))
            {
                modificationKeys.Add((Keys)key, key);
            }

            // List of mofication keys - compared when pressed in InputManager
            //modificationKeys = new List<Keys>();
            //foreach (var key in Enum.GetValues(typeof(ModificationManager.AddinControls)))
            //    modificationKeys.Add((Keys)key);
            //foreach (var key in Enum.GetValues(typeof(ModificationManager.BaseControls)))
            //    modificationKeys.Add((Keys)key);
            //foreach (var key in Enum.GetValues(typeof(ModificationManager.CupControls)))
            //    modificationKeys.Add((Keys)key);
            //foreach (var key in Enum.GetValues(typeof(ModificationManager.TakeoutControls)))
            //    modificationKeys.Add((Keys)key);

            foreach (var key in modificationKeys)
                Debug.WriteLine(key.ToString());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Font = Content.Load<SpriteFont>("Fonts/TextFont");

            Instructions = Content.Load<Texture2D>("Images/Controls");
            ReceiptBackground = Content.Load<Texture2D>("Images/Receipt");

            // Define globals
            Global.Stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Global.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Global.GameManager = this;

            // Init Singletons
            AudioManager.GetInstance();
            SceneManager.GetInstance();
            CustomerManager.GetInstance();

            SceneManager.LoadScene("Menu");

            Components.Add(Global.InputManager = new InputManager(this));
            Components.Add(new Debug(this));
        }

        protected override void Update(GameTime gameTime)
        {
            
            //right now this is where audio is being updated for the options menu (preferably put this in the update call in the audiomanager)
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
                    SceneManager.soundEffectTex.Text = $"Effects Volume: {volume}";


                }
                else if (musicVolumeRect.Contains(mousePosition))
                {
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

            if (CustomerManager.Orders.Count > 0)
            {
                if(CustomerManager.PickupQueue.Count > 0)
                {
                    Customer cust = CustomerManager.PickupQueue.ToList()[0];

                    // Order
                    Global.SpriteBatch.Draw(ReceiptBackground, new Vector2(-10, Global.Stage.Y / 2), Color.White);
                    Global.SpriteBatch.DrawString(
                        Font, cust.ToString(),
                        new Vector2(
                            10,
                            Global.Stage.Y / 2 + 15
                        ), Color.Black
                    );
                }

                #region CONTROLS_UI
                string cupControls = "" +
                    "Cup:\n" +
                    "1 - Coffee\n" +
                    "2 - Lattee\n" +
                    "3 - Espresso\n" +
                    "4 - Takeout\n";

                string baseControls = "" +
                    "Base:\n" +
                    "C - Coffee\n" +
                    "E - Espresso\n" +
                    "M - Steamed Milk\n" +
                    "B - Creamer";

                string addInControls = "" +
                    "Add-ins:\n" +
                    "R - Caramel\n" +
                    "H - Hazelnut\n" +
                    "T - Toffee\n" +
                    "V - Vanilla\n" +
                    "A - Cinn. Powder";

                string takeoutControls = "" +
                    "Takeout:\n" +
                    "S - Sleeve\n" +
                    "L - Lid";

                // Controls
                Global.SpriteBatch.Draw(Instructions, new Vector2(
                        Global.Stage.X / 10 * 6 - Font.MeasureString(cupControls).X - 45,
                        Global.Stage.Y - Font.MeasureString(addInControls).Y - 30
                    ),
                    Color.White
                );

                Global.SpriteBatch.DrawString(
                    Font, cupControls,
                    new Vector2(
                        Global.Stage.X / 10 * 6 - Font.MeasureString(cupControls).X - 35,
                        Global.Stage.Y - Font.MeasureString(addInControls).Y - 20
                    ),
                    Color.Black
                );

                Global.SpriteBatch.DrawString(
                    Font, baseControls,
                    new Vector2(
                        Global.Stage.X / 10 * 7 - Font.MeasureString(baseControls).X,
                        Global.Stage.Y - Font.MeasureString(addInControls).Y - 20
                    ),
                    Color.Black
                );

                Global.SpriteBatch.DrawString(
                    Font, addInControls,
                    new Vector2(
                        Global.Stage.X / 10 * 8 - Font.MeasureString(addInControls).X + 25,
                        Global.Stage.Y - Font.MeasureString(addInControls).Y - 20
                    ),
                    Color.Black
                );

                Global.SpriteBatch.DrawString(
                    Font, takeoutControls,
                    new Vector2(
                        Global.Stage.X / 10 * 9 - Font.MeasureString(takeoutControls).X + 25,
                        Global.Stage.Y - Font.MeasureString(addInControls).Y - 20
                    ),
                    Color.Black
                );
                #endregion
            }

            Global.SpriteBatch.End();
        }
    }
}