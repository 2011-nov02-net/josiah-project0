using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Xml;

namespace StoreApp.Library
{
    public class StoreApplication : IStoreApp
    {
        private List<Customer> _customers = new List<Customer>();
        public List<Customer> Customers { get { return _customers; } private set { } }

        private List<Order> _orders = new List<Order>();
        public List<Order> Orders { get { return _orders; } private set { } }

        private List<Location> _locations = new List<Location>();
        public List<Location> Locations { get { return _locations; } private set { } }

        void IStoreApp.AddCustomer(Customer customer)
        {
            if (!_customers.Contains(customer))
            {
                _customers.Add(customer);
            }
        }

        void IStoreApp.AddLocation(Location location)
        {
            if (!_locations.Contains(location))
            {
                _locations.Add(location);
            }
        }

        void IStoreApp.AddOrder(Order order)
        {
            if (!_customers.Contains(order.Customer))
            {
                throw new ArgumentException("Can not place an order for a customer that does not exist");
            }
            if (!_locations.Contains(order.Location))
            {
                throw new ArgumentException("Can not place an order at a location that does not exist");
            }

            var l = _locations.Find(x => x.Name == order.Location.Name);

            foreach (var x in order.Items)
            {
                if (!l.Inventory.Contains(x))
                {
                    throw new ArgumentException("Can't place item for order that does not exist");
                }
                if (l.Inventory.Find(y => y == x).Amount < x.Amount)
                {
                    throw new ArgumentException("Can't order more of an item than exists in inventory");
                }
            }
            _orders.Add(order);
            
        }

        List<Order> IStoreApp.SearchOrdersByCustomer(Customer customer)
        {
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Customer == customer)
                {
                    result.Add(x);
                }
            }
            return result;
        }

        List<Order> IStoreApp.SearchOrdersByLocation(Location location)
        {
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Location == location)
                {
                    result.Add(x);
                }
            }
            return result;
        }
        void IStoreApp.AddInventoryToLocation(Location location, List<Product> product)
        {
            var addition = _locations.Find(x => x == location);
            addition.AddItems(product);
        }

        public static StoreApplication ReadData(string filepath)
        {
            string json = File.ReadAllText(filepath);
            StoreApplication result = JsonSerializer.Deserialize<StoreApplication>(json);
            return result;
        }

        void IStoreApp.WriteData(string Path)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(this, GetType(), options);

            File.WriteAllText(Path, json);
        }
    }
}
