using BeansBrewsBaristas.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BeansBrewsBaristas.ModificationManager;

namespace BeansBrewsBaristas
{
    public class Order
    {
        public const int MAX_MODIFIERS = 5;
        public CustomerManager.DrinkType DrinkType { get; set; }
        public Order()
        {
            GenerateDrinkType();
        }

        public List<Modification> Modifications = new List<Modification>();

        private void GenerateDrinkType()
        {
            // Grabs all values of type ModificationControls
            var values = Enum.GetValues(typeof(CustomerManager.DrinkType));

            // Creates random number between 0 and length of all enum values
            Random rand = new Random();
            int enumIndex = rand.Next(values.Length);

            // Sets Control as random value
            DrinkType = (CustomerManager.DrinkType)values.GetValue(enumIndex);


            // Unable to add modifications to espressos
            if(DrinkType != CustomerManager.DrinkType.ESPRESSO)
                GenerateModifications();
            // TODO: ADD OPTIONAL DUSTING FOR COFFEE OR LATTE
        }

        private void GenerateModifications()
        {
            Random rand = new Random();
            for (int i = rand.Next(0, MAX_MODIFIERS); i < MAX_MODIFIERS; i++)
                Modifications.Add(new Modification());
        }

        public override string ToString()
        {
            string str = $"{DrinkType}\n---\n";

            foreach (Modification mod in Modifications)
                str += $"{mod.Name}\n";

            return str;
        }
    }
}
