using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOCatalog : List<DTOCatalog.Brand>
    {

        public static DTOCatalog Factory(DTOCustomerTarifa oTarifa)
        {
            DTOCatalog retval = new DTOCatalog();
            foreach (var oBrand in oTarifa.Brands)
            {
                DTOCatalog.Brand pBrand = new DTOCatalog.Brand(oBrand.Guid, oBrand.Nom.Esp);
                retval.Add(pBrand);
                foreach (var oCategory in oBrand.Categories)
                {
                    var pCategory = new DTOCatalog.Category(oCategory.Guid, oCategory.Nom.Esp);
                    pBrand.Categories.Add(pCategory);
                    foreach (var oSku in oCategory.Skus)
                    {
                        var pSku = new DTOCatalog.Sku(oSku.Guid, oSku.nomCurtOrNom());
                        {
                            var withBlock = pSku;
                            withBlock.Id = oSku.Id;
                            withBlock.@Ref = oSku.RefProveidor;
                            withBlock.NomPrv = oSku.NomProveidor;
                            withBlock.Ean = oSku.Ean13;
                            withBlock.Price = CompactAmt.Factory(oSku.Price);
                            withBlock.CustomerDto = oSku.CustomerDto;
                            withBlock.Obsoleto = oSku.obsoleto;
                        }
                        pCategory.Skus.Add(pSku);
                    }
                }
            }
            return retval;
        }



        public static DTOCatalog Factory(List<DTOProductBrand> oBrands, DTOLang oLang)
        {
            DTOCatalog retval = new DTOCatalog();
            foreach (var oBrand in oBrands)
            {
                DTOCatalog.Brand pBrand = new DTOCatalog.Brand(oBrand.Guid, oBrand.Nom.Esp);
                retval.Add(pBrand);
                foreach (var oCategory in oBrand.Categories)
                {
                    var pCategory = new DTOCatalog.Category(oCategory.Guid, oCategory.Nom.Esp);
                    pBrand.Categories.Add(pCategory);
                    foreach (var oSku in oCategory.Skus)
                    {
                        var pSku = new DTOCatalog.Sku(oSku.Guid, oSku.nomCurtOrNom());
                        {
                            var withBlock = pSku;
                            withBlock.Id = oSku.Id;
                            withBlock.@Ref = oSku.RefProveidor;
                            withBlock.NomPrv = oSku.NomProveidor;
                            withBlock.NomCurt = oSku.Nom.Tradueix(oLang);
                            withBlock.Ean = oSku.Ean13;
                            withBlock.Stock = oSku.Stock;
                            withBlock.CustomersPending = oSku.Clients - oSku.ClientsEnProgramacio;
                            withBlock.SuppliersPending = oSku.Proveidors;
                            withBlock.Price = CompactAmt.Factory(oSku.Price);
                            withBlock.CustomerDto = oSku.CustomerDto;
                            withBlock.Obsoleto = oSku.obsoleto;
                        }
                        pCategory.Skus.Add(pSku);
                    }
                }
            }
            return retval;
        }

        public static DTOCatalog Factory(List<DTOProductSku> oSkus, DTOLang oLang)
        {
            DTOCatalog retval = new DTOCatalog();
            DTOCatalog.Brand oBrand = new DTOCatalog.Brand(Guid.NewGuid());
            DTOCatalog.Category oCategory = new DTOCatalog.Category(Guid.NewGuid());
            foreach (var oSku in oSkus)
            {
                if (!oSku.Category.Guid.Equals(oCategory.Guid))
                {
                    if (!oSku.Category.Brand.Guid.Equals(oBrand.Guid))
                    {
                        oBrand = new DTOCatalog.Brand(oSku.Category.Brand.Guid);
                        oBrand.Nom = oSku.Category.Brand.Nom.Esp;
                        retval.Add(oBrand);
                    }
                    oCategory = new DTOCatalog.Category(oSku.Category.Guid);
                    oCategory.Nom = oSku.Category.Nom.Esp;
                    oBrand.Categories.Add(oCategory);
                }
                var pSku = new DTOCatalog.Sku(oSku.Guid, oSku.nomCurtOrNom());
                {
                    var withBlock = pSku;
                    withBlock.Id = oSku.Id;
                    withBlock.@Ref = oSku.RefProveidor;
                    withBlock.NomPrv = oSku.NomProveidor;
                    withBlock.Ean = oSku.Ean13;
                    withBlock.Price = CompactAmt.Factory(oSku.Price);
                    withBlock.CustomerDto = oSku.CustomerDto;
                    withBlock.Stock = oSku.Stock;
                    withBlock.CustomersPending = oSku.Clients;
                    withBlock.SuppliersPending = oSku.Proveidors;
                    withBlock.Obsoleto = oSku.obsoleto;
                }
                oCategory.Skus.Add(pSku);
            }
            return retval;
        }

        //public string Serialized()
        //{
        //    var serializer = new JavaScriptSerializer();
        //    string retval = serializer.Serialize(this);
        //    return retval;
        //}


        public MatHelper.Excel.Sheet Excel(List<Exception> exs) //to deprecate to DTOCatalog
        {
            MatHelper.Excel.Sheet retval = new MatHelper.Excel.Sheet("Stocks", "M+O stocks " + VbUtilities.Format(DTO.GlobalVariables.Now(), "yyyyMMddThhmmssfffZ"));
            {
                retval.AddColumn("Code", MatHelper.Excel.Cell.NumberFormats.W50);
                retval.AddColumn("Product", MatHelper.Excel.Cell.NumberFormats.W50);
                retval.AddColumn("Stock", MatHelper.Excel.Cell.NumberFormats.Integer);
                retval.AddColumn("Customers", MatHelper.Excel.Cell.NumberFormats.Integer);
                retval.AddColumn("Supplier", MatHelper.Excel.Cell.NumberFormats.Integer);
                retval.AddColumn("Available", MatHelper.Excel.Cell.NumberFormats.Integer);
            }

            foreach (var oCompactBrand in this)
            {
                foreach (var oCompactCategory in oCompactBrand.Categories)
                {
                    foreach (var oCompactSku in oCompactCategory.Skus)
                    {
                        var iAvailable = Math.Max(0, oCompactSku.Stock - (oCompactSku.CustomersPending));

                        MatHelper.Excel.Row oRow = retval.AddRow();
                        oRow.AddCell(oCompactSku.@Ref);
                        oRow.AddCell(oCompactSku.NomPrv);
                        oRow.AddCell(oCompactSku.Stock);
                        oRow.AddCell(oCompactSku.CustomersPending);
                        oRow.AddCell(oCompactSku.SuppliersPending);
                        oRow.AddCell(iAvailable);
                    }
                }
            }
            return retval;
        }



        public List<DTOProductBrand> toProductBrands()
        {

            List<DTOProductBrand> retval = new List<DTOProductBrand>();
            foreach (var oCompactBrand in this)
            {
                DTOProductBrand oBrand = new DTOProductBrand(oCompactBrand.Guid);
                oBrand.Nom.Esp = oCompactBrand.Nom;
                retval.Add(oBrand);
                foreach (var oCompactCategory in oCompactBrand.Categories)
                {
                    DTOProductCategory oCategory = new DTOProductCategory(oCompactCategory.Guid);
                    {
                        var withBlock = oCategory;
                        withBlock.Brand = oBrand;
                        withBlock.Nom.Esp = oCompactCategory.Nom;
                    }
                    oBrand.Categories.Add(oCategory);
                    foreach (var oCompactSku in oCompactCategory.Skus)
                    {
                        DTOProductSku oSku = new DTOProductSku(oCompactSku.Guid);
                        {
                            var withBlock = oSku;
                            withBlock.Category = oCategory;
                            withBlock.Id = oCompactSku.@Id;
                            withBlock.Ean13 = oCompactSku.Ean;
                            withBlock.RefProveidor = oCompactSku.@Ref;
                            withBlock.NomProveidor = oCompactSku.NomPrv;
                            withBlock.Nom.Esp = oCompactSku.NomCurt;
                            withBlock.obsoleto = oCompactSku.Obsoleto;
                        }
                        oCategory.Skus.Add(oSku);
                    }
                }
            }
            return retval;
        }



        public class Brand
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public DTOProductBrand.CodDists CodDist { get; set; }
            public Boolean Obsoleto { get; set; }
            public List<Category> Categories { get; set; }

            public Brand()
            {
                this.Categories = new List<Category>();
            }

            public Brand(Guid guid, string nom = "")
            {
                if (guid == null)
                    guid = Guid.NewGuid();
                this.Guid = guid;
                this.Nom = nom;
                this.Categories = new List<Category>();
            }

        }
        public class Category
        {
            public Guid Guid { get; set; }
            public string Nom { get; set; }
            public DTOProductBrand.CodDists CodDist { get; set; }
            public Boolean IsBundle { get; set; }
            public Boolean Obsoleto { get; set; }
            public List<Sku> Skus { get; set; }

            public Category() { }
            public Category(Guid guid, string nom = "")
            {
                if (guid == null)
                    guid = Guid.NewGuid();
                this.Guid = guid;
                this.Nom = nom;
                this.Skus = new List<Sku>();
            }

            public new string ToString()
            {
                return this.Nom;
            }

        }
        public class Sku
        {
            public Guid Guid { get; set; }
            public int Id { get; set; }
            public string NomCurt { get; set; }
            public string Ref { get; set; }
            public string CustomRef { get; set; }
            public string NomPrv { get; set; }
            public DTOEan Ean { get; set; }
            public int Stock { get; set; }
            public int CustomersPending { get; set; }
            public int SuppliersPending { get; set; }
            public CompactAmt Price { get; set; }
            public Decimal CustomerDto { get; set; }
            public Boolean Obsoleto { get; set; }

            public Sku() { }
            public Sku(Guid guid, string nomCurt = "")
            {
                if (guid == null)
                    guid = Guid.NewGuid();
                this.Guid = guid;
                this.NomCurt = nomCurt;
            }

        }

        public class CompactAmt
        {
            public CompactCur Cur { get; set; }
            public Decimal Eur { get; set; }

            public static CompactAmt Factory(DTOAmt oAmt)
            {
                CompactAmt retval = null;
                if (oAmt != null)
                {
                    retval = new CompactAmt();
                    retval.Eur = oAmt.Eur;
                    retval.Cur = new CompactCur() { Tag = oAmt.Cur.Tag };
                }
                return retval;
            }
        }

        public class CompactCur
        {
            public string Tag = "EUR";
        }
    }
}
