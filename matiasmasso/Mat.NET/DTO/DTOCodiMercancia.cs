namespace DTO
{
    public class DTOCodiMercancia
    {
        private static string EmptyValue = new string('0', 8);

        public string Id { get; set; }
        public string Dsc { get; set; }

        public bool IsLoaded { get; set; }

        public DTOCodiMercancia(string id) : base()
        {
            Id = id;
        }

        public static string FullNom(DTOCodiMercancia oCodiMercancia)
        {
            string retval = "";
            if (oCodiMercancia != null)
                retval = string.Format("{0} {1}", oCodiMercancia.Id, oCodiMercancia.Dsc);
            return retval;
        }
    }
}
