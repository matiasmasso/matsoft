using System;

namespace DTO
{
    public class DTOMsg : DTOBaseGuid
    {
        public int Id { get; set; }
        public DTOUser User { get; set; }
        public DateTime Fch { get; set; }
        public string Text { get; set; }

        public DTOMsg() : base()
        {
        }

        public DTOMsg(Guid oGuid) : base(oGuid)
        {
        }
    }
}
