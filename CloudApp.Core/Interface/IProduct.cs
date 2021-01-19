using CloudApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Interface
{
    public interface IProduct
    {
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetsAsync();

        Task<Product> AddAsync(Product entity);
        Task<Product> EditAsync(int id, Product entity);
        Task DeleteAsync(int id);
    }
}
