using System.Collections.Generic;

namespace BookStore.ViewModels
{
    public class BookStoreModel
    {
        public IList<CategoryModel> Categories { get; set; }

        public IList<CatalogModel> Catalogs { get; set; }
    }
}
