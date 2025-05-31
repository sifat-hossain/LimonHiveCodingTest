namespace Limon.Hive.E.Bazar.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime? DiscountStartDate { get; set; }
    public DateTime? DiscountEndDate { get; set; }

    public ICollection<Cart> Carts { get; set; }
}
