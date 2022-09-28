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
        public List<ProductModel> ListProduct()
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
        public ProductModel GetProduct(long id)
        {
            var client = new RestClient("https://localhost:7299");
            var request = new RestRequest("Api/Get-Product", Method.Get);
            request.AddParameter("id", id);
            return client.Execute<ProductModel>(request).Data;
        }
        #endregion
        public IActionResult Index()
        {
            ViewBag.Product = ListProduct();
            return View();
        }
        public IActionResult Editar(long id)
        {
            var product = GetProduct(id);
            var model = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
            return View("Criar", model);
        }

        public IActionResult Criar(ProductModel model)
        {
            ViewBag.Product = createProduct(model);
            return View();
        }

        public IActionResult Excluir(long id)
        {
            DeleteProduct(id);
            return RedirectToAction("Index", "Product");
        }

    }
}
