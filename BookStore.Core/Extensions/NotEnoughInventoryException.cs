using BookStore.ViewModels.Interfaces;
using System;
using System.Collections.Generic;

namespace BookStore.Core.Extensions
{
    public class NotEnoughInventoryException : Exception
    {
        public NotEnoughInventoryException(IEnumerable<INameQuantity> missing)
            :base("NotEnoughInventoryException")
        {
            Missing = missing;
        }

        public IEnumerable<INameQuantity> Missing { get; }
    }
}
