using System;
using System.Collections.Generic;
using System.Text;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// API of the readings from the drink's machine sensors.
    /// </summary>
    public interface IMachineReadings
    {
        /// <summary>
        /// The current water temperature.
        /// </summary>
        decimal WaterTemperature { get; }

        /// <summary>
        /// The overall water tank capacity.
        /// </summary>
        decimal WaterTankCapacity { get; }

        /// <summary>
        /// The current water tank level.
        /// </summary>
        decimal WaterTankLevel { get; }

        /// <summary>
        /// Uses the machines water heater to heat the water up.
        /// If <paramref name="targetTemperature"/> is null, the default temperature is used.
        /// </summary>
        /// <param name="targetTemperature"></param>
        void IncreaseWaterTemperature(decimal? targetTemperature);
    }
}
