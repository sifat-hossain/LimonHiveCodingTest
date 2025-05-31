namespace Limon.Hive.E.Bazar.Application.Actions.Products.Query.PullProductByName;

public class PullProductByNameQueryHandler(ILimonHiveDbContext context) :
    IRequestHandler<PullProductByNameQueryRequest, ProductResponse>
{
    private readonly ILimonHiveDbContext _context = context;
    public async Task<ProductResponse> Handle(PullProductByNameQueryRequest request, CancellationToken cancellationToken)
    {
        List<Product> products = await _context.Product
            .Where(x => !x.IsDeleted && x.Name.Contains(request.ProductName))
            .ToListAsync(cancellationToken: cancellationToken);

        var totalProductCount = await _context.Product.CountAsync(cancellationToken: cancellationToken);
        var totalProductQuantity = await _context.Product
            .SumAsync(x => x.Quantity, cancellationToken);

        return new ProductResponse()
        {
            TotalProductCount = totalProductCount,
            TotalProductQuatity = totalProductQuantity,
            Products = products.Select(x => ProductModel.Create(x)).ToList()
        };
    }
}
