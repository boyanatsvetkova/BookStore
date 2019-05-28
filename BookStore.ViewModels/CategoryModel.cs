using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.ViewModels
{
    public class CategoryModel
    {
        [RegularExpression(@"^(.+)$")]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double? Discount { get; set;  }
    }
}
