using System;
using System.Collections.Generic;

namespace Spa4.Entities
{
    /// <summary>
    /// Contacts
    /// </summary>
    public partial class CliGral
    {
        public CliGral()
        {
            AlbBloqueigs = new HashSet<AlbBloqueig>();
            AlbCliGus = new HashSet<Alb>();
            AlbFacturarANavigations = new HashSet<Alb>();
            AlbMgzGus = new HashSet<Alb>();
            AlbPlatformGus = new HashSet<Alb>();
            AlbTrpGus = new HashSet<Alb>();
            Arcs = new HashSet<Arc>();
            BancTerms = new HashSet<BancTerm>();
            BancTransferBeneficiaris = new HashSet<BancTransferBeneficiari>();
            BookFras = new HashSet<BookFra>();
            Ccbs = new HashSet<Ccb>();
            CertificatIrpfs = new HashSet<CertificatIrpf>();
            CliContacts = new HashSet<CliContact>();
            CliCreditLogs = new HashSet<CliCreditLog>();
            CliDocs = new HashSet<CliDoc>();
            CliDtos = new HashSet<CliDto>();
            CliRepCcxGus = new HashSet<CliRep>();
            CliReturnCliNavigations = new HashSet<CliReturn>();
            CliReturnMgzNavigations = new HashSet<CliReturn>();
            CliTels = new HashSet<CliTel>();
            CliTpas = new HashSet<CliTpa>();
            Clls = new HashSet<Cll>();
            Contracts = new HashSet<Contract>();
            Credencials = new HashSet<Credencial>();
            Csbs = new HashSet<Csb>();
            CustomerDtos = new HashSet<CustomerDto>();
            CustomerPlatformCustomerNavigations = new HashSet<CustomerPlatform>();
            CustomerPlatformDestinations = new HashSet<CustomerPlatformDestination>();
            EcitransmCentres = new HashSet<EcitransmCentre>();
            EcitransmGroups = new HashSet<EcitransmGroup>();
            EdiInvrptHeaders = new HashSet<EdiInvrptHeader>();
            EdiRemadvHeaderEmisorPagoNavigations = new HashSet<EdiRemadvHeader>();
            EdiRemadvHeaderReceptorPagoNavigations = new HashSet<EdiRemadvHeader>();
            EdiversaDesadvHeaderEntregaNavigations = new HashSet<EdiversaDesadvHeader>();
            EdiversaDesadvHeaderProveidorNavigations = new HashSet<EdiversaDesadvHeader>();
            EdiversaOrderHeaderCompradorNavigations = new HashSet<EdiversaOrderHeader>();
            EdiversaOrderHeaderFacturarANavigations = new HashSet<EdiversaOrderHeader>();
            EdiversaOrderHeaderProveedorNavigations = new HashSet<EdiversaOrderHeader>();
            EdiversaOrderHeaderReceptorMercanciaNavigations = new HashSet<EdiversaOrderHeader>();
            EdiversaSalesReportHeaders = new HashSet<EdiversaSalesReportHeader>();
            EdiversaSalesReportItems = new HashSet<EdiversaSalesReportItem>();
            EmailClis = new HashSet<EmailCli>();
            Emails = new HashSet<Email>();
            EscripturaNotariNavigations = new HashSet<Escriptura>();
            EscripturaRegistreMercantilNavigations = new HashSet<Escriptura>();
            Forecasts = new HashSet<Forecast>();
            Fras = new HashSet<Fra>();
            FtpPaths = new HashSet<FtpPath>();
            Ibans = new HashSet<Iban>();
            ImportHdrPlataformaDeCargaNavigations = new HashSet<ImportHdr>();
            ImportHdrPrvGus = new HashSet<ImportHdr>();
            ImportHdrTrpGus = new HashSet<ImportHdr>();
            Insolvencia = new HashSet<Insolvencia>();
            InverseNomAnteriorGu = new HashSet<CliGral>();
            InverseNomNouGu = new HashSet<CliGral>();
            InvoiceReceivedHeaders = new HashSet<InvoiceReceivedHeader>();
            Mems = new HashSet<Mem>();
            Multa = new HashSet<Multum>();
            Nominas = new HashSet<Nomina>();
            PdcCliGus = new HashSet<Pdc>();
            PdcFacturarANavigations = new HashSet<Pdc>();
            PdcPlatformNavigations = new HashSet<Pdc>();
            Pnds = new HashSet<Pnd>();
            PremiumCustomers = new HashSet<PremiumCustomer>();
            PriceListCustomers = new HashSet<PriceListCustomer>();
            PriceListSuppliers = new HashSet<PriceListSupplier>();
            PrvCliNums = new HashSet<PrvCliNum>();
            SepaMes = new HashSet<SepaMe>();
            SorteoLeads = new HashSet<SorteoLead>();
            Spvs = new HashSet<Spv>();
            Transms = new HashSet<Transm>();
            VehicleFlotumConductorGus = new HashSet<VehicleFlotum>();
            VehicleFlotumVenedorGus = new HashSet<VehicleFlotum>();
            VisaCards = new HashSet<VisaCard>();
            WebCcxNavigations = new HashSet<Web>();
            WebClientNavigations = new HashSet<Web>();
            WebProveidorNavigations = new HashSet<Web>();
            WtbolSites = new HashSet<WtbolSite>();
            Xecs = new HashSet<Xec>();
            MailGus = new HashSet<Crr>();
        }

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

