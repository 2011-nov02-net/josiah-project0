using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public interface IStoreApp
    {
        public List<Order> SearchOrdersByCustomer(Customer customer);
        public List<Order> SearchOrdersByLocation(Location location);
        public void AddCustomer(Customer customer);
        public List<Customer> showCustomers();
        public void AddLocation(Location location);
        public List<Location> showLocations();
        public void AddOrder(Order order);
        public void AddInventoryToLocation(Location location, List<Product> product);
        public void ReadData(string Path);
        public void WriteData(string Path);
    }
}
