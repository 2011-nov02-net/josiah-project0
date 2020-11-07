using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Product
    {
        public string Name { get; }

        public Product(string name)
        {
            Name = name;
        }
    }
}
