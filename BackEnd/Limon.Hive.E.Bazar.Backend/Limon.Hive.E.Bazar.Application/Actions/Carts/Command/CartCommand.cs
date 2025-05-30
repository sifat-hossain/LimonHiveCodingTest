namespace Limon.Hive.E.Bazar.Application.Actions.Carts.Command;

public class CartCommand : BaseEntity, IRequest<LimonHiveActionResponse<CartModel>>
{
    public Guid ProductId { get; set; }
    public decimal FinalPrice { get; set; }
    public int ProductQuantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CustomerId { get; set; }
}
