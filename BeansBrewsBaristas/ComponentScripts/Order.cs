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

namespace BeansBrewsBaristas.ComponentScripts
{
    public class Order
    {
        public const int MAX_MODIFIERS = 5;
        public DrinkType DrinkType { get; set; }
        public Texture2D[] DrinkAssets;

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

        public Order()
        {
            GenerateDrinkType();
            GetDrinkControls();
            GetDrinkAssets();
        }

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

        private void GenerateModifications()
        {
            Random rand = new Random();
            for (int i = rand.Next(0, MAX_MODIFIERS); i < MAX_MODIFIERS; i++)
                Modifications.Add(new Modification());
        }

        private void GetDrinkControls()
        {
            // CupControls
            // All takeout drinks get the same cup
            if (Enum.GetName(DrinkType).Contains("TAKEOUT"))
            {
                PreModKeys.Add((Keys)CupControls.TAKEOUT);

                PostModKeys.Add((Keys)TakeoutControls.SLEEVE);
                PostModKeys.Add((Keys)TakeoutControls.LID);
            }    
            else
                switch (DrinkName)
                {
                    case "Coffee":
                        PreModKeys.Add((Keys)CupControls.COFFEE);

                        PreModKeys.Add((Keys)BaseControls.COFFEE);

                        if(HasCreamer)
                            PreModKeys.Add((Keys)BaseControls.CREAMER);

                        if (HasPowder)
                            PreModKeys.Add((Keys)BaseControls.CINN_POWDER);
                        break;
                    case "Latte":
                        PreModKeys.Add((Keys)CupControls.LATTE);

                        PreModKeys.Add((Keys)BaseControls.ESPRESSO);
                        PreModKeys.Add((Keys)BaseControls.STEAMED_MILK);

                        if (HasPowder)
                            PreModKeys.Add((Keys)BaseControls.CINN_POWDER);
                        break;
                    case "Espresso":
                        PreModKeys.Add((Keys)CupControls.ESPRESSO);

                        PreModKeys.Add((Keys)BaseControls.ESPRESSO);
                        break;
                }
        }

        private void GetDrinkAssets()
        {
            switch (DrinkType)
            {
                case DrinkType.COFFEE:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Coffee 171_152")
                    };
                    break;
                case DrinkType.LATTE:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Latte 213_132")
                    };
                    break;
                case DrinkType.ESPRESSO:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Espresso 102_79")
                    };
                    break;
                case DrinkType.TAKEOUT_COFFEE:
                case DrinkType.TAKEOUT_LATTE:
                    DrinkAssets = new Texture2D[]
                    {
                        Global.GameManager.Content.Load<Texture2D>("Images/Takeout 132_176"),
                        Global.GameManager.Content.Load<Texture2D>("Images/Lid 142_60"),
                        Global.GameManager.Content.Load<Texture2D>("Images/Sleeve 130_92")
                    };
                    break;
            }
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
