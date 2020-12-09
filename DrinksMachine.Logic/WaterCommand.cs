using DrinksMachine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinksMachine.Logic
{
    public class WaterCommand : IDrinkCommand
    {
        public CommandResult PerformCommand(Drink drink)
        {
            throw new NotImplementedException();
        }
    }
}
