using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOProductCatalog : DTOProduct
    {
        public List<DTOProductBrand> Brands { get; set; } = new List<DTOProductBrand>();
        public List<DTOProductCategory> Categories { get; set; } = new List<DTOProductCategory>();
        public List<DTOProductSku> Skus { get; set; } = new List<DTOProductSku>();
        public List<DTOProduct> bases { get; set; } = new List<DTOProduct>();
        public List<DTOCnap> cnaps { get; set; } = new List<DTOCnap>();

        public static MatHelper.Excel.Sheet RefsExcel(List<DTOProductSku> oSkus)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Referencias M+O");
            {
                var withBlock = retval;
                withBlock.AddColumn("Ref.M+O");
                withBlock.AddColumn("EAN 13");
                withBlock.AddColumn("Ref.Fabricante");
                withBlock.AddColumn("Marca Comercial");
                withBlock.AddColumn("Categoría");
                withBlock.AddColumn("Producto");
            }
            foreach (var oSku in oSkus)
            {
                MatHelper.Excel.Row oRow = retval.AddRow();
                oRow.AddCell(oSku.Id);
                if (oSku.Ean13 == null)
                    oRow.AddCell();
                else
                    oRow.AddCell(oSku.Ean13.Value);
                oRow.AddCell(oSku.RefProveidor);
                oRow.AddCell(oSku.Category.Brand.Nom.Esp);
                oRow.AddCell(oSku.Category.Nom.Esp);
                oRow.AddCell(oSku.Nom.Esp);
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet Excel(List<Exception> exs, DTOProductCatalog oCatalog) //deprecated to DTOCatalog
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Stocks", "M+O stocks " + VbUtilities.Format(DTO.GlobalVariables.Now(), "yyyyMMddThhmmssfffZ"));
            {
                var withBlock = retval;
                withBlock.AddColumn("Code", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Product", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Customers", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Supplier", MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn("Available", MatHelper.Excel.Cell.NumberFormats.Integer);
            }
            foreach (DTOProductSku item in oCatalog.Skus)
            {
                var iAvailable = Math.Max(0, item.Stock - (item.Clients - item.ClientsAlPot - item.ClientsEnProgramacio));

                MatHelper.Excel.Row oRow = retval.AddRow();
                oRow.AddCell(item.RefProveidor);
                oRow.AddCell(item.NomProveidor);
                oRow.AddCell(item.Stock);
                oRow.AddCell(item.Clients);
                oRow.AddCell(item.Proveidors);
                oRow.AddCell(iAvailable);
            }
            return retval;
        }

        public static MatHelper.Excel.Sheet ExcelStocks(DTOProductCatalog oCatalog, DTORol oRol)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Stocks", "M+O stocks " + VbUtilities.Format(DTO.GlobalVariables.Now(), "yyyyMMddThhmmssfffZ"));
            {
                var withBlock = retval;
                withBlock.AddColumn("Ref.M+O", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Ean", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Ref.Fabricante", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Producto", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer);
            }

            foreach (DTOProductBrand oBrand in oCatalog.Brands)
            {
                foreach (DTOProductCategory oCategory in oBrand.Categories)
                {
                    foreach (DTOProductSku item in oCategory.Skus)
                    {
                        var iAvailable = Math.Max(0, item.Stock - (item.Clients - item.ClientsAlPot - item.ClientsEnProgramacio));

                        MatHelper.Excel.Row oRow = retval.AddRow();
                        oRow.AddCell(item.Id);
                        if (item.Ean13 == null)
                            oRow.AddCell();
                        else
                            oRow.AddCell(item.Ean13.Value, "", MatHelper.Excel.Cell.NumberFormats.W50);

                        oRow.AddCell(item.RefProveidor);
                        if (oRol.id == DTORol.Ids.manufacturer)
                        {
                            oRow.AddCell(item.NomProveidorNoRef());
                        }
                        else
                        {
                            oRow.AddCell(item.NomLlarg.Esp);
                        }
                        oRow.AddCell(DTOProductSku.StockAvailable(item),"", MatHelper.Excel.Cell.NumberFormats.Integer);
                    }
                }
            }
            return retval;
        }

        public static DTOCsv CsvStocks(DTOProductCatalog oCatalog, DTORol oRol)
        {
            DTOCsv retval = new DTOCsv("M+O Stocks.csv");

            foreach (DTOProductSku oSku in oCatalog.Skus.Where(x => x.Stock > 0).OrderBy(y => y.Id))
            {
                DTOCsvRow oRow = retval.AddRow();
                oRow.AddCell(((int)oSku.Id).ToString());
                oRow.AddCell(DTOProductSku.TruncatedStockValue(oSku, oRol));
            }

            return retval;
        }
    }
}
