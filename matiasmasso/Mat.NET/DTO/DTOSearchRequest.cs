using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOSearchRequest : DTOBaseGuid
    {
        public string SearchKey { get; set; }
        public DTOUser User { get; set; }
        public DTOLang Lang { get; set; }
        public DTOEmp Emp { get; set; }
        public DateTime Fch { get; set; }
        public DTOContact Contact { get; set; }
        public List<Result> Results { get; set; }

        public DTOSearchRequest() : base()
        {
        }

        public DTOSearchRequest(Guid oGuid) : base(oGuid)
        {
        }

        public static DTOSearchRequest Factory(DTOEmp oEmp, DTOUser oUser, DTOLang oLang, string sSearchKey)
        {
            DTOSearchRequest retval = new DTOSearchRequest();
            {
                var withBlock = retval;
                withBlock.Emp = oEmp;
                withBlock.Fch = DTO.GlobalVariables.Now();
                withBlock.User = oUser;
                withBlock.Lang = oLang;
                withBlock.SearchKey = sSearchKey;
            }

            return retval;
        }


        public static string FoundCaption(DTOSearchRequest oRequest)
        {
            string text = "";

            DTOLang oLang = oRequest.Lang;
            switch (oRequest.Results.Count)
            {
                case 0:
                    {
                        text = oLang.Tradueix("No se ha encontrado ningún resultado por '{0}'", "No s'ha trobat cap resultat per '{0}'", "No results could be found by '{0}'", "Não se encontrou nenhum resultado para/por '{0}'");
                        break;
                    }

                case 1:
                    {
                        text = oLang.Tradueix("Se ha encontrado un resultado por '{0}'", "S'ha trobat un resultat per '{0}'", "One result has been found by '{0}'", "Se encontrou um resultado para/por '{0}'");
                        break;
                    }

                default:
                    {
                        text = oLang.Tradueix("Se han encontrado {1} resultados por '{0}'", "S'han trobat {1} resultats per '{0}'", "{1} results could be found by '{0}'", "Se encontraram {1} resultados para/por '{0}'");
                        break;
                    }
            }
            string retval = string.Format(text, oRequest.SearchKey, oRequest.Results.Count);
            return retval;
        }

        public DTOWebDomain Domain()
        {
            return DTOWebDomain.Factory(Lang, false);
        }

        public class Result
        {
            public DTOBaseGuid BaseGuid { get; set; }
            public string Caption { get; set; }
            public string Url { get; set; }
            public DTOUrl CanonicalUrl { get; set; }
            public string ThumbnailUrl { get; set; }
            public Cods Cod { get; set; }
            public DateTime Fch { get; set; }

            public enum Cods
            {
                Contact,
                Location,
                Brand,
                Category,
                Sku,
                Noticia,
                BlogPost,
                Product
            }
        }
    }
}
