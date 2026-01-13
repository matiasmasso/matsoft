using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MatHelper.Excel
{
    public class Book
    {
        public string Filename { get; set; }
        public List<Sheet> Sheets { get; set; }

        public enum UrlCods
        {
            NotSet,
            CustomerDeliveries
        }

        public Book() {
            Sheets = new List<Sheet>();
        }

        public Book(string filename="")
        {
            Sheets = new List<Sheet>();
            Filename = filename;    
        }

        public Sheet AddSheet(string sheetName)
        {
            var retval = new Sheet(sheetName);
            Sheets.Add(retval);
            return retval;
        }

        public Byte[] Bytes()
        {
            var ms = new MemoryStream();
            var xl = SpreadsheetDocument.Create(ms, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
            WorkbookPart wbp = xl.AddWorkbookPart();

            Workbook book = new Workbook();
            WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
            wsp.Worksheet = new Worksheet(new SheetData());
            Sheets sts = xl.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            DocumentFormat.OpenXml.UInt32Value sheetId = 0;
            foreach(Sheet sheet in Sheets)
            {
                sheetId += 1;
               var st = new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = xl.WorkbookPart.GetIdOfPart(wsp),
                    SheetId = sheetId,
                    Name = sheet.Name
                };

                foreach (Row row in sheet.Rows)
                {
                    var rw = new DocumentFormat.OpenXml.Spreadsheet.Row()
                    {
                    };
                    
                     foreach (Cell cell in row.Cells)
                    {
                        var cl = new DocumentFormat.OpenXml.Spreadsheet.Cell()
                        {
                            CellValue = new CellValue( cell.Content.ToString())
                        };
                    }

                }


                sts.Append(st);
            }

            //reset the position to the start of the stream
            ms.Seek(0, SeekOrigin.Begin);
            var retval = ms.ToArray();
            ms.Close();
            return retval;
        }

    }
}
