using DTO;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace Web.Services
{
    public class RafflesService : IDisposable
    {
        public List<RaffleModel>? Values { get; set; }
        public DbState State { get; set; } = DbState.StandBy;
        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private ProductsService productsService;
        private StoreLocatorRawService storeLocatorRawService;

        public RafflesService(AppStateService appstate,
            ProductsService productsService,
            StoreLocatorRawService storeLocatorRawService)
        {
            this.appstate = appstate;
            this.productsService = productsService;
            this.storeLocatorRawService = storeLocatorRawService;

            productsService.OnChange += NotifyChange;
            storeLocatorRawService.OnChange += NotifyChange;
            Task task = Task.Run(async () => await FetchAsync());
        }


        public async Task FetchAsync()
        {
            State = DbState.IsLoading;
            try
            {
                Values = await appstate.GetAsync<List<RaffleModel>>("Raffles") ?? new();
                State = DbState.StandBy;
                OnChange?.Invoke(null);
            }
            catch (Exception ex)
            {
                State = DbState.Failed;
                OnChange?.Invoke(ex);
            }
        }

        public RaffleModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);


        public RaffleModel? Current(CultureService.Tlds tld)
        {
            var country = tld == CultureService.Tlds.pt ? CountryModel.Wellknown(CountryModel.Wellknowns.Portugal)! : CountryModel.Wellknown(CountryModel.Wellknowns.Spain)!;
            var now = DateTime.Now;
            var retval = Values?
                .Where(x => x.FchFrom < now && x.FchTo > now && x.Country == country.Guid)
                .OrderBy(x => x.FchFrom)
                .FirstOrDefault();
            return retval;
        }

        public RaffleModel? Next(CultureService.Tlds tld)
        {
            var country = tld == CultureService.Tlds.pt ? CountryModel.Wellknown(CountryModel.Wellknowns.Portugal)! : CountryModel.Wellknown(CountryModel.Wellknowns.Spain)!;
            var now = DateTime.Now;
            var retval = Values?
                .Where(x => x.FchFrom > now && x.Country == country.Guid)
                .OrderBy(x => x.FchFrom)
                .FirstOrDefault();
            return retval;
        }

        public ProductBrandModel? Brand(RaffleModel? raffle) => productsService.Brand(raffle?.Product);

        public string? ProductName(RaffleModel? raffle, LangDTO lang)
        {
            string? retval = null;
            var product = productsService.GetValue(raffle?.Product);
            if (product?.Src == ProductModel.SourceCods.Sku)
                retval = ((ProductSkuModel?)product)?.NomLlarg?.Tradueix(lang);
            else
                retval = product?.Nom.Tradueix(lang);
            return retval;
        }

        public RaffleModel? CurrentOrNextRaffle(CultureService.Tlds tld) => Current(tld) ?? Next(tld);

        public string CanonicalUrl(CultureService culture)
        {
            var path = culture.Tradueix("sorteos", "sortejos", "raffles", "sorteios");
            var retval = culture.CanonicalUrl(culture.Lang(), path);
            return retval;
        }

        #region Participant

        public RaffleModel.Participant? CreateParticipant(RaffleModel? raffle, UserModel? user)
        {
            RaffleModel.Participant? retval = null;
            if (raffle != null && user != null)
            {
                retval = new RaffleModel.Participant()
                {
                    Raffle = raffle,
                    Lead = user,
                    Fch = DateTime.Now
                };
            }
            return retval;
        }
        #endregion

        #region Distributors

        public List<GuidNom> Areas(RaffleModel? raffle)
        {
            List<GuidNom> retval = new();
            if (raffle != null)
                retval = storeLocatorRawService.Areas(raffle.Product, raffle.Country, forRaffle: true);
            return retval;
        }

        public List<GuidNom> Locations(RaffleModel? raffle, Guid? area)
        {
            List<GuidNom> retval = new();
            if (raffle != null && area != null)
                retval = storeLocatorRawService.Locations(raffle.Product, area, forRaffle: true);
            return retval;
        }

        public List<ContactModel>? Distributors(RaffleModel? raffle, Guid? location)
        {
            List<ContactModel>? retval = null;
            if (raffle != null && location != null)
                retval = storeLocatorRawService.Distributors(raffle.Product, location, forRaffle: true);
            return retval;
        }

        public string? DistributorAddress(ContactModel distributor) => storeLocatorRawService.Address(distributor)?.Text;
        public string? DistributorZipyCit(ContactModel? distributor) => storeLocatorRawService.ZipyCit(distributor);

        public List<string> DistributorNameAndAddressLines(RaffleModel? raffle)
        {
            List<string> retval = new();
            var contact = storeLocatorRawService.Distributor(raffle?.Winner?.Distributor?.Guid);
            var address = storeLocatorRawService.Address(contact);
            var zipyCit = DistributorZipyCit(contact);
            if (contact != null) retval.Add(contact.NomComercialOrRaoSocial());
            if (!string.IsNullOrEmpty(address?.Text)) retval.Add(address.Text);
            if (!string.IsNullOrEmpty(zipyCit)) retval.Add(zipyCit);
            return retval;
        }
        #endregion

        public async Task<int?> Submit(RaffleModel.Participant? participant)
        {
            int retval = 0;
            if (participant == null) throw new Exception("missing raffle participant on RafflesService.Submit");
            retval = await appstate.PostAsync<RaffleModel.Participant, int>(participant, "Raffle");
            return retval;
        }

        public List<RaffleModel>? PastFromLang(LangDTO lang)
        {
            List<RaffleModel>? retval;
            if (lang.IsPor())
                retval = Values?.Where(x => x.Lang!.IsPor()).ToList();
            else
                retval = Values?.Where(x => x.Lang == null || x.Lang!.IsEsp()).ToList();
            retval = retval?.Where(x => x.FchFrom <= DateTime.Now)
                .OrderByDescending(x => x.FchFrom)
                .ToList();
            return retval;
        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            productsService.OnChange -= NotifyChange;
            storeLocatorRawService.OnChange -= NotifyChange;

        }
    }
}
