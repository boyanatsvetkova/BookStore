namespace BookStore.Contracts
{
    public interface IModelError
    {
        string Field { get; set; }

        string Message { get; set; }
    }
}
