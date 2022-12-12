using BeansBrewsBaristas.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BeansBrewsBaristas.Managers.ModificationManager;
using static BeansBrewsBaristas.Managers.CustomerManager;
using Microsoft.Xna.Framework.Graphics;
using BeansBrewsBaristas.BaseClassScripts;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;

namespace BeansBrewsBaristas.ComponentScripts
{
    /// <summary>
    /// The drink to be made when presented to the user
    /// </summary>
    public class Order
    {
        public const int MAX_MODIFIERS = 5;
        public DrinkType DrinkType { get; set; }
        public Texture2D[] DrinkAssets;
        public List<Rectangle> DrinkFrames = new List<Rectangle>();
        public int[] DrinkDrawnIndex;


        public Vector2 DrinkDimensions;
        public int ROWS = 1;
        public int COLS;

        public bool HasCreamer;
        public bool HasPowder;

        public string DrinkName
        {
            get => drinkName;
            set
            {
                // Remove text after underscore
                int index = value.IndexOf("_");
                if (index > 0)
                    value = value.Remove(0, index + 1);

                drinkName = value;
            }
        }
        private string drinkName = "";

        public List<Keys> PreModKeys = new List<Keys>();
        public List<Modification> Modifications = new List<Modification>();
        public List<Keys> PostModKeys = new List<Keys>();

        /// <summary>
        /// Creates a completed Order and isolates the sprites frames
        /// </summary>
        public Order()
        {
            GenerateDrinkType();
            GetDrinkControls();
            GetDrinkAssets();

            CreateFrames();
        }

        /// <summary>
        /// Randomly generates a drink with modifications
        /// </summary>
        private void GenerateDrinkType()
        {
            // Grabs all values of type AddinControls
            var values = Enum.GetValues(typeof(DrinkType));

            // Creates random number between 0 and length of all enum values
            Random rand = new Random();
            int enumIndex = rand.Next(values.Length);

            // Sets Control as random value
            DrinkType = (DrinkType)values.GetValue(enumIndex);
            DrinkName = DrinkType.ToString();

            // Unable to add modifications to espressos
            if (DrinkType != DrinkType.ESPRESSO)
            {
                GenerateModifications();

                // Randomly generate if drink has creamer and/or cinn. powder
                if (DrinkName.Contains("COFFEE"))
                    HasCreamer = RandTrueFalse();
                HasPowder = RandTrueFalse();
            }
        }

        /// <summary>
        /// Randomly generates modifications for the drink
        /// </summary>
        private void GenerateModifications()
        {
            Random rand = new Random();
            for (int i = rand.Next(0, MAX_MODIFIERS); i < MAX_MODIFIERS; i++)
                Modifications.Add(new Modification());
        }

        /// <summary>
        /// Loads needed controls based on DrinkType and Modifications
        /// </summary>
        private void GetDrinkControls()
        {
            // All takeout drinks get the same cup
            if (Enum.GetName(DrinkType).Contains("TAKEOUT"))
            {
                PreModKeys.Add((Keys)CupControls.TAKEOUT);
                switch (DrinkType)
                {
                    case DrinkType.TAKEOUT_COFFEE:
                        PreModKeys.Add((Keys)BaseControls.COFFEE);

                        if (HasCreamer)
                            PreModKeys.Add((Keys)BaseControls.CREAMER);

                        if (HasPowder)
                            PreModKeys.Add((Keys)BaseControls.CINN_POWDER);
                        break;
                    case DrinkType.TAKEOUT_LATTE:
                        PreModKeys.Add((Keys)BaseControls.ESPRESSO);
                        PreModKeys.Add((Keys)BaseControls.STEAMED_MILK);

                        if (HasPowder)
                            PreModKeys.Add((Keys)BaseControls.CINN_POWDER);
                        break;
                }

                PostModKeys.Add((Keys)TakeoutControls.SLEEVE);
                PostModKeys.Add((Keys)TakeoutControls.LID);
            }    
            else
                switch (DrinkName)
                {
                    case "COFFEE":
                        PreModKeys.Add((Keys)CupControls.COFFEE);

                        PreModKeys.Add((Keys)BaseControls.COFFEE);

                        if(HasCreamer)
                            PreModKeys.Add((Keys)BaseControls.CREAMER);

                        if (HasPowder)
                            PreModKeys.Add((Keys)BaseControls.CINN_POWDER);
                        break;
                    case "LATTE":
                        PreModKeys.Add((Keys)CupControls.LATTE);

                        PreModKeys.Add((Keys)BaseControls.ESPRESSO);
                        PreModKeys.Add((Keys)BaseControls.STEAMED_MILK);

                        if (HasPowder)
                            PreModKeys.Add((Keys)BaseControls.CINN_POWDER);
                        break;
                    case "ESPRESSO":
                        PreModKeys.Add((Keys)CupControls.ESPRESSO);

                        PreModKeys.Add((Keys)BaseControls.ESPRESSO);
                        break;
                }
        }

