namespace DTO
{
    public class DTOValueNom
    {
        public int value { get; set; }
        public string nom { get; set; }

        public DTOValueNom(int Value, string Nom) : base()
        {
            value = Value;
            nom = Nom;
        }
    }
}
