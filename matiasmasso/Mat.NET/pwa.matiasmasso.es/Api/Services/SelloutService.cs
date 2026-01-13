using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Api.Services
{
    public class SelloutService
    {
        public static SelloutDTO Load(SelloutDTO.Request request, UserModel user)
        {
            var retval = new SelloutDTO();
            using (var db = new Entities.MaxiContext())
            {
                retval.Items = Items(db, request, user).OrderByDescending(x => x.Year).ThenByDescending(y => y.Month).ThenByDescending(z => z.Day).ToList();
                var firstItem = retval.Items.First();
                request.Fch = new DateTime(firstItem.Year, firstItem.Month, firstItem.Day);
                retval.Orders = Orders(db, request, user);
                return retval;
            }
        }

        public static List<SelloutDTO.Item> Items(Entities.MaxiContext db, SelloutDTO.Request value, UserModel user)
        {
            List<SelloutDTO.Item> retval = new();
            if (user.isRep())
            {
                retval = db.VwSelloutReps.
                   Where(x => x.Cod == 2 && x.EmailGuid == user.Guid).
                   GroupBy(y => new { y.FchCreated!.Value.Year, y.FchCreated!.Value.Month, y.FchCreated!.Value.Day }).
                   Select(z => new SelloutDTO.Item
                   {
                       Year = z.Key.Year,
                       Month = z.Key.Month,
                       Day = z.Key.Day,
                       Pdcs = z.Select(x => x.PdcGuid).Distinct().Count(),
                       Eur = z.Sum(s => (decimal?)s.Eur ?? 0)
                   }).
               ToList();
            }
            else if (user.isCustomer())
            {
                retval = db.VwSelloutClis.
                   Where(x => x.Cod == 2 && x.EmailGuid == user.Guid).
                   GroupBy(y => new { y.FchCreated!.Value.Year, y.FchCreated!.Value.Month, y.FchCreated!.Value.Day }).
                   Select(z => new SelloutDTO.Item
                   {
                       Year = z.Key.Year,
                       Month = z.Key.Month,
                       Day = z.Key.Day,
                       Pdcs = z.Select(x => x.PdcGuid).Distinct().Count(),
                       Eur = z.Sum(s => (decimal?)s.Eur ?? 0)
                   }).
               ToList();
            }
            else if (user.isManufacturer())
            {
                retval = db.VwSelloutProveidors.
                   Where(x => x.Cod == 2 && x.EmailGuid == user.Guid).
                   GroupBy(y => new { y.FchCreated!.Value.Year, y.FchCreated!.Value.Month, y.FchCreated!.Value.Day }).
                   Select(z => new SelloutDTO.Item
                   {
                       Year = z.Key.Year,
                       Month = z.Key.Month,
                       Day = z.Key.Day,
                       Pdcs = z.Select(x => x.PdcGuid).Distinct().Count(),
                       Eur = z.Sum(s => (decimal?)s.Eur ?? 0)
                   }).
               ToList();
            }
            else if (user.isMainboardOrAccounts())
            {
                retval = db.VwSellouts.
                   Where(x => x.Emp == value.EmpId && x.Cod == 2).
                   GroupBy(y => new { y.FchCreated!.Value.Year, y.FchCreated!.Value.Month, y.FchCreated!.Value.Day }).
                   Select(z => new SelloutDTO.Item
                   {
                       Year = z.Key.Year,
                       Month = z.Key.Month,
                       Day = z.Key.Day,
                       Pdcs = z.Select(x => x.PdcGuid).Distinct().Count(),
                       Eur = z.Sum(s => (decimal?)s.Eur ?? 0)
                   }).
               ToList();
            }


            return retval;
        }

        public static List<SelloutDTO.Order> Orders(SelloutDTO.Request value, UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Orders(db, value, user);
            }
        }

        public static List<SelloutDTO.Order> Orders(Entities.MaxiContext db, SelloutDTO.Request value, UserModel user)
        {
            var fchFrom = (value.Fch ?? DateTime.Today).Date;
            var fchTo = fchFrom.AddDays(1);

            var retval = new List<SelloutDTO.Order>();
            if (user.isRep())
            {
                //retval = db.VwSelloutReps
                //  .Where(x => x.EmailGuid == user.Guid && x.Cod == 2 && x.FchCreated >= fchFrom && x.FchCreated < fchTo)

                retval = db.VwSelloutReps
                  .Join(db.Pdcs, sell => sell.PdcGuid, pdc => pdc.Guid, (sell, pdc) => new { sell, pdc })
                  .Join(db.VwAddresses, res => res.sell.CustomerGuid, adr => adr.SrcGuid, (res, adr) => new { res, adr })
                  .Where(x => x.res.sell.EmailGuid == user.Guid && x.res.sell.Cod == 2 && x.res.sell.FchCreated! > fchFrom && x.res.sell.FchCreated <= fchTo)
                  .GroupBy(x => new
                  {
                      x.res.sell.FchCreated,
                      x.res.sell.PdcGuid,
                      x.res.sell.CustomerGuid,
                      x.res.sell.CustomerNom,
                      x.res.sell.CustomerNomCom,
                      x.res.sell.ClientRef,
                      x.res.pdc.Pdd,
                      x.adr.LocationNom,
                      x.adr.ProvinciaNom,
                      x.adr.CountryIso
                  })
                  .Select(x => new SelloutDTO.Order()
                  {
                      Guid = x.Key.PdcGuid,
                      Concept = x.Key.Pdd,
                      FchCreated = (DateTime)x.Key.FchCreated!,
                      CustomerName = CustomerName(x.Key.CustomerNom, x.Key.CustomerNomCom, x.Key.ClientRef),
                      Location = AddressDTO.LocationProvinceCountry(x.Key.LocationNom, x.Key.ProvinciaNom, x.Key.CountryIso),
                      Eur = (decimal)x.Sum(y => y.res.sell.Eur ?? 0)
                  })
                  .OrderByDescending(x => x.FchCreated)
                  .ToList();


            }
            else if (user.isCustomer())
            {
                retval = db.VwSelloutClis
                  .Join(db.Pdcs, sell => sell.PdcGuid, pdc => pdc.Guid, (sell, pdc) => new { sell, pdc })
                  .Join(db.VwAddresses, res => res.sell.CustomerGuid, adr => adr.SrcGuid, (res, adr) => new { res, adr })
                  .Where(x => x.res.sell.EmailGuid == user.Guid && x.res.sell.Cod == 2 && x.res.sell.FchCreated! > fchFrom && x.res.sell.FchCreated <= fchTo)
                  .GroupBy(x => new
                  {
                      x.res.sell.FchCreated,
                      x.res.sell.PdcGuid,
                      x.res.sell.CustomerGuid,
                      x.res.sell.CustomerNom,
                      x.res.sell.CustomerNomCom,
                      x.res.sell.ClientRef,
                      x.res.pdc.Pdd,
                      x.adr.LocationNom,
                      x.adr.ProvinciaNom,
                      x.adr.CountryIso
                  })
                  .Select(x => new SelloutDTO.Order()
                  {
                      Guid = x.Key.PdcGuid,
                      Concept = x.Key.Pdd,
                      FchCreated = (DateTime)x.Key.FchCreated!,
                      CustomerName = CustomerName(x.Key.CustomerNom, x.Key.CustomerNomCom, x.Key.ClientRef),
                      Location = AddressDTO.LocationProvinceCountry(x.Key.LocationNom, x.Key.ProvinciaNom, x.Key.CountryIso),
                      Eur = (decimal)x.Sum(y => y.res.sell.Eur ?? 0)
                  })
                  .OrderByDescending(x => x.FchCreated)
                  .ToList();
            }
            else if (user.isManufacturer())
            {
                retval = db.VwSelloutProveidors
                  .Join(db.Pdcs, sell => sell.PdcGuid, pdc => pdc.Guid, (sell, pdc) => new { sell, pdc })
                  .Join(db.VwAddresses, res => res.sell.CustomerGuid, adr => adr.SrcGuid, (res, adr) => new { res, adr })
                  .Where(x => x.res.sell.EmailGuid == user.Guid && x.res.sell.Cod == 2 && x.res.sell.FchCreated! > fchFrom && x.res.sell.FchCreated <= fchTo)
                  .GroupBy(x => new
                  {
                      x.res.sell.FchCreated,
                      x.res.sell.PdcGuid,
                      x.res.sell.CustomerGuid,
                      x.res.sell.CustomerNom,
                      x.res.sell.CustomerNomCom,
                      x.res.sell.ClientRef,
                      x.res.pdc.Pdd,
                      x.adr.LocationNom,
                      x.adr.ProvinciaNom,
                      x.adr.CountryIso
                  })
                  .Select(x => new SelloutDTO.Order()
                  {
                      Guid = x.Key.PdcGuid,
                      Concept = x.Key.Pdd,
                      FchCreated = (DateTime)x.Key.FchCreated!,
                      CustomerName = CustomerName(x.Key.CustomerNom, x.Key.CustomerNomCom, x.Key.ClientRef),
                      Location = AddressDTO.LocationProvinceCountry(x.Key.LocationNom, x.Key.ProvinciaNom, x.Key.CountryIso),
                      Eur = (decimal)x.Sum(y => y.res.sell.Eur ?? 0)
                  })
                  .OrderByDescending(x => x.FchCreated)
                  .ToList();
            }
            else if (user.isMainboardOrAccounts())
            {
                retval = db.VwSellouts
                  .Join(db.Pdcs, sell => sell.PdcGuid, pdc => pdc.Guid, (sell, pdc) => new { sell, pdc })
                  .Join(db.VwAddresses, res => res.sell.CustomerGuid, adr => adr.SrcGuid, (res, adr) => new { res, adr })
                  .Where(x => x.res.sell.Emp == 1 && x.res.sell.Cod == 2 && x.res.sell.FchCreated! > fchFrom && x.res.sell.FchCreated <= fchTo)
                  .GroupBy(x => new
                  {
                      x.res.sell.FchCreated,
                      x.res.sell.PdcGuid,
                      x.res.sell.CustomerGuid,
                      x.res.sell.CustomerNom,
                      x.res.sell.CustomerNomCom,
                      x.res.sell.ClientRef,
                      x.res.pdc.Pdd,
                      x.adr.LocationNom,
                      x.adr.ProvinciaNom,
                      x.adr.CountryIso
                  })
                  .Select(x => new SelloutDTO.Order()
                  {
                      Guid = x.Key.PdcGuid,
                      Concept = x.Key.Pdd,
                      FchCreated = (DateTime)x.Key.FchCreated!,
                      CustomerName = CustomerName(x.Key.CustomerNom, x.Key.CustomerNomCom, x.Key.ClientRef),
                      Location = AddressDTO.LocationProvinceCountry(x.Key.LocationNom, x.Key.ProvinciaNom, x.Key.CountryIso),
                      Eur = (decimal)x.Sum(y => y.res.sell.Eur ?? 0)
                  })
                  .OrderByDescending(x => x.FchCreated)
                  .ToList();
            }
            return retval;
        }

        private static string CustomerName(string raoSocial, string? nomComercial, string? reference)
        {
            var retval = string.IsNullOrEmpty(nomComercial) ? raoSocial : nomComercial;
            if (!string.IsNullOrEmpty(reference)) retval = string.Format("{0} [{1}]", retval, reference);
            return retval;
        }
    }
}
