using System;
using engine.core;

namespace engine.rules
{
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
                    var discountToApply =  -(int) Math.Ceiling(item.UnitPrice * ((double) m_Percentage / 100));
                    discountedBasket.Add(string.Format("{0}:{1}% discount", m_Item, m_Percentage), discountToApply);
                }
            }

            return discountedBasket;
        }
    }
}