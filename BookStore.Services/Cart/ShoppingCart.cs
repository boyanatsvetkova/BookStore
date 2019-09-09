using BookStore.Contracts.ShoppingCart;
using BookStore.Infrastructure.Exceptions;
using BookStore.Models;
using System.Collections.Generic;

namespace BookStore.Services.Cart
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IPricingCalculator _pricingCalculator;

        public ShoppingCart(IPricingCalculator pricingCalculator)
        {
            _pricingCalculator = pricingCalculator;
        }

        public decimal Buy(params string[] basketByNames)
        {
            Dictionary<string, IList<IShoppingCartCatalogDto>> basket = new Dictionary<string, IList<IShoppingCartCatalogDto>>();
            foreach (var name in basketByNames)
            {
               
            }

            throw new NotEnoughInventoryException();

            return 0;
        }
    }
}
