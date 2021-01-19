using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CloudApp.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using CloudApp.Core.Interface;
using CloudApp.Model;

namespace CloudApp.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[ServiceFilter(typeof(SampleActionFilter))]
    public class ProductController : ControllerBase
    {
        protected IProductCategory _productCategory;
        protected IProduct _product;
        protected IMemoryCache _memoryCache;
        public ProductController(
            IProductCategory productCategory,
            IProduct product,
            IMemoryCache memoryCache
            )
        {
            _productCategory = productCategory;
            _product = product;
            _memoryCache = memoryCache;
        }


        //[AddHeader("Key", "Done")]
        //[ShortCircuitFilter]
        [HttpGet]
        [Route("gets")]
        public async Task<IActionResult> Get()
        {
            if(!_memoryCache.TryGetValue(CacheKeys.ProductList, out List<Product> products))
            {
                products = await _product.GetsAsync();

                _memoryCache.Set(CacheKeys.ProductList, products, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20)));

                return Ok(products);
            }
            else
            {
                return Ok(products);
            }
           
        }

        [HttpGet]
        [Route("category/gets")]
        public async Task<IActionResult> GetCategories()
        {

            return Ok(await _productCategory.GetsAsync()) ;
        }

        [HttpGet]
        [Route("category/get")]
        public async Task<IActionResult> GetCategory()
        {

            return Ok(await _productCategory.GetAsync(1));
        }

        [HttpGet] 
        [Route("detail")]
        public async Task<IActionResult> GetDetails ()
        {

            var cats = await _productCategory.GetsAsync();
            var prods = await _product.GetsAsync();

            var res = from c in cats
                      join p in prods on c.Id equals p.CategoryId into t
                      from p in t.DefaultIfEmpty()
                      group new { c, p } by new { c.Id, c.Name } into grp
                      //new { c, hasProd = (p != null) } by new { c.Id, c.Name } into grp
                      select new
                      {
                          Id = grp.Key.Id,
                          Name = grp.Key.Name,
                          Count = grp.Count(x => x.p != null)
                      };

            //var res = (from p in this.products
            //           join c in this.categories on p.CategoryId equals c.Id into t
            //           from c in t.DefaultIfEmpty()
            //           select new
            //           {
            //               Name = p.Name,
            //               CatName =  c == null ? null : c.Name
            //           }).ToList();



            return Ok(res);
        }
    }

    public static class CacheKeys
    {
        public static string ProductList { get { return "_ProductList"; } }
    }
}
