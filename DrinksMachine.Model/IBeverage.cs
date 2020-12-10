using DrinksMachine.Logic.Interface;
using System.Collections.Generic;

namespace DrinksMachine.Model
{
    /// <summary>
    /// API for a beverage.
    /// </summary>
    public interface IBeverage
    {
        /// <summary>
        /// Get/Set the list of actions required to serve the drink.
        /// </summary>
        List<IDrinkCommand> ServingActions { get; }
    }
}
