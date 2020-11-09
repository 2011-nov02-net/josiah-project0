using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Library
{
    public class Order
    {
        private List<Product> _items = new List<Product>();
        public List<Product> Items
        {
            get { return _items; }
        }
        public Location Location { get; }
        public Customer Customer { get; }
        public DateTime Time { get; }

        public Order(Location l, Customer c, List<Product> p)
        {
            Location = l; Customer = c; Time = DateTime.Now; _items = p;
            foreach (var x in p)
            {
                if (x.Amount < 0 || x.Amount > 50)
                {
                    throw new ArgumentException($"Item quantity ({x.Name}:{x.Amount}) out of range");
                }
            }
        }
        public void DisplayOrderToConsole()
        {
            var items = Items.Select(x => string.Format("{0}{1}{2, -5}", x.Name, " ", x.Amount));
            var price = Items.Select(x => x.Price).Sum();

            Console.WriteLine($"{Customer.FirstName, -5} {Customer.LastName,-10} " +
                $"{Location.Name,-20} {Time,-30} " +
                $"{string.Join("  |  ", items)}" +
                $"{price.ToString("c"), -10}");
        }

    }
}
