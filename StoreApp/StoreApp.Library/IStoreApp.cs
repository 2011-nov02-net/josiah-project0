using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    /// <summary>
    /// interface for the StoreApplication class, more details about each function
    /// are in the derived class
    /// </summary>
    public interface IStoreApp
    {
        public List<Order> ShowOrders();
        public List<Order> ShowOrdersByCustomer(Customer customer);
        public List<Order> ShowOrdersByLocation(Location location);
        public void AddCustomer(Customer customer);
        public List<Customer> ShowCustomers();
        public void AddLocation(Location location);
        public List<Location> ShowLocations();
        public List<Product> ShowProducts();
        public void AddProduct(Product product);
        public void AddOrder(Order order);
        public void AddInventoryToLocation(Location location, List<Product> product);
        public List<Product> ShowLocationInventory(Location location);
        /*public void ReadData(string Path);
        public void WriteData(string Path);*/
    }
}
