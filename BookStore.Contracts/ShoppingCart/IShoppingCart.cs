namespace BookStore.Contracts.ShoppingCart
{
    public interface IShoppingCart
    {
        decimal Buy(params string[] basketByNames);
    }
}
