namespace Limon.Hive.E.Bazar.Application.Actions.Products.Command;

public class ProductCommand : BaseEntity, IRequest<LimonHiveActionResponse<ProductModel>>
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime DiscountStartDate { get; set; }
    public DateTime DiscountEndDate { get; set; }
}
