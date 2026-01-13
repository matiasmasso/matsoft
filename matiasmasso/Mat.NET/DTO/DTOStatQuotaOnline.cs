namespace DTO
{
    public class DTOStatQuotaOnline
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public DTOAmt Base { get; set; }
        public DTOAmt Online { get; set; }

        public static decimal Share(DTOStatQuotaOnline oQuota)
        {
            decimal retval = 0;
            if (oQuota.Base.Eur != 0)
                retval = oQuota.Online.Eur / oQuota.Base.Eur;
            return retval;
        }
    }
}
