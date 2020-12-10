using Common;
using DrinksMachine.Logic.Interface;
using DrinksMachine.Model;
using System;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// Allows the exection of serving actions using the <see cref="IDrinkCommand"/> API.
    /// </summary>
    public class ServeDrinkCommand : IDrinkCommand
    {

        /// <summary>
        /// Get/Set the action that this command will perform.
        /// </summary>
        private Func<CommandResult, string, bool> ActionToPerform { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="machineSensorReadings">An interface to the drinks machine's sensor readings.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="machineSensorReadings"/> is null.
        /// </exception>
        public ServeDrinkCommand(Func<CommandResult, string, bool> actionToPerform)
        {
            Require.IsNotNull(nameof(actionToPerform), actionToPerform);

            this.ActionToPerform = actionToPerform;
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

            this.ActionToPerform(result, CommandMessages.ActionToPerformSuccess);

            return result.IsSuccess;
        }
    }
}
