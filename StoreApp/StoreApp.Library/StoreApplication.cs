using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace StoreApp.Library
{
    [DataContract(Name = "StoreApplication")]
    public class StoreApplication : IStoreApp
    {

        private StoreRepository DataRepo;
        public StoreApplication(StreamWriter logger) { DataRepo = new StoreRepository(logger); }
        public StoreApplication() { DataRepo = new StoreRepository(); }
        void IStoreApp.AddCustomer(Customer customer)
        {
            DataRepo.AddCustomer(customer);
        }
        void IStoreApp.AddLocation(Location location)
        {
            DataRepo.AddLocation(location);
        }
        void IStoreApp.AddProduct(Product product)
        {
            DataRepo.AddProduct(product);
        }

        void IStoreApp.AddOrder(Order order)
        {
            var customers = ShowCustomers();
            var locations = ShowLocations();

            if (!customers.Contains(order.Customer))
            {
                throw new ArgumentException("Can not place an order for a customer that does not exist");
            }
            if (!locations.Contains(order.Location))
            {
                throw new ArgumentException("Can not place an order at a location that does not exist");
            }

            var l = DataRepo.GetLocationInventory(order.Location);

            foreach (var x in order.Items)
            {
                if (!l.Contains(x))
                {
                    throw new ArgumentException("Can't place order for item that does not exist in inventory");
                }
                if (l.Find(y => y == x).Amount < x.Amount)
                {
                    throw new ArgumentException("Can't order more of an item than exists in inventory");
                }
            }
            DataRepo.AddOrder(order);
        }
        List<Order> IStoreApp.ShowOrdersByCustomer(Customer customer)
        {/*
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Customer == customer)
                {
                    result.Add(x);
                }
            }
            return result;*/
            return DataRepo.OrdersByCustomer(customer);
        }
        List<Order> IStoreApp.ShowOrdersByLocation(Location location)
        {/*
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Location == location)
                {
                    result.Add(x);
                }
            }
            return result;*/
            return DataRepo.OrdersByLocation(location);
        }
        List<Product> IStoreApp.ShowLocationInventory(Location location)
        {
            return DataRepo.GetLocationInventory(location);
        }
        void IStoreApp.AddInventoryToLocation(Location location, List<Product> product)
        {/*
            var addition = _locations.Find(x => x == location);
            addition.AddItems(product);*/

            DataRepo.AddInventoryToLocation(location, product);
        }
        
        /*void IStoreApp.ReadData(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(StoreApplication));

            StoreApplication app = (StoreApplication)ser.ReadObject(reader, true);

            reader.Close();
            fs.Close();

            this.Customers = app.Customers;
            this.Locations = app.Locations;
            this.Orders = app.Orders;
        }
        void IStoreApp.WriteData(string path)
        {
            var ds = new DataContractSerializer(typeof(StoreApplication));
            var settings = new XmlWriterSettings { Indent = true };

            using (var writer = XmlWriter.Create(path, settings))
                ds.WriteObject(writer, this);
        }*/
        public List<Order> ShowOrders()
        {
            return DataRepo.AllOrders();
        }
        public List<Customer> ShowCustomers()
        {
            return DataRepo.AllCustomers();
        }
        public List<Location> ShowLocations()
        {
            return DataRepo.AllLocations();
        }
        public List<Product> ShowProducts()
        {
            return DataRepo.AllProducts();
        }
    }
}
