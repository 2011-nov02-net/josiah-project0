using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }
        
        public Customer(string f, string l)
        {
            FirstName = f; LastName = l;
        }
    }
}
