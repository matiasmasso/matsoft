using DocumentFormat.OpenXml.Math;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class EmpService
    {

        public static EmpModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emps
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new EmpModel(x.Guid)
                    {
                        Id = (EmpModel.EmpIds)x.Emp1,
                        Ord = x.Ord,
                        Nom = x.Nom,
                        Abr = x.Abr,
                        Nif = x.Nif,
                        Org = x.Org,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo,
                        Cert = x.CertData == null ? null : new CertModel
                        {
                            Password = x.CertPwd,
                            FchTo = x.CertFchTo,
                            Data = new Media((Media.MimeCods)x.CertMime, null),
                            Image = new Media((Media.MimeCods)x.CertImageMime, null)
                        }
                    }).FirstOrDefault();
            }
        }


        public static EmpModel? FromId(EmpModel.EmpIds id)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emps
                    .AsNoTracking()
                    .Where(x => x.Emp1 == (int)id)
                    .Select(x => new EmpModel(x.Guid)
                    {
                        Id = (EmpModel.EmpIds)x.Emp1,
                        Ord = x.Ord,
                        Nom = x.Nom,
                        Abr = x.Abr,
                        Nif = x.Nif,
                        Org = x.Org,
                        FchFrom = x.FchFrom,
                        FchTo= x.FchTo,
                        Cert = x.CertData == null ? null : new CertModel
                        {
                            Password = x.CertPwd,
                            FchTo = x.CertFchTo,
                            Data = new Media((Media.MimeCods)x.CertMime, null),
                            Image = new Media((Media.MimeCods)x.CertImageMime, null)
                        }

                    }).FirstOrDefault();
            }
        }

        public static Media? CertData(int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emps
                    .AsNoTracking()
                    .Where(x => x.Emp1 == emp)
                    .Select(x => new Media((Media.MimeCods)x.CertMime, x.CertData))
                    .FirstOrDefault();
            }
        }

        public static Media? CertImage(int emp)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emps
                    .AsNoTracking()
                    .Where(x => x.Emp1 == emp)
                    .Select(x => new Media((Media.MimeCods)x.CertImageMime, x.CertImage))
                    .FirstOrDefault();
            }
        }

        public static bool Update(EmpModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Emp? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Emp();
                    db.Emps.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Emps.FirstOrDefault(x => x.Guid == value.Guid);

                if (entity == null) throw new Exception("Emp not found");

                entity.Emp1 = (int)value.Id;
                entity.Ord = value.Ord;
                entity.FchFrom = value.FchFrom;
                entity.FchTo = value.FchTo;
                entity.Nom = value.Nom ?? "";
                entity.Abr = value.Abr ?? "";
                entity.Nif = value.Nif;
                entity.Org = value.Org;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Emps.Remove(db.Emps.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class EmpsService
    {
        public static List<EmpModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Emps
                    .AsNoTracking()
                    .Select(x => new EmpModel(x.Guid)
                    {
                        Id =(EmpModel.EmpIds) x.Emp1,
                        Ord = x.Ord,
                        Nom = x.Nom,
                        Abr = x.Abr,
                        Nif = x.Nif,
                        Org = x.Org,
                        FchFrom = x.FchFrom,
                        FchTo = x.FchTo
                        //Cert = x.CertData == null ? null : new CertModel
                        //{
                        //    Password = x.CertPwd,
                        //    FchTo = x.CertFchTo,
                        //    Data = new Media((Media.MimeCods)x.CertMime, null),
                        //    Image = new Media((Media.MimeCods)x.CertImageMime, null)
                        //}
                    })
                    .OrderBy(x=>x.FchTo == null ? 0:1)
                    .ThenBy(x=>x.Ord ?? 'z')
                    .ThenBy(x=>x.Abr ?? x.Nom)
                    .ToList();
            }
        }
    }
}
