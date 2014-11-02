using System;
using System.Collections;
using System.Collections.Generic;
using engine.items;

namespace engine.core
{
    public class Basket : IEnumerable<BasketItem>
    {
        private readonly IList<BasketItem> m_Items = new List<BasketItem>();

        public void Add(string item, int unitPrice, int quantity = 1)
        {
            var basketItem = new BasketItem(item, unitPrice);
            Add(basketItem, quantity);
        }

        public void Add(BasketItem basketItem, int quantity = 1)
        {
            if (quantity < 1) throw new ArgumentOutOfRangeException("quantity", "Quantity must be greater than zero");

            for (var i = 0; i < quantity; i++)
            {
                var cloneBasketItem = new BasketItem(basketItem.Name, basketItem.UnitPrice, basketItem.UsedInOffer);
                m_Items.Add(cloneBasketItem);
            }
        }

        public IEnumerator<BasketItem> GetEnumerator()
        {
            return m_Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}