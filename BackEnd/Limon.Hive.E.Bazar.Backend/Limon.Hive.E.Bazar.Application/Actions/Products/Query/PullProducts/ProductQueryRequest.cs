namespace Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProducts;

public class ProductQueryRequest : IRequest<ProductResponse>
{
    public int? Skip { get; set; }
    public int? Take { get; set; }
}
