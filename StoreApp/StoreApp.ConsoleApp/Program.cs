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

            int input = 0;

            while (input != 8)
            {
            Menu:
                Console.Clear();
                Console.WriteLine("(1) Place order\n" +
                                  "(2) Add customer\n" +
                                  "(3) Display customers\n" +
                                  "(4) Display orders by customer\n" +
                                  "(5) Dispaly orders by location\n" +
                                  "(6) Display all orders\n" +
                                  "(7) Add inventory to location\n" +
                                  "(8) Quit");
                try
                {
                    input = System.Convert.ToInt32(Console.ReadLine());
                    if (input < 1 || input > 8) goto Menu;
                }
                catch (FormatException) { goto Menu; }

                Console.Clear();

                switch (input)
                {
                    case 1:
                        PlaceOrder(app);
                        break;
                    case 2:
                        AddNewCustomer(app);
                        break;
                    case 3:
                        DisplayCustomers(app);
                        break;
                    case 4:
                        DisplayOrdersByCustomer(app);
                        break;
                    case 5:
                        DisplayOrdersByLocation(app);
                        break;
                    case 6:
                        DisplayOrders(app);
                        break;
                    case 7:
                        AddInventoryToLocation(app);
                        break;
                }
            }

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

        static void PlaceOrder(IStoreApp app)
        {

        }
        static void AddNewCustomer(IStoreApp app)
        {
            string[] fullname;
            while (true)
            {
                Console.WriteLine("Write the first and last name of the new customer");
                fullname = Console.ReadLine().Split(" ");
                if (fullname.Length == 2) break;
                Console.WriteLine("Incorrect format");
            }
            app.AddCustomer(new Customer(fullname[0], fullname[1]));

        }
        static void DisplayCustomers(IStoreApp app)
        {
            var customers = app.ShowCustomers();

            foreach (var x in customers)
            {
                Console.WriteLine($"ID: {x.ID, -3}|{x.FirstName, 7} {x.LastName,-5}");
            }
            Console.ReadKey();
        }
        static void DisplayOrders(IStoreApp app)
        {
            var orders = app.ShowOrders();

            foreach (var x in orders)
            {
                Console.WriteLine(x.DisplayOrder());
            }
        }
        static void DisplayOrdersByLocation(IStoreApp app)
        {
            var locations = app.ShowLocations();
            string[] storeName = { };

            while (storeName.Length != 1)
            {
                Console.WriteLine("Enter the location name");
                storeName = Console.ReadLine().Split(" ");
                if (storeName.Length != 1)
                {
                    Console.WriteLine("Incorrect format");
                    continue;
                }
                var orders = app.ShowOrdersByLocation(new Location(storeName[0]));
                if (orders.Count == 0)
                {
                    Console.WriteLine("Invalid store name");

                }
            }



        }
        static void DisplayOrdersByCustomer(IStoreApp app)
        {

        }
        static void AddInventoryToLocation(IStoreApp app)
        {

        }
    }
}
