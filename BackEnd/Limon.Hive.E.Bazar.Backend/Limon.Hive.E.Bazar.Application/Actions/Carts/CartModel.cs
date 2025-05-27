using Limon.Hive.E.Bazar.Application.Actions.Carts.Command;

namespace Limon.Hive.E.Bazar.Application.Actions.Carts;

public class CartModel : BaseModel
{
    public Guid ProductId { get; set; }
    public decimal FinalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CustomerId { get; set; }

    public static CartModel CreateFromCommand(CartCommand command)
    {
        return new CartModel
        {
            CustomerId = command.CustomerId,
            ProductId = command.ProductId,
            CreatedDate = command.CreatedDate,
            FinalPrice = command.FinalPrice,
            IsDeleted = command.IsDeleted,
        };
    }

    public static Expression<Func<Cart, CartModel>> Projection
    {
        get
        {
            return entity => new CartModel
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ProductId = entity.ProductId,
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
