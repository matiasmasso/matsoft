using DocumentFormat.OpenXml.Bibliography;
using DTO;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Org.BouncyCastle.Crypto.Engines;
using Web.LocalComponents;

namespace Web.Services
{
    public class CcasService : IDisposable
    {
        public List<CcaModel>? Values { get; set; }
        public int ValuesCount { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private AtlasService atlas;
        private PgcCtasService pgcCtasService;

        public CcasService(AppStateService appstate, AtlasService atlas, PgcCtasService pgcCtasService)
        {
            this.appstate = appstate;
            this.atlas = atlas;
            this.pgcCtasService = pgcCtasService;

            atlas.OnChange += NotifyChange;
            pgcCtasService.OnChange += NotifyChange;
        }

        public async Task FetchAsync(EmpModel emp, int year)
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    var empId = (int)emp.Id;
                    Values = await appstate.GetAsync<List<CcaModel>>("Ccas", empId.ToString(), year.ToString()) ?? new();
                    State = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
            }
        }

        public async Task<CcaModel?> Find(Guid guid)
        {
            return await appstate.GetAsync<CcaModel>("Cca", guid.ToString());
        }
        public CcaModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public string? CtaIdNom(Guid guid, LangDTO lang) => pgcCtasService.IdNom(guid, lang);
        public string? ContactNom(Guid? guid) => atlas.Contact(guid)?.FullNom;

        public async Task<bool> UpdateAsync(CcaModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval =await appstate.PostMultipartAsync<CcaModel, bool>(value, value.Docfile, "cca");
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(CcaModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Cca", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            atlas.OnChange -= NotifyChange;
            pgcCtasService.OnChange -= NotifyChange;
        }
    }
}


