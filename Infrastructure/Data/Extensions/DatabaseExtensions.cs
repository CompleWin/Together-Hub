namespace Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        var dbContext = scope
            .ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        var manager = scope
            .ServiceProvider
            .GetRequiredService<UserManager<CustomIdentityUser>>();
        
        await dbContext.Database.MigrateAsync();
        await SeedData(dbContext, manager);
    }

    private static async Task SeedData(ApplicationDbContext dbContext,
        UserManager<CustomIdentityUser> manager)
    {
        await SeedTopicAsync(dbContext);
        await SeedUsersAsync(manager);
    }

    

    private static async Task SeedTopicAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Topics.AnyAsync())
        {
            await dbContext.Topics.AddRangeAsync(InitialData.Topics);
            await dbContext.SaveChangesAsync();
        }
    }
    
    private static async Task SeedUsersAsync(UserManager<CustomIdentityUser> manager)
    {
        if (!await manager.Users.AnyAsync())
        {
            foreach (var user in InitialData.Users)
            {
                await manager.CreateAsync(user, "1111");
            }
        }
    }
}