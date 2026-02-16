using System.Reflection;
using Application.Data.DataBaseContext;
using Domain.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infastructure.Data.DataBaseContext;

public class ApplicationDbContext : IdentityDbContext<CustomIdentityUser>, IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();

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