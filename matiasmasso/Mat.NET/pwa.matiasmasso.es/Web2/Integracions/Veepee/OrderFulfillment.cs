using DTO;
using DTO.Integracions.Veepee;
using System.Collections.Generic;

namespace Web.Integracions.Veepee
{
    public class OrderFulfillment
    {
        public static List<OrderLine> FromExcel(Byte[]? bytes)
        {
            List<OrderLine> retval = new();
            if (bytes == null) throw new ArgumentNullException("No Excel book passed to read");

            var book = DTO.Excel.ClosedXml.Read(bytes);
            var sheet = book?.Sheets.FirstOrDefault();
            if (sheet == null) throw new ArgumentNullException("No sheets on Excel book");

            foreach (var row in sheet.Rows)
            {
                if (sheet.Rows.IndexOf(row) > 0)
                {
                    var item = new DTO.Integracions.Veepee.OrderLine
                    {
                        Batch = row.Cells[0].Content?.ToString(),
                        OrderNumber = row.Cells[1].Content?.ToString(),
                        DeliveryOrder = row.Cells[2].Content?.ToString(),
                        ParcelNumber = row.Cells[3].Content?.ToString(),
                        Transporter = row.Cells[4].Content?.ToString(),
                        DestinationAddress = row.Cells[5].Content?.ToString(),
                        ProductNumber = row.Cells[6].Content?.ToString(),
                        Ean = row.Cells[7].Content?.ToString(),
                        SupplierRef = row.Cells[8].Content?.ToString(),
                        ProductLabel = row.Cells[9].Content?.ToString(),
                        Qty = int.Parse(row.Cells[10].Content?.ToString() ?? "0"),
                        QtyAssigned = int.Parse(row.Cells[11].Content?.ToString() ?? "0"),
                        LabeledQty = int.Parse(row.Cells[12].Content?.ToString() ?? "0"),
                        SentQty = int.Parse(row.Cells[13].Content?.ToString() ?? "0"),
                        StockOutQty = int.Parse(row.Cells[14].Content?.ToString() ?? "0"),
                        Weight = row.Cells[15].Content?.ToString()
                    };
                    retval.Add(item);
                }
            }
            return retval;
        }
    }
}
