namespace Limon.Hive.E.Bazar.Infrastractures;

public class LimonHiveDbContext(DbContextOptions options) : DbContext(options), ILimonHiveDbContext
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Cart> Cart { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LimonHiveDbContext).Assembly);
    }
}
