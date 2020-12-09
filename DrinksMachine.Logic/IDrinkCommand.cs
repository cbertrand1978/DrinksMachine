using System;
using System.Collections.Generic;
using System.Text;

using DrinksMachine.Model;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// API for a drink command - an action to be taken on a drink.
    /// </summary>
    public interface IDrinkCommand
    {
        /// <summary>
        /// Performs a command for the specific task.
        /// </summary>
        /// <param name="drink">The drink to apply the command to.</param>
        /// <param name="template">The drink that should be used as a template.</param>
        /// <returns>A <see cref="CommandResult"/> containing the result of the command.</returns>
        CommandResult PerformCommand(Drink drink, Drink template);
    }
}
