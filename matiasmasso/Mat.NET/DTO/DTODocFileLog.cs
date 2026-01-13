using System;

namespace DTO
{

    public class DTODocFileLog
    {
        public DTODocFile DocFile { get; set; }
        public DTOUser User { get; set; }
        public DateTime Fch { get; set; }
    }
}
