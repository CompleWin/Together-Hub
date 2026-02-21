namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }
    DbSet<Relationship> Relationships { get; }
    DbSet<CustomIdentityUser> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}