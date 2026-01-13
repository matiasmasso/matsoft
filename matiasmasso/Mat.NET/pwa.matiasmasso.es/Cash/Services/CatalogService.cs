using DTO;

namespace Cash.Services
{
    public class CatalogService
    {
        public List<EmpModel>? Emps { get; set; }
        public List<ContactModel>? Contacts { get; set; }
        public List<PgcCtaModel>? PgcCtas { get; set;}

        public List<PgcClassModel>? PgcClasses { get; set; }
        public List<ProjecteModel>? Projectes { get; set; }
        public List<CountryModel>? Countries { get; set; }
        public List<ZonaModel>? Zonas { get; set; }
        public List<LocationModel>? Locations { get; set; }
        public List<ZipModel>? Zips { get; set; }

        public List<DbState> States { get; set; } = new();


        public enum Items
        {
            Emps,
            Contacts,
            PgcCtas,
            PgcClasses,
            Projectes,
            Countries,
            Zonas,
            Locations,
            Zips
        }

        public CatalogService()
        {
            foreach(Items item in Enum.GetValues<Items>())
            {
                States.Add(DbState.StandBy);
            }
        }

        public void Refresh()
        {
            Contacts = null;
            PgcCtas = null;
            PgcClasses = null;
            Projectes = null;
            Countries = null;
            Zonas = null;
            Locations = null;
            Zips = null;
        }
    }
}
