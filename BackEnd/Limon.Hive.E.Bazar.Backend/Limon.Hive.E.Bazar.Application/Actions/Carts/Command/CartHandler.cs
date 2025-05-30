namespace Limon.Hive.E.Bazar.Application.Actions.Carts.Command;

public class CartHandler(ILimonHiveDbContext context) : IRequestHandler<CartCommand, LimonHiveActionResponse<CartModel>>
{
    private readonly ILimonHiveDbContext _context = context;

    public async Task<LimonHiveActionResponse<CartModel>> Handle(CartCommand command, CancellationToken cancellationToken)
    {
        try
        {
            Cart? cart = await _context.Cart
                .Where(x => !x.IsDeleted && x.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = command.Id
                };

                await _context.Cart.AddAsync(cart, cancellationToken);
            }

            cart.CustomerId = command.CustomerId;
            cart.ProductId = command.ProductId;
            cart.ProductQuantity = command.ProductQuantity;
            cart.CreatedDate = command.CreatedDate;
            cart.FinalPrice = command.FinalPrice;
            cart.IsDeleted = command.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);

            return new LimonHiveActionResponse<CartModel>
            {
                IsSuccessful = true,
                Message = null,
                Model = CartModel.CreateFromCommand(command)
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
