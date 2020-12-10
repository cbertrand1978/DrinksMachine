using System;

namespace DrinksMachine.Model
{
    /// <summary>
    /// Represents an item that can be added to make a drink.
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Get/Set the name of the ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Ingredient()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the ignredient.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Raised if <paramref name="name"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// Raised if <paramref name="name"/> is empty.
        /// </exception>
        public Ingredient(string name)
        {
            Require.IsNotNullOrEmpty(nameof(name), name);

            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            var otherIngredient = obj as Ingredient;

            if (otherIngredient != null)
            {
                return this.Name == otherIngredient.Name;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
