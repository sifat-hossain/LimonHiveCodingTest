
namespace Limon.Hive.E.Bazar.Application.Actions.Products.Command;

public class ProductHandler(ILimonHiveDbContext context) : IRequestHandler<ProductCommand, LimonHiveActionResponse<ProductModel>>
{
    private readonly ILimonHiveDbContext _context = context;

    public async Task<LimonHiveActionResponse<ProductModel>> Handle(ProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            Product? product = await _context.Product
                .Where(x => !x.IsDeleted && x.Id == command.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (product == null)
            {
                product = new Product
                {
                    Id = command.Id
                };

                await _context.Product.AddAsync(product);
            }

            product.Name = command.Name;
            product.ImageUrl = command.ImageUrl;
            product.Price = command.Price;
            product.Quantity = command.Quantity;
            product.DiscountStartDate = command.DiscountStartDate;
            product.DiscountEndDate = command.DiscountEndDate;
            product.IsDeleted = command.IsDeleted;

            await _context.SaveChangesAsync(cancellationToken);

            return new LimonHiveActionResponse<ProductModel>
            {
                IsSuccessful = true,
                Message = null,
                Model = ProductModel.CreateFromCommand(command)
            };

        }
        catch (Exception ex)
        {
            return new LimonHiveActionResponse<ProductModel>
            {
                IsSuccessful = false,
                Message = $"Failed to push product with message: {ex.Message}, " +
                $"Inner exception: {ex.InnerException?.Message}"
            };
        }
    }
}
