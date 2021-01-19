using CloudApp.Core.Service;
using CloudApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using XUnitTest.Mock;

namespace XUnitTest.Fixture
{
    public class ProductFixture
    {
        private readonly MockDBContext mockDBContext;

        public ProductService productService;
        public ProductFixture() 
        {
            mockDBContext = new MockDBContext();

            mockDBContext.ProductCategories.AddRange(
               new ProductCategory[]
               {
                        new ProductCategory
                        {
                            Id = 1,
                            Name = "Laptop"
                        },
                        new ProductCategory
                        {
                            Id = 2,
                            Name = "Mobile"
                        }
               });

            mockDBContext.Products.AddRange(
                new Product[] 
                {   
                    new Product 
                    {
                        Id = 1,
                        Name = "Asus S15",
                        Price = 70000,
                        Description = "n/a",
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Samsun A20",
                        Price = 15000,
                        Description = "n/a",
                        CategoryId = 2,
                    }
                });
            mockDBContext.SaveChanges();

            productService = new ProductService(mockDBContext);
        }
    }
}
