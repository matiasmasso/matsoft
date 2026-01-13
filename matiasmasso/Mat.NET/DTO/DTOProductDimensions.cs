namespace DTO
{
    public class DTOProductDimensions
    {
        public bool Hereda { get; set; }
        public bool NoDimensions { get; set; }
        public decimal KgNet { get; set; }
        public decimal KgBrut { get; set; }
        public decimal M3 { get; set; }
        public decimal DimensionAlto { get; set; }
        public decimal DimensionAncho { get; set; }
        public decimal DimensionLargo { get; set; }
        public int InnerPack { get; set; }
        public int OuterPack { get; set; }
        public bool ForzarInnerPack { get; set; }
        public DTOCodiMercancia CodiMercancia { get; set; }
        public DTOEan PackageEan { get; set; }


        public static DTOProductDimensions DimensionLess()
        {
            DTOProductDimensions oRetVal = new DTOProductDimensions();
            {
                var withBlock = oRetVal;
                withBlock.NoDimensions = true;
                withBlock.Hereda = false;
            }
            return oRetVal;
        }
    }
}
