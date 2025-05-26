namespace Limon.Hive.E.Bazar.Infrastractures.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(nameof(Cart), b => b.IsTemporal());

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
           .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.FinalPrice)
            .HasColumnType(Constants.Precision.Decimal);

        builder.Property(b => b.CreatedDate)
          .HasColumnType(Constants.Precision.DateTime);

        builder.Property(b => b.IsDeleted)
          .HasDefaultValue(false);

        builder.HasOne(b => b.Product)
            .WithMany(c => c.Carts)
            .HasForeignKey(c => c.ProductId);

    }
}
