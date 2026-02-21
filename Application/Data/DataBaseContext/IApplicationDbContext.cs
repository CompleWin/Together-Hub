namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }
    DbSet<Relationship> Relationships { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}