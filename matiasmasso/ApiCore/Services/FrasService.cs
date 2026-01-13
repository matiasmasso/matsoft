using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class FraService
    {

        public static void Update(MaxiContext db, FraModel value)
        {
            Entities.Fra? entity;
            var fch = value.Fch!;
            if (value.IsNew)
            {
                entity = new Entities.Fra();
                entity.Guid = value.Guid;
                entity.FchCreated = DateTime.Now;
                db.Fras.Add(entity);
            }
            else
            {
                entity = db.Fras
                    .FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new System.Exception("Fra not found");
            }

            entity.Emp = (int)value.Emp!;
            entity.Yea = (short)value.Fch.Year;
            entity.Serie = 0;
            entity.Fra1 = value.FraNum;
            entity.Fch = value.Fch.ToDateTime(TimeOnly.MinValue);
            entity.Vto = value.Vto.ToDateTime(TimeOnly.MinValue);
            entity.Nom = value.Nom;
            entity.Nif = value.Nif;
            entity.NifCod = 1;
            entity.Adr = value.Address;
                        entity.Zip = value.ZipGuid;
            entity.SumItems = (decimal)value.BaseImponible!;
            entity.Cur = "EUR";
            entity.EurBase = entity.SumItems;
            entity.IvaStdBase = entity.SumItems;
            entity.IvaStdPct = value.IVApct ?? 0;
            entity.IvaStdAmt = value.IVA;
            entity.EurLiq = value.Total;
            if(value.IRPFpct != null && value.IRPFpct != 0)
            {
                entity.IrpfPct = value.IRPFpct;
                entity.IrpfBase = entity.SumItems;
                entity.IrpfAmt = value.IRPF;
            }
            entity.CliGuid = value.CliGuid;
            entity.CcaGuid = value.CcaGuid;
            entity.Lang = value.Lang?.Tag() ?? "ESP";

            entity.FchLastEdited = entity.FchCreated;
        }
    }
    public class FrasService
    {
        public static List<CustomerInvoiceModel.LastFraNum> LastFraNums()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Fras
                    .GroupBy(i => new { i.Emp, i.Serie })
                    .Select(g => g.OrderByDescending(i => i.Fra1)
                        .Select(i => new CustomerInvoiceModel.LastFraNum()
                        {
                            Emp = (EmpModel.EmpIds)i.Emp,
                            FraNum = i.Fra1,
                            Fch = DateOnly.FromDateTime(i.Fch),
                            Serial = i.Serie.ToString()
                        }).FirstOrDefault()!)
                    .ToList();
            }
        }

        public static List<FraModel> GetValues(EmpModel.EmpIds emp)
        {
            using (var db = new Entities.MaxiContext())
            {

                return db.Fras
                    .Include(x => x.Cca)
                     .Where(x => x.Emp == (int)emp)
                     .OrderByDescending(x => x.Fra1)
                     .Select(x => new FraModel(x.Guid)
                     {
                         Emp = emp,
                         FraNum = x.Fra1,
                         Fch = DateOnly.FromDateTime(x.Fch),
                         CliGuid = x.CliGuid,
                         BaseImponible = x.SumItems,
                         IVApct = x.IvaStdPct,
                         IRPFpct = x.IrpfPct,
                         Cca = x.Cca == null ? null : new CcaModel(x.Cca.Guid)
                         {
                             Docfile = x.Cca.Hash == null ? null :  new DocfileModel(x.Cca.Hash)
                         }
                     }).ToList();
            }
        }
    }
}
