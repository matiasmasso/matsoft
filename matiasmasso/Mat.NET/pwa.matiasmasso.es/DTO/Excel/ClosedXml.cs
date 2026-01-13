using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DTO.Excel
{
    public class ClosedXml
    {

        #region "Read"

        //Read from an Excel stream
        public static Book? Read(Byte[] bytes, string? filename = null, bool hasHeaderRow = false)
        {
            Book? retval = null;
            using (var ms = new MemoryStream(bytes))
            {
                var workbook = new XLWorkbook(ms);
                retval = Read(workbook, filename, hasHeaderRow);
            }

            return retval;
        }

        //Read from a Memory stream
        public static Book? Read(List<Exception> exs, Stream stream, string filename = null, bool hasHeaderRow = false)
        {
            Book? retval = null;
            try
            {
                var workbook = new XLWorkbook(stream);
                retval = Read(workbook, filename, hasHeaderRow);

            }
            catch (Exception ex)
            {
                exs.Add(ex);
            }
            return retval;
        }

        //Read from an Excel filename
        public static Book Read(List<Exception> exs, string filename, bool hasheaderRow = false)
        {
            using (var workbook = new XLWorkbook(filename))
            {
                var retval = Read(workbook, filename, hasheaderRow);
                return retval;
            }
        }

        //Read from a CloseXml workbook
        private static Book Read(XLWorkbook workbook, string? filename = null, bool hasHeaderRow = false)
        {
            var retval = new Book(filename);
            foreach (var worksheet in workbook.Worksheets)
            {
                var targetSheet = retval.AddSheet(worksheet.Name);
                var wsRows = worksheet.RangeUsed().RowsUsed(); //.Skip(1); header row
                foreach (var wsRow in wsRows)
                {
                    var rowNumber = wsRow.RowNumber();
                    var targetRow = targetSheet.AddRow();

                    foreach (var wsCell in wsRow.Cells())
                    {
                        var targetCell = targetRow.AddCell();
                        targetCell.Content = wsCell.Value.ToString();
                    }
                }
            }
            return retval;
        }

        #endregion


        #region "Write"

        //Write Excel file stream to disk temp folder and return path
        public static string SaveExcelStream(List<Exception> exs, byte[] bytes, string filename = null)
        {
            filename = string.IsNullOrEmpty(filename) ? System.Guid.NewGuid().ToString() : filename;
            if (!filename.EndsWith(".xlsx")) filename += ".xlsx";
            string fullPath = Path.Combine(Path.GetTempPath(), filename);
            System.IO.File.WriteAllBytes(fullPath, bytes);
            return fullPath;
        }


        public static Byte[] Bytes(Sheet sheet)
        {
            var book = new Book(sheet.Filename);
            book.Sheets.Add(sheet);
            return Bytes(book);
        }

        public static Byte[] Bytes(Book book)
        {
            using (var workbook = new XLWorkbook())
            {
                foreach (var sheet in book.Sheets)
                {
                    var worksheet = workbook.Worksheets.Add(sheet.Name);
                    var rowIdx = 0;
                    var cellIdx = 0;
                    var hasColHeaders = false;

                    //set column captions
                    if (sheet.Columns.Any(x => !string.IsNullOrEmpty(x.Header)))
                    {
                        hasColHeaders = true;
                        rowIdx += 1;
                        foreach (var col in sheet.Columns)
                        {
                            cellIdx += 1;
                            var wsCell = worksheet.Cell(rowIdx, cellIdx);
                            wsCell.Style.Alignment.SetHorizontal(HorizontalAlingment(col.NumberFormat));
                            wsCell.Value = col.Header;
                        }
                    }


                    //write data
                    foreach (var row in sheet.Rows)
                    {
                        rowIdx += 1;
                        cellIdx = 0;
                        foreach (var cell in row.Cells)
                        {
                            cellIdx += 1;
                            var wsCell = worksheet.Cell(rowIdx, cellIdx);
                            if (cell.Content != null)
                            {
                                if (cell.Content is decimal)
                                    wsCell.SetValue((XLCellValue)Convert.ToDouble(cell.Content));
                                else
                                    wsCell.SetValue((XLCellValue)cell.Content.ToString());

                                wsCell.Style.Alignment.SetHorizontal(HorizontalAlingment(cell.NumberFormat));
                                if (cell.NumberFormat != Cell.NumberFormats.NotSet)
                                    wsCell.Style.NumberFormat.Format = NumberFormat(cell.NumberFormat);

                                if (!string.IsNullOrEmpty(cell.Url))
                                    wsCell.SetHyperlink(new XLHyperlink(cell.Url));

                                if (!string.IsNullOrEmpty(cell.FormulaR1C1))
                                    wsCell.FormulaR1C1 = cell.FormulaR1C1;
                            }

                            if (row.Backcolor != null)
                                wsCell.Style.Fill.SetBackgroundColor(XLColor.FromHtml(row.Backcolor));

                            if (row.Forecolor != null)
                                wsCell.Style.Font.SetFontColor(XLColor.FromHtml(row.Forecolor));
                        }
                    }

                    if(sheet.RowGroups.Count> 0)
                    {
                        worksheet.Outline.SummaryVLocation = XLOutlineSummaryVLocation.Top;
                        foreach (var group in sheet.RowGroups)
                        {
                            worksheet.Rows(group.FirstRow, group.LastRow).Group();
                        }
                    }


                    //set column format on cells
                    if (sheet.Columns.Any(x => !string.IsNullOrEmpty(x.Header)))
                    {
                        cellIdx = 0;
                        foreach (var col in sheet.Columns)
                        {
                            cellIdx += 1;
                            if (col.NumberFormat != Cell.NumberFormats.NotSet)
                                worksheet.Column(cellIdx).CellsUsed().Style.NumberFormat.Format = NumberFormat(col.NumberFormat);

                            worksheet.Column(cellIdx).CellsUsed().Style.Alignment.SetHorizontal(HorizontalAlingment(col.NumberFormat));
                        }
                    }


                    //adjust columns width
                    worksheet.Columns().AdjustToContents();
                }


                //return result as byte array
                byte[] retval;
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    retval = ms.ToArray();
                };


                return retval;
            }
        }

        public static XLAlignmentHorizontalValues HorizontalAlingment(Cell.NumberFormats value)
        {
            var retval = XLAlignmentHorizontalValues.General;
            switch (value)
            {
                case Cell.NumberFormats.Integer:
                case Cell.NumberFormats.Decimal2Digits:
                case Cell.NumberFormats.Euro:
                case Cell.NumberFormats.Kg:
                case Cell.NumberFormats.KgD1:
                case Cell.NumberFormats.m3:
                case Cell.NumberFormats.m3D2:
                case Cell.NumberFormats.mm:
                case Cell.NumberFormats.Percent:
                    retval = XLAlignmentHorizontalValues.Right;
                    break;
                case Cell.NumberFormats.DDMMYY:
                    retval = XLAlignmentHorizontalValues.Center;
                    break;
            }
            return retval;
        }

        public static string NumberFormat(Cell.NumberFormats value)
        {
            var retval = "@";
            switch (value)
            {
                case Cell.NumberFormats.Integer:
                    retval = @"#,##0;-#,##0;#";
                    break;
                case Cell.NumberFormats.DDMMYY:
                    retval = @"dd/MM/yy";
                    break;
                case Cell.NumberFormats.Decimal2Digits:
                    retval = @"#,##0.00;-#,##0.00;#";
                    break;
                case Cell.NumberFormats.Euro:
                    retval = @"#,##0.00 €;-#,##0.00 €;#";
                    break;
                case Cell.NumberFormats.Kg:
                    retval = @"#,##0 \K\g;-#,##0 \K\g;#";
                    break;
                case Cell.NumberFormats.KgD1:
                    retval = @"#,##0.0 \K\g;-#,##0.0 \K\g;#";
                    break;
                case Cell.NumberFormats.m3:
                    retval = @"#,##0.00 \m3;-#,##0.00 \m3;#";
                    break;
                case Cell.NumberFormats.m3D2:
                    retval = @"#,##0.00 \m3;-#,##0.00 \m3;#";
                    break;
                case Cell.NumberFormats.mm:
                    retval = @"#,##0 \m\m;-#,##0 \m\m;#";
                    break;
                case Cell.NumberFormats.Percent:
                    retval = @"#\%;-#\%;#";
                    break;
                case Cell.NumberFormats.EAN13:
                    retval = @"@";
                    break;
            }
            return retval;
        }

        #endregion
    }
}

