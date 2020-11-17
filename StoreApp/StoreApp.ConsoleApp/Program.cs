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

            StreamWriter logger = new StreamWriter("../../../../ef-log.txt");

            IStoreApp app = new StoreApplication(logger);

   
            Console.WriteLine("(1) Place order\n" +
                "(2) Add a new customer\n" +
                "(3) Display Customer\n" +
                "(4) );

            /*
            app.AddOrder(new Order(
                new Location("Target"),
                new Customer("Morty", "Smith"),
                new List<Product>
                {
                    new Product("Lightbulb", 7.00, 3),
                    new Product("Jerry Can", 25.00, 1),
                    new Product("flipflam", 33.00, 2)
                }
            ));

            /*
            var orders = app.ShowOrders();

            var orders = app.ShowOrdersByCustomer(new Customer("Jerry", "Smith", 1));
            var orders = app.ShowOrdersByCustomer(new Customer("Morty", "Smith", 1));

            var orders = app.ShowOrdersByLocation(new Location("Walmart"));
            var orders = app.ShowOrdersByLocation(new Location("Target"));

            var walmart = new Location("Target");
            var newItems = new List<Product>();

            newItems.Add(new Product("Jerry Can", 25.00, 3));

            app.AddInventoryToLocation(walmart, newItems);

            /*
            string filepath = @"../../../../Data/AppData.xml";

            IStoreApp app = new StoreApplication();

            int write_flag = 0;

            if (write_flag == 1)
            {
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
            }
            else
            {
                app.ReadData(filepath);
            }

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

            if (write_flag == 1)
            {
                app.WriteData(filepath);
            } */

        }
    }
}
