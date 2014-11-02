﻿using System;
using System.Collections;
using System.Collections.Generic;
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
            rules.Add(new BuyOneGetOneFreeRule("pennySweet"));

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

    public abstract class Rule
    {
        public abstract Basket Apply(Basket basket);
    }

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
                var cloneBasketItem = new BasketItem(basketItem.Item, basketItem.UnitPrice, basketItem.UsedInOffer);
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

    public class BasketItem
    {
        public string Item { get; private set; }
        public int UnitPrice { get; private set; }
        public bool UsedInOffer { get; set; }

        public BasketItem(string item, int unitPrice, bool usedInOffer = false)
        {
            Item = item;
            UnitPrice = unitPrice;
            UsedInOffer = usedInOffer;
        }

        protected bool Equals(BasketItem other)
        {
            return string.Equals(Item, other.Item) && UnitPrice == other.UnitPrice;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BasketItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Item != null ? Item.GetHashCode() : 0)*397) ^ UnitPrice;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}p", Item, UnitPrice);
        }
    }

    public class Till
    {
        public int CalculatePrice(Basket basket)
        {
            return CalculatePrice(basket, new List<Rule>());
        }

        public int CalculatePrice(Basket basket, List<Rule> rules)
        {
            var total = 0;

            foreach (var item in basket)
            {
                total += item.UnitPrice;
            }

            return total;
        }
    }
}