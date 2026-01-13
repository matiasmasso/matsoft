using Microsoft.VisualStudio.Threading;
using DTO;
using DTO.Integracions.Redsys;

namespace Components
{
    public interface IAppState
    {
        CacheDTO Cache();

        public Task SendMailAsync(string To, string Subject, string Body); 

    }
}
