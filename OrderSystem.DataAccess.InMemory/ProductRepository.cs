using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderSystem.Core;
using System.Runtime.Caching;
using OrderSystem.Core.Models;

namespace OrderSystem.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default; // create in memory cache
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>; // look for a cache called Products - if there, use that
            if (products == null)
            {
                products = new List<Product>(); //if no cache, create a new list of products
            }
        }
        public void Commit()
        {
            cache["products"] = products; // save the Product list to the cache
        }
        public void Insert(Product p)
        {
            products.Add(p);  // add a product to the list
        }
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id); // find the product in the database

            if (productToUpdate != null)
            {
                productToUpdate = product; // if exists, update it
            }
            else
            {
                throw new Exception("Product not found"); // not found - error
            }
        }      
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id); // find the product in the database

            if (product != null)
            {
                return(product); // if exists, return it
            }
            else
            {
                throw new Exception("Product not found"); // not found - error
            }
        }
        public IQueryable<Product> Collection() // returns a list of products that can be queried
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id); // find the product in the database

            if (productToDelete != null)
            {
                products.Remove(productToDelete); // if exists, delete it
            }
            else
            {
                throw new Exception("Product not found"); // not found - error
            }
        }
    }
}
