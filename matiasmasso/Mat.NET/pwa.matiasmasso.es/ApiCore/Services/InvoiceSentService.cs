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
                        .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Include(x => x.Cli)
                    .Select(x => new InvoiceSentModel
                    {
                        Guid = x.Guid,
                        Id = x.Fra1,
                        Serie = (InvoiceSentModel.Series)x.Serie,
                        Fch = x.Fch,
                        Amt = new Amt(x.EurBase, Amt.Curs.EUR, x.EurBase),
                        Contact = x.CliGuid,
                        Fpg = x.Fpg,
                        TipoFactura = x.TipoFactura,
                        SiiL9 = x.SiiL9,
                        SiiResult = x.SiiResult,
                        RegimenEspecialOTrascendencia = x.RegimenEspecialOtrascendencia,
                        Concepte = (int)x.Concepte,
                        PrintMode = x.PrintMode,
                    }).FirstOrDefault();
                if(retval != null)
                {
                    var deliveryPattern = lang.Tradueix("Albarán {0:N0} del {1:dd/MM/yy}", "Albarà {0:N0} del {1:dd/MM/yy}", "Delivery note {0:N0} from {1:dd/MM/yy}");
                    var poPattern = lang.Tradueix("Su pedido {0} del {1:dd/MM/yy}", "La seva comanda {0} del {1:dd/MM/yy}", "Your purchase order {0} from {1:dd/MM/yy}");
                    retval.Items = db.Arcs
                        .AsNoTracking()
                        .Where(x => x.Alb.FraGuid == guid)
                        .OrderBy(x => x.Alb.Alb1)
                        .ThenBy(x => x.Lin)
                        .Select(x => new InvoiceSentModel.Item(x.Guid)
                        {
                            Delivery = new GuidNom(x.AlbGuid, string.Format(deliveryPattern, x.Alb.Alb1, x.Alb.Fch)),
                            PurchaseOrder = x.PdcGuid == null ? null : new GuidNom((Guid)x.PdcGuid, x.Pdc == null ? null : string.Format(poPattern, x.Pdc.Pdd, x.Pdc.Fch)),
                            Qty = x.Qty,
                            Sku = x.ArtGuid,
                            Price = x.Eur,
                            Dto = x.Dto
                        }).ToList();
                }
                return retval;
            }
        }
    }
    public class InvoicesSentService
    {

        public static InvoiceSentListDTO FromUser(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new InvoiceSentListDTO();
                if (user.IsCustomer())
                {
                    retval.Items = db.Fras
                        .AsNoTracking()
                        .Join(db.EmailClis, a => a.CliGuid, e => e.ContactGuid, (a, e) => new { a, e })
                        .Where(x => x.e.EmailGuid == user.Guid)
                        .OrderByDescending(x => x.a.Fra1)
                        .Select(x => new InvoiceSentListDTO.Item
                        {
                            Guid = x.a.Guid,
                            Contact = x.a.Cli == null ? null : new GuidNom(x.a.Cli.Guid, x.a.Cli.FullNom),
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

        public static List<InvoiceSentModel> GetValues(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Fras
                        .AsNoTracking()
                    .Where(x => x.Emp == emp && x.Yea == year)
                    .Include(x => x.Cli)
                    .OrderByDescending(x => x.Fra1)
                    .Select(x => new InvoiceSentModel
                    {
                        Guid = x.Guid,
                        Id = x.Fra1,
                        Serie = (InvoiceSentModel.Series)x.Serie,
                        Fch = x.Fch,
                        Amt = new Amt(x.EurBase, Amt.Curs.EUR, x.EurBase),
                        Contact = x.CliGuid,
                        Fpg = x.Fpg,
                        TipoFactura = x.TipoFactura,
                        SiiL9 = x.SiiL9,
                        SiiResult = x.SiiResult,
                        RegimenEspecialOTrascendencia = x.RegimenEspecialOtrascendencia,
                        Concepte = (int)x.Concepte,
                        PrintMode = x.PrintMode
                    }).ToList();
            }

        }
    }
}
