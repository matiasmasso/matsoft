using System;
using System.Collections.Generic;

namespace Api.Entities;

/// <summary>
/// Users
/// </summary>
public partial class Email
{
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
    public DateOnly? Birthday { get; set; }

    /// <summary>
    /// Number of children
    /// </summary>
    public int? ChildCount { get; set; }

    /// <summary>
    /// Birthday of youngest children
    /// </summary>
    public DateOnly? LastChildBirthday { get; set; }

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

    public virtual ICollection<AlbBloqueig> AlbBloqueigs { get; set; } = new List<AlbBloqueig>();

    public virtual ICollection<Alb> AlbUsrCreateds { get; set; } = new List<Alb>();

    public virtual ICollection<Alb> AlbUsrLastEditeds { get; set; } = new List<Alb>();

    public virtual ICollection<AppUsrLog> AppUsrLogs { get; set; } = new List<AppUsrLog>();

    public virtual Cod? BadMail { get; set; }

    public virtual ICollection<BlogPost> BlogPostUsrCreatedNavigations { get; set; } = new List<BlogPost>();

    public virtual ICollection<BlogPost> BlogPostUsrLastEditedNavigations { get; set; } = new List<BlogPost>();

    public virtual ICollection<Blogger> Bloggers { get; set; } = new List<Blogger>();

    public virtual ICollection<Cca> CcaUsrCreateds { get; set; } = new List<Cca>();

    public virtual ICollection<Cca> CcaUsrLastEditeds { get; set; } = new List<Cca>();

    public virtual ICollection<CliCreditLog> CliCreditLogUserCreatedNavigations { get; set; } = new List<CliCreditLog>();

    public virtual ICollection<CliCreditLog> CliCreditLogUserLastEditedNavigations { get; set; } = new List<CliCreditLog>();

    public virtual ICollection<CliReturn> CliReturnUsrCreatedNavigations { get; set; } = new List<CliReturn>();

    public virtual ICollection<CliReturn> CliReturnUsrLastEditedNavigations { get; set; } = new List<CliReturn>();

    public virtual ICollection<Cod> Cods { get; set; } = new List<Cod>();

    public virtual ICollection<CondCapitol> CondCapitolUsrCreatedNavigations { get; set; } = new List<CondCapitol>();

    public virtual ICollection<CondCapitol> CondCapitolUsrLastEditedNavigations { get; set; } = new List<CondCapitol>();

    public virtual ICollection<ConsumerTicket> ConsumerTickets { get; set; } = new List<ConsumerTicket>();

    public virtual ICollection<Credencial> CredencialUsrCreatedNavigations { get; set; } = new List<Credencial>();

    public virtual ICollection<Credencial> CredencialUsrLastEditedNavigations { get; set; } = new List<Credencial>();

    public virtual ICollection<Crr> CrrUsrCreatedNavigations { get; set; } = new List<Crr>();

    public virtual ICollection<Crr> CrrUsrLastEditedNavigations { get; set; } = new List<Crr>();

    public virtual CliGral? DefaultContact { get; set; }

    public virtual ICollection<DocFileLog> DocFileLogs { get; set; } = new List<DocFileLog>();

    public virtual ICollection<EdiversaOrderItem> EdiversaOrderItemSkipDtoValidationUserNavigations { get; set; } = new List<EdiversaOrderItem>();

    public virtual ICollection<EdiversaOrderItem> EdiversaOrderItemSkipItemUserNavigations { get; set; } = new List<EdiversaOrderItem>();

    public virtual ICollection<EdiversaOrderItem> EdiversaOrderItemSkipPreuValidationUserNavigations { get; set; } = new List<EdiversaOrderItem>();

    public virtual ICollection<EmailCli> EmailClis { get; set; } = new List<EmailCli>();

    public virtual Emp EmpNavigation { get; set; } = null!;

    public virtual ICollection<Forecast> Forecasts { get; set; } = new List<Forecast>();

    public virtual ICollection<Fra> FraEmailedTos { get; set; } = new List<Fra>();

    public virtual ICollection<Fra> FraUsrLastPrinteds { get; set; } = new List<Fra>();

    public virtual ICollection<Iban> IbanUsrApprovedNavigations { get; set; } = new List<Iban>();

    public virtual ICollection<Iban> IbanUsrDownloadedNavigations { get; set; } = new List<Iban>();

    public virtual ICollection<Iban> IbanUsrUploadedNavigations { get; set; } = new List<Iban>();

