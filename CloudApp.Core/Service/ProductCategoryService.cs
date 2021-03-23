using CloudApp.Infra;
using CloudApp.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CloudApp.Model;
using Paging;

namespace CloudApp.Core.Service
{
    public class ProductCategoryService: IProductCategory
    {
        protected readonly AppDBContext _context;
        public ProductCategoryService(AppDBContext context)
        {
            _context = context;
        }

        protected async Task Validation(ProductCategory entity)
        {

            if (string.IsNullOrEmpty(entity.Name))
                throw new Exception("ProductCategory name required.");

            else if (await _context.ProductCategories.AnyAsync(x => x.Id != entity.Id && x.Name == entity.Name))
                throw new Exception("Duplicate name not allowed.");
        }

        public async Task<ProductCategory> AddAsync(ProductCategory entity)
        {
            await Validation(entity);

            await _context.ProductCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {

            var entity = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
            _context.ProductCategories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductCategory> EditAsync(int id, ProductCategory entity)
        {
            await Validation(entity);

            _context.ProductCategories.Attach(entity);
            _context.Entry(entity).Property(x => x.Name).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ProductCategory> GetAsync(int id)
        {
            var res = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
            var prod = res.Products;
            return res;
        }

        public async Task<List<ProductCategory>> GetsAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<PageList<ProductCategory>> GetsAsync(int page, int pageSize, string search)
        {
            return await _context.ProductCategories.GetPagedAsync(page, pageSize);
        }
    }
}
