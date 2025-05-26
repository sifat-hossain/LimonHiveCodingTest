using Limon.Hive.E.Bazar.Domain.Entities;

namespace Limon.Hive.E.Bazar.Application;

public interface ILimonHiveDbContext
{
    DbSet<Product> Product { get; set; }
    DbSet<Cart> Cart { get; set; }

    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
