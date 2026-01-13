namespace DTO
{
    public class DTOQtyDto
    {
        public int Qty { get; set; }
        public decimal Dto { get; set; }
        public int FreeUnits { get; set; }

        public DTOQtyDto(int iQty = 0, decimal DcDto = 0, int iFreeUnits = 0) : base()
        {
            Qty = iQty;
            Dto = DcDto;
            FreeUnits = iFreeUnits;
        }
    }
}
