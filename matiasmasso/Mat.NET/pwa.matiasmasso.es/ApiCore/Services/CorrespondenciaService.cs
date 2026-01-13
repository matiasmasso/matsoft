using Api.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Api.Services
{
    public class CorrespondenciaService
    {

        public static CorrespondenciaModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwCrrs
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new CorrespondenciaModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Id = x.Crr,
                        Fch = x.Fch,
                        Subject = x.Dsc,
                        Rt = (CorrespondenciaModel.Rts)x.Rt,
                        Docfile = string.IsNullOrEmpty(x.Hash) ? null : new DocfileModel
                        {
                            Hash = x.Hash,
                            Size = x.Size,
                            Document = x.Mime == null ? null : new Media{Mime = (Media.MimeCods)x.Mime },
                            Thumbnail = x.ThumbnailMime == null ? null : new Media { Mime = (Media.MimeCods)x.ThumbnailMime },
                            Pags = x.Pags,
                            Width=x.Width,
                            Height=x.Height
                        },
                        UsrLog = new UsrLogModel
                        {
                            UsrCreated = x.UsrCreated == null ? null : new GuidNom
                            {
                                Guid = (Guid)x.UsrCreated,
                                Nom = x.UsrCreatedNickname ?? x.UsrCreatedEmailAddress
                            },
                            UsrLastEdited = x.UsrLastEdited == null ? null : new GuidNom
                            {
                                Guid = (Guid)x.UsrLastEdited,
                                Nom = x.UsrLastEditedNickname ?? x.UsrLastEditedEmailAddress
                            },
                            FchCreated = x.FchCreated,
                            FchLastEdited = x.FchLastEdited
                        }

                    }).FirstOrDefault();
                if(retval != null)
                    retval.Targets = db.VwCrrClis.Where(x=>x.Guid == retval.Guid)
                    .Select(x=>x.CliGuid).ToList();
                return retval;
            }
        }

        public static DocfileModel? Docfile(Guid guid)
        {
            DocfileModel? retval = null;
            using (var db = new Entities.MaxiContext())
            {
                retval = (from item in db.Crrs
                          join docfiles in db.DocFiles
                          on item.Hash equals docfiles.Hash
                          where item.Guid.Equals(guid)
                          select new DocfileModel()
                          {
                              Document = new Media((Media.MimeCods)docfiles.Mime, docfiles.Doc)
                          }).FirstOrDefault();
            }
            return retval;
        }

        public static bool Update(CorrespondenciaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

                Entities.Crr? entity;
                var fch = value.Fch!;
                if (value.IsNew)
                {
                    entity = new Entities.Crr();
                    SetNextId(db, value, ref entity);
                    db.Crrs.Add(entity);
                }
                else
                {
                    entity = db.Crrs
                        .Include(x => x.Clis)
                        .FirstOrDefault(x=>x.Guid == value.Guid);

                    if (entity == null) throw new System.Exception("Crr not found");

                    if (((DateOnly)fch).Year != entity.Yea)
                        SetNextId(db, value, ref entity);
                }

                entity.Fch = (DateOnly)value.Fch!;
                entity.Yea = (short)((DateOnly)value.Fch).Year;
                entity.Dsc = value.Subject ?? "";
                entity.Rt = (byte)value.Rt;
                entity.Hash = value.Docfile?.Hash;
                entity.UsrCreated = value.UsrLog?.UsrCreated?.Guid;
                entity.UsrLastEdited = value.UsrLog?.UsrLastEdited?.Guid;

                if(value.Targets != null)
                {
                    entity.Clis.Clear();
                    db.SaveChanges();

                    foreach (var target in value.Targets)
                    {
                        var cli = db.CliGrals.FirstOrDefault(x => x.Guid == target);
                        if (cli != null) entity.Clis.Add(cli);
                    }
                }

                db.SaveChanges();
                return true;
            }
        }

        public static void SetNextId(MaxiContext db, CorrespondenciaModel value, ref Entities.Crr entity)
        {
            var emp = (int?)value.Emp ?? (int)EmpModel.EmpIds.MatiasMasso;
            var fch = value.Fch!;
            var yea = ((DateOnly)fch).Year;

            int lastId;
            if (db.Crrs.Any(x => x.Emp == emp && x.Yea == yea))
                lastId = db.Crrs.Where(x => x.Emp == emp & x.Yea == yea).Max(x => x.Crr1);
            else
                lastId = 0;

            entity.Guid = value.Guid;
            entity.Emp = emp;
            entity.Yea = (short)yea;
            entity.Crr1 = lastId + 1;
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Crrs.Remove(db.Crrs.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;
        }

    }


    public class CorrespondenciasService
    {


        public static List<CorrespondenciaModel> GetValues(Guid contact)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.VwCrrClis
                    .AsNoTracking()
                    .Where(x => x.CliGuid == contact)
                    .Select(x => new CorrespondenciaModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Id = x.Crr,
                        Fch = x.Fch,
                        Subject = x.Dsc,
                        Rt = (CorrespondenciaModel.Rts)x.Rt,
                        Docfile = string.IsNullOrEmpty(x.Hash) ? null : new DocfileModel
                        {
                            Hash = x.Hash,
                            Size = x.Size,
                            Document = x.Mime == null ? null : new Media { Mime = (Media.MimeCods)x.Mime },
                            Pags = x.Pags,
                            Width = x.Width,
                            Height = x.Height
                        },
                        UsrLog = new UsrLogModel
                        {
                            UsrCreated = x.UsrCreated == null ? null : new GuidNom
                            {
                                Guid = (Guid)x.UsrCreated,
                                Nom = x.UsrCreatedNickname ?? x.UsrCreatedEmailAddress
                            },
                            UsrLastEdited = x.UsrLastEdited == null ? null : new GuidNom
                            {
                                Guid = (Guid)x.UsrLastEdited,
                                Nom = x.UsrLastEditedNickname ?? x.UsrLastEditedEmailAddress
                            },
                            FchCreated = x.FchCreated,
                            FchLastEdited = x.FchLastEdited
                        }

                    }).OrderByDescending(x=>x.Fch).ToList();
                return retval;
            }
        }
    }
}
