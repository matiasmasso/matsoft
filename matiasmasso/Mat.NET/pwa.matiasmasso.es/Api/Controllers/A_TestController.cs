using Api.Entities;
using Api.Services;
using Api.Shared;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using static DTO.MgzInventoryDTO;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class A_TestController : ControllerBase
    {


        [HttpGet()]
        public IActionResult Get()
        {
            var fch = DateTime.Today.AddDays(-1).Date;
            DateTime fchStart = fch; 
            DateTime fchEnd = fch.AddDays(1);   

            using (var db = new Entities.MaxiContext())
            {
                var values = db.VwSellouts
                    .Join(db.Pdcs, sell => sell.PdcGuid, pdc => pdc.Guid, (sell, pdc) => new { sell, pdc })
                    .Join(db.VwAddresses, res => res.sell.CustomerGuid, adr => adr.SrcGuid, (res, adr) => new { res, adr })
                  .Where(x => x.res.sell.Emp == 1 && x.res.sell.Cod == 2 && x.res.sell.FchCreated! > fchStart && x.res.sell.FchCreated <= fchEnd)
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
                        Eur = (decimal)x.Sum(y => y.res.sell.Qty * (y.res.sell.Eur ?? 0))
                    })
                    .OrderByDescending(x => x.FchCreated)
                    .ToList();

                IActionResult retval = Ok(values);
                return retval;
            }
        }

    //    private List<SelloutDTO.Order> Orders(Entities.MaxiContext db, IQueryable <AbandonedMutexException>)
    //    {
    //        return query.AsQueryable()
    //.Join(db.Pdcs, sell => sell.PdcGuid, pdc => pdc.Guid, (sell, pdc) => new { sell, pdc })
    //.Join(db.VwAddresses, res => res.sell.CustomerGuid, adr => adr.SrcGuid, (res, adr) => new { res, adr })
    //.OrderByDescending(y => y.res.sell.FchCreated)
    //.Select(x => new SelloutDTO.Order()
    //{
    //    Guid = x.res.sell.PdcGuid,
    //    Concept = x.res.pdc.Pdd,
    //    FchCreated = (DateTime)x.res.sell.FchCreated!,
    //    CustomerName = x.res.sell.CustomerName,
    //    Location = AddressDTO.LocationProvinceCountry(x.adr.LocationNom, x.adr.ProvinciaNom, x.adr.CountryIso),
    //    Eur = x.res.sell.Eur
    //}).ToList();
    //    }

        private static string CustomerName(string raoSocial, string? nomComercial, string? reference)
        {
            var retval = string.IsNullOrEmpty(nomComercial) ? raoSocial : nomComercial;
            if (!string.IsNullOrEmpty(reference)) retval = string.Format("{0} [{1}]", retval, reference);
            return retval;
        }


    }

}

