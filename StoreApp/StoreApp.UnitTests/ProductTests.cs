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
            Product test = new Product("Bread", 3.95);

            Assert.True(test.Name == "Bread");
        }

        [Theory]
        [InlineData(4, 0.85, 3.4)]
        [InlineData(5, 0.5, 2.5)]
        [InlineData(3, 0, 0)]
        public void ProductAddDiscountTest(double original_price, double discount, double final_price)
        {
            Product test = new Product("", original_price);

            test.AddDiscount(discount);

            Assert.True(test.Price == final_price);
        }

        [Theory]
        [InlineData(4, 0.85)]
        [InlineData(5, 0.5)]
        [InlineData(3, 0)]
        public void ProductRemoveDiscountTest(double original_price, double discount)
        {
            Product test = new Product("", original_price);

            test.AddDiscount(discount);
            test.RemoveDiscount();

            Assert.True(test.Price == original_price);
        }


    }
}
