namespace DTO
{
    public class DTOProductArea
    {
        public DTOArea Area { get; set; }
        public DTOProduct Product { get; set; }


        public static DTOProductArea Factory(DTOProduct oProduct, DTOArea oArea)
        {
            DTOProductArea retval = new DTOProductArea();
            {
                var withBlock = retval;
                withBlock.Product = oProduct;
                withBlock.Area = oArea;
            }
            return retval;
        }
    }

    public class DTOProductAreaQty
    {
        public DTOArea Area { get; set; }
        public DTOProduct Product { get; set; }
        public int Qty { get; set; }
    }
}
