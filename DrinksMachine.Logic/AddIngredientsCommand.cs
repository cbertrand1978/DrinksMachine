using DrinksMachine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinksMachine.Logic
{
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

            return result.IsSuccess;
        }
    }
}
