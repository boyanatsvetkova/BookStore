using BookStore.Contracts;
using BookStore.Contracts.ShoppingCart;
using BookStore.Models;

using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services
{
    public class StoreService : IStore
    {
        private readonly IImporter _importer;
        private readonly IShoppingCart _shoppingCart;

        public StoreService(IImporter importer, IShoppingCart shoppingCart)
        {
            _importer = importer;
            _shoppingCart = shoppingCart;
        }

        public IModelError Import(string catalogAsJson)
        {
            return _importer.Import(catalogAsJson);
        }

        public int Quantity(string name)
        {
            return BooksStoreModel.Catalogs.FirstOrDefault(b => b.Name == name).Quantity;
        }

        public decimal Buy(params string[] basketByNames)
        {
            return _shoppingCart.Buy(basketByNames);
        }
    }
}
