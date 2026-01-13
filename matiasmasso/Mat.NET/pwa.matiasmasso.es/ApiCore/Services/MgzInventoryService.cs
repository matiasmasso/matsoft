using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class MgzInventoryService
    {
        public static List<MgzInventoryModel> GetValues(MgzModel mgz, DateTime fch)
        {
            var retval = new List<MgzInventoryModel>();
            var item = new MgzInventoryModel();
            using (var db = new Entities.MaxiContext())
            {
                var entities = db.Arcs
                    .Include(x => x.Alb)
                    .Include(x => x.Art)
                    .Include("Art.CategoryNavigation")
                        .AsNoTracking()
                    .Where(x => x.MgzGuid == mgz.Guid && x.Alb.Fch.Date <= fch.Date && !x.Art.IsBundle && !x.Art.NoStk)
                    .OrderBy(x => x.ArtGuid)
                    .ThenBy(x => x.Alb.Fch.Date)
                    .ThenBy(x => x.Alb.Alb1)
                    .Select(x => new
                    {
                        Sku = x.ArtGuid,
                        Category = x.Art.Category,
                        Brand = x.Art.CategoryNavigation.Brand,
                        Qty = x.Qty,
                        Cod = x.Cod,
                        Eur = Math.Round((x.Eur ?? 0) * (100 - x.Dto) / 100, 2),
                        Fch = x.Alb.Fch
                    }).ToList();


                int stk = 0;
                decimal inventory = 0;
                decimal pmc = 0;

                foreach (var x in entities)
                {
                    if (item.Sku != x.Sku)
                    {
                        stk = 0;
                        inventory = 0;
                        pmc = 0;

                        item = new MgzInventoryModel();
                        retval.Add(item);
                        item.Sku = x.Sku;
                        item.Category = x.Category;
                        item.Brand = x.Brand;
                    }

                    if (x.Cod < 50)
                    {
                        if (x.Cod == 11 || x.Cod == 12)
                        {
                            item.LastIn = x.Fch;
                            if (stk + x.Qty != 0)
                                pmc = (inventory + x.Qty * x.Eur) / (stk + x.Qty);
                        }
                        stk += x.Qty;
                    }
                    else
                        stk -= x.Qty;

                    item.Stk = stk;
                    item.Eur = pmc;
                    inventory = stk * pmc;
                }
            }
            return retval;
        }

        public static MgzInventoryExtracteModel Extracte(MgzModel mgz, ProductSkuModel sku, DateTime fch)
        {
            var retval = new MgzInventoryExtracteModel() { Sku = sku.Guid };
            using (var db = new Entities.MaxiContext())
            {
                retval.Items = db.Arcs
                    .Include(x => x.Alb)
                        .AsNoTracking()
                        .Where(x => x.MgzGuid == mgz.Guid && x.ArtGuid == sku.Guid && x.Alb.Fch.Date <= fch.Date)
                        .OrderBy(x => x.Alb.Fch.Date)
                        .ThenBy(x => x.Alb.Alb1)
                        .Select(x => new MgzInventoryExtracteModel.Item
                        {
                            AlbGuid = x.AlbGuid,
                            Alb = x.Alb.Alb1,
                            Fch = x.Alb.Fch,
                            Nom = x.Alb.Nom,
                            Cod = x.Cod,
                            Qty = x.Qty,
                            Eur = x.Eur ?? 0,
                            Dto = x.Dto
                        }).ToList();

                int stock = 0;
                decimal inventory = 0;
                decimal pmc = 0;
                foreach (var item in retval.Items)
                {
                    if (item.Cod < 50)
                    {
                        if (item.Cod == 11 || item.Cod == 12)
                            pmc = (inventory + item.Amount()) / (stock + item.Qty);
                        stock += item.Qty;
                    }
                    else
                        stock -= item.Qty;

                    item.Stock = stock;
                    item.Pmc = pmc;
                    inventory = stock * pmc;
                }
            }
            return retval;
        }
    }
}
