using Newtonsoft.Json.Linq;
using System;

namespace DTO
{
    public class DTOCliProductBlocked
    {
        public DTOContact contact { get; set; } = null/* TODO Change to default(_) if this is not a reference type */;
        public Object product { get; set; }
        public DistModels distModel { get; set; }
        public Codis cod { get; set; }
        public string zip { get; set; }
        public string obs { get; set; }

        public DateTime lastFch { get; set; }
        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public enum DistModels
        {
            notSet,
            open // lliure
    ,
            closed // llista tancada de distribuidors oficials
        }

        public enum Codis
        {
            standard,
            exclusiva,
            noAplicable,
            exclos,
            distribuidorOficial,
            altresEnExclusiva
        }

        public static DTOCliProductBlocked Factory(DTOContact oContact, DTOProduct oProduct = null/* TODO Change to default(_) if this is not a reference type */)
        {
            DTOCliProductBlocked retval = new DTOCliProductBlocked();
            {
                var withBlock = retval;
                withBlock.contact = oContact;
                withBlock.product = oProduct;
            }
            return retval;
        }

        public void RestoreObjects()
        {
            this.product = DTOProduct.fromJObject(this.product as JObject);
        }
    }
}
