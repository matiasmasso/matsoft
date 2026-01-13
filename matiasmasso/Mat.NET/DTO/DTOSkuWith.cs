using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace DTO
{
    public class DTOSkuWith
    {
        public DTOProductSku Parent { get; set; }
        public DTOProductSku Child { get; set; }
        public int Qty { get; set; }

    }

}
