namespace BookStore.Services.Contracts
{
    public interface IStore
    {
        void Import(string catalogAsJson, out string errorMessage);

        int Quantity(string name);

        double Buy(params string[] basketNames);

        bool IsBookAvailable(string name);
    }
}
