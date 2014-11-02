using engine.core;
using engine.rules;
using NUnit.Framework;

namespace engine.tests
{
    [TestFixture]
    public class PercentageDiscountTests
    {
        private Basket m_InputBasket;

        [Test]
        public void EmptyBasket()
        {
            var rule = new PercentageDiscount("item", 100);

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [SetUp]
        public void Setup()
        {
            m_InputBasket = new Basket();
        }
    }

    public class PercentageDiscount : Rule
    {
        public PercentageDiscount(string item, int percentage)
        {

        }

        public override Basket Apply(Basket basket)
        {
            return null;
        }
    }
}