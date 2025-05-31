namespace Limon.Hive.E.Bazar.Application.Actions.Carts.Command;

public class CartCommand : IRequest<LimonHiveActionResponse<CartModel>>
{
    public List<CartCreateCommand> CartCreateCommands { get; set; }
}
