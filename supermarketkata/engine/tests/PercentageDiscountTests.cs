using engine.core;
using engine.items;
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
            var rule = new PercentageDiscount("discountedItem", 100);

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithOneUnrelatedItem()
        {
            var rule = new PercentageDiscount("discountedItem", 100);
            m_InputBasket.Add("apple", 1);

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add("apple", 1);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithOneRelatedItem()
        {
            var rule = new PercentageDiscount("discountedItem", 100);
            m_InputBasket.Add("discountedItem", 1);

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("discountedItem", 1, true));
            expectedBasket.Add(string.Format("discountedItem:{0}% discount", 100), -1);
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
            return basket;
        }
    }
}