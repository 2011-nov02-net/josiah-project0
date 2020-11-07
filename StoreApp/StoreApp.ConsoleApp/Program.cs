using StoreApp.Library;
using System;

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


            
        }
    }
}
