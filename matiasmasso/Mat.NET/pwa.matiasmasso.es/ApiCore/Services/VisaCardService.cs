using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class VisaCardService
    {
        public static VisaCardModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VisaCards
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new VisaCardModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Emisor = x.Emisor,
                        Banc = x.Banc,
                        Titular = x.Titular,
                        Nom = x.Alias,
                        Digits = x.Digits,
                        Caduca = x.Caduca,
                        FchTo = x.FchCanceled
                    }).FirstOrDefault();

            }
        }

        public static bool Update(VisaCardModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.VisaCard? entity;
                if (value.IsNew)
                {
                    entity = new Entities.VisaCard();
                    db.VisaCards.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.VisaCards.Find(value.Guid);

                if (entity == null) throw new System.Exception("VisaCard not found");

                entity.Emp = (int)value.Emp;
                entity.Emisor = (Guid)value.Emisor!;
                entity.Banc = (Guid)value.Banc!;
                entity.Titular = (Guid)value.Titular!;
                entity.Alias = value.Nom ?? "";
                entity.Digits = value.Digits ?? "";
                entity.Caduca = value.Caduca ?? "";
                entity.FchCanceled = value.FchTo;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.VisaCards.Remove(db.VisaCards.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class VisaCardsService
    {
        public static List<VisaCardModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VisaCards
                    .AsNoTracking()
                    .Select(x => new VisaCardModel(x.Guid)
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Emisor = x.Emisor,
                        Banc = x.Banc,
                        Titular = x.Titular,
                        Nom = x.Alias,
                        Digits = x.Digits,
                        Caduca = x.Caduca,
                        FchTo = x.FchCanceled
                    }).ToList();
            }
        }
    }
}
