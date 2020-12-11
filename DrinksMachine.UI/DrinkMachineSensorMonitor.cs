using DrinksMachine.Logic;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DrinksMachine.UI
{
    public class DrinkMachineSensorMonitor : IMachineSensorReadings
    {
        public int WaterTemperature { get; private set; }

        public int WaterTankCapacity { get; private set; }

        public int WaterTankLevel { get; private set; }

        public Dictionary<string, int> IngredientSupply { get; private set; }

        public DrinkMachineSensorMonitor()
        {
            this.IngredientSupply = new Dictionary<string, int>();
        }

        public DrinkMachineSensorMonitor(int waterTemperature, int waterTankCapacity, int waterTankLevel, Dictionary<string, int> IngredientSupply)
        {
            this.WaterTemperature = waterTemperature;
            this.WaterTankCapacity = waterTankCapacity;
            this.WaterTankLevel = waterTankLevel;
            this.IngredientSupply = IngredientSupply;
        }

        public void IncreaseWaterTemperature(int targetTemperature)
        {
            while (this.WaterTemperature < targetTemperature)
            {
                this.WaterTemperature += 1;
                Console.WriteLine($"Water temperature: {this.WaterTemperature}c.");
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Decrease the water level by the specified <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">The amount to decrease the water level by.</param>
        public void DecreaseWaterLevel(int amount)
        {
            this.WaterTankLevel -= amount;
            Console.WriteLine($"Water tank level now: {this.WaterTankLevel}.");
        }

        /// <summary>
        /// Decrease the specified <paramref name="ingredient"/> by the given <paramref name="amount"/>.
        /// </summary>
        /// <param name="ingredient">The ingredient to decrease.</param>
        /// <param name="amount">The amount to decrease it by.</param>
        public void DecreaseIngredient(string ingredient, int amount)
        {
            if (this.IngredientSupply.ContainsKey(ingredient))
            {
                this.IngredientSupply[ingredient] -= amount;
                Console.WriteLine($"{this.IngredientSupply[ingredient]} of {ingredient} left."); 
            }
        }
    }
}
