using System;

namespace DTO
{
    public class DTOIncoterm
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public static DTOIncoterm Factory(String tag)
        {
            DTOIncoterm retval = new DTOIncoterm();
            retval.Id = tag;
            return retval;
        }
    }

}
