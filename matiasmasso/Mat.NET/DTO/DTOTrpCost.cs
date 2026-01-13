namespace DTO
{
    public class DTOTrpCost
    {
        public DTOTrpZon Parent { get; set; }
        public decimal HastaKg { get; set; }
        public decimal Eur { get; set; }
        public decimal FixEur { get; set; }
        public decimal MinEur { get; set; }
        public decimal Suplidos { get; set; }
        public Codis Cod { get; set; }

        public enum Codis
        {
            CostFinsAKg,
            CostPerKg
        }

    }
}
