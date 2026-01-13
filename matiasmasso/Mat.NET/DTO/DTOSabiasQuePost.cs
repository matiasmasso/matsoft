using System;

namespace DTO
{
    public class DTOSabiasQuePost : DTONoticia
    {
        public string FriendlyUrl { get; set; }

        public DTOSabiasQuePost() : base()
        {
            base.Src = Srcs.SabiasQue;
        }

        public DTOSabiasQuePost(Guid oGuid) : base(oGuid)
        {
            base.Src = Srcs.SabiasQue;
        }
    }
}
