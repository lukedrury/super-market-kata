using System.Collections.Generic;
using NUnit.Framework;

namespace engine.tests
{
    [TestFixture]
    public class SupermarketAcceptanceTests
    {
        [Test]
        public void EmptyBasket()
        {
            var till = new Till();
            var basket = new Basket();

            var total = till.CalculatePrice(basket);

            var expected = 0;
            Assert.That(total, Is.EqualTo(expected));
        }

        [Test]
        public void SingleItemInBasket()
        {
            var till = new Till();
            var basket = new Basket();
            basket.Add("pennySweet", 1);

            var total = till.CalculatePrice(basket);

            var expected = 1;
            Assert.That(total, Is.EqualTo(expected));
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
            return 0;
        }
    }
}