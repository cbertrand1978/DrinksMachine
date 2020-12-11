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
        public void PerformCommandRejectsNullIngredients()
        {
            var result = new CommandResult();
            this.Drink.Ingredients = null;
            result.Drink = this.Drink;
            this.DrinkTemplate.Ingredients = null;
            Assert.Throws<ArgumentNullException>(() => this.Target.PerformCommand(this.DrinkTemplate, result), "Command must reject an empty ingredient list.");
        }

        [Test]
        public void PerformCommandRejectsEmptyIngredients()
        {
            var result = new CommandResult();
            result.Drink = this.Drink;
            Assert.Throws<ArgumentException>(() => this.Target.PerformCommand(this.DrinkTemplate, result), "Command must reject an empty ingredient list.");
        }

        [Test]
        public void PerformCommandRejectsIngredientNotFound()
        {
            var ingredientName = "Coffee";
            var availableIngredient = "Tea";
            var ingredientAmount = 2;
            var ingredients = new Dictionary<string, int>() { { new string(ingredientName), ingredientAmount } };
            var availableIngredients = new Dictionary<string, int>() { { availableIngredient, ingredientAmount } };
            var errorMessage = string.Format(CommandMessages.AddIngredientCannotFindIngredient, ingredientName);
            this.DrinkTemplate.Ingredients = ingredients; ;


            this.MockMachineSensorReadings.SetupGet(x => x.IngredientSupply)
                                          .Returns(availableIngredients);

            var result = new CommandResult(this.Drink);
            this.Target = new AddIngredientsCommand(this.MockMachineSensorReadings.Object);

            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeFalse("Command must reject insufficient ingredients.");
            result.Messages.Should().Contain(errorMessage, "Add Ingredient insufficient supply error message incorrect.");
        }

        [Test]
        public void PerformCommandRejectsInsufficientIngredients()
        {
            var ingredientName = "Coffee";
            var coffeeAmount = 2;
            var ingredientSupplyAvailable = 1;
            var ingredients = new Dictionary<string, int>() { { ingredientName, coffeeAmount } };
            var availableIngredients = new Dictionary<string, int>() { { ingredientName, ingredientSupplyAvailable } };
            var errorMessage = string.Format(CommandMessages.AddIngredientInsuffientIngredientError, coffeeAmount, ingredientName, ingredientSupplyAvailable);
            this.DrinkTemplate.Ingredients = ingredients; ;


            this.MockMachineSensorReadings.SetupGet(x => x.IngredientSupply)
                                          .Returns(availableIngredients);

            var result = new CommandResult(this.Drink);
            this.Target = new AddIngredientsCommand(this.MockMachineSensorReadings.Object);

            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeFalse("Command must reject insufficient ingredients.");
            result.Messages.Should().Contain(errorMessage, "Add Ingredient insufficient supply error message incorrect.");
        }

        [Test]
        public void PerformCommandSuccessful()
        {
            var ingredientName = "Coffee";
            var coffeeAmount = 2;
            var ingredientSupplyAvailable = 2;
            var ingredients = new Dictionary<string, int>() { { ingredientName, coffeeAmount } };
            var availableIngredients = new Dictionary<string, int>() { { ingredientName, ingredientSupplyAvailable } };
            this.DrinkTemplate.Ingredients = ingredients;
            var successMessage = string.Format(CommandMessages.AddIngredientSuccess, string.Join(", ", this.DrinkTemplate.Ingredients.Select(x => $"{x.Key}: {x.Value}")));


            this.MockMachineSensorReadings.SetupGet(x => x.IngredientSupply)
                                          .Returns(availableIngredients);

            var result = new CommandResult(this.Drink);
            this.Target = new AddIngredientsCommand(this.MockMachineSensorReadings.Object);

            this.Target.PerformCommand(this.DrinkTemplate, result);

            result.IsSuccess.Should().BeTrue("Command should be successful with correct supplies.");
            result.Messages.Should().Contain(successMessage, "Add Ingredient success message incorrect.");
            result.Drink.Ingredients.All(kvp => kvp.Value.Should().Equals(this.DrinkTemplate.Ingredients[kvp.Key]));
        }
    }
}
