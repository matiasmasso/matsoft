using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Services
{
    public class AeatService
    {

        public static AeatModel? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.AeatMods
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select(x => new AeatModel(x.Guid)
                    {
                        Id = x.Mod,
                        Dsc = x.Dsc
                    }).FirstOrDefault();
            }
        }

        public static AeatModel.Item? GetItem(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.Aeats
                    .AsNoTracking()
                    .Where(x => x.Guid == guid)
                    .Select (x => new AeatModel.Item(x.Guid) 
                    {
                        Emp = (EmpModel.EmpIds)x.Emp,
                        Parent = x.Model,
                        Fch = x.Fch,
                        Docfile = x.Hash == null ? null : new DocfileModel(x.Hash)
                    }).FirstOrDefault();
            }
        }

        public static bool Update(AeatModel value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.AeatMod? entity;
                if (value.IsNew)
                {
                    entity = new Entities.AeatMod();
                    db.AeatMods.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.AeatMods.Find(value.Guid);

                if (entity == null) throw new Exception("Aeat not found");

                entity.Mod = value.Id ?? "";
                entity.Dsc = value.Dsc;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Update(AeatModel.Item value)
        {
            using (var db = new Entities.MaxiContext())
            {
                if (value.Docfile?.Hash != null) DocfileService.Update(db, value.Docfile);

                Entities.Aeat? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Aeat();
                    db.Aeats.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Aeats.Find(value.Guid);

                if (entity == null) throw new Exception("Aeat not found");

                entity.Model = (Guid)value.Parent!;
                entity.Emp = (int)value.Emp;
                entity.Fch = value.Fch;
                entity.Yea = value.Fch.Year;
                entity.Hash = value.Docfile?.Hash;

                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.AeatMods.Remove(db.AeatMods.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }

        public static bool DeleteItem(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Aeats.Remove(db.Aeats.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }


    }
    public class AeatsService
    {
        public static List<AeatModel> GetValues()
        {
            using (var db = new Entities.MaxiContext())
            {

                return db.AeatMods
                    .AsNoTracking()
                    .Select(x => new AeatModel(x.Guid)
                    {
                        Id = x.Mod,
                        Dsc = x.Dsc,
                        Items = x.Aeats.Select(y => new AeatModel.Item(y.Guid)
                        {
                            Emp = (EmpModel.EmpIds)y.Emp,
                            Fch = y.Fch,
                            Docfile = y.Hash == null ? null : new DocfileModel(y.Hash)
                        }).ToList()
                    }).OrderBy(x => x.Id)
                    .ToList();

            }
        }
        public static List<AeatModel> GetValuesWithItems()
        {
            using (var db = new Entities.MaxiContext())
            {

                return db.AeatMods
                    .AsNoTracking()
                    .Select(x => new AeatModel(x.Guid)
                    {
                        Id = x.Mod,
                        Dsc = x.Dsc,
                        Items = x.Aeats.Select(y => new AeatModel.Item(y.Guid)
                        {
                            Emp = (EmpModel.EmpIds)y.Emp,
                            Parent = x.Guid,
                            Fch = y.Fch,
                            Docfile = y.Hash == null ? null : new DocfileModel(y.Hash)
                        }).OrderBy(x=>x.Fch)
                        .ToList()
                    })
                    .OrderBy(x => x.Id)
                    .ToList();
            }
        }
    }
}
