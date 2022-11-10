﻿using Microsoft.Xna.Framework;
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
        //testing stuff to delete later
        Texture2D spawnArea;
        Vector2 coor;
        SpriteBatch _spriteBatch;

        //setting up the first rectangle and ball texture to see if i can get it to spawn within bounds of rectangle
        Rectangle customerSpawn;
        Sprite ball;
        Texture2D ballTex;
        int randomX;
        int randomY;
        Vector2 spawnPos;
        int customerCounter = 300;//5 seconds?



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
            Global.AudioManager = AudioManager.GetInstance();
            Global.SceneManager = SceneManager.GetInstance();
            Global.AudioManager.PlaySound("MenuTheme");
            Global.SceneManager.LoadScene("Level1");

            #region Testing spawn stuff
            //here temporarily just to draw the outline rectangle to practice ***TESTING***
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //create the rectangle
            customerSpawn = new Rectangle(0, (int)Global.Stage.Y / 3, 100, 200);
            ballTex = this.Content.Load<Texture2D>("Images/ball (1)");
            //generate random number for x
            Random rnd = new Random();
            for (int i = 0; i < 1; i++)
            {
                randomX = rnd.Next(customerSpawn.Width);
            }
            //generate random number for y
            for (int i = 0; i < 1; i++)
            {
                randomY = rnd.Next(customerSpawn.Height);
            }
            //create ball ***TESTING***
            spawnPos = new Vector2(0, randomY);
            ball = new Sprite(spawnPos, ballTex); 
            #endregion

            this.Components.Add(new InputManager(this));
            this.Components.Add(ball);
            
        }
        #region Rectangle Drawing tool
        //used for drawing rectangles just trying to find coordinates easier.
        private void DrawRectangle(Rectangle coords, Color color)
        {
            if (spawnArea == null)
            {
                spawnArea = new Texture2D(GraphicsDevice, 1, 1);
                spawnArea.SetData(new[] { Color.White });
            }
            _spriteBatch.Draw(spawnArea, coords, color);
        } 
        #endregion

        protected override void Update(GameTime gameTime)
        {
            #region Testing Customer walking
            //timing down until customer can move
            customerCounter--;
            //check to see ball position if its less than the customer spawn box and its timer is less than 0 move to order screen
            //in the future potentially if the line is full just move to line and add to list then start patience timer.
            if (ball.Position.Y < customerSpawn.Bottom && customerCounter <= 0)
            {
                ball.Position += new Vector2(ball.Position.X, 2);
            } 
            #endregion
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            #region Testing tool to spawn a drawn rectangle
            //used to draw a rectangle using the rectangle drawing tool.
            _spriteBatch.Begin();
            DrawRectangle(new Rectangle(0, (int)Global.Stage.Y / 3, 100, 200), Color.White);
            _spriteBatch.End(); 
            #endregion
            base.Draw(gameTime);
        }
    }
}