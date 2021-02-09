using System;
using DbProducts = Zlotyy.GoldenFarmer.Database.Products;

namespace Zlotyy.GoldenFarmer.TransportModels.Products
{
    public class ProductModel
	{
        public ProductModel()
        {
		}

		public ProductModel(DbProducts dbProduct)
		{
			ProductId = dbProduct.ProductId;
			Name = dbProduct.Name;
			IsAvailable = dbProduct.IsAvailable;
			Units = dbProduct.Units;
			SoonExpire = dbProduct.SoonExpire;
			SoonAvailable = dbProduct.SoonAvailable;
			AvailableDate = dbProduct.AvailableDate;
		}

		public int ProductId { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
		public string Units { get; set; }
		public bool SoonExpire { get; set; }
		public bool SoonAvailable { get; set; }
		public DateTime? AvailableDate { get; set; }
	}
}
