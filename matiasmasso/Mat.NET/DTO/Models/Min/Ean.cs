using Newtonsoft.Json.Linq;

namespace DTO.Models.Min
{
    public class Ean : Minifiable
    {
        public static Ean Factory(DTOEan value)
        {
            Ean retval = new Ean();
            retval.Add("0", value.Value.ToString());
            return retval;
        }

        public static DTOEan Expand(JObject jObject)
        {
            DTOEan retval = null;
            JToken value = null;
            if (jObject.TryGetValue("0", out value))
            {
                retval = new DTOEan(value.ToString());
            }
            return retval;
        }
    }

}