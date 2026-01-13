using DTO;
namespace Api.Services
{
    public class ContractService
    {
        public static Byte[]? Stream(Guid guid)
        {
            Byte[]? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                retval = (from item in db.Contracts
                          join docfiles in db.DocFiles
                          on item.Hash equals docfiles.Hash
                          where item.Guid.Equals(guid)
                          select docfiles.Doc).FirstOrDefault();
            }
            return retval;
        }

    }

    public class ContractsService
    {
        public static List<ContractDTO> Fetch()
        {
            using(var db = new Entities.MaxiContext())
            {
                var retval =  (from contract in db.Contracts
                                    join codi in db.ContractCodis on contract.CodiGuid equals codi.Guid
                                    join contact in db.CliGrals on contract.ContactGuid equals contact.Guid
                                    orderby codi.Nom, contract.FchFrom descending
                                    select new ContractDTO()
                                    {
                                        Guid = contract.Guid,
                                        Nom = contract.Nom,
                                        Num = contract.Num,
                                        FchFrom = contract.FchFrom,
                                        FchTo = contract.FchTo,
                                        Codi = contract.CodiGuid == null ? null : new ContractDTO.CodiClass() {
                                            Guid = codi.Guid,
                                            Nom = codi.Nom,
                                        },
                                        Contact = contract.ContactGuid == null ? null : new ContractDTO.ContactClass()
                                        {
                                            Guid = contact.Guid,
                                            FullNom = contact.FullNom,
                                        },
                                        HasPdf = contract.Hash != null
                                    }).ToList();

                return retval;
            }
        }

        public static List<GuidNom> FromCodi(Guid guid)
        {

            using (var db = new Entities.MaxiContext())
            {
                return db.Contracts
                    .Where(x => x.CodiGuid == guid)
                    .Select(x => new GuidNom
                    {
                        Guid = x.Guid,
                        Nom =( x.ContactGu == null?"":string.IsNullOrEmpty(x.ContactGu.NomCom)?x.ContactGu.RaoSocial: x.ContactGu.NomCom ?? "") + " " + x.Num
                    })
                    .ToList();
            }
        }

    }
}
