using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.BaseClassScripts
{
    /// <summary>
    /// Used for storing information passed from a class for other classes
    /// when the direct properties should not be available.
    /// </summary>
    public class Global
    {
        // Managers
        public static GameManager GameManager { get; set; }

        public static InputManager InputManager { get; set; }
        // GameManager Properties
        public static SpriteBatch SpriteBatch { get; set; }



        // Global Vars
        public static Vector2 Stage { get; set; }

        public enum Vertex
        {
            NONE,
            TOP,
            TOP_RIGHT,
            RIGHT,
            BOTTOM_RIGHT,
            BOTTOM,
            BOTTOM_LEFT,
            LEFT,
            TOP_LEFT,
            CENTER
        }
    }
}
