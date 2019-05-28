using BookStore.ViewModels.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class CatalogModel : INameQuantity
    {
        [Required]
        [RegularExpression(@"^(.+)$")]
        public string Name { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        [JsonIgnore]
        public CategoryModel Category { get; set; }
    }
}
