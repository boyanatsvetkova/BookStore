namespace BookStore.API.ApiCustomResponse
{
    public class ValidationError
    {
        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }

        public string Message { get; set; }
    }
}
