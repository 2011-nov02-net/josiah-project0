using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    class Product
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
        public Operators+
    }
}
