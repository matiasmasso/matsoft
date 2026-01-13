using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace Shop4moms.Services
{
    public interface ICatalogService
    {
        public DTO.CacheDTO Cache { get; set; }
        public bool IsLoaded { get; set; }
        public AsyncEventHandler<DTO.Helpers.MatEventArgs<ProblemDetails>>? Loaded { get; set; }
        public Task LoadCatalogAsync();

        public Task ReloadStringsLocalizerAsync();

    }
}
