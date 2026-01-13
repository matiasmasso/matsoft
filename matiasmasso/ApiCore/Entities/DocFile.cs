using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Documents
/// </summary>
public partial class DocFile
{
    /// <summary>
    /// Primary key, MD5 hash signature of the document
    /// </summary>
    public string Hash { get; set; } = null!;

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

    public int ThumbnailMime { get; set; }

    public string? Esp { get; set; }

    public string? Cat { get; set; }

    public string? Eng { get; set; }

    public string? Por { get; set; }

    public byte[]? Thumb140 { get; set; }

    public string? Sha256 { get; set; }

    public string? Md5 { get; set; }

    public virtual ICollection<Aeat> Aeats { get; set; } = new List<Aeat>();

    public virtual ICollection<Alb> Albs { get; set; } = new List<Alb>();

    public virtual ICollection<Cca> Ccas { get; set; } = new List<Cca>();

    public virtual ICollection<CertificatIrpf> CertificatIrpfs { get; set; } = new List<CertificatIrpf>();

    public virtual ICollection<CliDoc> CliDocs { get; set; } = new List<CliDoc>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Crr> Crrs { get; set; } = new List<Crr>();

    public virtual ICollection<DocFileLog> DocFileLogs { get; set; } = new List<DocFileLog>();

    public virtual ICollection<DocFileSrc> DocFileSrcs { get; set; } = new List<DocFileSrc>();

    public virtual ICollection<Escriptura> Escripturas { get; set; } = new List<Escriptura>();

    public virtual ICollection<ImportDtl> ImportDtls { get; set; } = new List<ImportDtl>();

    public virtual ICollection<IncidenciaDocFile> IncidenciaDocFiles { get; set; } = new List<IncidenciaDocFile>();

    public virtual ICollection<Intrastat> Intrastats { get; set; } = new List<Intrastat>();

    public virtual ICollection<PriceListSupplier> PriceListSuppliers { get; set; } = new List<PriceListSupplier>();

    public virtual ICollection<ProductDownload> ProductDownloads { get; set; } = new List<ProductDownload>();

    public virtual ICollection<SepaMe> SepaMes { get; set; } = new List<SepaMe>();
}
