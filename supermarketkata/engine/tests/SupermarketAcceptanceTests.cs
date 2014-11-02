using System.Collections.Generic;
using engine.core;
using engine.rules;
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

        [Test]
        public void MultipleItemsInBasket()
        {
            m_Basket.Add("pennySweet", 1, 3);

            var total = m_Till.CalculatePrice(m_Basket);

            var expected = 3;
            Assert.That(total, Is.EqualTo(expected));
        }

        [Test]
        public void BuyOneGetOneFree()
        {
            m_Basket.Add("pennySweet", 1, 2);
            var rules = new List<Rule>();
            rules.Add(new BuyOneGetOneFree("pennySweet"));

            var total = m_Till.CalculatePrice(m_Basket, rules);

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
}