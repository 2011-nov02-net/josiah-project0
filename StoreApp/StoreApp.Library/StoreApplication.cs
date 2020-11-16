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
        private List<Customer> _customers = new List<Customer>();
        [DataMember(Order=0)]
        public List<Customer> Customers { get { return _customers; } private set { _customers = value; } }

        private List<Order> _orders = new List<Order>();
        [DataMember(Order=1)]
        public List<Order> Orders { get { return _orders; } private set { _orders = value; } }

        private List<Location> _locations = new List<Location>();
        [DataMember(Order=2)]
        public List<Location> Locations { get { return _locations; } private set { _locations = value; } }

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

        void IStoreApp.AddOrder(Order order)
        {
            if (!_customers.Contains(order.Customer))
            {
                throw new ArgumentException("Can not place an order for a customer that does not exist");
            }
            if (!_locations.Contains(order.Location))
            {
                throw new ArgumentException("Can not place an order at a location that does not exist");
            }

            var l = _locations.Find(x => x.Name == order.Location.Name);

            foreach (var x in order.Items)
            {
                if (!l.Inventory.Contains(x))
                {
                    throw new ArgumentException("Can't place order for item that does not exist");
                }
                if (l.Inventory.Find(y => y == x).Amount < x.Amount)
                {
                    throw new ArgumentException("Can't order more of an item than exists in inventory");
                }
            }
            _orders.Add(order);
            
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
        void IStoreApp.AddInventoryToLocation(Location location, List<Product> product)
        {/*
            var addition = _locations.Find(x => x == location);
            addition.AddItems(product);*/

            DataRepo.AddInventoryToLocation(location, product);
        }

        void IStoreApp.ReadData(string path)
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
        }

        public List<Customer> showCustomers()
        {
            throw new NotImplementedException();
        }

        public List<Location> showLocations()
        {
            throw new NotImplementedException();
        }

        public List<Order> ShowOrders()
        {
            return DataRepo.AllOrders();
        }

        public List<Customer> ShowCustomers()
        {
            throw new NotImplementedException();
        }

        public List<Location> ShowLocations()
        {
            throw new NotImplementedException();
        }
    }
}
