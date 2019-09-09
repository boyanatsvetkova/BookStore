using BookStore.Contracts;
using BookStore.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Services
{
    public class JsonImporter : IImporter
    {
        public IModelError Import(string catalog)
        {
            // No use of try-catch block
            // Handle exceptions, on one hand, with Exception handler filter for exceptions that occured within the json validation.
            // On the other hand, with global exception handler for any other exceptions.

            JObject bookStore = JObject.Parse(catalog);

            IList<JToken> categoryResults = bookStore["Category"].Children().ToList();
            foreach (JToken result in categoryResults)
            {
                CategoryModel categoryModel = JsonConvert.DeserializeObject<CategoryModel>(result.ToString());
                if (!BooksStoreModel.Categories.Any(c => c.Name == categoryModel.Name))
                {
                    BooksStoreModel.Categories.Add(categoryModel);
                }
                else
                {
                    // Error: inform user that a duplicate category name is inserted

                    return new ModelError($"{categoryModel.GetType().Name}.{nameof(categoryModel.Name)}",
                        $"Category {categoryModel.Name} already exists!");
                }
            }

            IList<JToken> catalogResults = bookStore["Catalog"]
                .Children()
                .ToList();

            foreach (JToken result in catalogResults)
            {
                CatalogModel catalogModel = new CatalogModel();
                string name = (string)result["Name"];
                if (!BooksStoreModel.Catalogs.Any(b => b.Name == name))
                {
                    catalogModel.Name = name;
                    catalogModel.Price = (decimal)result["Price"];
                    catalogModel.Quantity = (int)result["Quantity"];

                    catalogModel.Category = BooksStoreModel.Categories.FirstOrDefault(c => c.Name == result["Category"].Value<string>());
                    BooksStoreModel.Catalogs.Add(catalogModel);
                }
                else
                {
                    // Error: Inform the user that a duplicate book name is inserted
                    return new ModelError($"{catalogModel.GetType().Name}.{nameof(catalogModel.Name)}",
                        $"Catalog {name} already exists!");
                }
            }

            return null;
        }
    }
}
