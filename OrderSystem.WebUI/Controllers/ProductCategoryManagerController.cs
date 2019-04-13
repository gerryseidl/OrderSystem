using OrderSystem.Core.Models;
using OrderSystem.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderSystem.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository(); // initialize the repository
        }
        public ActionResult Index() // page with current list of product categories. Index View is created from here
        {
            List<ProductCategory> productCategories = context.Collection().ToList(); // list of product categories

            return View(productCategories); // send productcategories to the View
        }
        public ActionResult Create() // displays the page to fill in the details
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory) // post back the changes to be saved. Create View is created from here
        {
            if (!ModelState.IsValid) // if validation did not pass, redisplay page with errors
            {
                return View(productCategory);
            }
            else // otherwise add the new product category to the collection, save it and go back to the product category list
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id) // displays product category for editing
        {
            ProductCategory productCategory = context.Find(Id); // find it from the database
            if (productCategory == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                return View(productCategory); // if found, return it to the View
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                if (!ModelState.IsValid) // if validation did not pass, redisplay page with errors
                {
                    return View(productCategory);
                }
                // assign values
                productCategoryToEdit.Category = productCategory.Category;
                context.Commit(); //save it back to the collection

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")] // Alternative action name
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound(); // if not found - error
            }
            else
            {
                context.Delete(Id); // delete the product category from the collection
                context.Commit(); // commit the change
                return RedirectToAction("Index"); // return to the product category list page
            }
        }
    }
}