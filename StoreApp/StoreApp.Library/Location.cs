using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace StoreApp.Library
{
    [DataContract(Name = "Location")]
    public class Location
    {
        [DataMember]
        public string Name { get; private set; }

        public Location(string name)
        {
            Name = name;
        }
        private Location() { }
        private List<Product> _inventory = new List<Product>();
        [DataMember]
        public List<Product> Inventory
        {
            get { return _inventory; }
            private set { _inventory = value; }
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
        public void AddItems(List<Product> p)
        {
            foreach (var x in p)
            {
                if (!_inventory.Contains(x))
                {
                    _inventory.Add(x);
                }
                else
                {
                    var adding = _inventory.Find(y => y == x);
                    adding.Amount += x.Amount;
                }
            }
        }

        public void SellItems(Product p)
        {
            if (!_inventory.Contains(p))
            {
                throw new ArgumentException("Can't sell item that does not exist in inventory");
            }
            else
            {
                foreach (var x in _inventory)
                {
                    if (x==p)
                    {
                        if (x.Amount < p.Amount)
                        {
                            throw new ArgumentException("Can't sell more of an item than exists in inventory");
                        }
                        else
                        {
                            x.Amount -= p.Amount;
                            break;
                        }
                    }
                }
            }
        }
        public bool containsProduct(Product p)
        {
            if (_inventory.Exists(x => x == p)) return true;
            return false;
        }
    }
}
