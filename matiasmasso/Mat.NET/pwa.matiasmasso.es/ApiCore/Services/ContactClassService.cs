using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ContactClassesService
    {
        public static List<ContactClassModel> GetValues()
        {
            using(var db = new Entities.MaxiContext())
            {
                var retval = db.ContactClasses
                    .AsNoTracking()
                    .Select(x => new ContactClassModel(x.Guid)
                    {
                        Nom = new LangTextModel(x.Esp, x.Cat, x.Eng, x.Por),
                        Channel = x.DistributionChannel ?? DistributionChannelModel.Default().Guid
                    })
                    .ToList();
                retval = retval.OrderBy(x => x.Nom?.Esp).ToList();
                return retval;
            }
        }
    }
}
