namespace Identity.Services.Implementations
{
    using Identity.Data;
    using Identity.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppService : IAppService
    {
        private readonly IdentityDbContext _db;

        public AppService(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<App>> GetAll()
        {
            return await _db.Apps.ToListAsync();
        }

        public async Task<App?> GetById(Guid appId)
        {
            return await _db.Apps.FirstOrDefaultAsync(a => a.AppId == appId);
        }
    }

}
