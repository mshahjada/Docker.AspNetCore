using CloudApp.Infra;
using CloudApp.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudApp.Model;

namespace CloudApp.Core.Service
{
    public class ProductService: IProduct
    {
        protected readonly AppDBContext _context;
        public ProductService(AppDBContext context) {
            _context = context;
        }

        protected async Task Validation(Product entity)
        {

            if (string.IsNullOrEmpty(entity.Name))
                throw new Exception("Product name required.");

            else if (entity.Price<=0)
                throw new Exception("Product price will be greater than zero.");

            else if(await _context.Products.AnyAsync(x => x.Id != entity.Id && x.Name == entity.Name))
               throw new Exception("Duplicate name not allowed.");
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await Validation(entity);

            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {

            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> EditAsync(int id, Product entity)
        {
            await Validation(entity);

            _context.Products.Attach(entity);
            _context.Entry(entity).Property(x => x.Name).IsModified = true;
            _context.Entry(entity).Property(x => x.Price).IsModified = true;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await  _context.Products
                .Include(x=>x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
