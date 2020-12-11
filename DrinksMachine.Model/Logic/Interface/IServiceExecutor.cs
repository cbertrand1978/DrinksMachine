using DrinksMachine.Logic;

namespace DrinksMachine.Model.Logic.Interface
{
    /// <summary>
    /// API for a service executor.
    /// </summary>
    public interface IServiceExecutor
    {
        /// <summary>
        /// Execute the commands associated with an <see cref="IBeverageTemplate"/>.
        /// </summary>
        /// <param name="beverage">The <see cref="IBeverage"/> whose commands should be executed.</param>
        /// <param name="template">The <see cref="IBeverageTemplate"/> that should be used as the template to complete against.</param>
        /// <returns>A <see cref="CommandResult"/> containing the outcome of the operation.</returns>
        CommandResult ExecuteCommands(/*IBeverage beverage,*/ IBeverageTemplate template);
    }
}
