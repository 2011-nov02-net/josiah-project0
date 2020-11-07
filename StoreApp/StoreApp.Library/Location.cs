using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location
    {
        public string Address { get; }

        private Dictionary<Product, int> _inventory = new Dictionary<Product, int>();
        public Dictionary<Product, int> Inventory
        {
            get { return Inventory; }
        }

        public bool SellItem(Dictionary<Product, int> items)
        {

        }

    }
}
