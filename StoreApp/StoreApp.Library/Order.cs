using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    class Order
    {
        private List<Product> products = new List<Product>();
        public Location Location { get; }
        public Customer Customer { get; }
        public DateTime Time { get; }

        public Order(Location l, Customer c, Amount
        {
            Amount = a;
        }

    }
}
