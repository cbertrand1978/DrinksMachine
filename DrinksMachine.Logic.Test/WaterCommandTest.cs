using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace DrinksMachine.Logic.Test
{
    [TestFixture]
    public class WaterCommandTest
    {
        public WaterCommand Target { get; set; }
        public Mock<IDrinkCommand> MockSuccessor { get; set; }

        [SetUp]
        public void Setup()
        {
            this.MockSuccessor = new Mock<IDrinkCommand>();
        }

        [Test]
        public void ImplementsCorrectAPI()
        {
            var temp = new WaterCommand(MockSuccessor.Object);
            //(temp as IDrinkCommand).Should().NotBeNull("Must implement the correct interface.");
            Assert.IsNotNull(temp as IDrinkCommand, "Must implement the correct interface.");
        }

        [Test]
        public void ConstructorRejectsNullSuccessor()
        {
            Assert.Throws<ArgumentNullException>(() => new WaterCommand(null), "Constructor must reject null successor.");
        }
    }
}
