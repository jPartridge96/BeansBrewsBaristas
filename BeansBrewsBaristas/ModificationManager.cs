using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace BeansBrewsBaristas
{
    public static class ModificationManager
    {
        public enum ModificationControls
        {
            VANILLA = Keys.V,
            CARAMEL = Keys.C,
            TOFFEE = Keys.T,
            HAZELNUT = Keys.H
        }
    }
}
