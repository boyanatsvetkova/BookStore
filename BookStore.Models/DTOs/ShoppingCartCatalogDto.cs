using BookStore.Contracts.ShoppingCart;
using System;

namespace BookStore.Models.DTOs
{
    public class ShoppingCartCatalogDto : IShoppingCartCatalogDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsDiscountApplied { get; set; }
    }
}
