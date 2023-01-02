using Activity2.Models;
using Activity2.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activity2.Controllers
{
    public class ProductsController : Controller
    {
        /*
        ProductsDAO repository;

        public ProductsController()
        {
            repository = new ProductsDAO();
        }
        */


        // using the hardcodedsampledatarepository without dependecyInjection
        // HardCodedSampleDataRepository repository;

        // using the Dependency Injection, see startup.cs to see the data source for repository.
        public IProductDataService repository { get; set; }
        public ProductsController(IProductDataService dataService)
        {
            repository = dataService;
        }


        public IActionResult Index()
        {

            //ProductsDAO products = new ProductsDAO();

            // return View(products.GetAllProducts());

            return View(repository.GetAllProducts());
        }

        public IActionResult SearchResults(string searchTerm)
        {
            ProductsDAO products = new ProductsDAO();

            List<ProductModel> productList = products.SearchProducts(searchTerm);

            return View("Index", productList);
        }

        public IActionResult SearchForm()
        {
            return View();
        }

        public IActionResult ShowDetails(int id)    // ShowOneProduct
        {
            ProductsDAO product = new ProductsDAO();
            ProductModel foundProduct = product.GetProductById(id);
            return View(foundProduct);
        }

        public IActionResult ShowOneProductJSON(int id)
        {
            return Json(repository.GetProductById(id));
        }

        public IActionResult Edit(int id)
        {
            ProductsDAO product = new ProductsDAO();
            ProductModel foundProduct = product.GetProductById(id);
            return View("ShowEdit", foundProduct);
        }

        public IActionResult ProcessEdit(ProductModel product)
        {
            ProductsDAO products = new ProductsDAO();
            products.Update(product);
            return View("Index", products.GetAllProducts());
        }

        public IActionResult ProcessEditReturnPartial(ProductModel product)
        {
            repository.Update(product);
            return PartialView("_productCard", product);
        }

        public IActionResult Delete(int id)
        {
            ProductsDAO products = new ProductsDAO();
            ProductModel product = products.GetProductById(id);
            products.Delete(product);
            return View("Index", products.GetAllProducts());
        }

        public IActionResult ShowCreateForm()
        {
            return View();
        }

        public IActionResult Create(ProductModel productModel)
        {
            ProductsDAO product = new ProductsDAO();
            if (product.Insert(productModel) > 0)
            {
                return View("InsertValid");
            }
            else
            {
                return View("InsertProductFailed");
            }
        }

        public IActionResult Message()
        {
            return View("message");
        }

        public IActionResult Welcome(string name, int secretNumber=13)
        {
            ViewBag.Name = name;
            ViewBag.Secret = secretNumber;
            return View();
        }
    }
}
