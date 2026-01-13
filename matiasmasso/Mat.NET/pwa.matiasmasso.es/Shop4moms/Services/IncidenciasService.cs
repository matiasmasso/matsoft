using DTO;
using System.Net.Http.Headers;

namespace Shop4moms.Services
{
    public class IncidenciasService
    {
        public List<IncidenciaModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action? OnChange;
        private AppStateService appstate;

        public IncidenciasService(AppStateService appstate)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                Values = await appstate.GetAsync<List<IncidenciaModel>>("Incidencias") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke();
            }
        }

        public IncidenciaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(IncidenciaModel incidencia)
        {
            var docfiles = new List<DocFileModel>();
            docfiles.AddRange(incidencia.PurchaseTickets);
            docfiles.AddRange(incidencia.DocFileImages);
            docfiles.AddRange(incidencia.Videos);

            return await appstate.PostMultipartAsync<IncidenciaModel, bool>(incidencia, docfiles, "incidencia");
        }

        public IncidenciaModel? Factory(UserModel? user)
        {
            IncidenciaModel? retval = null;
            if (user != null)
            {
                retval = new IncidenciaModel()
                {
                    Emp = appstate.EmpId,
                    EmailAdr = user?.EmailAddress,
                    Customer = CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor)!.Guid,
                    Fch = DateTime.Now,
                    ContactType = IncidenciaModel.ContactTypes.consumidor,
                    Src = IncidenciaModel.Srcs.Producte,
                    ContactPerson = user?.NomiCognomsOrNickname(),
                    Tel = user?.Tel,
                    UsrLog = UsrLogModel.Factory(user)
                };
            }
            return retval;
        }

    }
}


