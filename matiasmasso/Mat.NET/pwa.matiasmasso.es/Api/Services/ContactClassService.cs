using DTO;
namespace Api.Services
{
    public class ContactClassesService
    {
        public static List<GuidNom> All(LangDTO lang)
        {
            using(var db = new Entities.MaxiContext())
            {
                var retval = db.ContactClasses
                    .Select(x => new GuidNom(x.Guid, lang.Tradueix(x.Esp, x.Cat, x.Eng, x.Por) ))
                    .ToList();
                retval = retval.OrderBy(x => x.Nom).ToList();
                return retval;
            }
        }
    }
}
