using Api.Entities;
using DocumentFormat.OpenXml.Vml.Office;
using DTO;
using DTO.Integracions.Redsys;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace Api.Services
{
    public class TpvLogService
    {


        public static string BookRequest(DTO.Integracions.Redsys.TpvLog value)
        {
            using (var db = new MaxiContext())
            {
                var tpv = TpvService.Find(db, DTO.Integracions.Redsys.Tpv.Ids.Shop4moms);

                string prefix = tpv.Environment == Common.Environments.Development ? "FAKE0" : "9999A";
                var lastDsOrder = db.TpvLogs
                        .AsNoTracking()
                    .Where(x => x.DsOrder.StartsWith(prefix))
                    .OrderByDescending(x => x.DsOrder)
                    .Select(x => x.DsOrder)
                    .FirstOrDefault();

                string retval = Common.NextTpvOrderNum(tpv.Environment, lastDsOrder);
                var entity = new Entities.TpvLog();
                entity.Guid = value.Guid;
                entity.User = value.User!.Guid;
                entity.DsOrder = retval;
                entity.DsAmount = value.Ds_Amount;
                entity.DsCurrency = value.Ds_Currency;
                entity.DsMerchantCode = value.Ds_MerchantCode;
                entity.DsTerminal = value.Ds_Terminal.ToString();
                entity.DsProductDescription = value.Ds_ProductDescription;
                entity.DsTransactionType = value.Ds_TransactionType;
                entity.DsConsumerLanguage = value.Ds_ConsumerLanguage;
                entity.DsMerchantParameters = value.Ds_MerchantParameters;
                entity.DsSignature = value.Ds_Signature;
                entity.Mode = (int)value.Mode;
                entity.Request = value.SrcGuid;
                entity.Titular = value.Titular;
                db.TpvLogs.Add(entity);
                db.SaveChanges();

                return retval;
            }
        }


        public static CcaModel LogCca(MaxiContext db, DTO.Integracions.Redsys.Tpv? tpv, DTO.Integracions.Redsys.TpvLog? tpvLog, ShoppingBasketModel? basket)
        {
            if (tpv == null) throw new System.Exception("null Tpv");
            if (tpvLog == null) throw new System.Exception("null TpvLog");
            if (basket == null) throw new System.Exception("null basket");
            if (basket.User == null) throw new System.Exception("null basket user");

            var ctas = PgcCtasService.GetValues(db);
            var ctaDebit = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.VisasCobradas)!;
            var ctaCredit = ctas.FirstOrDefault(x => x.Cod == PgcCtaModel.Cods.Clients_Anticips)!;
            var consumer = CustomerModel.Wellknown(CustomerModel.Wellknowns.consumidor);

            var cca = CcaModel.Factory(tpv.Emp, basket.User, CcaModel.CcdEnum.VisaCobros);
            cca.Concept = $"CaixaBank Tpv {tpvLog.Ds_Order} {tpvLog.Ds_ProductDescription} {basket.Fullnom()}";
            cca.AddDebit(basket.Cash(), ctaDebit, tpv.Banc?.Guid);
            cca.AddSaldo(ctaCredit, consumer.Guid);
            return cca;
        }

        public static bool LogResponse(MaxiContext db, DTO.Integracions.Redsys.TpvLog value, CcaModel? cca)
        {
            var entity = db.TpvLogs
                .Where(x => x.DsOrder == value.Ds_Order)
                .FirstOrDefault();

            if (entity == null) throw new System.Exception($"Tpv order {value.Ds_Order} not found");

            entity.DsDate = value.Ds_Date;
            entity.DsHour = value.Ds_Hour;
            entity.DsSignature = value.Ds_Signature;
            entity.DsResponse = value.Ds_Response;
            entity.DsMerchantData = value.Ds_MerchantData;
            entity.DsSecurePayment = value.Ds_SecurePayment;
            entity.DsCardCountry = value.Ds_Card_Country;
            entity.DsAuthorisationCode = value.Ds_AuthorisationCode;
            entity.DsCardType = value.Ds_Card_Type;
            entity.DsMerchantParameters = value.Ds_MerchantParameters;
            entity.DsSignatureReceived = value.Ds_SignatureReceived;
            entity.SignatureValidated = value.Ds_Signature == value.Ds_SignatureReceived;
            entity.CcaGuid = cca?.Guid;
            return true;

        }
        public static DTO.Integracions.Redsys.TpvLog? FromTpvOrder(Entities.MaxiContext db, string tpvOrder)
        {
            DTO.Integracions.Redsys.TpvLog? retval = null;
            var guid = db.TpvLogs
                        .AsNoTracking()
                .FirstOrDefault(x => x.DsOrder == tpvOrder)?.Guid;

            if (guid != null) retval = GetValue(db, (Guid)guid);
            return retval;
        }

        public static DTO.Integracions.Redsys.TpvLog? GetValue(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                return GetValue(db, guid);
            }
        }
        public static DTO.Integracions.Redsys.TpvLog? GetValue(MaxiContext db, Guid guid)
        {
            return db.TpvLogs
                        .AsNoTracking()
                .Where(x => x.Guid == guid)
                .Select(x => new DTO.Integracions.Redsys.TpvLog(x.Guid)
                {
                    User = new UserModel((Guid)x.User!),
                    Ds_Order = x.DsOrder,
                    Ds_Amount = x.DsAmount,
                    Ds_Response = x.DsResponse,
                    Ds_Currency = x.DsCurrency!,
                    Ds_MerchantCode = x.DsMerchantCode,
                    Ds_Terminal = Convert.ToInt32(x.DsTerminal),
                    Ds_ProductDescription = x.DsProductDescription,
                    Ds_TransactionType = x.DsTransactionType!,
                    Ds_ConsumerLanguage = x.DsConsumerLanguage,
                    Ds_AuthorisationCode = x.DsAuthorisationCode,
                    Ds_MerchantParameters = x.DsMerchantParameters,
                    Ds_Signature = x.DsSignature,
                    Ds_SignatureReceived = x.DsSignatureReceived,
                    Mode = (Common.Modes)x.Mode!,
                    SrcGuid = x.Request,
                    Titular = x.Titular,
                    FchCreated = x.FchCreated
                })
                .FirstOrDefault();
        }

        public static bool Update(DTO.Integracions.Redsys.TpvLog value)
        {
            using (var db = new Entities.MaxiContext())
            {
                Entities.TpvLog? entity;
                if (value.IsNew)
                {
                    entity = new Entities.TpvLog();
                    db.TpvLogs.Add(entity);
                    entity.Guid = value.Guid;
                }
                else
                    entity = db.TpvLogs.Find(value.Guid);

                if (entity == null) throw new System.Exception("TpvLog not found");

                entity.User = value.User!.Guid;
                entity.DsOrder = value.Ds_Order ?? "";
                entity.DsAmount = value.Ds_Amount;
                entity.DsCurrency = value.Ds_Currency;
                entity.DsMerchantCode = value.Ds_MerchantCode;
                entity.DsTerminal = value.Ds_Terminal.ToString();
                entity.DsProductDescription = value.Ds_ProductDescription;
                entity.DsTransactionType = value.Ds_TransactionType;
                entity.DsConsumerLanguage = value.Ds_ConsumerLanguage;
                entity.Mode = (int)value.Mode;
                entity.Request = value.SrcGuid;
                entity.Titular = value.Titular;

                // Save changes in database
                db.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Guid guid)
        {
            using (var db = new Entities.MaxiContext())
            {
                var entity = db.TpvLogs.FirstOrDefault(x => x.Guid.Equals(guid));
                if (entity != null)
                {
                    db.TpvLogs.Remove(entity);
                    db.SaveChanges();
                }
            }
            return true;
        }

    }
    public class TpvLogsService
    {
        public static List<DTO.Integracions.Redsys.TpvLog> GetValues(string? merchantCode = null)
        {
            using (var db = new Entities.MaxiContext())
            {
                return db.TpvLogs
                        .AsNoTracking()
                    .Where(x => merchantCode == null ? true : x.DsMerchantCode == merchantCode)
                    .OrderByDescending(x => x.FchCreated)
                    .Select(x => new DTO.Integracions.Redsys.TpvLog(x.Guid)
                    {
                        User = new UserModel((Guid)x.User!),
                        Ds_Order = x.DsOrder,
                        Ds_Amount = x.DsAmount,
                        Ds_Response = x.DsResponse,
                        Ds_Currency = x.DsCurrency!,
                        Ds_MerchantCode = x.DsMerchantCode,
                        Ds_Terminal = Convert.ToInt32(x.DsTerminal),
                        Ds_ProductDescription = x.DsProductDescription,
                        Ds_TransactionType = x.DsTransactionType!,
                        Ds_ConsumerLanguage = x.DsConsumerLanguage,
                        Ds_AuthorisationCode = x.DsAuthorisationCode,
                        Mode = (Common.Modes)x.Mode!,
                        SrcGuid = x.Request,
                        Titular = x.Titular,
                        FchCreated = x.FchCreated
                    })
                    .ToList();
            }
        }
    }
}
