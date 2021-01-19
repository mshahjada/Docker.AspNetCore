using CloudApp.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;
using XUnitTest.Fixture;

namespace XUnitTest
{
    public class ProductTest:IClassFixture<ProductFixture>
    {
        protected readonly ProductFixture _productFixture;
        public ProductTest(ProductFixture productFixture)
        {
            _productFixture = productFixture;
        }

        [Theory]
        [InlineData(0)]
        public async Task GetByInvalidIdAsync(int id)
        {
            var result = await _productFixture.productService.GetAsync(id);

            Assert.Equal(null, result);
            //Assert.Equal("User not found!", result);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByValidIdAsync(int id)
        {
            var result = await _productFixture.productService.GetAsync(id);

            Assert.IsType<Product>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetsAsync()
        {
            var result = await _productFixture.productService.GetsAsync();

            Assert.Equal(2, result.Count);
            Assert.True(result.Count == 2);
        }
    }
}
