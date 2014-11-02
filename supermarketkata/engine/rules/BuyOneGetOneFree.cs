using engine.core;
using engine.items;

namespace engine.rules
{
    public class BuyOneGetOneFree : Rule
    {
        private readonly string m_Item;

        public BuyOneGetOneFree(string item)
        {
            m_Item = item;
        }

        public override Basket Apply(Basket basket)
        {
            var updatedBasket = new Basket();

            BasketItem previousApplicableItem = null;
            foreach (var item in basket)
            {
                if (item.Name.Equals(m_Item))
                {
                    if (previousApplicableItem == null)
                    {
                        previousApplicableItem = item;
                    }
                    else
                    {
                        updatedBasket.Add(new BasketItem(previousApplicableItem.Name, previousApplicableItem.Price, true), 2);
                        updatedBasket.Add(new BasketItem(string.Format("{0}:bogof", previousApplicableItem.Name), -1, true));
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