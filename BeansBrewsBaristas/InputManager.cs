using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas
{
    public class InputManager : GameComponent
    {
        public InputManager(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mState = Mouse.GetState();
            KeyboardState kbState = Keyboard.GetState();

            // Keyboard Input
            foreach(Keys keyPressed in kbState.GetPressedKeys())
            {
                switch (keyPressed)
                {
                    case Keys.A:
                        break;
                }
            }

            // Left click Input
            switch(mState.LeftButton)
            {
                case ButtonState.Pressed:
                    break;
                case ButtonState.Released:
                    break;
            }

            // Right click Input
            switch (mState.RightButton)
            {
                case ButtonState.Pressed:
                    break;
                case ButtonState.Released:
                    break;
            }

            base.Update(gameTime);
        }

        public void OnRecievedInput()
        {

        }
    }
}