        /// <summary>
        /// Loads assets based on DrinkType enum
        /// </summary>
        private void GetDrinkAssets()
        {
            switch (DrinkType)
            {
                case DrinkType.COFFEE:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Coffee 171_152")
                    };
                    DrinkDimensions = new Vector2(171, 152);
                    COLS = 3;
                    break;
                case DrinkType.LATTE:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Latte 213_132")
                    };
                    DrinkDimensions = new Vector2(213, 132);
                    COLS = 3;
                    break;
                case DrinkType.ESPRESSO:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Espresso 102_79")
                    };
                    DrinkDimensions = new Vector2(102, 79);
                    COLS = 2;
                    break;
                case DrinkType.TAKEOUT_COFFEE:
                case DrinkType.TAKEOUT_LATTE:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Takeout 132_176"),
                        Global.GameManager.Content.Load<Texture2D>("Images/Lid 142_60"),
                        Global.GameManager.Content.Load<Texture2D>("Images/Sleeve 130_92")
                    };
                    DrinkDimensions = new Vector2(132, 176);
                    COLS = 5;
                    break;
            }
            GameManager.DrinkPos = new Vector2(
                        Global.Stage.X / 2 - DrinkDimensions.X / 2,
                        Global.Stage.Y / 2 - DrinkDimensions.Y / 2
                    );
        }

        /// <summary>
        /// Generates a value of True or False based on Random
        /// </summary>
        /// <returns>Random True or False value</returns>
        private bool RandTrueFalse()
        {
            Random rand = new Random();
            switch(rand.Next(2))
            {
                case 1:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Creates rectangles of the spritesheet of the AnimatedSprite
        /// </summary>
        private void CreateFrames()
        {
            DrinkFrames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)DrinkDimensions.X;
                    int y = i * (int)DrinkDimensions.Y;

                    Rectangle r = new Rectangle(x, y, (int)DrinkDimensions.X, (int)DrinkDimensions.Y);
                    DrinkFrames.Add(r);
                }
            }
        }

        /// <summary>
        /// Overridden string that includes all modifications and datetime
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            Dictionary<string, int> modifications = new Dictionary<string, int>();

            foreach (Modification mod in Modifications)
                if (modifications.ContainsKey(mod.Name))
                    modifications[mod.Name]++;
                else modifications.Add(mod.Name, 1);

            string str = $"\n{DrinkName}\n";
            foreach (KeyValuePair<string, int> kvp in modifications)
                if (kvp.Value > 1)
                    str += $"{kvp.Value} pumps {kvp.Key}\n";
                else
                    str += $"{kvp.Value} pump {kvp.Key}\n";

            if (HasCreamer)
                str += $"Creamer\n";
            if (HasPowder)
                str += "Cinn. Powder\n";

            str += $"\n{DateTime.Now.ToString("d-MMM-yyyy  h:mm tt")}\n";
            if (DrinkType.ToString().Contains("TAKEOUT"))
                str += ">TAKEOUT<";
            else
                str += ">DINE-IN<";

            return str;
        }
    }
}
