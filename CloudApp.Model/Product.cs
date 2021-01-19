using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudApp.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        [ForeignKey("ProductCategory")]
        public int CategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
