using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.ViewModels
{
    public class CategoryModel
    {
        [Required]
        [RegularExpression(@"^(.+)$")]
        public string Name { get; set; }

        [Required]
        public double? Discount { get; set;  }
    }
}
