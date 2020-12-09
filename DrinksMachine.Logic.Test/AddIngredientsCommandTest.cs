using System;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using DrinksMachine.Model;
using Common;

namespace DrinksMachine.Logic.Test
{
    [TestFixture]
    public class AddIngredientsCommandTest
    {
        public AddIngredientsCommand Target { get; set; }

        public Mock<IMachineSensorReadings> MockMachineSensorReadings { get; set; }
        public Drink Drink { get; set; }
        public Drink DrinkTemplate { get; set; }


        [SetUp]
        public void Setup()
        {
            this.MockMachineSensorReadings = new Mock<IMachineSensorReadings>();
            this.Drink = new Drink();
            this.DrinkTemplate = new Drink();
            this.Target = new AddIngredientsCommand(this.MockMachineSensorReadings.Object);
        }

        [Test]
        public void ImplementsCorrectAPI()
        {
            var temp = new AddIngredientsCommand(this.MockMachineSensorReadings.Object);
            Assert.IsNotNull(temp as IDrinkCommand, "Must implement the correct interface.");
        }

        [Test]
        public void ConstructorRejectsNullMachineSensorReadings()
        {
            Assert.Throws<ArgumentNullException>(() => new AddIngredientsCommand(null), "Constructor must reject null sensor readings.");
        }

        [Test]
        public void PerformCommandRejectsNullDrink()
        {
            var result = new CommandResult();
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
        public void PerformCommandRejectsEmptyIngredients()
        {

        }
    }
}
