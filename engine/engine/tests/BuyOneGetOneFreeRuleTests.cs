using System.Collections;
using NUnit.Framework;

namespace engine.tests
{
    [TestFixture]
    public class BuyOneGetOneFreeRuleTests
    {
        [Test]
        public void EmptyBasket()
        {
            var inputBasket = new Basket();
            var rule = new BuyOneGetOneFreeRule("item");

            var returnedBasket = rule.Apply(inputBasket);

            var expectedBasket = new Basket();
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }
    }
}