using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    interface IStoreApp
    {
        List<Order> SearchByCustomer(Customer customer);
        List<Order> SearchByLocation(Location location);
        bool addCustomer(Customer customer);
        bool addNewOrder(Order order);
        void writeData();
        void readData();
    }
}
