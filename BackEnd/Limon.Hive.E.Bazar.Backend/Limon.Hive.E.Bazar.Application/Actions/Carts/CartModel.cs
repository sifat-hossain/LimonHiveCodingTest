namespace Limon.Hive.E.Bazar.Application.Actions.Carts;

public class CartModel : BaseModel
{
    public Guid ProductId { get; set; }
    public decimal FinalPrice { get; set; }
    public int ProductQuantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CustomerId { get; set; }

    public static Expression<Func<Cart, CartModel>> Projection
    {
        get
        {
            return entity => new CartModel
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ProductId = entity.ProductId,
                ProductQuantity = entity.ProductQuantity,
                CreatedDate = entity.CreatedDate,
                FinalPrice = entity.FinalPrice,
                IsDeleted = entity.IsDeleted,
            };
        }
    }

    public static CartModel Create(Cart cart)
    {
        return Projection.Compile().Invoke(cart);
    }
}
