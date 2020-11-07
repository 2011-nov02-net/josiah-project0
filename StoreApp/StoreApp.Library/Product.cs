using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Product
    {
        public string Name { get; }
        public int Amount { get; private set; }

        public Product(string name)
        {
            Name = name;
            Amount = 1;
        }
        public Product(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
        public void removeProduct(Product p)
        {
            if (Amount-p.Amount < 0)
            {
                throw new ArgumentException("Product amount can't be less than 0");
            }
            if (p.Name != this.Name)
            {
                throw new ArgumentException("Can't change amount of different products");
            }
            Amount -= p.Amount;
        }
    }
}
