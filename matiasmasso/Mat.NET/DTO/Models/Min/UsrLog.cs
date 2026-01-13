using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class UsrLog : Minifiable
    {
        public enum Cods
        {
            FchCreated,
            FchLastEdited,
            UsrCreated,
            UsrLastEdited
        }

        public static Dictionary<string, Object> Factory(DTOUsrLog value)
        {
            UsrLog retval = new UsrLog();
            if (value.FchLastEdited != DateTime.MinValue)
                retval.Add(Cods.FchLastEdited, SerializeDateTime(value.FchLastEdited));
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }

        public static DTOUsrLog Expand(Object jObject)
        {
            DTOUsrLog retval = new DTOUsrLog();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.FchCreated)
                    retval.FchCreated = ParseFch(x);
                else if (cod == Cods.FchLastEdited)
                    retval.FchLastEdited = ParseFch(x);
            }
            return retval;
        }

    }
}
