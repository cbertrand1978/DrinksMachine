using DrinksMachine.Model;
using DrinksMachine.Model.Logic.Interface;
using System;

namespace DrinksMachine.Logic.Executors
{
    /// <summary>
    /// Implementation of the <see cref="IServiceExecutor"/> API for drinks.
    /// </summary>
    public class DrinkExecutor : IServiceExecutor
    {
        /// <summary>
        /// Execute the commands associated with an <see cref="IBeverage"/>.
        /// </summary>
        /// <param name="beverage">The <see cref="IBeverage"/> whose commands should be executed.</param>
        /// <param name="template">The <see cref="IBeverage"/> that should be used as the template to complete against.</param>
        /// <returns>A <see cref="CommandResult"/> containing the outcome of the operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="beverage"/> is not of type <see cref="Drink"/>, which is required for this particular executor.
        /// Raised if <paramref name="template"/> is not of type <see cref="Drink"/>, which is required for this particular executor.
        /// </exception>
        public CommandResult ExecuteCommands(IBeverage beverage, IBeverage template)
        {
            var drink = beverage as Drink;
            var drinkTemplate = template as Drink;

            Require.IsNotNull(nameof(beverage), beverage);
            Require.IsNotNull(nameof(template), template);
            Require.IsCorrectType<Drink>(nameof(beverage), beverage);
            Require.IsCorrectType<Drink>(nameof(drinkTemplate), drinkTemplate);

            var result = new CommandResult(drink);

            foreach (var command in drink.ServingActions)
            {
                command.PerformCommand(drinkTemplate, result);
            }

            return result;
        }
    }
}
