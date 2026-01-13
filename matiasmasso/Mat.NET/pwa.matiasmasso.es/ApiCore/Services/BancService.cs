using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class BancService
    {
        public static Byte[]? Logo(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwIbans
                    .AsNoTracking()
                    .Where(x => x.IbanContactGuid == guid)
                    .Join(db.Bn1s, iban => iban.BankGuid, bank => bank.Guid, (iban, bank) => new { Logo = bank.Logo48 })
                    .Select(x => x.Logo)
                    .FirstOrDefault();

                return retval;
            }
        }

    }
    public class BancsService
    {
        public static List<BancModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwBancs.Select(x => new BancModel(x.Guid){
                    Emp = (EmpModel.EmpIds)x.Emp,
                    Abr = x.Abr,
                    Ccc = x.Ccc, 
                    Obsoleto = x.Obsoleto
                }).ToList();
                return retval;
            }
        }

        public static List<BancModel.Saldo> Saldos(UserModel? user = null)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwBancsSdos
                    .AsNoTracking()
                    .Select(x => new BancModel.Saldo
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Guid = x.Banc,
                        Abr = x.Abr,
                        Ccc = x.IbanCcc,
                        Eur = x.Sdo,
                        Fch = x.Fch
                    }).ToList();

                if (user != null)
                    retval = retval.Where(x => user.Emps.Any(y => x.Emp == y)).ToList();

                return retval;
            }
        }

    }
}
