using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudApp.Model
{
    public class ProductCategory
    {
        //private readonly ILazyLoader _lazyLoader;
        //private List<Product> _Products;
        public ProductCategory() { }
        //public ProductCategory(ILazyLoader lazyLoader) { _lazyLoader = lazyLoader; }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        //public List<Product> Products
        //{
        //    get => this._lazyLoader.Load(this, ref _Products);
        //    set => _Products = value;
        //}
    }
}
