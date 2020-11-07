using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Product
    {
        public string Name { get; }

        private double _price;
        public double Price {
            get { return _price * _discount; }
            private set { _price = value; }
        }

        private double _discount = 1;

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
        public void AddDiscount(double d)
        {
            _discount = d;
        }
        public void RemoveDiscount()
        {
            _discount = 1;
        }
    }
}
