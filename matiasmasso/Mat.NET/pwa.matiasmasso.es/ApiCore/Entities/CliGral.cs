using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Contacts
/// </summary>
public partial class CliGral
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Company; foreign key for Emp table
    /// </summary>
    public int Emp { get; set; }

    /// <summary>
    /// Sequential order, unique for each Emp
    /// </summary>
    public int Cli { get; set; }

    /// <summary>
    /// Corporate name
    /// </summary>
    public string RaoSocial { get; set; } = null!;

    /// <summary>
    /// Friendly name
    /// </summary>
    public string Alias { get; set; } = null!;

    /// <summary>
    /// Primary tax id number (usually VAT number)
    /// </summary>
    public string Nif { get; set; } = null!;

    /// <summary>
    /// Type of primary tax id number, Enumerable DTONif.Cods
    /// </summary>
    public int? NifCod { get; set; }

    /// <summary>
    /// Secondary id number
    /// </summary>
    public string? Nif2 { get; set; }

    /// <summary>
    /// Type of secondary tax id number
    /// </summary>
    public int? Nif2Cod { get; set; }

    /// <summary>
    /// Commercial name
    /// </summary>
    public string? NomCom { get; set; }

    /// <summary>
    /// Searchkey by which this contact may be searched
    /// </summary>
    public string? NomKey { get; set; }

    /// <summary>
    /// Previous account from same contact
    /// </summary>
    public Guid? NomAnteriorGuid { get; set; }

    /// <summary>
    /// Account substituting current one
    /// </summary>
    public Guid? NomNouGuid { get; set; }

    /// <summary>
    /// Displayable name with commercial name and location
    /// </summary>
    public string FullNom { get; set; } = null!;

    /// <summary>
    /// Default rol for this contact users. Enumerable DTORol.Ids
    /// </summary>
    public short Rol { get; set; }

    /// <summary>
    /// ISO 639-2 language code
    /// </summary>
    public string LangId { get; set; } = null!;

    /// <summary>
    /// Global Location Number, format EAN 13. Used on EDI messages
    /// </summary>
    public string? Gln { get; set; }

    /// <summary>
    /// Website address
    /// </summary>
    public string? Web { get; set; }

    /// <summary>
    /// Main activity classification. Foreign key for ContactClass table
    /// </summary>
    public Guid? ContactClass { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string? Obs { get; set; }

    /// <summary>
    /// True if no longer active
    /// </summary>
    public bool Obsoleto { get; set; }

    /// <summary>
    /// Date this entry was created
    /// </summary>
    public DateTime FchCreated { get; set; }

    public virtual ICollection<ActiuMov> ActiuMovs { get; set; } = new List<ActiuMov>();

    public virtual ICollection<AlbBloqueig> AlbBloqueigs { get; set; } = new List<AlbBloqueig>();

    public virtual ICollection<Alb> AlbClis { get; set; } = new List<Alb>();

    public virtual ICollection<Alb> AlbFacturarANavigations { get; set; } = new List<Alb>();

    public virtual ICollection<Alb> AlbMgzs { get; set; } = new List<Alb>();

    public virtual ICollection<Alb> AlbPlatforms { get; set; } = new List<Alb>();

    public virtual ICollection<Alb> AlbTrps { get; set; } = new List<Alb>();

    public virtual ICollection<BancTerm> BancTerms { get; set; } = new List<BancTerm>();

    public virtual ICollection<BancTransferBeneficiari> BancTransferBeneficiaris { get; set; } = new List<BancTransferBeneficiari>();

    public virtual ICollection<BookFra> BookFras { get; set; } = new List<BookFra>();

    public virtual ICollection<CcaSchedItem> CcaSchedItems { get; set; } = new List<CcaSchedItem>();

    public virtual ICollection<Ccb> Ccbs { get; set; } = new List<Ccb>();

    public virtual ICollection<CertificatIrpf> CertificatIrpfs { get; set; } = new List<CertificatIrpf>();

    public virtual CliBnc? CliBnc { get; set; }

    public virtual CliCert? CliCert { get; set; }

    public virtual CliClient? CliClient { get; set; }

    public virtual ICollection<CliContact> CliContacts { get; set; } = new List<CliContact>();

    public virtual ICollection<CliCreditLog> CliCreditLogs { get; set; } = new List<CliCreditLog>();

    public virtual ICollection<CliDoc> CliDocs { get; set; } = new List<CliDoc>();

    public virtual ICollection<CliDto> CliDtos { get; set; } = new List<CliDto>();

    public virtual CliPrv? CliPrv { get; set; }

    public virtual ICollection<CliRep> CliRepCcxes { get; set; } = new List<CliRep>();

    public virtual CliRep? CliRepGu { get; set; }

    public virtual ICollection<CliReturn> CliReturnCliNavigations { get; set; } = new List<CliReturn>();

    public virtual ICollection<CliReturn> CliReturnMgzNavigations { get; set; } = new List<CliReturn>();

    public virtual CliStaff? CliStaff { get; set; }

    public virtual ICollection<CliTel> CliTels { get; set; } = new List<CliTel>();

    public virtual ICollection<CliTpa> CliTpas { get; set; } = new List<CliTpa>();

    public virtual ICollection<Cll> Clls { get; set; } = new List<Cll>();

    public virtual Clx? Clx { get; set; }

    public virtual ContactClass? ContactClassNavigation { get; set; }

    public virtual ICollection<ContactGlnDeprecated> ContactGlnDeprecateds { get; set; } = new List<ContactGlnDeprecated>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Credencial> Credencials { get; set; } = new List<Credencial>();

    public virtual ICollection<Csb> Csbs { get; set; } = new List<Csb>();

    public virtual ICollection<CustomerDept> CustomerDepts { get; set; } = new List<CustomerDept>();

    public virtual ICollection<CustomerDto> CustomerDtos { get; set; } = new List<CustomerDto>();

    public virtual ICollection<CustomerPlatform> CustomerPlatformCustomerNavigations { get; set; } = new List<CustomerPlatform>();

    public virtual ICollection<CustomerPlatformDestination> CustomerPlatformDestinations { get; set; } = new List<CustomerPlatformDestination>();

    public virtual CustomerPlatform? CustomerPlatformGu { get; set; }

    public virtual ICollection<EcitransmCentre> EcitransmCentres { get; set; } = new List<EcitransmCentre>();

    public virtual ICollection<EcitransmGroup> EcitransmGroups { get; set; } = new List<EcitransmGroup>();

    public virtual ICollection<EdiRemadvHeader> EdiRemadvHeaderEmisorPagoNavigations { get; set; } = new List<EdiRemadvHeader>();

    public virtual ICollection<EdiRemadvHeader> EdiRemadvHeaderReceptorPagoNavigations { get; set; } = new List<EdiRemadvHeader>();

    public virtual ICollection<EdiversaDesadvHeader> EdiversaDesadvHeaderEntregaNavigations { get; set; } = new List<EdiversaDesadvHeader>();

    public virtual ICollection<EdiversaDesadvHeader> EdiversaDesadvHeaderProveidorNavigations { get; set; } = new List<EdiversaDesadvHeader>();

    public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderCompradorNavigations { get; set; } = new List<EdiversaOrderHeader>();

    public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderFacturarANavigations { get; set; } = new List<EdiversaOrderHeader>();

    public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderProveedorNavigations { get; set; } = new List<EdiversaOrderHeader>();

    public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderReceptorMercanciaNavigations { get; set; } = new List<EdiversaOrderHeader>();

    public virtual ICollection<EdiversaSalesReportHeader> EdiversaSalesReportHeaders { get; set; } = new List<EdiversaSalesReportHeader>();

    public virtual ICollection<EdiversaSalesReportItem> EdiversaSalesReportItems { get; set; } = new List<EdiversaSalesReportItem>();

    public virtual ICollection<EmailCli> EmailClis { get; set; } = new List<EmailCli>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual ICollection<Escriptura> EscripturaNotariNavigations { get; set; } = new List<Escriptura>();

    public virtual ICollection<Escriptura> EscripturaRegistreMercantilNavigations { get; set; } = new List<Escriptura>();

    public virtual ICollection<Forecast> Forecasts { get; set; } = new List<Forecast>();

    public virtual ICollection<Fra> Fras { get; set; } = new List<Fra>();

    public virtual ICollection<FtpPath> FtpPaths { get; set; } = new List<FtpPath>();

    public virtual FtpServer? FtpServer { get; set; }

    public virtual ICollection<Iban> Ibans { get; set; } = new List<Iban>();

    public virtual ICollection<ImportHdr> ImportHdrPlataformaDeCargaNavigations { get; set; } = new List<ImportHdr>();

    public virtual ICollection<ImportHdr> ImportHdrPrvs { get; set; } = new List<ImportHdr>();

    public virtual ICollection<ImportHdr> ImportHdrTrps { get; set; } = new List<ImportHdr>();

    public virtual ICollection<Insolvencia> Insolvencia { get; set; } = new List<Insolvencia>();

    public virtual ICollection<CliGral> InverseNomAnterior { get; set; } = new List<CliGral>();

    public virtual ICollection<CliGral> InverseNomNou { get; set; } = new List<CliGral>();

    public virtual ICollection<InvoiceReceivedHeader> InvoiceReceivedHeaders { get; set; } = new List<InvoiceReceivedHeader>();

    public virtual MarketPlace? MarketPlace { get; set; }

    public virtual ICollection<Mem> Mems { get; set; } = new List<Mem>();

    public virtual Mgz? Mgz { get; set; }

    public virtual ICollection<Multum> Multa { get; set; } = new List<Multum>();

    public virtual CliGral? NomAnterior { get; set; }

    public virtual CliGral? NomNou { get; set; }

    public virtual ICollection<Nomina> Nominas { get; set; } = new List<Nomina>();

    public virtual ICollection<Pdc> PdcClis { get; set; } = new List<Pdc>();

    public virtual ICollection<Pdc> PdcFacturarANavigations { get; set; } = new List<Pdc>();

    public virtual ICollection<Pdc> PdcPlatformNavigations { get; set; } = new List<Pdc>();

    public virtual ICollection<Pnd> Pnds { get; set; } = new List<Pnd>();

    public virtual ICollection<PremiumCustomer> PremiumCustomers { get; set; } = new List<PremiumCustomer>();

    public virtual ICollection<PriceListCustomer> PriceListCustomers { get; set; } = new List<PriceListCustomer>();

    public virtual ICollection<PriceListSupplier> PriceListSuppliers { get; set; } = new List<PriceListSupplier>();

    public virtual ICollection<PrvCliNum> PrvCliNums { get; set; } = new List<PrvCliNum>();

    public virtual ICollection<SepaMe> SepaMes { get; set; } = new List<SepaMe>();

    public virtual ICollection<SorteoLead> SorteoLeads { get; set; } = new List<SorteoLead>();

    public virtual ICollection<Spv> Spvs { get; set; } = new List<Spv>();

    public virtual ICollection<Transm> Transms { get; set; } = new List<Transm>();

    public virtual Trp? Trp { get; set; }

    public virtual ICollection<VehicleFlotum> VehicleFlotumConductors { get; set; } = new List<VehicleFlotum>();

    public virtual ICollection<VehicleFlotum> VehicleFlotumVenedors { get; set; } = new List<VehicleFlotum>();

    public virtual ICollection<VisaCard> VisaCards { get; set; } = new List<VisaCard>();

    public virtual ICollection<Web> WebCcxNavigations { get; set; } = new List<Web>();

    public virtual ICollection<Web> WebClientNavigations { get; set; } = new List<Web>();

    public virtual ICollection<Web> WebProveidorNavigations { get; set; } = new List<Web>();

    public virtual ICollection<WtbolSite> WtbolSites { get; set; } = new List<WtbolSite>();

    public virtual ICollection<Xec> Xecs { get; set; } = new List<Xec>();

    public virtual ICollection<Crr> Mail { get; set; } = new List<Crr>();
}
