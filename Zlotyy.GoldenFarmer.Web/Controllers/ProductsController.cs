using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Zlotyy.GoldenFarmer.TransportModels.Products;
using Zlotyy.GoldenFarmer.Web.Services;

namespace Zlotyy.GoldenFarmer.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private string ApiUrl(string endpoint = "") => $"/api/v1/products/{endpoint}";
        private readonly IRestService _restService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IRestService restService, ILogger<ProductsController> logger)
        {
            _restService = restService;
            _logger = logger;
        }

        [HttpGet]
        public List<ProductModel> GetProducts()
        {
            var result = _restService.Get<List<ProductModel>>(ApiUrl());
            if (!result.IsSuccessful)
            {
                return new List<ProductModel>();
            }
            return result.Data;
        }
    }
}
