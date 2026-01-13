using Identity.Domain.Entities;

namespace Identity.Services
{
    public interface IAppService { 
        Task<IEnumerable<App>> GetAll(); 
        Task<App?> GetById(Guid appId); 
    }
}
