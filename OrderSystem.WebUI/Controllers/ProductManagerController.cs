using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderSystem.Core.Models;
using OrderSystem.DataAccess.InMemory;

namespace OrderSystem.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository(); // initialize the repository
        }
        // GET: ProductManager
        public ActionResult Index() // page with current list of products. Index View is created from here
        {
            List<Product> products = context.Collection().ToList(); // list of products

            return View(products); // send products to the View
        }
        public ActionResult Create() // displays the page to fill in the details
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product) // post back the changes to be saved. Create View is created from here
        {
            if (!ModelState.IsValid) // if validation did not pass, redisplay page with errors
            {
                return View(product);
            }
            else // otherwise add the new product to the collection, save it and go back to the product list
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id) // displays product for editing
        {
            Product product = context.Find(Id); // find it from the database
            if (product == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                return View(product); // if found, return it to the View
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                if (!ModelState.IsValid) // if validation did not pass, redisplay page with errors
                {
                    return View(product);
                }
                // assign values
                productToEdit.Category      = product.Category;
                productToEdit.Description   = product.Description;
                productToEdit.Image         = product.Image;
                productToEdit.Name          = product.Name;
                productToEdit.Price         = product.Price;

                context.Commit(); //save it back to the collection

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")] // Alternative action name
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                context.Delete(Id); // delete the product from the collection
                context.Commit(); // commit the change
                return RedirectToAction("Index"); // return to the product list page

            }
        }
    }
}