using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Api.Services
{
    public class CcaService
    {
        public static CcaModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                CcaModel? retval = db.Ccas
                    .Where(x => x.Guid == guid)
                    .Select(x => new CcaModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Id = x.Cca1,
                        Fch = x.Fch,
                        Concept = x.Txt,
                        Ccd = (DTO.CcaModel.CcdEnum?)x.Ccd ?? DTO.CcaModel.CcdEnum.Manual,
                        Cdn = x.Cdn,
                        Projecte = x.Projecte,
                        Docfile = x.Hash == null ? null : new DocfileModel(x.Hash)
                    })
                    .FirstOrDefault();

                if (retval != null)
                {
                    retval.Items = db.Ccbs
                        .Where(x => x.CcaGuid == guid)
                        .Select(x => new CcaModel.Item(x.Guid)
                        {
                            Cta = x.CtaGuid,
                            Contact = x.ContactGuid,
                            Eur = x.Eur,
                            Dh = (CcaModel.Item.DhEnum)x.Dh
                        }).ToList();
                }

                if (retval?.Docfile != null)
                {
                    var hash = retval.Docfile.Hash;
                    retval.Docfile = db.DocFiles
                        .Where(x => x.Hash == hash)
                        .Select(x => new DocfileModel(x.Hash)
                        {
                            Document = new Media((Media.MimeCods)x.Mime, null),
                            Thumbnail = new Media((Media.MimeCods)x.ThumbnailMime, null),
                            Pags = x.Pags,
                            Size = x.Size,
                            Width = x.Width,
                            Height = x.Height,
                            FchCreated = x.FchCreated
                        })
                        .FirstOrDefault();
                }

                return retval;
            }
        }

        public static DocfileModel? Docfile(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Ccas
                    .Where(x => x.Guid == guid)
                    .Join(db.DocFiles, cca => cca.Hash, docfile => docfile.Hash, (cca, docfile) => new DocfileModel
                    {
                        Document = new Media((Media.MimeCods)docfile.Mime, docfile.Doc)
                    })
                    .FirstOrDefault();
                return retval;
            }
        }

        public static Media? Thumbnail(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Ccas
                    .Where(x => x.Guid == guid)
                    .Join(db.DocFiles, cca => cca.Hash, docfile => docfile.Hash, (cca, docfile) =>
                    new Media((Media.MimeCods)docfile.ThumbnailMime, docfile.Thumbnail))
                    .FirstOrDefault();
                return retval;
            }
        }

        public static bool Update(CcaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Update(db, value);
                db.SaveChanges();
                return true;
            }
        }
        public static bool Update(MaxiContext db, CcaModel value)
        {
            if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

            Entities.Cca? entity;
            var fch = value.Fch!;
            if (value.IsNew)
            {
                entity = new Entities.Cca();
                SetNextId(db, value, ref entity);
                db.Ccas.Add(entity);
            }
            else
            {
                entity = db.Ccas
                    .Include(x => x.Ccbs)
                    .FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new System.Exception("Cca not found");

                if ((int)value.Emp != entity.Emp || ((DateOnly)fch).Year != entity.Yea )
                    SetNextId(db, value, ref entity);
            }

            entity.Fch = value.Fch;
            entity.Txt = value.Concept;
            entity.Ccd = (int?)value.Ccd ?? 0;
            entity.Cdn = value.Cdn ?? 0;
            entity.Projecte = value.Projecte;
            entity.Hash = value.Docfile?.Hash;
            entity.UsrCreatedGuid = value.UsrLog?.UsrCreated?.Guid;
            entity.UsrLastEditedGuid = value.UsrLog?.UsrLastEdited?.Guid;
            entity.Ccbs = value.Items.Select(x => new Entities.Ccb()
            {
                Lin = value.Items.IndexOf(x) + 1,
                CtaGuid = (Guid)x.Cta!,
                ContactGuid = x.Contact,
                Eur = x.Eur,
                Cur = "EUR",
                Pts = x.Eur,
                Dh = (byte)x.Dh
            }).ToList();
            return true;
        }

        public static void Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                Delete(db, guid);
                db.SaveChanges();
            }
        }

        public static void Delete(Entities.MaxiContext db, Guid guid)
        {

            db.Ccbs.RemoveRange(db.Ccbs.Where(x => x.CcaGuid == guid));
            db.Ccas.Remove(db.Ccas.Single(x => x.Guid.Equals(guid)));
        }

        public static void SetNextId(MaxiContext db, CcaModel value, ref Entities.Cca entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
            var fch = value.Fch!;
            var yea = ((DateOnly)fch).Year;

            int lastId;
            if (db.Ccas.Any(x => x.Emp == emp && x.Yea == yea))
                lastId = db.Ccas.Where(x => x.Emp == emp & x.Yea == yea).Max(x => x.Cca1);
            else
                lastId = 0;

            entity.Guid = value.Guid;
            entity.Emp = emp;
            entity.Yea = (short)yea;
            entity.Cca1 = lastId + 1;
        }

    }

    public class CcasService
    {

        public static List<CcaModel> GetValues(EmpModel.EmpIds emp, int year)
        {
            using (var db = new Entities.MaxiContext())
            {

                return db.VwCcasLists
                     .Where(x => x.Emp == (int)emp & x.Yea == year)
                     .OrderByDescending(x => x.Cca)
                     .Select(x => new CcaModel(x.Guid)
                     {
                         Emp = emp,
                         Id = x.Cca,
                         Fch = x.Fch,
                         Concept = x.Txt,
                         Docfile = x.Hash == null ? null : new DocfileModel(x.Hash),
                         Amount = x.Eur,
                         UsrCreated = x.UsrNom == null ? null : new GuidNom(Guid.NewGuid(), x.UsrNom),
                         UsrLog = new UsrLogModel() {FchLastEdited=x.FchLastEdited}
                     }).ToList();
            }
        }

        public static List<CcaModel> Search(int emp, decimal value)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Ccas
                    .Include(x => x.Ccbs)
                 .Where(x => x.Emp == emp & x.Ccbs.Any(y=>y.Eur==value))
                 .OrderByDescending(x => x.Yea)
                 .ThenBy(x=>x.Cca1)
                     .Select(x => new CcaModel(x.Guid)
                     {
                         Id = x.Cca1,
                         Emp = (EmpModel.EmpIds)x.Emp,
                         Fch = x.Fch,
                         Concept = x.Txt,
                         Docfile = x.Hash == null ? null : new DocfileModel(x.Hash),
                         Projecte = x.Projecte,
                         Items = x.Ccbs.Select(y => new CcaModel.Item(y.Guid)
                         {
                             Dh = (CcaModel.Item.DhEnum)y.Dh,
                             Eur = y.Eur,
                             Cta = y.CtaGuid,
                             Contact = y.ContactGuid
                         }).ToList()
                     }).ToList();
            }
        }

        public static List<CcaModel> FromExercise(ExerciciModel exercici)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Ccas
                     .Where(x => x.Emp == (int)exercici.Emp & x.Yea == exercici.Year)
                     .OrderByDescending(x => x.Cca1)
                     .Select(x => new CcaModel(x.Guid)
                     {
                         Id = x.Cca1,
                         Emp = (EmpModel.EmpIds)x.Emp,
                         Fch = x.Fch,
                         Concept = x.Txt,
                         Docfile = x.Hash == null ? null : new DocfileModel(x.Hash),
                         Projecte = x.Projecte,
                         UsrLog = new UsrLogModel
                         {
                             FchCreated= x.FchCreated,
                             FchLastEdited = x.FchLastEdited
                         },
                         Items = x.Ccbs.Select(y => new CcaModel.Item(y.Guid)
                         {
                             Dh = (CcaModel.Item.DhEnum)y.Dh,
                             Eur = y.Eur,
                             Cta = y.CtaGuid,
                             Contact = y.ContactGuid
                         }).ToList()
                     }).ToList();
            }
        }


        public static PgcCtaModel.Extracte Extracte(PgcCtaModel.Extracte extracte)
        {
            using (var db = new Entities.MaxiContext())
            {
                extracte.Ccas = db.Ccas
                     .Where(x => x.Emp == (int)extracte.Emp! && x.Yea == extracte.Year && x.Ccbs.Any(y => y.CtaGuid == extracte.Cta && y.ContactGuid == extracte.Contact))
                     .OrderByDescending(x => x.Cca1)
                     .Select(x => new CcaModel(x.Guid)
                     {
                         Id = x.Cca1,
                         Emp = (EmpModel.EmpIds)x.Emp,
                         Fch = x.Fch,
                         Concept = x.Txt,
                         Docfile = x.Hash == null ? null : new DocfileModel(x.Hash),
                         Projecte = x.Projecte,
                         Ccd = (CcaModel.CcdEnum?)x.Ccd ?? CcaModel.CcdEnum.Manual,
                         Cdn = x.Cdn,
                         Items = x.Ccbs.Select(y => new CcaModel.Item(y.Guid)
                         {
                             Dh = (CcaModel.Item.DhEnum)y.Dh,
                             Eur = y.Eur,
                             Cta = y.CtaGuid,
                             Contact = y.ContactGuid
                         }).ToList()
                     }).ToList();
                return extracte;
            }
        }

        public static void Delete(Entities.MaxiContext db, List<Guid> guids)
        {
            var ccbsToRemove = db.Ccbs.Where(x => guids.Any(y => x.CcaGuid == y)).ToList();
            db.Ccbs.RemoveRange(ccbsToRemove);

            var ccasToRemove = db.Ccas.Where(x => guids.Any(y => x.Guid == y)).ToList();
            db.Ccas.RemoveRange(ccasToRemove);
        }

        public static byte[] LlibreDiari(ExerciciModel exercici)
        {
            var ccas = FromExercise(exercici).OrderBy(x => x.Id).ToList();
            var ctas = Services.PgcCtasService.GetValues();
            var contacts = Services.ContactsService.FetchAll().Where(x => x.Emp == exercici.Emp).ToList();
            var filename = $"Llibre diari {exercici.Emp.ToString()} {exercici.Year}";

            var sheet = new DTO.Excel.Sheet("Llibre diari", filename);
            sheet.AddColumn("registro", DTO.Excel.Cell.NumberFormats.Integer);
            sheet.AddColumn("fecha", DTO.Excel.Cell.NumberFormats.DDMMYY);
            sheet.AddColumn("concepto");
            sheet.AddColumn("cuenta");
            sheet.AddColumn("descripción");
            sheet.AddColumn("debe", DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn("haber", DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn("documento");
            foreach (var cca in ccas)
            {
                foreach (var ccb in cca.Items)
                {

                    var cta = ctas.FirstOrDefault(x => x.Guid == ccb.Cta);
                    var contact = contacts.FirstOrDefault(x => x.Guid == ccb.Contact);
                    var ctaId = $"{(cta == null ? 0 : cta.Id):00000}{(contact == null ? 0 : contact.Id):00000}";
                    var ctaNom = $"{(cta == null ? "" : cta.NomCat())}{(contact == null ? "" : contact.RaoSocial)}";
                    var row = sheet.AddRow();
                    row.AddInt(cca.Id);
                    row.AddFch(cca.Fch);
                    row.AddCell(cca.Concept);
                    row.AddCell(ctaId);
                    row.AddCell(ctaNom);
                    row.AddCell(ccb.Debit() == null ? 0 : (decimal)ccb.Debit()!);
                    row.AddCell(ccb.Credit() == null ? 0 : (decimal)ccb.Credit()!);
                    row.AddCell(cca.Docfile == null ? "" : cca.DownloadUrl());
                }
            }
            var retval = DTO.Excel.ClosedXml.Bytes(sheet);
            return retval;
        }


        public static byte[] LlibreFacturesEmeses(ExerciciModel exercici)
        {
            var ccas = FromExercise(exercici).OrderBy(x => x.Id).ToList();
            var ctas = Services.PgcCtasService.GetValues();
            var contacts = Services.ContactsService.FetchAll().Where(x => x.Emp == exercici.Emp).ToList();
            var filename = $"Llibre diari {exercici.Emp.ToString()} {exercici.Year}";

            //registre	factura	data	NIF	client	Base imponible	Iva

            var sheet = new DTO.Excel.Sheet("Llibre diari", filename);
            sheet.AddColumn("registre", DTO.Excel.Cell.NumberFormats.Integer);
            sheet.AddColumn("factura", DTO.Excel.Cell.NumberFormats.Integer);
            sheet.AddColumn("data", DTO.Excel.Cell.NumberFormats.DDMMYY);
            sheet.AddColumn("NIF");
            sheet.AddColumn("client");
            sheet.AddColumn("base imponible", DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn("Iva", DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn("document");
            foreach (var cca in ccas)
            {
                foreach (var ccb in cca.Items)
                {

                    var cta = ctas.FirstOrDefault(x => x.Guid == ccb.Cta);
                    var contact = contacts.FirstOrDefault(x => x.Guid == ccb.Contact);
                    var ctaId = $"{(cta == null ? 0 : cta.Id):00000}{(contact == null ? 0 : contact.Id):00000}";
                    var ctaNom = $"{(cta == null ? "" : cta.NomCat())}{(contact == null ? "" : contact.RaoSocial)}";
                    var row = sheet.AddRow();
                    row.AddInt(cca.Id);
                    row.AddFch(cca.Fch);
                    row.AddCell(cca.Concept);
                    row.AddCell(ctaId);
                    row.AddCell(ctaNom);
                    row.AddCell(ccb.Debit() == null ? 0 : (decimal)ccb.Debit()!);
                    row.AddCell(ccb.Credit() == null ? 0 : (decimal)ccb.Credit()!);
                    row.AddCell(cca.Docfile == null ? "" : cca.DownloadUrl());
                }
            }
            var retval = DTO.Excel.ClosedXml.Bytes(sheet);
            return retval;
        }


        public static byte[] LlibreFacturesRebudes(ExerciciModel exercici)
        {
            var ccas = FromExercise(exercici).OrderBy(x => x.Id).ToList();
            var ctas = Services.PgcCtasService.GetValues();
            var contacts = Services.ContactsService.FetchAll().Where(x => x.Emp == exercici.Emp).ToList();
            var filename = $"Llibre diari {exercici.Emp.ToString()} {exercici.Year}";

            var sheet = new DTO.Excel.Sheet("Llibre diari", filename);
            sheet.AddColumn("registro", DTO.Excel.Cell.NumberFormats.Integer);
            sheet.AddColumn("fecha", DTO.Excel.Cell.NumberFormats.DDMMYY);
            sheet.AddColumn("concepto");
            sheet.AddColumn("cuenta");
            sheet.AddColumn("descripción");
            sheet.AddColumn("debe", DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn("haber", DTO.Excel.Cell.NumberFormats.Euro);
            sheet.AddColumn("documento");
            foreach (var cca in ccas)
            {
                foreach (var ccb in cca.Items)
                {

                    var cta = ctas.FirstOrDefault(x => x.Guid == ccb.Cta);
                    var contact = contacts.FirstOrDefault(x => x.Guid == ccb.Contact);
                    var ctaId = $"{(cta == null ? 0 : cta.Id):00000}{(contact == null ? 0 : contact.Id):00000}";
                    var ctaNom = $"{(cta == null ? "" : cta.NomCat())}{(contact == null ? "" : contact.RaoSocial)}";
                    var row = sheet.AddRow();
                    row.AddInt(cca.Id);
                    row.AddFch(cca.Fch);
                    row.AddCell(cca.Concept);
                    row.AddCell(ctaId);
                    row.AddCell(ctaNom);
                    row.AddCell(ccb.Debit() == null ? 0 : (decimal)ccb.Debit()!);
                    row.AddCell(ccb.Credit() == null ? 0 : (decimal)ccb.Credit()!);
                    row.AddCell(cca.Docfile == null ? "" : cca.DownloadUrl());
                }
            }
            var retval = DTO.Excel.ClosedXml.Bytes(sheet);
            return retval;
        }

        public static List<CcaModel> NoCuadran(EmpModel.EmpIds emp, int yea)
        {
            using (var db = new Entities.MaxiContext())
            {
                var query = from cca in db.Ccas
                        join ccb in db.Ccbs on cca.Guid equals ccb.CcaGuid
                        where cca.Emp == (int)emp && cca.Yea == yea
                        group ccb by new { cca.Guid, cca.Cca1, cca.Fch, cca.Txt } into g
                        let sum = g.Sum(ccb => ccb.Dh == 1 ? ccb.Eur : -ccb.Eur)
                        where sum != 0
                        select new CcaModel(g.Key.Guid)
                        {
                           Id=  g.Key.Cca1,
                           Fch= g.Key.Fch,
                           Concept = g.Key.Txt
                        };
                return query.AsEnumerable().ToList();
            }
        }

    }
}
