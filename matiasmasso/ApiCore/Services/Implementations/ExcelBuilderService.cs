using Api.Services.Interfaces;
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;

namespace Api.Services.Implementations
{
    public class ExcelBuilderService : IExcelBuilderService
    {
        private readonly IWebHostEnvironment _env;

        public ExcelBuilderService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public XLWorkbook CreateWorkbook()
        {
            return new XLWorkbook();
        }

        public IXLWorksheet AddSheet(XLWorkbook wb, string name)
        {
            return wb.AddWorksheet(name);
        }

        public void AddHeaderRow(IXLWorksheet ws, int row, params string[] headers)
        {
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(row, i + 1);
                cell.Value = headers[i];
                //ApplyHeaderStyle(cell);
            }
        }

        public void AddImageLink(IXLWorksheet ws, int row, int col, string imagePath, string url)
        {
            var cell = ws.Cell(row, col);
            cell.SetHyperlink(new XLHyperlink(url));

            var fullPath = Path.Combine(_env.WebRootPath, imagePath);

            ws.AddPicture(fullPath)
                .MoveTo(cell)
                .Scale(1.0); // per iconos de 16x16px
        }

        public void ApplyHeaderStyle(IXLCell cell)
        {
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.LightGray;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        public void ApplyCurrencyStyle(IXLCell cell)
        {
            cell.Style.NumberFormat.Format = "#,##0.00 €";
        }

        public void ApplyDateStyle(IXLCell cell)
        {
            cell.Style.DateFormat.Format = "dd/MM/yyyy";
        }

        public void AutoAdjust(IXLWorksheet ws)
        {
            ws.Columns().AdjustToContents();
        }

        public void FreezeTopRows(IXLWorksheet ws, int rows)
        {
            ws.SheetView.FreezeRows(rows);
        }

        public byte[] Save(XLWorkbook wb)
        {
            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
