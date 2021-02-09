using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Zlotyy.GoldenFarmer.Api.Queries;
using Zlotyy.GoldenFarmer.TransportModels.Products;

namespace Zlotyy.GoldenFarmer.Api.Controllers
{
    public class ProductsController : BaseSecureController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductQueries _queries;

        public ProductsController(ILogger<ProductsController> logger, IProductQueries queries)
        {
            _logger = logger;
            _queries = queries;
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetProducts()
        {
            _logger.LogDebug($"Request {nameof(GetProducts)}");
            return _queries.GetProducts().Select(s => new ProductModel(s));
        }
    }
}
