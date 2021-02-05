using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Zlotyy.GoldenFarmer.Database;

namespace Zlotyy.GoldenFarmer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : BaseSecureController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IDbContextFactory _dbFactory;

        public ProductsController(ILogger<ProductsController> logger, IDbContextFactory dbFactory)
        {
            _logger = logger;
            _dbFactory = dbFactory;
        }

        [HttpGet]
        public List<Products> GetProducts()
        {
            _logger.LogDebug($"Request {nameof(GetProducts)}");
            using (var db = _dbFactory.Create())
            {
                var result = db.Products.ToList();
                return result;
            }
        }
    }
}
