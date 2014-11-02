using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace engine.tests
{
    [TestFixture]
    public class SupermarketAcceptanceTests
    {
        private Till m_Till;
        private Basket m_Basket;

        [Test]
        public void EmptyBasket()
        {
            var total = m_Till.CalculatePrice(m_Basket);

            var expected = 0;
            Assert.That(total, Is.EqualTo(expected));
        }

        [Test]
        public void SingleItemInBasket()
        {
            m_Basket.Add("pennySweet", 1);

            var total = m_Till.CalculatePrice(m_Basket);

            var expected = 1;
            Assert.That(total, Is.EqualTo(expected));
        }

        [SetUp]
        public void Setup()
        {
            m_Till = new Till();
            m_Basket = new Basket();
        }
    }

    public class Basket : IEnumerable<BasketItem>
    {
        private readonly IList<BasketItem> items = new List<BasketItem>();

        public void Add(string item, int unitPrice)
        {
            items.Add(new BasketItem(item, unitPrice));
        }

        public IEnumerator<BasketItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class BasketItem
    {
        public string Item { get; private set; }
        public int UnitPrice { get; private set; }

        public BasketItem(string item, int unitPrice)
        {
            Item = item;
            UnitPrice = unitPrice;
        }
    }

    public class Till
    {
        public int CalculatePrice(Basket basket)
        {
            var total = 0;

            foreach (var item in basket)
            {
                total += item.UnitPrice;
            }

            return total;
        }
    }
}