using Domain.Security;

namespace Infastructure.Data.Configurations;

public class FullNameConfiguration : IEntityTypeConfiguration<CustomIdentityUser>
{
    public void Configure(EntityTypeBuilder<CustomIdentityUser> builder)
    {
        builder.OwnsOne(user => user.FullName, fullName =>
        {
            fullName.Property(f => f.FirstName).HasColumnName("FirstName");
            fullName.Property(f => f.LastName).HasColumnName("LastName");
        });

    }
}