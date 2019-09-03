using BookStore.Contracts.ShoppingCart;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services.Cart
{
    public class PricingCalculator : IPricingCalculator
    {
        private readonly IList<IPriceRule> _rules;

        public PricingCalculator(IList<IPriceRule> rules)
        {
            _rules = rules;
        }

        public decimal CalculatePriceOfBooks(IShoppingCartCatalogDto catalog)
        {
            return _rules.First(r => r.isMatch(catalog)).CalculatePrice(catalog);
        }
    }
}
