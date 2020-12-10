using DrinksMachine.Model;
using System;
using System.Collections.Generic;

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
        /// Get the messages from the actions.
        /// </summary>
        public List<string> Messages { get; private set; }

        /// <summary>
        /// Get the drink from the action.
        /// </summary>
        public Drink Drink { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommandResult()
        {
            this.Messages = new List<string>();
            this.Drink = new Drink();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="drink">The drink to associate with this result.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="drink"/> is null.
        /// </exception>
        public CommandResult(Drink drink)
        {
            Require.IsNotNull(nameof(drink), drink);
            this.Messages = new List<string>();

            this.Drink = drink;
        }

        /// <summary>
        /// Sets the result as success and adds a message plus the result.
        /// </summary>
        /// <param name="message">The message to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="message"/> is null.
        /// </exception>
        public void SetSuccess(string message)
        {
            Require.IsNotNullOrEmpty(nameof(message), message);
            this.IsSuccess = true;
            this.Messages.Add(message);
        }

        /// <summary>
        /// Sets the result as success and adds a message plus the result.
        /// </summary>
        /// <param name="message">The message to be added.</param>
        /// <exception cref="ArgumentNullException">
        /// Raised if <paramref name="message"/> is null.
        /// </exception>
        public void SetFailure(string message)
        {
            Require.IsNotNullOrEmpty(nameof(message), message);
            this.IsSuccess = false;
            this.Messages.Add(message);
        }
    }
}
