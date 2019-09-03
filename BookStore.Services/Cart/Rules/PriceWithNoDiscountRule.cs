using BookStore.Contracts.ShoppingCart;

namespace BookStore.Services.Cart.Rules
{
    public class PriceWithNoDiscountRule : IPriceRule
    {
        public decimal CalculatePrice(IShoppingCartCatalogDto catalog)
        {
            return catalog.Quantity * catalog.Price;
        }

        public bool isMatch(IShoppingCartCatalogDto catalog)
        {
            return catalog.IsDiscountApplied;
        }
    }
}
