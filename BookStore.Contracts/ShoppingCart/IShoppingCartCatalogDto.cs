namespace BookStore.Contracts.ShoppingCart
{
    public interface IShoppingCartCatalogDto
    {
        string Name { get; set; }

        int Quantity { get; set; }

        decimal Price { get; set; }

        decimal Discount { get; set; }

        bool IsDiscountApplied { get; set; }
    }
}
