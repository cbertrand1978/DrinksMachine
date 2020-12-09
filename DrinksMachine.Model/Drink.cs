using System.Collections.Generic;

namespace DrinksMachine.Model
{
    /// <summary>
    /// Represents a drink - i.e. a liquid that can be swallowed as refreshment or nourishment.
    /// </summary>
    public class Drink
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
        /// Get/Set the exact temperature required to make the drink.
        /// </summary>
        public decimal Temperature { get; set; }

        /// <summary>
        /// Get/Set the list of <see cref="Ingredient"/> needed to make the drink.
        /// </summary>
        public List<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Drink()
        {
        }

        public Drink(string name)
        {
            Require.IsNotNullOrEmpty(nameof(name), name);

            this.Name = name;
        }
    }
}
