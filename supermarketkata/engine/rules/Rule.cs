using engine.core;

namespace engine.rules
{
    public abstract class Rule
    {
        public abstract Basket Apply(Basket basket);
    }
}