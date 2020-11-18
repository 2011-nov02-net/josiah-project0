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
        /// <summary>
        /// The Repository instance that accesses the data using EF
        /// </summary>
        private StoreRepository DataRepo;
        /// <summary>
        /// Overloaded constructor that takes a streamwriter object so it can log query information
        /// </summary>
        /// <param name="logger"></param>
        public StoreApplication(StreamWriter logger) { DataRepo = new StoreRepository(logger); }
        /// <summary>
        /// default constructor, does not log query information
        /// </summary>
        public StoreApplication() { DataRepo = new StoreRepository(); }
        /// <summary>
        /// These next 3 classes do not perform any business logic, they just
        /// call the approriate method from the DataRepo to insert it into the table
        /// </summary>
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
        /// <summary>
        /// performs a few checks against the current data to see if the order is valid,
        /// then passes it to the repository class to insert all necessary records into the database
        /// </summary>
        void IStoreApp.AddOrder(Order order)
        {
            if (order.Items.Count == 0)
            {
                throw new ArgumentException("Can't place order for no items");
            }
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
        /// <summary>
        /// the rest of these functions do not perform any logic themselves, but just call
        /// the required function from the repository
        /// </summary>
        public List<Order> ShowOrders()
        {
            return DataRepo.AllOrders();
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
        /// <summary>
        /// old data serialization methods, now obsolete since this class does
        /// not contain all the fields it used to (data is now entirely stored on the database)
        /// </summary>
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
    }
}
