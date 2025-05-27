namespace Limon.Hive.E.Bazar.Application.Actions.Carts.Query;

public class CartQueryHandler(ILimonHiveDbContext context) : IRequestHandler<CartQueryRequest, List<CartModel>>
{
    private readonly ILimonHiveDbContext _context = context;
    public async Task<List<CartModel>> Handle(CartQueryRequest request, CancellationToken cancellationToken)
    {
        List<Cart> carts = await _context.Cart
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);

        return carts.Select(x => CartModel.Create(x)).ToList();
    }
}
