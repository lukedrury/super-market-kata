﻿using System;
using engine.core;

namespace engine.rules
{
    public class PercentageDiscount : Rule
    {
        private readonly string m_ApplicableItemName;
        private readonly double m_Percentage;

        public PercentageDiscount(string applicableItemName, int percentage)
        {
            m_ApplicableItemName = applicableItemName;
            m_Percentage = percentage;
        }

        public override Basket Apply(Basket basket)
        {
            var discountedBasket = new Basket();

            foreach (var item in basket)
            {
                discountedBasket.Add(item);

                if (item.Name.Equals(m_ApplicableItemName) && !item.UsedInOffer)
                {
                    var discountToApply =  Math.Ceiling(item.Price * m_Percentage / 100);
                    discountedBasket.Add(string.Format("{0}:{1}% discount", m_ApplicableItemName, m_Percentage), (int) -discountToApply);
                }
            }

            return discountedBasket;
        }
    }
}