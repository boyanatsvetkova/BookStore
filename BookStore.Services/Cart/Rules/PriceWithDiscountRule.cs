using BookStore.Contracts.ShoppingCart;

namespace BookStore.Services.Cart.Rules
{
    public class PriceWithDiscountRule : IPriceRule
    {
        public decimal CalculatePrice(IShoppingCartCatalogDto catalog)
        {
            decimal bookWithDiscountPrice =  catalog.Price * (1 - catalog.Discount);
            catalog.Quantity--;

            PriceWithNoDiscountRule noDiscountRule = new PriceWithNoDiscountRule();           
            decimal booksWitNoDiscountPrice = noDiscountRule.CalculatePrice(catalog);

            return bookWithDiscountPrice + booksWitNoDiscountPrice;
        }

        public bool isMatch(IShoppingCartCatalogDto catalog)
        {
            return catalog.IsDiscountApplied;
        }
    }
}
