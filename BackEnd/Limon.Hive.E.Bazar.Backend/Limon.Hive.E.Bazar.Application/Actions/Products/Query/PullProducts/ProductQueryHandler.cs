namespace Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProducts;

public class ProductQueryHandler(ILimonHiveDbContext context) : IRequestHandler<ProductQueryRequest, List<ProductModel>>
{
    private readonly ILimonHiveDbContext _context = context;
    public async Task<List<ProductModel>> Handle(ProductQueryRequest request, CancellationToken cancellationToken)
    {
        List<Product> products = await _context.Product
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);

        return products.Select(x => ProductModel.Create(x)).ToList();
    }
}
