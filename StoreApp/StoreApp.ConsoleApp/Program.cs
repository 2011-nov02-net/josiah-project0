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
                                  "(5) Display orders by location\n" +
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
            var customers = app.ShowCustomers();
            int input = 0;

            while (input < 1 || input > customers.Count)
            {
            PlaceOrderChooseCustomer:
                Console.Clear();
                Console.WriteLine("Enter the number of the customer");

                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {customers[i].FirstName} {customers[i].LastName}");
                }
                try
                {
                    input = System.Convert.ToInt32(Console.ReadLine());
                    if (input < 1 || input > customers.Count) goto PlaceOrderChooseCustomer;
                }
                catch (FormatException) { goto PlaceOrderChooseCustomer; }
            }
            var customer = customers[input - 1];

            var locations = app.ShowLocations();
            input = 0;

            while (input < 1 || input > locations.Count)
            {
            PlaceOrderChooseLocation:
                Console.Clear();
                Console.WriteLine("Enter the number of the location");

                for (int i = 0; i < locations.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {locations[i].Name}");
                }
                try
                {
                    input = System.Convert.ToInt32(Console.ReadLine());
                    if (input < 1 || input > locations.Count) goto PlaceOrderChooseLocation;
                }
                catch (FormatException) { goto PlaceOrderChooseLocation; }
            }
            var location = locations[input - 1];

            var inventory = app.ShowLocationInventory(location);

            var cart = new List<Product>();

            string Scart_input = "";
            int[] Icart_input = { 0, 0 };

            while (Icart_input[0] != inventory.Count+1)
            {
            PlaceOrderBuildCart:
                Console.Clear();
                Console.WriteLine($"{location.Name} inventory");
                for (int i = 0; i < inventory.Count; i++)
                {
                    Console.WriteLine($"({i+1}) {inventory[i].DisplayProduct()}");
                }
                Console.WriteLine($"({inventory.Count+1}) to finish");
                if (cart.Count > 0)
                {
                    Console.WriteLine("Cart items");
                    foreach (var x in cart)
                    {
                        Console.WriteLine(x.DisplayProduct());
                    }
                }
                else
                {
                    Console.WriteLine("Cart is empty");
                }
                Console.WriteLine("Enter the product number and quantity desired:");
                try
                {
                    Scart_input = Console.ReadLine();
                    Icart_input[0] = System.Convert.ToInt32(Scart_input.Split(" ")[0]);
                    if (Icart_input[0] == inventory.Count + 1) break;

                    if (Scart_input.Split(" ").Length != 2)
                    {
                        Console.WriteLine("Invalid input");
                        Console.ReadKey();
                        goto PlaceOrderBuildCart;
                    }
                    Icart_input[1] = System.Convert.ToInt32(Scart_input.Split(" ")[1]);

                    if (Icart_input[0] < 1 || Icart_input[0] > locations.Count+1) goto PlaceOrderBuildCart;
                }
                catch (FormatException) {
                    Console.WriteLine("Invalid input");
                    Console.ReadKey();
                    goto PlaceOrderBuildCart;
                }
                if (inventory[Icart_input[0]-1].Amount < Icart_input[1])
                {
                    Console.WriteLine("Can't order more of an item than exists in inventory");
                    goto PlaceOrderBuildCart;
                }
                else
                {
                    inventory[Icart_input[0] - 1].Amount -= Icart_input[1];
                    cart.Add(new Product(inventory[Icart_input[0] - 1].Name, inventory[Icart_input[0] - 1].Price, Icart_input[1]));
                }
            }
            Console.Clear();
            Console.WriteLine("Order to be placed:\n");
            Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Location: {location.Name}");
            foreach (var x in cart)
            {
                Console.WriteLine(x.DisplayProduct());
            }
            Console.ReadKey();
            try
            {
                app.AddOrder(new Order(location, customer, cart));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Order placed successfully");
            Console.ReadKey();

        }
        static void AddNewCustomer(IStoreApp app)
        {
            string[] fullname;
            while (true)
            {
                Console.WriteLine("Write the first and last name of the new customer (q to return to main menu)");
                fullname = Console.ReadLine().Split(" ");
                if (fullname.Length == 1 && fullname[0] == "q") return;
                if (fullname.Length == 2) break;
                Console.WriteLine("Incorrect input");
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
            Console.ReadKey();
        }
        static void DisplayOrdersByLocation(IStoreApp app)
        {
            var locations = app.ShowLocations();
            int input = 0;

            while (input < 1 || input > locations.Count)
            {
            DisplayOrdersLocation:
                Console.Clear();
                Console.WriteLine("Enter the number of the location");

                for (int i = 0; i < locations.Count; i++)
                {
                    Console.WriteLine($"{i+1} {locations[i].Name}");
                }
                try
                {
                    input = System.Convert.ToInt32(Console.ReadLine());
                    if (input < 1 || input > locations.Count) goto DisplayOrdersLocation;
                }
                catch (FormatException) { goto DisplayOrdersLocation; }

                var orders = app.ShowOrdersByLocation(locations[input-1]);
                if (orders.Count == 0)
                {
                    Console.WriteLine("Empty order history");
                    Console.ReadKey();
                }
                else
                {
                    foreach (var x in orders)
                    {
                        Console.WriteLine(x.DisplayOrder());
                    }
                    Console.ReadKey();
                }
            }
        }
        static void DisplayOrdersByCustomer(IStoreApp app)
        {
            var customers = app.ShowCustomers();
            int input = 0;

            while (input < 1 || input > customers.Count)
            {
            DisplayOrdersLocation:
                Console.Clear();
                Console.WriteLine("Enter the number of the customer");

                for (int i = 0; i < customers.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {customers[i].FirstName} {customers[i].LastName}");
                }
                try
                {
                    input = System.Convert.ToInt32(Console.ReadLine());
                    if (input < 1 || input > customers.Count) goto DisplayOrdersLocation;
                }
                catch (FormatException) { goto DisplayOrdersLocation; }

                var orders = app.ShowOrdersByCustomer(customers[input - 1]);
                if (orders.Count == 0)
                {
                    Console.WriteLine("Empty order history");
                    Console.ReadKey();
                }
                else
                {
                    foreach (var x in orders)
                    {
                        Console.WriteLine(x.DisplayOrder());
                    }
                    Console.ReadKey();
                }
            }
        }
        static void AddInventoryToLocation(IStoreApp app)
        {

        }
        static void DisplayLocationInventory(IStoreApp app)
        {

        }
    }
}
