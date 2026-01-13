using DocumentFormat.OpenXml.Presentation;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class ClaimService
    {

        public static ClaimModel? Find(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VwClaims
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new ClaimModel(x.Guid)
                    {
                        Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.ClaimNom)
                        {
                            Esp = x.NomEsp,
                            Cat = x.NomCat,
                            Eng = x.NomEng,
                            Por = x.NomPor
                        },
                        Description = new LangTextModel(x.Guid, LangTextModel.Srcs.ClaimDescription)
                        {
                            Esp = x.DscEsp,
                            Cat = x.DscCat,
                            Eng = x.DscEng,
                            Por = x.DscPor
                        },
                        Cod = (ClaimModel.Cods)x.Cod
                    }).FirstOrDefault();
            }
        }

        public static bool Update(ClaimModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Claim? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Claim();
                    db.Claims.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Claims.Find(value.Guid);

                if (entity == null) throw new Exception("Claim not found");

                entity.Cod = (int)value.Cod;

                LangTextService.Update(db, value.Nom);
                LangTextService.Update(db, value.Description);

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                LangTextService.Delete(db, guid, LangTextModel.Srcs.ClaimNom);
                LangTextService.Delete(db, guid, LangTextModel.Srcs.ClaimDescription);
                db.Claims.Remove(db.Claims.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }

    }
    public class ClaimsService
    {
        public static List<ClaimModel> All()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.VwClaims
                    .AsNoTracking()
                    .Select(x => new ClaimModel(x.Guid)
                    {
                        Nom = new LangTextModel(x.Guid, LangTextModel.Srcs.ClaimNom)
                        {
                            Esp = x.NomEsp,
                            Cat = x.NomCat,
                            Eng = x.NomEng,
                            Por = x.NomPor
                        },
                        Description = new LangTextModel(x.Guid, LangTextModel.Srcs.ClaimDescription)
                        {
                            Esp = x.DscEsp,
                            Cat = x.DscCat,
                            Eng = x.DscEng,
                            Por = x.DscPor
                        },
                        Cod = (ClaimModel.Cods)x.Cod
                    }).ToList();
            }
        }
    }
}
