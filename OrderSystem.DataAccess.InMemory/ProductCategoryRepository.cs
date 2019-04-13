using OrderSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default; // create in memory cache
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>; // look for a cache called productCategories - if there, use that
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>(); //if no cache, create a new list of product categories
            }
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories; // save the Product category list to the cache
        }
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);  // add a product category to the list
        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id); // find the product category in the database

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory; // if exists, update it
            }
            else
            {
                throw new Exception("Product Category not found"); // not found - error
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id); // find the product category in the database

            if (productCategory != null)
            {
                return (productCategory); // if exists, return it
            }
            else
            {
                throw new Exception("Product Category not found"); // not found - error
            }
        }
        public IQueryable<ProductCategory> Collection() // returns a list of product categories that can be queried
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id); // find the product category in the database

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete); // if exists, delete it
            }
            else
            {
                throw new Exception("Product Category not found"); // not found - error
            }
        }
    }
}
