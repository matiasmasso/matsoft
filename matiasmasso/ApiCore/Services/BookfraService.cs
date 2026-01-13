using Api.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using DTO;
using Microsoft.EntityFrameworkCore;
using static DTO.LloguerModel;
//using Microsoft.Data.Extensions;

namespace Api.Services
{
    public class BookfraService
    {

        public static BookfraModel GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var retval = db.BookFras
                .Include(x => x.Cca)
                .Include(x => x.Contact)
                    .AsNoTracking()
                    .Where(x => x.CcaGuid == guid)
                        .Select(BookfrasService.BookFraMapper.Map)
                    .FirstOrDefault(); ;

                return retval;
            }
        }
        public static bool Update(BookfraModel value)
        {
            using var db = new Entities.MaxiContext();
            Update(db, value);

            db.SaveChanges();
            return true;
        }

        public static void Update(Entities.MaxiContext db, BookfraModel value)
        {
            var entity = db.BookFras.FirstOrDefault(x => x.CcaGuid == value.CcaGuid);

            if (entity == null)
            {
                entity = new Entities.BookFra
                {
                    CcaGuid = value.CcaGuid!.Value
                };
                db.BookFras.Add(entity);
            } else
            {
                entity.BaseExenta = null;
                entity.BaseSujeta = null;
                entity.TipoIva = null;
                entity.QuotaIva = null;
                entity.BaseSujeta1 = null;
                entity.TipoIva1 = null;
                entity.QuotaIva1 = null;
                entity.BaseSujeta2 = null;
                entity.TipoIva2 = null;
                entity.QuotaIva2 = null;
                entity.BaseIrpf = null;
                entity.TipoIrpf = null;
                entity.Irpf = 0;
            }

            entity.ContactGuid = value.ContactGuid;
            entity.CtaGuid = value.CtaGuid;
            entity.FraNum = value.FraNum ?? string.Empty;

            var baseExenta = value.Ivas
                .Where(x => x.IsExenta())
                .Sum(x => x.Base);

            if (baseExenta != null && baseExenta != 0)
                entity.BaseExenta = baseExenta;

            var sujetas = value.Ivas
                .Where(x => x.IsSujeta())
                .ToList();

            if (sujetas.Count > 0)
            {
                entity.BaseSujeta = sujetas[0].Base ?? 0;
                entity.TipoIva = sujetas[0].Tipo ?? 0;
                entity.QuotaIva = sujetas[0].Quota ?? 0;
            }
            if (sujetas.Count > 1)
            {
                entity.BaseSujeta1 = sujetas[1].Base ?? 0;
                entity.TipoIva1 = sujetas[1].Tipo ?? 0;
                entity.QuotaIva1 = sujetas[1].Quota ?? 0;
            }
            if (sujetas.Count > 2)
            {
                entity.BaseSujeta2 = sujetas[2].Base ?? 0;
                entity.TipoIva2 = sujetas[2].Tipo ?? 0;
                entity.QuotaIva2 = sujetas[2].Quota ?? 0;
            }
            if(value.Irpf !=null && value.Irpf.Quota != 0)
            {
                entity.BaseIrpf = value.Irpf.Base;
                entity.TipoIrpf = value.Irpf.Tipo;
                entity.Irpf = value.Irpf.Quota ?? 0;
            }
        }

             public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.BookFras.Remove(db.BookFras.Single(x => x.CcaGuid.Equals(guid)));
                CcaService.Delete(db, guid);
                db.SaveChanges();
            }
            return true;
        }
    }

    public class BookfrasService
    {

        public static List<CcaModel> MissingValues(int emp, int year)
        {
            using var db = new Entities.MaxiContext();

            var targetGuid = Guid.Parse("db082ea9-d9da-4c91-817b-d65f2ae5e4f4");

            return db.Ccbs
                .AsNoTracking()
                .Where(x =>
                    x.Cca.Emp == emp &&
                    x.Cca.Yea == year &&
                    x.Cca.BookFra == null &&
                    x.CtaGuid == targetGuid &&
                    x.Cca.Txt != null &&
                    !x.Cca.Txt.Contains("Mod.303"))
                .OrderBy(x => x.Cca.Fch)
                .Select(x => new CcaModel
                {
                    Guid = x.CcaGuid,
                    Fch = x.Cca.Fch,
                    Emp = (EmpModel.EmpIds)emp,
                    Concept = x.Cca.Txt,
                    Docfile = x.Cca.Hash == null ? null : new DocfileModel(x.Cca.Hash)
                })
                .ToList();
        }



        public static List<BookfraModel> GetValues(int emp, int year)
        {
            using var db = new Entities.MaxiContext();

            var retval = db.BookFras
                .AsNoTracking()
                .Include(x => x.Cca)
                .Include(x => x.Contact)
                .Where(x => x.Cca.Emp == emp && x.Cca.Yea == year)
                .OrderBy(x => x.Cca.Fch)
                .Select(BookFraMapper.Map)
                .ToList();
            return retval;
        }



        public static class BookFraMapper
        {
            public static BookfraModel Map(BookFra x)
            {
                var retval= new BookfraModel
                {
                    
                    CcaGuid = x.CcaGuid,
                    CtaGuid = x.CtaGuid,
                    ContactGuid = x.ContactGuid,
                    FraNum = x.Cca.BookFra?.FraNum,
                    FraFch = x.Cca.Fch,
                    RaoSocial = x.Contact?.FullNom,
                    NIF = x.Contact?.Nif,
                    Ivas = x.Cca.BookFra != null
                        ? BuildIvas(x.Cca.BookFra)
                        : new List<BaseQuotaModel>(),
                    Irpf = x.TipoIrpf == null ? null : new BaseQuotaModel
                    {
                        Base = x.BaseIrpf,
                        Tipo = x.TipoIrpf,
                        Quota = x.Irpf
                    }
            };
                return retval;
            }

            private static List<BaseQuotaModel> BuildIvas(BookFra bf)
            {
                var list = new List<BaseQuotaModel>();

                void Add(decimal? @base, decimal? tipo = null, decimal? quota = null)
                {
                    if (@base.HasValue)
                    {
                        list.Add(new BaseQuotaModel
                        {
                            Base = @base.Value,
                            Tipo = tipo,
                            Quota = quota
                        });
                    }
                }

                Add(bf.BaseExenta);
                Add(bf.BaseSujeta, bf.TipoIva, bf.QuotaIva);
                Add(bf.BaseSujeta1, bf.TipoIva1, bf.QuotaIva1);
                Add(bf.BaseSujeta2, bf.TipoIva2, bf.QuotaIva2);
                return list;
            }
        }


        //public static List<BookfraModel> GetValues(int emp, int year)
        //{
        //    using (var db = new Entities.MaxiContext())
        //    {
        //        var retval = db.BookFras
        //            .AsNoTracking()
        //            .Where(x => x.Cca.Emp == emp && x.Cca.Yea == year )
        //            .OrderBy(x => x.Cca.Fch)
        //            .Select(x => new BookfraModel
        //            {
        //                CtaGuid = x.CtaGuid,
        //                ContactGuid = x.ContactGuid,
        //                FraNum = x.Cca.BookFra != null ? x.Cca.BookFra.FraNum : null,
        //                FraFch = x.Cca.Fch,
        //                RaoSocial = x.Contact != null ? x.Contact.FullNom : null,
        //                NIF = x.Contact != null ? x.Contact.Nif : null,
        //                Ivas = x.Cca != null && x.Cca.BookFra != null
        //                    ? new List<BaseQuotaModel>
        //                    {
        //                                new BaseQuotaModel
        //                                {
        //                                    Base = x.Cca.BookFra.BaseExenta
        //                                },
        //                                new BaseQuotaModel
        //                                {
        //                                    Base = x.Cca.BookFra.BaseSujeta1,
        //                                    Tipo = x.Cca.BookFra.TipoIva1,
        //                                    Quota = x.Cca.BookFra.QuotaIva1
        //                                },
        //                                new BaseQuotaModel
        //                                {
        //                                    Base = x.Cca.BookFra.BaseSujeta2,
        //                                    Tipo = x.Cca.BookFra.TipoIva2,
        //                                    Quota = x.Cca.BookFra.QuotaIva2
        //                                }
        //                    }
        //                    : new List<BaseQuotaModel>()
        //            }).ToList();

        //        return retval;
        //    }
        //}


    }
}
