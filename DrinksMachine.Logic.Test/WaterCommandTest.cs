using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace DrinksMachine.Logic.Test
{
    [TestFixture]
    public class WaterCommandTest
    {
        public WaterCommand Target { get; set; }

        [Test]
        public void ImplementsCorrectAPI()
        {
            var temp = new WaterCommand();
            Assert.IsNotNull(temp as IDrinkCommand, "Must implement the correct interface.");
        }
    }
}
