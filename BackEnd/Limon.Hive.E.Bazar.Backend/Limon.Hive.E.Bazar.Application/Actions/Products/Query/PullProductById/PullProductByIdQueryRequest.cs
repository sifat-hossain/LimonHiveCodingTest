namespace Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProductById;

public class PullProductByIdQueryRequest : IRequest<ProductModel>
{
    public Guid ProductId { get; set; }
}
