using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data.DataBaseContext;

public class ApplicationDbContext : IdentityDbContext<CustomIdentityUser>, IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Relationship> Relationships => Set<Relationship>();
    public DbSet<Comment> Comments => Set<Comment>();
    
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
    }
}