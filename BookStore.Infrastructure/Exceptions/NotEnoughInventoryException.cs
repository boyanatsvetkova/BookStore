using BookStore.Contracts;
using System;
using System.Collections.Generic;

namespace BookStore.Infrastructure.Exceptions
{
    public class NotEnoughInventoryException : Exception
    {
        public IEnumerable<INameQuantity> Missing { get; }
    }
}
