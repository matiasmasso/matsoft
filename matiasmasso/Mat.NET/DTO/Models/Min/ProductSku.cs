using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class ProductSku : Minifiable
    {
        public enum Cods
        {
            Guid,
            Category,
            Id,
            RefProveidor,
            Ean13,
            Nom,
            NomLlarg,
            NomProveidor,
            LastProduction,
            ImageExists,
            Obsoleto,
            Moq,
            ObsoletoConfirmed,
            NoPro,
            NoWeb,
            NoEcom
        }

        public static Dictionary<string, Object> Factory(DTOProductSku value)
        {
            ProductSku retval = new ProductSku();
            retval.Add(Cods.Guid, value.Guid.ToString());
            retval.Add(Cods.Id, value.Id.ToString());
            if (!string.IsNullOrEmpty(value.RefProveidor))
                retval.Add(Cods.RefProveidor, value.RefProveidor.ToString());
            if (value.Ean13 != null)
                retval.Add(Cods.Ean13, Ean.Factory(value.Ean13));
            retval.Add(Cods.Category, BaseGuid.Factory(value.Category));
            retval.Add(Cods.Nom, LangText.Factory(value.Nom));
            retval.Add(Cods.NomLlarg, LangText.Factory(value.NomLlarg));
            if (!string.IsNullOrEmpty(value.NomProveidor))
                retval.Add(Cods.NomProveidor, value.NomProveidor);
            if (value.LastProduction)
                retval.Add(Cods.LastProduction, 1);
            if (value.ImageExists)
                retval.Add(Cods.ImageExists, 1);
            if (value.NoPro)
                retval.Add(Cods.NoPro, 1);
            if (value.NoWeb)
                retval.Add(Cods.NoWeb, 1);
            if (value.NoEcom)
                retval.Add(Cods.NoEcom, 1);
            if (value.obsoleto)
                retval.Add(Cods.Obsoleto, 1);
            if (value.ObsoletoConfirmed != DateTime.MinValue)
                retval.Add(Cods.ObsoletoConfirmed, Minifiable.SerializeDateTime(value.ObsoletoConfirmed));
            if (value.ForzarInnerPack & value.innerPackOrInherited() > 1)
                retval.Add(Cods.Moq, value.innerPackOrInherited());
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }

        public static DTOProductSku Expand(Object jObject)
        {
            DTOProductSku retval = new DTOProductSku();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                else if (cod == Cods.Id)
                    retval.Id = ParseInt(x);
                else if (cod == Cods.RefProveidor)
                    retval.RefProveidor = x.Value.ToString();
                else if (cod == Cods.Ean13)
                    retval.Ean13 = Ean.Expand((JObject)x.Value);
                else if (cod == Cods.Category)
                    retval.Category = ProductCategory.Expand((JObject)x.Value);
                else if (cod == Cods.Nom)
                    retval.Nom = LangText.Expand((Object)x.Value);
                else if (cod == Cods.NomLlarg)
                    retval.NomLlarg = LangText.Expand((JObject)x.Value);
                else if (cod == Cods.NomProveidor)
                    retval.NomProveidor = x.Value.ToString();
                else if (cod == Cods.LastProduction)
                    retval.LastProduction = ParseBool(x);
                else if (cod == Cods.ImageExists)
                    retval.ImageExists = ParseBool(x);
                else if (cod == Cods.Obsoleto)
                    retval.obsoleto = ParseBool(x);
                else if (cod == Cods.NoPro)
                    retval.NoPro = ParseBool(x);
                else if (cod == Cods.NoEcom)
                    retval.NoEcom = ParseBool(x);
                else if (cod == Cods.NoWeb)
                    retval.NoWeb = ParseBool(x);
                else if (cod == Cods.ObsoletoConfirmed)
                    retval.ObsoletoConfirmed = Minifiable.ParseFch(x);
                else if (cod == Cods.Moq)
                {
                    retval.InnerPack = ParseInt(x);
                    retval.ForzarInnerPack = true;

                }
            }
            return retval;
        }

        public bool Obsoleto()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);
            bool retval = false;
            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Obsoleto)
                    retval = ParseBool(x);
            }
            return retval;
        }

    }
}
