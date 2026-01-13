using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class ProductBrand : Minifiable
    {
        public enum Cods
        {
            Guid,
            Nom,
            Obsoleto,
            Proveidor
        }

        public static Dictionary<string, Object> Factory(DTOProductBrand value)
        {
            ProductBrand retval = new ProductBrand();
            retval.Add(Cods.Guid, value.Guid.ToString());
            retval.Add(Cods.Nom, (Object)LangText.Factory(value.Nom));
            if (value.Proveidor != null)
                retval.Add(Cods.Proveidor, (Object)Models.Min.GuidNom.Factory(value.Proveidor.Guid, value.Proveidor.FullNom));
            if (value.obsoleto)
                retval.Add(Cods.Obsoleto, 1);
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }


        public static DTOProductBrand Expand(Object jObject)
        {
            DTOProductBrand retval = new DTOProductBrand();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                else if (cod == Cods.Nom)
                    retval.Nom = LangText.Expand(x.Value);
                else if (cod == Cods.Proveidor)
                {
                    Models.Base.GuidNom guidNom = Min.GuidNom.Expand(x.Value);
                    retval.Proveidor = new DTOProveidor(guidNom.Guid);
                    retval.Proveidor.FullNom = guidNom.Nom;
                }
                else if (cod == Cods.Obsoleto)
                    retval.obsoleto = ParseBool(x);
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
