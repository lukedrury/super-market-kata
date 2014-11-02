using engine.core;
using engine.items;
using engine.rules;
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
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithSingleUnrelatedItem()
        {
            m_InputBasket.Add("apple", 1);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("apple", 1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithMultipleUnrelatedItems()
        {
            m_InputBasket.Add("apple", 1, 3);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("apple", 1, 3);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithSingleRelatedItem()
        {
            m_InputBasket.Add("item", 1);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("item", 1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithTwoRelatedItems()
        {
            m_InputBasket.Add("item", 1, 2);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("item", 1, true), 2);
            expectedBasket.Add("item:bogof", -1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithThreeRelatedItems()
        {
            m_InputBasket.Add("item", 1, 3);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("item", 1));
            expectedBasket.Add(new BasketItem("item", 1, true), 2);
            expectedBasket.Add("item:bogof", -1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithFourRelatedItems()
        {
            m_InputBasket.Add("item", 1, 4);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("item", 1, true), 4);
            expectedBasket.Add("item:bogof", -1, 2);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithMultipleRelatedAndUnrelatedItems()
        {
            m_InputBasket.Add("apple", 1, 3);
            m_InputBasket.Add("item", 1, 3);
            var rule = new BuyOneGetOneFree("item");

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("apple", 1), 3);
            expectedBasket.Add(new BasketItem("item", 1));
            expectedBasket.Add(new BasketItem("item", 1, true), 2);
            expectedBasket.Add("item:bogof", -1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [SetUp]
        public void Setup()
        {
            m_InputBasket = new Basket();
        }
    }
}