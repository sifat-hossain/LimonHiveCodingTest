namespace Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProductByName;

public class PullProductByNameQueryRequest : IRequest<ProductResponse>
{
    public string ProductName { get; set; }
}
