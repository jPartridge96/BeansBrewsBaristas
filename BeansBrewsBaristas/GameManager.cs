using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using SharpDX.DXGI;
using BeansBrewsBaristas.Content.scripts;
using System.Linq;

namespace BeansBrewsBaristas
{
    
    public class GameManager : Game
    {
        private const string GAME_TITLE = "Brews, Beans, Baristas!";
        private const string GAME_VER = "0.01";

        bool isMap2 = false;
        List<DrawableGameComponent> _map1;
        List<DrawableGameComponent> _map2;

        private GraphicsDeviceManager _graphics;

        public GameManager()
        {
            Window.Title = $"{GAME_TITLE} | v{GAME_VER}";

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Global.SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Define globals
            Global.GameManager = this;
            Global.AudioManager = AudioManager.GetInstance();
            Global.Stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Global.AudioManager.PlaySound("menuTheme");

            // TODO: use this.Content to load your game content here
            _map1 = new List<DrawableGameComponent>()
            {
                new TextElement("Message", Color.LimeGreen, new Vector2(0, 0)),
                new TextElement("Second Message", Color.White, new Vector2(100,0))
            };
            _map2 = new List<DrawableGameComponent>()
            {
                new TextElement("Another Message", Color.LimeGreen, new Vector2(100, 0)),
                new TextElement("Third Message", Color.White, new Vector2(200,0))
            };

            foreach (DrawableGameComponent comp in _map1)
                this.Components.Add(comp);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if(Keyboard.GetState().IsKeyDown(Keys.K))
            {
                if(!isMap2)
                {
                    foreach (DrawableGameComponent map1 in _map1)
                    {
                        map1.Visible = false;
                        map1.Enabled = false;
                    }

                    foreach (DrawableGameComponent map2 in _map2)
                        this.Components.Add(map2);
                }
                isMap2 = true;
            }
                
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}