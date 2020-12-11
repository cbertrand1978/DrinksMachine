using DrinksMachine.Logic.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DrinksMachine.Model
{
    /// <summary>
    /// Represents a drink - i.e. a liquid that can be swallowed as refreshment or nourishment.
    /// </summary>
    public class Drink : IBeverage, IBeverageTemplate
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

        /// <summary>
        /// Get/Set the list of actions required to serve the drink.
        /// </summary>
        public List<IDrinkCommand> ServingActions { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Drink()
        {
            this.Ingredients = new Dictionary<string, int>();
            this.ServingActions = new List<IDrinkCommand>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the drink.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="name"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Raised if <paramref name="name"/> is empty.
        /// </exception>
        public Drink(string name)
        {
            Require.IsNotNullOrEmpty(nameof(name), name);

            this.Name = name;
            this.Ingredients = new Dictionary<string, int>();
            this.ServingActions = new List<IDrinkCommand>();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="original">The original drink to copy from.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="original"/> is null.
        /// </exception>
        public Drink(Drink original)
        {
            Require.IsNotNull(nameof(original), original);

            this.Ingredients = original.Ingredients.ToDictionary((kvp) => kvp.Key, (kvp) => kvp.Value);
            this.IsHot = original.IsHot;
            this.Name = original.Name;
            this.Temperature = original.Temperature;
            this.ServingActions = original.ServingActions;

        }
    }
}
