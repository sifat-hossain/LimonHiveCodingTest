namespace Limon.Hive.E.Bazar.Infrastractures.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product), b => b.IsTemporal());

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
           .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.Name)
          .HasMaxLength(Constants.FieldSize.Name);

        builder.Property(b => b.ImageUrl)
          .HasMaxLength(Constants.FieldSize.FilePath);

        builder.Property(b => b.DiscountStartDate)
            .HasColumnType(Constants.Precision.DateTime);

        builder.Property(b => b.DiscountEndDate)
            .HasColumnType(Constants.Precision.DateTime);

        builder.Property(b => b.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(b => b.Price)
            .HasColumnType(Constants.Precision.Decimal);
    }
}
