using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location
    {
        public string Name { get; }

        public Location(string name)
        {
            Name = name;
        }

        private Dictionary<Product, int> _inventory = new Dictionary<Product, int>();
        public Dictionary<Product, int> Inventory
        {
            get { return Inventory; }
        }

        public bool Equals(Location l)
        {
            if (l.Name == Name) return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Location);
        }
        public static bool operator ==(Location c1, Location c2)
        {
            if (c1.Equals(c2)) return true;
            return false;
        }
        public static bool operator !=(Location c1, Location c2)
        {
            return (!(c1 == c2));
        }
        public void AddItems(Product p, int count)
        {
            if (_inventory.ContainsKey(p))
            {
                _inventory[p] += count;
            }
            else
            {
                _inventory.Add(p, count);
            }
        }

        public bool SellItems(Product p, int count)
        {
            if (_inventory.ContainsKey(p))
            {
                if (_inventory[p] >= count)
                {
                    _inventory[p] -= count;
                    return true;
                }
            }
            return false;
        }

    }
}
