using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Zlotyy.GoldenFarmer.Web.Services;

namespace Zlotyy.GoldenFarmer.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public List<object> GetProducts()
        {
            var result = _restService.Get<List<object>>(ApiUrl());
            if (!result.IsSuccessful)
            {
                return new List<object>();
            }
            return result.Data;
        }
    }
}
