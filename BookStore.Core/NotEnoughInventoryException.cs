using BookStore.ViewModels.Interfaces;
using System;
using System.Collections.Generic;

namespace BookStore.Core
{
    public class NotEnoughInventoryException : Exception
    {
        public NotEnoughInventoryException(IEnumerable<INameQuantity> missing)
            :base("Not enough inventory.")
        {
            Missing = missing;
        }

        public IEnumerable<INameQuantity> Missing { get; }
    }
}
