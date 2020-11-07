using System;
using Xunit;
using StoreApp.Library;

namespace StoreApp.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void ProductInstantiation()
        {
            Product test = new Product("Bread");

            Assert.True(test.Name == "Bread");
        }


    }
}
