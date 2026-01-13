using DTO;

namespace Web.Services
{
    public class IncidenciasService:IDisposable
    {
        public event Action<Exception?>? OnChange;

        private AppStateService appstate;
        private AtlasService atlasService;
        private ProductsService productsService;
        public IncidenciasService(AppStateService appstate, AtlasService atlasService, ProductsService productsService)
        {
            this.appstate = appstate;
            this.atlasService = atlasService;
            this.productsService = productsService;

            atlasService.OnChange += NotifyChange;
           productsService.OnChange += NotifyChange;
        }

        public async Task<List<IncidenciaModel>?> GetOpenValuesAsync()
        {
            return await appstate.GetAsync<List<IncidenciaModel>>("incidencias/open", ((int)EmpModel.EmpIds.MatiasMasso).ToString());
        }
        public async Task<IncidenciaModel?> GetValueAsync(Guid guid)
        {
            return await appstate.GetAsync<IncidenciaModel>("incidencia", guid.ToString());
        }

        public string? CustomerFullNom(IncidenciaModel? incidencia) => incidencia == null ? null : atlasService.Contact(incidencia.Customer)?.FullNom;
        public LangTextModel? ProductNom(IncidenciaModel? incidencia) => incidencia == null ? null : productsService.Nom(incidencia.Product);
        public LangTextModel ProcedenciaNom(IncidenciaModel? incidencia){
            return new LangTextModel( incidencia?.Procedencia.ToString());
        }

        public List<LangTextModel> Procedencias()
        {
            return new List<LangTextModel> {
                new LangTextModel("No especificada","No especificada","Not set"),
                new LangTextModel("Adquirida en mi establecimiento","Adquirida en el meu establiment","Purchased on my shop"),
                new LangTextModel("Adquirida en otros establecimientos","Adquirida en altres establiments","Purchased on other shops"),
                new LangTextModel("De mi exposición","De la meva exposició","From my exhibition"),
            };
        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            atlasService.OnChange -= NotifyChange;
            productsService.OnChange -= NotifyChange;
        }
    }
}
