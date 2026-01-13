using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class ProductCategory : Minifiable
    {
        public enum Cods
        {
            Guid,
            Brand,
            Nom,
            Codi,
            IsBundle,
            Obsoleto
        }

        public static Dictionary<string, Object> Factory(DTOProductCategory value)
        {
            ProductCategory retval = new ProductCategory();
            retval.Add(Cods.Guid, value.Guid.ToString());
            retval.Add(Cods.Brand, BaseGuid.Factory(value.Brand));
            retval.Add(Cods.Nom, LangText.Factory(value.Nom));
            retval.Add(Cods.Codi, value.Codi);
            if (value.IsBundle)
                retval.Add(Cods.IsBundle, 1);
            if (value.obsoleto)
                retval.Add(Cods.Obsoleto, 1);
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }


        public static DTOProductCategory Expand(Object jObject)
        {
            DTOProductCategory retval = new DTOProductCategory();

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
                else if (cod == Cods.Codi)
                    retval.Codi = (DTOProductCategory.Codis)ParseInt(x);
                else if (cod == Cods.IsBundle)
                    retval.IsBundle =ParseBool(x);
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
