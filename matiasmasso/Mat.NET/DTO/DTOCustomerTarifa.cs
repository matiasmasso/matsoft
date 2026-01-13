using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DTO
{
    public class DTOCustomerTarifa
    {
        public DateTime Fch { get; set; }
        public DTOCustomer Customer { get; set; }
        public Boolean CostEnabled { get; set; }
        public List<DTOProductBrand> Brands { get; set; }

        public DTOCustomerTarifa()
        {
            this.Brands = new List<DTOProductBrand>();
        }

        public List<DTOProductSku> Skus()
        {
            var retval = this.Brands.SelectMany(x => x.Categories).SelectMany(y => y.Skus).ToList();
            return retval;
        }

        public DTOProductSku FindSku(DTOProductSku oSku)
        {
            var retval = Skus().FirstOrDefault(z => z.Guid.Equals(oSku.Guid));
            return retval;
        }

        public DTOProductSku FindByEan(string sEan)
        {
            var retval = Skus().FirstOrDefault(z => z.Ean13 != null && z.Ean13.Value == sEan);
            return retval;
        }

        public bool Missing(DTOProductSku oSku)
        {
            return this.FindSku(oSku) == null;
        }

        public DTOProductSku GetSkuFromEan(string sEan)
        {
            var retval = Skus().FirstOrDefault(z => z.Ean13 != null && z.Ean13.Value == sEan);
            return retval;
        }

        public DTOProductSku GetSkuFromRefProveidor(string refProveidor)
        {
            var retval = Skus().FirstOrDefault(z => z.RefProveidor == refProveidor);
            return retval;
        }


        public DTOCustomerTarifa Trimmed()
        {
            var retval = this;
            foreach (var oBrand in retval.Brands)
            {
                foreach (var oCategory in oBrand.Categories)
                {
                    oCategory.Brand = null;
                    foreach (var oSku in oCategory.Skus)
                        oSku.Category = null;
                }
            }
            return retval;
        }

        public MatHelper.Excel.Sheet Excel(DTOLang oLang, List<DTOLangText> Excerpts = null)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            bool DtoVisible = this.Skus().Exists(x => x.CustomerDto != 0);

            {
                var withBlock = retval;
                withBlock.AddColumn("Ref.M+O", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Ref.Custom", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn(oLang.Tradueix("Ref.fabricante", "Ref.fabricant", "Ref.manufacturer"), MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("EAN producto", MatHelper.Excel.Cell.NumberFormats.EAN13);
                withBlock.AddColumn("EAN packaging", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn(oLang.Tradueix("Marca", "Marca", "Brand"), MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"), MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), MatHelper.Excel.Cell.NumberFormats.W50);
                if (this.CostEnabled)
                {
                    withBlock.AddColumn(oLang.Tradueix("Coste", "Cost", "Cost"), MatHelper.Excel.Cell.NumberFormats.Euro);
                    if (DtoVisible)
                        withBlock.AddColumn(oLang.Tradueix("Dto", "Dte", "Discount"), MatHelper.Excel.Cell.NumberFormats.Percent);
                }
                withBlock.AddColumn(oLang.Tradueix("Venta", "Venda", "Retail"), MatHelper.Excel.Cell.NumberFormats.Euro);
                withBlock.AddColumn(oLang.Tradueix("Pedido mín.", "Comanda min.", "Moq"), MatHelper.Excel.Cell.NumberFormats.Integer);
                withBlock.AddColumn(oLang.Tradueix("Largo", "Llarg", "Length"), MatHelper.Excel.Cell.NumberFormats.mm);
                withBlock.AddColumn(oLang.Tradueix("Ancho", "Ample", "Width"), MatHelper.Excel.Cell.NumberFormats.mm);
                withBlock.AddColumn(oLang.Tradueix("Alto", "Alt", "Height"), MatHelper.Excel.Cell.NumberFormats.mm);
                withBlock.AddColumn(oLang.Tradueix("Peso", "Pes", "Weight"), MatHelper.Excel.Cell.NumberFormats.Kg);
                withBlock.AddColumn("Made In", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn("Codigo arancelario", MatHelper.Excel.Cell.NumberFormats.W50);
                withBlock.AddColumn(oLang.Tradueix("Imagen", "Imatge", "Image"), MatHelper.Excel.Cell.NumberFormats.PlainText);
                withBlock.AddColumn(oLang.Tradueix("Landing page"), MatHelper.Excel.Cell.NumberFormats.PlainText);
                if(Excerpts != null)
                {
                    withBlock.AddColumn(oLang.Tradueix("Español", "Espanyol", "Spanish"), MatHelper.Excel.Cell.NumberFormats.PlainText);
                    withBlock.AddColumn(oLang.Tradueix("Catalán", "Català", "Catalan"), MatHelper.Excel.Cell.NumberFormats.PlainText);
                    withBlock.AddColumn(oLang.Tradueix("Inglés", "Anglès", "English"), MatHelper.Excel.Cell.NumberFormats.PlainText);
                    withBlock.AddColumn(oLang.Tradueix("Portugués", "Portuguès", "Portuguese"), MatHelper.Excel.Cell.NumberFormats.PlainText);
                }
            }

            foreach (DTOProductBrand oBrand in this.Brands)
            {
                foreach (DTOProductCategory oCategory in oBrand.Categories)
                {
                    foreach (DTOProductSku oSku in oCategory.Skus)
                    {
                        MatHelper.Excel.Row oRow = retval.AddRow();
                        oRow.AddCell(oSku.Id);
                        oRow.AddCell(oSku.RefCustomer);
                        oRow.AddCell(oSku.RefProveidor);
                        oRow.AddCellEan(oSku.Ean13);
                        if (oSku.PackageEan == null)
                            oRow.AddCell();
                        else
                            oRow.AddCell(oSku.PackageEan.Value);
                        oRow.AddCell(oBrand.Nom.Tradueix(oLang));
                        oRow.AddCell(oCategory.Nom.Tradueix(oLang));
                        oRow.AddCell(oSku.Nom.Tradueix(oLang));

                        if (this.CostEnabled)
                        {
                            oRow.AddCellAmt(oSku.Price);//);
                            if (DtoVisible)
                                oRow.AddCell(oSku.CustomerDto);//,"", MatHelper.Excel.Cell.NumberFormats.Percent);
                        }
                        oRow.AddCellAmt(oSku.Rrpp);
                        oRow.AddCell(DTOProductSku.Moq(oSku));//,"", MatHelper.Excel.Cell.NumberFormats.Integer);

                        oRow.AddCell(oSku.DimensionLOrInherited());//,"", MatHelper.Excel.Cell.NumberFormats.mm);
                        oRow.AddCell(oSku.DimensionWOrInherited());//, "", MatHelper.Excel.Cell.NumberFormats.mm);
                        oRow.AddCell(oSku.DimensionHOrInherited());//, "", MatHelper.Excel.Cell.NumberFormats.mm);
                        oRow.AddCell(oSku.weightKgOrInherited());//, "", MatHelper.Excel.Cell.NumberFormats.Kg);
                        oRow.AddCell(oSku.madeInOrInheritedISO());
                        oRow.AddCell(oSku.CodiMercanciaIdOrInherited());

                        if (oSku.ImageExists)
                        {
                            string url = oSku.imageUrl(true);
                            oRow.AddCell(url, url);
                        }
                        else
                            oRow.AddCell();

                        string landingPage = oSku.UrlCanonicas.CanonicalUrl(oLang);
                        oRow.AddCell(landingPage, landingPage);

                        if (Excerpts != null)
                        {
                            var langText = Excerpts.FirstOrDefault(x => x.Guid == oSku.Guid);
                            oRow.AddCell(langText?.Esp ?? "");
                            oRow.AddCell(langText?.Cat ?? "");
                            oRow.AddCell(langText?.Eng ?? "");
                            oRow.AddCell(langText?.Por ?? "");
                        }
                    }
                }
            }
            return retval;
        }



        public class Compact
        {
            public DateTime Fch { get; set; }
            public DTOCustomer Customer { get; set; }
            public Boolean CostEnabled { get; set; }
            public List<DTOProductBrand.Treenode> Brands { get; set; }

            public static Compact Factory(DTOCustomer customer, DateTime fch)
            {
                Compact retval = new Compact();
                retval.Customer = customer;
                retval.Fch = fch;
                retval.Brands = new List<DTOProductBrand.Treenode>();
                return retval;
            }

            public List<DTOProductSku.Treenode> Skus()
            {
                var retval = this.Brands.SelectMany(x => x.Categories).SelectMany(y => y.Skus).ToList();
                return retval;
            }

            public DTOProductSku.Treenode FindSku(DTOProductSku.Treenode oSku)
            {
                var retval = Skus().FirstOrDefault(z => z.Guid.Equals(oSku.Guid));
                return retval;
            }

            public DTOProductSku.Treenode FindSku(DTOProductSku oSku)
            {
                var retval = Skus().FirstOrDefault(z => z.Guid.Equals(oSku.Guid));
                return retval;
            }

            public DTOProductSku.Treenode FindByEan(string sEan)
            {
                var retval = Skus().FirstOrDefault(z => z.Ean13 != null && z.Ean13.Value == sEan);
                return retval;
            }

            public bool Missing(DTOProductSku.Treenode oSku)
            {
                return this.FindSku(oSku) == null;
            }

            public DTOProductSku.Treenode GetSkuFromEan(string sEan)
            {
                var retval = Skus().FirstOrDefault(z => z.Ean13 != null && z.Ean13.Value == sEan);
                return retval;
            }
        }
    }
}
