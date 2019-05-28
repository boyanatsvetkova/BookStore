namespace BookStore.Services.Contracts
{
    public interface IStore
    {
        void Import(string catalogAsJson);

        int Quantity(string name);

        double Buy(params string[] basketNames);

        bool IsBookAvailable(string name);
    }
}
