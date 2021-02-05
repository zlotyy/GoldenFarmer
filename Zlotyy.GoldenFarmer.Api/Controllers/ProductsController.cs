using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Zlotyy.GoldenFarmer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : BaseSecureController
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<object> GetProducts()
        {
            _logger.LogDebug($"Request {nameof(GetProducts)}");
            return new List<object>
            {
                new
                {
                    ProductId = 1,
                    Name = "Ziemniak",
                    IsAvailable = 1
                }
            };
        }
    }
}
