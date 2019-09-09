namespace BookStore.API.ApiCustomResponse
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
