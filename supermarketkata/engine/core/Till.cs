using System.Collections.Generic;
using System.Linq;
using engine.rules;

namespace engine.core
{
    public class Till
    {
        public int CalculatePrice(Basket basket)
        {
            return CalculatePrice(basket, new List<Rule>());
        }

        public int CalculatePrice(Basket basket, List<Rule> rules)
        {
            var rulesAppliedBasket = rules.Aggregate(basket, (currentBasket, rule) => rule.Apply(currentBasket));
            return rulesAppliedBasket.Sum(item => item.Price);
        }
    }
}