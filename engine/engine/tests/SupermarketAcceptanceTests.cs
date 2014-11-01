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
    }

    public class Basket
    {
    }

    public class Till
    {
        public int CalculatePrice(Basket basket)
        {
            return -1;
        }
    }
}