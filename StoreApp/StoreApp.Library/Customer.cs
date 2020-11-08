using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
        public bool Equals(Customer c)
        {
            if (FirstName == c.FirstName && LastName == c.LastName) return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Customer);
        }
        public static bool operator ==(Customer c1, Customer c2)
        {
            if (c1.Equals(c2)) return true;
            return false;
        }
        public static bool operator !=(Customer c1, Customer c2){
            return (!(c1==c2));
        }
    }
}
