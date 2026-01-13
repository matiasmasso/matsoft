namespace DTO.Models.Base
{
    public class IdNom
    {
        public int Id { get; }
        public string Nom { get; set; }

        public IdNom(int id, string nom = "")
        {
            Id = id;
            Nom = nom;
        }
    }
}
