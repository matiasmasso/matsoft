using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.Api.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseSqlServer("Data Source=10.74.52.10;TrustServerCertificate=true;Initial Catalog=Identity;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc")
            .Options;

        return new IdentityDbContext(options);
    }
}