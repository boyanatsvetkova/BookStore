namespace BookStore.Models
{
    public class CatalogModel
    {
        public string Name { get; set; }

        public CategoryModel Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
