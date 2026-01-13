using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTONomina
    {
        public DTOCca Cca { get; set; }

        public DTOStaff Staff { get; set; }
        public DTOAmt Devengat { get; set; }
        public DTOAmt Dietes { get; set; }
        public DTOAmt SegSocial { get; set; }
        public DTOAmt IrpfBase { get; set; }
        public DTOAmt Irpf { get; set; }
        public DTOAmt Embargos { get; set; }
        public DTOAmt Deutes { get; set; }
        public DTOAmt Anticips { get; set; }
        public DTOAmt Liquid { get; set; }
        public string IbanDigits { get; set; }
        public DTOIban Iban { get; set; }
        public List<Item> Items { get; set; }

        public bool IsLoaded { get; set; }
        public bool IsNew { get; set; }

        public DTONomina() : base()
        {
            IsNew = true;
        }

        public DTONomina(DTOCca oCca) : base()
        {
            Cca = oCca;
        }

        public DTONomina(DTOStaff oStaff) : base()
        {
            Staff = oStaff;
            Iban = Staff.Iban;
            IsNew = true;
        }

        public static string CcaConcepte(DTONomina oNomina)
        {
            string sText = "nomina";
            string sNom = DTOStaff.AliasOrNom(oNomina.Staff);
            int iLenText = sText.Length;
            int iLenNom = sNom.Length;
            int iLenField = 60;
            if (iLenNom + iLenText + 1 > iLenField)
                sNom = sNom.Substring(0, iLenField - iLenText - 1);
            var retval = string.Format("{0}-{1}", sNom, sText);
            return retval;
        }

        public class Item
        {
            public Concepte Concepte { get; set; }
            public int Qty { get; set; }
            public DTOAmt Price { get; set; }
            public DTOAmt Devengo { get; set; }
            public DTOAmt Deduccio { get; set; }

            public Item(DTONomina.Concepte oConcepte) : base()
            {
                Concepte = oConcepte;
            }

            public DTOAmt Import
            {
                get
                {
                    DTOAmt retval = null/* TODO Change to default(_) if this is not a reference type */;
                    if (Price != null)
                        retval = DTOAmt.Factory(Qty * Price.Eur);
                    return retval;
                }
            }
        }

        public class Concepte
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Concepte(int iId, string sName = "") : base()
            {
                Id = iId;
                Name = sName;
            }
        }

        public class Header
        {
            public string DownloadUrl { get; set; }
            public string ThumbnailUrl { get; set; }
            public DateTime Fch { get; set; }


            public class Collection : List<DTONomina.Header>
            {
                public List<int> Years()
                {
                    var retval = this.GroupBy(x => x.Fch.Year).Select(y => y.First()).Select(z => z.Fch.Year).OrderByDescending(o => o).ToList();
                    return retval;
                }

                public static Collection Factory(List<DTONomina> oNominas)
                {
                    Collection retval = new Collection();
                    foreach (var oNomina in oNominas)
                    {
                        var item = new Header();
                        item.Fch = oNomina.Cca.Fch;
                        item.ThumbnailUrl = oNomina.Cca.DocFile.ThumbnailUrl();
                        item.DownloadUrl = oNomina.Cca.DocFile.DownloadUrl();
                        retval.Add(item);
                    }
                    return retval;
                }

                //public string Serialized()
                //{
                //    JavaScriptSerializer serializer = new JavaScriptSerializer();
                //    return serializer.Serialize(this);
                //}
            }
        }
    }
}
