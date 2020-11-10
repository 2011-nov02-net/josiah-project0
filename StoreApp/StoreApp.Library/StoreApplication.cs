using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace StoreApp.Library
{
    [DataContract(Name = "StoreApplication")]
    public class StoreApplication : IStoreApp
    {

        private List<Customer> _customers = new List<Customer>();
        [DataMember]
        public List<Customer> Customers { get { return _customers; } private set { _customers = value; } }

        private List<Order> _orders = new List<Order>();
        [DataMember]
        public List<Order> Orders { get { return _orders; } private set { _orders = value; } }

        private List<Location> _locations = new List<Location>();
        [DataMember]
        public List<Location> Locations { get { return _locations; } private set { _locations = value; } }

        void IStoreApp.AddCustomer(Customer customer)
        {
            if (!_customers.Contains(customer))
            {
                _customers.Add(customer);
            }
        }

        void IStoreApp.AddLocation(Location location)
        {
            if (!_locations.Contains(location))
            {
                _locations.Add(location);
            }
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
                    throw new ArgumentException("Can't place item for order that does not exist");
                }
                if (l.Inventory.Find(y => y == x).Amount < x.Amount)
                {
                    throw new ArgumentException("Can't order more of an item than exists in inventory");
                }
            }
            _orders.Add(order);
            
        }

        List<Order> IStoreApp.SearchOrdersByCustomer(Customer customer)
        {
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Customer == customer)
                {
                    result.Add(x);
                }
            }
            return result;
        }

        List<Order> IStoreApp.SearchOrdersByLocation(Location location)
        {
            List<Order> result = new List<Order>();
            foreach (var x in _orders)
            {
                if (x.Location == location)
                {
                    result.Add(x);
                }
            }
            return result;
        }
        void IStoreApp.AddInventoryToLocation(Location location, List<Product> product)
        {
            var addition = _locations.Find(x => x == location);
            addition.AddItems(product);
        }

        public void ReadData(string path)
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
    }
}
