using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Managers
{
    public class InputManager : GameComponent
    {
        public InputManager(Game game) : base(game) { }

        public override void Update(GameTime gameTime)
        {
            _msState = Mouse.GetState();
            _kbState = Keyboard.GetState();

            // Keyboard Input
            foreach (Keys keyPressed in _kbState.GetPressedKeys())
            {
                if (OnKeyDown(keyPressed))
                {
                    switch (keyPressed)
                    {
                        // Game Controls //
                        case Keys.Escape:
                            Global.GameManager.Exit();
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

                        // Debugging Controls //
                        case Keys.Z:
                            Debug.Output("This is a debug message.");
                            break;
                    }
                }

                if (OnKeyUp(keyPressed))
                {
                    switch (keyPressed)
                    {
                        case Keys.A:
                            break;
                    }
                }
            }

            // Left click Input
            switch(OnLeftMouseDown())
            {
                case true:
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
                    CustomerManager.CreateCustomer();
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

        private bool OnKeyDown(Keys key)
        {
            if (_kbState.IsKeyDown(key) && _prevKbState.IsKeyUp(key))
                return true;
            else return false;
        }

        private bool OnKeyUp(Keys key)
        {
            if (_kbState.IsKeyUp(key) && _prevKbState.IsKeyDown(key))
                return true;
            else return false;
        }

        private bool OnLeftMouseDown()
        {
            if (_msState.LeftButton == ButtonState.Pressed && _prevMsState.LeftButton == ButtonState.Released)
                return true;
            else return false;
        }

        private bool OnLeftMouseUp()
        {
            if (_msState.LeftButton == ButtonState.Released && _prevMsState.LeftButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        private bool OnRightMouseDown()
        {
            if (_msState.RightButton == ButtonState.Pressed && _prevMsState.RightButton == ButtonState.Released)
                return true;
            else return false;
        }

        private bool OnRightMouseUp()
        {
            if (_msState.RightButton == ButtonState.Released && _prevMsState.RightButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        MouseState _msState;
        KeyboardState _kbState;

        MouseState _prevMsState;
        KeyboardState _prevKbState;
    }
}
