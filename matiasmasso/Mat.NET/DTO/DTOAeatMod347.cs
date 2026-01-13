using MatHelperStd;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOAeatMod347
    {
        public DTOExercici Exercici { get; set; }
        public DTOContact Contact { get; set; }
        public List<DTOAeatMod347Item> Vendes { get; set; }
        public List<DTOAeatMod347Item> Compres { get; set; }
        public List<DTOAeatMod347Item> Items { get; set; }

        public static bool CompresExists(DTOAeatMod347 oMod347)
        {
            bool retval = false;
            if (oMod347.Compres != null)
            {
                if (oMod347.Compres.Count > 0)
                    retval = oMod347.Compres[0].CodPais == "ES";
            }
            return retval;
        }

        public static bool VendesExists(DTOAeatMod347 oMod347)
        {
            bool retval = false;
            if (oMod347.Vendes != null)
            {
                if (oMod347.Vendes.Count > 0)
                    retval = true;
            }
            return retval;
        }



        public static string Nif(DTOContact oContact)
        {
            string retval = oContact.PrimaryNifValue();

            if (oContact.PrimaryNifValue().StartsWith("ES"))
                retval = retval.Substring(2);
            return retval;
        }

        public static DTOAmt Total(DTOAeatMod347Item item)
        {
            var retval = DTOAmt.Factory(item.T1 + item.T2 + item.T3 + item.T4);
            return retval;
        }

        public static string ClauOperacio(DTOAeatMod347Item item)
        {
            string retval = TextHelper.VbChoose((int)item.ClauOp, "A", "B", "C", "D", "E", "F", "G");
            return retval;
        }


        public static MatHelper.Excel.Sheet ExcelSheet(List<DTOAeatMod347Cca> oCcas)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            var oRow = retval.AddRow();
            retval.Rows.Add(oRow);
            oRow.AddCell("assentament");
            oRow.AddCell("data");
            oRow.AddCell("concepte");
            oRow.AddCell("base imponible");
            oRow.AddCell("IVA");

            foreach (DTOAeatMod347Cca source in oCcas)
            {
                oRow = retval.AddRow();
                retval.Rows.Add(oRow);
                oRow.AddCell(source.Cca.Id);
                oRow.AddCell(source.Cca.Fch);
                oRow.AddCell(source.Cca.Concept);
                oRow.AddCell(source.Base.Eur);
                oRow.AddCell(source.Iva.Eur);
            }

            return retval;
        }
    }

    public class DTOAeatMod347Item
    {
        public DTOAeatMod347 Parent { get; set; }
        public DTOContact Contact { get; set; }
        public decimal T1 { get; set; }
        public decimal T2 { get; set; }
        public decimal T3 { get; set; }
        public decimal T4 { get; set; }
        public string CodProvincia { get; set; }
        public string CodPais { get; set; }
        public ClauOps ClauOp { get; set; }

        public enum ClauOps
        {
            NotSet,
            Compres,
            Vendes
        }

        public static bool Declarable(DTOAeatMod347Item oItem, decimal MinimDeclarable, List<Exception> exs)
        {
            bool retval = false;

            bool BlExport = false;
            switch (DTOContact.ExportCod(oItem.Contact))
            {
                case DTOInvoice.ExportCods.intracomunitari:
                case DTOInvoice.ExportCods.extracomunitari:
                    {
                        // Exclou estrangers, Canarias, Ceuta i Melilla
                        BlExport = true;
                        break;
                    }
            }

            // Inclou estrangers que operan a Espanya amb NIF nacional
            string sNif = oItem.Contact.PrimaryNifValue();
            if (sNif.StartsWith("ES"))
                BlExport = false;

            if (!BlExport)
            {
                // Exclou els que no arriben al minim declarable
                decimal total = DTOAeatMod347.Total(oItem).Eur;
                retval = total > MinimDeclarable;
            }

            return retval;
        }
    }

    public class DTOAeatMod347Cca
    {
        public DTOCca Cca { get; set; }
        public DTOAmt Base { get; set; }
        public DTOAmt Iva { get; set; }

        public DTOAmt Total()
        {
            var retval = DTOAmt.Factory(Base, Iva);
            return retval;
        }
    }

}
