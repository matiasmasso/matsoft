namespace DTO
{
    public class DTOIntrastatException
    {
        public Codis Codi { get; set; }
        public object Tag { get; set; }

        public enum Codis
        {
            NotSet,
            CodiMercancia,
            Weight,
            Amount
        }

        public DTOIntrastatException(Codis oCodi, object oTag)
        {
            Codi = oCodi;
            Tag = oTag;
        }
    }
}
