using DTO;
namespace Api.Services
{
    public class IbanService
    {
        public static IbanModel Get(Entities.MaxiContext db, Guid titular, IbanModel.Cods cod)
        {
            var retval = db.VwIbans
            .Where(x => x.IbanContactGuid == titular && x.IbanCod == (int)cod)
                .Select(x => new IbanModel(x.IbanGuid)
                {
                    Titular = x.IbanContactGuid,
                    Cod = x.IbanCod,
                    Ccc = x.IbanCcc,
                    BankBranch = x.BankBranchGuid == null ? null : new BankModel.Branch((Guid)x.BankBranchGuid)
                    {
                        Bank = x.BankGuid == null ? null : new BankModel((Guid)x.BankGuid)
                        {
                            NomComercial = x.BankAlias,
                            RaoSocial = x.BankNom
                        },
                        Location = x.BankBranchLocationGuid == null ? null : new LocationDTO((Guid)x.BankBranchLocationGuid)
                        {
                            Zona = x.BankBranchZonaGuid == null ? null : new ZonaDTO((Guid)x.BankBranchZonaGuid)
                            {
                                Country = x.BankBranchCountryGuid == null ? null : new CountryDTO((Guid)x.BankBranchCountryGuid)
                                {
                                    Iso = x.BankBranchCountryIso
                                },
                                Nom = x.BankBranchZonaNom
                            },
                            Nom = x.BankBranchLocationNom
                        },
                        Adr = x.BankBranchAdr
                    }
                }).FirstOrDefault();

                return retval;
        }


    }
}
