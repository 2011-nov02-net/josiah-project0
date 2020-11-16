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
        public void AddOrder(Order order)
        {
            /*
            using var context = new StoredbContext(_contextOptions);
            var new_order = new DataModel.Order
            {
                LocationId = 
            };
            context.Locations.Add(new_order);
            context.SaveChanges(); */
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
                    var newProduct = new DataModel.Product
                    {
                        Name = product.Name,
                        Price = (decimal)product.Price
                    };
                    context.Products.Add(newProduct);
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
