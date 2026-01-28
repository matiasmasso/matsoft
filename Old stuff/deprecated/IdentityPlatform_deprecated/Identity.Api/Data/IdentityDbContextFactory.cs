using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Api.Data;

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

        // Use the same connection string as appsettings.json
        optionsBuilder.UseSqlServer(
            "Data Source=10.74.52.10;TrustServerCertificate=true;Initial Catalog=Identity;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc");

        return new IdentityDbContext(optionsBuilder.Options);
    }
}