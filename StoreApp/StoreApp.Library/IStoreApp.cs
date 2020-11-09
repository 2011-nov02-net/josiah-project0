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
        public void AddLocation(Location location);
        public void AddOrder(Order order);
        public void AddInventoryToLocation(Location location, List<Product> product);
        public void WriteData(string Path);
        public void ReadData();
    }
}
