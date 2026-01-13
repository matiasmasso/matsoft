using System;

namespace DTO
{
    public class DTOInvoicePrintLog
    {
        public DateTime Fch { get; set; }
        public DTOEdiversaFile EdiversaFile { get; set; }
        public DTOInvoice Invoice { get; set; }
        public DTOInvoice.PrintModes PrintMode { get; set; }
        public DTOUser WinUser { get; set; }
        public DTOUser DestUser { get; set; }
    }
}
