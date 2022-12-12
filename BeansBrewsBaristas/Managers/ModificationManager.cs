using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeansBrewsBaristas.ComponentScripts;
using Microsoft.Xna.Framework.Input;

namespace BeansBrewsBaristas.Managers
{
    /// <summary>
    /// A collection of all the drink modifications
    /// </summary>
    public static class ModificationManager
    {
        public enum CupControls
        {
            COFFEE = Keys.D1,
            LATTE = Keys.D2,
            ESPRESSO = Keys.D3,
            TAKEOUT = Keys.D4
        }

        public enum BaseControls
        {
            COFFEE = Keys.C,
            ESPRESSO = Keys.E,
            STEAMED_MILK = Keys.M,
            CREAMER = Keys.B,
            CINN_POWDER = Keys.A
        }
        public enum AddinControls
        {
            VANILLA = Keys.V,
            CARAMEL = Keys.R,
            TOFFEE = Keys.T,
            HAZELNUT = Keys.H
        }

        public enum TakeoutControls
        {
            SLEEVE = Keys.S,
            LID = Keys.L
        }
    }
}
