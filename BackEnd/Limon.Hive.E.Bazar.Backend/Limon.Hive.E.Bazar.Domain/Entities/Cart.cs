namespace Limon.Hive.E.Bazar.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int ProductQuantity { get; set; }
    public decimal FinalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CustomerId { get; set; }
}
