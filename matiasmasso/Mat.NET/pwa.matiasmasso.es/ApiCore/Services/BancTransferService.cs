using Api.Entities;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class BancTransferService
    {

    }
    public class BancTransfersService
    {
        public static List<BancModel.Transfer> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.BancTransferPools
                    .Include(x => x.BancTransferBeneficiaris)
                    .Include(x => x.CcaNavigation)
                    .AsNoTracking()
                    .Select(x => new BancModel.Transfer(x.Guid)
                    {
                        Banc = x.BancEmissor,
                        Cca = new CcaModel(x.CcaNavigation.Guid)
                        {
                            Emp = (EmpModel.EmpIds)x.CcaNavigation.Emp,
                            Id = x.CcaNavigation.Cca1,
                            Fch = x.CcaNavigation.Fch,
                            Concept = x.CcaNavigation.Txt
                        },
                        Items = x.BancTransferBeneficiaris.Select(x => new BancModel.Transfer.Item(x.Guid)
                        {
                            Beneficiari = x.Contact,
                            Amount = new Amt(x.Val, x.Cur, x.Eur),
                            BankBranch = x.BankBranch,
                            Ccc = x.Account,
                            Concept = x.Concepte
                        }).ToList()

                    }).ToList();
            }
        }

        public static void Update(List<BancModel.Transfer> transfers)
        {
            using (var db = new MaxiContext())
            {
                foreach (var value in transfers)
                {
                    Update(db, value);
                }
                db.SaveChanges();
            }
        }

        public static void Update(MaxiContext db, BancModel.Transfer value)
        {
            CcaService.Update(db, value.Cca);

            db.BancTransferPools.Add(
                new Entities.BancTransferPool
                {
                    Guid = value.Guid,
                    BancEmissor = (Guid)value.Banc!,
                    Cca = value.Cca.Guid,
                    BancTransferBeneficiaris = value.Items
                        .Select(x => new Entities.BancTransferBeneficiari
                        {
                            Guid = x.Guid,
                            Contact = x.Beneficiari,
                            BankBranch = (Guid)x.BankBranch!,
                            Account = x.Ccc ?? "",
                            Concepte = x.Concept,
                            Val = x.Amount?.Value ?? 0,
                            Eur = x.Amount?.Eur ?? 0,
                            Cur = x.Amount?.Cur.ToString() ?? "EUR"
                        }).ToList()
                });

            // Save changes in database
            db.SaveChanges();
        }

    }
}
