using System;

namespace DTO
{
    public class DTOCertificatIrpf : DTOBaseGuid
    {
        public string filename { get; set; }
        public string Nif { get; set; }
        public string Nom { get; set; }
        public DTODocFile DocFile { get; set; }
        public DTOContact Contact { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }


        public DTOCertificatIrpf() : base()
        {
        }

        public DTOCertificatIrpf(Guid oGuid) : base(oGuid)
        {
        }


        public static DTOCertificatIrpf Factory(string sFilename)
        {
            DTOCertificatIrpf retval = new DTOCertificatIrpf();
            retval.filename = sFilename;
            return retval;
        }


        public string FullPeriod()
        {
            var retval = Year.ToString();
            if (Period == 0)
                retval = string.Format("{0:0000}", Year);
            else
                retval = string.Format("{0:0000}-{1:00}", Year, Period);
            return retval;
        }
    }
}
