using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class InvoiceSentService
    {
        public static InvoiceSentModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var lang = LangDTO.Esp();
                var retval =  db.Fras
                    .Where(x => x.Guid == guid)
                    .Include(x => x.CliGu)
                    .Select(x => new InvoiceSentModel
                    {
                        Guid = x.Guid,
                        Id = x.Fra1,
                        Serie = x.Serie,
                        Fch = x.Fch,
                        Amt = new Amt(x.EurBase, Amt.Curs.EUR, x.EurBase),
                        Contact = new GuidNom(x.CliGuid, x.CliGu.RaoSocial),
                        Fpg = x.Fpg,
                        TipoFactura = x.TipoFactura,
                        SiiL9 = x.SiiL9 ?? "",
                        SiiResult = x.SiiResult,
                        RegimenEspecialOTrascendencia = x.RegimenEspecialOtrascendencia ?? "",
                        Concepte = (int)x.Concepte,
                        PrintMode = x.PrintMode,
                    }).FirstOrDefault();
                if(retval != null)
                {
                    var deliveryPattern = lang.Tradueix("Albarán {0:N0} del {1:dd/MM/yy}", "Albarà {0:N0} del {1:dd/MM/yy}", "Delivery note {0:N0} from {1:dd/MM/yy}");
                    var poPattern = lang.Tradueix("Su pedido {0} del {1:dd/MM/yy}", "La seva comanda {0} del {1:dd/MM/yy}", "Your purchase order {0} from {1:dd/MM/yy}");
                    retval.Items = db.Arcs
                        .Where(x => x.AlbGu.FraGuid == guid)
                        .OrderBy(x => x.AlbGu.Alb1)
                        .ThenBy(x => x.Lin)
                        .Select(x => new InvoiceSentModel.Item
                        {
                            Delivery = new GuidNom(x.AlbGuid, string.Format(deliveryPattern, x.AlbGu.Alb1, x.AlbGu.Fch)),
                            PurchaseOrder = x.PdcGuid == null ? null : new GuidNom((Guid)x.PdcGuid, string.Format(poPattern, x.PdcGu.Pdd, x.PdcGu.Fch)),
                            Qty = x.Qty,
                            Sku = x.ArtGuid,
                            Price = x.Eur,
                            Dto = x.Dto == null ? null : (decimal)x.Dto
                        }).ToList();
                }
                return retval;
            }
        }
    }
    public class InvoiceSentListService
    {

        public static InvoiceSentListDTO FromUser(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new InvoiceSentListDTO();
                if (user.isCustomer())
                {
                    retval.Items = db.Fras
                        .Join(db.EmailClis, a => a.CliGuid, e => e.ContactGuid, (a, e) => new { a, e })
                        .Where(x => x.e.EmailGuid == user.Guid)
                        .OrderByDescending(x => x.a.Fra1)
                        .Select(x => new InvoiceSentListDTO.Item
                        {
                            Guid = x.a.Guid,
                            Contact = x.a.CliGu == null ? null : new GuidNom(x.a.CliGu.Guid, x.a.CliGu.FullNom),
                            Id = x.a.Fra1,
                            Serie = x.a.Serie,
                            Fch = x.a.Fch,
                            Amt = new Amt(x.a.EurLiq, Amt.Curs.EUR, x.a.EurLiq),
                        })
                        .ToList();
                }
                return retval;
            }
        }

        public static InvoiceSentListDTO FromCustomer(Guid contactGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new InvoiceSentListDTO();
                retval.Items = (from x in db.VwInvoicesSents
                                where x.CliGuid.Equals(contactGuid)
                                orderby x.Fch descending
                                select new InvoiceSentListDTO.Item
                                {
                                    Guid = x.Guid,
                                    Id = x.Fra,
                                    Serie = x.Serie,
                                    Fch = x.Fch,
                                    Amt = new Amt(x.EurLiq, Amt.Curs.EUR, x.EurLiq)
                                }).ToList();
                return retval;
            }
        }

        public static InvoiceSentListDTO FromEmpYear(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new InvoiceSentListDTO();
                retval.Years = db.Fras
                    .Where(x => x.Emp == emp)
                    .GroupBy(x => x.Yea)
                    .Select(x => new IdEur
                    {
                        Id = x.Key,
                        Eur = x.Sum(y => y.EurBase)
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();

                if (year == 0 && retval.Years.Count>0) year = retval.Years.First().Id;

                retval.Items = db.Fras
                    .Where(x => x.Emp == emp && x.Yea == year)
                    .Include(x => x.CliGu)
                    .OrderByDescending(x => x.Fra1)
                    .Select(x => new InvoiceSentListDTO.Item
                    {
                        Guid = x.Guid,
                        Id = x.Fra1,
                        Serie = x.Serie,
                        Fch = x.Fch,
                        Amt = new Amt(x.EurBase, Amt.Curs.EUR, x.EurBase),
                        Contact = new GuidNom(x.CliGuid, x.CliGu.RaoSocial),
                        Fpg = x.Fpg,
                        TipoFactura = x.TipoFactura,
                        SiiL9 = x.SiiL9 ?? "",
                        SiiResult = x.SiiResult,
                        RegimenEspecialOTrascendencia = x.RegimenEspecialOtrascendencia ?? ""  ,
                        Concepte = (int)x.Concepte,
                        PrintMode = x.PrintMode
                    }).ToList();
                return retval;
            }
        }

    }
}
