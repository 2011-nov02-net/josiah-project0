using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
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
            throw new NotImplementedException();
        }
        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public List<Order> OrdersByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public List<Order> OrdersByLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public void AddInventoryToLocation(Location location, List<Product> items)
        {
            throw new NotImplementedException();
        }


    }
}
