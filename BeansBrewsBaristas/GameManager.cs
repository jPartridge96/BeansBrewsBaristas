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

namespace BeansBrewsBaristas
{
    public class GameManager : Game
    {
        private const string GAME_TITLE = "Brews, Beans, Baristas!";
        private const string GAME_VER = "0.01";

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

            AudioManager.PlaySound("MenuTheme");
            SceneManager.LoadScene("Level1");

            this.Components.Add(new InputManager(this));
            this.Components.Add(new Debug(this));
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Global.SpriteBatch.Begin();

            #region DEBUG RECTANGLES

            Debug.DrawRectangle(
                (int)(Global.Stage.X / 8) * 2, 0,
                (int)(Global.Stage.X / 8),
                (int)Global.Stage.Y
            );

            Debug.DrawRectangle(
                (int)(Global.Stage.X / 8) * 5, 0,
                (int)(Global.Stage.X / 8),
                (int)Global.Stage.Y
            );

            #endregion

            Global.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}