using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Documents
    /// </summary>
    public partial class DocFile
    {
        public DocFile()
        {
            Aeats = new HashSet<Aeat>();
            Albs = new HashSet<Alb>();
            Ccas = new HashSet<Cca>();
            CertificatIrpfs = new HashSet<CertificatIrpf>();
            CliDocs = new HashSet<CliDoc>();
            Contracts = new HashSet<Contract>();
            Crrs = new HashSet<Crr>();
            DocFileSrcs = new HashSet<DocFileSrc>();
            Escripturas = new HashSet<Escriptura>();
            ImportDtls = new HashSet<ImportDtl>();
            IncidenciaDocFiles = new HashSet<IncidenciaDocFile>();
            Intrastats = new HashSet<Intrastat>();
            PdcEtiquetesTransportNavigations = new HashSet<Pdc>();
            PdcHashNavigations = new HashSet<Pdc>();
            PremiumCustomers = new HashSet<PremiumCustomer>();
            PriceListSuppliers = new HashSet<PriceListSupplier>();
            ProductDownloads = new HashSet<ProductDownload>();
            SepaMes = new HashSet<SepaMe>();
        }

        /// <summary>
        /// Enumerable MatHelperStd.MimeCods
        /// </summary>
        public int Mime { get; set; }
        /// <summary>
        /// Bytes count
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Pages count, if any
        /// </summary>
        public int Pags { get; set; }
        /// <summary>
        /// Docuemnt width, in pixels
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Document height, in pixels
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Horizontal resolution, if applicable
        /// </summary>
        public int Hres { get; set; }
        /// <summary>
        /// Vertical resoli¡ution, if applicable
        /// </summary>
        public int Vres { get; set; }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// If true, the document is outdated
        /// </summary>
        public bool Obsolet { get; set; }
        /// <summary>
        /// Thumbnail 350x400 px
        /// </summary>
        public byte[]? Thumbnail { get; set; }
        /// <summary>
        /// Stream as byte array
        /// </summary>
        public byte[]? Doc { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Primary key, MD5 hash signature of the document
        /// </summary>
        public string Hash { get; set; } = null!;
        public int ThumbnailMime { get; set; }

        public virtual ICollection<Aeat> Aeats { get; set; }
        public virtual ICollection<Alb> Albs { get; set; }
        public virtual ICollection<Cca> Ccas { get; set; }
        public virtual ICollection<CertificatIrpf> CertificatIrpfs { get; set; }
        public virtual ICollection<CliDoc> CliDocs { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Crr> Crrs { get; set; }
        public virtual ICollection<DocFileSrc> DocFileSrcs { get; set; }
        public virtual ICollection<Escriptura> Escripturas { get; set; }
        public virtual ICollection<ImportDtl> ImportDtls { get; set; }
        public virtual ICollection<IncidenciaDocFile> IncidenciaDocFiles { get; set; }
        public virtual ICollection<Intrastat> Intrastats { get; set; }
        public virtual ICollection<Pdc> PdcEtiquetesTransportNavigations { get; set; }
        public virtual ICollection<Pdc> PdcHashNavigations { get; set; }
        public virtual ICollection<PremiumCustomer> PremiumCustomers { get; set; }
        public virtual ICollection<PriceListSupplier> PriceListSuppliers { get; set; }
        public virtual ICollection<ProductDownload> ProductDownloads { get; set; }
        public virtual ICollection<SepaMe> SepaMes { get; set; }
    }
}
