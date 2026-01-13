using System;

namespace DTO
{
    public class DTOException : DTOBaseGuid
    {
        public int Cod { get; set; }
        public string Msg { get; set; }
        public DTOBaseGuid Tag { get; set; }

        public DTOException() : base()
        {
        }

        public DTOException(Guid oGuid) : base(oGuid)
        {
        }

    }
}
