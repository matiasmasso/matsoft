using DTO;
using iText.StyledXmlParser.Jsoup.Helper;


namespace Web.Services
{
    public class IbansService : IDisposable
    {
        public List<IbanModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private BankBranchesService bankBranchesService;
        private IbanStructuresService ibanStructuresService;

        public IbansService(AppStateService appstate,
            IbanStructuresService ibanStructuresService,
            BankBranchesService bankBranchesService)
        {
            this.appstate = appstate;
            _ = Task.Run(async () => await FetchAsync());

            this.bankBranchesService = bankBranchesService;
            this.ibanStructuresService = ibanStructuresService;

            bankBranchesService.OnChange += NotifyChange;
            ibanStructuresService.OnChange += NotifyChange;
        }

        public async Task FetchAsync()
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    Values = await appstate.GetAsync<List<IbanModel>>("Ibans") ?? new();
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

        public IbanModel? Parse(string? ccc)
        {
            IbanModel? retval = new IbanModel { Ccc = IbanStructureModel.CleanCcc(ccc) };
            ccc = IbanStructureModel.CleanCcc(ccc);
            var dbCandidates = Values?.Where(x=>x.Ccc == ccc).ToList();
            if(dbCandidates?.Count == 0)
            {
                retval = new IbanModel { Ccc = IbanStructureModel.CleanCcc(ccc) };
                var ibanStructure = ibanStructuresService.GetValue(ccc);
                if (ibanStructure != null)
                {
                    var countryGuid = ibanStructure.Country;
                    var bankId = ibanStructure.GetBankId(ccc);
                    var branchId = ibanStructure.GetBranchId(ccc);
                    retval.Branch = bankBranchesService.GetValue(countryGuid, bankId, branchId)?.Guid;
                }
            } else
            {
                var candidate = dbCandidates!.OrderByDescending(x => x.FchFrom).FirstOrDefault();
                retval.Branch = candidate!.Branch;
            }
            return retval;
        }

        public bool IsValidIban(string? ccc)
        {
            bool retval = false;
            ccc = IbanStructureModel.CleanCcc(ccc);
            var ibanStructure = ibanStructuresService.GetValue(ccc);
            if (ibanStructure != null)
            {
                var countryGuid = ibanStructure.Country;
                var bankId = ibanStructure.GetBankId(ccc);
                var branchId = ibanStructure.GetBranchId(ccc);
                var branch = bankBranchesService.GetValue(countryGuid, bankId, branchId);
                retval = branch != null;
            }
            return retval;
        }

        public string? Swift(Guid? branchGuid) => bankBranchesService.Swift(branchGuid);
        public IbanModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);
        public IbanModel? GetActiveValue(IbanModel.Cods cod, Guid contact) => Values?
            .FirstOrDefault(x => x.Titular == contact && x.Cod == cod && x.IsActive());
        public IbanModel? FromStaff(Guid? guid) => guid == null ? null : Values?
            .FirstOrDefault(x => x.Titular == guid && x.Cod == IbanModel.Cods.staff && x.IsActive());
        public List<IbanModel>? FromBanc(Guid? guid) => guid == null ? null : Values?
            .Where(x => x.Titular == guid && x.Cod == IbanModel.Cods.banc && x.IsActive()).ToList();

        public CountryModel? DefaultCountry() => bankBranchesService.DefaultCountry();
        public List<CountryModel>? Countries(bool isShowingAllCountries) => bankBranchesService.Countries(isShowingAllCountries);
        public List<BankModel>? Banks(CountryModel? country) => bankBranchesService.Banks(country);
        public List<BankModel.Branch>? Branches(BankModel? bank) => bankBranchesService.Values?.Where(x=>x.Bank == bank.Guid).ToList();


        public string BankLogoUrl(IbanModel iban) => bankBranchesService.BankLogoUrl(iban.Branch);
        public string? BankIdNom(string? src) {
            string? retval = null;
            var ccc = IbanStructureModel.CleanCcc(src);
            var ibanStructure = ibanStructuresService.GetValue(ccc);
            if (ibanStructure != null)
            {
                var countryGuid = ibanStructure.Country;
                var bankId = ibanStructure.GetBankId(ccc);
                var bank = bankBranchesService.Bank(countryGuid, bankId);
                retval = $"{bankId} {bank?.NomComercialOrRaoSocial()}";
            }
            return retval;
        }

        public BankModel? Bank(string? src)
        {
            BankModel? retval = null;
            var ccc = IbanStructureModel.CleanCcc(src);
            var ibanStructure = ibanStructuresService.GetValue(ccc);
            if (ibanStructure != null)
            {
                var countryGuid = ibanStructure.Country;
                var bankId = ibanStructure.GetBankId(ccc);
                retval = bankBranchesService.Bank(countryGuid, bankId);
            }
            return retval;
        }

        public BankModel.Branch? Branch(string? src)
        {
            BankModel.Branch? retval = null;
            var ccc = IbanStructureModel.CleanCcc(src);
            var ibanStructure = ibanStructuresService.GetValue(ccc);
            if (ibanStructure != null)
            {
                var countryGuid = ibanStructure.Country;
                var bankId = ibanStructure.GetBankId(ccc);
                var bank = bankBranchesService.Bank(countryGuid, bankId);
                var branchId = ibanStructure.GetBranchId(ccc);
                retval = bankBranchesService.GetValue(countryGuid, bankId, branchId);
            }
            return retval;
        }

        public string? BranchIdNom(string src)
        {
            string? retval = null;
            var ccc = IbanStructureModel.CleanCcc(src);
            var ibanStructure = ibanStructuresService.GetValue(ccc);
            if (ibanStructure != null)
            {
                var countryGuid = ibanStructure.Country;
                var bankId = ibanStructure.GetBankId(ccc);
                var bank = bankBranchesService.Bank(countryGuid, bankId);
                var branchId = ibanStructure.GetBranchId(ccc);
                var branch = bankBranchesService.GetValue(countryGuid, bankId, branchId)?.Guid;
                var locationAndAddress = bankBranchesService.LocationAndAddress(branch);

                retval = $"{branchId} {locationAndAddress}";
            }
            return retval;
        }
        public string BranchAddress(IbanModel? iban) => bankBranchesService.LocationAndAddress(iban?.Branch);
        public string? FormattedCcc(string? src)
        {
            string? retval = null;
            if (!string.IsNullOrEmpty(src))
            {
                var ccc = IbanStructureModel.CleanCcc(src);
                retval = string.Join(' ', ccc.Chunk(size: 4).Select(x => new string(x)));
            }
            return retval;
        }

        public static string CleanCcc(string? src) => IbanStructureModel.CleanCcc(src);

        public async Task<bool> UpdateAsync(IbanModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<IbanModel, bool>(value, "Iban");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(IbanModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Iban/delete", value.Guid.ToString());
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
            bankBranchesService.OnChange -= NotifyChange;
            ibanStructuresService.OnChange -= NotifyChange;
        }
    }
}

