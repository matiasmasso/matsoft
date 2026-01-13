using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class ProductDept : Minifiable
    {
        public enum Cods
        {
            Guid,
            Brand,
            Nom,
            Obsoleto,
            UsrLog
        }

        public static Dictionary<string, Object> Factory(DTODept value)
        {
            ProductDept retval = new ProductDept();
            retval.Add(Cods.Guid, value.Guid.ToString());
            retval.Add(Cods.Brand, BaseGuid.Factory(value.Brand));
            retval.Add(Cods.Nom, (Object)LangText.Factory(value.Nom));
            if (value.obsoleto)
                retval.Add(Cods.Obsoleto, 1);
            if (value.UsrLog != null)
                retval.Add(Cods.UsrLog, (Object)UsrLog.Factory(value.UsrLog));
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }

        public static DTODept Expand(Object jObject)
        {
            DTODept retval = new DTODept();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                else if (cod == Cods.Brand)
                    retval.Brand = ProductBrand.Expand(x.Value);
                else if (cod == Cods.Nom)
                    retval.Nom = LangText.Expand(x.Value);
                else if (cod == Cods.UsrLog)
                    retval.UsrLog = UsrLog.Expand(x.Value);
                else if (cod == Cods.Obsoleto)
                    retval.obsoleto = ParseBool(x);
            }
            return retval;
        }

    }
}
