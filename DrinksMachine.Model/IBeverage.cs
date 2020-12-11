using System;
using System.Collections.Generic;
using System.Text;

namespace DrinksMachine.Model
{
    public interface IBeverage
    {
        /// <summary>
        /// Get/Set the name of the drink.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get/Set the flag indicating if it is hot or not.
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// Get/Set the amount of water required to make the drink.
        /// </summary>
        public int AmountOfWaterRequired { get; set; }

        /// <summary>
        /// Get/Set the exact temperature required to make the drink.
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// Get/Set the list of <see cref="Ingredient"/> needed to make the drink.
        /// </summary>
        public Dictionary<string, int> Ingredients { get; set; }
    }
}
