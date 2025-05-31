using Limon.Hive.Frontend.Entities;

namespace Limon.Hive.Frontend.Response;

public class ProductResponse
{
    public int TotalProductCount { get; set; }
    public int TotalProductQuatity { get; set; }
    public List<Product> Products { get; set; }
}
