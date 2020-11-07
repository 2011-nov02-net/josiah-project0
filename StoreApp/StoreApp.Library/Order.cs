using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        private Dictionary<Product, int> _items = new Dictionary<Product, int>();
        public Dictionary<Product, int> Items
        {
            get { return _items; }
        }

        public Location Location { get; }
        public Customer Customer { get; }
        public DateTime Time { get; }

        public Order(Location l, Customer c, Dictionary<Product, int> d)
        {
            Location = l; Customer = c; Time = DateTime.Now; _items = d;
        }

    }
}
