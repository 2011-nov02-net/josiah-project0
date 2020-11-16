﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace StoreApp.Library
{
    [DataContract(Name = "Order")]
    public class Order
    {
        [DataMember]
        private List<Product> _items = new List<Product>();
        public List<Product> Items
        {
            get { return _items; }
            private set { _items = value; }
        }
        [DataMember]
        public Location Location { get; private set; }
        [DataMember]
        public Customer Customer { get; private set; }
        [DataMember]
        public DateTime Time { get; private set; }

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
        public Order(Location l, Customer c, List<Product> p, DateTime t)
        {
            Location = l; Customer = c; Time = t; _items = p;
            foreach (var x in p)
            {
                if (x.Amount < 0 || x.Amount > 50)
                {
                    throw new ArgumentException($"Item quantity ({x.Name}:{x.Amount}) out of range");
                }
            }
        }
        private Order() { }
        public string DisplayOrder()
        {
            var items = Items.Select(x => string.Format("{0}{1}{2, -5}", x.Name, " ", x.Amount));
            var price = Items.Select(x => x.Price * x.Amount).Sum();

            return ($"{Customer.FirstName, -5} {Customer.LastName,-10} " +
                $"{Location.Name,-20} {Time,-30} " +
                $"{string.Join("  |  ", items)}" +
                $"{price.ToString("c"), -10}");
        }

    }
}
