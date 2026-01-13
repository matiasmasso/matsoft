using System.Collections.Generic;

namespace DTO
{
    public class DTOQualityDistribution
    {
        public DTOCustomer Customer { get; set; }
        public List<DTOProductSku> Skus { get; set; }

        public DTOQualityDistribution() : base()
        {
            Skus = new List<DTOProductSku>();
        }

        public static MatHelper.Excel.Sheet ExcelSheet(List<DTOQualityDistribution> items)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            {
                var withBlock = retval;
                withBlock.AddColumn("product depth", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("customer");
            }

            foreach (var item in items)
            {
                var oRow = retval.AddRow();
                retval.Rows.Add(oRow);
                oRow.AddCell(item.Skus.Count);
                oRow.AddCell(item.Customer.FullNom);
            }

            return retval;
        }
    }

}
