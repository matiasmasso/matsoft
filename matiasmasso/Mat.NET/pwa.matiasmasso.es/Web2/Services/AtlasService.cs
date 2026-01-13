using DocumentFormat.OpenXml.Drawing;
using DTO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.RegularExpressions;

namespace Web.Services
{
    public class AtlasService : IDisposable
    {


        public event Action<Exception?>? OnChange;

        private CountriesService countriesService;
        private ProvinciasService provinciasService;
        private ZonasService zonasService;
        private LocationsService locationsService;
        private ZipsService zipsService;
        private AddressesService addressesService;
        private ContactsService contactsService;
        private ContactClassesService contactClassesService;
        private LangTextsService langtextsService;


        public AtlasService(
         CountriesService countriesService,
         ProvinciasService provinciasService,
         ZonasService zonasService,
         LocationsService locationsService,
         ZipsService zipsService,
         AddressesService addressesService,
         ContactsService contactsService,
         ContactClassesService contactClassesService, 
         LangTextsService langtextsService)
        {
            this.countriesService = countriesService;
            this.provinciasService = provinciasService;
            this.zonasService = zonasService;
            this.locationsService = locationsService;
            this.zipsService = zipsService;
            this.addressesService = addressesService;
            this.contactsService = contactsService;
            this.contactClassesService = contactClassesService;
            this.langtextsService = langtextsService;

            countriesService.OnChange += NotifyChange;
            zonasService.OnChange += NotifyChange;
            provinciasService.OnChange += NotifyChange;
            locationsService.OnChange += NotifyChange;
            zipsService.OnChange += NotifyChange;
            addressesService.OnChange += NotifyChange;
            contactsService.OnChange += NotifyChange;
            contactClassesService.OnChange += NotifyChange;
            langtextsService.OnChange += NotifyChange;
        }



        public DbState State()
        {
            if (countriesService.State == DbState.IsLoading
                || provinciasService.State == DbState.IsLoading
                || zonasService.State == DbState.IsLoading
                || locationsService.State == DbState.IsLoading
                || zipsService.State == DbState.IsLoading
                || langtextsService.State == DbState.IsLoading
                )
                return DbState.IsLoading;
            else
                return DbState.StandBy;
        }

        public CountryModel? Country(Guid? guid) => countriesService.GetValue(guid);
        public CountryModel? DefaultCountry() => countriesService.GetValue(CountryModel.Default().Guid);

        public List<CountryModel>? Countries(bool? isShowingAllCountries = false) => isShowingAllCountries ?? false ? countriesService.Values : countriesService.Favorites();
        public List<ZonaModel>? Zonas(CountryModel? country) => zonasService.Values?.Where(x => x.Country == country?.Guid).ToList();
        public List<LocationModel>? Locations(ZonaModel? zona, bool isShowingObsolets = false)
        {
            List<LocationModel>? retval;
            if (isShowingObsolets)
                retval = locationsService.Values?.Where(x => x.Zona == zona?.Guid).ToList();
            else
            {
                //retval = locationsService.Values?.Where(x => x.Zona == zona.Guid)?
                //    .SelectMany(x=>zipsService.Values?.Where(y=>y.Location == x.Guid))?
                //    .SelectMany(x => addressesService.Values?.Where(y => y.Zip?.Guid == x.Guid && contactsService.GetValue(y.Contact).Obsoleto == false))?
                //    .Select(x => contactsService.GetValue(x.Contact))?
                //    .Where(x => x != null && x.Obsoleto == false).ToList();

                retval = locationsService.Values?.Where(x => x.Zona == zona?.Guid).ToList();
            }
            return retval;
        }

        public List<ZipModel>? Zips(LocationModel? location) => zipsService.Values?.Where(x => x.Location == location?.Guid).ToList();

        public string? ProvinciaOrZonaNom(Guid guid)
        {
            string? retval = null;
            var provincia = provinciasService.GetValue(guid);
            if (provincia != null)
                retval = provincia.Nom;
            else
            {
                var zona = zonasService.GetValue(guid);
                if (zona != null)
                    retval = zona.Nom;
            }
            return retval;
        }

        public ContactModel? Contact(Guid? guid) => guid == null ? null : contactsService.GetValue(guid);

        public async Task<ContactModel?> GetContactAsync(Guid? guid) => await contactsService.FetchAsync(guid);

