using System.Collections.Generic;

namespace BookStore.Models
{
    public static class BooksStoreModel
    {
        static BooksStoreModel()
        {
            Categories = new List<CategoryModel>();
            Catalogs = new List<CatalogModel>();
        }

        public static IList<CategoryModel> Categories { get; set; }

        public static IList<CatalogModel> Catalogs { get; set; }
    }
}