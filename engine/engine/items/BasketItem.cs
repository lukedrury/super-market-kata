namespace engine.items
{
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
}