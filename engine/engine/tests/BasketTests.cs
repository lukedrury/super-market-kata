using System;
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
    }
}