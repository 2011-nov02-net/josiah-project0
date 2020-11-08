using StoreApp.Library;
using System;
using System.Collections.Generic;

namespace StoreApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IStoreApp app = new StoreApplication();

            app.AddCustomer(new Customer("Jeff", "Winger"));
            app.AddCustomer(new Customer("Britta", "Perry"));
            app.AddCustomer(new Customer("Abed", "Nadir"));
            app.AddCustomer(new Customer("Troy", "Barnes"));
            app.AddCustomer(new Customer("Shirley", "Bennett"));
            app.AddCustomer(new Customer("Annie", "Edison"));
            app.AddCustomer(new Customer("Pierce", "Hawthorne"));

            app.AddOrder(new Order(
                new Location("West Street"),
                new Customer("Jerry", "Smith"),
                new Dictionary<Product, int> { { new Product("lollipop", 1.00), 5 } }
                ));
            app.AddOrder(new Order(
                new Location("West Street"),
                new Customer("Beth", "Smith"),
                new Dictionary<Product, int> { { new Product("gumdrop", 1.00), 1 } }
                ));
            app.AddOrder(new Order(
                new Location("Doppler Emporium"),
                new Customer("Jerry", "Smith"),
                new Dictionary<Product, int> { { new Product("tootsie rolls", 1.00), 30 },
                                               { new Product("cinnamon roll", 5.00), 5 },
                                               { new Product("Cupcake", 3.00), 1 }
                }
                ));
            app.AddOrder(new Order(
                new Location("Doppler"),
                new Customer("Rick", "Sanchez"),
                new Dictionary<Product, int> { { new Product("pop rocks", 2.00), 5 } }
                ));

            try
            {
                app.AddOrder(new Order(
                    new Location("West Street"),
                    new Customer("Jerry", "Smith"),
                    new Dictionary<Product, int> { { new Product("lollipop", 1.00), 50 } }
                    ));
            }
            catch (ArgumentException)
            {

            }


            //var test = app.SearchOrdersByCustomer(new Customer("Jerry", "Smith"));

            var test = app.SearchOrdersByLocation(new Location("West Street"));


            foreach (var x in test)
            {
                x.displayOrder();
            }
        }
    }
}
