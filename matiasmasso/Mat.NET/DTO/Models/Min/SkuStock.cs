using System;
using System.Collections.Generic;

namespace DTO.Models.Min
{
    public class SkuStock : Minifiable
    {
        public enum Cods
        {
            Guid,
            Stock,
            Clients,
            ClientsAlPot,
            ClientsEnProgramacio,
            ClientsBlockStock,
            Proveidors
        }

        public static Dictionary<string, Object> Factory(Models.SkuStock value)
        {
            SkuStock retval = new SkuStock();
            retval.Add(Cods.Guid, value.Guid.ToString());
            if (value.Stock != 0)
                retval.Add(Cods.Stock, value.Stock.ToString());
            if (value.Clients != 0)
                retval.Add(Cods.Clients, value.Clients.ToString());
            if (value.ClientsAlPot != 0)
                retval.Add(Cods.ClientsAlPot, value.ClientsAlPot.ToString());
            if (value.ClientsEnProgramacio != 0)
                retval.Add(Cods.ClientsEnProgramacio, value.ClientsEnProgramacio.ToString());
            if (value.ClientsBlockStock != 0)
                retval.Add(Cods.ClientsBlockStock, value.ClientsBlockStock.ToString());
            if (value.Proveidors != 0)
                retval.Add(Cods.Proveidors, value.Proveidors.ToString());
            return retval;
        }

        public void Add(Cods cod, Object value)
        {
            base.Add(((int)cod).ToString(), value);
        }


        public static Models.SkuStock Expand(Object jObject)
        {
            Models.SkuStock retval = new Models.SkuStock();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jObject);
            Dictionary<string, Object> baseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

            foreach (KeyValuePair<string, Object> x in baseObj)
            {
                Cods cod = (Cods)x.Key.toInteger();
                if (cod == Cods.Guid)
                    retval.Guid = new Guid(x.Value.ToString());
                else if (cod == Cods.Stock)
                    retval.Stock = ParseInt(x);
                else if (cod == Cods.Clients)
                    retval.Clients = ParseInt(x);
                else if (cod == Cods.ClientsAlPot)
                    retval.ClientsAlPot = ParseInt(x);
                else if (cod == Cods.ClientsEnProgramacio)
                    retval.ClientsEnProgramacio = ParseInt(x);
                else if (cod == Cods.ClientsBlockStock)
                    retval.ClientsBlockStock = ParseInt(x);
                else if (cod == Cods.Proveidors)
                    retval.Proveidors = ParseInt(x);


            }
            return retval;
        }

    }
}
