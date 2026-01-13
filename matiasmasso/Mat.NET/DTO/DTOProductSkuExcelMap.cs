namespace DTO
{
    public class DTOProductSkuExcelMap
    {
        public int SheetCol { get; set; }
        public string ColHeader { get; set; }
        public SkuFields SkuField { get; set; }

        public enum SkuFields
        {
            Ref_proveidor,
            Descripcio_proveidor,
            Ean_producte,
            Ean_packaging,
            Amplada_mm,
            Longitut_mm,
            Alçada_mm,
            Pes_net_grams,
            Pes_brut_grams,
            Moq
        }

        public static string FieldName(SkuFields oField)
        {
            return oField.ToString().Replace("_", " ");
        }

        public static DTOProductSkuExcelMap Factory(int sheetcol, string colheader, SkuFields skufield)
        {
            DTOProductSkuExcelMap retval = new DTOProductSkuExcelMap();
            {
                var withBlock = retval;
                withBlock.SheetCol = sheetcol;
                withBlock.ColHeader = colheader;
                withBlock.SkuField = skufield;
            }
            return retval;
        }
    }
}
