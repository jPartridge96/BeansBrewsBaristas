using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static BeansBrewsBaristas.ModificationManager;

namespace BeansBrewsBaristas
{
    public class Modification
    {
        public Modification()
        {
            // Grabs all values of type ModificationControls
            var values = Enum.GetValues(typeof(ModificationControls));

            // Creates random number between 0 and length of all enum values
            Random rand = new Random();
            int enumIndex = rand.Next(values.Length);

            // Sets Control as random value
            Control = (ModificationControls)values.GetValue(enumIndex);
            Name = Control.ToString();
        }

        public string Name { get; }
        public ModificationControls Control { get; }
    }
}
