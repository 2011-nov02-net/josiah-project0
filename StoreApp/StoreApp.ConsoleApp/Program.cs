using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StoreApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string writepath = @"../../../../Data/storeAppData.json";
            string readpath = @"../../../../Data/readFromData.json";
            IStoreApp app = new StoreApplication();

            
            app.AddCustomer(new Customer("Jeff", "Winger"));
            app.AddCustomer(new Customer("Britta", "Perry"));
            app.AddCustomer(new Customer("Abed", "Nadir"));
            app.AddCustomer(new Customer("Troy", "Barnes"));
            app.AddCustomer(new Customer("Shirley", "Bennett"));
            app.AddCustomer(new Customer("Annie", "Edison"));
            app.AddCustomer(new Customer("Pierce", "Hawthorne"));
            app.AddCustomer(new Customer("Jerry", "Smith"));

            app.AddLocation(new Location("West Street"));
            app.AddLocation(new Location("Doppler Emporium"));

            app.AddInventoryToLocation(new Location("West Street"),
                new List<Product> { new Product("lollipop", 1.00, 300),
                                    new Product("cupcake", 3.00, 20),
                                    new Product("muffin", 5.00, 30)
                });

            
            try
            {
                app.AddOrder(new Order(
                    new Location("West Street"),
                    new Customer("Jerry", "Smith"),
                    new List<Product>
                    {
                        new Product("lollipop", 1.00, 50),
                        new Product("cupcake", 3.00, 3),
                        new Product("muffin", 5.00, 1)
                    }
                    ));
                
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            

            var test = app.SearchOrdersByCustomer(new Customer("Jerry", "Smith"));

            //var test = app.SearchOrdersByLocation(new Location("West Street"));


            foreach (var x in test)
            {
                Console.WriteLine(x.DisplayOrder());
            }

            //string json = JsonSerializer.Serialize(app.SearchOrdersByCustomer(new Customer("Jerry", "Smith")));
            //File.WriteAllText(filepath, json);

        }
    }
}
