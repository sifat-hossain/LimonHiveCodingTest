using Limon.Hive.E.Bazar.Application.Actions.Products;

namespace Limon.Hive.E.Bazar.Application.Responses;

public class ProductResponse
{
    public int TotalProductCount { get; set; }
    public int TotalProductQuatity { get; set; }
    public List<ProductModel> Products { get; set; }
}
