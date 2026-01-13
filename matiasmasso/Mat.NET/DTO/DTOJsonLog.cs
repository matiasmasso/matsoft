using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DTO
{
    public class DTOJsonLog : DTOBaseGuid
    {
        public DateTime FchCreated { get; set; }
        public string Json { get; set; }
        public DTOJsonSchema Schema { get; set; }
        public int Length { get; set; }

        public Results Result { get; set; }
        public DTOBaseGuid ResultTarget { get; set; }

        public enum Results
        {
            NotSet,
            Success,
            Failure
        }



        public DTOJsonLog() : base()
        {

        }
        public DTOJsonLog(Guid guid) : base(guid)
        {

        }

        public static DTOJsonLog Factory(DTOJsonSchema schema, string json)
        {
            DTOJsonLog retval = new DTOJsonLog();
            retval.Schema = schema;
            retval.Json = json;
            return retval;
        }

        public string Formatted(Boolean indented)
        {
            JObject jObject = JObject.Parse(Json);

            string retval = JsonConvert.SerializeObject(jObject, indented ? Formatting.Indented : Formatting.None);
            return retval;
        }

    }
}
