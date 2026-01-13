using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class GuidNom : Minifiable
    {
        public enum Cods
        {
            Guid,
            Nom
        }

        public static Dictionary<string, Object> Factory(Guid guid, string nom)
        {
            GuidNom retval = new GuidNom();
            retval.Add(Cods.Guid, guid.ToString());
            retval.Add(Cods.Nom, nom);
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }


        public static Base.GuidNom Expand(Object jObject)
        {
            Base.GuidNom retval = new Base.GuidNom();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                else if (cod == Cods.Nom)
                    retval.Nom = x.Value.ToString();
            }
            return retval;
        }
    }
}
