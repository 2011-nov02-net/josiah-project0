using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public interface IStoreApp
    {
        public List<Order> SearchByCustomer(Customer customer);
        public List<Order> SearchByLocation(Location location);
        public void AddCustomer(Customer customer);
        public void AddOrder(Order order);
        public void WriteData();
        public void ReadData();
    }
}
