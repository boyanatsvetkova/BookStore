using BookStore.Core;
using BookStore.Services.Contracts;
using BookStore.ViewModels;
using BookStore.ViewModels.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services
{
    public class StoreService : IStore
    {
        public static BookStoreModel BookStore { get; private set; }

        static StoreService()
        {
            BookStore = new BookStoreModel();
        }

        public double Buy(params string[] basketNames)
        {
            Dictionary<string, int> shoppingCart = new Dictionary<string, int>();
            HashSet<CatalogModel> books = new HashSet<CatalogModel>();
            foreach (var name in basketNames)
            {
                CatalogModel book = BookStore.Catalogs.Where(b => b.Name == name).FirstOrDefault();
                if (!shoppingCart.ContainsKey(name))
                {
                    shoppingCart.Add(name, 1);
                    books.Add(book);
                }
                else
                {
                    shoppingCart[name]++;
                }
            }

            // Check if there are enough books in each category in stock
            List<INameQuantity> itemsNotFound = new List<INameQuantity>();
            foreach (var item in shoppingCart)
            {
                int inStock = books.FirstOrDefault(i => i.Name == item.Key).Quantity.Value;
                if (inStock < item.Value)
                {
                    itemsNotFound.Add(new NameQuantityModel { Name = item.Key, Quantity = inStock });
                }
            }

            if (itemsNotFound.Count() > 0)
            {
                throw new NotEnoughInventoryException(itemsNotFound);
            }

            if (books.Count() == 1 && shoppingCart.FirstOrDefault().Value == 1)
            {
                return books.FirstOrDefault().Price.Value;
            }

            // Check if all books belong to the same Category and there is only one copy of them
            CategoryModel category = books.FirstOrDefault().Category;
            double priceOfBasket = 0;
            if (shoppingCart.All(b => b.Value == 1) && books.All(b => b.Category.Name == category.Name))
            {
                foreach (var book in books)
                {
                    priceOfBasket += book.Price.Value;
                }

                priceOfBasket = priceOfBasket - (priceOfBasket * category.Discount.Value);
                return priceOfBasket;
            }

            // A very complex basket calculations!!! :))))
            foreach (var book in books)
            {
                int bookQuantityToBuy = shoppingCart[book.Name];
                if (bookQuantityToBuy != 1)
                {
                    priceOfBasket += book.Price.Value * (1 - book.Category.Discount.Value);
                    priceOfBasket += (book.Price.Value * (bookQuantityToBuy - 1));
                }
                else
                {
                    if (books.Any(b => b.Category.Name == book.Category.Name && b.Name != book.Name))
                    {
                        priceOfBasket += book.Price.Value * (1 - book.Category.Discount.Value);
                        priceOfBasket += (book.Price.Value * (bookQuantityToBuy - 1));
                    }
                    else
                    {
                        priceOfBasket += book.Price.Value;
                    }                    
                }
            }

            return priceOfBasket;
        }

        public void Import(string catalogAsJson)
        {
            // Throw exception if Json string cannot be deserialized

            JObject jsonObject = JObject.Parse(catalogAsJson);
            IList<JToken> categoryResults = jsonObject["Category"].Children().ToList();
            IList<JToken> catalogResults = jsonObject["Catalog"].Children().ToList();

            BookStore.Categories = new List<CategoryModel>();
            BookStore.Catalogs = new List<CatalogModel>();
            foreach (JToken result in categoryResults)
            {
                CategoryModel category = JsonConvert.DeserializeObject<CategoryModel>(result.ToString());
                if (!BookStore.Categories.Any(c => c.Name == category.Name))
                {
                    BookStore.Categories.Add(category);
                }
            }

            foreach (JToken result in catalogResults)
            {
                CatalogModel catalog = JsonConvert.DeserializeObject<CatalogModel>(result.ToString());
                if (!BookStore.Catalogs.Any(b => b.Name == catalog.Name))
                {
                    catalog.Category = BookStore.Categories.FirstOrDefault(c => c.Name == result["Category"].Value<string>());
                    BookStore.Catalogs.Add(catalog);
                }
            }
        }

        public bool IsBookAvailable(string name)
        {
            CatalogModel book = BookStore.Catalogs.Where(b => b.Name == name).FirstOrDefault();

            if (book == null)
            {
                return false;
            }

            return true;
        }

        public int Quantity(string name)
        {
            return BookStore.Catalogs.Where(b => b.Name == name).FirstOrDefault().Quantity.Value;
        }
    }
}
