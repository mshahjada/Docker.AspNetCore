using CloudApp.Model;
using Paging;
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

        Task<PageList<ProductCategory>> GetsAsync(int page, int pageSize, string search);
    }
}
