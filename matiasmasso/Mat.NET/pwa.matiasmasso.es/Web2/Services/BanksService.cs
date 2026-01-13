using DTO;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Web.Services
{
    public class BanksService
    {
        public List<BankModel>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;


        private AppStateService appstate;
        private CountriesService countriesService;

        public BanksService(AppStateService appstate,
            CountriesService countriesService)
        {
            this.appstate = appstate;
            this.countriesService = countriesService;

            countriesService.OnChange += NotifyChange;
            _ = Task.Run(async () => await FetchAsync());
        }


        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<BankModel>>("Banks") ?? new();
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.Failed;
                    OnChange?.Invoke(ex);
                }
                State = DbState.StandBy;
            }
        }

        public BankModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public BankModel? GetValue(Guid? countryGuid, string? bankId) => Values?.FirstOrDefault(x => x.Country == countryGuid && x.Id == bankId);
        public List<BankModel>? GetValues(Guid? countryGuid) => Values?.Where(x => x.Country == countryGuid)?.ToList();

        public string? BankLogoUrl(Guid? guid) => guid == null ? null : appstate.ApiUrl("bank/logo",guid.ToString()!);
        public string? BankNom(Guid? guid) => GetValue(guid)?.NomComercialOrRaoSocial();

        public List<CountryModel>? Countries(bool? isShowingAllCountries = false) => isShowingAllCountries ?? false ? countriesService.Values : countriesService.Favorites();
        public CountryModel? Country(Guid guid) => countriesService.GetValue(guid);

        public async Task<bool> UpdateAsync(BankModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<BankModel, bool>(value, "Bank");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(BankModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Bank/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }

        private void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Disable()
        {
            countriesService.OnChange -= NotifyChange;
        }
    }
}

