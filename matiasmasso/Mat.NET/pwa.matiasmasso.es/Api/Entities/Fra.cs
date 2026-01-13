using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Issued invoices
    /// </summary>
    public partial class Fra
    {
        public Fra()
        {
            Albs = new HashSet<Alb>();
            Rps = new HashSet<Rp>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company. Foreign key to Emp table
        /// </summary>
        public int Emp { get; set; }
        /// <summary>
        /// Year of the invoice
        /// </summary>
        public short Yea { get; set; }
        /// <summary>
        /// Set of invoices that share a unique numeration each year and Company. Enumerable DTOInvoice.Series (standard, rectificativa, simplificada...)
        /// </summary>
        public int Serie { get; set; }
        /// <summary>
        /// Invoice number, within each Company, Year and Serie
        /// </summary>
        public int Fra1 { get; set; }
        /// <summary>
        /// Customer. Foreign key to CliGral table
        /// </summary>
        public Guid CliGuid { get; set; }
        /// <summary>
        /// Invoice date
        /// </summary>
        public DateTime Fch { get; set; }
        /// <summary>
        /// Customer name
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// Customer primary VAT id
        /// </summary>
        public string? Nif { get; set; }
        /// <summary>
        /// Primary VAT Id document. Enumerable DTONif.Cods
        /// </summary>
        public int? NifCod { get; set; }
        /// <summary>
        /// Secondary VAT id. Used for Andorra and UK post-Brexit customers
        /// </summary>
        public string? Nif2 { get; set; }
        /// <summary>
        /// id document for secondary VAT id
        /// </summary>
        public int? Nif2Cod { get; set; }
        /// <summary>
        /// Address, without location and postal code
        /// </summary>
        public string? Adr { get; set; }
        /// <summary>
        /// Location and postal code, as foreign key for Zip table
        /// </summary>
        public Guid? Zip { get; set; }
        /// <summary>
        /// Payment method.Enumerable DTOPaymentTerms.CodsFormaDePago
        /// </summary>
        public short Cfp { get; set; }
        /// <summary>
        /// Payment method details
        /// </summary>
        public string Fpg { get; set; } = null!;
        /// <summary>
        /// Line 1 comments. Usually payment terms details
        /// </summary>
        public string Ob1 { get; set; } = null!;
        /// <summary>
        /// Line 2 comments
        /// </summary>
        public string Ob2 { get; set; } = null!;
        /// <summary>
        /// Line 3 comments
        /// </summary>
        public string Ob3 { get; set; } = null!;
        /// <summary>
        /// Due date
        /// </summary>
        public DateTime Vto { get; set; }
        /// <summary>
        /// Sum of all item amounts before taxes
        /// </summary>
        public decimal SumItems { get; set; }
        /// <summary>
        /// Currency (ISO 4217)
        /// </summary>
        public string Cur { get; set; } = null!;
        /// <summary>
        /// Tax base amount in Euro
        /// </summary>
        public decimal EurBase { get; set; }
        /// <summary>
        /// Discount base
        /// </summary>
        public decimal DtoBase { get; set; }
        /// <summary>
        /// Discount percentage
        /// </summary>
        public decimal? DtoPct { get; set; }
        /// <summary>
        /// Discount amount
        /// </summary>
        public decimal DtoAmt { get; set; }
        /// <summary>
        /// Early payment discount base
        /// </summary>
        public decimal DppBase { get; set; }
        /// <summary>
        /// Early payment discount percentage
        /// </summary>
        public decimal DppPct { get; set; }
        /// <summary>
        /// Early payment discount amount
        /// </summary>
        public decimal DppAmt { get; set; }
        /// <summary>
        /// Standard VAT base
        /// </summary>
        public decimal IvaStdBase { get; set; }
        /// <summary>
        /// Standard VAT percentage
        /// </summary>
        public decimal IvaStdPct { get; set; }
        /// <summary>
        /// Standard VAT amount
        /// </summary>
        public decimal IvaStdAmt { get; set; }
        /// <summary>
        /// Standard Sales Equalization Tax  base amount
        /// </summary>
        public decimal ReqStdPct { get; set; }
        /// <summary>
        /// Standard Sales Equalization Tax amount
        /// </summary>
        public decimal ReqStdAmt { get; set; }
        /// <summary>
        /// Reduced VAT base
        /// </summary>
        public decimal IvaRedBase { get; set; }
        /// <summary>
        /// Reduced VAT rate
        /// </summary>
        public decimal IvaRedPct { get; set; }
        /// <summary>
        /// Reduced VAT amount
        /// </summary>
        public decimal IvaRedAmt { get; set; }
        /// <summary>
        /// Reduced Sales Equalization Tax rate
        /// </summary>
        public decimal ReqRedPct { get; set; }
        /// <summary>
        /// Reduced Sales Equalization Tax amount
        /// </summary>
        public decimal ReqRedAmt { get; set; }
        /// <summary>
        /// Super reduced VAT base
        /// </summary>
        public decimal IvaSuperRedBase { get; set; }
        /// <summary>
        /// Super reduced VAT rate
        /// </summary>
        public decimal IvaSuperRedPct { get; set; }
        /// <summary>
        /// Super reduced VAT amount
        /// </summary>
        public decimal IvaSuperRedAmt { get; set; }
        /// <summary>
        /// Super reduced Sales Equalization Tax rate
        /// </summary>
        public decimal ReqSuperRedPct { get; set; }
        /// <summary>
        /// Super reduced Sales Equalization Tax amount
        /// </summary>
        public decimal ReqSuperRedAmt { get; set; }
        /// <summary>
        /// Total cash, Euros
        /// </summary>
        public decimal EurLiq { get; set; }
        /// <summary>
        /// Total in foreign currency
        /// </summary>
        public decimal PtsLiq { get; set; }
        /// <summary>
        /// Number of promotional points earned
        /// </summary>
        public decimal PuntsQty { get; set; }
        /// <summary>
        /// Base amount to calculate promotional points
        /// </summary>
        public decimal PuntsBase { get; set; }
        /// <summary>
        /// Promotional points rate
        /// </summary>
        public decimal PuntsTipus { get; set; }
        /// <summary>
        /// Accounts entry; foreign key for Cca table
        /// </summary>
        public Guid? CcaGuid { get; set; }
        /// <summary>
        /// Enumerable DTOInvoice.PrintModes (0.pending, 1.No print, 2.Printer, 3.Email, 4.Edi)
        /// </summary>
        public short PrintMode { get; set; }
        /// <summary>
        /// Validation code returned by Spanish Tax autorities when submiting the invoice notification
        /// </summary>
        public string? SiiCsv { get; set; }
        /// <summary>
        /// Date the invoice was notified to Spanish tax authorities
        /// </summary>
        public DateTime? SiiFch { get; set; }
        /// <summary>
        /// Error, if any, thrown when submitting the invoice to tax authorities
        /// </summary>
        public string? SiiErr { get; set; }
        /// <summary>
        /// Enumerable DTOInvoice.SiiResults(1.Success, 2.Accepted with errors, 3.Rejected)
        /// </summary>
        public int SiiResult { get; set; }
        /// <summary>
        /// Enumerable DTOInvoice.Conceptes (1.Sales, 2.Services...)
        /// </summary>
        public int Concepte { get; set; }
        /// <summary>
        /// VAT exemption reason tax authorities code  (E2, E5...)
        /// </summary>
        public string? SiiL9 { get; set; }
        /// <summary>
        /// Invoice type tax authorities  (F1, F2...)
        /// </summary>
        public string TipoFactura { get; set; } = null!;
        /// <summary>
        /// Tax code required by tax authorities
        /// </summary>
        public string? RegimenEspecialOtrascendencia { get; set; }
        /// <summary>
        /// Import/Export Customs declaration. Foreign key to Intrastat table
        /// </summary>
        public Guid? Intrastat { get; set; }
        /// <summary>
        /// International Commerce Terms. foreign key for Incoterms table
        /// </summary>
        public string? Incoterm { get; set; }
        /// <summary>
        /// Enumerable DTOInvoice.ExportCods (1.National, 2.EU, 3.rest)
        /// </summary>
        public int? ExportCod { get; set; }
        /// <summary>
        /// ISO 639-2 language code
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }
        /// <summary>
        /// Date this entry was edited for last time
        /// </summary>
        public DateTime FchLastEdited { get; set; }
        /// <summary>
        /// Date this invoice was printed for last time
        /// </summary>
        public DateTime? FchLastPrinted { get; set; }
        /// <summary>
        /// Date this invoice was emailed
        /// </summary>
        public Guid? EmailedToGuid { get; set; }
        /// <summary>
        /// User who printed this invoice for last time
        /// </summary>
        public Guid? UsrLastPrintedGuid { get; set; }

        public virtual Cca? CcaGu { get; set; }
        public virtual CliGral CliGu { get; set; } = null!;
        public virtual Email? EmailedToGu { get; set; }
        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual Incoterm? IncotermNavigation { get; set; }
        public virtual Intrastat? IntrastatNavigation { get; set; }
        public virtual Email? UsrLastPrintedGu { get; set; }
        public virtual Zip? ZipNavigation { get; set; }
        public virtual ICollection<Alb> Albs { get; set; }
        public virtual ICollection<Rp> Rps { get; set; }
    }
}
