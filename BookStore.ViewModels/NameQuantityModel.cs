using BookStore.ViewModels.Interfaces;

namespace BookStore.ViewModels
{
    public class NameQuantityModel : INameQuantity
    {
        public string Name { get; set; }

        public int? Quantity { get; set; }
    }
}
