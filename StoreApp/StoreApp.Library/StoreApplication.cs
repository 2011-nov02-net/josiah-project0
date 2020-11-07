using System;
using System.Collections.Generic;

namespace StoreApp.Library
{
    public class StoreApplication : IStoreApp
    {
        private List<Customer> _customers = new List<Customer>();
        private List<Order> _orders = new List<Order>();
        private List<Product> _products = new List<Product>();

        bool IStoreApp.addCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        bool IStoreApp.addNewOrder(Order order)
        {
            throw new NotImplementedException();
        }

        void IStoreApp.readData()
        {
            throw new NotImplementedException();
        }

        List<Order> IStoreApp.SearchByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        List<Order> IStoreApp.SearchByLocation(Location location)
        {
            throw new NotImplementedException();
        }

        void IStoreApp.writeData()
        {
            throw new NotImplementedException();
        }
    }
}
