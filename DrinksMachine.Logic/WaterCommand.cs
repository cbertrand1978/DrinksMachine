using DrinksMachine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// Implementation of the <see cref="IDrinkCommand"/> API.  This implementation deals with the application of water to the drink.
    /// </summary>
    public class WaterCommand : IDrinkCommand
    {
        /// <summary>
        /// Get/Set the next command.
        /// </summary>
        private IDrinkCommand Successor { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="successor">The successor command to execute.</param>
        public WaterCommand(IDrinkCommand successor)
        {
            Require.IsNotNull(nameof(successor), successor);
            this.Successor = successor;
        }

        /// <summary>
        /// Performs a command for the specific task.
        /// </summary>
        /// <param name="drink">The drink to apply the command to.</param>
        /// <param name="template">The drink that should be used as a template.</param>
        /// <returns>A <see cref="CommandResult"/> containing the result of the command.</returns>
        public CommandResult PerformCommand(Drink drink, Drink template)
        {
            throw new NotImplementedException();
        }
    }
}
