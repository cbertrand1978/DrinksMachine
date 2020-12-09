using DrinksMachine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinksMachine.Logic
{
    /// <summary>
    /// The result from a command action.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Get the flag indicating the action was successful.
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Get the message from the action.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Get the drink from the action.
        /// </summary>
        public Drink Drink { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isSuccess">Flag indicating if the action was successful.</param>
        /// <param name="message">Message from the action.</param>
        /// <param name="drink">The resulting drink from the action.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="message"/> is null.
        /// Raised if <paramref name="drink"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Raised if <paramref name="message"/> is empty.
        /// </exception>
        public CommandResult(bool isSuccess, string message, Drink drink)
        {
            Require.IsNotNullOrEmpty(nameof(message), message);
            Require.IsNotNull(nameof(drink), drink);

            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Drink = drink;
        }
    }
}
