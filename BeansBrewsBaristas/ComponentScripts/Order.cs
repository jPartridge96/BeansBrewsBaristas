using BeansBrewsBaristas.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BeansBrewsBaristas.Managers.ModificationManager;

namespace BeansBrewsBaristas.ComponentScripts
{
    public class Order
    {
        public const int MAX_MODIFIERS = 5;
        public CustomerManager.DrinkType DrinkType { get; set; }

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

        public List<Modification> Modifications = new List<Modification>();

        public Order()
        {
            GenerateDrinkType();
        }


        private void GenerateDrinkType()
        {
            // Grabs all values of type ModificationControls
            var values = Enum.GetValues(typeof(CustomerManager.DrinkType));

            // Creates random number between 0 and length of all enum values
            Random rand = new Random();
            int enumIndex = rand.Next(values.Length);

            // Sets Control as random value
            DrinkType = (CustomerManager.DrinkType)values.GetValue(enumIndex);
            DrinkName = DrinkType.ToString();

            // Unable to add modifications to espressos
            if (DrinkType != CustomerManager.DrinkType.ESPRESSO)
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

            str += $"\n{DateTime.Now.ToString("d-MMM-yyyy  h:mm tt")}\n";
            if (DrinkType.ToString().Contains("TAKEOUT"))
                str += ">TAKEOUT<";
            else
                str += ">DINE-IN<";

            return str;
        }
    }
}
