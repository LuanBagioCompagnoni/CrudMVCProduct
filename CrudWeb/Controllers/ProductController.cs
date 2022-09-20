using CrudWeb.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace CrudWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        #region requisições
        public bool createProduct(ProductModel model)
        {
            var client = new RestClient("https://localhost:7299/");
            var request = new RestRequest("Api/Create-Product", Method.Post);
            request.AddBody(model);
            return client.Execute<bool>(request).Data;
        }
        public IEnumerable<ProductModel> ListProduct()
        {
            var client = new RestClient("https://localhost:7299/");
            var request = new RestRequest("Api/List-Products", Method.Get);
            return client.Execute<List<ProductModel>>(request).Data;

        }

        public bool DeleteProduct(long id)
        {
            var client = new RestClient("https://localhost:7299");
            var request = new RestRequest("Api/Delete-Product", Method.Delete);
            request.AddParameter("id", id);
            return client.Execute<bool>(request).Data;
        }
        #endregion
        public IActionResult Index()
        {
            ViewBag.Product = ListProduct();
            return View();
        }
    }
}
