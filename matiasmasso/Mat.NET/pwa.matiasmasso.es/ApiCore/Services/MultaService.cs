using Api.Entities;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class MultaService
    {
        public static MultaModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Multa
                        .AsNoTracking()
                        .Where(x => x.Guid == guid)
                        .OrderByDescending(x => x.Fch)
                        .Select(x => new MultaModel(x.Guid)
                        {
                            Fch = x.Fch,
                            Emisor = x.Emisor,
                            Subjecte = x.Subjecte,
                            Vto = x.Vto,
                            Expedient = x.Expedient,
                            Amt = x.Eur,
                            Pagat = x.Pagat
                        })
                        .FirstOrDefault();

                if(retval != null)
                {
                    retval.Docfiles = DocfilesService.FromSrc(db, guid);
                }
                return retval;
            }
        }

        public static bool Update(MultaModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Multum? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Multum();
                    db.Multa.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Multa.Find(value.Guid);

                if (entity == null) throw new System.Exception("Multa not found");

                entity.Fch = (DateOnly)value.Fch!;
                entity.Emisor = value.Emisor;
                entity.Subjecte = value.Subjecte;
                entity.Vto = value.Vto;
                entity.Expedient = value.Expedient;
                entity.Eur = value.Amt ?? 0;
                entity.Pagat = value.Pagat;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Multa.Remove(db.Multa.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }
    }


    public class MultasService
    {
        public static List<MultaModel> GetValues(Guid target)
        {
            using(var db = new Entities.MaxiContext())
            {
                return db.Multa
                        .AsNoTracking()
                        .Where(x => x.Subjecte == target)
                        .OrderByDescending(x=>x.Fch)
                        .Select(x => new MultaModel(x.Guid)
                        {
                            Fch = x.Fch,
                            Emisor = x.Emisor,
                            Subjecte = target,
                            Vto = x.Vto,
                            Expedient = x.Expedient,
                            Amt = x.Eur,
                            Pagat = x.Pagat
                        })
                        .ToList();

            }
        }
    }
}

