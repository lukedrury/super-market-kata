using engine.core;
using engine.items;

namespace engine.rules
{
    public class BuyOneGetOneFreeRule : Rule
    {
        private readonly string m_Item;

        public BuyOneGetOneFreeRule(string item)
        {
            m_Item = item;
        }

        public override Basket Apply(Basket basket)
        {
            var updatedBasket = new Basket();

            BasketItem previousApplicableItem = null;
            foreach (var item in basket)
            {
                if (item.Item.Equals(m_Item))
                {
                    if (previousApplicableItem == null)
                    {
                        previousApplicableItem = item;
                    }
                    else
                    {
                        updatedBasket.Add(new BasketItem(previousApplicableItem.Item, previousApplicableItem.UnitPrice, true), 2);
                        updatedBasket.Add(new BasketItem(string.Format("{0}:bogof", previousApplicableItem.Item), -1, true));
                        previousApplicableItem = null;
                    }
                }
                else
                {
                    updatedBasket.Add(item);
                }
            }

            if (previousApplicableItem != null)
            {
                updatedBasket.Add(previousApplicableItem);
            }

            return updatedBasket;
        }
    }
}