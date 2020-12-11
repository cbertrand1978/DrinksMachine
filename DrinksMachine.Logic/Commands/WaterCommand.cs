using Common;
using DrinksMachine.Model;
using System;
using DrinksMachine.Logic.Interface;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// Implementation of the <see cref="IDrinkCommand"/> API.  This implementation deals with the application of water to the drink.
    /// </summary>
    public class WaterCommand : IDrinkCommand
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
        public WaterCommand(IMachineSensorReadings machineSensorReadings)
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

            if (this.SensorReadings.WaterTankLevel < template.AmountOfWaterRequired)
            {
                result.SetFailure(string.Format(CommandMessages.WaterCommandWaterAmountFailure, template.AmountOfWaterRequired, this.SensorReadings.WaterTankLevel));
            }
            else if (this.SensorReadings.WaterTemperature < template.Temperature)
            {
                this.SensorReadings.IncreaseWaterTemperature(template.Temperature);
            }

            if (this.SensorReadings.WaterTemperature == template.Temperature)
            {
                result.Drink.Temperature = this.SensorReadings.WaterTemperature;
                result.Drink.AmountOfWaterRequired = template.AmountOfWaterRequired;
                this.SensorReadings.DecreaseWaterLevel(template.AmountOfWaterRequired);
                result.SetSuccess(string.Format(CommandMessages.WaterCommandTemperatureSuccess, result.Drink.Temperature));
            }
            else
            {
                result.SetFailure(string.Format(CommandMessages.WaterCommandTemperatureFailure, template.Temperature));
            }

            return result.IsSuccess;
        }
    }
}
