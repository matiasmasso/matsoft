using DTO;


namespace Web.Services
{
    public class BankBranchesService : IDisposable
    {
        public List<BankModel.Branch>? Values { get; set; }

        public event Action<Exception?>? OnChange;
        public DbState State { get; set; } = DbState.StandBy;
        private AppStateService appstate;
        private AtlasService atlas;
        private BanksService banksService;

        public BankBranchesService(AppStateService appstate, BanksService banksService, AtlasService atlas)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());

            this.atlas = atlas;
            this.banksService = banksService;

            atlas.OnChange += NotifyChange;
            banksService.OnChange += NotifyChange;
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<BankModel.Branch>>("BankBranches") ?? new();
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

        public CountryModel? DefaultCountry() => atlas.DefaultCountry();
        public List<CountryModel>? Countries(bool isShowingAllCountries) => atlas.Countries(isShowingAllCountries);
        public List<BankModel>? Banks(CountryModel? country) => banksService.Values?.Where(x => x.Country == country?.Guid).ToList();
        public List<BankModel>? Banks() => banksService.Values;
        public BankModel? Bank(Guid? guid) => banksService.GetValue(guid);
        public List<LocationModel>? Locations() => atlas.Locations();
        public LocationModel? Location(Guid? guid) => atlas.Location(guid);
        public string? BankLogoUrl(Guid? branchGuid) => banksService.BankLogoUrl(GetValue(branchGuid)?.Bank);
        public string? BankNom(Guid? branchGuid) => banksService.BankNom(GetValue(branchGuid)?.Bank);
        public string? LocationAndAddress(Guid? branchGuid) => LocationAndAddress(GetValue(branchGuid));
        public string? LocationAndAddress(BankModel.Branch? branch)
        {
            string? retval = null;
            if (branch != null)
            {
                var location = atlas.Location(branch?.Location);
                retval = $"{location?.Nom} - {branch?.Adr}";
            }
            return retval;
        }


        public BankModel.Branch? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public BankModel.Branch? GetValue(Guid? bankGuid, string? branchId) => Values?.FirstOrDefault(x => x.Bank == bankGuid && x.Id == branchId);
        public BankModel.Branch? GetValue(Guid? countryGuid, string? bankId, string? branchId)
        {
            BankModel.Branch? retval = null;
            var bank = Bank(countryGuid, bankId);
            if (bank != null)
                retval = GetValue(bank.Guid, branchId);
            return retval;
        }
        public BankModel? Bank(Guid? countryGuid, string? bankId) => banksService.GetValue(countryGuid, bankId);
        public string? Swift(Guid? branchGuid)
        {
            string? retval = null;
            var branch = GetValue(branchGuid);
            if (branch != null)
            {
                if (string.IsNullOrEmpty(branch.Swift))
                    retval = banksService.GetValue(branch.Bank)?.Swift;
                else
                    retval = branch.Swift;
            }
            return retval;
        }

        public List<CountryModel>? Countries() => atlas.Countries();




        public async Task<bool> UpdateAsync(BankModel.Branch value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<BankModel.Branch, bool>(value, "BankBranch");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(BankModel.Branch? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("BankBranch/delete", value.Guid.ToString());
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
            banksService.OnChange -= NotifyChange;
        }
    }
}

