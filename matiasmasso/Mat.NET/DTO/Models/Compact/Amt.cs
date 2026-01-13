namespace DTO.Models.Compact
{
    public class Amt
    {
        public decimal Eur { get; set; }

        public static Amt Factory(DTOAmt value)
        {
            Amt retval = null;
            if (value != null)
            {
                retval = new Amt();
                retval.Eur = value.Eur;
            }
            return retval;
        }
    }
}
