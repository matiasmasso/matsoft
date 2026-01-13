using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace Api.Services
{
    public class IncidenciaService
    {
        public static IncidenciaModel? Get(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Incidencies
                    .AsNoTracking()
                    .Include(x => x.IncidenciaDocFiles)
                    .Include(x => x.UsrCreatedNavigation)
                    .Include(x => x.UsrLastEditedNavigation)
                    .Where(x => x.Guid == guid)
                    .Select(x => new IncidenciaModel(x.Guid)
                    {
                        Id = x.Id,
                        Asin = x.Asin ?? "",
                        Fch = x.Fch,
                        ContactType = (IncidenciaModel.ContactTypes)x.ContactType,
                        ContactPerson = x.Person,
                        EmailAdr = x.Email,
                        Tel = x.Tel,
                        CustomerRef = x.SRef,
                        Customer = x.ContactGuid,
                        Product = x.ProductGuid,
                        SerialNumber = x.SerialNumber,
                        ManufactureDate = x.ManufactureDate,
                        Procedencia = (IncidenciaModel.Procedencias)x.Procedencia,
                        FchCompra = x.FchCompra,
                        BoughtFrom = x.BoughtFrom,
                        Obs = x.Obs,
                        Src = (IncidenciaModel.Srcs)x.Cod,
                        CodiTancament = x.CodiTancament,
                        PurchaseTickets = x.IncidenciaDocFiles
                                .Where(y => y.Cod == (int)IncidenciaModel.AttachmentCods.ticket)
                                .Select(y => new DocfileModel(y.Hash)).ToList(),
                        DocFileImages = x.IncidenciaDocFiles
                                .Where(y => y.Cod == (int)IncidenciaModel.AttachmentCods.imatge)
                                .Select(y => new DocfileModel(y.Hash)).ToList(),
                        Videos = x.IncidenciaDocFiles
                                .Where(y => y.Cod == (int)IncidenciaModel.AttachmentCods.video)
                                .Select(y => new DocfileModel(y.Hash)).ToList(),
                        UsrLog = new UsrLogModel
                        {
                            FchCreated = x.FchCreated,
                            FchLastEdited = x.FchLastEdited,
                            UsrCreated = x.UsrCreatedNavigation == null ? null : new GuidNom(x.UsrCreatedNavigation.Guid, x.UsrCreatedNavigation!.Adr),
                            UsrLastEdited = x.UsrLastEditedNavigation == null ? null : new GuidNom(x.UsrLastEditedNavigation.Guid, x.UsrCreatedNavigation!.Adr),
                        }
                    }).FirstOrDefault();
            }
        }

        public async static Task<bool> Update(IncidenciaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Incidency? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Incidency();
                    await db.Incidencies.AddAsync(entity);
                    entity.Guid = value.Guid;
                    entity.Emp = (int)value.Emp;
                    entity.Id = NextId(db);
                }
                else
                    entity = db.Incidencies.Find(value.Guid);

                if (entity == null) throw new System.Exception("Incidencia not found");

                entity.Asin = value.Asin;
                entity.Fch = value.Fch;
                entity.Person = value.ContactPerson;
                entity.Email = value.EmailAdr;
                entity.Tel = value.Tel;
                entity.SRef = value.CustomerRef;
                entity.ProductGuid = value.Product;
                entity.SerialNumber = value.SerialNumber;
                entity.ManufactureDate = value.ManufactureDate;
                entity.Procedencia = (int)value.Procedencia;
                entity.FchCompra = value.FchCompra;
                entity.BoughtFrom = value.BoughtFrom;
                entity.Obs = value.Description;
                entity.ContactGuid = value.Customer;
                entity.ContactType = (int)value.ContactType;
                entity.Cod = (int)value.Src;
                entity.UsrCreated = value.UsrLog?.UsrCreated!.Guid;
                entity.FchCreated = value.UsrLog?.FchCreated ?? DateTime.Now;
                entity.UsrLastEdited = value.UsrLog?.UsrLastEdited!.Guid;
                entity.FchLastEdited = value.UsrLog?.FchLastEdited ?? DateTime.Now;

                foreach (var ticket in value.PurchaseTickets)
                {
                    DocfileService.Update(db, ticket);
                    await SaveAttachmentAsync(db, value, ticket, IncidenciaModel.AttachmentCods.ticket);
                }
                foreach (var image in value.DocFileImages)
                {
                    DocfileService.Update(db, image);
                    await SaveAttachmentAsync(db, value, image, IncidenciaModel.AttachmentCods.imatge);
                }
                foreach (var video in value.Videos)
                {
                    DocfileService.Update(db, video);
                    await SaveAttachmentAsync(db, value, video, IncidenciaModel.AttachmentCods.video);
                }

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }

        private static async Task<bool> SaveAttachmentAsync(Entities.MaxiContext db, IncidenciaModel incidencia, DocfileModel docfile, IncidenciaModel.AttachmentCods cod)
        {
            var attachmentEntity = new Entities.IncidenciaDocFile();
            await db.IncidenciaDocFiles.AddAsync(attachmentEntity);
            attachmentEntity.Hash = docfile.Hash ?? "";
            attachmentEntity.Incidencia = incidencia.Guid;
            attachmentEntity.Cod = (int)cod;
            return true;
        }


        public static int NextId(MaxiContext db)
        {

            var lastId = db.Incidencies
                .Max(x => x.Id);
            return lastId + 1;
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                db.IncidenciaDocFiles.RemoveRange(db.IncidenciaDocFiles.Where(x => x.Incidencia == guid));
                db.Incidencies.Remove(db.Incidencies.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class IncidenciasService
    {
        public static List<IncidenciaModel> Open(int empId)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new List<IncidenciaModel>();
                retval = db.Incidencies
                .AsNoTracking()
                .Include(x => x.IncidenciaDocFiles)
                    .Where(x => x.Emp == empId && x.FchClose == null)
                    .OrderByDescending(x => x.FchCreated)
                    .Select(x => new IncidenciaModel(x.Guid)
                    {
                        Id = x.Id,
                        Asin = x.Asin ?? "",
                        Fch = x.Fch,
                        Customer = x.ContactGuid,
                        Product = x.ProductGuid,
                        CodiTancament = x.CodiTancament,
                        ExistTickets = x.IncidenciaDocFiles.Any(y => y.Cod == (int)IncidenciaModel.AttachmentCods.ticket),
                        ExistImages = x.IncidenciaDocFiles.Any(y => y.Cod == (int)IncidenciaModel.AttachmentCods.imatge),
                        ExistVideos = x.IncidenciaDocFiles.Any(y => y.Cod == (int)IncidenciaModel.AttachmentCods.video),
                        Obs = x.Obs,
                        UsrLog = new UsrLogModel
                        {
                            UsrLastEdited = x.UsrLastEdited == null ? null : new GuidNom((Guid)x.UsrLastEdited, ""),
                            FchLastEdited = x.FchLastEdited
                        }
                    })
                    .ToList();
                return retval;
            }
        }

        public static List<IncidenciaModel> All(UserModel user, LangDTO lang, int empId, int? year = null)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = new List<IncidenciaModel>();
                switch ((UserModel.Rols)user.Rol!)
                {
                    case UserModel.Rols.cliFull:
                    case UserModel.Rols.cliLite:
                        retval = db.Incidencies
                        .AsNoTracking()
                            .Join(db.EmailClis, inc => inc.ContactGuid, ec => ec.ContactGuid, (inc, ec) => new { inc, ec })
                            .Where(x => x.ec.EmailGuid == user.Guid)
                            .OrderByDescending(x => x.inc.Id)
                            .Select(x => new IncidenciaModel
                            {
                                Guid = x.inc.Guid,
                                Id = x.inc.Id,
                                Asin = x.inc.Asin ?? "",
                                Fch = x.inc.Fch,
                                Customer = x.inc.ContactGuid,
                                Product = x.inc.ProductGuid,
                                CodiTancament = x.inc.CodiTancament
                            })
                            .ToList();
                        break;
                    default:
                        retval = db.Incidencies
                        .AsNoTracking()
                            .Where(x => x.Emp == empId && x.CodiTancament == null)
                            .OrderByDescending(x => x.Id)
                            .Select(x => new IncidenciaModel
                            {
                                Guid = x.Guid,
                                Id = x.Id,
                                Asin = x.Asin ?? "",
                                Fch = x.Fch,
                                Customer = x.ContactGuid,
                                Product = x.ProductGuid,
                                CodiTancament = x.CodiTancament
                            })
                            .ToList();
                        break;
                }
                return retval;
            }
        }
    }
}
