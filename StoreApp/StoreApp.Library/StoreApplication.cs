using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace StoreApp.Library
{
    public class StoreApplication : IStoreApp
    {
        private List<Customer> _customers = new List<Customer>();
        public List<Customer> Customers { get { return _customers; } }

        private List<Order> _orders = new List<Order>();
        public List<Order> Orders { get { return _orders; } }

        private List<Location> _locations = new List<Location>();
        public List<Location> Locations { get { return _locations; } }

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

            var l = _locations.Select(x => x.Name == order.Location.Name);

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
        void IStoreApp.ReadData()
        {
            throw new NotImplementedException();
        }

        void IStoreApp.WriteData(string Path)
        {
            string json = JsonSerializer.Serialize(this, GetType());
            var options = new JsonSerializerOptions();

            File.WriteAllText(Path, json);
        }
    }
}
