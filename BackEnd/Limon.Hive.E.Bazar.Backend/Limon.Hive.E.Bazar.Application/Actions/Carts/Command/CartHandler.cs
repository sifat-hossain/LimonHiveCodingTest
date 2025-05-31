namespace Limon.Hive.E.Bazar.Application.Actions.Carts.Command;

public class CartHandler(ILimonHiveDbContext context) : IRequestHandler<CartCommand, LimonHiveActionResponse<CartModel>>
{
    private readonly ILimonHiveDbContext _context = context;

    public async Task<LimonHiveActionResponse<CartModel>> Handle(CartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var item in command.CartCreateCommands)
            {
                Cart? cart = await _context.Cart
                    .Where(x => !x.IsDeleted && x.Id == item.Id)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        Id = item.Id
                    };

                    await _context.Cart.AddRangeAsync(cart);
                }

                cart.CustomerId = item.CustomerId;
                cart.ProductId = item.ProductId;
                cart.ProductQuantity = item.ProductQuantity;
                cart.CreatedDate = item.CreatedDate;
                cart.FinalPrice = item.FinalPrice;
            }
            await _context.SaveChangesAsync(cancellationToken);

            return new LimonHiveActionResponse<CartModel>
            {
                IsSuccessful = true,
                Message = null
            };

        }
        catch (Exception ex)
        {
            return new LimonHiveActionResponse<CartModel>
            {
                IsSuccessful = false,
                Message = $"Failed to push cart with message: {ex.Message}, " +
                $"Inner exception: {ex.InnerException?.Message}"
            };
        }
    }
}
