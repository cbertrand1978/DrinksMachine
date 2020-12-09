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
        CommandResult PerformCommand(Drink drink);
    }
}
