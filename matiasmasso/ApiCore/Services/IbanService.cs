using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class IbanService
    {
        public static IbanModel? Get(Entities.MaxiContext db, Guid titular, IbanModel.Cods cod)
        {
            var retval = db.Ibans 
            .AsNoTracking()
            .Where(x => x.ContactGuid == titular && x.Cod == (int)cod)
                .Select(x => new IbanModel(x.Guid)
                {
                    Titular = x.ContactGuid,
                    Cod = (IbanModel.Cods)x.Cod,
                    FchFrom = x.MandatoFch,
                    FchTo= x.CaducaFch,
                    Ccc = x.Ccc,
                    Branch = x.BankBranch
                }).FirstOrDefault();

                return retval;
        }


    }

    public class IbansService
    {
        public static List<IbanModel> GetValues()
        {
            using (var db = new MaxiContext())
            {
                return db.Ibans
                    .Select(x => new IbanModel(x.Guid)
                    {
                        FchFrom = x.MandatoFch,
                        FchTo = x.CaducaFch,
                        Cod = (IbanModel.Cods)x.Cod,
                        Titular = x.ContactGuid,
                        Ccc = x.Ccc,
                        Branch = x.BankBranch
                    }).ToList();
            }
        }
    }
}
