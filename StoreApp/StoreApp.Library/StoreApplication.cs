﻿using System;
using System.Collections.Generic;

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
            _orders.Add(order);
        }

        List<Order> IStoreApp.SearchOrdersByCustomer(Customer customer)
        {
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Customer.Equals(customer))
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
                if (x.Location.Equals(location))
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
