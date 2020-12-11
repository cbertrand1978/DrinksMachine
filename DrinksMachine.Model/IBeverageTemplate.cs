using DrinksMachine.Logic.Interface;
using System.Collections.Generic;

namespace DrinksMachine.Model
{
    /// <summary>
    /// API for a beverage template.
    /// </summary>
    public interface IBeverageTemplate
    {
        /// <summary>
        /// Get/Set the name of the drink.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get/Set the flag indicating if it is hot or not.
        /// </summary>
        public bool IsHot { get; }

        /// <summary>
        /// Get/Set the amount of water required to make the drink.
        /// </summary>
        public int AmountOfWaterRequired { get; }

        /// <summary>
        /// Get/Set the exact temperature required to make the drink.
        /// </summary>
        public int Temperature { get; }

        /// <summary>
        /// Get/Set the list of <see cref="Ingredient"/> needed to make the drink.
        /// </summary>
        public Dictionary<string, int> Ingredients { get; }

        /// <summary>
        /// Get/Set the list of actions required to serve the drink.
        /// </summary>
        List<IDrinkCommand> ServingActions { get; }
    }
}
