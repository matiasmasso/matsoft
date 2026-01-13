using Api.Entities;
using Api.Helpers;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class TransmissionService
    {

        public static int Update(TransmissionModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                UpdateHeader(db, value);
                UpdateDeliveries(db, value);
                db.SaveChanges();
                return value.Id;
            }
        }
        public static void UpdateDeliveries(Entities.MaxiContext db, TransmissionModel value)
        {
            foreach(var delivery in value.Deliveries)
            {
                var entity = db.Albs.FirstOrDefault(x=>x.Guid ==  delivery.Guid);
                if(entity != null)  entity.TransmGuid = value.Guid;
            }
        }
        public static void UpdateHeader(Entities.MaxiContext db, TransmissionModel value)
        {
                Entities.Transm? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Transm();
                    SetNextId(db, value, ref entity);
                    db.Transms.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Transms.Find(value.Guid);

                if (entity == null) throw new System.Exception("Transmission not found");

                entity.Fch = value.Fch;
                entity.MgzGuid = value.Mgz;
                entity.Albs = value.Deliveries.Count;
                entity.Cur = "EUR";
                entity.Eur = value.Deliveries.Sum(x => x.Amt?.Eur ?? 0);
                entity.Val = value.Deliveries.Sum(x => x.Amt?.Eur ?? 0);
        }


        public static void SetNextId(MaxiContext db, TransmissionModel value, ref Entities.Transm entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
            var fch = value.Fch;
            var yea = (short)fch.Year;

            var lastId = db.Transms
                .AsNoTracking()
                .Where(x => x.Emp == emp & x.Yea == yea)
                .Max(x => x.Transm1);

            value.Id = lastId + 1;
            entity.Emp = emp;
            entity.Yea = yea;
            entity.Transm1 = value.Id;
        }
        public static List<DeliveryModel> Deliveries(Guid guid)
        {
            List<DeliveryModel> retval = new();
            using (var db = new Entities.MaxiContext())
            {
                return db.Albs
                    .AsNoTracking()
                    .Include(x => x.Fra)
                    .Where(x => x.TransmGuid == guid)
                    .OrderBy(x => x.Alb1)
                    .Select(x => new DeliveryModel(x.Guid)
                    {
                        Id = x.Alb1,
                        Fch = x.Fch,
                        Nom = x.Nom,
                        Address = new AddressModel
                        {
                            ZipGuid = x.Zip
                        },
                        Amt = new Amt(x.Eur),
                        Invoice = x.Fra == null ? null : new GuidNom((Guid)x.FraGuid!, x.Fra.Fra1.ToString())
                    }).ToList();
            }
        }
        public static Media? DeliveriesPdf(Guid guid)
        {
            Media? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                var isElCorteIngles = db.Albs
                    .Include(x => x.Cli)
                    .ThenInclude(x => x.CliClient)
                    .Any(x => x.TransmGuid == guid && x.Cli != null && x.Cli.CliClient != null && (x.Cli.CliClient.CcxGuid == CustomerModel.Wellknown(CustomerModel.Wellknowns.elCorteIngles)!.Guid || x.Cli.CliClient.CcxGuid == CustomerModel.Wellknown(CustomerModel.Wellknowns.eciga)!.Guid));

                if (isElCorteIngles)
                {
                    //delega-ho de moment a la Api antiga que endreça per destinacions i genera una portada per plataforma
                    var url = $"https://api.matiasmasso.es/api/Transmisio/PdfDeliveries/{guid.ToString()}";
                    retval = new Media(Media.MimeCods.Pdf, ApiHelper.GetAsync(url));
                }
                else
                {
                    var guids = db.Albs.Where(x => x.TransmGuid == guid)
                        .OrderBy(x => x.Alb1)
                        .Select(x => x.Guid).ToList();
                    retval = DeliveriesService.Pdfs(guids);
                }
            }
            return retval;
        }
    }
    public class TransmissionsService
    {
        public static void Test()
        {
            using (var db = new Entities.MaxiContext())
            {
                var a = db.Albs
                    .Include(x => x.Arcs)
                    .Select(x => new
                    {
                        a = x.Arcs.Sum(y => y.Qty)
                    }).ToList();
            }
        }
        public static List<TransmissionModel> GetValues(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                var albs = db.VwTransms
                    .AsNoTracking()
                    .Where(x=>x.Yea == year)
                    .OrderByDescending(x=>x.Transm)
                    .ToList();
                var retval = new List<TransmissionModel>();
                var transm = new TransmissionModel();
                foreach (var a in albs.OrderByDescending(x=>x.Transm))
                {
                   if(a.Guid != transm.Guid)
                    {
                        transm = new TransmissionModel(a.Guid)
                        {
                            Id = a.Transm,
                            Fch = (DateTime)a.Fch!.Value.DateTime
                        };
                        retval.Add(transm);
                    }
                    transm.AlbsCount += 1;
                    transm.Eur += a.AlbEur ?? 0;
                    transm.LinsCount += a.Lins ?? 0;
                    transm.UnitsCount += a.Qties ?? 0;
                    if(a.FraGuid == null && a.AlbEur != null && a.AlbEur != 0)
                            transm.AlbsInvoicePending += 1;
                }

                return retval;


            }

        }
    }
}
