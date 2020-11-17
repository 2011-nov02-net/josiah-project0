using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace StoreApp.Library
{
    class StoreRepository
    {
        private readonly DbContextOptions<StoredbContext> _contextOptions;
        private static string GetConnectionString()
        {
            string path = $"../../../../ConnectionString.json";

            string json;
            json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<string>(json);
        }
        public StoreRepository(StreamWriter logger)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoredbContext>();
            optionsBuilder.UseSqlServer(StoreRepository.GetConnectionString());
            optionsBuilder.LogTo(logger.WriteLine, LogLevel.Information);
            _contextOptions = optionsBuilder.Options;
        }
        public StoreRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoredbContext>();
            optionsBuilder.UseSqlServer(StoreRepository.GetConnectionString());
            _contextOptions = optionsBuilder.Options;
        }
        public void AddCustomer(Customer customer)
        {
            using var context = new StoredbContext(_contextOptions);
            var new_customer = new DataModel.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
            context.Customers.Add(new_customer);
            context.SaveChanges();
        }
        public void AddLocation(Location location)
        {
            using var context = new StoredbContext(_contextOptions);
            var new_location = new DataModel.Location
            {
                Name = location.Name
            };
            context.Locations.Add(new_location);
            context.SaveChanges();
        }
        public void AddProduct(Product product)
        {
            using var context = new StoredbContext(_contextOptions);
            var new_product = new DataModel.Product
            {
                Name = product.Name,
                Price = (decimal)product.Price
            };
            context.Products.Add(new_product);
            context.SaveChanges();
        }
        public void AddOrder(Order order)
        {
            using var context = new StoredbContext(_contextOptions);

            // get all values to create new order
            var _LocationID = context.Locations.Where(x => x.Name == order.Location.Name).First().Id;
            var _CustomerID = context.Customers.Where(x => x.FirstName == order.Customer.FirstName && x.LastName == order.Customer.LastName).First().Id;
            var _total = order.Items.Sum(x => x.Price * x.Amount);
            var _date = DateTime.Now;

            // create and add the new order to the database
            var new_order = new DataModel.Order
            {
                LocationId = _LocationID,
                CustomerId = _CustomerID,
                Total = (decimal)_total,
                Date = _date
            };
            context.Orders.Add(new_order);
            context.SaveChanges();

            var OrderID = new_order.Id;
            foreach (var product in order.Items)
            {
                // create a new orderline for each product
                var productID = context.Products.Where(x => x.Name == product.Name).First().Id;
                var new_orderline = new DataModel.OrderLine
                {
                    OrderId = OrderID,
                    ProductId = productID,
                    Quantity = product.Amount,
                    Discount = (decimal)product.Discount
                };
                context.OrderLines.Add(new_orderline);

                // update the inventory lines of the location to reflect the new order
                var locationline = context.LocationLines
                    .Include(x => x.Location)
                    .Where(x => _LocationID == x.Location.Id && productID == x.ProductId).First();

                locationline.Quantity -= product.Amount;
            }
            context.SaveChanges();
        }
        public List<Product> GetLocationInventory(Location l)
        {
            using var context = new StoredbContext(_contextOptions);
            List<Product> result = new List<Product>();

            var location = context.LocationLines
                .Include(x => x.Location)
                .Include(x => x.Product)
                .Where(x => x.Location.Name == l.Name).ToList();

            foreach (var x in location)
            {
                result.Add(new Product(x.Product.Name, (double)x.Product.Price, (int)x.Quantity));
            }
            return result;
        }
        public List<Customer> AllCustomers()
        {
            using var context = new StoredbContext(_contextOptions);

            List<Customer> result = new List<Customer>();

            var customers = context.Customers.ToList();

            foreach (var customer in customers)
            {
                result.Add(new Customer(customer.FirstName, customer.LastName, customer.Id));
            }
            return result;
        }
        public List<Location> AllLocations()
        {
            using var context = new StoredbContext(_contextOptions);

            List<Location> result = new List<Location>();

            var locations = context.Locations.ToList();

            foreach (var location in locations)
            {
                result.Add(new Location(location.Name));
            }
            return result;
        }
        public List<Product> AllProducts()
        {
            using var context = new StoredbContext(_contextOptions);

            List<Product> result = new List<Product>();

            var products = context.Products.ToList();

            foreach (var product in products)
            {
                result.Add(new Product(product.Name, (double)product.Price, 1));
            }
            return result;

        }
        public List<Order> OrdersByCustomer(Customer customer)
        {
            List<Order> temp = AllOrders();
            List<Order> result = new List<Order>();
            foreach (var x in temp)
            {
                if (x.Customer == customer) result.Add(x);
            }
            return result;
        }
        public List<Order> OrdersByLocation(Location location)
        {
            List<Order> temp = AllOrders();
            List<Order> result = new List<Order>();
            foreach (var x in temp)
            {
                if (x.Location == location) result.Add(x);
            }
            return result;
        }
        public List<Order> AllOrders()
        {
            using var context = new StoredbContext(_contextOptions);

            List<Order> result = new List<Order>();

            var orders = context.Orders.ToList();

            foreach (var ord in orders)
            {
                var orderlines = context.OrderLines
                    .Include(x => x.Product)
                    .Include(x => x.Order.Customer)
                    .Include(x => x.Order.Location)
                    .Where(x => x.OrderId == ord.Id).ToList();

                List<Product> items = new List<Product>();

                foreach (var x in orderlines)
                {
                    items.Add(new Product(x.Product.Name, (double)x.Product.Price, x.Quantity));
                }

                result.Add(
                    new Order(
                        new Location(ord.Location.Name),
                        new Customer(ord.Customer.FirstName, ord.Customer.LastName, ord.CustomerId),
                        items,
                        ord.Date
                    ));
            }
            return result;

        }
        public void AddInventoryToLocation(Location location, List<Product> items)
        {
            using var context = new StoredbContext(_contextOptions);

            var LocationID = context.Locations.Where(x => x.Name == location.Name).First().Id;

            foreach (var product in items)
            {
                if (!context.Products.Any(x => x.Name == product.Name))
                {
                    AddProduct(product);
                }
                var productID = context.Products.Where(x => x.Name == product.Name).First().Id;

                if (!context.LocationLines.Any(x => x.ProductId == productID && x.LocationId == LocationID))
                {
                    var newLine = new DataModel.LocationLine
                    {
                        LocationId = LocationID,
                        ProductId = context.Products.Where(x => x.Name == product.Name).First().Id,
                        Quantity = product.Amount
                    };
                    context.LocationLines.Add(newLine);
                }
                else
                {
                    var line = context.LocationLines.Where(x => x.ProductId == productID && x.LocationId == LocationID).First();
                    line.Quantity += product.Amount;
                }
            }
            context.SaveChanges();
        }
    }
}
