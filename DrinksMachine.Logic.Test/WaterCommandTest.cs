using System;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using DrinksMachine.Model;
using Common;
using DrinksMachine.Logic.Interface;

namespace DrinksMachine.Logic.Test
{
    [TestFixture]
    public class WaterCommandTest
    {
        public WaterCommand Target { get; set; }

        public Mock<IMachineSensorReadings> MockMachineSensorReadings { get; set; }
        public Drink Drink { get; set; }
        public Drink DrinkTemplate { get; set; }


        [SetUp]
        public void Setup()
        {
            this.MockMachineSensorReadings = new Mock<IMachineSensorReadings>();
            this.Drink = new Drink();
            this.DrinkTemplate = new Drink();
            this.Target = new WaterCommand(this.MockMachineSensorReadings.Object);
        }

        [Test]
        public void ImplementsCorrectAPI()
        {
            var temp = new WaterCommand(this.MockMachineSensorReadings.Object);
            Assert.IsNotNull(temp as IDrinkCommand, "Must implement the correct interface.");
        }

        [Test]
        public void ConstructorRejectsNullMachineSensorReadings()
        {
            Assert.Throws<ArgumentNullException>(() => new WaterCommand(null), "Constructor must reject null sensor readings.");
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
        public void PerformCommandMatchesCorrectTemperature()
        {
            var waterAmount = 5;
            this.DrinkTemplate.Temperature = 100;
            this.DrinkTemplate.AmountOfWaterRequired = waterAmount;
            this.MockMachineSensorReadings.SetupGet(x => x.WaterTemperature)
                                          .Returns(100);

            this.MockMachineSensorReadings.SetupGet(x => x.WaterTankLevel)
                                          .Returns(waterAmount);

            this.Target = new WaterCommand(this.MockMachineSensorReadings.Object);
            var result = new CommandResult(this.Drink);
            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeTrue();
            result.Messages.Should().Contain(string.Format(CommandMessages.WaterCommandTemperatureSuccess, this.DrinkTemplate.Temperature), "Water command success message incorrect.");
            result.Drink.Temperature.Should().Be(this.DrinkTemplate.Temperature, "Result drink not at the correct temperature.");
        }

        [Test]
        public void PerformCommandAdjustsToTheCorrectTemperature()
        {
            var waterAmount = 5;
            this.DrinkTemplate.Temperature = 100;
            this.DrinkTemplate.AmountOfWaterRequired = waterAmount;
            this.MockMachineSensorReadings.SetupGet(x => x.WaterTemperature)
                                          .Returns(60);

            this.MockMachineSensorReadings.SetupGet(x => x.WaterTankLevel)
                                          .Returns(waterAmount);

            this.MockMachineSensorReadings.Setup(x => x.IncreaseWaterTemperature(It.IsAny<int>()))
                                          .Callback<int?>((i) => this.MockMachineSensorReadings.SetupGet(x => x.WaterTemperature).Returns(i ?? 0));

            this.Target = new WaterCommand(this.MockMachineSensorReadings.Object);
            var result = new CommandResult(this.Drink);
            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeTrue();
            result.Messages.Should().Contain(string.Format(CommandMessages.WaterCommandTemperatureSuccess, this.DrinkTemplate.Temperature), "Water command success message incorrect.");
            result.Drink.Temperature.Should().Be(this.DrinkTemplate.Temperature, "Result drink not at the correct temperature.");
        }

        [Test]
        public void PerformCommandRejectsBadTemperature()
        {
            var waterAmount = 5;
            this.DrinkTemplate.Temperature = 100;
            this.DrinkTemplate.AmountOfWaterRequired = waterAmount;
            this.MockMachineSensorReadings.SetupGet(x => x.WaterTemperature)
                                          .Returns(90);

            this.DrinkTemplate.AmountOfWaterRequired = waterAmount;

            this.MockMachineSensorReadings.SetupGet(x => x.WaterTankLevel)
                                          .Returns(waterAmount);

            this.Target = new WaterCommand(this.MockMachineSensorReadings.Object);
            var result = new CommandResult(this.Drink);
            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeFalse();
            result.Messages.Should().Contain(string.Format(CommandMessages.WaterCommandTemperatureFailure, this.DrinkTemplate.Temperature), "Water command temperature failure message incorrect.");
        }


        [Test]
        public void PerformCommandRejectsLackOfWater()
        {
            var waterAmount = 4;
            var waterRequired = 5;
            this.DrinkTemplate.Temperature = 100;
            this.MockMachineSensorReadings.SetupGet(x => x.WaterTemperature)
                                          .Returns(100);

            this.DrinkTemplate.AmountOfWaterRequired = waterRequired;

            this.MockMachineSensorReadings.SetupGet(x => x.WaterTankLevel)
                                          .Returns(waterAmount);

            this.Target = new WaterCommand(this.MockMachineSensorReadings.Object);
            var result = new CommandResult(this.Drink);
            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeFalse();
            result.Messages.Should().Contain(string.Format(CommandMessages.WaterCommandWaterAmountFailure, waterRequired, waterAmount), "Water command water amount failure message incorrect.");
        }
    }
}
