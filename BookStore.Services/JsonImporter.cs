using BookStore.Contracts;
using BookStore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.Services
{
    public class JsonImporter : IImporter
    {
        public void Import(string catalog)
        {
            try
            {
                JObject bookStore = JObject.Parse(catalog);

                var bundleAssembly = AppDomain.CurrentDomain.GetAssemblies()
                                .First(x => x.FullName.Contains("BookStore.Infrastructure"));
                var assemblyPath = new Uri(bundleAssembly.CodeBase).LocalPath;
                var jsonPath = Path.Combine(Path.GetDirectoryName(assemblyPath), "Documents", "BookStoreJSONSchema.json");

                JSchema bookStoreSchema = JSchema.Parse(File.ReadAllText(jsonPath));

                IList<string> messages;
                bool isSchemaValid = bookStore.IsValid(bookStoreSchema, out messages);

                if (!isSchemaValid)
                {
                    // Return error messages
                }

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
                    }
                }

                IList<JToken> catalogResults = bookStore["Catalog"].Children().ToList();
                foreach (JToken result in catalogResults)
                {
                    CatalogModel catalogModel = JsonConvert.DeserializeObject<CatalogModel>(result.ToString());
                    if (!BooksStoreModel.Catalogs.Any(b => b.Name == catalogModel.Name))
                    {
                        catalogModel.Category = BooksStoreModel.Categories.FirstOrDefault(c => c.Name == result["Category"].Value<string>());
                        BooksStoreModel.Catalogs.Add(catalogModel);
                    }
                    else
                    {
                        // Error: Inform the user that a duplicate book name is inserted
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
