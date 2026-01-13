using System;

namespace DTO
{
    public class DTOLiniaTelefon : DTOBaseGuid
    {
        public string num { get; set; }
        public string alias { get; set; }
        public DTOContact contact { get; set; }
        public string icc { get; set; }
        public string imei { get; set; }
        public string puk { get; set; }
        public DateTime alta { get; set; }
        public DateTime baixa { get; set; }
        public bool privat { get; set; }


        public DTOLiniaTelefon() : base()
        {
        }

        public DTOLiniaTelefon(Guid oGuid) : base(oGuid)
        {
        }


        public class Consum : DTOBaseGuid
        {
            public DTOLiniaTelefon linia { get; set; }
            public DTODocFile docFile { get; set; }
            public DTOYearMonth yearMonth { get; set; }

            public Consum() : base()
            {
            }

            public Consum(Guid oGuid) : base(oGuid)
            {
            }

            public static DTOLiniaTelefon.Consum Factory(DTOLiniaTelefon oLiniaTelefon, DTOYearMonth oYearMonth, DTODocFile oDocFile)
            {
                DTOLiniaTelefon.Consum retval = new DTOLiniaTelefon.Consum();
                {
                    var withBlock = retval;
                    withBlock.linia = oLiniaTelefon;
                    withBlock.yearMonth = oYearMonth;
                    withBlock.docFile = oDocFile;
                }
                return retval;
            }
        }
    }
}
