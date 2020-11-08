using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var x in d)
            {
                if (x.Value < 0 || x.Value > 50)
                {
                    throw new ArgumentException($"Item quantity ({x.Key}:{x.Value}) out of range");
                }
            }
        }
        public void displayOrder()
        {

            var items = Items.Select(x => string.Format("{0}{1}{2, -5}", x.Key.Name, " ", x.Value));
            var price = Items.Select(x => x.Key.Price * x.Value).Sum();

            Console.WriteLine($"{Customer.FirstName, -5} {Customer.LastName,-10} " +
                $"{Location.Name,-20} {Time,-30} " +
                $"{string.Join("  |  ", items)}" +
                $"{price.ToString("c"), -10}");
        }

    }
}
