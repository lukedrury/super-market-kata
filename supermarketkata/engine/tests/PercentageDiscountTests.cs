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

        [Test]
        public void BasketWithMultipleRelatedAndUnrelatedItems()
        {
            var rule = new PercentageDiscount("discountedItem", 100);
            m_InputBasket.Add("discountedItem", 1, 3);
            m_InputBasket.Add("apple", 1, 3);

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("discountedItem", 1, true), 3);
            expectedBasket.Add("apple", 1, 3);
            expectedBasket.Add(string.Format("discountedItem:{0}% discount", 100), -1, 3);
            Assert.That(returnedBasket, Is.EquivalentTo(expectedBasket));
        }

        [Test]
        public void BasketWithHalfPriceDiscountRule()
        {
            var rule = new PercentageDiscount("discountedItem", 50);
            m_InputBasket.Add("discountedItem", 2);

            var returnedBasket = rule.Apply(m_InputBasket);

            var expectedBasket = new Basket();
            expectedBasket.Add(new BasketItem("discountedItem", 2, true));
            expectedBasket.Add(string.Format("discountedItem:{0}% discount", 50), -1);
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
        private readonly string m_Item;
        private readonly int m_Percentage;

        public PercentageDiscount(string item, int percentage)
        {
            m_Item = item;
            m_Percentage = percentage;
        }

        public override Basket Apply(Basket basket)
        {
            var discountedBasket = new Basket();

            foreach (var item in basket)
            {
                discountedBasket.Add(item);

                if (item.Item.Equals(m_Item) && !item.UsedInOffer)
                {
                    var discountToApply = (int) (-item.UnitPrice * ((double) m_Percentage / 100));
                    discountedBasket.Add(string.Format("{0}:{1}% discount", m_Item, m_Percentage), discountToApply);
                }
            }

            return discountedBasket;
        }
    }
}