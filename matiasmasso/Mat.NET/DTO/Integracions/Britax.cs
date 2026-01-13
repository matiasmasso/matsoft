using System.Collections.Generic;

namespace DTO
{
    public class Britax
    {
        public class XML : List<DTO.Britax.XML.Customer>// Storelocator
        {
            public class Customer
            {
                public string AccountNum { get; set; }
                public string Name { get; set; }
                public string Street { get; set; }
                public string ZipCode { get; set; }
                public string City { get; set; }
                public string Country { get; set; }
                public string Region { get; set; }
                //public string Phone { get; set; }
                //public string Email { get; set; }
                //public string Web { get; set; }
                //public bool Distributor { get; set; }

                public double Latitude { get; set; }
                public double Longitude { get; set; }
                public List<Item> Items { get; set; }
            }

            public class Item
            {
                public string ItemId { get; set; }
            }
        }

        public class DiscrepancyReport : MatHelper.Excel.Sheet
        {
            public static DiscrepancyReport Factory(DTOImportacio.Confirmation confirmation)
            {
                DiscrepancyReport retval = new DiscrepancyReport();
                MatHelper.Excel.Row headerRow = retval.AddRow();
                headerRow.AddCell("Item code");
                headerRow.AddCell("Item name");
                headerRow.AddCell("Shipment");
                headerRow.AddCell("Packing slip");
                headerRow.AddCell("Invoice number");
                headerRow.AddCell("Quantity invoiced");
                headerRow.AddCell("Quantity received");
                headerRow.AddCell("Difference");
                headerRow.AddCell("M+O");

                foreach (DTOInvoiceReceived.Item item in confirmation.Items)
                {
                    MatHelper.Excel.Row row = retval.AddRow();
                    row.AddCell("Item code");
                    row.AddCell("Item name");
                    row.AddCell("Shipment");
                    row.AddCell("Packing slip");
                    row.AddCell("Invoice number");
                    row.AddCell(item.Qty);
                    row.AddCell(item.QtyConfirmed);
                    row.AddFormula("R[C-1]-R[C-2]");
                    row.AddCell("M+O");
                }
                return retval;
            }
        }
    }
}
