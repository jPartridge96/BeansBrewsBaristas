using BeansBrewsBaristas.BaseClassScripts;
using BeansBrewsBaristas.ComponentScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using SharpDX.Direct3D9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BeansBrewsBaristas.Managers.CustomerManager;

namespace BeansBrewsBaristas.Managers
{
    public class InputManager : GameComponent
    {
        
        public InputManager(Game game) : base(game) { }

        public string CurrentScene { get; set; } = "Menu";

        public override void Update(GameTime gameTime)
        {
            _msState = Mouse.GetState();
            _kbState = Keyboard.GetState();

            // Keyboard Input
            foreach (Keys keyPressed in _kbState.GetPressedKeys())
            {
                if (OnKeyDown(keyPressed))
                {
                    switch(SceneManager.ActiveScene)
                    {
                        case "Menu":
                            switch(keyPressed)
                            {
                                case Keys.Escape:
                                    Global.GameManager.Exit();
                                    break;

                                case Keys.W:
                                case Keys.Up:
                                    MenuComponent.SelectedIndex--;
                                    break;
                                case Keys.S:
                                case Keys.Down:
                                    MenuComponent.SelectedIndex++;
                                    break;

                                case Keys.Enter:
                                case Keys.Space:
                                    MenuComponent.Select();
                                    break;
                            }
                            break;

                        case "Help":
                        case "Credits":
                        case "Options":
                            switch (keyPressed)
                            {
                                case Keys.Escape:
                                    SceneManager.LoadScene("Menu");
                                    break;
                            }

                            break;
                            //need to add a method here that checks for button click
                            //i want to check to see if the texture is clicked...
                            //alternatively also adding a hover method to check and see if mouse is inside of area and then change texture in background


                        case "Level1":
                        case "Level2":
                        case "Level3":
                            switch (keyPressed)
                            {
                                // Main Menu //
                                case Keys.Escape:
                                    SceneManager.LoadScene("Menu");
                                    break;

                                // Scene Management //
                                case Keys.F1:
                                    SceneManager.LoadScene("Level1");
                                    break;
                                case Keys.F2:
                                    SceneManager.LoadScene("Level2");
                                    break;
                                case Keys.F3:
                                    SceneManager.LoadScene("Level3");
                                    break;
                            }
                            break;
                    }
                }
            }

            // Left click Input
            switch(OnLeftMouseDown())
            {
                case true:
                    CreateCustomer();
                    break;
                case false:
                    break;
            }

            switch (OnLeftMouseUp())
            {
                case true:
                    break;
                case false:
                    break;
            }

            // Right click Input
            switch (OnRightMouseDown())
            {
                case true:
                    TakeNextOrder();
                    break;
                case false:
                    break;
            }

            switch (OnRightMouseUp())
            {
                case true:
                    break;
                case false:
                    break;
            }

            _prevKbState = _kbState;
            _prevMsState = _msState;

            base.Update(gameTime);
        }

        public bool OnKeyDown(Keys key)
        {
            if (_kbState.IsKeyDown(key) && _prevKbState.IsKeyUp(key))
                return true;
            else return false;
        }

        public static bool OnKeyUp(Keys key)
        {
            if (_kbState.IsKeyUp(key) && _prevKbState.IsKeyDown(key))
                return true;
            else return false;
        }

        public static bool OnLeftMouseDown()
        {
            if (_msState.LeftButton == ButtonState.Pressed && _prevMsState.LeftButton == ButtonState.Released)
                return true;
            else return false;
        }

        public static bool OnLeftMouseUp()
        {
            if (_msState.LeftButton == ButtonState.Released && _prevMsState.LeftButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public static bool OnRightMouseDown()
        {
            if (_msState.RightButton == ButtonState.Pressed && _prevMsState.RightButton == ButtonState.Released)
                return true;
            else return false;
        }

        private static bool OnRightMouseUp()
        {
            if (_msState.RightButton == ButtonState.Released && _prevMsState.RightButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public static MouseState _msState;
        static KeyboardState _kbState;

        static MouseState _prevMsState;
        static KeyboardState _prevKbState;
    }
}
