using Common;
using DrinksMachine.Logic.Interface;
using DrinksMachine.Model;
using System;
using System.Linq;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// Allows the adding of ingredients using the <see cref="IDrinkCommand"/> API.
    /// </summary>
    public class AddIngredientsCommand : IDrinkCommand
    {
        /// <summary>
        /// Get/Set the sensor readings interface.
        /// </summary>
        private IMachineSensorReadings SensorReadings { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="machineSensorReadings">An interface to the drinks machine's sensor readings.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="machineSensorReadings"/> is null.
        /// </exception>
        public AddIngredientsCommand(IMachineSensorReadings machineSensorReadings)
        {
            Require.IsNotNull(nameof(machineSensorReadings), machineSensorReadings);

            this.SensorReadings = machineSensorReadings;
        }
        /// <summary>
        /// Performs a command for the specific task.
        /// </summary>
        /// <param name="template">The drink that should be used as a template.</param>
        /// <param name="result">A <see cref="CommandResult"/> to record the outcome.</param>
        /// <returns>True if the command executed successfully, else false.</returns>
        public bool PerformCommand(Drink template, CommandResult result)
        {
            Require.IsNotNull(nameof(template), template);
            Require.IsNotNull(nameof(result), result);
            Require.IsNotNull(nameof(result.Drink), result.Drink);
            Require.CollectionNotNullOrEmpty(nameof(template.Ingredients), template.Ingredients);
            var success = true;

            foreach (var ingredient in template.Ingredients)
            {
                if (!this.SensorReadings.IngredientSupply.ContainsKey(ingredient.Key))
                {
                    result.SetFailure(string.Format(CommandMessages.AddIngredientCannotFindIngredient, ingredient.Key));
                    success = false;
                    break;
                }

                if (ingredient.Value == 0)
                {
                    continue;
                }

                if (this.SensorReadings.IngredientSupply[ingredient.Key] < ingredient.Value)
                {
                    result.SetFailure(string.Format(CommandMessages.AddIngredientInsuffientIngredientError, ingredient.Value, ingredient.Key,
                        this.SensorReadings.IngredientSupply[ingredient.Key]));
                    success = false;
                }
                else

                {
                    result.Drink.Ingredients.Add(ingredient.Key, ingredient.Value);
                    this.SensorReadings.DecreaseIngredient(ingredient.Key, ingredient.Value);
                }
            }

            if (success)
            {
                result.SetSuccess(string.Format(CommandMessages.AddIngredientSuccess, string.Join(", ", result.Drink.Ingredients.Select(x => $"{x.Key}: {x.Value}"))));
            }

            return result.IsSuccess;
        }
    }
}
