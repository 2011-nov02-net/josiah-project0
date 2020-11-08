using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.Library
{
    public class StoreApplication : IStoreApp
    {
        private List<Customer> _customers = new List<Customer>();
        private List<Order> _orders = new List<Order>();
        private List<Location> _locations = new List<Location>();

        void IStoreApp.AddCustomer(Customer customer)
        {
            if (!_customers.Contains(customer))
            {
                _customers.Add(customer);
            }
        }

        void IStoreApp.AddOrder(Order order)
        {
            if (!_customers.Contains(order.Customer))
            {
                throw new ArgumentException("Can not place an order for a customer that does not exist");
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

        void IStoreApp.WriteData()
        {
            throw new NotImplementedException();
        }
    }
}
