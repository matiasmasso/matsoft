using Api.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class TpvService
    {

        public static DTO.Integracions.Redsys.Tpv? Find(MaxiContext db, DTO.Integracions.Redsys.Tpv.Ids id)
        {
            var value = DTO.Integracions.Redsys.Tpv.Wellknown(id)!;
            return Find(db, value.Guid);
        }
        public static DTO.Integracions.Redsys.Tpv? Find(MaxiContext db, Guid guid)
        {
            return db.Tpvs
                        .AsNoTracking()
                .Where(x => x.Guid == guid)
                .Select(x => new DTO.Integracions.Redsys.Tpv(x.Guid)
                {
                    Emp = (DTO.EmpModel.EmpIds)x.Emp,
                    Environment = (DTO.Integracions.Redsys.Common.Environments)x.Environment,
                    GatewayUrlDevelopment = x.GatewayUrlDevelopment,
                    GatewayUrlProduction = x.GatewayUrlProduction,
                    PrivateKeyDevelopment = x.PrivateKeyDevelopment,
                    PrivateKeyProduction = x.PrivateKeyProduction,
                    SignatureVersion = x.SignatureVersion,
                    MerchantCode = x.MerchantCode,
                    MerchantTerminal = x.MerchantTerminal,
                    MerchantUrl = x.MerchantUrl,
                    UrlOk = x.UrlOk,
                    UrlKo = x.UrlKo,
                    Banc = x.Banc == null ? null : new BancModel((Guid)x.Banc)
                }).FirstOrDefault();
        }

        public static bool Update(DTO.Integracions.Redsys.Tpv value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.Tpv? entity;
                if (value.IsNew)
                {
                    entity = new Entities.Tpv();
                    db.Tpvs.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.Tpvs.Find(value.Guid);

                if (entity == null) throw new System.Exception("Tpv not found");

                entity.Emp = (int)value.Emp;
                entity.Environment = (int)value.Environment;
                entity.GatewayUrlDevelopment = value.GatewayUrlDevelopment;
                entity.GatewayUrlProduction = value.GatewayUrlProduction;
                entity.PrivateKeyDevelopment = value.PrivateKeyDevelopment;
                entity.PrivateKeyProduction = value.PrivateKeyProduction;
                entity.SignatureVersion = value.SignatureVersion;
                entity.MerchantCode = value.MerchantCode;
                entity.MerchantTerminal = value.MerchantTerminal ?? 1;
                entity.MerchantUrl = value.MerchantUrl;
                entity.UrlOk = value.UrlOk;
                entity.UrlKo = value.UrlKo;
                entity.Banc = value.Banc?.Guid;

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.Tpvs.Remove(db.Tpvs.Single(x => x.Guid.Equals(guid)));
                db.SaveChanges();
            }
            return true;

        }

    }
    public class TpvsService
    {
        public static List<DTO.Integracions.Redsys.Tpv> All(UserModel user)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.Tpvs
                        .AsNoTracking()
                              .Select(x => new DTO.Integracions.Redsys.Tpv(x.Guid)
                              {
                                  Emp = (DTO.EmpModel.EmpIds)x.Emp,
                                  Environment = (DTO.Integracions.Redsys.Common.Environments)x.Environment,
                                  GatewayUrlDevelopment = x.GatewayUrlDevelopment,
                                  GatewayUrlProduction = x.GatewayUrlProduction,
                                  PrivateKeyDevelopment = x.PrivateKeyDevelopment,
                                  PrivateKeyProduction = x.PrivateKeyProduction,
                                  SignatureVersion = x.SignatureVersion,
                                  MerchantCode = x.MerchantCode,
                                  MerchantTerminal = x.MerchantTerminal,
                                  MerchantUrl = x.MerchantUrl,
                                  UrlOk = x.UrlOk,
                                  UrlKo = x.UrlKo,
                                  Banc = x.BancNavigation == null ? null : new BancModel((Guid)x.Banc)
                                  {
                                      Abr = x.BancNavigation.Abr
                                  }
                              }).ToList();
                return retval;
            }
        }
    }
}
