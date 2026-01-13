using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOAeatModel : DTOBaseGuid
    {
        public Cods Cod { get; set; }
        public string Nom { get; set; }
        public string Dsc { get; set; }
        public PeriodTypes PeriodType { get; set; }
        public bool SoloInfo { get; set; }
        public bool VisibleBancs { get; set; }
        public bool Obsolet { get; set; }
        public DTOAeatDoc.Header.Collection Docs { get; set; }

        public enum Cods
        {
            NotSet,
            IRPFmensual,
            IRPFanual = 3,
            Mod347 = 9,
            BalanceProvisional = 13,
            IVAmensual = 18,
            CuentasAuditadas = 28,
            IVAanual = 42,
            SegSocialTC2 = 36,
            Auditoria = 28
        }

        public enum PeriodTypes
        {
            NotSet,
            Altres,
            Mensual,
            Trimestral,
            Anual
        }

        public DTOAeatModel() : base()
        {
            this.Docs = new DTOAeatDoc.Header.Collection();
        }

        public DTOAeatModel(Guid oGuid) : base(oGuid)
        {
            this.Docs = new DTOAeatDoc.Header.Collection();
        }


        public string Url(DTOUser oUser, bool absoluteUrl = false)
        {
            return DTOWebDomain.Default(absoluteUrl).Url("aeatmodel", this.Guid.ToString(), ((int)oUser.Emp.Id).ToString()); // oUser.Guid.ToString());
        }

        public string Contents(DTOLang lang)
        {
            string retval = "";
            if (this.Docs.Count == 0)
            {
                retval = lang.Tradueix("Sin documentos", "sense documents", "No documents found");
            }
            else
            {
                int yearFrom = this.Docs.Last().Fch.Year;
                int yearTo = this.Docs.First().Fch.Year;
                string singleDoc = "1 doc.";
                string multipleDocs = String.Format("{0} docs.", this.Docs.Count);
                string docsCount = this.Docs.Count == 1 ? singleDoc : multipleDocs;
                string singleYear = string.Format("{0} {1}", lang.Tradueix("año", "any", "year"), yearFrom);
                string multipleYears = string.Format("{0} {1}-{2}", lang.Tradueix("años", "anys", "years"), yearFrom, yearTo);
                string exercise = (yearFrom == yearTo) ? singleYear : multipleYears;
                retval = String.Format("{0} {1}", docsCount, exercise);
            }
            return retval;
        }

        public class Collection : List<DTOAeatModel>
        {
            public Collection WithDocs()
            {
                IEnumerable<DTOAeatModel> docs = this.Where(x => x.Docs.Count > 0);
                Collection retval = new Collection();
                retval.AddRange(docs);
                return retval;
            }

            public static string Url(DTOUser oUser, bool absoluteUrl = false)
            {
                return DTOWebDomain.Default(absoluteUrl).Url("aeatmodels", oUser.Guid.ToString());
            }
        }
    }

}



