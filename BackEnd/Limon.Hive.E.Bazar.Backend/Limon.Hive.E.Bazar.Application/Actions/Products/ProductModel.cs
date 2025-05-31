namespace Limon.Hive.E.Bazar.Application.Actions.Products;

public class ProductModel : BaseModel
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime? DiscountStartDate { get; set; }
    public DateTime? DiscountEndDate { get; set; }

    public static ProductModel CreateFromCommand(ProductCommand command)
    {
        return new ProductModel
        {
            Id = command.Id,
            Name = command.Name,
            ImageUrl = command.ImageUrl,
            Price = command.Price,
            Quantity = command.Quantity,
            DiscountStartDate = command.DiscountStartDate,
            DiscountEndDate = command.DiscountEndDate
        };
    }

    public static Expression<Func<Product, ProductModel>> Projection
    {
        get
        {
            return entity => new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
                Quantity = entity.Quantity,
                DiscountStartDate = entity.DiscountStartDate,
                DiscountEndDate = entity.DiscountEndDate,
                IsDeleted = entity.IsDeleted
            };
        }
    }

    public static ProductModel Create(Product product)
    {
        return Projection.Compile().Invoke(product);
    }
}