        public virtual ContactClass? ContactClassNavigation { get; set; }
        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual CliGral? NomAnteriorGu { get; set; }
        public virtual CliGral? NomNouGu { get; set; }
        public virtual CliBnc CliBnc { get; set; } = null!;
        public virtual CliCert CliCert { get; set; } = null!;
        public virtual CliClient CliClient { get; set; } = null!;
        public virtual CliPrv CliPrv { get; set; } = null!;
        public virtual CliRep CliRepGu { get; set; } = null!;
        public virtual Clx Clx { get; set; } = null!;
        public virtual CustomerPlatform CustomerPlatformGu { get; set; } = null!;
        public virtual FtpServer FtpServer { get; set; } = null!;
        public virtual MarketPlace MarketPlace { get; set; } = null!;
        public virtual Mgz Mgz { get; set; } = null!;
        public virtual Trp Trp { get; set; } = null!;
        public virtual ICollection<AlbBloqueig> AlbBloqueigs { get; set; }
        public virtual ICollection<Alb> AlbCliGus { get; set; }
        public virtual ICollection<Alb> AlbFacturarANavigations { get; set; }
        public virtual ICollection<Alb> AlbMgzGus { get; set; }
        public virtual ICollection<Alb> AlbPlatformGus { get; set; }
        public virtual ICollection<Alb> AlbTrpGus { get; set; }
        public virtual ICollection<Arc> Arcs { get; set; }
        public virtual ICollection<BancTerm> BancTerms { get; set; }
        public virtual ICollection<BancTransferBeneficiari> BancTransferBeneficiaris { get; set; }
        public virtual ICollection<BookFra> BookFras { get; set; }
        public virtual ICollection<Ccb> Ccbs { get; set; }
        public virtual ICollection<CertificatIrpf> CertificatIrpfs { get; set; }
        public virtual ICollection<CliContact> CliContacts { get; set; }
        public virtual ICollection<CliCreditLog> CliCreditLogs { get; set; }
        public virtual ICollection<CliDoc> CliDocs { get; set; }
        public virtual ICollection<CliDto> CliDtos { get; set; }
        public virtual ICollection<CliRep> CliRepCcxGus { get; set; }
        public virtual ICollection<CliReturn> CliReturnCliNavigations { get; set; }
        public virtual ICollection<CliReturn> CliReturnMgzNavigations { get; set; }
        public virtual ICollection<CliTel> CliTels { get; set; }
        public virtual ICollection<CliTpa> CliTpas { get; set; }
        public virtual ICollection<Cll> Clls { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Credencial> Credencials { get; set; }
        public virtual ICollection<Csb> Csbs { get; set; }
        public virtual ICollection<CustomerDto> CustomerDtos { get; set; }
        public virtual ICollection<CustomerPlatform> CustomerPlatformCustomerNavigations { get; set; }
        public virtual ICollection<CustomerPlatformDestination> CustomerPlatformDestinations { get; set; }
        public virtual ICollection<EcitransmCentre> EcitransmCentres { get; set; }
        public virtual ICollection<EcitransmGroup> EcitransmGroups { get; set; }
        public virtual ICollection<EdiInvrptHeader> EdiInvrptHeaders { get; set; }
        public virtual ICollection<EdiRemadvHeader> EdiRemadvHeaderEmisorPagoNavigations { get; set; }
        public virtual ICollection<EdiRemadvHeader> EdiRemadvHeaderReceptorPagoNavigations { get; set; }
        public virtual ICollection<EdiversaDesadvHeader> EdiversaDesadvHeaderEntregaNavigations { get; set; }
        public virtual ICollection<EdiversaDesadvHeader> EdiversaDesadvHeaderProveidorNavigations { get; set; }
        public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderCompradorNavigations { get; set; }
        public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderFacturarANavigations { get; set; }
        public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderProveedorNavigations { get; set; }
        public virtual ICollection<EdiversaOrderHeader> EdiversaOrderHeaderReceptorMercanciaNavigations { get; set; }
        public virtual ICollection<EdiversaSalesReportHeader> EdiversaSalesReportHeaders { get; set; }
        public virtual ICollection<EdiversaSalesReportItem> EdiversaSalesReportItems { get; set; }
        public virtual ICollection<EmailCli> EmailClis { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Escriptura> EscripturaNotariNavigations { get; set; }
        public virtual ICollection<Escriptura> EscripturaRegistreMercantilNavigations { get; set; }
        public virtual ICollection<Forecast> Forecasts { get; set; }
        public virtual ICollection<Fra> Fras { get; set; }
        public virtual ICollection<FtpPath> FtpPaths { get; set; }
        public virtual ICollection<Iban> Ibans { get; set; }
        public virtual ICollection<ImportHdr> ImportHdrPlataformaDeCargaNavigations { get; set; }
        public virtual ICollection<ImportHdr> ImportHdrPrvGus { get; set; }
        public virtual ICollection<ImportHdr> ImportHdrTrpGus { get; set; }
        public virtual ICollection<Insolvencia> Insolvencia { get; set; }
        public virtual ICollection<CliGral> InverseNomAnteriorGu { get; set; }
        public virtual ICollection<CliGral> InverseNomNouGu { get; set; }
        public virtual ICollection<InvoiceReceivedHeader> InvoiceReceivedHeaders { get; set; }
        public virtual ICollection<Mem> Mems { get; set; }
        public virtual ICollection<Multum> Multa { get; set; }
        public virtual ICollection<Nomina> Nominas { get; set; }
        public virtual ICollection<Pdc> PdcCliGus { get; set; }
        public virtual ICollection<Pdc> PdcFacturarANavigations { get; set; }
        public virtual ICollection<Pdc> PdcPlatformNavigations { get; set; }
        public virtual ICollection<Pnd> Pnds { get; set; }
        public virtual ICollection<PremiumCustomer> PremiumCustomers { get; set; }
        public virtual ICollection<PriceListCustomer> PriceListCustomers { get; set; }
        public virtual ICollection<PriceListSupplier> PriceListSuppliers { get; set; }
        public virtual ICollection<PrvCliNum> PrvCliNums { get; set; }
        public virtual ICollection<SepaMe> SepaMes { get; set; }
        public virtual ICollection<SorteoLead> SorteoLeads { get; set; }
        public virtual ICollection<Spv> Spvs { get; set; }
        public virtual ICollection<Transm> Transms { get; set; }
        public virtual ICollection<VehicleFlotum> VehicleFlotumConductorGus { get; set; }
        public virtual ICollection<VehicleFlotum> VehicleFlotumVenedorGus { get; set; }
        public virtual ICollection<VisaCard> VisaCards { get; set; }
        public virtual ICollection<Web> WebCcxNavigations { get; set; }
        public virtual ICollection<Web> WebClientNavigations { get; set; }
        public virtual ICollection<Web> WebProveidorNavigations { get; set; }
        public virtual ICollection<WtbolSite> WtbolSites { get; set; }
        public virtual ICollection<Xec> Xecs { get; set; }

        public virtual ICollection<Crr> MailGus { get; set; }
    }
}
