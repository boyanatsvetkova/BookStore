using BookStore.Contracts;

namespace BookStore.Models
{
    public class ModelError : IModelError
    {
        public ModelError(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }

        public string Message { get; set;  }
    }
}
