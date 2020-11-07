using System;
using Xunit;
using StoreApp.Library;

namespace StoreApp.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void Test1()
        {
            Product test = new Product("Bread");

            Assert.True(test.Amount == 1 && test.Name == "Bread");
        }

        [Fact]
        public void Test2()
        {
            Product test = new Product("Lollipop", 73);

            Assert.True(test.Name == "Lollipop" && test.Amount == 73);
        }

        [Theory]
        [InlineData(31, 21, 10)]
        [InlineData(0, 0, 0)]
        [InlineData(10, 10, 0)]
        public void Test3(int n1, int n2, int n3)
        {
            Product test1 = new Product("", n1);
            Product test2 = new Product("", n2);

            test1.removeProduct(test2);

            Assert.True(test1.Amount == n3);
        }
    }
}
