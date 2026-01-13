using System;

namespace DTO
{
    public class DTOContactMessage : DTOBaseGuid
    {
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Location { get; set; }
        public string Text { get; set; }
        public DateTime FchCreated { get; set; }

        public DTOContactMessage() : base()
        {
        }

        public DTOContactMessage(Guid oGuid) : base(oGuid)
        {
        }
    }
}
