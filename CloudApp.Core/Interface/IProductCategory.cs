using CloudApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Interface
{
    public interface IProductCategory
    {
        Task<ProductCategory> GetAsync(int id);
        Task<List<ProductCategory>> GetsAsync();

        Task<ProductCategory> AddAsync(ProductCategory entity);
        Task<ProductCategory> EditAsync(int id, ProductCategory entity);
        Task DeleteAsync(int id);
    }
}
