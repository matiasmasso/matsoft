using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Received invoices book, including SII (Suministro inmediato de información) data
/// </summary>
public partial class BookFra
{
    /// <summary>
    /// Accounts entry; primary key and also foreign key for Cca table
    /// </summary>
    public Guid CcaGuid { get; set; }

    /// <summary>
    /// Supplier, foreign key for CliGral table
    /// </summary>
    public Guid? ContactGuid { get; set; }

    /// <summary>
    /// Account; foreign key to PgcCta table
    /// </summary>
    public Guid? CtaGuid { get; set; }

    /// <summary>
    /// Invoice number
    /// </summary>
    public string FraNum { get; set; } = null!;

    /// <summary>
    /// Date this entry was cresated
    /// </summary>
    public DateTime FchCreated { get; set; }

    /// <summary>
    /// Tax base for VAT type TipoIva, if any
    /// </summary>
    public decimal? BaseSujeta { get; set; }

    /// <summary>
    /// VAT rate
    /// </summary>
    public decimal? TipoIva { get; set; }

    /// <summary>
    /// VAT charge
    /// </summary>
    public decimal? QuotaIva { get; set; }

    /// <summary>
    /// Tax base for VAT type TipoIva1, if any
    /// </summary>
    public decimal? BaseSujeta1 { get; set; }

    /// <summary>
    /// VAT rate
    /// </summary>
    public decimal? TipoIva1 { get; set; }

    /// <summary>
    /// VAT charge
    /// </summary>
    public decimal? QuotaIva1 { get; set; }

    /// <summary>
    /// Tax base for VAT type TipoIva2, if any
    /// </summary>
    public decimal? BaseSujeta2 { get; set; }

    /// <summary>
    /// VAT rate
    /// </summary>
    public decimal? TipoIva2 { get; set; }

    /// <summary>
    /// VAT charge
    /// </summary>
    public decimal? QuotaIva2 { get; set; }

    /// <summary>
    /// Tax exempt base, if any
    /// </summary>
    public decimal? BaseExenta { get; set; }

    /// <summary>
    /// Spanish tax authorities code for Tax exempt causes
    /// </summary>
    public string? ClaveExenta { get; set; }

    /// <summary>
    /// Irpf (tax withholdings) base, if any
    /// </summary>
    public decimal? BaseIrpf { get; set; }

    /// <summary>
    /// Irpf rate
    /// </summary>
    public decimal? TipoIrpf { get; set; }

    /// <summary>
    /// Irpf charge
    /// </summary>
    public decimal Irpf { get; set; }

    /// <summary>
    /// Spanish tax authorities &quot;Tipo de Factura&quot; codes (F1,F2,F3,F4,F5,F6,R1)
    /// </summary>
    public string TipoFra { get; set; } = null!;

    /// <summary>
    /// Short description of the purpose of the invoice
    /// </summary>
    public string? Dsc { get; set; }

    /// <summary>
    /// Enumerable DTOSiiLog.Results (1.Correcto, 2.Parcialmente correcto, 3.Incorrecto, 4.ErrorDeComunicacion)
    /// </summary>
    public int SiiResult { get; set; }

    /// <summary>
    /// Date it was logged to Sii (Suministro Inmediato de Información)
    /// </summary>
    public DateTime? SiiFch { get; set; }

    /// <summary>
    /// Codigo Seguro de Validación. Spanish tax authorities return code after notifying this invoice
    /// </summary>
    public string? SiiCsv { get; set; }

    /// <summary>
    /// Errors, if any, on submitting the invoice to Sii
    /// </summary>
    public string? SiiErr { get; set; }

    /// <summary>
    /// Status code returned by tax authorities indicating if it matches the data notified by the invoice issuer
    /// </summary>
    public int? SiiEstadoCuadre { get; set; }

    /// <summary>
    /// Date SiiEstadoCuadre field was returned by tax authorities
    /// </summary>
    public DateTime? SiiTimestampEstadoCuadre { get; set; }

    /// <summary>
    /// Date of last amendment
    /// </summary>
    public DateTime? SiiTimestampUltimaModificacion { get; set; }

    /// <summary>
    /// Error code returned by Sii service
    /// </summary>
    public int? SiiErrCod { get; set; }

    /// <summary>
    /// Enumerable from Sii service &quot;L3.1 Clave de régimen especial o trascendencia en facturas recibidas&quot;
    /// </summary>
    public string? ClaveRegimenEspecialOtrascendencia { get; set; }

    public virtual Cca Cca { get; set; } = null!;

    public virtual CliGral? Contact { get; set; }

    public virtual PgcCtum? Cta { get; set; }
}
