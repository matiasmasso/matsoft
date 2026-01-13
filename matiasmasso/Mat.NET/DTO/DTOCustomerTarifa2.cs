using MatHelperStd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
    public class DTOCustomerTarifa2
    {
        public DateTime Fch { get; set; }
        public DTOGuidNom.Compact Customer { get; set; }
        public Boolean CostEnabled { get; set; }
        public List<Brand> Brands { get; set; }

        public DTOCustomerTarifa2()
        {
            this.Brands = new List<Brand>();
        }

        public List<Sku> Skus()
        {
            var retval = this.Brands.SelectMany(x => x.Categories).SelectMany(y => y.Skus).ToList();
            return retval;
        }

        public Sku FindSku(Sku oSku)
        {
            var retval = Skus().FirstOrDefault(z => z.Guid.Equals(oSku.Guid));
            return retval;
        }

        public Sku FindByEan(string sEan)
        {
            var retval = Skus().FirstOrDefault(z => z.Ean13.Value == sEan);
            return retval;
        }

        public bool Missing(Sku oSku)
        {
            return this.FindSku(oSku) == null;
        }

        public Sku GetSkuFromEan(string sEan)
        {
            var retval = Skus().FirstOrDefault(z => z.Ean13.Value == sEan);
            return retval;
        }

        public Sku GetSkuFromRefProveidor(string refProveidor)
        {
            var retval = Skus().FirstOrDefault(z => z.RefProveidor == refProveidor);
            return retval;
        }

        public MatHelper.Excel.Sheet Excel(DTOLang oLang)
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet();
            bool DtoVisible = this.Skus().Exists(x => x.CustomerDto != 0);

            {
                var withBlock = retval;
                withBlock.AddColumn("Ref.M+O");
                withBlock.AddColumn("Ref.Custom");
                withBlock.AddColumn(oLang.Tradueix("Ref.fabricante", "Ref.fabricant", "Ref.manufacturer"));
                withBlock.AddColumn("EAN producto");
                withBlock.AddColumn("EAN packaging");
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
            }

            foreach (Brand oBrand in this.Brands)
            {
                foreach (Category oCategory in oBrand.Categories)
                {
                    foreach (Sku oSku in oCategory.Skus)
                    {
                        MatHelper.Excel.Row oRow = retval.AddRow();
                        oRow.AddCell(oSku.Id);
                        oRow.AddCell(oSku.RefCustomer);
                        oRow.AddCell(oSku.RefProveidor);
                        oRow.AddCell(oSku.Ean13?.Value ?? "");
                        if (oSku.PackageEan == null)
                            oRow.AddCell();
                        else
                            oRow.AddCell(oSku.PackageEan);
                        oRow.AddCell(oBrand.Nom);
                        oRow.AddCell(oCategory.Nom);
                        oRow.AddCell(oSku.Nom.Esp);

                        if (this.CostEnabled)
                        {
                            oRow.AddCell(oSku.Price);
                            if (DtoVisible)
                                oRow.AddCell(oSku.CustomerDto);
                        }
                        oRow.AddCell(oSku.Rrpp);
                        oRow.AddCell(oSku.Moq);

                        oRow.AddCell(oSku.DimensionL);
                        oRow.AddCell(oSku.DimensionW);
                        oRow.AddCell(oSku.DimensionH);
                        oRow.AddCell(oSku.NetWeight);
                        oRow.AddCell(oSku.MadeIn);
                        oRow.AddCell(oSku.CodiMercancia);

                        if (oSku.ImageExists)
                        {
                            string url = oSku.ImageUrl(true);
                            oRow.AddCell(url, url);
                        }
                        else
                            oRow.AddCell();
                    }
                }
            }
            return retval;
        }


        public class Brand : DTOGuidNom.Compact
        {
            public List<Category> Categories { get; set; }
            public String NomProveidor { get; set; }

            public Brand()
            {
                this.Categories = new List<DTOCustomerTarifa2.Category>();
            }
        }
        public class Category : DTOGuidNom.Compact
        {
            public List<Sku> Skus { get; set; }
            public Category()
            {
                this.Skus = new List<DTOCustomerTarifa2.Sku>();
            }
        }
        public class Sku : DTOGuidNom.Compact
        {
            public int Id { get; set; }
            public new DTOLangText.Compact Nom { get; set; }
            public DTOLangText.Compact NomLlarg { get; set; }
            public string RefProveidor { get; set; }
            public string RefCustomer { get; set; }

            public DTOEan Ean13 { get; set; }
            public string PackageEan { get; set; }

            public int Moq { get; set; }

            public int Stock { get; set; }
            public int Clients { get; set; }
            public decimal Rrpp { get; set; }
            public decimal Price { get; set; }
            public decimal CustomerDto { get; set; }

            public int DimensionL { get; set; }
            public int DimensionW { get; set; }
            public int DimensionH { get; set; }

            public decimal NetWeight { get; set; }
            public decimal GrossWeight { get; set; }

            public int InnerPack { get; set; }
            public bool ForzarInnerPack { get; set; }

            public string MadeIn { get; set; }
            public string CodiMercancia { get; set; }

            public bool ImageExists { get; set; }

            public string ImageUrl(bool AbsoluteUrl = false)
            {
                return MmoUrl.image(Defaults.ImgTypes.art, base.Guid, AbsoluteUrl);
            }

            public DTOProductSku ToProductSku()
            {
                DTOProductSku retval = new DTOProductSku(this.Guid);
                return retval;
            }
        }
    }
}
