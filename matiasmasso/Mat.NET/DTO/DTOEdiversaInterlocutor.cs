namespace DTO
{
    public class DTOEdiversaInterlocutor
    {
        public string Ean { get; set; }
        public string Nom { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Zip { get; set; }


        public DTOEdiversaInterlocutor(string segment) : base()
        {
            var fields = segment.Split('|');
            if (fields.Length > 1)
            {
                Ean = fields[1];
                if (fields.Length > 2)
                {
                    Nom = fields[2];
                    if (fields.Length > 3)
                    {
                        Address = fields[3];
                        if (fields.Length > 4)
                        {
                            Location = fields[4];
                            if (fields.Length > 5)
                                Zip = fields[5];
                        }
                    }
                }
            }
        }
    }
}
