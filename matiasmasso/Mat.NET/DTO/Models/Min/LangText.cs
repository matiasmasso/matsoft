using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class LangText : Minifiable
    {
        public enum Cods
        {
            Esp,
            Cat,
            Eng,
            Por
        }

        public static LangText Factory(DTOLangText langText)
        {
            LangText retval = new LangText();
            retval.Add(Cods.Esp, langText.Esp);
            if (!string.IsNullOrEmpty(langText.Cat) & langText.Cat != langText.Esp)
                retval.Add(Cods.Cat, langText.Cat);
            if (!string.IsNullOrEmpty(langText.Eng) & langText.Eng != langText.Esp)
                retval.Add(Cods.Eng, langText.Eng);
            if (!string.IsNullOrEmpty(langText.Por) & langText.Por != langText.Esp)
                retval.Add(Cods.Por, langText.Por);
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }

        public static DTOLangText Expand(object jObject)
        {
            DTOLangText retval = new DTOLangText();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<int, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, Object>>(json);
            foreach (KeyValuePair<int, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key;
                if (cod == Cods.Esp)
                    retval.Esp = x.Value.ToString();
                else if (cod == Cods.Cat)
                    retval.Cat = x.Value.ToString();
                else if (cod == Cods.Eng)
                    retval.Eng = x.Value.ToString();
                else if (cod == Cods.Por)
                    retval.Por = x.Value.ToString();
            }
            return retval;
        }
    }
}
