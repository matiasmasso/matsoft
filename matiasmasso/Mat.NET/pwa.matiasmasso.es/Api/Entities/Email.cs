using System;
using System.Collections.Generic;

namespace Api.Entities
{
    /// <summary>
    /// Users
    /// </summary>
    public partial class Email
    {
        public Email()
        {
            AlbBloqueigs = new HashSet<AlbBloqueig>();
            AlbUsrCreatedGus = new HashSet<Alb>();
            AlbUsrLastEditedGus = new HashSet<Alb>();
            AppUsrLogs = new HashSet<AppUsrLog>();
            BlogPostUsrCreatedNavigations = new HashSet<BlogPost>();
            BlogPostUsrLastEditedNavigations = new HashSet<BlogPost>();
            Bloggers = new HashSet<Blogger>();
            CcaUsrCreatedGus = new HashSet<Cca>();
            CcaUsrLastEditedGus = new HashSet<Cca>();
            CliCreditLogUserCreatedNavigations = new HashSet<CliCreditLog>();
            CliCreditLogUserLastEditedNavigations = new HashSet<CliCreditLog>();
            CliReturnUsrCreatedNavigations = new HashSet<CliReturn>();
            CliReturnUsrLastEditedNavigations = new HashSet<CliReturn>();
            Cods = new HashSet<Cod>();
            CondCapitolUsrCreatedNavigations = new HashSet<CondCapitol>();
            CondCapitolUsrLastEditedNavigations = new HashSet<CondCapitol>();
            CredencialUsrCreatedNavigations = new HashSet<Credencial>();
            CredencialUsrLastEditedNavigations = new HashSet<Credencial>();
            CrrUsrCreatedNavigations = new HashSet<Crr>();
            CrrUsrLastEditedNavigations = new HashSet<Crr>();
            EdiversaOrderItemSkipDtoValidationUserNavigations = new HashSet<EdiversaOrderItem>();
            EdiversaOrderItemSkipItemUserNavigations = new HashSet<EdiversaOrderItem>();
            EdiversaOrderItemSkipPreuValidationUserNavigations = new HashSet<EdiversaOrderItem>();
            EmailClis = new HashSet<EmailCli>();
            Forecasts = new HashSet<Forecast>();
            FraEmailedToGus = new HashSet<Fra>();
            FraUsrLastPrintedGus = new HashSet<Fra>();
            IbanUsrApprovedNavigations = new HashSet<Iban>();
            IbanUsrDownloadedNavigations = new HashSet<Iban>();
            IbanUsrUploadedNavigations = new HashSet<Iban>();
            IncidencyUsrCreatedNavigations = new HashSet<Incidency>();
            IncidencyUsrLastEditedNavigations = new HashSet<Incidency>();
            JsonSchemaUsrCreatedNavigations = new HashSet<JsonSchema>();
            JsonSchemaUsrLastEditedNavigations = new HashSet<JsonSchema>();
            MailingLogs = new HashSet<MailingLog>();
            Mems = new HashSet<Mem>();
            Msgs = new HashSet<Msg>();
            NoticiumUsrCreatedNavigations = new HashSet<Noticium>();
            NoticiumUsrLastEditedNavigations = new HashSet<Noticium>();
            PdcUsrCreatedGus = new HashSet<Pdc>();
            PdcUsrLastEditedGus = new HashSet<Pdc>();
            PostComments = new HashSet<PostComment>();
            PremiumCustomerUsrCreatedNavigations = new HashSet<PremiumCustomer>();
            PremiumCustomerUsrLastEditedNavigations = new HashSet<PremiumCustomer>();
            ProductPluginUsrCreatedNavigations = new HashSet<ProductPlugin>();
            ProductPluginUsrLastEditedNavigations = new HashSet<ProductPlugin>();
            RecallClis = new HashSet<RecallCli>();
            RepCliComs = new HashSet<RepCliCom>();
            SearchRequests = new HashSet<SearchRequest>();
            SepaMeUsrCreatedNavigations = new HashSet<SepaMe>();
            SepaMeUsrLastEditedNavigations = new HashSet<SepaMe>();
            SkuMoqLocks = new HashSet<SkuMoqLock>();
            SorteoLeads = new HashSet<SorteoLead>();
            SpvIns = new HashSet<SpvIn>();
            SpvUsrOutOfSpvInGus = new HashSet<Spv>();
            SpvUsrOutOfSpvOutGus = new HashSet<Spv>();
            SpvUsrRegisterGus = new HashSet<Spv>();
            SpvUsrTecnicGus = new HashSet<Spv>();
            SscEmails = new HashSet<SscEmail>();
            SscLogs = new HashSet<SscLog>();
            Trackings = new HashSet<Tracking>();
            UserDefaults = new HashSet<UserDefault>();
            UsrSessions = new HashSet<UsrSession>();
            WebErrs = new HashSet<WebErr>();
            WebLogBrowses = new HashSet<WebLogBrowse>();
            WtbolSiteUsrCreatedNavigations = new HashSet<WtbolSite>();
            WtbolSiteUsrLastEditedNavigations = new HashSet<WtbolSite>();
            Credencials = new HashSet<Credencial>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Company; foreign key for Emp table
        /// </summary>
        public int Emp { get; set; }
        public int Id { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Adr { get; set; } = null!;
        /// <summary>
        /// User name
        /// </summary>
        public string? Nom { get; set; }
        /// <summary>
        /// User surname
        /// </summary>
        public string? Cognoms { get; set; }
        /// <summary>
        /// User alias
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// Enumerable DTORol.Ids
        /// </summary>
        public short Rol { get; set; }
        /// <summary>
        /// ISO 639-2 language code
        /// </summary>
        public string Lang { get; set; } = null!;
        /// <summary>
        /// User password
        /// </summary>
        public string? Pwd { get; set; }
        public string? Hash { get; set; }
        /// <summary>
        /// If registered proffessional, foreign key for CliGral table
        /// </summary>
        public Guid? DefaultContactGuid { get; set; }
        /// <summary>
        /// foreign key for Cod table if emails to this user are being returned
        /// </summary>
        public Guid? BadMailGuid { get; set; }
        /// <summary>
        /// If true, do not send auitomated messages there
        /// </summary>
        public bool Privat { get; set; }
        /// <summary>
        /// If true, do not send general mailings
        /// </summary>
        public bool NoNews { get; set; }
        /// <summary>
        /// Enumerable DTOEnums.Sex
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// Year of birth
        /// </summary>
        public int? BirthYea { get; set; }
        /// <summary>
        /// User birthday
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// Number of children
        /// </summary>
        public int? ChildCount { get; set; }
        /// <summary>
        /// Birthday of youngest children
        /// </summary>
        public DateTime? LastChildBirthday { get; set; }
        /// <summary>
        /// Postal address
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// foreign key for Location table
        /// </summary>
        public Guid? Location { get; set; }
        /// <summary>
        /// Location name
        /// </summary>
        public string? LocationNom { get; set; }
        /// <summary>
        /// Province name
        /// </summary>
        public string? ProvinciaNom { get; set; }
        /// <summary>
        /// ISO 3166-1 Country
        /// </summary>
        public string? Pais { get; set; }
        /// <summary>
        /// Postal code
        /// </summary>
        public string? ZipCod { get; set; }
        public Guid? Residence { get; set; }
        /// <summary>
        /// Phone number
        /// </summary>
        public string? Tel { get; set; }
        /// <summary>
        /// Enumerable DTOUser.Sources
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string? Obs { get; set; }
        /// <summary>
        /// True if no longer active
        /// </summary>
        public bool Obsoleto { get; set; }
        /// <summary>
        /// Date the user requested to be deleted
        /// </summary>
        public DateTime? FchDeleted { get; set; }
        /// <summary>
        /// Date the user email address was verified
        /// </summary>
        public DateTime? FchActivated { get; set; }
        /// <summary>
        /// Date this entry was created
        /// </summary>
        public DateTime FchCreated { get; set; }

        public virtual Cod? BadMailGu { get; set; }
        public virtual CliGral? DefaultContactGu { get; set; }
        public virtual Emp EmpNavigation { get; set; } = null!;
        public virtual Location? LocationNavigation { get; set; }
        public virtual Zip? ResidenceNavigation { get; set; }
        public virtual ICollection<AlbBloqueig> AlbBloqueigs { get; set; }
        public virtual ICollection<Alb> AlbUsrCreatedGus { get; set; }
        public virtual ICollection<Alb> AlbUsrLastEditedGus { get; set; }
        public virtual ICollection<AppUsrLog> AppUsrLogs { get; set; }
        public virtual ICollection<BlogPost> BlogPostUsrCreatedNavigations { get; set; }
        public virtual ICollection<BlogPost> BlogPostUsrLastEditedNavigations { get; set; }
        public virtual ICollection<Blogger> Bloggers { get; set; }
        public virtual ICollection<Cca> CcaUsrCreatedGus { get; set; }
        public virtual ICollection<Cca> CcaUsrLastEditedGus { get; set; }
        public virtual ICollection<CliCreditLog> CliCreditLogUserCreatedNavigations { get; set; }
        public virtual ICollection<CliCreditLog> CliCreditLogUserLastEditedNavigations { get; set; }
        public virtual ICollection<CliReturn> CliReturnUsrCreatedNavigations { get; set; }
        public virtual ICollection<CliReturn> CliReturnUsrLastEditedNavigations { get; set; }
        public virtual ICollection<Cod> Cods { get; set; }
        public virtual ICollection<CondCapitol> CondCapitolUsrCreatedNavigations { get; set; }
        public virtual ICollection<CondCapitol> CondCapitolUsrLastEditedNavigations { get; set; }
        public virtual ICollection<Credencial> CredencialUsrCreatedNavigations { get; set; }
        public virtual ICollection<Credencial> CredencialUsrLastEditedNavigations { get; set; }
        public virtual ICollection<Crr> CrrUsrCreatedNavigations { get; set; }
        public virtual ICollection<Crr> CrrUsrLastEditedNavigations { get; set; }
        public virtual ICollection<EdiversaOrderItem> EdiversaOrderItemSkipDtoValidationUserNavigations { get; set; }
        public virtual ICollection<EdiversaOrderItem> EdiversaOrderItemSkipItemUserNavigations { get; set; }
        public virtual ICollection<EdiversaOrderItem> EdiversaOrderItemSkipPreuValidationUserNavigations { get; set; }
        public virtual ICollection<EmailCli> EmailClis { get; set; }
        public virtual ICollection<Forecast> Forecasts { get; set; }
        public virtual ICollection<Fra> FraEmailedToGus { get; set; }
        public virtual ICollection<Fra> FraUsrLastPrintedGus { get; set; }
        public virtual ICollection<Iban> IbanUsrApprovedNavigations { get; set; }
        public virtual ICollection<Iban> IbanUsrDownloadedNavigations { get; set; }
        public virtual ICollection<Iban> IbanUsrUploadedNavigations { get; set; }
        public virtual ICollection<Incidency> IncidencyUsrCreatedNavigations { get; set; }
        public virtual ICollection<Incidency> IncidencyUsrLastEditedNavigations { get; set; }
        public virtual ICollection<JsonSchema> JsonSchemaUsrCreatedNavigations { get; set; }
        public virtual ICollection<JsonSchema> JsonSchemaUsrLastEditedNavigations { get; set; }
        public virtual ICollection<MailingLog> MailingLogs { get; set; }
        public virtual ICollection<Mem> Mems { get; set; }
        public virtual ICollection<Msg> Msgs { get; set; }
        public virtual ICollection<Noticium> NoticiumUsrCreatedNavigations { get; set; }
        public virtual ICollection<Noticium> NoticiumUsrLastEditedNavigations { get; set; }
        public virtual ICollection<Pdc> PdcUsrCreatedGus { get; set; }
        public virtual ICollection<Pdc> PdcUsrLastEditedGus { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PremiumCustomer> PremiumCustomerUsrCreatedNavigations { get; set; }
        public virtual ICollection<PremiumCustomer> PremiumCustomerUsrLastEditedNavigations { get; set; }
        public virtual ICollection<ProductPlugin> ProductPluginUsrCreatedNavigations { get; set; }
        public virtual ICollection<ProductPlugin> ProductPluginUsrLastEditedNavigations { get; set; }
        public virtual ICollection<RecallCli> RecallClis { get; set; }
        public virtual ICollection<RepCliCom> RepCliComs { get; set; }
        public virtual ICollection<SearchRequest> SearchRequests { get; set; }
        public virtual ICollection<SepaMe> SepaMeUsrCreatedNavigations { get; set; }
        public virtual ICollection<SepaMe> SepaMeUsrLastEditedNavigations { get; set; }
        public virtual ICollection<SkuMoqLock> SkuMoqLocks { get; set; }
        public virtual ICollection<SorteoLead> SorteoLeads { get; set; }
        public virtual ICollection<SpvIn> SpvIns { get; set; }
        public virtual ICollection<Spv> SpvUsrOutOfSpvInGus { get; set; }
        public virtual ICollection<Spv> SpvUsrOutOfSpvOutGus { get; set; }
        public virtual ICollection<Spv> SpvUsrRegisterGus { get; set; }
        public virtual ICollection<Spv> SpvUsrTecnicGus { get; set; }
        public virtual ICollection<SscEmail> SscEmails { get; set; }
        public virtual ICollection<SscLog> SscLogs { get; set; }
        public virtual ICollection<Tracking> Trackings { get; set; }
        public virtual ICollection<UserDefault> UserDefaults { get; set; }
        public virtual ICollection<UsrSession> UsrSessions { get; set; }
        public virtual ICollection<WebErr> WebErrs { get; set; }
        public virtual ICollection<WebLogBrowse> WebLogBrowses { get; set; }
        public virtual ICollection<WtbolSite> WtbolSiteUsrCreatedNavigations { get; set; }
        public virtual ICollection<WtbolSite> WtbolSiteUsrLastEditedNavigations { get; set; }

        public virtual ICollection<Credencial> Credencials { get; set; }
    }
}
