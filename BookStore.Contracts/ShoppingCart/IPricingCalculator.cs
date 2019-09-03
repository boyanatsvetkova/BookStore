namespace BookStore.Contracts.ShoppingCart
{
    public interface IPricingCalculator
    {
        decimal CalculatePriceOfBooks(IShoppingCartCatalogDto catalog);
    }
}
