using System;

using NUnit.Framework;
using Moq;
using FluentAssertions;
using DrinksMachine.Model;
using Common;
using DrinksMachine.Logic.Interface;
using DrinksMachine.Logic.Executors;
using System.Collections.Generic;

namespace DrinksMachine.Logic.Test
{
    [TestFixture]
    public class DrinkExecutorTest
    {
        public DrinkExecutor Target { get; set; }

        public Drink Drink { get; set; }

        public Drink Template { get; set; }

        public Mock<IDrinkCommand> MockCommand { get; set; }

        [SetUp]
        public void Setup()
        {
            this.MockCommand = new Mock<IDrinkCommand>();
            this.MockCommand.Setup(x => x.PerformCommand(It.IsAny<Drink>(), It.IsAny<CommandResult>()))
                            .Callback<Drink, CommandResult>((d, cr) => 
                            {
                                cr.Drink = this.Template;
                                cr.SetSuccess("Test Success.");
                            });


            this.Target = new DrinkExecutor();
            this.Drink = new Drink("Test");
            this.Template = new Drink("TestTemplate");
            this.Template.ServingActions.Add(this.MockCommand.Object);
            this.Template.IsHot = true;
            this.Template.Ingredients.Add("TestIngredient", 1);
        }

        [Test]
        public void ExecuteCommandsRejectsNullTemplate()
        {
            Assert.Throws<ArgumentNullException>(() => this.Target.ExecuteCommands(null), "Should reject null template.");
        }

        [Test]
        public void ExecuteCommandsRejectsWrongTemplateType()
        {
            var template = new ColdDrink();
            Assert.Throws<ArgumentException>(() => this.Target.ExecuteCommands(template), "Should reject incorrect template type.");
        }

        [Test]
        public void ExecuteCommandsSuccess()
        {
            var result = this.Target.ExecuteCommands(this.Template);
            result.IsSuccess.Should().BeTrue("Command should execute successfully.");
        }

        private class ColdDrink : IBeverage, IBeverageTemplate
        {
            public string Name { get; set; }
            public bool IsHot { get; set; }
            public int AmountOfWaterRequired { get; set; }
            public int Temperature { get; set; }
            public Dictionary<string, int> Ingredients { get; set; }
            public List<IDrinkCommand> ServingActions { get; private set; }

            public ColdDrink()
            {
                this.ServingActions = new List<IDrinkCommand>();
            }
        }
    }
}