    public virtual ICollection<Incidency> IncidencyUsrCreatedNavigations { get; set; } = new List<Incidency>();

    public virtual ICollection<Incidency> IncidencyUsrLastEditedNavigations { get; set; } = new List<Incidency>();

    public virtual ICollection<JsonSchema> JsonSchemaUsrCreatedNavigations { get; set; } = new List<JsonSchema>();

    public virtual ICollection<JsonSchema> JsonSchemaUsrLastEditedNavigations { get; set; } = new List<JsonSchema>();

    public virtual Location? LocationNavigation { get; set; }

    public virtual ICollection<MailingLog> MailingLogs { get; set; } = new List<MailingLog>();

    public virtual ICollection<Mem> Mems { get; set; } = new List<Mem>();

    public virtual ICollection<Msg> Msgs { get; set; } = new List<Msg>();

    public virtual ICollection<Noticium> NoticiumUsrCreatedNavigations { get; set; } = new List<Noticium>();

    public virtual ICollection<Noticium> NoticiumUsrLastEditedNavigations { get; set; } = new List<Noticium>();

    public virtual ICollection<Pdc> PdcUsrCreateds { get; set; } = new List<Pdc>();

    public virtual ICollection<Pdc> PdcUsrLastEditeds { get; set; } = new List<Pdc>();

    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    public virtual ICollection<PremiumCustomer> PremiumCustomerUsrCreatedNavigations { get; set; } = new List<PremiumCustomer>();

    public virtual ICollection<PremiumCustomer> PremiumCustomerUsrLastEditedNavigations { get; set; } = new List<PremiumCustomer>();

    public virtual ICollection<ProductPlugin> ProductPluginUsrCreatedNavigations { get; set; } = new List<ProductPlugin>();

    public virtual ICollection<ProductPlugin> ProductPluginUsrLastEditedNavigations { get; set; } = new List<ProductPlugin>();

    public virtual ICollection<RecallCli> RecallClis { get; set; } = new List<RecallCli>();

    public virtual ICollection<RepCliCom> RepCliComs { get; set; } = new List<RepCliCom>();

    public virtual Zip? ResidenceNavigation { get; set; }

    public virtual ICollection<SearchRequest> SearchRequests { get; set; } = new List<SearchRequest>();

    public virtual ICollection<SepaMe> SepaMeUsrCreatedNavigations { get; set; } = new List<SepaMe>();

    public virtual ICollection<SepaMe> SepaMeUsrLastEditedNavigations { get; set; } = new List<SepaMe>();

    public virtual ICollection<ShoppingBasket> ShoppingBaskets { get; set; } = new List<ShoppingBasket>();

    public virtual ICollection<SkuMoqLock> SkuMoqLocks { get; set; } = new List<SkuMoqLock>();

    public virtual ICollection<SorteoLead> SorteoLeads { get; set; } = new List<SorteoLead>();

    public virtual ICollection<SpvIn> SpvIns { get; set; } = new List<SpvIn>();

    public virtual ICollection<Spv> SpvUsrOutOfSpvIns { get; set; } = new List<Spv>();

    public virtual ICollection<Spv> SpvUsrOutOfSpvOuts { get; set; } = new List<Spv>();

    public virtual ICollection<Spv> SpvUsrRegisters { get; set; } = new List<Spv>();

    public virtual ICollection<Spv> SpvUsrTecnics { get; set; } = new List<Spv>();

    public virtual ICollection<SscEmail> SscEmails { get; set; } = new List<SscEmail>();

    public virtual ICollection<SscLog> SscLogs { get; set; } = new List<SscLog>();

    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();

    public virtual ICollection<UserDefault> UserDefaults { get; set; } = new List<UserDefault>();

    public virtual ICollection<UsrSession> UsrSessions { get; set; } = new List<UsrSession>();

    public virtual ICollection<WebErr> WebErrs { get; set; } = new List<WebErr>();

    public virtual ICollection<WebLogBrowse> WebLogBrowses { get; set; } = new List<WebLogBrowse>();

    public virtual ICollection<WtbolSite> WtbolSiteUsrCreatedNavigations { get; set; } = new List<WtbolSite>();

    public virtual ICollection<WtbolSite> WtbolSiteUsrLastEditedNavigations { get; set; } = new List<WtbolSite>();

    public virtual ICollection<Credencial> Credencials { get; set; } = new List<Credencial>();
}
