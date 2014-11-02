using System.Collections;
using NUnit.Framework;

namespace engine.tests
{
    [TestFixture]
    public class BuyOneGetOneFreeRuleTests
    {
        private Basket m_InputBasket;

        [Test]
        public void EmptyBasket()
        {
            var rule = new BuyOneGetOneFreeRule("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithSingleUnrelatedItem()
        {
            m_InputBasket.Add("apple", 1);
            var rule = new BuyOneGetOneFreeRule("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("apple", 1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithMultipleUnrelatedItems()
        {
            m_InputBasket.Add("apple", 1, 3);
            var rule = new BuyOneGetOneFreeRule("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("apple", 1, 3);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithSingleRelatedItem()
        {
            m_InputBasket.Add("item", 1);
            var rule = new BuyOneGetOneFreeRule("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("item", 1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [SetUp]
        public void Setup()
        {
            m_InputBasket = new Basket();
        }
    }
}