using System;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using DrinksMachine.Model;
using Common;
using System.Collections.Generic;
using System.Linq;
using DrinksMachine.Logic.Interface;

namespace DrinksMachine.Logic.Test
{
    [TestFixture]
    public class ServeDrinkCommandTest
    {
        public ServeDrinkCommand Target { get; set; }

        public Drink Drink { get; set; }
        public Drink DrinkTemplate { get; set; }

        public Func<CommandResult, string, bool> ActionToPerform { get; set; }

        public string SuccessMessage = "Drink Served";


        [SetUp]
        public void Setup()
        {
            this.Drink = new Drink();
            this.DrinkTemplate = new Drink();
            this.ActionToPerform = (cr, s) => 
            {
                try
                {
                    Require.IsNotNullOrEmpty(nameof(s), s);
                    cr.SetSuccess(string.Format(s, SuccessMessage));
                    return true;
                }
                catch (ArgumentException)
                {
                    cr.SetFailure(string.Format(s, SuccessMessage));
                    return false;
                }  
            };

            this.Target = new ServeDrinkCommand(this.ActionToPerform);
        }

        [Test]
        public void ImplementsCorrectAPI()
        {
            var temp = new ServeDrinkCommand(this.ActionToPerform);
            Assert.IsNotNull(temp as IDrinkCommand, "Must implement the correct interface.");
        }

        [Test]
        public void ConstructorRejectsNullActionToPerform()
        {
            Assert.Throws<ArgumentNullException>(() => new ServeDrinkCommand(null), "Constructor must reject null sensor readings.");
        }

        [Test]
        public void PerformCommandRejectsNullDrink()
        {
            var result = new CommandResult();
            result.Drink = null;
            Assert.Throws<ArgumentNullException>(() => this.Target.PerformCommand(this.DrinkTemplate, result), "Command must reject a null drink.");
        }

        [Test]
        public void PerformCommandRejectsNullTemplate()
        {
            var result = new CommandResult();
            Assert.Throws<ArgumentNullException>(() => this.Target.PerformCommand(null, result), "Command must reject a null template.");
        }

        [Test]
        public void PerformCommandRejectsNullResult()
        {
            Assert.Throws<ArgumentNullException>(() => this.Target.PerformCommand(this.DrinkTemplate, null), "Command must reject a null result.");
        }

        [Test]
        public void PerformCommandSuccessfullyRuns()
        {
            var result = new CommandResult(this.Drink);

            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeTrue("Command should complete successfully.");
            result.Messages.Should().Contain(string.Format(CommandMessages.ActionToPerformSuccess, SuccessMessage));
        }
    }
}
