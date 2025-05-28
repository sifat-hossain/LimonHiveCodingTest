namespace Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProductById;

public class PullProductByIdQueryHandler(ILimonHiveDbContext context) : IRequestHandler<PullProductByIdQueryRequest, ProductModel>
{
    private readonly ILimonHiveDbContext _context = context;
    public async Task<ProductModel> Handle(PullProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Product product = await _context.Product
            .Where(x => !x.IsDeleted && x.Id == request.ProductId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return ProductModel.Create(product);
    }
}
