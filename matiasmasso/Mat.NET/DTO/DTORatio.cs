namespace DTO
{
    public class DTORatio
    {
        public string Nom { get; set; }
        public string Dsc { get; set; }
        public decimal Value { get; set; }

        public Formatos Formato { get; set; }

        public enum Formatos
        {
            Eur,
            Ratio,
            Percent
        }
    }
}
