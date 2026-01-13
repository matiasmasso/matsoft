using Api.Entities;
using Api.Helpers;
using DocumentFormat.OpenXml.Wordprocessing;
using DTO;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Api.Services
{
    public class DeliveryService
    {
        public static async Task<DeliveryModel?> WithTracking(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = Fetch(db, guid);
                if (retval != null)
                    retval.Trace = await Integracions.Vivace.Tracking.Trace(retval);
                return retval;
            }
        }

        public static DeliveryModel? Fetch(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return Fetch(db, guid);
            }

        }
        public static DeliveryModel? Fetch(Entities.MaxiContext db, Guid guid)
        {
            var retval = db.Albs
                .AsNoTracking()
                .Where(x => x.Guid == guid)
                .Select(x => new DeliveryModel(x.Guid)
                {
                    Id = x.Alb1,
                    Fch = x.Fch,
                    Cod = (PurchaseOrderModel.Cods)x.Cod,
                    Nom = x.Nom,
                    Address = new AddressModel
                    {
                        Text = x.Adr,
                        Zip = new ZipDTO((Guid)x.Zip!)
                        {
                            Location = x.ZipNavigation == null ? null : new LocationDTO(x.ZipNavigation.Location)
                            {
                                Nom = x.ZipNavigation.LocationNavigation.Nom,
                                Zona = new ZonaDTO(x.ZipNavigation.LocationNavigation.Zona)
                                {
                                    Nom = x.ZipNavigation.LocationNavigation.ZonaNavigation.Nom,
                                    Country = new CountryDTO(x.ZipNavigation.LocationNavigation.ZonaNavigation.Country)
                                    {
                                        Nom = new LangTextDTO
                                        {
                                            Esp = x.ZipNavigation.LocationNavigation.ZonaNavigation.CountryNavigation.NomEsp,
                                            Cat = x.ZipNavigation.LocationNavigation.ZonaNavigation.CountryNavigation.NomCat,
                                            Eng = x.ZipNavigation.LocationNavigation.ZonaNavigation.CountryNavigation.NomEng,
                                            Por = x.ZipNavigation.LocationNavigation.ZonaNavigation.CountryNavigation.NomPor
                                        }
                                    }
                                }
                            }
                        }
                    },
                    Contact = x.CliGuid == null ? null : new GuidNom((Guid)x.CliGuid, x.Nom),
                    Deutor = x.Deutor,
                    ExportCod = (ZonaModel.ExportCods)x.ExportCod,
                    PortsCod = (CustomerModel.PortsCodes)x.PortsCod,
                    CashCod = (CustomerModel.CashCodes)x.CashCod,
                    RetencioCod = (DeliveryModel.RetencioCods)x.RetencioCod,
                    Invoice = x.Fra == null ? null : new GuidNom((Guid)x.FraGuid!, string.Format("{0} del {1:dd/MM/yy}", x.Fra.Fra1, x.Fra.Fch)),
                    Transmission = x.Transm == null ? null : new GuidNom((Guid)x.TransmGuid!, string.Format("{0} del {1:dd/MM/yy HH:mm}", x.Transm.Transm1, x.Transm.Fch)),
                    Items = x.Arcs.OrderBy(y => y.Lin)
                        .Select(y => new DeliveryModel.Item(y.Guid)
                        {
                            Lin = y.Lin,
                            Qty = y.Qty,
                            Price = y.Eur,
                            Dto = (decimal?)y.Dto,
                            Sku = y.ArtGuid,
                            PdcGuid = y.PdcGuid
                        }).ToList(),
                    UsrLog = new UsrLogModel
                    {
                        UsrCreated = x.UsrCreatedGuid == null ? null : new GuidNom((Guid)x.UsrCreatedGuid, x.UsrCreated!.Nickname ?? x.UsrCreated.Nom ?? x.UsrCreated!.Adr ?? ""),
                        FchCreated = x.FchCreated,
                        UsrLastEdited = x.UsrLastEditedGuid == null ? null : new GuidNom((Guid)x.UsrLastEditedGuid, x.UsrLastEdited!.Nickname ?? x.UsrLastEdited.Nom ?? x.UsrLastEdited!.Adr ?? ""),
                        FchLastEdited = x.FchLastEdited
                    }

                })
                .FirstOrDefault();

            if (retval != null)
            {
                var pdcGuids = retval.Items.Select(x => x.PdcGuid).Distinct().ToList();
                retval.PurchaseOrders = db.Pdcs
                    .Where(x => pdcGuids.Any(y => x.Guid == y))
                    .Select(x => new DeliveryModel.PurchaseOrderClass(x.Guid)
                    {
                        Concept = x.Pdd,
                        Fch = x.Fch
                    }).ToList();
            }
            return retval;
        }


        public static Media Pdf(Guid guid)
        {
            var guids = new List<Guid> { guid };
            var retval = DeliveriesService.Pdfs(guids);
            return retval;

            //Media? retval;
            //var url = $"https://api.matiasmasso.es/api/delivery/pdf/{guid.ToString()}/0";
            //var alb = ApiHelper.GetAsync(url);

            //string? customDocsUrl;
            //using (var db = new MaxiContext())
            //{
            //    customDocsUrl = db.Albs.Where(x => x.Guid == guid).Select(x => x.CustomerDocUrl).FirstOrDefault();
            //}

            //if (customDocsUrl == null)
            //    retval = new Media(Media.MimeCods.Pdf, alb);
            //else
            //{
            //    var customDoc = ApiHelper.GetAsync(customDocsUrl);
            //    var pdfList = new List<byte[]> { alb, customDoc };
            //    var mergedBytes = IText7Helper.MergePdfs(pdfList);
            //    retval = new Media(Media.MimeCods.Pdf, mergedBytes);
            //}

            //return retval;
        }



        public static void Update(Entities.MaxiContext db, DeliveryModel value)
        {
            UpdateHeader(db, value);
            UpdateItems(db, value);
            UpdateAttachedDocs(db, value);
        }

        public static void UpdateAttachedDocs(Entities.MaxiContext db, DeliveryModel value)
        {
            foreach(var docfile in value.AttachedDocs)
            {
                DocfileService.Update(db,docfile);

                var entity = new Entities.DocFileSrc();
                entity.SrcGuid = value.Guid;
                entity.Hash = docfile.Hash!;
                entity.SrcCod = (int)DocfileModel.Cods.deliveryAttachment;
                db.DocFileSrcs.Add(entity);
            }
        }
        public static void UpdateHeader(Entities.MaxiContext db, DeliveryModel value)
        {
            var guid = value.Guid;
            var fch = value.Fch!;

            Entities.Alb? entity;
            if (value.IsNew)
            {
                entity = new Entities.Alb();
                SetNextId(db, value, ref entity);
                db.Albs.Add(entity);
                entity.Guid = value.Guid;
            }
            else
            {
                entity = db.Albs.Find(value.Guid);
                if (entity == null) throw new System.Exception("Alb not found");
                if (fch.Year != entity.Yea)
                    SetNextId(db, value, ref entity);
            }

            if (entity == null) throw new System.Exception("Delivery not found");

            entity.Fch = value.Fch as DateTime? ?? DateTime.Today;
            entity.Cod = (short?)value.Cod ?? (int)PurchaseOrderModel.Cods.Customer;
            entity.CliGuid = value.Contact?.Guid ?? value.ContactGuid;
            entity.Deutor = value.Deutor;
            entity.Nom = value.Nom ?? "";
            entity.Adr = value.Address?.Text ?? "";
            entity.Zip = value.Address?.Zip?.Guid;
            entity.Tel = value.Tel ?? "";
            entity.ExportCod = (int?)value.ExportCod ?? 0;
            entity.PortsCod = (int?)value.PortsCod ?? 0;
            entity.CashCod = (int?)value.CashCod ?? 0;
            entity.RetencioCod = (int?)value.RetencioCod ?? 0;
            entity.Incoterm = value.Incoterm;
            entity.Fpg = value.Fpg;
            entity.MgzGuid = value.Mgz?.Guid;
            entity.Eur = value.Amt?.Eur ?? 0;
            entity.Cur = "EUR";
            entity.Pts = value.Amt?.Eur ?? 0;
            entity.Obs = value.Obs;
            entity.ObsTransp = value.ObsTransp;
            entity.UsrCreatedGuid = value.UsrLog?.UsrCreated?.Guid;
            entity.UsrLastEditedGuid = value.UsrLog?.UsrLastEdited?.Guid;
            entity.FchCreated = value.UsrLog?.FchCreated ?? DateTime.Now;
            entity.FchLastEdited = value.UsrLog?.FchLastEdited ?? DateTime.Now;

        }
        public static void UpdateItems(Entities.MaxiContext db, DeliveryModel value)
        {
            var lin = 1;
            foreach (var item in value.Items)
            {
                var entity = new Entities.Arc();
                entity.Guid = item.Guid;
                entity.Lin = lin;
                entity.Cod = (int)DeliveryModel.Item.Cods.Sale;
                entity.AlbGuid = value.Guid;
                entity.Qty = item.Qty ?? 0;
                entity.ArtGuid = (Guid)item.Sku!;
                entity.PncGuid = item.PncGuid;
                entity.PdcGuid = item.PdcGuid;
                entity.MgzGuid = item.MgzGuid;
                entity.Eur = item.Price ?? 0;
                entity.Pts = item.Price ?? 0;
                entity.Cur = "EUR";
                entity.Net = item.Amt();
                db.Arcs.Add(entity);
                lin += 1;
            }
        }

        public static void SetNextId(MaxiContext db, DeliveryModel value, ref Entities.Alb entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
            var fch = (DateTime)value.Fch!;
            var yea = (short)fch.Year;

            var lastId = db.Albs
                .AsNoTracking()
                .Where(x => x.Emp == emp & x.Yea == yea)
                .Max(x => x.Alb1);

            value.Id = lastId + 1;
            entity.Emp = emp;
            entity.Yea = yea;
            entity.Alb1 = value.Id;
        }
    }
    public class DeliveriesService
    {

        public static Tuple<List<int>, List<DeliveryModel>>? GetValues(int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                Tuple<List<int>, List<DeliveryModel>>? retval = null;
                var years = GetYears(db, emp);
                if (years.Count > 0)
                {
                    var lastYear = years.First();
                    var albs = GetValues(db, emp, lastYear);
                    retval = new Tuple<List<int>, List<DeliveryModel>>(years, albs);
                }
                return retval;
            }
        }
        public static List<DeliveryModel> GetValues(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValues(db, emp, year);
            }
        }

        public static List<int> GetYears(MaxiContext db, int emp)
        {
            return db.Albs
                .AsNoTracking()
                .Where(x => x.Emp == emp)
                .GroupBy(x => x.Yea)
                .Select(x => (int)x.First().Yea)
                .OrderByDescending(x => x)
                .ToList();
        }

        public static List<DeliveryModel> GetValues(MaxiContext db, int emp, int year)
        {
            var empId = (EmpModel.EmpIds)emp;
            return db.Albs
                .AsNoTracking()
                .Where(x => x.Emp == emp && x.Yea == year)
                .OrderByDescending(x => x.Alb1)
                .Select(x => new DeliveryModel(x.Guid)
                {
                    Emp = empId,
                    Fch = x.Fch,
                    Id = x.Alb1,
                    Cod = (PurchaseOrderModel.Cods)x.Cod,
                    ContactGuid = x.CliGuid,
                    Nom = x.Nom,
                    Amt = new Amt(x.Pts, x.Cur, x.Eur)
                })
                .ToList();
        }


        //-------------------------------------------------
        public static DeliveryModel.List Fetch(Guid contactGuid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new DeliveryModel.List();

                retval.Items = db.Albs
                .AsNoTracking()
                .Where(x => x.CliGuid == contactGuid)
                .Select(x => new DeliveryModel(x.Guid)
                {
                    Id = x.Alb1,
                    Fch = x.Fch,
                    Cod = (PurchaseOrderModel.Cods)x.Cod,
                    Contact = x.CliGuid == null ? null : new GuidNom((Guid)x.CliGuid, x.Nom),
                    Amt = new Amt(x.Pts, x.Cur, x.Eur)
                })
                .OrderByDescending(x => x.Fch.Year)
                .ThenByDescending(x => x.Id)
                .ToList();
                return retval;
            }
        }

        public static DeliveryModel.List FromYear(int emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new DeliveryModel.List();

                retval.Years = db.Albs
                    .AsNoTracking()
                    .Where(x => x.Emp == emp)
                    .GroupBy(x => x.Yea)
                    .Select(x => (int)x.Key)
                    .OrderByDescending(x => x)
                    .ToList();

                if (year == 0) year = retval.Years.FirstOrDefault();


                retval.Items = db.Albs
                    .AsNoTracking()
                    .Where(x => x.Emp == emp && x.Yea == year)
                    .Select(x => new DeliveryModel(x.Guid)
                    {
                        Id = x.Alb1,
                        Fch = x.Fch,
                        Cod = (PurchaseOrderModel.Cods)x.Cod,
                        Contact = new GuidNom((Guid)x.CliGuid!, x.Nom),
                        Amt = new Amt(x.Pts, x.Cur, x.Eur)
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();
                return retval;
            }
        }

        public static Media? Pdfs(List<Guid> guids)
        {
            //build a list of deliveries with an online document to download and attach
            List<KeyValuePair<Guid, string>> customDocUrls = new();
            //using (var db = new MaxiContext())
            //{
            //    customDocUrls = db.Albs.Where(x => guids.Any(y => x.Guid == y) && !string.IsNullOrEmpty(x.CustomerDocUrl))
            //    .Select(x => new KeyValuePair<Guid, string>(x.Guid, x.CustomerDocUrl!))
            //    .ToList();
            //}

            //build a list of delivery attachment docfiles
            List<KeyValuePair<Guid, byte[]>> customAttachedFiles = new();
            //using (var db = new MaxiContext())
            //{
            //    customAttachedFiles = db.DocFileSrcs
            //        .Include(x => x.HashNavigation)
            //        .Where(x => guids.Any(y => x.SrcGuid == y) && x.HashNavigation != null && x.HashNavigation.Doc != null)
            //        .Select(x => new KeyValuePair<Guid, byte[]>(x.SrcGuid, x.HashNavigation!.Doc!))
            //        .ToList();
            //}

            //build a list of delivery pdfs, inserting shipping label pdfs where appropiate
            List<byte[]> pdfBytes = new();
            foreach (var guid in guids)
            {
                //download main delivery document from old api (since it still uses C1.Pdf component)
                var url = $"https://api.matiasmasso.es/api/delivery/pdf/{guid.ToString()}/0";
                pdfBytes.Add(ApiHelper.GetAsync(url));

                //download online document from customer url if any
                var hasOnlineDoc = customDocUrls.Any(x => x.Key == guid);
                if (hasOnlineDoc)
                {
                    url = customDocUrls.First(x => x.Key == guid).Value;
                    pdfBytes.Add(ApiHelper.GetAsync(url));
                }

                //attach any attachments registered with this delivery note
                var hasDocfiles = customAttachedFiles.Any(x => x.Key == guid);
                if (hasDocfiles)
                {
                    var attachments = customAttachedFiles
                        .Where(x => x.Key == guid)
                        .Select(x=>x.Value)
                        .ToList();

                    pdfBytes.AddRange(attachments);
                }
            }

            //merge the list of pdfs on a single byte array
            Media? retval = default;
            if (pdfBytes.Count > 0)
            {
                var mergedBytes = IText7Helper.MergePdfs(pdfBytes);
                retval = new Media(Media.MimeCods.Pdf, mergedBytes);
            }
            return retval;
        }

        public static void Update(List<DeliveryModel> values)
        {
            using(var db = new MaxiContext())
            {
                foreach (var value in values)
                {
                    DeliveryService.Update(db,value);
                    db.SaveChanges(); // so it can pull new lastId numbers at each update
                }
            }
        }
    }
}
