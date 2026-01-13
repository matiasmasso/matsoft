using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CertificatIrpfModel:BaseGuid
    {
        public int Year { get; set; }
        public Guid Contact { get; set; }
        public DocfileModel Docfile { get; set; }

        public CertificatIrpfModel():base() { }
        public CertificatIrpfModel(Guid guid):base(guid) { }

        public string PdfUrl() => Globals.RemoteApiUrl("CertificatIrpf/pdf", Guid.ToString());

    }
}
