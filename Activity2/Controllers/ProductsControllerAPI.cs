using Activity2.Models;
using Activity2.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace Activity2.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductsControllerAPI : Controller
    {
        ProductsDAO repository;

        public ProductsControllerAPI()
        {
            repository = new ProductsDAO();
        }

        /*
        
        [HttpGet]
        public ActionResult <IEnumerable<ProductModel>> Index()
        {

            return repository.GetAllProducts();
        }

        [HttpGet("searchproducts/{searchTerm}")]
        public ActionResult <IEnumerable<ProductModel>> SearchResults(string searchTerm)
        {

            return repository.SearchProducts(searchTerm);
        }

        [HttpGet("showoneproduct/{id}")]
        public ActionResult <ProductModel> ShowDetails(int id)    // ShowOneProduct
        {

            return repository.GetProductById(id);
        }

        [HttpPost("insertOne")]
        // post action
        // expecting a product in json format in the body of the request.
        public ActionResult <int> Create(ProductModel productModel)
        {
            int newId = repository.Insert(productModel);
            return newId;
        }

        [HttpPut("ProcessEdit")]
        // put request
        // expect a json formatted object in the body of the request. id number must match the item being modified.
        public ActionResult <ProductModel> ProcessEdit(ProductModel product)
        {
            repository.Update(product);
            return repository.GetProductById(product.Id);
        }

        [HttpDelete("DeleteOne/{id}")]
        public ActionResult <bool> Delete(int id)
        {

            ProductModel deleteMe = repository.GetProductById(id);
            int success = repository.Delete(deleteMe);
            if (success != -1)
            {
                return true;
            }    
            return false;
        }

        */

        // Use DTO

        [HttpGet]
        [ResponseType(typeof(List<ProductModelDTO>))]
        public IEnumerable<ProductModelDTO> Index()
        {
            // fetch all products from the database in productModel format.
            List<ProductModel> products = repository.GetAllProducts();

            // List<ProductModelDTO> productDTOs = new List<ProductModelDTO>();
            // translate the list into a list of productModelDTO objects

            IEnumerable<ProductModelDTO> productModelDTOs = from p in products select new ProductModelDTO(p);

            /*foreach (var p in products)
            {
                productDTOs.Add( new ProductModelDTO(p));
            }*/

            return productModelDTOs;
        }


        [HttpGet("showoneproduct/{id}")]
        public ActionResult<ProductModelDTO> ShowDetails(int id)    // ShowOneProduct
        {
            ProductModel p = repository.GetProductById(id);
            ProductModelDTO pDTO = new ProductModelDTO(p);
            return pDTO;
        }

        [HttpGet("searchproducts/{searchTerm}")]
        public IEnumerable<ProductModelDTO> SearchResults(string searchTerm)
        {
            // fetch all products with the search term 
            List<ProductModel> products = repository.SearchProducts(searchTerm);

            // create an empty list of productDtOs
            List<ProductModelDTO> productDTOs = new List<ProductModelDTO>();

            foreach (var p in products)
            {
                productDTOs.Add(new ProductModelDTO(p));
            }

            return productDTOs;
        }

    }
}
