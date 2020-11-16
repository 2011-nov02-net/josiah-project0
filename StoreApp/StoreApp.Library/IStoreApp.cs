using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public interface IStoreApp
    {
        public List<Order> ShowOrders();
        public List<Order> ShowOrdersByCustomer(Customer customer);
        public List<Order> ShowOrdersByLocation(Location location);
        public void AddCustomer(Customer customer);
        public List<Customer> ShowCustomers();
        public void AddLocation(Location location);
        public List<Location> ShowLocations();
        public void AddOrder(Order order);
        public void AddInventoryToLocation(Location location, List<Product> product);
        public void ReadData(string Path);
        public void WriteData(string Path);
    }
}
