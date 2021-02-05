namespace Zlotyy.GoldenFarmer.Database
{
    public interface IDbContextFactory
    {
        GoldenFarmerDB Create();
    }

    public class DbContextFactory : IDbContextFactory
    {
        public GoldenFarmerDB Create()
        {
            return new GoldenFarmerDB();
        }
    }
}
