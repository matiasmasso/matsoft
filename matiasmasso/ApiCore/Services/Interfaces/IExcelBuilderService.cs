using ClosedXML.Excel;

namespace Api.Services.Interfaces
{
    public interface IExcelBuilderService
    {
        XLWorkbook CreateWorkbook();

        IXLWorksheet AddSheet(XLWorkbook wb, string name);

        void AddHeaderRow(IXLWorksheet ws, int row, params string[] headers);

        void AddImageLink(IXLWorksheet ws, int row, int col, string imagePath, string url);

        void ApplyHeaderStyle(IXLCell cell);

        void ApplyCurrencyStyle(IXLCell cell);

        void ApplyDateStyle(IXLCell cell);

        void AutoAdjust(IXLWorksheet ws);

        void FreezeTopRows(IXLWorksheet ws, int rows);

        byte[] Save(XLWorkbook wb);
    }
}
