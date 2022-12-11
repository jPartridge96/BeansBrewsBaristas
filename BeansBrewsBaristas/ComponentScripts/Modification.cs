using BeansBrewsBaristas.Managers;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static BeansBrewsBaristas.Managers.ModificationManager;

namespace BeansBrewsBaristas.ComponentScripts
{
    public class Modification
    {
        public Order activeOrder { get; set; }
        public Modification()
        {
            // Grabs all values of type AddinControls
            var values = Enum.GetValues(typeof(AddinControls));

            // Creates random number between 0 and length of all enum values
            Random rand = new Random();
            int enumIndex = rand.Next(values.Length);

            // Sets Control as random value
            Control = (AddinControls)values.GetValue(enumIndex);
            Name = Control.ToString();
        }

        public string Name
        {
            get => name;
            set
            {
                // Convert first letter to uppercase
                value = value.ToLower();
                value = value.Replace(value[0], char.Parse(value[0].ToString().ToUpper()));

                name = value;
            }
        }
        private string name;
        public AddinControls Control { get; }

    }
}
