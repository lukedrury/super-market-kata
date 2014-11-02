using System;
using engine.core;
using NUnit.Framework;

namespace engine.tests
{
    [TestFixture]
    public class BasketTests
    {
        [Test]
        public void ThrowsWhenQuantityIsNegativeNumber()
        {
            var basket = new Basket();
            Assert.Throws<ArgumentOutOfRangeException>(() => basket.Add("item", 1, -1));
        }

        [Test]
        public void ThrowsWhenQuantityIsZero()
        {
            var basket = new Basket();
            Assert.Throws<ArgumentOutOfRangeException>(() => basket.Add("item", 1, 0));
        }
    }
}