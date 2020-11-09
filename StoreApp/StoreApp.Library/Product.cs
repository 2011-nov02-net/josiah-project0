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

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public Product(string name, double price, int amount)
        {
            Name = name;
            Price = price;
            Amount = amount;
        }
        public override bool Equals(object obj)
        {
            return (((Product)obj).Name == this.Name);
        }
        public static bool operator ==(Product c1, Product c2)
        {
            if (c1.Equals(c2)) return true;
            return false;
        }
        public static bool operator !=(Product c1, Product c2)
        {
            return (!(c1 == c2));
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