        public List<ContactModel>? Contacts(string? searchterm = null)
        {
            if (string.IsNullOrEmpty(searchterm))
                return contactsService.Values;
            else
                return contactsService.Values?.Where(x => x.Matches(searchterm)).ToList();
        }
        public List<ContactModel>? Contacts(LocationModel? location, EmpModel emp, bool isShowingObsolets)
        {
            List<ContactModel>? retval = null;
            if (location != null)
            {
                if (isShowingObsolets)
                    retval = zipsService.Values?.Where(x => x.Location == location.Guid)?
                        .SelectMany(x => addressesService.Values?.Where(y => y.Zip?.Guid == x.Guid))?
                        .Select(x => contactsService.GetValue(x.Contact))?
                        .Where(x => x != null && x.Emp == emp.Id).ToList();
                else
                    retval = zipsService.Values?.Where(x => x.Location == location.Guid)?
                        .SelectMany(x => addressesService.Values?.Where(y => y.Zip?.Guid == x.Guid))?
                        .Select(x => contactsService.GetValue(x.Contact))?
                        .Where(x => x != null && x.Emp == emp.Id && x.Obsoleto == false).ToList();

            }
            return retval;
        }
        public string? LocationNom(Guid? guid) => guid == null ? null : locationsService.GetValue(guid)?.Nom;
        public ZipModel? Zip(Guid? guid) => zipsService.GetValue(guid);
        public string? ZipyCit(ZipDTO? zip)
        {
            string? retval = null;
            if (zip != null)
            {
                var zipmodel = zipsService.GetValue(zip.Guid);
                var location = locationsService.GetValue(zipmodel?.Location);
                if (string.IsNullOrEmpty(zipmodel?.ZipCod))
                    retval = location?.Nom;
                else
                    retval = $"{zipmodel.ZipCod} {location?.Nom}";
            }
            return retval;
        }
        public string? ZipyCit(ZipModel? zip)
        {
            string? retval = null;
            if (zip != null)
            {
                var zipmodel = zipsService.GetValue(zip.Guid);
                var location = locationsService.GetValue(zipmodel?.Location);
                if (string.IsNullOrEmpty(zipmodel?.ZipCod))
                    retval = location?.Nom;
                else
                    retval = $"{zipmodel.ZipCod} {location?.Nom}";
            }
            return retval;
        }

        public ContactModel? CcxOrMe(Guid? contactGuid)
        {
            var contact = Contact(contactGuid);
            ContactModel? retval = Contact(contact?.Ccx) ?? contact;
            return retval;
        }

        public List<string> DeliveryAddress(Guid? srcGuid)
        {
            List<string>? retval = new();
            if (srcGuid != null)
            {
                var address = addressesService.GetValue(srcGuid, AddressModel.Cods.Entregas) ?? addressesService.GetValue(srcGuid, AddressModel.Cods.Fiscal);
                retval = new List<string>
                    {
                        address?.Text ?? "",
                        ZipyCit(address?.Zip) ?? ""
                    };
            }
            return retval;
        }

        public Guid? Channel(Guid? customer)
        {
            var contact = contactsService.GetValue(customer);
            var contactClass = contactClassesService.GetValue(contact?.ContactClass);
            var retval = contactClass?.Channel;
            return retval;
        }

        public AddressModel? Address(Guid? srcGuid, AddressModel.Cods srcCod) => addressesService.GetValue(srcGuid, srcCod);

        public List<ZipModel>? GetZips(CountryModel country, string? zipCode) => zipsService.Values?.Where(x => x.ZipCod == zipCode?.Trim() && Country(x)?.Guid == country.Guid)?.ToList();
        public LocationModel? Location(ZipModel zip) => Location(zip.Location);
        public LocationModel? Location(Guid? guid) => locationsService.GetValue(guid);
        public List<LocationModel>? Locations() => locationsService.Values;
        public ZonaModel? Zona(LocationModel? location) => zonasService.GetValue(location?.Zona);

        public CountryModel? Country(ZipModel zip)
        {
            var location = Location(zip);
            var zona = Zona(location);
            return countriesService.GetValue(zona?.Country);
        }

        public async Task UpdateZonaAsync(ZonaModel value)
        {
            await zonasService.UpdateAsync(value);
        }

        public async Task UpdateLocationAsync(LocationModel value)
        {
            await locationsService.UpdateAsync(value);
        }

        public async Task UpdateZipAsync(ZipModel value)
        {
            await zipsService.UpdateAsync(value);
        }

        public async Task FetchAsync()
        {
            await zipsService.FetchAsync();
            await locationsService.FetchAsync();
            await zonasService.FetchAsync();
            await countriesService.FetchAsync();
        }

        void NotifyChange(Exception? ex)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            countriesService.OnChange -= NotifyChange;
            zonasService.OnChange -= NotifyChange;
            provinciasService.OnChange -= NotifyChange;
            locationsService.OnChange -= NotifyChange;
            zipsService.OnChange -= NotifyChange;
            addressesService.OnChange -= NotifyChange;
            contactsService.OnChange -= NotifyChange;
            contactClassesService.OnChange -= NotifyChange;
            langtextsService.OnChange -= NotifyChange;
        }

    }
}
