using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using marketplaceE.appDbContext;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // ЗАМЕНИ строку подключения на свою!
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Marketplace;Username=postgres;Password=sendHELP52");

        return new AppDbContext(optionsBuilder.Options);
    }
}