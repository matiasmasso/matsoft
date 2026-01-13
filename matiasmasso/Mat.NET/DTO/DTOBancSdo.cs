using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOBancSdo : DTOBaseGuid
    {
        public DTOBanc Banc { get; set; }
        public DateTime Fch { get; set; }
        public DTOAmt Saldo { get; set; }

        public DTOBancSdo() : base()
        {
        }

        public DTOBancSdo(Guid oGuid) : base(oGuid)
        {
        }

        public static MatHelper.Excel.Sheet Excel(List<DTOBancSdo> oSaldos)
        {
            string sFilename = string.Format("Saldos Bancaris a {0:dd/MM/yy}", DTO.GlobalVariables.Today());
            string sSheetName = "Saldos Bancaris";
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet(sSheetName, sFilename);
            retval.AddColumn("Entitat", MatHelper.Excel.Cell.NumberFormats.PlainText);
            retval.AddColumn("Saldo", MatHelper.Excel.Cell.NumberFormats.Decimal2Digits);
            retval.AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY);
            foreach (DTOBancSdo Item in oSaldos)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                oRow.AddCell(Item.Banc.Abr);
                oRow.AddCellAmt(Item.Saldo);
                oRow.AddCell(Item.Fch);
            }
            return retval;
        }
    }
}
