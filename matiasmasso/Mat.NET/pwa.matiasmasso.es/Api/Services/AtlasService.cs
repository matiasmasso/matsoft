using DTO;
namespace Api.Services
{
    public class AtlasService
    {
        public static AtlasModel Model(AtlasModel request)
        {
            //Compares client last refresh with api last refresh and with database last time it was updated
            //and reads from database only if needed

            using (var db = new Entities.MaxiContext())
            {
                var retval = request;

                //check if database data is more recent than client cache
                //var lastUpdate = SQLService.LastTablesUpdate(db, "Country", "Zona", "Location", "Zip", "Provincia");
                //if (request.Fch == null || lastUpdate > request.Fch)
                //{
                //    //check if database data is more recent than Api cache
                //    var emp = (int)EmpDTO.Ids.MatiasMasso;
                //    var apiCache = Api.Models.AppState.Cache(emp);
                //    if (apiCache.Atlas.Fch == null || lastUpdate > apiCache.Atlas.Fch)
                //    {
                //        apiCache.Atlas.Fch = DateTime.Now;
                //        apiCache.Atlas.Countries = CountriesService.All(db);
                //        apiCache.Atlas.Regions = RegionsService.All(db);
                //        apiCache.Atlas.Provincias = ProvinciasService.All(db);
                //        apiCache.Atlas.Zonas = ZonasService.All(db);
                //        apiCache.Atlas.Locations = LocationsService.All(db);
                //        apiCache.Atlas.Zips = ZipsService.All(db);
                //    }
                //    retval = apiCache.Atlas;
                //}
                return retval;
            }

        }


        public static AtlasDTO Contacts(int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new AtlasDTO();
                retval.Items = db.VwAtlas
                    .Join(db.CliGrals.Where(x => x.Emp == emp && x.Obsoleto == false), atlas => atlas.ContactGuid, contact => contact.Guid, (atlas, contact) => new AtlasDTO.Item
                    {
                        Country = new GuidNom(atlas.CountryGuid, atlas.CountryEsp),
                        Zona = new GuidNom(atlas.ZonaGuid, atlas.ZonaNom),
                        Location = new GuidNom(atlas.LocationGuid, atlas.LocationNom),
                        Contact = new GuidNom(atlas.ContactGuid, (atlas.RaoSocial + ' ' + atlas.NomCom).Trim())
                    }).ToList();
                return retval;
            }

        }
    }
}
