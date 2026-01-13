using MatHelperStd;

namespace DTO
{
    public static class ExcelExtensions
    {
        public static MatHelper.Excel.Cell AddCellAmt(this MatHelper.Excel.Row oRow, DTOAmt oAmt)
        {
            MatHelper.Excel.Cell oCell = new MatHelper.Excel.Cell();
            if (oAmt != null)
                oCell = new MatHelper.Excel.Cell(oAmt.Eur);//,"" , MatHelper.Excel.Cell.NumberFormats.Euro);
            oRow.Cells.Add(oCell);
            return oCell;
        }


        public static MatHelper.Excel.Cell AddCellEan(this MatHelper.Excel.Row oRow, DTOEan oEan)
        {
            MatHelper.Excel.Cell oCell = new MatHelper.Excel.Cell();
            if (oEan != null)
                oCell = new MatHelper.Excel.Cell(oEan.Value);
            oRow.Cells.Add(oCell);
            return oCell;
        }
    }
}
