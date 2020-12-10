using DrinksMachine.Model;

namespace DrinksMachine.Logic.Interface
{
    /// <summary>
    /// API for a drink command - an action to be taken on a drink.
    /// </summary>
    public interface IDrinkCommand
    {
        /// <summary>
        /// Performs a command for the specific task.
        /// </summary>
        /// <param name="result">A <see cref="CommandResult"/> to record the outcome.</param>
        /// <returns>True if the command executed successfully, else false.</returns>
        bool PerformCommand(Drink template, CommandResult result);
    }
}
