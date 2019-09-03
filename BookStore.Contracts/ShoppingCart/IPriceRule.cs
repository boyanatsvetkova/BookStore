namespace BookStore.Contracts.ShoppingCart
{
    public interface IPriceRule
    {
        bool isMatch(IShoppingCartCatalogDto catalog);

        decimal CalculatePrice(IShoppingCartCatalogDto catalog);
    }
}
