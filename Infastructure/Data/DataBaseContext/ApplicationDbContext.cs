using Application.Data.DataBaseContext;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data.DataBaseContext;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Topic>()
            .Property(topic => topic.Id)
            .HasConversion(
                id => id.Value, value => TopicId.Of(value)
                );

        modelBuilder.Entity<Topic>()
            .OwnsOne(topic => topic.Location, location =>
            {
                location.Property(l => l.City).HasColumnName("City");
                location.Property(l => l.Street).HasColumnName("Street");
            });
    }
}