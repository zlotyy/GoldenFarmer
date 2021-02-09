using System.Collections.Generic;
using System.Linq;
using Zlotyy.GoldenFarmer.Database;

namespace Zlotyy.GoldenFarmer.Api.Queries
{
    public interface IProductQueries
    {
        List<Products> GetProducts();
    }

    public class ProductQueries : IProductQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public ProductQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Products> GetProducts()
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Products.ToList();                
            }
        }
    }
}
