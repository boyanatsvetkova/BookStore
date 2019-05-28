using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.ViewModels.Interfaces
{
    public interface INameQuantity
    {
        string Name { get; }

        int? Quantity { get; }
    }
}
