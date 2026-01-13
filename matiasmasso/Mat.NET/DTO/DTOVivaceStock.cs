using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOVivaceStock
    {
        public string Referencia { get; set; }
        public string Descripcio { get; set; }
        public int Stock { get; set; }
        public string Ubicacio { get; set; }
        public DateTime FchEntrada { get; set; }
        public DateTime LastMove { get; set; }
        public string Procedencia { get; set; }
        public DTOAmt Cost { get; set; }

        public static int PaletsCount(List<DTOVivaceStock> values)
        {
            int retval = values.Select(x => x.Ubicacio).Distinct().Count();
            return retval;
        }

        public static int PaletsInactius(List<DTOVivaceStock> values, int dies)
        {
            DateTime FchLimit = DTO.GlobalVariables.Today().AddDays(-dies);
            var palets = values.GroupBy(x => x.Ubicacio).Select(y => new { Ubicacio = y.Key, LastMove = y.Max(z => z.LastMove) });
            int retval = palets.Count(x => x.LastMove < FchLimit);
            return retval;
        }

        public static int RefsCount(List<DTOVivaceStock> values)
        {
            int retval = values.Select(x => x.Referencia).Distinct().Count();
            return retval;
        }

        public static int RefsInactives(List<DTOVivaceStock> values, int dies)
        {
            DateTime FchLimit = DTO.GlobalVariables.Today().AddDays(-dies);
            var refs = values.GroupBy(x => x.Referencia).Select(y => new { Referencia = y.Key, LastMove = y.Max(z => z.LastMove) });
            int retval = refs.Count(x => x.LastMove < FchLimit);
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(List<DTOVivaceStock> items)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Inventari Vivace valorat");
            {
                var withBlock = retval;
                withBlock.AddColumn("Referencia");
                withBlock.AddColumn("Producte");
                withBlock.AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Ult.Entrada", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
                withBlock.AddColumn("Ubicació");
                withBlock.AddColumn("Cost", MatHelper.Excel.Cell.NumberFormats.Euro);
            }

            foreach (DTOVivaceStock item in items)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                oRow.AddCell(item.Referencia);
                oRow.AddCell(item.Descripcio);
                oRow.AddCell(item.Stock);
                oRow.AddCell(item.FchEntrada);
                oRow.AddCell(item.Ubicacio);
                oRow.AddCellAmt(item.Cost);
            }

            return retval;
        }

        public static DTOCsv Csv(List<DTOVivaceStock> items)
        {
            DTOCsv retval = new DTOCsv("Inventari Vivace valorat.csv");

            DTOCsvRow oHeader = retval.AddRow();
            oHeader.AddCell("Referencia");
            oHeader.AddCell("Producte");
            oHeader.AddCell("Stock");
            oHeader.AddCell("Ult.Entrada");
            oHeader.AddCell("Ubicació");
            oHeader.AddCell("Cost");

            foreach (DTOVivaceStock item in items)
            {
                DTOCsvRow oRow = retval.AddRow();
                oRow.AddCell(item.Referencia);
                oRow.AddCell(item.Descripcio);
                oRow.AddCell(item.Stock.ToString());
                oRow.AddCell(TextHelper.VbFormat(item.FchEntrada, "dd/MM/yyyy"));
                oRow.AddCell(item.Ubicacio);
                if (item.Cost == null)
                    oRow.AddCell("0");
                else
                    oRow.AddCell(item.Cost.Eur.ToString());
            }

            return retval;
        }
    }
}
