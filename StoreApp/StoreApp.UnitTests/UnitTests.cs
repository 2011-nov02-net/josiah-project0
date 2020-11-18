using System;
using Xunit;
using StoreApp.Library;
using System.Collections.Generic;

namespace StoreApp.UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void ProductInstantiation()
        {
            Product test = new Product("Bread", 3.95, 1);

            Assert.True(test.Name == "Bread");
        }

        [Theory]
        [InlineData(4, 0.85, 3.4)]
        [InlineData(5, 0.5, 2.5)]
        [InlineData(3, 0, 0)]
        public void ProductAddDiscountTest(double original_price, double discount, double final_price)
        {
            Product test = new Product("", original_price, 1);

            test.AddDiscount(discount);

            Assert.True(test.Price == final_price);
        }

        [Theory]
        [InlineData(4, 0.85)]
        [InlineData(5, 0.5)]
        [InlineData(3, 0)]
        public void ProductRemoveDiscountTest(double original_price, double discount)
        {
            Product test = new Product("", original_price, 1);

            test.AddDiscount(discount);
            test.RemoveDiscount();

            Assert.True(test.Price == original_price);
        }
        /*
        [Fact]
        public void AddCustomerTest()
        {
            IStoreApp app = new StoreApplication();

            app.AddCustomer(new Customer("Jotaro", "Kujo"));

            var test = ((StoreApplication)app).Customers[0];

            Assert.True(test.FirstName == "Jotaro" && test.LastName == "Kujo");
        }

        [Fact]
        public void AddOrderTest()
        {
            IStoreApp app = new StoreApplication();
            app.AddCustomer(new Customer("Jotaro", "Kujo"));
            app.AddLocation(new Location("Egypt"));
            app.AddInventoryToLocation(new Location("Egypt"), new List<Product> { new Product("Dio", 100, 1) });

            var test = ((StoreApplication)app).Locations[0];

            Assert.True(test.Inventory[0] == new Product("Dio", 100, 1));
        }

        [Fact]
        public void PlaceOrderTest()
        {
            IStoreApp app = new StoreApplication();
            Customer cust = new Customer("Jotaro", "Kujo");
            Location loc = new Location("Egypt");
            Product prod = new Product("Dio", 100, 1);
            Order ord = new Order(loc, cust, new List<Product> { prod });

            app.AddCustomer(cust);
            app.AddLocation(loc);
            app.AddInventoryToLocation(loc, new List<Product> { prod });

            app.AddOrder(ord);

            var test = app.SearchOrdersByCustomer(cust)[0];

            Assert.True(test.Customer == cust);
        }
        [Fact]
        public void PlaceOrderExceptionTest1()
        {
            IStoreApp app = new StoreApplication();
            Customer cust = new Customer("Jotaro", "Kujo");
            Location loc = new Location("Egypt");
            Product prod = new Product("Dio", 100, 1);
            Order ord = new Order(loc, cust, new List<Product> { prod });

            app.AddCustomer(cust);
            app.AddLocation(loc);
            app.AddInventoryToLocation(loc, new List<Product> { prod });

            Order ord2 = new Order(loc, cust, new List<Product> { new Product("Polnareff", 50, 1) });

            Assert.Throws<ArgumentException>(() => app.AddOrder(ord2));
        }
        
        [Fact]
        public void PlaceOrderExceptionTest2()
        {
            IStoreApp app = new StoreApplication();
            Customer cust = new Customer("Jotaro", "Kujo");
            Location loc = new Location("Egypt");
            Product prod = new Product("Dio", 100, 1);
            Order ord = new Order(loc, cust, new List<Product> { prod });

            app.AddCustomer(cust);
            app.AddLocation(loc);
            app.AddInventoryToLocation(loc, new List<Product> { prod });

            Order ord2 = new Order(loc, cust, new List<Product> { new Product("Dio", 100, 10) });

            Assert.Throws<ArgumentException>(() => app.AddOrder(ord2));
        }
        
        [Fact]
        public void PlaceOrderExceptionTest3()
        {
            IStoreApp app = new StoreApplication();
            Customer cust = new Customer("Jotaro", "Kujo");
            Location loc = new Location("Egypt");
            Product prod = new Product("Dio", 100, 1);
            Order ord = new Order(loc, cust, new List<Product> { prod });

            app.AddCustomer(cust);
            app.AddLocation(loc);
            app.AddInventoryToLocation(loc, new List<Product> { prod });

            Order ord2 = new Order(loc, new Customer("Joseph", "Joestar"), new List<Product> { new Product("Dio", 100, 1) });

            Assert.Throws<ArgumentException>(() => app.AddOrder(ord2));
        }
                
        [Fact]
        public void PlaceOrderExceptionTest4()
        {
            IStoreApp app = new StoreApplication();
            Customer cust = new Customer("Jotaro", "Kujo");
            Location loc = new Location("Egypt");
            Product prod = new Product("Dio", 100, 1);
            Order ord = new Order(loc, cust, new List<Product> { prod });

            app.AddCustomer(cust);
            app.AddLocation(loc);
            app.AddInventoryToLocation(loc, new List<Product> { prod });

            Order ord2 = new Order(new Location("Japan"), cust, new List<Product> { new Product("Dio", 100, 1) });

            Assert.Throws<ArgumentException>(() => app.AddOrder(ord2));
        }*/
    }
}
