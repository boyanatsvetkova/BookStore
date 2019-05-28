using BookStore.ViewModels.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class CatalogModel : INameQuantity
    {
        [RegularExpression(@"^(.+)$")]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double? Price { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int? Quantity { get; set; }

        [JsonIgnore]
        public CategoryModel Category { get; set; }
    }
}
