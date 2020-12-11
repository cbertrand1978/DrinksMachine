using DrinksMachine.Model;
using System.Collections.Generic;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// API of the readings from the drink's machine sensors.
    /// </summary>
    public interface IMachineSensorReadings
    {
        /// <summary>
        /// The current water temperature.
        /// </summary>
        int WaterTemperature { get; }

        /// <summary>
        /// The overall water tank capacity.
        /// </summary>
        int WaterTankCapacity { get; }

        /// <summary>
        /// The current water tank level.
        /// </summary>
        int WaterTankLevel { get; }

        /// <summary>
        /// Get the ingredient supply.
        /// </summary>
        Dictionary<string, int> IngredientSupply { get; }

        /// <summary>
        /// Uses the machines water heater to heat the water up.
        /// If <paramref name="targetTemperature"/> is null, the default temperature is used.
        /// </summary>
        /// <param name="targetTemperature"></param>
        void IncreaseWaterTemperature(int targetTemperature);

        /// <summary>
        /// Decrease the water level by the specified <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">The amount to decrease the water level by.</param>
        void DecreaseWaterLevel(int amount);

        /// <summary>
        /// Decrease the specified <paramref name="ingredient"/> by the given <paramref name="amount"/>.
        /// </summary>
        /// <param name="ingredient">The ingredient to decrease.</param>
        /// <param name="amount">The amount to decrease it by.</param>
        void DecreaseIngredient(string ingredient, int amount);
    }
}
