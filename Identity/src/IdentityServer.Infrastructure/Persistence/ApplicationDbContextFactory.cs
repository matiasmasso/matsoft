using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Adjust connection string as needed
        optionsBuilder.UseSqlServer(
            "Data Source=10.74.52.10;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc;Database=IdentityServer;Trust Server Certificate=True");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}