using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.ViewModels
{
    public class ProductManagerViewModel // used to allow a controller to pass compound data to a view -- data is not saved, only to be viewed
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
