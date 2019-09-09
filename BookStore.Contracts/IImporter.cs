using System.Collections.Generic;

namespace BookStore.Contracts
{
    public interface IImporter
    {
        IModelError Import(string catalog);
    }
}
