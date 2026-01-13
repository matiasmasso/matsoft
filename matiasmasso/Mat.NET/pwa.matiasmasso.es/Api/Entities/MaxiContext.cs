using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Entities
{
    public partial class MaxiContext : DbContext
    {
        public MaxiContext()
        {
        }

        public MaxiContext(DbContextOptions<MaxiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aeat> Aeats { get; set; } = null!;
        public virtual DbSet<AeatMod> AeatMods { get; set; } = null!;
        public virtual DbSet<Aecoc> Aecocs { get; set; } = null!;
        public virtual DbSet<Alb> Albs { get; set; } = null!;
        public virtual DbSet<AlbBloqueig> AlbBloqueigs { get; set; } = null!;
        public virtual DbSet<App> Apps { get; set; } = null!;
        public virtual DbSet<AppUsrLog> AppUsrLogs { get; set; } = null!;
        public virtual DbSet<Arc> Arcs { get; set; } = null!;
        public virtual DbSet<Art> Arts { get; set; } = null!;
        public virtual DbSet<ArtCustRef> ArtCustRefs { get; set; } = null!;
        public virtual DbSet<ArtSpare> ArtSpares { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<AuditStock> AuditStocks { get; set; } = null!;
        public virtual DbSet<BancSdo> BancSdos { get; set; } = null!;
        public virtual DbSet<BancTerm> BancTerms { get; set; } = null!;
        public virtual DbSet<BancTransferBeneficiari> BancTransferBeneficiaris { get; set; } = null!;
        public virtual DbSet<BancTransferPool> BancTransferPools { get; set; } = null!;
        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public virtual DbSet<Blogger> Bloggers { get; set; } = null!;
        public virtual DbSet<Bloggerpost> Bloggerposts { get; set; } = null!;
        public virtual DbSet<BloggerpostProduct> BloggerpostProducts { get; set; } = null!;
        public virtual DbSet<Bn1> Bn1s { get; set; } = null!;
        public virtual DbSet<Bn2> Bn2s { get; set; } = null!;
        public virtual DbSet<BookFra> BookFras { get; set; } = null!;
        public virtual DbSet<BrandArea> BrandAreas { get; set; } = null!;
        public virtual DbSet<CategoriaDeNoticium> CategoriaDeNoticia { get; set; } = null!;
        public virtual DbSet<Cca> Ccas { get; set; } = null!;
        public virtual DbSet<Ccb> Ccbs { get; set; } = null!;
        public virtual DbSet<CertificatIrpf> CertificatIrpfs { get; set; } = null!;
        public virtual DbSet<ChannelDto> ChannelDtos { get; set; } = null!;
        public virtual DbSet<CliAdr> CliAdrs { get; set; } = null!;
        public virtual DbSet<CliApertura> CliAperturas { get; set; } = null!;
        public virtual DbSet<CliBnc> CliBncs { get; set; } = null!;
        public virtual DbSet<CliCert> CliCerts { get; set; } = null!;
        public virtual DbSet<CliClient> CliClients { get; set; } = null!;
        public virtual DbSet<CliContact> CliContacts { get; set; } = null!;
        public virtual DbSet<CliCreditLog> CliCreditLogs { get; set; } = null!;
        public virtual DbSet<CliDoc> CliDocs { get; set; } = null!;
        public virtual DbSet<CliDto> CliDtos { get; set; } = null!;
        public virtual DbSet<CliGral> CliGrals { get; set; } = null!;
        public virtual DbSet<CliPrv> CliPrvs { get; set; } = null!;
        public virtual DbSet<CliRep> CliReps { get; set; } = null!;
        public virtual DbSet<CliReturn> CliReturns { get; set; } = null!;
        public virtual DbSet<CliStaff> CliStaffs { get; set; } = null!;
        public virtual DbSet<CliTel> CliTels { get; set; } = null!;
        public virtual DbSet<CliTpa> CliTpas { get; set; } = null!;
        public virtual DbSet<Cll> Clls { get; set; } = null!;
        public virtual DbSet<Clx> Clxes { get; set; } = null!;
        public virtual DbSet<Cnap> Cnaps { get; set; } = null!;
        public virtual DbSet<Cod> Cods { get; set; } = null!;
        public virtual DbSet<CodisMercancium> CodisMercancia { get; set; } = null!;
        public virtual DbSet<Comarca> Comarcas { get; set; } = null!;
        public virtual DbSet<Computer> Computers { get; set; } = null!;
        public virtual DbSet<Cond> Conds { get; set; } = null!;
        public virtual DbSet<CondCapitol> CondCapitols { get; set; } = null!;
        public virtual DbSet<ConsumerTicket> ConsumerTickets { get; set; } = null!;
        public virtual DbSet<ContactClass> ContactClasses { get; set; } = null!;
        public virtual DbSet<ContactGlnDeprecated> ContactGlnDeprecateds { get; set; } = null!;
        public virtual DbSet<ContactMessage> ContactMessages { get; set; } = null!;
        public virtual DbSet<Content> Contents { get; set; } = null!;
        public virtual DbSet<ContentUrl> ContentUrls { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<ContractCodi> ContractCodis { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Credencial> Credencials { get; set; } = null!;
        public virtual DbSet<Crr> Crrs { get; set; } = null!;
        public virtual DbSet<Csa> Csas { get; set; } = null!;
        public virtual DbSet<Csb> Csbs { get; set; } = null!;
        public virtual DbSet<Cur> Curs { get; set; } = null!;
        public virtual DbSet<CurExchangeRate> CurExchangeRates { get; set; } = null!;
        public virtual DbSet<CustomerCluster> CustomerClusters { get; set; } = null!;
        public virtual DbSet<CustomerDto> CustomerDtos { get; set; } = null!;
        public virtual DbSet<CustomerPlatform> CustomerPlatforms { get; set; } = null!;
        public virtual DbSet<CustomerPlatformDestination> CustomerPlatformDestinations { get; set; } = null!;
        public virtual DbSet<Default> Defaults { get; set; } = null!;
        public virtual DbSet<DefaultImage> DefaultImages { get; set; } = null!;
        public virtual DbSet<DeliveryShipment> DeliveryShipments { get; set; } = null!;
        public virtual DbSet<DeliveryTracking> DeliveryTrackings { get; set; } = null!;
        public virtual DbSet<Dept> Depts { get; set; } = null!;
        public virtual DbSet<DistributionChannel> DistributionChannels { get; set; } = null!;
        public virtual DbSet<DocFile> DocFiles { get; set; } = null!;
        public virtual DbSet<DocFileLog> DocFileLogs { get; set; } = null!;
        public virtual DbSet<DocFileSrc> DocFileSrcs { get; set; } = null!;
        public virtual DbSet<DownloadTarget> DownloadTargets { get; set; } = null!;
        public virtual DbSet<Ecidept> Ecidepts { get; set; } = null!;
        public virtual DbSet<EciplantillaModLog> EciplantillaModLogs { get; set; } = null!;
        public virtual DbSet<Ecisku2021> Ecisku2021s { get; set; } = null!;
        public virtual DbSet<EcitransmCentre> EcitransmCentres { get; set; } = null!;
        public virtual DbSet<EcitransmGroup> EcitransmGroups { get; set; } = null!;
        public virtual DbSet<Edi> Edis { get; set; } = null!;
        public virtual DbSet<EdiInvrptHeader> EdiInvrptHeaders { get; set; } = null!;
        public virtual DbSet<EdiInvrptItem> EdiInvrptItems { get; set; } = null!;
        public virtual DbSet<EdiRemadvHeader> EdiRemadvHeaders { get; set; } = null!;
        public virtual DbSet<EdiRemadvItem> EdiRemadvItems { get; set; } = null!;
        public virtual DbSet<EdiversaDesadvHeader> EdiversaDesadvHeaders { get; set; } = null!;
        public virtual DbSet<EdiversaDesadvItem> EdiversaDesadvItems { get; set; } = null!;
        public virtual DbSet<EdiversaException> EdiversaExceptions { get; set; } = null!;
        public virtual DbSet<EdiversaInterlocutor> EdiversaInterlocutors { get; set; } = null!;
        public virtual DbSet<EdiversaOrderHeader> EdiversaOrderHeaders { get; set; } = null!;
        public virtual DbSet<EdiversaOrderItem> EdiversaOrderItems { get; set; } = null!;
        public virtual DbSet<EdiversaOrdrspHeader> EdiversaOrdrspHeaders { get; set; } = null!;
        public virtual DbSet<EdiversaOrdrspItem> EdiversaOrdrspItems { get; set; } = null!;
        public virtual DbSet<EdiversaRecadvHdr> EdiversaRecadvHdrs { get; set; } = null!;
        public virtual DbSet<EdiversaRecadvItm> EdiversaRecadvItms { get; set; } = null!;
        public virtual DbSet<EdiversaSalesReportHeader> EdiversaSalesReportHeaders { get; set; } = null!;
        public virtual DbSet<EdiversaSalesReportItem> EdiversaSalesReportItems { get; set; } = null!;
        public virtual DbSet<ElCorteInglesAlineamientoStock> ElCorteInglesAlineamientoStocks { get; set; } = null!;
        public virtual DbSet<Email> Emails { get; set; } = null!;
        public virtual DbSet<EmailCli> EmailClis { get; set; } = null!;
        public virtual DbSet<Emp> Emps { get; set; } = null!;
        public virtual DbSet<Escriptura> Escripturas { get; set; } = null!;
        public virtual DbSet<Exception> Exceptions { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Filter> Filters { get; set; } = null!;
        public virtual DbSet<FilterItem> FilterItems { get; set; } = null!;
        public virtual DbSet<FilterTarget> FilterTargets { get; set; } = null!;
        public virtual DbSet<Forecast> Forecasts { get; set; } = null!;
        public virtual DbSet<Fra> Fras { get; set; } = null!;
        public virtual DbSet<FtpPath> FtpPaths { get; set; } = null!;
        public virtual DbSet<FtpServer> FtpServers { get; set; } = null!;
        public virtual DbSet<Gallery> Galleries { get; set; } = null!;
        public virtual DbSet<Holding> Holdings { get; set; } = null!;
        public virtual DbSet<HourInOut> HourInOuts { get; set; } = null!;
        public virtual DbSet<Iban> Ibans { get; set; } = null!;
        public virtual DbSet<IbanStructure> IbanStructures { get; set; } = null!;
        public virtual DbSet<Immoble> Immobles { get; set; } = null!;
        public virtual DbSet<Impagat> Impagats { get; set; } = null!;
        public virtual DbSet<ImportDtl> ImportDtls { get; set; } = null!;
        public virtual DbSet<ImportHdr> ImportHdrs { get; set; } = null!;
        public virtual DbSet<ImportPrevisio> ImportPrevisios { get; set; } = null!;
        public virtual DbSet<Incentiu> Incentius { get; set; } = null!;
        public virtual DbSet<IncentiuProduct> IncentiuProducts { get; set; } = null!;
        public virtual DbSet<IncentiuQtyDto> IncentiuQtyDtos { get; set; } = null!;
        public virtual DbSet<IncidenciaDocFile> IncidenciaDocFiles { get; set; } = null!;
        public virtual DbSet<IncidenciesCod> IncidenciesCods { get; set; } = null!;
        public virtual DbSet<Incidency> Incidencies { get; set; } = null!;
        public virtual DbSet<Incoterm> Incoterms { get; set; } = null!;
        public virtual DbSet<Insolvencia> Insolvencias { get; set; } = null!;
        public virtual DbSet<Intrastat> Intrastats { get; set; } = null!;
        public virtual DbSet<IntrastatPartidum> IntrastatPartida { get; set; } = null!;
        public virtual DbSet<InventariItem> InventariItems { get; set; } = null!;
        public virtual DbSet<InvoiceReceivedHeader> InvoiceReceivedHeaders { get; set; } = null!;
        public virtual DbSet<InvoiceReceivedItem> InvoiceReceivedItems { get; set; } = null!;
        public virtual DbSet<JornadaLaboral> JornadaLaborals { get; set; } = null!;
        public virtual DbSet<JsonLog> JsonLogs { get; set; } = null!;
        public virtual DbSet<JsonSchema> JsonSchemas { get; set; } = null!;
        public virtual DbSet<Keyword> Keywords { get; set; } = null!;
        public virtual DbSet<LangText> LangTexts { get; set; } = null!;
        public virtual DbSet<LiniaTelefon> LiniaTelefons { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<MailingLog> MailingLogs { get; set; } = null!;
        public virtual DbSet<MarketPlace> MarketPlaces { get; set; } = null!;
        public virtual DbSet<MarketplaceSku> MarketplaceSkus { get; set; } = null!;
        public virtual DbSet<MediaResource> MediaResources { get; set; } = null!;
        public virtual DbSet<MediaResourceTarget> MediaResourceTargets { get; set; } = null!;
        public virtual DbSet<Mem> Mems { get; set; } = null!;
        public virtual DbSet<Mgz> Mgzs { get; set; } = null!;
        public virtual DbSet<Mr2> Mr2s { get; set; } = null!;
        public virtual DbSet<Mrt> Mrts { get; set; } = null!;
        public virtual DbSet<MrtTipu> MrtTipus { get; set; } = null!;
        public virtual DbSet<Msg> Msgs { get; set; } = null!;
        public virtual DbSet<Multum> Multa { get; set; } = null!;
        public virtual DbSet<Nav> Navs { get; set; } = null!;
        public virtual DbSet<NavEmp> NavEmps { get; set; } = null!;
        public virtual DbSet<NavRol> NavRols { get; set; } = null!;
        public virtual DbSet<Newsletter> Newsletters { get; set; } = null!;
        public virtual DbSet<NewsletterSource> NewsletterSources { get; set; } = null!;
        public virtual DbSet<Nomina> Nominas { get; set; } = null!;
        public virtual DbSet<NominaConcepte> NominaConceptes { get; set; } = null!;
        public virtual DbSet<NominaItem> NominaItems { get; set; } = null!;
        public virtual DbSet<Noticium> Noticia { get; set; } = null!;
        public virtual DbSet<Offer> Offers { get; set; } = null!;
        public virtual DbSet<PaymentGateway> PaymentGateways { get; set; } = null!;
        public virtual DbSet<Pdc> Pdcs { get; set; } = null!;
        public virtual DbSet<Pdd> Pdds { get; set; } = null!;
        public virtual DbSet<PgcClass> PgcClasses { get; set; } = null!;
        public virtual DbSet<PgcCtum> PgcCta { get; set; } = null!;
        public virtual DbSet<PgcPlan> PgcPlans { get; set; } = null!;
        public virtual DbSet<Plantilla> Plantillas { get; set; } = null!;
        public virtual DbSet<Pnc> Pncs { get; set; } = null!;
        public virtual DbSet<Pnd> Pnds { get; set; } = null!;
        public virtual DbSet<PortadaImg> PortadaImgs { get; set; } = null!;
        public virtual DbSet<PortsCondicion> PortsCondicions { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<PremiumCustomer> PremiumCustomers { get; set; } = null!;
        public virtual DbSet<PremiumLine> PremiumLines { get; set; } = null!;
        public virtual DbSet<PremiumProduct> PremiumProducts { get; set; } = null!;
        public virtual DbSet<PriceListCustomer> PriceListCustomers { get; set; } = null!;
        public virtual DbSet<PriceListItemCustomer> PriceListItemCustomers { get; set; } = null!;
        public virtual DbSet<PriceListItemSupplier> PriceListItemSuppliers { get; set; } = null!;
        public virtual DbSet<PriceListSupplier> PriceListSuppliers { get; set; } = null!;
        public virtual DbSet<ProductChannel> ProductChannels { get; set; } = null!;
        public virtual DbSet<ProductDownload> ProductDownloads { get; set; } = null!;
        public virtual DbSet<ProductPlugin> ProductPlugins { get; set; } = null!;
        public virtual DbSet<ProductPluginItem> ProductPluginItems { get; set; } = null!;
        public virtual DbSet<Projecte> Projectes { get; set; } = null!;
        public virtual DbSet<PromofarmaFeed> PromofarmaFeeds { get; set; } = null!;
        public virtual DbSet<Provincium> Provincia { get; set; } = null!;
        public virtual DbSet<PrvCliNum> PrvCliNums { get; set; } = null!;
        public virtual DbSet<PwaMenuItem> PwaMenuItems { get; set; } = null!;
        public virtual DbSet<Recall> Recalls { get; set; } = null!;
        public virtual DbSet<RecallCli> RecallClis { get; set; } = null!;
        public virtual DbSet<RecallProduct> RecallProducts { get; set; } = null!;
        public virtual DbSet<RedsysErr> RedsysErrs { get; set; } = null!;
        public virtual DbSet<Regio> Regios { get; set; } = null!;
        public virtual DbSet<RepCliCom> RepCliComs { get; set; } = null!;
        public virtual DbSet<RepLiq> RepLiqs { get; set; } = null!;
        public virtual DbSet<RepProduct> RepProducts { get; set; } = null!;
        public virtual DbSet<Rp> Rps { get; set; } = null!;
        public virtual DbSet<SalesManager> SalesManagers { get; set; } = null!;
        public virtual DbSet<SatRecall> SatRecalls { get; set; } = null!;
        public virtual DbSet<SearchRequest> SearchRequests { get; set; } = null!;
        public virtual DbSet<SearchResult> SearchResults { get; set; } = null!;
        public virtual DbSet<SegSocialGrup> SegSocialGrups { get; set; } = null!;
        public virtual DbSet<SepaMe> SepaMes { get; set; } = null!;
        public virtual DbSet<SepaText> SepaTexts { get; set; } = null!;
        public virtual DbSet<Sheet1> Sheet1s { get; set; } = null!;
        public virtual DbSet<ShoppingBasket> ShoppingBaskets { get; set; } = null!;
        public virtual DbSet<ShoppingBasketItem> ShoppingBasketItems { get; set; } = null!;
        public virtual DbSet<SkuBundle> SkuBundles { get; set; } = null!;
        public virtual DbSet<SkuMoqLock> SkuMoqLocks { get; set; } = null!;
        public virtual DbSet<SkuWith> SkuWiths { get; set; } = null!;
        public virtual DbSet<SocialMediaWidget> SocialMediaWidgets { get; set; } = null!;
        public virtual DbSet<Sorteo> Sorteos { get; set; } = null!;
        public virtual DbSet<SorteoLead> SorteoLeads { get; set; } = null!;
        public virtual DbSet<Spv> Spvs { get; set; } = null!;
        public virtual DbSet<SpvIn> SpvIns { get; set; } = null!;
        public virtual DbSet<Ssc> Sscs { get; set; } = null!;
        public virtual DbSet<SscEmail> SscEmails { get; set; } = null!;
        public virtual DbSet<SscLog> SscLogs { get; set; } = null!;
        public virtual DbSet<SscRol> SscRols { get; set; } = null!;
        public virtual DbSet<StaffCategory> StaffCategories { get; set; } = null!;
        public virtual DbSet<StaffHoliday> StaffHolidays { get; set; } = null!;
        public virtual DbSet<StaffPo> StaffPos { get; set; } = null!;
        public virtual DbSet<StaffSched> StaffScheds { get; set; } = null!;
        public virtual DbSet<StaffSchedItem> StaffSchedItems { get; set; } = null!;
        public virtual DbSet<Stp> Stps { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskLog> TaskLogs { get; set; } = null!;
        public virtual DbSet<TaskSchedule> TaskSchedules { get; set; } = null!;
        public virtual DbSet<Tax> Taxes { get; set; } = null!;
        public virtual DbSet<Tmp> Tmps { get; set; } = null!;
        public virtual DbSet<TmpInspeccioConsum2022> TmpInspeccioConsum2022s { get; set; } = null!;
        public virtual DbSet<TmpInspeccioRef> TmpInspeccioRefs { get; set; } = null!;
        public virtual DbSet<Tmpinspecciorefs2> Tmpinspecciorefs2s { get; set; } = null!;
        public virtual DbSet<Tpa> Tpas { get; set; } = null!;
        public virtual DbSet<TpvLog> TpvLogs { get; set; } = null!;
        public virtual DbSet<Tracking> Trackings { get; set; } = null!;
        public virtual DbSet<Transm> Transms { get; set; } = null!;
        public virtual DbSet<Trp> Trps { get; set; } = null!;
        public virtual DbSet<Tutorial> Tutorials { get; set; } = null!;
        public virtual DbSet<TutorialRol> TutorialRols { get; set; } = null!;
        public virtual DbSet<TutorialSubject> TutorialSubjects { get; set; } = null!;
        public virtual DbSet<Txt> Txts { get; set; } = null!;
        public virtual DbSet<UrlSegment> UrlSegments { get; set; } = null!;
        public virtual DbSet<UserDefault> UserDefaults { get; set; } = null!;
        public virtual DbSet<UsrRol> UsrRols { get; set; } = null!;
        public virtual DbSet<UsrSession> UsrSessions { get; set; } = null!;
        public virtual DbSet<VehicleFlotum> VehicleFlota { get; set; } = null!;
        public virtual DbSet<VehicleMarca> VehicleMarcas { get; set; } = null!;
        public virtual DbSet<VehicleModel> VehicleModels { get; set; } = null!;
        public virtual DbSet<VisaCard> VisaCards { get; set; } = null!;
        public virtual DbSet<VisaEmisor> VisaEmisors { get; set; } = null!;
        public virtual DbSet<VwAddress> VwAddresses { get; set; } = null!;
        public virtual DbSet<VwAddressBase> VwAddressBases { get; set; } = null!;
        public virtual DbSet<VwAppUsr> VwAppUsrs { get; set; } = null!;
        public virtual DbSet<VwAppUsrLog> VwAppUsrLogs { get; set; } = null!;
        public virtual DbSet<VwArea> VwAreas { get; set; } = null!;
        public virtual DbSet<VwAreaNom> VwAreaNoms { get; set; } = null!;
        public virtual DbSet<VwAreaParent> VwAreaParents { get; set; } = null!;
        public virtual DbSet<VwAtla> VwAtlas { get; set; } = null!;
        public virtual DbSet<VwAtlasRep> VwAtlasReps { get; set; } = null!;
        public virtual DbSet<VwAtlasSalesManager> VwAtlasSalesManagers { get; set; } = null!;
        public virtual DbSet<VwBalance> VwBalances { get; set; } = null!;
        public virtual DbSet<VwBancsSdo> VwBancsSdos { get; set; } = null!;
        public virtual DbSet<VwBank> VwBanks { get; set; } = null!;
        public virtual DbSet<VwBrand> VwBrands { get; set; } = null!;
        public virtual DbSet<VwBrandCategory> VwBrandCategories { get; set; } = null!;
        public virtual DbSet<VwBundleRetail> VwBundleRetails { get; set; } = null!;
        public virtual DbSet<VwBundleStock> VwBundleStocks { get; set; } = null!;
        public virtual DbSet<VwCategory> VwCategories { get; set; } = null!;
        public virtual DbSet<VwCategoryNom> VwCategoryNoms { get; set; } = null!;
        public virtual DbSet<VwCca> VwCcas { get; set; } = null!;
        public virtual DbSet<VwCca1> VwCcas1 { get; set; } = null!;
        public virtual DbSet<VwCcasList> VwCcasLists { get; set; } = null!;
        public virtual DbSet<VwCcxOrMe> VwCcxOrMes { get; set; } = null!;
        public virtual DbSet<VwChannelDto> VwChannelDtos { get; set; } = null!;
        public virtual DbSet<VwChannelOpenSku> VwChannelOpenSkus { get; set; } = null!;
        public virtual DbSet<VwChannelSkusExcluded> VwChannelSkusExcludeds { get; set; } = null!;
        public virtual DbSet<VwCliTpaExcludedCustomer> VwCliTpaExcludedCustomers { get; set; } = null!;
        public virtual DbSet<VwCnap> VwCnaps { get; set; } = null!;
        public virtual DbSet<VwCnapParent> VwCnapParents { get; set; } = null!;
        public virtual DbSet<VwCondicion> VwCondicions { get; set; } = null!;
        public virtual DbSet<VwContact> VwContacts { get; set; } = null!;
        public virtual DbSet<VwContactChannel> VwContactChannels { get; set; } = null!;
        public virtual DbSet<VwContactMenu> VwContactMenus { get; set; } = null!;
        public virtual DbSet<VwContentUrl> VwContentUrls { get; set; } = null!;
        public virtual DbSet<VwCtaMe> VwCtaMes { get; set; } = null!;
        public virtual DbSet<VwCurExchange> VwCurExchanges { get; set; } = null!;
        public virtual DbSet<VwCustomerChannelOpenSku> VwCustomerChannelOpenSkus { get; set; } = null!;
        public virtual DbSet<VwCustomerChannelSkusExcluded> VwCustomerChannelSkusExcludeds { get; set; } = null!;
        public virtual DbSet<VwCustomerCredit> VwCustomerCredits { get; set; } = null!;
        public virtual DbSet<VwCustomerDto> VwCustomerDtos { get; set; } = null!;
        public virtual DbSet<VwCustomerIban> VwCustomerIbans { get; set; } = null!;
        public virtual DbSet<VwCustomerProductGuidsIncluded> VwCustomerProductGuidsIncludeds { get; set; } = null!;
        public virtual DbSet<VwCustomerSku> VwCustomerSkus { get; set; } = null!;
        public virtual DbSet<VwCustomerSkusExcluded> VwCustomerSkusExcludeds { get; set; } = null!;
        public virtual DbSet<VwCustomerSkusIncluded> VwCustomerSkusIncludeds { get; set; } = null!;
        public virtual DbSet<VwCustomerSkusLite> VwCustomerSkusLites { get; set; } = null!;
        public virtual DbSet<VwDelivery> VwDeliveries { get; set; } = null!;
        public virtual DbSet<VwDeliveryShipment> VwDeliveryShipments { get; set; } = null!;
        public virtual DbSet<VwDeliveryTracking> VwDeliveryTrackings { get; set; } = null!;
        public virtual DbSet<VwDeliveryTrackingTrp> VwDeliveryTrackingTrps { get; set; } = null!;
        public virtual DbSet<VwDept> VwDepts { get; set; } = null!;
        public virtual DbSet<VwDeptCategory> VwDeptCategories { get; set; } = null!;
        public virtual DbSet<VwDocfile> VwDocfiles { get; set; } = null!;
        public virtual DbSet<VwDocfileThumbnail> VwDocfileThumbnails { get; set; } = null!;
        public virtual DbSet<VwDownloadTarget> VwDownloadTargets { get; set; } = null!;
        public virtual DbSet<VwEciPlantillaModLog> VwEciPlantillaModLogs { get; set; } = null!;
        public virtual DbSet<VwEciSalesReport> VwEciSalesReports { get; set; } = null!;
        public virtual DbSet<VwEdiInvRpt> VwEdiInvRpts { get; set; } = null!;
        public virtual DbSet<VwEdiInvrptHeader> VwEdiInvrptHeaders { get; set; } = null!;
        public virtual DbSet<VwEdiSalesReport> VwEdiSalesReports { get; set; } = null!;
        public virtual DbSet<VwEmailDefault> VwEmailDefaults { get; set; } = null!;
        public virtual DbSet<VwFeedback> VwFeedbacks { get; set; } = null!;
        public virtual DbSet<VwFeedbackSum> VwFeedbackSums { get; set; } = null!;
        public virtual DbSet<VwFilter> VwFilters { get; set; } = null!;
        public virtual DbSet<VwFilterTarget> VwFilterTargets { get; set; } = null!;
        public virtual DbSet<VwForecast> VwForecasts { get; set; } = null!;
        public virtual DbSet<VwHoldingCustomRef> VwHoldingCustomRefs { get; set; } = null!;
        public virtual DbSet<VwIban> VwIbans { get; set; } = null!;
        public virtual DbSet<VwImpagat> VwImpagats { get; set; } = null!;
        public virtual DbSet<VwImpagatsOinsolvent> VwImpagatsOinsolvents { get; set; } = null!;
        public virtual DbSet<VwInsolvent> VwInsolvents { get; set; } = null!;
        public virtual DbSet<VwInvRpt> VwInvRpts { get; set; } = null!;
        public virtual DbSet<VwInvRptException> VwInvRptExceptions { get; set; } = null!;
        public virtual DbSet<VwInvoicesSent> VwInvoicesSents { get; set; } = null!;
        public virtual DbSet<VwLangText> VwLangTexts { get; set; } = null!;
        public virtual DbSet<VwLastRetailPrice> VwLastRetailPrices { get; set; } = null!;
        public virtual DbSet<VwLocation> VwLocations { get; set; } = null!;
        public virtual DbSet<VwMgzInventoryCost> VwMgzInventoryCosts { get; set; } = null!;
        public virtual DbSet<VwMgzInventoryIo> VwMgzInventoryIos { get; set; } = null!;
        public virtual DbSet<VwMgzInventoryMovement> VwMgzInventoryMovements { get; set; } = null!;
        public virtual DbSet<VwNav> VwNavs { get; set; } = null!;
        public virtual DbSet<VwNoticia> VwNoticias { get; set; } = null!;
        public virtual DbSet<VwPdc> VwPdcs { get; set; } = null!;
        public virtual DbSet<VwPgcCtaSdo> VwPgcCtaSdos { get; set; } = null!;
        public virtual DbSet<VwPgcCtum> VwPgcCta { get; set; } = null!;
        public virtual DbSet<VwPncSku> VwPncSkus { get; set; } = null!;
        public virtual DbSet<VwPostalAddress> VwPostalAddresses { get; set; } = null!;
        public virtual DbSet<VwPremiumLineExcludedCustomer> VwPremiumLineExcludedCustomers { get; set; } = null!;
        public virtual DbSet<VwPremiumLineExclusiveCustomer> VwPremiumLineExclusiveCustomers { get; set; } = null!;
        public virtual DbSet<VwProductBreadcrumb> VwProductBreadcrumbs { get; set; } = null!;
        public virtual DbSet<VwProductCanonicalUrl> VwProductCanonicalUrls { get; set; } = null!;
        public virtual DbSet<VwProductCnap> VwProductCnaps { get; set; } = null!;
        public virtual DbSet<VwProductDefaultUrl> VwProductDefaultUrls { get; set; } = null!;
        public virtual DbSet<VwProductGuid> VwProductGuids { get; set; } = null!;
        public virtual DbSet<VwProductLandingPage> VwProductLandingPages { get; set; } = null!;
        public virtual DbSet<VwProductLangText> VwProductLangTexts { get; set; } = null!;
        public virtual DbSet<VwProductNom> VwProductNoms { get; set; } = null!;
        public virtual DbSet<VwProductParent> VwProductParents { get; set; } = null!;
        public virtual DbSet<VwProductText> VwProductTexts { get; set; } = null!;
        public virtual DbSet<VwProductUrl> VwProductUrls { get; set; } = null!;
        public virtual DbSet<VwProductUrlCanonical> VwProductUrlCanonicals { get; set; } = null!;
        public virtual DbSet<VwRepCustomer> VwRepCustomers { get; set; } = null!;
        public virtual DbSet<VwRepPncsLiqPending> VwRepPncsLiqPendings { get; set; } = null!;
        public virtual DbSet<VwRepProduct> VwRepProducts { get; set; } = null!;
        public virtual DbSet<VwRepSku> VwRepSkus { get; set; } = null!;
        public virtual DbSet<VwRetail> VwRetails { get; set; } = null!;
        public virtual DbSet<VwRetailPrice> VwRetailPrices { get; set; } = null!;
        public virtual DbSet<VwSalesManagerCustomer> VwSalesManagerCustomers { get; set; } = null!;
        public virtual DbSet<VwSalesManagerSku> VwSalesManagerSkus { get; set; } = null!;
        public virtual DbSet<VwSellout> VwSellouts { get; set; } = null!;
        public virtual DbSet<VwSellout2> VwSellout2s { get; set; } = null!;
        public virtual DbSet<VwSelloutCli> VwSelloutClis { get; set; } = null!;
        public virtual DbSet<VwSelloutProveidor> VwSelloutProveidors { get; set; } = null!;
        public virtual DbSet<VwSelloutRep> VwSelloutReps { get; set; } = null!;
        public virtual DbSet<VwSku> VwSkus { get; set; } = null!;
        public virtual DbSet<VwSkuAndBundlePnc> VwSkuAndBundlePncs { get; set; } = null!;
        public virtual DbSet<VwSkuAndBundleStock> VwSkuAndBundleStocks { get; set; } = null!;
        public virtual DbSet<VwSkuBundleRetail> VwSkuBundleRetails { get; set; } = null!;
        public virtual DbSet<VwSkuCost> VwSkuCosts { get; set; } = null!;
        public virtual DbSet<VwSkuLastCost> VwSkuLastCosts { get; set; } = null!;
        public virtual DbSet<VwSkuNom> VwSkuNoms { get; set; } = null!;
        public virtual DbSet<VwSkuPnc> VwSkuPncs { get; set; } = null!;
        public virtual DbSet<VwSkuRetail> VwSkuRetails { get; set; } = null!;
        public virtual DbSet<VwSkuStock> VwSkuStocks { get; set; } = null!;
        public virtual DbSet<VwStaff> VwStaffs { get; set; } = null!;
        public virtual DbSet<VwStaffIban> VwStaffIbans { get; set; } = null!;
        public virtual DbSet<VwStat> VwStats { get; set; } = null!;
        public virtual DbSet<VwStoreLocator> VwStoreLocators { get; set; } = null!;
        public virtual DbSet<VwTaskLastLog> VwTaskLastLogs { get; set; } = null!;
        public virtual DbSet<VwTel> VwTels { get; set; } = null!;
        public virtual DbSet<VwTelDefault> VwTelDefaults { get; set; } = null!;
        public virtual DbSet<VwTelsyEmail> VwTelsyEmails { get; set; } = null!;
        public virtual DbSet<VwTrp> VwTrps { get; set; } = null!;
        public virtual DbSet<VwUsrArtCustRef> VwUsrArtCustRefs { get; set; } = null!;
        public virtual DbSet<VwUsrLog> VwUsrLogs { get; set; } = null!;
        public virtual DbSet<VwUsrNickname> VwUsrNicknames { get; set; } = null!;
        public virtual DbSet<VwVehicle> VwVehicles { get; set; } = null!;
        public virtual DbSet<VwWebAtla> VwWebAtlas { get; set; } = null!;
        public virtual DbSet<VwWtbolBasket> VwWtbolBaskets { get; set; } = null!;
        public virtual DbSet<VwWtbolDisplay> VwWtbolDisplays { get; set; } = null!;
        public virtual DbSet<VwWtbolInventory> VwWtbolInventories { get; set; } = null!;
        public virtual DbSet<VwWtbolLandingPage> VwWtbolLandingPages { get; set; } = null!;
        public virtual DbSet<VwWtbolStock> VwWtbolStocks { get; set; } = null!;
        public virtual DbSet<VwZip> VwZips { get; set; } = null!;
        public virtual DbSet<VwZona> VwZonas { get; set; } = null!;
        public virtual DbSet<Web> Webs { get; set; } = null!;
        public virtual DbSet<WebErr> WebErrs { get; set; } = null!;
        public virtual DbSet<WebLogBrowse> WebLogBrowses { get; set; } = null!;
        public virtual DbSet<WebMenuGroup> WebMenuGroups { get; set; } = null!;
        public virtual DbSet<WebMenuItem> WebMenuItems { get; set; } = null!;
        public virtual DbSet<WebPageAlias> WebPageAliases { get; set; } = null!;
        public virtual DbSet<WinBug> WinBugs { get; set; } = null!;
        public virtual DbSet<WinMenuItem> WinMenuItems { get; set; } = null!;
        public virtual DbSet<WinMenuItemRol> WinMenuItemRols { get; set; } = null!;
        public virtual DbSet<Worten> Wortens { get; set; } = null!;
        public virtual DbSet<WortenCatalog> WortenCatalogs { get; set; } = null!;
        public virtual DbSet<WortenEan> WortenEans { get; set; } = null!;
        public virtual DbSet<WtbolBasket> WtbolBaskets { get; set; } = null!;
        public virtual DbSet<WtbolBasketItem> WtbolBasketItems { get; set; } = null!;
        public virtual DbSet<WtbolCtr> WtbolCtrs { get; set; } = null!;
        public virtual DbSet<WtbolLandingPage> WtbolLandingPages { get; set; } = null!;
        public virtual DbSet<WtbolLog> WtbolLogs { get; set; } = null!;
        public virtual DbSet<WtbolSerp> WtbolSerps { get; set; } = null!;
        public virtual DbSet<WtbolSerpItem> WtbolSerpItems { get; set; } = null!;
        public virtual DbSet<WtbolSite> WtbolSites { get; set; } = null!;
        public virtual DbSet<WtbolStock> WtbolStocks { get; set; } = null!;
        public virtual DbSet<Xec> Xecs { get; set; } = null!;
        public virtual DbSet<XecDetail> XecDetails { get; set; } = null!;
        public virtual DbSet<Yea> Yeas { get; set; } = null!;
        public virtual DbSet<YouTube> YouTubes { get; set; } = null!;
        public virtual DbSet<YouTubeProduct> YouTubeProducts { get; set; } = null!;
        public virtual DbSet<Zip> Zips { get; set; } = null!;
        public virtual DbSet<Zona> Zonas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=sql.matiasmasso.es;TrustServerCertificate=true;Initial Catalog=Maxi;Persist Security Info=True;User ID=sa_cXJSQYte;Password=CC1SURJQXHyfem27Bc", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aeat>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_AEAT");

                entity.ToTable("Aeat");

                entity.HasComment("Tax declaration documents");

                entity.HasIndex(e => new { e.Model, e.Emp, e.Fch }, "IX_Aeat_ModelFch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Emp).HasComment("Company. Foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date of declaration");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Foreign key to document storage Docfile table");

                entity.Property(e => e.Model).HasComment("Foreign key for parent table Aeat_Mod");

                entity.Property(e => e.Period).HasComment("Period within the year");

                entity.Property(e => e.Tperiod).HasComment("Enumerable DTOAeatModel.PeriodTypes (mensual, trimestral, anual...)");

                entity.Property(e => e.Yea).HasComment("Year of the declaration");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Aeats)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aeat_Emp");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Aeats)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Aeat_DocFile");

                entity.HasOne(d => d.ModelNavigation)
                    .WithMany(p => p.Aeats)
                    .HasForeignKey(d => d.Model)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aeat_Aeat_Mod");
            });

            modelBuilder.Entity<AeatMod>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_AEAT_MOD_1");

                entity.ToTable("Aeat_Mod");

                entity.HasComment("Spanish Tax Agency form models (AEAT=Agencia Estatal de Administración Tributaria)");

                entity.HasIndex(e => e.Mod, "IX_AeatMod_Mod")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Purpose. Ennumerable DTOAeatModel.Cods");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength()
                    .HasComment("Model description");

                entity.Property(e => e.GranEmpresa)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If true, specific form for big companies");

                entity.Property(e => e.Mod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Model name");

                entity.Property(e => e.Obsolet).HasComment("If true, the model is outdated and no longer used");

                entity.Property(e => e.Pyme)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Specific for PYMEs (small companies)");

                entity.Property(e => e.SoloInfo).HasComment("If true, no economic transaction applicable, just informative declaration");

                entity.Property(e => e.Tperiod)
                    .HasColumnName("TPeriod")
                    .HasComment("Enumerable DTOAeatModel.PeriodTypes (mensual, trimestral, anual...)");

                entity.Property(e => e.VisibleBancs).HasComment("Our extranet publishes some tax declarations depending on the users rol.\r\nThis field controls which models are accessible to bank employees.");
            });

            modelBuilder.Entity<Aecoc>(entity =>
            {
                entity.HasKey(e => e.Ean)
                    .HasName("PK_AECOC_1");

                entity.ToTable("AECOC");

                entity.HasComment("EAN numbers assigned from our own range (AECOC=Asociación Española de Codificiación Comercial)");

                entity.Property(e => e.Ean)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("EAN 13 code (primary key)");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the Ean code was generated");

                entity.Property(e => e.Guid).HasComment("Target. It may be the foreign key to a Customer (for GLN code for Edi destinations) or a product Sku ");
            });

            modelBuilder.Entity<Alb>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ALB");

                entity.ToTable("Alb");

                entity.HasComment("Delivery note parent table for shipped goods, either received or sent");

                entity.HasIndex(e => e.CliGuid, "IX_ALB_CliGuid");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Alb1 }, "IX_ALB_EmpYeaAlb")
                    .IsUnique();

                entity.HasIndex(e => e.Guid, "IX_Alb_EmpYea");

                entity.HasIndex(e => e.FraGuid, "IX_Alb_FraGuid");

                entity.HasIndex(e => e.Emp, "IX_Alb_Suggested");

                entity.HasIndex(e => e.TransmGuid, "IX_Alb_Transm");

                entity.HasIndex(e => new { e.Emp, e.FraGuid, e.Facturable, e.Cod }, "Idx_Alb_Facturable");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Address (street/road and number, without location) to deliver the goods");

                entity.Property(e => e.Alb1)
                    .HasColumnName("Alb")
                    .HasComment("Delivery note number. It is restarted each year per Company");

                entity.Property(e => e.Bultos).HasComment("Number of packages");

                entity.Property(e => e.CashCod)
                    .HasDefaultValueSql("((9))")
                    .HasComment("Enumerable DTOCustomer.CashCodes: 1.Credit, 2.Cash against goods, 3.Transfer, 4.Credit card, 5.Deposit");

                entity.Property(e => e.CliGuid).HasComment("Either customer destination or supplier origin, depending on Cod field value");

                entity.Property(e => e.Cobro)
                    .HasColumnType("datetime")
                    .HasComment("For cash against goods, date of payment recovery");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOPurchaseOrder.Codis: 1.Entrance from suplier, 2.Shipment to customer...");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("Currency in ISO 4217");

                entity.Property(e => e.CustomerDocUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CustomerDocURL")
                    .HasDefaultValueSql("('')")
                    .HasComment("Url to download customer documentation to be included on the package, usually invoices from e-commerce");

                entity.Property(e => e.Dpp).HasComment("Early payment discount");

                entity.Property(e => e.Dt2).HasComment("Global discount");

                entity.Property(e => e.Emp).HasComment("Company, foreign key for Emp table");

                entity.Property(e => e.EtiquetesTransport)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Transport labels to be attached to package if different from warehouse defaults. Foreign key to DocFile table");

                entity.Property(e => e.Eur)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("Total amount in Eur");

                entity.Property(e => e.ExportCod).HasComment("Enumerable DTOInvoice.ExportCods: 1.National, 2.EEC, 3.Rest of the world");

                entity.Property(e => e.Facturable)
                    .HasDefaultValueSql("((1))")
                    .HasComment("True if these goods should be invoiced");

                entity.Property(e => e.FacturarA).HasComment("Destinatary of the invoice if different from the destination of the goods");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date when the delivery note is generated");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the delivery note was issued");

                entity.Property(e => e.FchJustificante)
                    .HasColumnType("datetime")
                    .HasComment("Date we received delivery prove");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the document was last edited");

                entity.Property(e => e.Fpg)
                    .HasColumnType("text")
                    .HasComment("Specific payment terms for this shipment, if different from customer's default terms");

                entity.Property(e => e.FraGuid).HasComment("Invoice. Foreign key to Fra table");

                entity.Property(e => e.Incoterm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("International Commerce Terms");

                entity.Property(e => e.IvaExempt).HasComment("True if VAT not applicable");

                entity.Property(e => e.Justificante).HasComment("Enumerable DTODelivery.JustificanteCodes. Whether we have requested delivery receipt and if we have received it");

                entity.Property(e => e.Kgs).HasComment("Total weight in Kg");

                entity.Property(e => e.M3)
                    .HasColumnType("decimal(6, 3)")
                    .HasComment("Total volume in cubic meters");

                entity.Property(e => e.MgzGuid).HasComment("Warehouse to add or deduct from inventory");

                entity.Property(e => e.Nom)
                    .HasMaxLength(60)
                    .HasDefaultValueSql("('')")
                    .HasComment("Name of the destinatary of the goods");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Comments");

                entity.Property(e => e.ObsTransp).HasColumnType("text");

                entity.Property(e => e.PlatformGuid).HasComment("A platform is an entity which collects the goods to be delivered to its final destination.\r\nIt may be a customer warehouse or a logistic platform to ship for example to Canary Islands, etc");

                entity.Property(e => e.PortsCod)
                    .HasDefaultValueSql("((9))")
                    .HasComment("Enumerable DTOCustomer.PortsCodes about whether transport is paid on origin or destination etc");

                entity.Property(e => e.Pt2)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Extra charges when paid cash not related to the shipment (for example debts)");

                entity.Property(e => e.Pts)
                    .HasColumnType("money")
                    .HasComment("Total amount in foreign currency");

                entity.Property(e => e.RetencioCod).HasComment("Enumerable DTODelivery.RetencioCods. Used to prevent delivery instructions to be sent to our warehouse until cash is in our account");

                entity.Property(e => e.Tel)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("('')")
                    .HasComment("Contact phone to provide to the forwarder in case of transport incidences");

                entity.Property(e => e.TransmGuid).HasComment("Batch of delivery notes sent at once to the warehouse to be prepared. Foreign key to Transm table");

                entity.Property(e => e.TrpGuid).HasComment("Forwarder used to deliver the goods.");

                entity.Property(e => e.UsrCreatedGuid).HasComment("User who generated the delivery note. Foreign key for Email table");

                entity.Property(e => e.UsrLastEditedGuid).HasComment("User who edited last time this document. Foreign key for Email table");

                entity.Property(e => e.Valorado)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If true, prices are displayed on delivery note");

                entity.Property(e => e.Yea).HasComment("Year (delivery number is reinitialized each year per Company)");

                entity.Property(e => e.Zip).HasComment("Foreign location to Zip table, which cares for country, location and postal code");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.AlbCliGus)
                    .HasForeignKey(d => d.CliGuid)
                    .HasConstraintName("FK_ALB_CliGral");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Albs)
                    .HasForeignKey(d => d.Emp)
                    .HasConstraintName("FK_ALB_Emp");

                entity.HasOne(d => d.EtiquetesTransportNavigation)
                    .WithMany(p => p.Albs)
                    .HasForeignKey(d => d.EtiquetesTransport)
                    .HasConstraintName("FK_ALB_EtiquetesTransport");

                entity.HasOne(d => d.FacturarANavigation)
                    .WithMany(p => p.AlbFacturarANavigations)
                    .HasForeignKey(d => d.FacturarA)
                    .HasConstraintName("FK_ALB_FacturarA");

                entity.HasOne(d => d.FraGu)
                    .WithMany(p => p.Albs)
                    .HasForeignKey(d => d.FraGuid)
                    .HasConstraintName("FK_ALB_FRA");

                entity.HasOne(d => d.MgzGu)
                    .WithMany(p => p.AlbMgzGus)
                    .HasForeignKey(d => d.MgzGuid)
                    .HasConstraintName("FK_ALB_Mgz");

                entity.HasOne(d => d.PlatformGu)
                    .WithMany(p => p.AlbPlatformGus)
                    .HasForeignKey(d => d.PlatformGuid)
                    .HasConstraintName("FK_ALB_Platform");

                entity.HasOne(d => d.TransmGu)
                    .WithMany(p => p.AlbsNavigation)
                    .HasForeignKey(d => d.TransmGuid)
                    .HasConstraintName("FK_ALB_Transm");

                entity.HasOne(d => d.TrpGu)
                    .WithMany(p => p.AlbTrpGus)
                    .HasForeignKey(d => d.TrpGuid)
                    .HasConstraintName("FK_ALB_Trp");

                entity.HasOne(d => d.UsrCreatedGu)
                    .WithMany(p => p.AlbUsrCreatedGus)
                    .HasForeignKey(d => d.UsrCreatedGuid)
                    .HasConstraintName("FK_ALB_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedGu)
                    .WithMany(p => p.AlbUsrLastEditedGus)
                    .HasForeignKey(d => d.UsrLastEditedGuid)
                    .HasConstraintName("FK_ALB_UsrLastEdited");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.Albs)
                    .HasForeignKey(d => d.Zip)
                    .HasConstraintName("FK_ALB_Zip");
            });

            modelBuilder.Entity<AlbBloqueig>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ALB_BLOQUEIG");

                entity.ToTable("Alb_Bloqueig");

                entity.HasComment("Prevents user from concurrently registering orders or delivery notes for same customer");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ALB')")
                    .IsFixedLength()
                    .HasComment("PDC for purchase order, ALB for delivery notes");

                entity.Property(e => e.Contact).HasComment("Contact being locked. Foreign key for CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time of the lock request");

                entity.Property(e => e.UserGuid).HasComment("User requesting lock. Foreign key to Email table");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.AlbBloqueigs)
                    .HasForeignKey(d => d.Contact)
                    .HasConstraintName("FK_Alb_Bloqueig_CliGral");

                entity.HasOne(d => d.UserGu)
                    .WithMany(p => p.AlbBloqueigs)
                    .HasForeignKey(d => d.UserGuid)
                    .HasConstraintName("FK_Alb_Bloqueig_Email");
            });

            modelBuilder.Entity<App>(entity =>
            {
                entity.ToTable("App");

                entity.HasComment("Corporate Apps from Matsoft");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.LastVersion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Last version available");

                entity.Property(e => e.MinVersion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Minim version a user is allowed to run");

                entity.Property(e => e.Nom)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("App name");
            });

            modelBuilder.Entity<AppUsrLog>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("AppUsrLog");

                entity.HasComment("Log of apps launched by users");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.App).HasComment("App id, foreign key for App table");

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("App version");

                entity.Property(e => e.DeviceId)
                    .HasColumnType("text")
                    .HasComment("user device id");

                entity.Property(e => e.DeviceModel)
                    .HasColumnType("text")
                    .HasComment("user device model");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Log date");

                entity.Property(e => e.FchTo).HasColumnType("datetime");

                entity.Property(e => e.Os)
                    .HasColumnType("text")
                    .HasColumnName("OS")
                    .HasComment("user Operative system");

                entity.Property(e => e.Usr).HasComment("User, foreign key to Email table");

                entity.HasOne(d => d.AppNavigation)
                    .WithMany(p => p.AppUsrLogs)
                    .HasForeignKey(d => d.App)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUsrLog_App");

                entity.HasOne(d => d.UsrNavigation)
                    .WithMany(p => p.AppUsrLogs)
                    .HasForeignKey(d => d.Usr)
                    .HasConstraintName("FK_AppUsrLog_Email");
            });

            modelBuilder.Entity<Arc>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ARC");

                entity.ToTable("Arc");

                entity.HasComment("Delivery note items table for shipped goods, either received or sent");

                entity.HasIndex(e => e.ArtGuid, "Arc_ArtGuid");

                entity.HasIndex(e => e.RepComLiquidable, "IX_ARC_ComRepLiquidable");

                entity.HasIndex(e => new { e.AlbGuid, e.Lin }, "IX_Arc_AlbGuidLin")
                    .IsUnique();

                entity.HasIndex(e => e.MgzGuid, "IX_Arc_MgzGuid");

                entity.HasIndex(e => e.MgzGuid, "IX_Arc_MgzGuid2");

                entity.HasIndex(e => e.PncGuid, "IX_PncGuid");

                entity.HasIndex(e => new { e.MgzGuid, e.Cod }, "Ix_Arc_Inventari");

                entity.HasIndex(e => e.Cod, "Xl_ArcCod");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.AlbGuid).HasComment("Foreign key to parent table Alb");

                entity.Property(e => e.ArtGuid).HasComment("Product sku. Foreign key to Art table");

                entity.Property(e => e.Bundle).HasComment("Bundle is a virtual agregation of different products sold at once.\r\nDeliveries register one item for the bundle itself (parent bundle) and one item for each component (child bundle)\r\nParent bundles get on bundle field their Guid value\r\nChildren bundles get on bundle field their parent bundle Guid value");

                entity.Property(e => e.Cod).HasComment("Enumerable DeliveryItem.Cods. Input or output and purpose");

                entity.Property(e => e.Com)
                    .HasColumnType("numeric(5, 2)")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Agent commission");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency in ISO 4217");

                entity.Property(e => e.Dt2).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Dto)
                    .HasColumnType("numeric(5, 2)")
                    .HasComment("Discount on price");

                entity.Property(e => e.Eur)
                    .HasColumnType("numeric(18, 2)")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Unitary price in Euros");

                entity.Property(e => e.Incentiu).HasComment("Related to promos. Foreign key to Incentiu table");

                entity.Property(e => e.IvaCod).HasComment("Enumerable DTOTax.Codis (standard, reduced, exempt...)");

                entity.Property(e => e.Lin).HasComment("Line number");

                entity.Property(e => e.MgzGuid).HasComment("Warehouse. Foreign key for CliGral table");

                entity.Property(e => e.Net)
                    .HasColumnType("numeric(9, 2)")
                    .HasComment("Net price");

                entity.Property(e => e.PdcGuid).HasComment("Purchase order. Foreign key to Pdc table");

                entity.Property(e => e.Pmc)
                    .HasColumnType("numeric(9, 2)")
                    .HasComment("Unit average cost price, for inventory");

                entity.Property(e => e.PncGuid).HasComment("Purchase order item. Foreign key to Pnc table but not linked since it gives conflict problems when deleting Arc");

                entity.Property(e => e.Pts)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Price in foreign currency");

                entity.Property(e => e.Qty).HasComment("Units delivered");

                entity.Property(e => e.RepComLiquidable).HasComment("Foreign key to Rps table which lists what delivery items are to be liquidated to what agent");

                entity.Property(e => e.RepGuid).HasComment("Commercial agent earning a commission. Foreign key to CliGral table");

                entity.Property(e => e.SpvGuid).HasComment("Service. Foreign key to Spv table");

                entity.Property(e => e.Stk).HasComment("Stock in this moment, in order to calculate the average price for inventory");

                entity.HasOne(d => d.AlbGu)
                    .WithMany(p => p.Arcs)
                    .HasForeignKey(d => d.AlbGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arc_Alb");

                entity.HasOne(d => d.BundleNavigation)
                    .WithMany(p => p.InverseBundleNavigation)
                    .HasForeignKey(d => d.Bundle)
                    .HasConstraintName("FK_Arc_Bundle");

                entity.HasOne(d => d.PdcGu)
                    .WithMany(p => p.Arcs)
                    .HasForeignKey(d => d.PdcGuid)
                    .HasConstraintName("FK_Arc_Pdc");
            });

            modelBuilder.Entity<Art>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ART_1");

                entity.ToTable("Art");

                entity.HasComment("Product sku properties");

                entity.HasIndex(e => e.Cbar, "IX_Art_CBarNotNull")
                    .IsUnique()
                    .HasFilter("([CBar] IS NOT NULL)");

                entity.HasIndex(e => e.FchObsoleto, "IX_Art_FchObsoleto");

                entity.HasIndex(e => e.Ref, "Ix_Art_Ref");

                entity.HasIndex(e => new { e.Category, e.Obsoleto, e.Ord }, "NonClusteredIndex-20220321-185228");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Art1)
                    .HasColumnName("Art")
                    .HasComment("Unique product number within the Company");

                entity.Property(e => e.Availability)
                    .HasColumnType("date")
                    .HasComment("Expected availability date, if it is not currently available");

                entity.Property(e => e.BundleDto)
                    .HasColumnType("decimal(10, 8)")
                    .HasComment("Discount applicable when purchased as a bundle over the total of the amount");

                entity.Property(e => e.Category).HasComment("Category to which this Sku belongs. Foreign key to Stp table.");

                entity.Property(e => e.Cbar)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CBar")
                    .HasDefaultValueSql("('')")
                    .HasComment("EAN 13 product bar code");

                entity.Property(e => e.CnapGuid).HasComment("Product classification (CNAP: Clasificación Normalizada de Articulos de Puericultura). Foreign key for Cnap table");

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('00000000')")
                    .IsFixedLength()
                    .HasComment("Customs code. Foreign key for CodisMercancia table");

                entity.Property(e => e.DimensionH).HasComment("Product height in mm, packaging included");

                entity.Property(e => e.DimensionL).HasComment("Product length in mm, packaging included");

                entity.Property(e => e.DimensionW).HasComment("Product width in mm, packaging included");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the product was registered");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Last time the product was edited");

                entity.Property(e => e.FchObsoleto)
                    .HasColumnType("datetime")
                    .HasComment("Date and time the product was outdated");

                entity.Property(e => e.ForzarInnerPack).HasComment("If true, order quantities are restricted to multiple of InnerPack field value");

                entity.Property(e => e.Hereda)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If true the product inherits descriptions from its category");

                entity.Property(e => e.HeredaDimensions).HasComment("If true, the product inherits logistic data from its category");

                entity.Property(e => e.HideUntil)
                    .HasColumnType("date")
                    .HasComment("The product won't be displayed until this date if not null");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasComment("Product image, 700x800 pixels or whatever defined on DTOProductSku.IMAGEWIDTH and DTOProductSku.IMAGEHEIGHT classes");

                entity.Property(e => e.ImgExists).HasComment("True if an image has been uploaded for this product");

                entity.Property(e => e.ImgFch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time for last image upload");

                entity.Property(e => e.InnerPack).HasComment("Units on each package");

                entity.Property(e => e.IsBundle).HasComment("True if the product is an agregation of different products");

                entity.Property(e => e.IvaCod)
                    .HasDefaultValueSql("((1))")
                    .HasComment("product VAT type applicable in Spain (standard, reduced, super reduced...)");

                entity.Property(e => e.Kg)
                    .HasColumnType("numeric(6, 3)")
                    .HasComment("Gross weight, in Kg, including packaging");

                entity.Property(e => e.KgNet)
                    .HasColumnType("numeric(6, 3)")
                    .HasComment("Net weight, in Kg, without packaging");

                entity.Property(e => e.LastProduction).HasComment("If  true, no new orders will be allowed further current stock + pending from supplier");

                entity.Property(e => e.M3)
                    .HasColumnType("numeric(6, 3)")
                    .HasComment("Volume, in m3, including packaging");

                entity.Property(e => e.MadeIn).HasComment("Country of Origin. Foreign key for Country table");

                entity.Property(e => e.NoDimensions).HasComment("True if dimensions not applicable for this product");

                entity.Property(e => e.NoPro).HasComment("If true, it won't be displayed to customers or reps");

                entity.Property(e => e.NoStk).HasComment("True for inmaterial products which do not participate in inventory (services...)");

                entity.Property(e => e.NoTarifa).HasComment("If true it is hidden from customer pricelists");

                entity.Property(e => e.NoWeb).HasComment("If true it prevents this product from being displayed on the website");

                entity.Property(e => e.Obsoleto).HasComment("True for outdated products");

                entity.Property(e => e.ObsoletoConfirmed).HasColumnType("datetime");

                entity.Property(e => e.OuterPack).HasComment("Units on a master package");

                entity.Property(e => e.Outlet).HasComment("True if the product is offered at the website outlet");

                entity.Property(e => e.OutletDto).HasComment("Discount offered at website outlet for this product");

                entity.Property(e => e.OutletQty).HasComment("Minimum quantity required to get the discount offered at website outlet");

                entity.Property(e => e.PackageEan)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("External package bar code");

                entity.Property(e => e.Pmc)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Average cost (Precio Medio de Compra) for current stock. Used in inventory and updated daily by a Windows service");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Manufacturer product code");

                entity.Property(e => e.RefPrv)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Manufacturer product name (without manufacturer code)");

                entity.Property(e => e.SecurityStock).HasComment("Minimum stock to keep for this product");

                entity.Property(e => e.Substitute).HasComment("Replacement product for outdated skus. Foreign key for same table");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("image")
                    .HasComment("Product thumbnail image, 150 pixels width or whatever defined on DTOProductSku class constants");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Arts)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ART_STP");

                entity.HasOne(d => d.CnapGu)
                    .WithMany(p => p.Arts)
                    .HasForeignKey(d => d.CnapGuid)
                    .HasConstraintName("FK_ART_Cnap");

                entity.HasOne(d => d.CodiMercanciaNavigation)
                    .WithMany(p => p.Arts)
                    .HasForeignKey(d => d.CodiMercancia)
                    .HasConstraintName("FK_Art_CodisMercancia");

                entity.HasOne(d => d.MadeInNavigation)
                    .WithMany(p => p.Arts)
                    .HasForeignKey(d => d.MadeIn)
                    .HasConstraintName("FK_Art_Country");

                entity.HasOne(d => d.SubstituteNavigation)
                    .WithMany(p => p.InverseSubstituteNavigation)
                    .HasForeignKey(d => d.Substitute)
                    .HasConstraintName("FK_Art_Art");
            });

            modelBuilder.Entity<ArtCustRef>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ArtCustRef");

                entity.HasComment("Customer product code equivalences");

                entity.HasIndex(e => new { e.CliGuid, e.Ref }, "IX_ArtCustRef_CliGuid_Ref");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.ArtGuid).HasComment("Produc tsku id; foreign key for Art table");

                entity.Property(e => e.CliGuid).HasComment("Customer id; foreign key for CliGral table");

                entity.Property(e => e.Color)
                    .HasColumnType("text")
                    .HasComment("Color");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Customer product description");

                entity.Property(e => e.Dun14)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("DUN14")
                    .HasComment("DUN14 bar code");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this record was created");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasComment("Date of code generation");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Date when this code turned outdated");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer code for this product sku");

                entity.HasOne(d => d.ArtGu)
                    .WithMany(p => p.ArtCustRefs)
                    .HasForeignKey(d => d.ArtGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtCustRef_ART");
            });

            modelBuilder.Entity<ArtSpare>(entity =>
            {
                entity.HasKey(e => new { e.Cod, e.TargetGuid, e.ProductGuid })
                    .HasName("PK_ARTSPARE_1");

                entity.ToTable("ArtSpare");

                entity.HasComment("Accessories and spares for each product");

                entity.Property(e => e.Cod).HasComment("Relationship. Enumerable DTOProduct.Relateds 1.accessory, 2.spare");

                entity.Property(e => e.TargetGuid).HasComment("Target product (parent), for which children accessories or spares exist. Foreign key for Art table");

                entity.Property(e => e.ProductGuid).HasComment("Children product (the accessory or spare part itself). Foreign key for Art table");

                entity.HasOne(d => d.ProductGu)
                    .WithMany(p => p.ArtSpares)
                    .HasForeignKey(d => d.ProductGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ARTSPARE_ART");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AuditStock>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_AUDITSTK");

                entity.ToTable("AuditStock");

                entity.HasComment("Stocks reported by logistic center in order to match our records");

                entity.HasIndex(e => new { e.Yea, e.Ref }, "IX_AuditStk_YeaArt");

                entity.HasIndex(e => new { e.Yea, e.Sku }, "IX_AuditStk_Yea_Sku");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Product cost in order to value the inventory");

                entity.Property(e => e.Dias).HasComment("Days kept in stock, so we can devaluate it if it taklse too long to be sold");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(60)
                    .HasComment("Logistic center product sku description");

                entity.Property(e => e.Entrada)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Logistic center entrance reference");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Report date");

                entity.Property(e => e.FchEntrada)
                    .HasColumnType("datetime")
                    .HasComment("Date of entrance into logistic center");

                entity.Property(e => e.Palet)
                    .HasMaxLength(50)
                    .HasComment("Palet plate number");

                entity.Property(e => e.Procedencia)
                    .HasMaxLength(150)
                    .HasComment("Product origin");

                entity.Property(e => e.Qty).HasComment("Units is stock reported by the logistic center");

                entity.Property(e => e.Ref)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Sequential number unique per Company/product Sku which logistic center uses as primary key. Stored on Art field from Art table");

                entity.Property(e => e.Sku).HasComment("Product sku; foreign key for Art table");

                entity.Property(e => e.Yea).HasComment("Report year");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.AuditStocks)
                    .HasForeignKey(d => d.Sku)
                    .HasConstraintName("FK_AuditStock_Art");
            });

            modelBuilder.Entity<BancSdo>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("BancSdo");

                entity.HasComment("Bank balance. Every day we log the balance as appears on the bank website as a base for account conciliation");

                entity.HasIndex(e => new { e.Fch, e.Banc }, "IX_BancSdo_FchBanc")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Banc).HasComment("Bank account, foreign key for CliGral");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date");

                entity.Property(e => e.Sdo)
                    .HasColumnType("decimal(12, 2)")
                    .HasComment("Balance");
            });

            modelBuilder.Entity<BancTerm>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_BancTerms");

                entity.ToTable("BancTerm");

                entity.HasComment("Bank conditions for remittance advances");

                entity.HasIndex(e => new { e.Banc, e.Fch }, "BancTerm_BancFch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Banc).HasComment("Our bank; foreign key for CliGral table");

                entity.Property(e => e.Diferencial)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Interest rate on top of Euribor");

                entity.Property(e => e.Euribor).HasComment("True if interest rate is indexed to Euribor");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the conditions were agreed");

                entity.Property(e => e.Minim)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Minimum fee per remittance");

                entity.Property(e => e.Target).HasComment("Enumerable DTOBancTerm.targets (1.advanced remittance, 2.remittance at sight)");

                entity.HasOne(d => d.BancNavigation)
                    .WithMany(p => p.BancTerms)
                    .HasForeignKey(d => d.Banc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BancTerm_CliGral");
            });

            modelBuilder.Entity<BancTransferBeneficiari>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("BancTransferBeneficiari");

                entity.HasComment("Transfer beneficiaries and amounts");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Full beneficiary account number");

                entity.Property(e => e.BankBranch).HasComment("Bank branch id, foreign key for Bn2 table");

                entity.Property(e => e.Concepte)
                    .HasColumnType("text")
                    .HasComment("Transfer concept, free text");

                entity.Property(e => e.Contact).HasComment("Beneficiari Id, foreign key for CliGral table");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Transfer amount in Euro");

                entity.Property(e => e.Pool).HasComment("Transfer pool, foreign key for BancTransferPool table");

                entity.Property(e => e.Val)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Transfer amount in foreign currency");

                entity.HasOne(d => d.BankBranchNavigation)
                    .WithMany(p => p.BancTransferBeneficiaris)
                    .HasForeignKey(d => d.BankBranch)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BancTransferBeneficiari_BN2");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.BancTransferBeneficiaris)
                    .HasForeignKey(d => d.Contact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BancTransferBeneficiari_CliGral");

                entity.HasOne(d => d.PoolNavigation)
                    .WithMany(p => p.BancTransferBeneficiaris)
                    .HasForeignKey(d => d.Pool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BancTransferBeneficiari_BancTransferPool");
            });

            modelBuilder.Entity<BancTransferPool>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("BancTransferPool");

                entity.HasComment("Sets of bank transfers");

                entity.HasIndex(e => e.Cca, "IX_BancTransferPool_Cca")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.BancEmissor).HasComment("Issuing bank, foreign key for CliBnc table");

                entity.Property(e => e.Cca).HasComment("Accounts entry, foreign key for Cca table");

                entity.Property(e => e.Ref)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("Transfer reference returned by the bank");

                entity.HasOne(d => d.BancEmissorNavigation)
                    .WithMany(p => p.BancTransferPools)
                    .HasForeignKey(d => d.BancEmissor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BancTransferPool_CliBnc");

                entity.HasOne(d => d.CcaNavigation)
                    .WithOne(p => p.BancTransferPool)
                    .HasForeignKey<BancTransferPool>(d => d.Cca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BancTransferPool_CCA");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Banner");

                entity.HasComment("Website home page banners");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the image was uploaded");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date to be published from");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Last date to be published");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasComment("Banner image");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO639-2 language code");

                entity.Property(e => e.NavigateTo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Landing page to link when someone clicks the banner");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Title");

                entity.Property(e => e.Product).HasComment("Product the banner refers to");
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("BlogPost");

                entity.HasComment("Blog posts");

                entity.HasIndex(e => e.Fch, "IX_BlogPost_Fch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasComment("Date this entry was last edited");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("image")
                    .HasComment("Featured image");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.Property(e => e.Visible).HasComment("If false, the post will be hidden to blog visitors");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.BlogPostUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_BlogPost_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.BlogPostUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_BlogPost_UsrLastEdited");
            });

            modelBuilder.Entity<Blogger>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Blogger");

                entity.HasComment("External blog authors");

                entity.HasIndex(e => e.Author, "IX_Blogger_Author");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Author).HasComment("Foreign key for Email table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Logo).HasColumnType("image");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Blogger friendly name");

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Blogger website");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Bloggers)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK_Blogger_EMAIL");
            });

            modelBuilder.Entity<Bloggerpost>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Bloggerpost");

                entity.HasComment("Posts of interest from external bloggers");

                entity.HasIndex(e => new { e.Blogger, e.Fch }, "IX_Bloggerpost_Blogger_fch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Blogger).HasComment("Foreign key to parent table Blogger");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Post date");

                entity.Property(e => e.HighlightFrom)
                    .HasColumnType("date")
                    .HasComment("Date to start publishing on our website");

                entity.Property(e => e.HighlightTo)
                    .HasColumnType("date")
                    .HasComment("Date to terminate publication on our website");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code (3 digits)");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Post title");

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Post landing page");

                entity.HasOne(d => d.BloggerNavigation)
                    .WithMany(p => p.Bloggerposts)
                    .HasForeignKey(d => d.Blogger)
                    .HasConstraintName("FK_Bloggerpost_Blogger");
            });

            modelBuilder.Entity<BloggerpostProduct>(entity =>
            {
                entity.HasKey(e => new { e.Post, e.Product });

                entity.ToTable("BloggerpostProduct");

                entity.HasComment("Product promoted on a blogger post");

                entity.HasIndex(e => e.Product, "IX_BloggerpostProduct_Product");

                entity.Property(e => e.Post).HasComment("Foreign key to BloggerPost table");

                entity.Property(e => e.Product).HasComment("Foreign key to product, either to brand Tpa table, category Stp table or sku Art table");

                entity.HasOne(d => d.PostNavigation)
                    .WithMany(p => p.BloggerpostProducts)
                    .HasForeignKey(d => d.Post)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BloggerpostProduct_Bloggerpost");
            });

            modelBuilder.Entity<Bn1>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_BN1_1");

                entity.ToTable("Bn1");

                entity.HasComment("Bank entities");

                entity.HasIndex(e => new { e.Country, e.Bn11 }, "IX_BN1_Country_Id");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Abr)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')")
                    .HasComment("Bank friendly name");

                entity.Property(e => e.Bn11)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Bn1")
                    .HasComment("Bank national code");

                entity.Property(e => e.Country).HasComment("Country of residence of the bank");

                entity.Property(e => e.Logo48)
                    .HasColumnType("image")
                    .HasComment("Bank logo 48x48 pixels");

                entity.Property(e => e.Nom)
                    .HasMaxLength(60)
                    .HasDefaultValueSql("('')")
                    .HasComment("Bank corporate name");

                entity.Property(e => e.Obsoleto).HasComment("True if the bank is no longer active");

                entity.Property(e => e.Sepa).HasComment("True if the bank is member of Single Euro Payments Area");

                entity.Property(e => e.Swift)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Swift (BIC) code");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')")
                    .HasComment("Default bank phone number");

                entity.Property(e => e.Web)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Website");
            });

            modelBuilder.Entity<Bn2>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_BN2");

                entity.ToTable("Bn2");

                entity.HasComment("Bank branches");

                entity.HasIndex(e => new { e.Bank, e.Agc }, "IX_BN2_Bank_Id")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("Address (street and number). Free text");

                entity.Property(e => e.Agc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Branch national code within its bank");

                entity.Property(e => e.Bank).HasComment("Bank id, foreignn key for Bn1 table");

                entity.Property(e => e.Location).HasComment("Location id, foreign key for Location table");

                entity.Property(e => e.Obsoleto).HasComment("True if the branch is no longer active");

                entity.Property(e => e.Swift)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasComment("Branch swift code");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')")
                    .HasComment("Branch phone number");

                entity.HasOne(d => d.BankNavigation)
                    .WithMany(p => p.Bn2s)
                    .HasForeignKey(d => d.Bank)
                    .HasConstraintName("FK_BN2_BN1");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Bn2s)
                    .HasForeignKey(d => d.Location)
                    .HasConstraintName("FK_BN2_Location");
            });

            modelBuilder.Entity<BookFra>(entity =>
            {
                entity.HasKey(e => e.CcaGuid)
                    .HasName("PK_BookFras2");

                entity.HasComment("Received invoices book, including SII (Suministro inmediato de información) data");

                entity.Property(e => e.CcaGuid)
                    .ValueGeneratedNever()
                    .HasComment("Accounts entry; primary key and also foreign key for Cca table");

                entity.Property(e => e.BaseExenta)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Tax exempt base, if any");

                entity.Property(e => e.BaseIrpf)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Irpf (tax withholdings) base, if any");

                entity.Property(e => e.BaseSujeta)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Tax base for VAT type TipoIva, if any");

                entity.Property(e => e.BaseSujeta1)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Tax base for VAT type TipoIva1, if any");

                entity.Property(e => e.BaseSujeta2)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Tax base for VAT type TipoIva2, if any");

                entity.Property(e => e.ClaveExenta)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasComment("Spanish tax authorities code for Tax exempt causes");

                entity.Property(e => e.ClaveRegimenEspecialOtrascendencia)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ClaveRegimenEspecialOTrascendencia")
                    .IsFixedLength()
                    .HasComment("Enumerable from Sii service \"L3.1 Clave de régimen especial o trascendencia en facturas recibidas\"");

                entity.Property(e => e.ContactGuid).HasComment("Supplier, foreign key for CliGral table");

                entity.Property(e => e.CtaGuid).HasComment("Account; foreign key to PgcCta table");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Short description of the purpose of the invoice");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was cresated");

                entity.Property(e => e.FraNum)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Invoice number");

                entity.Property(e => e.Irpf)
                    .HasColumnType("numeric(9, 2)")
                    .HasComment("Irpf charge");

                entity.Property(e => e.QuotaIva)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("VAT charge");

                entity.Property(e => e.QuotaIva1)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("VAT charge");

                entity.Property(e => e.QuotaIva2)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("VAT charge");

                entity.Property(e => e.SiiCsv)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("Codigo Seguro de Validación. Spanish tax authorities return code after notifying this invoice");

                entity.Property(e => e.SiiErr)
                    .HasColumnType("text")
                    .HasComment("Errors, if any, on submitting the invoice to Sii");

                entity.Property(e => e.SiiErrCod).HasComment("Error code returned by Sii service");

                entity.Property(e => e.SiiEstadoCuadre).HasComment("Status code returned by tax authorities indicating if it matches the data notified by the invoice issuer");

                entity.Property(e => e.SiiFch)
                    .HasColumnType("datetime")
                    .HasComment("Date it was logged to Sii (Suministro Inmediato de Información)");

                entity.Property(e => e.SiiResult).HasComment("Enumerable DTOSiiLog.Results (1.Correcto, 2.Parcialmente correcto, 3.Incorrecto, 4.ErrorDeComunicacion)");

                entity.Property(e => e.SiiTimestampEstadoCuadre)
                    .HasColumnType("datetime")
                    .HasComment("Date SiiEstadoCuadre field was returned by tax authorities");

                entity.Property(e => e.SiiTimestampUltimaModificacion)
                    .HasColumnType("datetime")
                    .HasComment("Date of last amendment");

                entity.Property(e => e.TipoFra)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('F1')")
                    .HasComment("Spanish tax authorities \"Tipo de Factura\" codes (F1,F2,F3,F4,F5,F6,R1)");

                entity.Property(e => e.TipoIrpf)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Irpf rate");

                entity.Property(e => e.TipoIva)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("VAT rate");

                entity.Property(e => e.TipoIva1)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("VAT rate");

                entity.Property(e => e.TipoIva2)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("VAT rate");

                entity.HasOne(d => d.CcaGu)
                    .WithOne(p => p.BookFra)
                    .HasForeignKey<BookFra>(d => d.CcaGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookFras_Cca");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.BookFras)
                    .HasForeignKey(d => d.ContactGuid)
                    .HasConstraintName("FK_BookFras_CliGral");

                entity.HasOne(d => d.CtaGu)
                    .WithMany(p => p.BookFras)
                    .HasForeignKey(d => d.CtaGuid)
                    .HasConstraintName("FK_BookFras_PgcCta");
            });

            modelBuilder.Entity<BrandArea>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_BrandArea_1");

                entity.ToTable("BrandArea");

                entity.HasComment("Geographical distribution areas agreed with the manufacturer for each product brand");

                entity.HasIndex(e => new { e.Brand, e.Area, e.FchFrom }, "IX_BrandArea_Brand_Area_Fch")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Area).HasComment("Foreign key for either Country, Zona or Location");

                entity.Property(e => e.Brand).HasComment("Product brand, foreign key for Tpa table");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("Date of the agreement");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("Date of termination");

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.BrandAreas)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BrandArea_Tpa");
            });

            modelBuilder.Entity<CategoriaDeNoticium>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("News categories");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Excerpt)
                    .HasMaxLength(160)
                    .IsUnicode(false)
                    .HasComment("Category description");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Category name");
            });

            modelBuilder.Entity<Cca>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CCA");

                entity.ToTable("Cca");

                entity.HasComment("Account entries");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Cca1 }, "IX_CCA_EmpYeaCca")
                    .IsUnique();

                entity.HasIndex(e => e.Hash, "IX_Cca.Hash");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Ccd, e.Cdn }, "IX_Cca_EmpYeaCcdCdn");

                entity.HasIndex(e => e.Projecte, "IX_Cca_Projecte");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cca1)
                    .HasColumnName("Cca")
                    .HasComment("Account entry number; unique for each combination of Company and year");

                entity.Property(e => e.Ccd).HasComment("Enumerable DTOCca.CcdEnum for entry purpose, used on sorting together with Cdn field");

                entity.Property(e => e.Cdn).HasComment("Sort number (for example invoice number on invoices which help to sort invoice entries of same day in the right order)");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Entry date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the entry was registered");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the entry was last updated");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Hash of support document when applicable, foreign key for Docfile table");

                entity.Property(e => e.Projecte).HasComment("Project when applicable; foreign key to Projecte");

                entity.Property(e => e.Ref).HasComment("Reference when applicable; foreign key to different tables depending on type of entry");

                entity.Property(e => e.Txt)
                    .HasMaxLength(60)
                    .HasComment("Concept, free text");

                entity.Property(e => e.UsrCreatedGuid).HasComment("User who registered the entry; foreign key for Email table");

                entity.Property(e => e.UsrLastEditedGuid).HasComment("User who updated this entry for last time, foreign key for Email table");

                entity.Property(e => e.Yea).HasComment("Date year");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Ccas)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cca_Emp");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Ccas)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Cca_DocFile");

                entity.HasOne(d => d.ProjecteNavigation)
                    .WithMany(p => p.Ccas)
                    .HasForeignKey(d => d.Projecte)
                    .HasConstraintName("FK_Cca_Projecte");

                entity.HasOne(d => d.UsrCreatedGu)
                    .WithMany(p => p.CcaUsrCreatedGus)
                    .HasForeignKey(d => d.UsrCreatedGuid)
                    .HasConstraintName("FK_Cca_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedGu)
                    .WithMany(p => p.CcaUsrLastEditedGus)
                    .HasForeignKey(d => d.UsrLastEditedGuid)
                    .HasConstraintName("FK_Cca_UsrLastEdited");
            });

            modelBuilder.Entity<Ccb>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CCB");

                entity.ToTable("Ccb");

                entity.HasComment("Account entry items");

                entity.HasIndex(e => e.ContactGuid, "IX_CCB_ContactGuid");

                entity.HasIndex(e => e.CcaGuid, "IX_Ccb_CcaGuid");

                entity.HasIndex(e => new { e.CcaGuid, e.Lin }, "IX_Ccb_CcaLin")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CcaGuid).HasComment("Foreign key to parent table Cca");

                entity.Property(e => e.ContactGuid).HasComment("Account owner, foreign key to CliGral table");

                entity.Property(e => e.CtaGuid).HasComment("Account, foreign key to accounts table PgcCta");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Dh).HasComment("Enumerable DTOCcb.DhEnum (1.Debe, 2.Haber)");

                entity.Property(e => e.Eur)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("Amount, in Euro currency");

                entity.Property(e => e.Lin).HasComment("Line number to sort the items within same accounts entry");

                entity.Property(e => e.PndGuid).HasComment("foreign key to debt table Pnd, when applicable");

                entity.Property(e => e.Pts)
                    .HasColumnType("money")
                    .HasComment("Amount, in foreign currency");

                entity.HasOne(d => d.CcaGu)
                    .WithMany(p => p.Ccbs)
                    .HasForeignKey(d => d.CcaGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CCB_CCA");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.Ccbs)
                    .HasForeignKey(d => d.ContactGuid)
                    .HasConstraintName("FK_CCB_CliGral");

                entity.HasOne(d => d.CtaGu)
                    .WithMany(p => p.Ccbs)
                    .HasForeignKey(d => d.CtaGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CCB_PGCCTA");
            });

            modelBuilder.Entity<CertificatIrpf>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CertificatIrpf");

                entity.HasComment("Irpf tax withholdings certificates");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Contact).HasComment("Certificate owner, foreign key to CliGral table");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Foreign key to Docfile table");

                entity.Property(e => e.Period).HasComment("Period (0: anual)");

                entity.Property(e => e.Year).HasComment("Year the certificate was issued");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.CertificatIrpfs)
                    .HasForeignKey(d => d.Contact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CertificatIrpf_CliGral");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.CertificatIrpfs)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_CertificatIrpf_DocFile");
            });

            modelBuilder.Entity<ChannelDto>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ChannelDto");

                entity.HasComment("Default discount over retail price for new customers from each distributioon channel. Used to generate price list");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Channel).HasComment("Distribution channel. Foreign key for DistributionChannel table");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Discount on retail price to calculate price list");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this discount was set. Only the most recent entry for a channel/product combination is used to calculate the price list");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Product).HasComment("Foreign key for either product brand Tpa table, product category Stp table or product sku Art table");

                entity.HasOne(d => d.ChannelNavigation)
                    .WithMany(p => p.ChannelDtos)
                    .HasForeignKey(d => d.Channel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChannelDto_DistributionChannel");
            });

            modelBuilder.Entity<CliAdr>(entity =>
            {
                entity.HasKey(e => new { e.SrcGuid, e.Cod })
                    .HasName("PK_CliAdr_1");

                entity.ToTable("CliAdr");

                entity.HasComment("Contact addresses");

                entity.HasIndex(e => e.Geo, "CliAdr_Spatial");

                entity.HasIndex(e => e.Zip, "IX_CLIADR_Zip");

                entity.Property(e => e.SrcGuid).HasComment("Primary key, usually linked to a Contact field from another view.");

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasComment("Several addresses may be registered for same contact; this code specifies if it is the official corporate address, postal address or delivery address");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("adr")
                    .HasComment("The address within the location, the way it should be written locally");

                entity.Property(e => e.AdrNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adrNum")
                    .HasComment("The number within the street, the Km point within the road...");

                entity.Property(e => e.AdrPis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adrPis")
                    .HasComment("The flat or apartment number, or any data not relevant to locate the address in a map");

                entity.Property(e => e.AdrViaNom)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adrViaNom")
                    .HasComment("The name of the street");

                entity.Property(e => e.AdrViaType)
                    .HasColumnName("adrViaType")
                    .HasComment("Wether it is a street, road, avenue, square...");

                entity.Property(e => e.Geo).HasComment("Geography data type for distance calculations and nearest neighbours");

                entity.Property(e => e.GeoFchLastEdited)
                    .HasColumnType("datetime")
                    .HasComment("Date when last user edited the address coordinates");

                entity.Property(e => e.GeoFont)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Code to register where did the address come from");

                entity.Property(e => e.GeoUserLastEdited).HasComment("Who registered this address coordinates (foreign key for email addresses)");

                entity.Property(e => e.Latitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.Longitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.Zip).HasComment("Foreign key for Postal Code table");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.CliAdrs)
                    .HasForeignKey(d => d.Zip)
                    .HasConstraintName("FK_CliAdr_Zip");
            });

            modelBuilder.Entity<CliApertura>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CLI_APERTURAS");

                entity.ToTable("Cli_Aperturas");

                entity.HasComment("Potential new customer application form");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Address (street and number)");

                entity.Property(e => e.Associacions)
                    .HasColumnType("text")
                    .HasComment("Commercial associations which may be member of, if any");

                entity.Property(e => e.Cit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Location");

                entity.Property(e => e.CodAntiguedad).HasComment("Antiquity; enumerable DTOCliApertura.CodsAntiguedad");

                entity.Property(e => e.CodExperiencia).HasComment("Experience; enumerable DTOCliApertura.CodsExperience");

                entity.Property(e => e.CodSalePoints).HasComment("Number of sale points; enumerable DTOCliApertura.CodsSalePoints");

                entity.Property(e => e.CodSuperficie).HasComment("Commercial surface available; enumerable DTOCliApertura.CodsSuperficie");

                entity.Property(e => e.CodTancament).HasComment("Status; enumerable DTOCliApertura.CodsTancament (1.Visited, 2.Placed first order, 3.Cancelled)");

                entity.Property(e => e.CodVolumen).HasComment("Turnover; enumerable DTOCliApertura.CodsVolumen");

                entity.Property(e => e.ContactClass).HasComment("Activity; foreign key for ContactClass table");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Email address");

                entity.Property(e => e.FchApertura)
                    .HasColumnType("date")
                    .HasComment("Launch date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Inquiry date");

                entity.Property(e => e.IsoPais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ES')")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code");

                entity.Property(e => e.Nif)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("VAT number");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Contact person");

                entity.Property(e => e.NomComercial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Commercial name");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.OtherBrands)
                    .HasColumnType("text")
                    .HasComment("Otherchild care brands which may be found in this commerce");

                entity.Property(e => e.OtherShares)
                    .HasColumnType("text")
                    .HasComment("Other activities to complete turnover, if  any");

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Company name");

                entity.Property(e => e.RepObs)
                    .HasColumnType("text")
                    .HasComment("Comments from commercial agent");

                entity.Property(e => e.SharePuericultura).HasComment("Child care bussiness share within turnover");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Phone number");

                entity.Property(e => e.Web)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Website url");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Zip code");

                entity.Property(e => e.ZonaGuid).HasComment("Country zone; foreign key for Zona table");

                entity.HasOne(d => d.ContactClassNavigation)
                    .WithMany(p => p.CliAperturas)
                    .HasForeignKey(d => d.ContactClass)
                    .HasConstraintName("FK_Cli_Aperturas_ContactClass");

                entity.HasOne(d => d.ZonaGu)
                    .WithMany(p => p.CliAperturas)
                    .HasForeignKey(d => d.ZonaGuid)
                    .HasConstraintName("FK_Cli_Aperturas_Zona");

                entity.HasMany(d => d.Brands)
                    .WithMany(p => p.Aperturas)
                    .UsingEntity<Dictionary<string, object>>(
                        "CliAperturasBrand",
                        l => l.HasOne<Tpa>().WithMany().HasForeignKey("Brand").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Cli_Aperturas_Brands_TPA"),
                        r => r.HasOne<CliApertura>().WithMany().HasForeignKey("Apertura").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Cli_Aperturas_Brands_Cli_Aperturas"),
                        j =>
                        {
                            j.HasKey("Apertura", "Brand");

                            j.ToTable("Cli_Aperturas_Brands").HasComment("Commercial brands potential new customers request to distribute");

                            j.IndexerProperty<Guid>("Apertura").HasComment("Foreign key to parent table Cli_Aperturas");

                            j.IndexerProperty<Guid>("Brand").HasComment("Foreign key to product brand table Tpa");
                        });
            });

            modelBuilder.Entity<CliBnc>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliBnc");

                entity.HasComment("Our banks");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Abr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Classificacio)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Credit limit");

                entity.Property(e => e.ComisioGestioCobrBase)
                    .HasColumnType("smallmoney")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Commission for remittances at sight, VAT applicable part");

                entity.Property(e => e.ConditionsTransfers)
                    .HasColumnType("text")
                    .HasComment("Swift payments conditions");

                entity.Property(e => e.ConditionsUnpayments)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Unpayment conditions");

                entity.Property(e => e.NormaRmecedent)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NormaRMEcedent")
                    .HasDefaultValueSql("('')")
                    .HasComment("Id for remittances to Andorra");

                entity.Property(e => e.SepaCoreIdentificador)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasComment("Id for Sepa remittances");

                entity.Property(e => e.TransferReceiptPreferencia)
                    .HasDefaultValueSql("((9))")
                    .HasComment("Used when asking debtors to transfer money, the available accounts are sorted by this field");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.CliBnc)
                    .HasForeignKey<CliBnc>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliBnc_CliGral");
            });

            modelBuilder.Entity<CliCert>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CliCert_1");

                entity.ToTable("CliCert");

                entity.HasComment("Digital Certificates");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Caduca)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('01/01/1900')")
                    .HasComment("Expiration date");

                entity.Property(e => e.Ext)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("File extension");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasComment("Signature image");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Password");

                entity.Property(e => e.Stream)
                    .HasColumnType("image")
                    .HasComment("Byte array");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.CliCert)
                    .HasForeignKey<CliCert>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliCert_CliGral");
            });

            modelBuilder.Entity<CliClient>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliClient");

                entity.HasComment("Customers properties");

                entity.HasIndex(e => e.CcxGuid, "IX_CLICLIENT_CcxGuid");

                entity.HasIndex(e => e.Holding, "IX_CliClient_Holding");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key; foreign key for CliGral table");

                entity.Property(e => e.AlbVal).HasComment("If false, prices are hidden from delivery notes");

                entity.Property(e => e.CashCod).HasComment("DTOCustomer.CashCodes (1.credit, 2.cash against goods, 2.previous transfer...)");

                entity.Property(e => e.CcxGuid).HasComment("Headquarters to invoice sales delivered to this destination, in case not self. Foreign key for CliGral table");

                entity.Property(e => e.Cfp).HasComment("Payment way code DTOPaymentTerms.CodsFormaDePago");

                entity.Property(e => e.CustomerCluster).HasComment("For sale points from a unique customer, cluster classification. Foreign key for CustomerClustyer");

                entity.Property(e => e.DeliveryPlatform).HasComment("If any, deliveries to this customer should be addressed to the platform. Foreign key for CliGral table");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Global discount agreed, if any");

                entity.Property(e => e.ExportCod)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Enumerable DTOInvoice.ExportCods (1.national, 2.UE, 3.rest of the world)");

                entity.Property(e => e.FpgIndependent).HasComment("If true, this destination has specific payment terms which may differ from other destinations of same customer");

                entity.Property(e => e.FraPrintMode).HasComment("Invoices delivery mode; Enumerable DTOCustomer.FraPrintModes (1.No print, 2.Paper, 3.Email, 4.Edi)");

                entity.Property(e => e.Holding).HasComment("In case this customer is part of a holding corporation, foreign key for Holding table");

                entity.Property(e => e.HorarioEntregas)
                    .HasColumnType("text")
                    .HasComment("Time window for deliveries reception");

                entity.Property(e => e.Incoterm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("International Commerce Terms; foreign key for Incoterm table");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasComment("False if VAT exempt");

                entity.Property(e => e.Mes).HasComment("Number of months to pay after invoice date");

                entity.Property(e => e.MostrarEanenFactura)
                    .HasColumnName("MostrarEANenFactura")
                    .HasComment("If true, invoice items should include product EAN numbers");

                entity.Property(e => e.NoIncentius).HasComment("If true, no promos are available to this customer");

                entity.Property(e => e.NoRaffles).HasComment("If true, this customer should be removed from sale points participants list on raffles");

                entity.Property(e => e.NoRep).HasComment("If true, no rep gets any commission");

                entity.Property(e => e.NoWeb).HasComment("If true, customer should not be displayed as a sale point to consumers");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.ObsComercial)
                    .HasColumnType("text")
                    .HasComment("Comments like opening hours, etc");

                entity.Property(e => e.OrdersToCentral).HasComment("True if the customer wants to centralize all orders to headquarters");

                entity.Property(e => e.PaymentDays)
                    .HasMaxLength(31)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("If any, days when the customer pays after the months from previous field");

                entity.Property(e => e.Ports).HasComment("DTOCustomer.PortsCodes (1.paid on origin, 2.paid on destination...)");

                entity.Property(e => e.PortsCondicions).HasComment("Foreign key for PortsCondicions table");

                entity.Property(e => e.QuotaOnline).HasComment("Rate of online sales within this customer turnover");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer reference to distinguish this destination among others from same customer");

                entity.Property(e => e.Req)
                    .HasColumnName("REQ")
                    .HasComment("False if Equivalence tax exempt");

                entity.Property(e => e.SuProveedorNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Our supplier number on customers database");

                entity.Property(e => e.Vacaciones)
                    .HasColumnType("text")
                    .HasComment("Holidays which imply payments delay. 6 comma separated digits in 3 groups of month,day: from, to, forward to");

                entity.Property(e => e.Warning)
                    .HasColumnType("text")
                    .HasComment("Message to display to operators when entering orders or deliveries to this customer");

                entity.Property(e => e.WebAtlasPriority).HasComment("Priority when displaying sale points to consumers");

                entity.HasOne(d => d.CcxGu)
                    .WithMany(p => p.InverseCcxGu)
                    .HasForeignKey(d => d.CcxGuid)
                    .HasConstraintName("FK_CliClient_Ccx");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.CliClient)
                    .HasForeignKey<CliClient>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliClient_CliGral");

                entity.HasOne(d => d.HoldingNavigation)
                    .WithMany(p => p.CliClients)
                    .HasForeignKey(d => d.Holding)
                    .HasConstraintName("FK_CliClient_Holding");

                entity.HasOne(d => d.IncotermNavigation)
                    .WithMany(p => p.CliClients)
                    .HasForeignKey(d => d.Incoterm)
                    .HasConstraintName("FK_CliClient_Platform");
            });

            modelBuilder.Entity<CliContact>(entity =>
            {
                entity.HasKey(e => new { e.ContactGuid, e.Id });

                entity.ToTable("CliContact");

                entity.HasComment("Those contacts who are Companies may need to list individual contact person names with their positions. This table is the place to store them");

                entity.Property(e => e.ContactGuid).HasComment("Foreign key for CliGral table");

                entity.Property(e => e.Id).HasComment("Sequential sort order id within each Contact");

                entity.Property(e => e.Contact)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Contact person name");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.CliContacts)
                    .HasForeignKey(d => d.ContactGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliContact_CliGral");
            });

            modelBuilder.Entity<CliCreditLog>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliCreditLog");

                entity.HasComment("Customer credit updates");

                entity.HasIndex(e => new { e.CliGuid, e.FchCreated }, "IX_CliCreditLog_CliGuid_FchCreated");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CliGuid).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOCliCreditLog.Cods (1.cancelled due to excess of time with no orders)");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(10, 2)")
                    .HasComment("Credit authorised");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments justifying why the customer has a new credit amount");

                entity.Property(e => e.UserCreated).HasComment("User who created this entry");

                entity.Property(e => e.UserLastEdited).HasComment("User who edited this entry for last time");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.CliCreditLogs)
                    .HasForeignKey(d => d.CliGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliCreditLog_CliGral");

                entity.HasOne(d => d.UserCreatedNavigation)
                    .WithMany(p => p.CliCreditLogUserCreatedNavigations)
                    .HasForeignKey(d => d.UserCreated)
                    .HasConstraintName("FK_CliCreditLog_UsrCreated");

                entity.HasOne(d => d.UserLastEditedNavigation)
                    .WithMany(p => p.CliCreditLogUserLastEditedNavigations)
                    .HasForeignKey(d => d.UserLastEdited)
                    .HasConstraintName("FK_CliCreditLog_UsrLastEdited");
            });

            modelBuilder.Entity<CliDoc>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CLIDOCS");

                entity.ToTable("CliDoc");

                entity.HasComment("Contact documentation");

                entity.HasIndex(e => new { e.Contact, e.Fch }, "IX_CliDoc_Contact");

                entity.HasIndex(e => e.Hash, "IX_CliDocs_Hash");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Contact).HasComment("Document owner. Foreign key for CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Document date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Pdf document. Foreign key to Docfile table");

                entity.Property(e => e.Obsoleto).HasComment("True if outdated");

                entity.Property(e => e.Ref)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Document title");

                entity.Property(e => e.Type).HasComment("Enumerable DTOContactDoc.Types (1.Nif, 2.Deed...)");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.CliDocs)
                    .HasForeignKey(d => d.Contact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliDoc_CliGral");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.CliDocs)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_CliDoc_Docfile");
            });

            modelBuilder.Entity<CliDto>(entity =>
            {
                entity.HasKey(e => new { e.Customer, e.Brand })
                    .HasName("PK_CLIDTO");

                entity.ToTable("CliDto");

                entity.HasComment("Fix discount some customers may have granted in certain product ranges");

                entity.Property(e => e.Customer).HasComment("Foreign key for CliGral table");

                entity.Property(e => e.Brand).HasComment("Product range. Foreign key for either Tpa, Stp or Art tables");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("dto")
                    .HasComment("Granted discount");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CliDtos)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliDto_CliGral");
            });

            modelBuilder.Entity<CliGral>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliGral");

                entity.HasComment("Contacts");

                entity.HasIndex(e => new { e.Emp, e.Cli }, "IX_CLIGRAL_EmpCli")
                    .IsUnique();

                entity.HasIndex(e => e.ContactClass, "IX_CliGral_ContactClass");

                entity.HasIndex(e => e.Gln, "IX_CliGral_GLN")
                    .IsUnique()
                    .HasFilter("([GLN] IS NOT NULL)");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Cli).HasComment("Sequential order, unique for each Emp");

                entity.Property(e => e.ContactClass).HasComment("Main activity classification. Foreign key for ContactClass table");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FullNom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Displayable name with commercial name and location");

                entity.Property(e => e.Gln)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("GLN")
                    .IsFixedLength()
                    .HasComment("Global Location Number, format EAN 13. Used on EDI messages");

                entity.Property(e => e.LangId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.Nif)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NIF")
                    .HasDefaultValueSql("('')")
                    .HasComment("Primary tax id number (usually VAT number)");

                entity.Property(e => e.Nif2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NIF2")
                    .HasComment("Secondary id number");

                entity.Property(e => e.Nif2Cod).HasComment("Type of secondary tax id number");

                entity.Property(e => e.NifCod).HasComment("Type of primary tax id number, Enumerable DTONif.Cods");

                entity.Property(e => e.NomAnteriorGuid).HasComment("Previous account from same contact");

                entity.Property(e => e.NomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Commercial name");

                entity.Property(e => e.NomKey)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Searchkey by which this contact may be searched");

                entity.Property(e => e.NomNouGuid).HasComment("Account substituting current one");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Obsoleto).HasComment("True if no longer active");

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Corporate name");

                entity.Property(e => e.Rol)
                    .HasDefaultValueSql("((20))")
                    .HasComment("Default rol for this contact users. Enumerable DTORol.Ids");

                entity.Property(e => e.Web)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Website address");

                entity.HasOne(d => d.ContactClassNavigation)
                    .WithMany(p => p.CliGrals)
                    .HasForeignKey(d => d.ContactClass)
                    .HasConstraintName("FK_CliGral_ContactClass");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.CliGrals)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliGral_Emp");

                entity.HasOne(d => d.NomAnteriorGu)
                    .WithMany(p => p.InverseNomAnteriorGu)
                    .HasForeignKey(d => d.NomAnteriorGuid)
                    .HasConstraintName("FK_CliGral_NomAnterior");

                entity.HasOne(d => d.NomNouGu)
                    .WithMany(p => p.InverseNomNouGu)
                    .HasForeignKey(d => d.NomNouGuid)
                    .HasConstraintName("FK_CliGral_NomNou");
            });

            modelBuilder.Entity<CliPrv>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CliPrv_1");

                entity.ToTable("CliPrv");

                entity.HasComment("Suppliers");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key; foreign key for CliGral table");

                entity.Property(e => e.Cfp).HasComment("Payment way code. Enumerable DTOPaymentTerms.CodsFormaDePago");

                entity.Property(e => e.CodIrpf).HasComment("Tax withholdings mode, enumerable DTOProveidor.IRPFCods (0.exempt, 1.standard, 2.reduced, 3.custom)");

                entity.Property(e => e.CodStk).HasComment("Enumerable DTOProductBrand.CodStks (0.Internal, we keep the stocks 1.Extrernal, the supplier keep the stocks)");

                entity.Property(e => e.CtaCarrec).HasComment("Default accounts account where to charge invoices; foreign key for DTOPgcCta table");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Default currency on invoices");

                entity.Property(e => e.DpgWeek).HasComment("Type of day referred on PaymentDays field. Enumerable DTOPaymentTerms.PaymentDayCods (0.month day, 1.week day)");

                entity.Property(e => e.Incoterm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('FOB')")
                    .HasComment("International Commerce Terms; foreign key to Incoterm table");

                entity.Property(e => e.Mes).HasComment("Number of months payment terms after invoice date");

                entity.Property(e => e.PaymentDays)
                    .HasColumnType("text")
                    .HasComment("If applicable, 31 digits string built from characters 0 or 1 indicating disabled or enabled payment day ");

                entity.Property(e => e.Vacaciones)
                    .HasColumnType("text")
                    .HasComment("Payment pause for holidays: 6 comma separated integers indicating starting month, starting day, end mont, end day, forward payment to month, forward payment to day");

                entity.HasOne(d => d.CtaCarrecNavigation)
                    .WithMany(p => p.CliPrvCtaCarrecNavigations)
                    .HasForeignKey(d => d.CtaCarrec)
                    .HasConstraintName("FK_CliPrv_PgcCta");

                entity.HasOne(d => d.CtaCreditoraNavigation)
                    .WithMany(p => p.CliPrvCtaCreditoraNavigations)
                    .HasForeignKey(d => d.CtaCreditora)
                    .HasConstraintName("FK_CliPrv_PgcCtaCreditora");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.CliPrv)
                    .HasForeignKey<CliPrv>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliPrv_CliGral");
            });

            modelBuilder.Entity<CliRep>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliRep");

                entity.HasComment("Commercial agents (Reps) details");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Abr)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Coloquial name to identify him internally");

                entity.Property(e => e.CcxGuid).HasComment("Foreign key to rep company name if any. Foreign key to CliGral");

                entity.Property(e => e.ComRed)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Reduced commission percentage, to apply when it is so agreed");

                entity.Property(e => e.ComStd)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Standard commission percentage");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Free text describing the areas covered");

                entity.Property(e => e.Desde)
                    .HasColumnType("date")
                    .HasComment("Enytry date");

                entity.Property(e => e.DisableLiqs).HasComment("If true, no commission statements will be issued");

                entity.Property(e => e.Foto)
                    .HasColumnType("image")
                    .HasComment("Agent picture");

                entity.Property(e => e.Hasta)
                    .HasColumnType("date")
                    .HasComment("Termination date");

                entity.Property(e => e.Irpf)
                    .HasColumnName("IRPF")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Enumerable DTOProveidor.IrpfCods: 0.Exempt, 1.Standard, 2.Reduced, 3.Custom");

                entity.Property(e => e.IrpfTipus)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Custom Irpf percentage");

                entity.Property(e => e.Iva)
                    .HasColumnName("IVA")
                    .HasDefaultValueSql("((1))")
                    .HasComment("VAT enumerable DTORep.IvaCods: 0.Exempt, 1.Standard");

                entity.Property(e => e.Obsoleto).HasComment("Outdated account, no longer active");

                entity.HasOne(d => d.CcxGu)
                    .WithMany(p => p.CliRepCcxGus)
                    .HasForeignKey(d => d.CcxGuid)
                    .HasConstraintName("FK_CliRep_Ccx");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.CliRepGu)
                    .HasForeignKey<CliRep>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliRep_CliGral");
            });

            modelBuilder.Entity<CliReturn>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliReturn");

                entity.HasComment("Warehouse reception of customer returns");

                entity.HasIndex(e => e.FchCreated, "IX_CliReturns_FchCreated");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Auth)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasComment("Return authorisation code");

                entity.Property(e => e.Bultos)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Number of packages");

                entity.Property(e => e.Cli).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.Entrada).HasComment("Packing list; foreign key for Alb table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Reception date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited last time");

                entity.Property(e => e.Mgz).HasComment("Warehouse; foreign key for CliGral table");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.RefMgz)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Warehouse reception reference");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.HasOne(d => d.CliNavigation)
                    .WithMany(p => p.CliReturnCliNavigations)
                    .HasForeignKey(d => d.Cli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliReturn_Cli");

                entity.HasOne(d => d.EntradaNavigation)
                    .WithMany(p => p.CliReturns)
                    .HasForeignKey(d => d.Entrada)
                    .HasConstraintName("FK_CliReturn_Alb");

                entity.HasOne(d => d.MgzNavigation)
                    .WithMany(p => p.CliReturnMgzNavigations)
                    .HasForeignKey(d => d.Mgz)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliReturn_Mgz");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.CliReturnUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliReturn_EmailUsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.CliReturnUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliReturn_EmailUsrLastEdited");
            });

            modelBuilder.Entity<CliStaff>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliStaff");

                entity.HasComment("Employees");

                entity.HasIndex(e => e.Abr, "IX_CliStaff_Abr");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key; foreign key for CliGral table");

                entity.Property(e => e.Abr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Alta)
                    .HasColumnType("datetime")
                    .HasComment("Entry date");

                entity.Property(e => e.Avatar)
                    .HasColumnType("image")
                    .HasComment("Employee picture");

                entity.Property(e => e.Baja)
                    .HasColumnType("datetime")
                    .HasComment("Termination date, if any");

                entity.Property(e => e.Categoria).HasComment("Social security category; foreign key for StaffCategory table");

                entity.Property(e => e.Nac)
                    .HasColumnType("datetime")
                    .HasComment("Birth date");

                entity.Property(e => e.NumSs)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NumSS")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength()
                    .HasComment("Social security number");

                entity.Property(e => e.Sex).HasComment("Enumerable DTOEnums.Sexs (1.male, 2.female)");

                entity.Property(e => e.StaffPos).HasComment("Employee position; foreign key for StaffPos table");

                entity.HasOne(d => d.CategoriaNavigation)
                    .WithMany(p => p.CliStaffs)
                    .HasForeignKey(d => d.Categoria)
                    .HasConstraintName("FK_CliStaff_StaffCategory");

                entity.HasOne(d => d.StaffPosNavigation)
                    .WithMany(p => p.CliStaffs)
                    .HasForeignKey(d => d.StaffPos)
                    .HasConstraintName("FK_CliStaff_StaffPos");
            });

            modelBuilder.Entity<CliTel>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CliTel");

                entity.HasComment("Contact phone numbers");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CliGuid).HasComment("Contact. Foreign key for CliGral table");

                entity.Property(e => e.Cod).HasComment("Device. Enumerable DTOContactTel.Cods (1.Phone, 2.Fax, 3.Cellular...)");

                entity.Property(e => e.Country).HasComment("ISO 3166-1 country  code");

                entity.Property(e => e.Num)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Phone number");

                entity.Property(e => e.Obs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Comments; usually the owner person");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((99))")
                    .HasComment("Sort order");

                entity.Property(e => e.Privat).HasComment("If true this number is jkust for call identification number, we shouldn't use it to call.");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.CliTels)
                    .HasForeignKey(d => d.CliGuid)
                    .HasConstraintName("FK_CliTel_CliGral");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.CliTels)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliTel_Country");
            });

            modelBuilder.Entity<CliTpa>(entity =>
            {
                entity.HasKey(e => new { e.CliGuid, e.ProductGuid })
                    .HasName("PK_CLITPA");

                entity.ToTable("CliTpa");

                entity.HasComment("Customer product ranges");

                entity.HasIndex(e => new { e.ProductGuid, e.Cod }, "IX_CliTpa_ProductGuidCod");

                entity.Property(e => e.CliGuid).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.ProductGuid).HasComment("Product range; it may either be a brand, a brand category or a sku, foreign key to Tpa, Stp or Art tables");

                entity.Property(e => e.Cod).HasComment("Inclusion or exclusion; enumerable DTOCliProductBlocked.Codis");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Comments justifying range inclusion or exclusion");

                entity.Property(e => e.Zip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Postal code in case it takes area exclusivity");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.CliTpas)
                    .HasForeignKey(d => d.CliGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliTpa_CliGral");
            });

            modelBuilder.Entity<Cll>(entity =>
            {
                entity.HasKey(e => new { e.Cll1, e.ContactGuid, e.Cod })
                    .HasName("PK_CLL");

                entity.ToTable("Cll");

                entity.HasComment("Contact search keys");

                entity.HasIndex(e => new { e.ContactGuid, e.Cll1 }, "IX_Cll_ContactGuid");

                entity.Property(e => e.Cll1)
                    .HasMaxLength(40)
                    .HasColumnName("Cll")
                    .HasComment("Search key");

                entity.Property(e => e.ContactGuid).HasComment("Foreign key for CliGral table");

                entity.Property(e => e.Cod)
                    .HasDefaultValueSql("((10))")
                    .HasComment("Enumerable DTOContact.ContactKeys (0.Name, 3.Location, 4.Commercial name, 26.Custom search key)");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.Clls)
                    .HasForeignKey(d => d.ContactGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLL_CliGral");
            });

            modelBuilder.Entity<Clx>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CLX");

                entity.ToTable("Clx");

                entity.HasComment("Contact logos");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Img48)
                    .HasColumnType("image")
                    .HasComment("Logo or avatar, 48 pixels height");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.Clx)
                    .HasForeignKey<Clx>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLX_CliGral");
            });

            modelBuilder.Entity<Cnap>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CNAP");

                entity.ToTable("Cnap");

                entity.HasComment("Product classification by function. Clasificacion Normalizada de Artículos de Puericultura");

                entity.HasIndex(e => e.Id, "IX_CNAP_Deprecated_Id")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("Classification Id, it takes its first digits from parent and adds a last digit");

                entity.Property(e => e.Parent).HasComment("Parent record; foreign key to self table");

                entity.Property(e => e.Tags).HasColumnType("text");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("FK_Cnap_Cnap");
            });

            modelBuilder.Entity<Cod>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Cod");

                entity.HasComment("Multi purpose codes");

                entity.HasIndex(e => new { e.Parent, e.Id }, "Idx_Cod_ParentOrd");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Id).HasComment("Id to be used by enumerables depending on each parent");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Parent).HasComment("Parent code; foreign key to self Cod table");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("FK_Cod_Cod");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.Cods)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_Cod_Email");
            });

            modelBuilder.Entity<CodisMercancium>(entity =>
            {
                entity.HasComment("Customs codes for products");

                entity.Property(e => e.Id)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Customs code");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Description of products within this code");
            });

            modelBuilder.Entity<Comarca>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Comarca");

                entity.HasComment("County details. A County is an optional area inside a Zona");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary Key");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the Comarca");

                entity.Property(e => e.Zona).HasComment("Foreign key to Zona table where the Comarca belongs");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.Comarcas)
                    .HasForeignKey(d => d.Zona)
                    .HasConstraintName("FK_Comarca_Zona");
            });

            modelBuilder.Entity<Computer>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Computer");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FchFrom).HasColumnType("date");

                entity.Property(e => e.FchTo).HasColumnType("date");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Text).HasColumnType("text");
            });

            modelBuilder.Entity<Cond>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Cond");

                entity.HasComment("Commercial terms and conditions");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");
            });

            modelBuilder.Entity<CondCapitol>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CondCapitol");

                entity.HasComment("Terms and conditions chapters");

                entity.HasIndex(e => new { e.Parent, e.Ord }, "IX_CondCapitol_ParentOrd");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this chapter was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this chapter was edited for last time");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((999))")
                    .HasComment("Chapter order");

                entity.Property(e => e.Parent).HasComment("Parent id, foreign key to parent table Cond");

                entity.Property(e => e.UsrCreated).HasComment("User who registered this chapter for first time");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this chapter for last time");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.CondCapitols)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CondCapitol_Cond");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.CondCapitolUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_CondCapitol_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.CondCapitolUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_CondCapitol_UsrLastEdited");
            });

            modelBuilder.Entity<ConsumerTicket>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_MarketPlace");

                entity.ToTable("ConsumerTicket");

                entity.HasComment("Sale operation to consumer");

                entity.HasIndex(e => e.Delivery, "IX_ConsumerTicket_Delivery")
                    .IsUnique()
                    .HasFilter("([Delivery] IS NOT NULL)");

                entity.HasIndex(e => e.Id, "IX_ConsumerTicket_Id");

                entity.HasIndex(e => e.PurchaseOrder, "IX_ConsumerTicket_PurchaseOrder");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Consumer delivery address");

                entity.Property(e => e.BuyerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Consumer purchaser email address");

                entity.Property(e => e.BuyerNom)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Consumer purchaser name");

                entity.Property(e => e.Cca).HasComment("Accounts entry; foreign key for Email table");

                entity.Property(e => e.Cognom1)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Consumer destination first surname");

                entity.Property(e => e.Cognom2)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Consumer destination second surname, if any");

                entity.Property(e => e.Comision)
                    .HasColumnType("decimal(7, 2)")
                    .HasComment("Market place commission");

                entity.Property(e => e.ConsumerLocation)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Consumer location name");

                entity.Property(e => e.ConsumerProvincia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Consumer province name");

                entity.Property(e => e.ConsumerZip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Consumer zip code");

                entity.Property(e => e.Delivery).HasComment("PurchaseOrder or Spv Guid");

                entity.Property(e => e.Emailaddress).HasColumnType("text");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Ticket date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchDelivered)
                    .HasColumnType("datetime")
                    .HasComment("Delivery date");

                entity.Property(e => e.FchReviewRequest)
                    .HasColumnType("datetime")
                    .HasComment("Date a review has been requested to the consumer");

                entity.Property(e => e.FchTrackingNotified)
                    .HasColumnType("datetime")
                    .HasComment("Date of efective deliver");

                entity.Property(e => e.FraAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Invoice address");

                entity.Property(e => e.FraNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Consumer name for invoice");

                entity.Property(e => e.FraZip).HasComment("Invoice postal code");

                entity.Property(e => e.Goods)
                    .HasColumnType("decimal(7, 2)")
                    .HasComment("Goods charges");

                entity.Property(e => e.Id).HasComment("Ticket Id");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.MarketPlace).HasComment("Market place where the conversion has been carried out, if any; foreign key for CliGral table");

                entity.Property(e => e.Nif)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("VAT number");

                entity.Property(e => e.Nom)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Consumer destination name");

                entity.Property(e => e.Op).HasComment("Operation cod as per DTOPurchaseOrder.Codis");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Order number assigned by the market place");

                entity.Property(e => e.Portes)
                    .HasColumnType("decimal(7, 2)")
                    .HasComment("Transport charges");

                entity.Property(e => e.PurchaseOrder).HasComment("Foreign key for Pdc table");

                entity.Property(e => e.Spv).HasComment("If repair job, foreign key for Spv table");

                entity.Property(e => e.Tel)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Contact phone number");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrDelivered).HasComment("User who issued the delivery");

                entity.Property(e => e.UsrReviewRequest).HasComment("User who requested the review");

                entity.Property(e => e.UsrTrackingNotified).HasComment("User who has been notified to receipt the package");

                entity.Property(e => e.Zip).HasComment("Foreign key for Zip table");
            });

            modelBuilder.Entity<ContactClass>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ContactClass");

                entity.HasComment("Contact activity classification");

                entity.HasIndex(e => e.DistributionChannel, "IX_ContactClass_Channel");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name, Catalan");

                entity.Property(e => e.DistributionChannel).HasComment("Distribution channel, if any. Foreign key for DistributionChannel table");

                entity.Property(e => e.Eng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name, English");

                entity.Property(e => e.Esp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name, Spanish");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((999))")
                    .HasComment("Sort order");

                entity.Property(e => e.Por)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name, Portuguese");

                entity.Property(e => e.Raffles).HasComment("True if members should participate as raffle prize pick up point");

                entity.Property(e => e.SalePoint).HasComment("True if members should be listed as sale points");

                entity.HasOne(d => d.DistributionChannelNavigation)
                    .WithMany(p => p.ContactClasses)
                    .HasForeignKey(d => d.DistributionChannel)
                    .HasConstraintName("FK_ContactClass_DistributionChannel");
            });

            modelBuilder.Entity<ContactGlnDeprecated>(entity =>
            {
                entity.HasKey(e => e.Gln)
                    .HasName("PK_ContactGln");

                entity.ToTable("ContactGln_Deprecated");

                entity.HasIndex(e => e.Contact, "ContactGln_Contact")
                    .HasFilter("([Contact] IS NOT NULL)");

                entity.Property(e => e.Gln)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("GLN")
                    .IsFixedLength()
                    .HasComment("Global Location Number for EDI partner identification");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.ContactGlnDeprecateds)
                    .HasForeignKey(d => d.Contact)
                    .HasConstraintName("FK_ContactGln_CliGral");
            });

            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ContactMessge");

                entity.ToTable("ContactMessage");

                entity.HasComment("Web visitor contact messages");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Email)
                    .HasColumnType("text")
                    .HasComment("User email address");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the message was created");

                entity.Property(e => e.Location)
                    .HasColumnType("text")
                    .HasComment("User location, free text");

                entity.Property(e => e.Nom)
                    .HasColumnType("text")
                    .HasComment("User name");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasComment("Message text");
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Content");

                entity.HasComment("Brand content for websites, Html formatted. Text stored in LangText table");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Visible)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Visible if true");
            });

            modelBuilder.Entity<ContentUrl>(entity =>
            {
                entity.HasKey(e => new { e.Target, e.Lang })
                    .HasName("PK_ContentUrl_1");

                entity.ToTable("ContentUrl");

                entity.HasIndex(e => e.UrlSegment, "IX_ContentUrl_Segment");

                entity.Property(e => e.Target).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength();

                entity.Property(e => e.UrlSegment)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CONTRACT");

                entity.ToTable("Contract");

                entity.HasComment("Contracts");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.CodiGuid).HasComment("Purpose code; foreign key for Contract_Codis");

                entity.Property(e => e.ContactGuid).HasComment("Contract counterpart; foreign key for CliGral table");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("Contract effective date");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("Contract expiry date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Document; foreign key for Docfile table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Num)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Contract number");

                entity.HasOne(d => d.CodiGu)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CodiGuid)
                    .HasConstraintName("FK_Contract_Contract_Codis");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ContactGuid)
                    .HasConstraintName("FK_Contract_CliGral");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Contract_DocFile");
            });

            modelBuilder.Entity<ContractCodi>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_Contract_Codis_1");

                entity.ToTable("Contract_Codis");

                entity.HasComment("Contract purposes");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Amortitzable).HasComment("True if refers to a depreciable asset");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasComment("Sort order");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Friendly name");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Country");

                entity.HasComment("Countries");

                entity.HasIndex(e => e.Iso, "IX_COUNTRY_ISO")
                    .IsUnique();

                entity.HasIndex(e => e.NomCat, "IX_Country_NomCat");

                entity.HasIndex(e => e.NomEng, "IX_Country_NomEng");

                entity.HasIndex(e => e.NomEsp, "IX_Country_NomEsp");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cee)
                    .HasColumnName("CEE")
                    .HasComment("whether the country belongs to UE");

                entity.Property(e => e.ExportCod).HasComment("Export code for Customs: National (1), European (2) others (3)");

                entity.Property(e => e.Flag)
                    .HasColumnType("image")
                    .HasComment("Country flag icon");

                entity.Property(e => e.Iso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code");

                entity.Property(e => e.LangIso)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LangISO")
                    .HasDefaultValueSql("('ENG')")
                    .HasComment("ISO 639-2 language code (3 letters) to address to residents");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Cat")
                    .HasComment("Country name in Catalan");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Eng")
                    .HasComment("Country name in English");

                entity.Property(e => e.NomEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Esp")
                    .HasComment("Country name in Spanish");

                entity.Property(e => e.NomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nom_Por")
                    .HasDefaultValueSql("('')")
                    .HasComment("Country name in Portuguese");

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("Country phone prefix");
            });

            modelBuilder.Entity<Credencial>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Credencial");

                entity.HasComment("User credentials to sign in partners systems");

                entity.HasIndex(e => e.Contact, "IX_Credencial_Contact");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Contact).HasComment("Owner of the site; foreign key for CliGral");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Password to sign in above site");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly caption");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Landing page");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.Property(e => e.Usuari)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("User name");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.Credencials)
                    .HasForeignKey(d => d.Contact)
                    .HasConstraintName("FK_Credencial_CliGral");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.CredencialUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Credencial_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.CredencialUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Credencial_Email");

                entity.HasMany(d => d.Owners)
                    .WithMany(p => p.Credencials)
                    .UsingEntity<Dictionary<string, object>>(
                        "CredencialOwner",
                        l => l.HasOne<Email>().WithMany().HasForeignKey("Owner").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CredencialOwner_Email"),
                        r => r.HasOne<Credencial>().WithMany().HasForeignKey("Credencial").HasConstraintName("FK_CredencialOwner_Credencial"),
                        j =>
                        {
                            j.HasKey("Credencial", "Owner");

                            j.ToTable("CredencialOwner").HasComment("Users with read, write and delete permissions to credentials");

                            j.HasIndex(new[] { "Owner", "Credencial" }, "IX_CredencialOwner").IsUnique();

                            j.IndexerProperty<Guid>("Credencial").HasComment("Foreign key for parent table Credencial");

                            j.IndexerProperty<Guid>("Owner").HasComment("Owners have write permissions for credentials. Foreign key for Email table");
                        });

                entity.HasMany(d => d.Rols)
                    .WithMany(p => p.Credencials)
                    .UsingEntity<Dictionary<string, object>>(
                        "CredencialRol",
                        l => l.HasOne<UsrRol>().WithMany().HasForeignKey("Rol").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CredencialRol_UsrRols"),
                        r => r.HasOne<Credencial>().WithMany().HasForeignKey("Credencial").HasConstraintName("FK_CredencialRol_Credencial"),
                        j =>
                        {
                            j.HasKey("Credencial", "Rol");

                            j.ToTable("CredencialRol").HasComment("Rols with readonly permissions to credentials");

                            j.HasIndex(new[] { "Rol", "Credencial" }, "IX_CredencialRol").IsUnique();

                            j.IndexerProperty<Guid>("Credencial").HasComment("Foreign key for Credencial table");

                            j.IndexerProperty<int>("Rol").HasComment("User rol. Foreign key for UsrRols table");
                        });
            });

            modelBuilder.Entity<Crr>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CRR");

                entity.ToTable("Crr");

                entity.HasComment("Correspondence documents");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Crr1 }, "IX_CRR");

                entity.HasIndex(e => new { e.Emp, e.Fch }, "IX_CRR_Fch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Atn)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')")
                    .HasComment("Destination person");

                entity.Property(e => e.Crr1)
                    .HasColumnName("Crr")
                    .HasComment("Sequential number, unique for each Company/year");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')")
                    .HasComment("Subject");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Date of issue/reception");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Document; foreign key for Docfile table");

                entity.Property(e => e.Rt).HasComment("Enumerable DTOCorrespondencia.Cods (1.received, 2.sent)");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for kast time; foreign key for Email table");

                entity.Property(e => e.Yea).HasComment("Year of issue/reception");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Crrs)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Crr_Emp");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Crrs)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Crr_DocFile");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.CrrUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_Crr_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.CrrUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_Crr_UsrLastEdited");

                entity.HasMany(d => d.CliGus)
                    .WithMany(p => p.MailGus)
                    .UsingEntity<Dictionary<string, object>>(
                        "CrrCli",
                        l => l.HasOne<CliGral>().WithMany().HasForeignKey("CliGuid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CrrCli_CliGral"),
                        r => r.HasOne<Crr>().WithMany().HasForeignKey("MailGuid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CrrCli_CRR"),
                        j =>
                        {
                            j.HasKey("MailGuid", "CliGuid");

                            j.ToTable("CrrCli").HasComment("Contacts receiving or sending correspondence");

                            j.HasIndex(new[] { "CliGuid", "MailGuid" }, "IX_CrrCli");

                            j.IndexerProperty<Guid>("MailGuid").HasComment("Document; Foreign key for Crr table");

                            j.IndexerProperty<Guid>("CliGuid").HasComment("Contact; Foreign key for CliGral table");
                        });
            });

            modelBuilder.Entity<Csa>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CSA");

                entity.ToTable("Csa");

                entity.HasComment("Bank remittances (remesas de efectos bancarios al cobro o al descuento)");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.BancGuid).HasComment("Our bank; foreign key for CliBnc table");

                entity.Property(e => e.CcaGuid).HasComment("Accounts entry; foreign key for Cca table");

                entity.Property(e => e.Csb).HasComment("Remittance number, unique for each Company/year");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Descomptat).HasComment("True if payments are advanced by the bank");

                entity.Property(e => e.Dias).HasComment("Number of days the bank is advancing the payment");

                entity.Property(e => e.Efts).HasComment("Number of payments included in the remittance");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Eur)
                    .HasColumnType("money")
                    .HasComment("Amount in Euro");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Remittance date");

                entity.Property(e => e.FileFormat).HasComment("Enumerable DTOCsa.FileFormats (norma 58, norma 19, remesa Caixa export, SEPA B2B, SEPA Core...)");

                entity.Property(e => e.Gts)
                    .HasColumnType("money")
                    .HasComment("Expenses");

                entity.Property(e => e.ImportMig)
                    .HasColumnType("money")
                    .HasComment("Average amount of the payments");

                entity.Property(e => e.Pts)
                    .HasColumnType("money")
                    .HasComment("Amount in foreign currency");

                entity.Property(e => e.Tae)
                    .HasColumnType("numeric(5, 2)")
                    .HasComment("Equivalent annual rate");

                entity.Property(e => e.Yea).HasComment("Remittance year");

                entity.HasOne(d => d.BancGu)
                    .WithMany(p => p.Csas)
                    .HasForeignKey(d => d.BancGuid)
                    .HasConstraintName("FK_CSA_CliBnc");

                entity.HasOne(d => d.CcaGu)
                    .WithMany(p => p.Csas)
                    .HasForeignKey(d => d.CcaGuid)
                    .HasConstraintName("FK_Csa_Cca");
            });

            modelBuilder.Entity<Csb>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CSB");

                entity.ToTable("Csb");

                entity.HasComment("Bank remittance items (efectos de las remesas bancarias)");

                entity.HasIndex(e => new { e.CsaGuid, e.Doc }, "IX_CSB_CsaDoc");

                entity.HasIndex(e => e.SepaMandato, "IX_CSB_Mandato");

                entity.HasIndex(e => e.Vto, "IX_CSB_Vto");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CcaVtoGuid).HasComment("In case the bank advances payments, the account entry when the customer pays on due date");

                entity.Property(e => e.Ccc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Debtor account number");

                entity.Property(e => e.CliGuid).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.CsaGuid).HasComment("Foreign key to parent table Csa");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Doc).HasComment("Consecutive payment number, unique on each remittance");

                entity.Property(e => e.Eur)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("Debt amount, in Euro");

                entity.Property(e => e.Fra).HasComment("Invoice, if any");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Debtor name");

                entity.Property(e => e.Result).HasComment("Enumerable DTOCsb.Results (Pending, due, unpaid, claimed)");

                entity.Property(e => e.SepaMandato).HasComment("Bank mandate; foreign key for Iban table");

                entity.Property(e => e.SepaTipoAdeudo).HasComment("Tipo de Adeudo AEB_SEPA_B2B Enumerable DTOCsb.TiposAdeudo (NotSet,FRST,RCUR,FNAL,OOF)");

                entity.Property(e => e.Txt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Debt description");

                entity.Property(e => e.Val)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("Debt amount, in foreign currency");

                entity.Property(e => e.Vto)
                    .HasColumnType("datetime")
                    .HasComment("Due date");

                entity.Property(e => e.Yef).HasComment("Year of invoice, if any");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.Csbs)
                    .HasForeignKey(d => d.CliGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Csb_CliGral");

                entity.HasOne(d => d.CsaGu)
                    .WithMany(p => p.Csbs)
                    .HasForeignKey(d => d.CsaGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CSB_CSA");

                entity.HasOne(d => d.SepaMandatoNavigation)
                    .WithMany(p => p.Csbs)
                    .HasForeignKey(d => d.SepaMandato)
                    .HasConstraintName("FK_CSB_IBAN");
            });

            modelBuilder.Entity<Cur>(entity =>
            {
                entity.HasKey(e => e.Tag)
                    .HasName("PK_CURRENCY");

                entity.ToTable("Cur");

                entity.HasComment("Currency");

                entity.Property(e => e.Tag)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Primary key; ISO 4217 3 digits currency code");

                entity.Property(e => e.Decimals).HasComment("Number of decimals used in this currency");

                entity.Property(e => e.Obsoleto).HasComment("True if the currency is outdated");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasComment("Symbol, if any");
            });

            modelBuilder.Entity<CurExchangeRate>(entity =>
            {
                entity.HasKey(e => new { e.Fch, e.Iso });

                entity.ToTable("CurExchangeRate");

                entity.HasComment("Currency Exchange rates");

                entity.HasIndex(e => new { e.Iso, e.Fch }, "CurExchangeRate_IDX_ISO_Fch")
                    .IsUnique();

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Date");

                entity.Property(e => e.Iso)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO")
                    .IsFixedLength()
                    .HasComment("ISO 4217 Currency code");

                entity.Property(e => e.Rate)
                    .HasColumnType("numeric(12, 6)")
                    .HasComment("Exchange rate of this currency on this date");
            });

            modelBuilder.Entity<CustomerCluster>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CustomerCluster");

                entity.HasComment("Cluster definitions for a certain holding");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Holding).HasComment("Holding the cluster is defined for. Foreign key to Holding table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Cluster friendly name");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.HasOne(d => d.HoldingNavigation)
                    .WithMany(p => p.CustomerClusters)
                    .HasForeignKey(d => d.Holding)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCluster_Holding");
            });

            modelBuilder.Entity<CustomerDto>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CustomerDto");

                entity.HasComment("Discount from Retail Price list VAT included a customer gets for a product range in order to generate his cost price list VAT excluded. Newer entries always override older entries for each customer and product range");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Customer).HasComment("Foreign key for CliGral table");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Discount over retail price list VAT included to get customer cost VAT excluded");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date. Further recent entries of same customer/product range override older ones");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments justifying this discount, if any");

                entity.Property(e => e.Product).HasComment("Product range. It may be a brand (Tpa table), a brand category (FK for Stp), or a single product sku (Art table)");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CustomerDtos)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerDto_CliGral");
            });

            modelBuilder.Entity<CustomerPlatform>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("CustomerPlatform");

                entity.HasComment("Delivery platforms available per customer");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Platform; foreign key for CliGral");

                entity.Property(e => e.Customer).HasComment("Customer owner of the platform; foreign key for CliGral");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CustomerPlatformCustomerNavigations)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPlatform_Customer");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.CustomerPlatformGu)
                    .HasForeignKey<CustomerPlatform>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPlatform_Platform");
            });

            modelBuilder.Entity<CustomerPlatformDestination>(entity =>
            {
                entity.HasKey(e => new { e.Platform, e.Destination });

                entity.ToTable("CustomerPlatformDestination");

                entity.HasComment("Destination centers covered by each delivery platform");

                entity.Property(e => e.Platform).HasComment("Delivery platform; foreign key for CustomerPlatform table");

                entity.Property(e => e.Destination).HasComment("Sales center, store or warehouse final destination of the deliveries sent to the platform; foreign key for CliGral table");

                entity.HasOne(d => d.PlatformNavigation)
                    .WithMany(p => p.CustomerPlatformDestinations)
                    .HasForeignKey(d => d.Platform)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPlatformDestination_Destination");

                entity.HasOne(d => d.Platform1)
                    .WithMany(p => p.CustomerPlatformDestinations)
                    .HasForeignKey(d => d.Platform)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPlatformDestination_CustomerPlatform");
            });

            modelBuilder.Entity<Default>(entity =>
            {
                entity.HasKey(e => new { e.Emp, e.Cod });

                entity.ToTable("Default");

                entity.HasComment("Stores system default values");

                entity.Property(e => e.Emp).HasComment("Company, foreign key for Emp table");

                entity.Property(e => e.Cod).HasComment("Enumerable DTODefault.Codis");

                entity.Property(e => e.Value)
                    .HasColumnType("text")
                    .HasComment("Default value for this code on this company");
            });

            modelBuilder.Entity<DefaultImage>(entity =>
            {
                entity.ToTable("DefaultImage");

                entity.HasComment("Default images for certain occasions");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Enumerable DTO.Defaults.ImgTypes");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasColumnName("image")
                    .HasComment("Image for this code");
            });

            modelBuilder.Entity<DeliveryShipment>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("DeliveryShipment");

                entity.HasComment("Log provided by our logistic center reporting delivery packages contents");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Delivery)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Our delivery number, formated YYYYNNNNNN");

                entity.Property(e => e.Emp).HasComment("Company; foreign key to Emp table");

                entity.Property(e => e.Expedition)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Logistic center expedition number");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Date");

                entity.Property(e => e.Line).HasComment("Delivery line number");

                entity.Property(e => e.Log).HasComment("Message received. Foreign key to JsonLog table");

                entity.Property(e => e.Package).HasComment("Package number");

                entity.Property(e => e.Packages).HasComment("Expedition total of number of packages");

                entity.Property(e => e.Pallet)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasComment("Pallet plate number, if any");

                entity.Property(e => e.Qty).HasComment("Units of this product inside this package");

                entity.Property(e => e.Sender)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("VAT number of logistic center");

                entity.Property(e => e.Sku).HasComment("Product Sku; foreign key for Art table");

                entity.Property(e => e.Sscc)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("SSCC")
                    .HasComment("Package plate number");

                entity.HasOne(d => d.LogNavigation)
                    .WithMany(p => p.DeliveryShipments)
                    .HasForeignKey(d => d.Log)
                    .HasConstraintName("FK_DeliveryShipment_JsonLog");
            });

            modelBuilder.Entity<DeliveryTracking>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("DeliveryTracking");

                entity.HasComment("Log provided by our logistic center reporting package transport tracking and costs");

                entity.HasIndex(e => e.Emp, "IX_DeliveryTracking_Suggested");

                entity.HasIndex(e => e.Emp, "IX_DeliveryTracking_Suggested2");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Cost in Eur");

                entity.Property(e => e.CubicKg)
                    .HasColumnType("decimal(9, 4)")
                    .HasComment("Weight adjusted by forwarder rate Kg/m3");

                entity.Property(e => e.Delivery)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Our delivery number, formated YYYYNNNNNN");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Date ");

                entity.Property(e => e.Forwarder)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("VAT number of the forwarder company");

                entity.Property(e => e.Height).HasComment("Package height, mm");

                entity.Property(e => e.Length).HasComment("Package length, mm");

                entity.Property(e => e.Log).HasComment("Message received. Foreign key to JsonLog table");

                entity.Property(e => e.Package).HasComment("Package number");

                entity.Property(e => e.Packages).HasComment("Total number of packages");

                entity.Property(e => e.Packaging)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Packaging (logistic center custom code)");

                entity.Property(e => e.Pallets).HasComment("Number of pallets, if any");

                entity.Property(e => e.Sender)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("VAT number of the sender partner");

                entity.Property(e => e.Sscc)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("SSCC")
                    .HasComment("Package plate");

                entity.Property(e => e.Tracking)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Shipment tracking number");

                entity.Property(e => e.Volume)
                    .HasColumnType("decimal(9, 6)")
                    .HasComment("Volume in m3");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Weight in Kg");

                entity.Property(e => e.Width).HasComment("Package width, mm");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.DeliveryTrackings)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryTracking_Emp");

                entity.HasOne(d => d.LogNavigation)
                    .WithMany(p => p.DeliveryTrackings)
                    .HasForeignKey(d => d.Log)
                    .HasConstraintName("FK_DeliveryTracking_JsonLog");
            });

            modelBuilder.Entity<Dept>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Dept");

                entity.HasComment("Product departments, to group brand categories into functional groups more intuitive for consumers");

                entity.HasIndex(e => new { e.Brand, e.Ord }, "IX_Dept_Brand");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Banner)
                    .HasColumnType("image")
                    .HasComment("Image ilustrating the department on websites");

                entity.Property(e => e.Brand).HasComment("Product brand. foreign key to Tpa table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Record creation date");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this record was edited for last time");

                entity.Property(e => e.Obsoleto).HasComment("Outdated if true");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((99))")
                    .HasComment("Department sort order");

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.Depts)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dept_TPA");

                entity.HasMany(d => d.Cnaps)
                    .WithMany(p => p.Depts)
                    .UsingEntity<Dictionary<string, object>>(
                        "DeptCnap",
                        l => l.HasOne<Cnap>().WithMany().HasForeignKey("Cnap").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DeptCnap_Cnap"),
                        r => r.HasOne<Dept>().WithMany().HasForeignKey("Dept").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_DeptCnap_Dept"),
                        j =>
                        {
                            j.HasKey("Dept", "Cnap");

                            j.ToTable("DeptCnap").HasComment("Relates Product departments with product functionalities as per classification Cnap");

                            j.IndexerProperty<Guid>("Dept").HasComment("Department id; foreign key to Dept table");

                            j.IndexerProperty<Guid>("Cnap").HasComment("Cnap id; foreign key to Cnap table");
                        });
            });

            modelBuilder.Entity<DistributionChannel>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("DistributionChannel");

                entity.HasComment("Distribution channels");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.ConsumerPriority)
                    .HasDefaultValueSql("((999))")
                    .HasComment("Priority when displayed to consumer");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Channel name (Catalan, if different from Spanish)");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Channel name (English, if different from Spanish)");

                entity.Property(e => e.NomEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Channel name (Spanish)");

                entity.Property(e => e.NomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Channel name (Portuguese, if different from Spanish)");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((999))")
                    .HasComment("Sort order");
            });

            modelBuilder.Entity<DocFile>(entity =>
            {
                entity.HasKey(e => e.Hash);

                entity.ToTable("DocFile");

                entity.HasComment("Documents");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('123456789012345678901234')")
                    .IsFixedLength()
                    .HasComment("Primary key, MD5 hash signature of the document");

                entity.Property(e => e.Doc).HasComment("Stream as byte array");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Creation date");

                entity.Property(e => e.Height).HasComment("Document height, in pixels");

                entity.Property(e => e.Hres)
                    .HasColumnName("HRes")
                    .HasComment("Horizontal resolution, if applicable");

                entity.Property(e => e.Mime).HasComment("Enumerable MatHelperStd.MimeCods");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Title");

                entity.Property(e => e.Obsolet).HasComment("If true, the document is outdated");

                entity.Property(e => e.Pags).HasComment("Pages count, if any");

                entity.Property(e => e.Size).HasComment("Bytes count");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("image")
                    .HasComment("Thumbnail 350x400 px");

                entity.Property(e => e.ThumbnailMime).HasDefaultValueSql("((1))");

                entity.Property(e => e.Vres)
                    .HasColumnName("VRes")
                    .HasComment("Vertical resoli¡ution, if applicable");

                entity.Property(e => e.Width).HasComment("Docuemnt width, in pixels");
            });

            modelBuilder.Entity<DocFileLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DocFileLog");

                entity.HasComment("Log of users who have browsed the documents");

                entity.HasIndex(e => new { e.Hash, e.Fch }, "IX_DocFileLog_HashFch");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the user browsed the document");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Document; foreign key for Docfile table");

                entity.Property(e => e.User).HasComment("User; foreign key for Email table");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Hash)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocFileLog_DocFile");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK_DocFileLog_Email");
            });

            modelBuilder.Entity<DocFileSrc>(entity =>
            {
                entity.HasKey(e => new { e.SrcGuid, e.SrcOrd, e.Hash, e.SrcCod })
                    .HasName("PK_DocFileSrc_1");

                entity.ToTable("DocFileSrc");

                entity.HasComment("Links objects with documents");

                entity.Property(e => e.SrcGuid).HasComment("Target object; foreign key to different tables like Vehicle, Mem, Immoble...");

                entity.Property(e => e.SrcOrd).HasComment("Sort order");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Document; foreign key for Docfile table");

                entity.Property(e => e.SrcCod).HasComment("Type of target object, enumerable DTODocfile.Cods");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.DocFileSrcs)
                    .HasForeignKey(d => d.Hash)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocFileSrc_DocFile");
            });

            modelBuilder.Entity<DownloadTarget>(entity =>
            {
                entity.HasKey(e => new { e.Download, e.Target })
                    .HasName("PK_ProductDownloadTarget");

                entity.ToTable("DownloadTarget");

                entity.HasComment("Used to specify different targets for a single document");

                entity.Property(e => e.Download).HasComment("foreign key to ProductDownload");

                entity.Property(e => e.Target).HasComment("Document owner");

                entity.Property(e => e.Cod).HasComment("Type of owner; enumerable DTOBaseGuidCodNom.Cods (1.vehicle, 2.product brand, 3.product category, 4.product sku, 5.phone line...)");

                entity.HasOne(d => d.DownloadNavigation)
                    .WithMany(p => p.DownloadTargets)
                    .HasForeignKey(d => d.Download)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDownloadTarget_ProductDownload");
            });

            modelBuilder.Entity<Ecidept>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ECIDept");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlantillaModSkuDocfile)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlantillaModSkuWeekDays)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EciplantillaModLog>(entity =>
            {
                entity.HasKey(e => new { e.Dept, e.Fch, e.Sku })
                    .HasName("PK_ECIDeptModLog");

                entity.ToTable("ECIPlantillaModLog");

                entity.Property(e => e.Fch).HasColumnType("datetime");
            });

            modelBuilder.Entity<Ecisku2021>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ECISku2021");

                entity.Property(e => e.Depto)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ean)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN");

                entity.Property(e => e.Eci)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ECI");
            });

            modelBuilder.Entity<EcitransmCentre>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ECITransmCentre");

                entity.HasComment("El Corte Ingles centers and the shipment groups they belong to share a common delivery platform");

                entity.HasIndex(e => e.Centre, "IX_ECITransmCentre_Centre");

                entity.HasIndex(e => new { e.Parent, e.Centre }, "IX_ECITransmCentre_ParentCentre");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Centre).HasComment("Final destination center; foreign key for CliGral table");

                entity.Property(e => e.Parent).HasComment("Foreign key to parent table ECITransmGroup");

                entity.HasOne(d => d.CentreNavigation)
                    .WithMany(p => p.EcitransmCentres)
                    .HasForeignKey(d => d.Centre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ECITransmCentre_CliGral");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.EcitransmCentres)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ECITransmCentre_ECITransmGroup");
            });

            modelBuilder.Entity<EcitransmGroup>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ECITransmGroup");

                entity.HasComment("Shipments to El Corte Ingles customer are grouped by destinations which share a common delivery platform. This table stores the group name and the delivery platform, and its children on EciTransmCentre table store the final destination centers for the shipments");

                entity.HasIndex(e => e.Ord, "IX_ECITransmGroup_Ord");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Group friendly name");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Platform).HasComment("Delivery platform; foreign key for CliGral table");

                entity.HasOne(d => d.PlatformNavigation)
                    .WithMany(p => p.EcitransmGroups)
                    .HasForeignKey(d => d.Platform)
                    .HasConstraintName("FK_ECITransmGroup_Platform");
            });

            modelBuilder.Entity<Edi>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Edi");

                entity.HasComment("Edi messages log (sent and received)");

                entity.HasIndex(e => e.Fch, "IX_Edi_Fch");

                entity.HasIndex(e => new { e.Tag, e.FchCreated }, "IX_Edi_TagFchCreated");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Docnum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Document number");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Total amount, if applicable, in Euros");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Message date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the message waas registered");

                entity.Property(e => e.Filename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Name of the file in the server");

                entity.Property(e => e.Iocod)
                    .HasColumnName("IOcod")
                    .HasComment("Enumerable DTOEdiversaFile.IOCods (1.Inbox, 2.Outbox)");

                entity.Property(e => e.Receiver)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComment("GLN EAN 13 code assigned to the receiver party of the message");

                entity.Property(e => e.Result).HasComment("Enumerable DTOEdiversaFile.Results (0.Pending, 1.Processed, 2.Deleted)");

                entity.Property(e => e.ResultGuid).HasComment("Primary key to result table depending on the message type");

                entity.Property(e => e.Sender)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComment("GLN EAN 13 code assigned to the sender party of the message");

                entity.Property(e => e.Tag)
                    .HasMaxLength(22)
                    .HasComment("Message identifier. Enumerable DTOEdiversaFile.Tags (ORDERS_D_96A_UN_EAN008, ...)");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasComment("Raw text of the message");

                entity.Property(e => e.Val)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Total amount, if applicable, in foreign currency");
            });

            modelBuilder.Entity<EdiInvrptHeader>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_EdiInvrpt");

                entity.ToTable("EdiInvrptHeader");

                entity.HasIndex(e => new { e.Nadms, e.Fch }, "EdiInvrptHeader_Nadms_Fch");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.DocNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nadgy)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADGY")
                    .IsFixedLength()
                    .HasComment("Stock holder party");

                entity.Property(e => e.Nadms)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADMS")
                    .IsFixedLength()
                    .HasComment("Message sender party");
            });

            modelBuilder.Entity<EdiInvrptItem>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Lin })
                    .HasName("PK_EdiInvrptItem_1");

                entity.ToTable("EdiInvrptItem");

                entity.Property(e => e.Ean)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RefCustomer)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RefSupplier)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.EdiInvrptItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiInvrptItem_EdiInvrptHeader");
            });

            modelBuilder.Entity<EdiRemadvHeader>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("EdiRemadvHeader");

                entity.HasComment("Remittance advice messages");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Amt)
                    .HasColumnType("decimal(9, 2)")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Total remittance amount");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Document number");

                entity.Property(e => e.DocRef)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Document reference");

                entity.Property(e => e.EmisorPago).HasComment("Remittance issuer; foreign key for CliGral table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchDoc)
                    .HasColumnType("date")
                    .HasComment("Document date");

                entity.Property(e => e.FchVto)
                    .HasColumnType("date")
                    .HasComment("Due date");

                entity.Property(e => e.ReceptorPago).HasComment("Remittance receiver; foreign key for CliGral table");

                entity.Property(e => e.Result).HasComment("Accounts entry; foreign key for Cca table");

                entity.HasOne(d => d.EmisorPagoNavigation)
                    .WithMany(p => p.EdiRemadvHeaderEmisorPagoNavigations)
                    .HasForeignKey(d => d.EmisorPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiRemadvHeader_Emisor");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.EdiRemadvHeader)
                    .HasForeignKey<EdiRemadvHeader>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiRemadvHeader_Edi");

                entity.HasOne(d => d.ReceptorPagoNavigation)
                    .WithMany(p => p.EdiRemadvHeaderReceptorPagoNavigations)
                    .HasForeignKey(d => d.ReceptorPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiRemadvHeader_Receptor");

                entity.HasOne(d => d.ResultNavigation)
                    .WithMany(p => p.EdiRemadvHeaders)
                    .HasForeignKey(d => d.Result)
                    .HasConstraintName("FK_EdiRemadvHeader_Cca");
            });

            modelBuilder.Entity<EdiRemadvItem>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Idx });

                entity.ToTable("EdiRemadvItem");

                entity.HasComment("Documents included on each remittance advice");

                entity.Property(e => e.Parent).HasComment("Foreign key to parent table EdiRemadvHeader");

                entity.Property(e => e.Idx).HasComment("Sort order within same remittance");

                entity.Property(e => e.ItemAmt)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Document amount");

                entity.Property(e => e.ItemCur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.ItemFch)
                    .HasColumnType("date")
                    .HasComment("Daocument date");

                entity.Property(e => e.ItemNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Document concept");

                entity.Property(e => e.ItemNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Document number");

                entity.Property(e => e.ItemType).HasComment("Enumerable DTOEdiversaRemadvItem.Types");

                entity.Property(e => e.PndGuid).HasComment("Debt; foreign key to Pnd table");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.EdiRemadvItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiRemadvItem_EdiRemadvHeader");
            });

            modelBuilder.Entity<EdiversaDesadvHeader>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("EdiversaDesadvHeader");

                entity.HasComment("Delivery notes sent/received by Edi");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Bgm)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Document number");

                entity.Property(e => e.Entrega).HasComment("Delivery destination (usually our warehouse); foreign key for CliGral table");

                entity.Property(e => e.FchDoc)
                    .HasColumnType("date")
                    .HasComment("Document date");

                entity.Property(e => e.FchShip)
                    .HasColumnType("date")
                    .HasComment("Shipping date");

                entity.Property(e => e.NadBy)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("EAN 13 code for buyer GLN");

                entity.Property(e => e.NadDp)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("EAN 13 code for delivery point GLN");

                entity.Property(e => e.NadSu)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("EAN 13 code for supplier GLN");

                entity.Property(e => e.Proveidor).HasComment("Supplier; foreign key for CliGral table");

                entity.Property(e => e.PurchaseOrder).HasComment("Purchase order; foreign key for Pdc table");

                entity.Property(e => e.Rff)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Order confirmation");

                entity.HasOne(d => d.EntregaNavigation)
                    .WithMany(p => p.EdiversaDesadvHeaderEntregaNavigations)
                    .HasForeignKey(d => d.Entrega)
                    .HasConstraintName("FK_EdiversaDesadvHeader_Entrega");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.EdiversaDesadvHeader)
                    .HasForeignKey<EdiversaDesadvHeader>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaDesadvHeader_Edi");

                entity.HasOne(d => d.ProveidorNavigation)
                    .WithMany(p => p.EdiversaDesadvHeaderProveidorNavigations)
                    .HasForeignKey(d => d.Proveidor)
                    .HasConstraintName("FK_EdiversaDesadvHeader_Proveidor");

                entity.HasOne(d => d.PurchaseOrderNavigation)
                    .WithMany(p => p.EdiversaDesadvHeaders)
                    .HasForeignKey(d => d.PurchaseOrder)
                    .HasConstraintName("FK_EdiversaDesadvHeader_Pdc");
            });

            modelBuilder.Entity<EdiversaDesadvItem>(entity =>
            {
                entity.HasKey(e => new { e.Desadv, e.Lin });

                entity.ToTable("EdiversaDesadvItem");

                entity.HasComment("Delivery note items");

                entity.Property(e => e.Desadv).HasComment("Foreign key for parent table EdiversaDesadvHeader");

                entity.Property(e => e.Lin).HasComment("Line number");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Product name");

                entity.Property(e => e.Ean)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Product EAN 13 code");

                entity.Property(e => e.Qty).HasComment("Units delivered");

                entity.Property(e => e.Ref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Product code");

                entity.Property(e => e.Sku).HasComment("Product; foreign key for Art table");

                entity.HasOne(d => d.DesadvNavigation)
                    .WithMany(p => p.EdiversaDesadvItems)
                    .HasForeignKey(d => d.Desadv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaDesadvItem_EdiversaDesadvHeader");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.EdiversaDesadvItems)
                    .HasForeignKey(d => d.Sku)
                    .HasConstraintName("FK_EdiversaDesadvItem_Art");
            });

            modelBuilder.Entity<EdiversaException>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Exceptions thrown when validating Edi files");

                entity.HasIndex(e => e.Parent, "IX_EdiversaExceptions_Parent");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Reason code. Enumerable DTOEdiversaException.Cods");

                entity.Property(e => e.Msg)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Exception message");

                entity.Property(e => e.Parent).HasComment("Parent Edi message where the exception was thrown");

                entity.Property(e => e.Tag)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.TagCod)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Object type of TagGuid, enumerable DTOEdiversaException.TagCods");

                entity.Property(e => e.TagGuid).HasComment("Object the exception refers to");
            });

            modelBuilder.Entity<EdiversaInterlocutor>(entity =>
            {
                entity.HasKey(e => e.Ean);

                entity.ToTable("EdiversaInterlocutor");

                entity.HasComment("GLN EAN 13 Codes of the entities whom we have agreed to exchange Edi documents");

                entity.Property(e => e.Ean)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("EAN 13 code of the counterpart");
            });

            modelBuilder.Entity<EdiversaOrderHeader>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_EdiOrderHeader");

                entity.ToTable("EdiversaOrderHeader");

                entity.HasComment("Orders received through Edi messages");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Centro)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("For El Corte Ingles, sales center");

                entity.Property(e => e.Comprador).HasComment("Buyer. Foreign key for CliGral table");

                entity.Property(e => e.CompradorEan)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("CompradorEAN")
                    .HasDefaultValueSql("('')")
                    .HasComment("Buyer. GLN EAN 13 code from NADBY segment");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Departamento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("For El Corte Ingles, Department");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Order number");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Order value in Euros");

                entity.Property(e => e.FacturarA).HasComment("Customer who should be invoiced for this order. Foreign key for CliGral table");

                entity.Property(e => e.FacturarAean)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("FacturarAEAN")
                    .HasComment("Customer who should be invoiced for this order. GLN EAN 13 code from NADIV segment");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this record was created");

                entity.Property(e => e.FchDelivery)
                    .HasColumnType("date")
                    .HasComment("Delivery date");

                entity.Property(e => e.FchDoc)
                    .HasColumnType("date")
                    .HasComment("Order date");

                entity.Property(e => e.FchLast)
                    .HasColumnType("date")
                    .HasComment("Delivery deadline");

                entity.Property(e => e.Funcion).HasComment("Third field of \"ORD\" segment from Ediversa ORDERS message");

                entity.Property(e => e.Nadms)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADMS")
                    .HasComment("Edi message sender (to be used as message receiver NADMR on DESADV messages)");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments. Contents from FTX segment");

                entity.Property(e => e.Proveedor).HasComment("Supplier. GLN EAN 13 code from NADSU segment");

                entity.Property(e => e.ReceptorMercancia).HasComment("Deliver point. Foreign key for CliGral table");

                entity.Property(e => e.ReceptorMercanciaEan)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("ReceptorMercanciaEAN")
                    .HasDefaultValueSql("('')")
                    .HasComment("Deliver point. GLN EAN 13 code for NADDP segment");

                entity.Property(e => e.Result).HasComment("Purchase order. Foreign key for Pdc table");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasComment("Document name code, as defined in https://www.gs1.org/sites/default/files/docs/eancom/s4/orders.pdf");

                entity.Property(e => e.Val)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Order value in foreign currency");

                entity.HasOne(d => d.CompradorNavigation)
                    .WithMany(p => p.EdiversaOrderHeaderCompradorNavigations)
                    .HasForeignKey(d => d.Comprador)
                    .HasConstraintName("FK_EdiversaOrderHeader_Comprador");

                entity.HasOne(d => d.FacturarANavigation)
                    .WithMany(p => p.EdiversaOrderHeaderFacturarANavigations)
                    .HasForeignKey(d => d.FacturarA)
                    .HasConstraintName("FK_EdiversaOrderHeader_FacturarA");

                entity.HasOne(d => d.ProveedorNavigation)
                    .WithMany(p => p.EdiversaOrderHeaderProveedorNavigations)
                    .HasForeignKey(d => d.Proveedor)
                    .HasConstraintName("FK_EdiversaOrderHeader_Proveidor");

                entity.HasOne(d => d.ReceptorMercanciaNavigation)
                    .WithMany(p => p.EdiversaOrderHeaderReceptorMercanciaNavigations)
                    .HasForeignKey(d => d.ReceptorMercancia)
                    .HasConstraintName("FK_EdiversaOrderHeader_ReceptorMercancia");

                entity.HasOne(d => d.ResultNavigation)
                    .WithMany(p => p.EdiversaOrderHeaders)
                    .HasForeignKey(d => d.Result)
                    .HasConstraintName("FK_EdiversaOrderHeader_Pdc");
            });

            modelBuilder.Entity<EdiversaOrderItem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("EdiversaOrderItem");

                entity.HasComment("Line items from orders received through Edi messages");

                entity.HasIndex(e => new { e.Parent, e.Lin }, "IX_EdiversaOrderItem_ParentLin")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product name");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Discount");

                entity.Property(e => e.Ean)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Product EAN 13 code");

                entity.Property(e => e.Lin).HasComment("Line number");

                entity.Property(e => e.Parent).HasComment("Foreign key to parent table EdiversaOrderHeader");

                entity.Property(e => e.Preu)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Price");

                entity.Property(e => e.Qty).HasComment("Units");

                entity.Property(e => e.RefClient)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer product code");

                entity.Property(e => e.RefProveidor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Manufacturer product code");

                entity.Property(e => e.SkipDtoValidationFch)
                    .HasColumnType("datetime")
                    .HasComment("Date it was authorized a different discount, if any");

                entity.Property(e => e.SkipDtoValidationUser).HasComment("if discount differs from standard, user who autorized it. Foreign key to Email table");

                entity.Property(e => e.SkipItemFch)
                    .HasColumnType("datetime")
                    .HasComment("Date it was denied to be processed");

                entity.Property(e => e.SkipItemUser).HasComment("If this line should not be processed, user who authorized it. Foreign key to Email table");

                entity.Property(e => e.SkipPreuValidationFch)
                    .HasColumnType("datetime")
                    .HasComment("Date when the user authorised a different price, if any");

                entity.Property(e => e.SkipPreuValidationUser).HasComment("If price differs from standard, user who authorized it. Foreign key to Email table");

                entity.Property(e => e.Sku).HasComment("Product; foireign key to Art table");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.EdiversaOrderItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaOrderItem_EdiversaOrderHeader");

                entity.HasOne(d => d.SkipDtoValidationUserNavigation)
                    .WithMany(p => p.EdiversaOrderItemSkipDtoValidationUserNavigations)
                    .HasForeignKey(d => d.SkipDtoValidationUser)
                    .HasConstraintName("FK_EdiversaOrderItem_SkipDtoValidationUser");

                entity.HasOne(d => d.SkipItemUserNavigation)
                    .WithMany(p => p.EdiversaOrderItemSkipItemUserNavigations)
                    .HasForeignKey(d => d.SkipItemUser)
                    .HasConstraintName("FK_EdiversaOrderItem_SkipItemUser");

                entity.HasOne(d => d.SkipPreuValidationUserNavigation)
                    .WithMany(p => p.EdiversaOrderItemSkipPreuValidationUserNavigations)
                    .HasForeignKey(d => d.SkipPreuValidationUser)
                    .HasConstraintName("FK_EdiversaOrderItem_SkipPreuValidationUser");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.EdiversaOrderItems)
                    .HasForeignKey(d => d.Sku)
                    .HasConstraintName("FK_EdiversaOrderItem_Art");
            });

            modelBuilder.Entity<EdiversaOrdrspHeader>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("EdiversaOrdrspHeader");

                entity.HasComment("Order confirmations sent or received through Edi");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.EdiversaOrder).HasComment("Purchase order which this document is confirming");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Document date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.HasOne(d => d.EdiversaOrderNavigation)
                    .WithMany(p => p.EdiversaOrdrspHeaderEdiversaOrderNavigations)
                    .HasForeignKey(d => d.EdiversaOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaOrdrspHeader_Edi1");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.EdiversaOrdrspHeaderGu)
                    .HasForeignKey<EdiversaOrdrspHeader>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaOrdrspHeader_Edi");
            });

            modelBuilder.Entity<EdiversaOrdrspItem>(entity =>
            {
                entity.HasKey(e => e.OrderItem)
                    .HasName("PK_EdiversaOrdrspItem_1");

                entity.ToTable("EdiversaOrdrspItem");

                entity.HasComment("Order confirmation lines");

                entity.Property(e => e.OrderItem)
                    .ValueGeneratedNever()
                    .HasComment("Purchase order line. Foreign key to EdiversaOrderItem table");

                entity.Property(e => e.Parent).HasComment("Document; foreign key to parent table EdiversaorderSp table");

                entity.Property(e => e.Qty).HasComment("Units confirmed");

                entity.HasOne(d => d.OrderItemNavigation)
                    .WithOne(p => p.EdiversaOrdrspItem)
                    .HasForeignKey<EdiversaOrdrspItem>(d => d.OrderItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaOrdrspItem_EdiversaOrderItem");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.EdiversaOrdrspItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaOrdrspItem_EdiversaOrdrspHeader");
            });

            modelBuilder.Entity<EdiversaRecadvHdr>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("EdiversaRecadvHdr");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Bgm)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.Dtm).HasColumnType("datetime");

                entity.Property(e => e.NadBy)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.RffDq)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.RffOn)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EdiversaRecadvItm>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Lin });

                entity.ToTable("EdiversaRecadvItm");

                entity.Property(e => e.Ean)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Pialin)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EdiversaSalesReportHeader>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_EdiversaSalesReportHeader2");

                entity.ToTable("EdiversaSalesReportHeader");

                entity.HasComment("Customer sales reports received through Edi");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 4217 Currency code");

                entity.Property(e => e.Customer).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Document date");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Document num");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.EdiversaSalesReportHeaders)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FK_EdiversaSalesReportHeader_CliGral");
            });

            modelBuilder.Entity<EdiversaSalesReportItem>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_EdiversaSalesReport");

                entity.ToTable("EdiversaSalesReportItem");

                entity.HasComment("Customer sales report items");

                entity.HasIndex(e => new { e.Customer, e.Fch, e.Sku }, "IX_EdiversaSalesReportItem_Customer_Fch_Sku");

                entity.HasIndex(e => e.Parent, "IX_EdiversaSalesReportItem_Parent");

                entity.HasIndex(e => e.Sku, "IX_EdiversaSalesReportItem_Sku");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Centro)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("Sales center");

                entity.Property(e => e.Customer).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.Dept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Department");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Total sales on this date of this product on this center and department");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Sales date");

                entity.Property(e => e.Parent).HasComment("Foreign key for parent table EdiversaSalesReportHeader");

                entity.Property(e => e.Qty).HasComment("Units sold");

                entity.Property(e => e.QtyBack).HasComment("Units returned");

                entity.Property(e => e.Sku).HasComment("Product; foreign key for Art table");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.EdiversaSalesReportItems)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FK_EdiversaSalesReportItem_CliGral");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.EdiversaSalesReportItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EdiversaSalesReportItem_EdiversaSalesReportHeader");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.EdiversaSalesReportItems)
                    .HasForeignKey(d => d.Sku)
                    .HasConstraintName("FK_EdiversaSalesReportItem_ART");
            });

            modelBuilder.Entity<ElCorteInglesAlineamientoStock>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasIndex(e => e.Fch, "NonClusteredIndex-20220228-181526");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Text).HasColumnType("text");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_EMAIL");

                entity.ToTable("Email");

                entity.HasComment("Users");

                entity.HasIndex(e => new { e.Emp, e.Adr }, "IX_EMAIL_ID_ADR")
                    .IsUnique();

                entity.HasIndex(e => e.Guid, "IX_EMAIL_ID_GUID")
                    .IsUnique();

                entity.HasIndex(e => new { e.Emp, e.Id }, "IX_Email_Emp_Id");

                entity.HasIndex(e => e.Hash, "IX_Email_Hash");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Postal address");

                entity.Property(e => e.Adr)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Email address");

                entity.Property(e => e.BadMailGuid).HasComment("foreign key for Cod table if emails to this user are being returned");

                entity.Property(e => e.BirthYea).HasComment("Year of birth");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasComment("User birthday");

                entity.Property(e => e.ChildCount).HasComment("Number of children");

                entity.Property(e => e.Cognoms)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("User surname");

                entity.Property(e => e.DefaultContactGuid).HasComment("If registered proffessional, foreign key for CliGral table");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.FchActivated)
                    .HasColumnType("datetime")
                    .HasComment("Date the user email address was verified");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchDeleted)
                    .HasColumnType("datetime")
                    .HasComment("Date the user requested to be deleted");

                entity.Property(e => e.Hash)
                    .HasMaxLength(44)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.LastChildBirthday)
                    .HasColumnType("date")
                    .HasComment("Birthday of youngest children");

                entity.Property(e => e.Location).HasComment("foreign key for Location table");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Location name");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("User alias");

                entity.Property(e => e.NoNews).HasComment("If true, do not send general mailings");

                entity.Property(e => e.Nom)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("User name");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Obsoleto).HasComment("True if no longer active");

                entity.Property(e => e.Pais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 3166-1 Country");

                entity.Property(e => e.Privat).HasComment("If true, do not send auitomated messages there");

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Province name");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("User password");

                entity.Property(e => e.Rol).HasComment("Enumerable DTORol.Ids");

                entity.Property(e => e.Sex).HasComment("Enumerable DTOEnums.Sex");

                entity.Property(e => e.Source).HasComment("Enumerable DTOUser.Sources");

                entity.Property(e => e.Tel)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Phone number");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Postal code");

                entity.HasOne(d => d.BadMailGu)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.BadMailGuid)
                    .HasConstraintName("FK_Email_Cod");

                entity.HasOne(d => d.DefaultContactGu)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.DefaultContactGuid)
                    .HasConstraintName("FK_Email_CliGral");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Email_Emp");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.Location)
                    .HasConstraintName("FK_Email_Location");

                entity.HasOne(d => d.ResidenceNavigation)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.Residence)
                    .HasConstraintName("FK_Email_Zip");
            });

            modelBuilder.Entity<EmailCli>(entity =>
            {
                entity.HasKey(e => new { e.EmailGuid, e.ContactGuid })
                    .HasName("PK_EMAIL_CLIS_1");

                entity.ToTable("Email_Clis");

                entity.HasComment("Users per contact");

                entity.HasIndex(e => new { e.ContactGuid, e.Ord }, "IX_EMAIL_CLIS_ContactGuid");

                entity.Property(e => e.EmailGuid).HasComment("User. Foreign key for Email table");

                entity.Property(e => e.ContactGuid).HasComment("Contact. Foreign key for CliGral table");

                entity.Property(e => e.Ord).HasComment("Sort order in which this user should appear within this contact");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.EmailClis)
                    .HasForeignKey(d => d.ContactGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMAIL_CLIS_CliGral");

                entity.HasOne(d => d.EmailGu)
                    .WithMany(p => p.EmailClis)
                    .HasForeignKey(d => d.EmailGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMAIL_CLIS_EMAIL");
            });

            modelBuilder.Entity<Emp>(entity =>
            {
                entity.HasKey(e => e.Emp1)
                    .HasName("PK_Emp_1");

                entity.ToTable("Emp");

                entity.HasComment("Company");

                entity.Property(e => e.Emp1)
                    .ValueGeneratedNever()
                    .HasColumnName("Emp")
                    .HasComment("Primary key");

                entity.Property(e => e.Abr)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Company friendly name");

                entity.Property(e => e.Cnae).HasComment("Company activity code (Clasificación Nacional de Actividades Económicas)");

                entity.Property(e => e.DadesRegistrals)
                    .HasColumnType("text")
                    .HasComment("Commercial Register details, free text. Used on Edi");

                entity.Property(e => e.Domini)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Internet domain");

                entity.Property(e => e.Ip)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MailboxPort)
                    .HasDefaultValueSql("((25))")
                    .HasComment("Mailbox Smtp port");

                entity.Property(e => e.MailboxPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Maibox password");

                entity.Property(e => e.MailboxSmtp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Mailbox Smtp server");

                entity.Property(e => e.MailboxUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Mailbox user name");

                entity.Property(e => e.Mgz).HasComment("Warehouse; fore¡gn key for CliGral table");

                entity.Property(e => e.MsgFrom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Corporate email address");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Company name");

                entity.Property(e => e.Org).HasComment("Company properties; foreign key for CliGral table");

                entity.Property(e => e.Taller).HasComment("Workshop; foreign key for CliGral table");
            });

            modelBuilder.Entity<Escriptura>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_Escriptures");

                entity.ToTable("Escriptura");

                entity.HasComment("Notarial deeds");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Codi).HasComment("Purpose. Enumerable DTOEscriptura.Codis");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Effective date");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Expiry date");

                entity.Property(e => e.Folio).HasComment("Commercial Register Page");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Document. Foreign key for Docfile table");

                entity.Property(e => e.Hoja)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Commercial Register Sheet number");

                entity.Property(e => e.Inscripcio).HasComment("Commercial Register Record number");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Notari).HasComment("Notary; foreign key for CliGral table");

                entity.Property(e => e.NumProtocol).HasComment("Document number");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.RegistreMercantil).HasComment("Commercial Register; foreign key for CliGral table");

                entity.Property(e => e.Tomo).HasComment("Commercial Register Book");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Escripturas)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Escriptura_DocFile");

                entity.HasOne(d => d.NotariNavigation)
                    .WithMany(p => p.EscripturaNotariNavigations)
                    .HasForeignKey(d => d.Notari)
                    .HasConstraintName("FK_Escriptura_Notari");

                entity.HasOne(d => d.RegistreMercantilNavigation)
                    .WithMany(p => p.EscripturaRegistreMercantilNavigations)
                    .HasForeignKey(d => d.RegistreMercantil)
                    .HasConstraintName("FK_Escriptura_RegistreMercantil");
            });

            modelBuilder.Entity<Exception>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Exception");

                entity.HasComment("Exceptions from different operations that need to be recorded. For example, validation exceptions detected when importing invoices received from Edi console");

                entity.HasIndex(e => new { e.Parent, e.Cod }, "Ix_Exception");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Versatile code each target object may refer to with its own enumerable");

                entity.Property(e => e.Msg)
                    .HasColumnType("text")
                    .HasComment("Descriptive message to the user");

                entity.Property(e => e.Parent).HasComment("Object target of the exception. Foreign key to different tables depending on the operation");

                entity.Property(e => e.Tag).HasComment("Optional object to be used when proposing solutions to the exception");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Feedback");

                entity.HasComment("Users feedback events log");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Feedback event type (1.Like, 2.Share)");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time of the event");

                entity.Property(e => e.Target).HasComment("Foreign key to the feedback source object; maybe for example a raffle");

                entity.Property(e => e.UserOrCustomer).HasComment("Foreign key  to either Email or CliGral depending on UserOrCustomerCod field value");

                entity.Property(e => e.UserOrCustomerCod).HasComment("Enumerable (1.User, 2.Customer)");
            });

            modelBuilder.Entity<Filter>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Filter");

                entity.HasComment("Product features filters");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Ord).HasComment("Sort order where this filter should be displayed");
            });

            modelBuilder.Entity<FilterItem>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_FilterItems");

                entity.ToTable("FilterItem");

                entity.HasComment("Options for product filters");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Filter).HasComment("Foreign key for parent table Filter");

                entity.Property(e => e.Ord).HasComment("Sort order in which this option should appear within its filter");

                entity.HasOne(d => d.FilterNavigation)
                    .WithMany(p => p.FilterItems)
                    .HasForeignKey(d => d.Filter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilterItem_Filter");
            });

            modelBuilder.Entity<FilterTarget>(entity =>
            {
                entity.HasKey(e => new { e.FilterItem, e.Target });

                entity.ToTable("FilterTarget");

                entity.HasComment("Products where filter options are applicable");

                entity.HasIndex(e => e.Target, "IX_FilterTarget_Target");

                entity.Property(e => e.FilterItem).HasComment("Filter option; foreign key for FilterItem");

                entity.Property(e => e.Target).HasComment("Product where this option is applicable");

                entity.HasOne(d => d.FilterItemNavigation)
                    .WithMany(p => p.FilterTargets)
                    .HasForeignKey(d => d.FilterItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilterTarget_FilterItem");
            });

            modelBuilder.Entity<Forecast>(entity =>
            {
                entity.HasKey(e => new { e.Sku, e.Yea, e.Mes, e.FchCreated });

                entity.ToTable("Forecast");

                entity.HasComment("Order forecasts");

                entity.Property(e => e.Sku).HasComment("Product; foreign key for Art table");

                entity.Property(e => e.Yea).HasComment("Forecast year");

                entity.Property(e => e.Mes).HasComment("Forecast month");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created; just the last entry for same year/month is valid");

                entity.Property(e => e.Customer).HasComment("If null, it means it is our own sales forecast; otherelse it is customer's forecast; foreign key for CliGral table");

                entity.Property(e => e.Qty).HasComment("Units forecasted");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Forecasts)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FK_Forecast_CliGral");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.Forecasts)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Forecast_Art");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.Forecasts)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_Forecast_Email");
            });

            modelBuilder.Entity<Fra>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_FRA");

                entity.ToTable("Fra");

                entity.HasComment("Issued invoices");

                entity.HasIndex(e => e.CcaGuid, "IX_FRA_CcaGuid");

                entity.HasIndex(e => new { e.CliGuid, e.Yea, e.Serie, e.Fra1 }, "IX_FRA_CliGuidYeaFra");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Serie, e.Fra1 }, "IX_FRA_EmpYeaFra")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasColumnType("text")
                    .HasComment("Address, without location and postal code");

                entity.Property(e => e.CcaGuid).HasComment("Accounts entry; foreign key for Cca table");

                entity.Property(e => e.Cfp).HasComment("Payment method.Enumerable DTOPaymentTerms.CodsFormaDePago");

                entity.Property(e => e.CliGuid).HasComment("Customer. Foreign key to CliGral table");

                entity.Property(e => e.Concepte).HasComment("Enumerable DTOInvoice.Conceptes (1.Sales, 2.Services...)");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("Currency (ISO 4217)");

                entity.Property(e => e.DppAmt)
                    .HasColumnType("money")
                    .HasComment("Early payment discount amount");

                entity.Property(e => e.DppBase)
                    .HasColumnType("money")
                    .HasComment("Early payment discount base");

                entity.Property(e => e.DppPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Early payment discount percentage");

                entity.Property(e => e.DtoAmt)
                    .HasColumnType("money")
                    .HasComment("Discount amount");

                entity.Property(e => e.DtoBase)
                    .HasColumnType("money")
                    .HasComment("Discount base");

                entity.Property(e => e.DtoPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Discount percentage");

                entity.Property(e => e.EmailedToGuid).HasComment("Date this invoice was emailed");

                entity.Property(e => e.Emp).HasComment("Company. Foreign key to Emp table");

                entity.Property(e => e.EurBase)
                    .HasColumnType("numeric(18, 2)")
                    .HasComment("Tax base amount in Euro");

                entity.Property(e => e.EurLiq)
                    .HasColumnType("money")
                    .HasComment("Total cash, Euros");

                entity.Property(e => e.ExportCod).HasComment("Enumerable DTOInvoice.ExportCods (1.National, 2.EU, 3.rest)");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Invoice date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.FchLastPrinted)
                    .HasColumnType("datetime")
                    .HasComment("Date this invoice was printed for last time");

                entity.Property(e => e.Fpg)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("Payment method details");

                entity.Property(e => e.Fra1)
                    .HasColumnName("Fra")
                    .HasComment("Invoice number, within each Company, Year and Serie");

                entity.Property(e => e.Incoterm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("International Commerce Terms. foreign key for Incoterms table");

                entity.Property(e => e.Intrastat).HasComment("Import/Export Customs declaration. Foreign key to Intrastat table");

                entity.Property(e => e.IvaRedAmt)
                    .HasColumnType("money")
                    .HasComment("Reduced VAT amount");

                entity.Property(e => e.IvaRedBase)
                    .HasColumnType("money")
                    .HasComment("Reduced VAT base");

                entity.Property(e => e.IvaRedPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Reduced VAT rate");

                entity.Property(e => e.IvaStdAmt)
                    .HasColumnType("money")
                    .HasComment("Standard VAT amount");

                entity.Property(e => e.IvaStdBase)
                    .HasColumnType("money")
                    .HasComment("Standard VAT base");

                entity.Property(e => e.IvaStdPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Standard VAT percentage");

                entity.Property(e => e.IvaSuperRedAmt)
                    .HasColumnType("money")
                    .HasComment("Super reduced VAT amount");

                entity.Property(e => e.IvaSuperRedBase)
                    .HasColumnType("money")
                    .HasComment("Super reduced VAT base");

                entity.Property(e => e.IvaSuperRedPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Super reduced VAT rate");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.Nif)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Customer primary VAT id");

                entity.Property(e => e.Nif2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Secondary VAT id. Used for Andorra and UK post-Brexit customers");

                entity.Property(e => e.Nif2Cod).HasComment("id document for secondary VAT id");

                entity.Property(e => e.NifCod).HasComment("Primary VAT Id document. Enumerable DTONif.Cods");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer name");

                entity.Property(e => e.Ob1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Line 1 comments. Usually payment terms details");

                entity.Property(e => e.Ob2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Line 2 comments");

                entity.Property(e => e.Ob3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Line 3 comments");

                entity.Property(e => e.PrintMode).HasComment("Enumerable DTOInvoice.PrintModes (0.pending, 1.No print, 2.Printer, 3.Email, 4.Edi)");

                entity.Property(e => e.PtsLiq)
                    .HasColumnType("money")
                    .HasComment("Total in foreign currency");

                entity.Property(e => e.PuntsBase)
                    .HasColumnType("money")
                    .HasColumnName("Punts_Base")
                    .HasComment("Base amount to calculate promotional points");

                entity.Property(e => e.PuntsQty)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("Punts_Qty")
                    .HasComment("Number of promotional points earned");

                entity.Property(e => e.PuntsTipus)
                    .HasColumnType("numeric(4, 2)")
                    .HasColumnName("Punts_Tipus")
                    .HasComment("Promotional points rate");

                entity.Property(e => e.RegimenEspecialOtrascendencia)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("RegimenEspecialOTrascendencia")
                    .IsFixedLength()
                    .HasComment("Tax code required by tax authorities");

                entity.Property(e => e.ReqRedAmt)
                    .HasColumnType("money")
                    .HasComment("Reduced Sales Equalization Tax amount");

                entity.Property(e => e.ReqRedPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Reduced Sales Equalization Tax rate");

                entity.Property(e => e.ReqStdAmt)
                    .HasColumnType("money")
                    .HasComment("Standard Sales Equalization Tax amount");

                entity.Property(e => e.ReqStdPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Standard Sales Equalization Tax  base amount");

                entity.Property(e => e.ReqSuperRedAmt)
                    .HasColumnType("money")
                    .HasComment("Super reduced Sales Equalization Tax amount");

                entity.Property(e => e.ReqSuperRedPct)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Super reduced Sales Equalization Tax rate");

                entity.Property(e => e.Serie).HasComment("Set of invoices that share a unique numeration each year and Company. Enumerable DTOInvoice.Series (standard, rectificativa, simplificada...)");

                entity.Property(e => e.SiiCsv)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasComment("Validation code returned by Spanish Tax autorities when submiting the invoice notification");

                entity.Property(e => e.SiiErr)
                    .HasColumnType("text")
                    .HasComment("Error, if any, thrown when submitting the invoice to tax authorities");

                entity.Property(e => e.SiiFch)
                    .HasColumnType("datetime")
                    .HasComment("Date the invoice was notified to Spanish tax authorities");

                entity.Property(e => e.SiiL9)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("VAT exemption reason tax authorities code  (E2, E5...)");

                entity.Property(e => e.SiiResult).HasComment("Enumerable DTOInvoice.SiiResults(1.Success, 2.Accepted with errors, 3.Rejected)");

                entity.Property(e => e.SumItems)
                    .HasColumnType("money")
                    .HasComment("Sum of all item amounts before taxes");

                entity.Property(e => e.TipoFactura)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('F1')")
                    .IsFixedLength()
                    .HasComment("Invoice type tax authorities  (F1, F2...)");

                entity.Property(e => e.UsrLastPrintedGuid).HasComment("User who printed this invoice for last time");

                entity.Property(e => e.Vto)
                    .HasColumnType("datetime")
                    .HasComment("Due date");

                entity.Property(e => e.Yea).HasComment("Year of the invoice");

                entity.Property(e => e.Zip).HasComment("Location and postal code, as foreign key for Zip table");

                entity.HasOne(d => d.CcaGu)
                    .WithMany(p => p.Fras)
                    .HasForeignKey(d => d.CcaGuid)
                    .HasConstraintName("FK_FRA_CCA");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.Fras)
                    .HasForeignKey(d => d.CliGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fra_CliGral");

                entity.HasOne(d => d.EmailedToGu)
                    .WithMany(p => p.FraEmailedToGus)
                    .HasForeignKey(d => d.EmailedToGuid)
                    .HasConstraintName("FK_Fra_EmailedTo");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Fras)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fra_Emp");

                entity.HasOne(d => d.IncotermNavigation)
                    .WithMany(p => p.Fras)
                    .HasForeignKey(d => d.Incoterm)
                    .HasConstraintName("FK_Fra_Incoterm");

                entity.HasOne(d => d.IntrastatNavigation)
                    .WithMany(p => p.Fras)
                    .HasForeignKey(d => d.Intrastat)
                    .HasConstraintName("FK_Fra_Intrastat");

                entity.HasOne(d => d.UsrLastPrintedGu)
                    .WithMany(p => p.FraUsrLastPrintedGus)
                    .HasForeignKey(d => d.UsrLastPrintedGuid)
                    .HasConstraintName("FK_Fra_UsrLastPrinted");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.Fras)
                    .HasForeignKey(d => d.Zip)
                    .HasConstraintName("FK_Fra_Zip");
            });

            modelBuilder.Entity<FtpPath>(entity =>
            {
                entity.HasKey(e => new { e.Owner, e.Cod });

                entity.ToTable("FtpPath");

                entity.HasComment("Partner ftp server paths");

                entity.Property(e => e.Owner).HasComment("Ftp server owner, foreign key for CliGral table");

                entity.Property(e => e.Cod).HasComment("Path purpose; enumerable DTOFtpserver.path.cods");

                entity.Property(e => e.Path)
                    .HasColumnType("text")
                    .HasComment("Path from ftp server root");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.FtpPaths)
                    .HasForeignKey(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FtpPath_CliGral");
            });

            modelBuilder.Entity<FtpServer>(entity =>
            {
                entity.HasKey(e => e.Owner)
                    .HasName("PK_Ftp");

                entity.ToTable("FtpServer");

                entity.HasComment("Partner ftp server with whom we usually connect to interchange documents");

                entity.Property(e => e.Owner)
                    .ValueGeneratedNever()
                    .HasComment("Ftp server owner; foreign key for CliGral table ");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.PassiveMode).HasComment("True if connexion should be stablished under passive mode");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Password to log in the server");

                entity.Property(e => e.Port).HasComment("Ftp port");

                entity.Property(e => e.Servername)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Server name");

                entity.Property(e => e.Ssl)
                    .HasColumnName("SSL")
                    .HasComment("True if secure sockets layer connection");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("User name to log in the server");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithOne(p => p.FtpServer)
                    .HasForeignKey<FtpServer>(d => d.Owner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FtpServer_CliGral");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Gallery");

                entity.HasComment("High resolution brand resources, filed in the server by its MD5 hash signature as filename");

                entity.HasIndex(e => e.FchCreated, "IX_Gallery_FchCreated");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Entry creation date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("MD5 signature hash of the resource");

                entity.Property(e => e.Height).HasComment("Original height, in pixels");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasComment("Thumbnail");

                entity.Property(e => e.Kb).HasComment("Weu¡ight, in bytes");

                entity.Property(e => e.Mime).HasComment("Mime type, enumerable MatHelperStd.Enums.MimeCods");

                entity.Property(e => e.Nom)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Resource name");

                entity.Property(e => e.Thumbnail).HasColumnType("image");

                entity.Property(e => e.Width).HasComment("Original width, in pixels");
            });

            modelBuilder.Entity<Holding>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Holding");

                entity.HasComment("Customers holdings, used for statistics gathering different customers from a unique commercial group");

                entity.HasIndex(e => new { e.Emp, e.Nom }, "Idx_Holding_Nom")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Friendly name");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Holdings)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Holding_Emp");
            });

            modelBuilder.Entity<HourInOut>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.HourFrom });

                entity.ToTable("HourInOut");

                entity.HasComment("Defines daily time intervals between hours, for example to register employees workdays");

                entity.Property(e => e.Parent).HasComment("Parent table, may be different , one of them table StaffLog for employee workdays registry");

                entity.Property(e => e.HourFrom).HasComment("Starting hour");

                entity.Property(e => e.HourTo).HasComment("Ending hour");

                entity.Property(e => e.MinuteFrom).HasComment("Starting minute");

                entity.Property(e => e.MinuteTo).HasComment("Ending minute");
            });

            modelBuilder.Entity<Iban>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_IBAN_1");

                entity.ToTable("Iban");

                entity.HasComment("Contacts bank accounts");

                entity.HasIndex(e => e.ContactGuid, "IX_IBAN_ContactGuid");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.BankBranch).HasComment("Bank branch. Foreign key for Bn2 table");

                entity.Property(e => e.CaducaFch)
                    .HasColumnType("datetime")
                    .HasColumnName("Caduca_Fch")
                    .HasComment("Date the mandate is no longer valid");

                entity.Property(e => e.Ccc)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Account number (24 digits in case of Spanish Iban)");

                entity.Property(e => e.Cod).HasComment("Owner rol. Enumerable DTOIban.Cods (supplier, customer, staff, bank...)");

                entity.Property(e => e.ContactGuid).HasComment("Owner of the bank account; foreign key for CliGral table");

                entity.Property(e => e.FchApproved)
                    .HasColumnType("datetime")
                    .HasComment("Date the mandate was aproved by our company staff, verifying it was correctly filled and signed");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this record was created");

                entity.Property(e => e.FchDownloaded)
                    .HasColumnType("datetime")
                    .HasComment("Date the bank account owner downloaded the empty mandate form to be filled");

                entity.Property(e => e.FchUploaded)
                    .HasColumnType("datetime")
                    .HasComment("Date the bank account owner uploaded the completed mandate form");

                entity.Property(e => e.Format).HasComment("Enumerable DTOIban.Formats (SEPA B2B, SEPA Core...)");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Mandate Pdf document. Foreign key to Docfile table");

                entity.Property(e => e.MandatoFch)
                    .HasColumnType("datetime")
                    .HasColumnName("Mandato_Fch")
                    .HasComment("Issue date of mandate");

                entity.Property(e => e.PersonDni)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("VAT number of the person who signed the mandate");

                entity.Property(e => e.PersonNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("The name of the person from the bank account owner organisation who signed the mandate");

                entity.Property(e => e.Status).HasComment("Enumerable DTOIban.StatusEnum (uploaded, downloaded, pending, aproved...)");

                entity.Property(e => e.UsrApproved).HasComment("Corporate user who approved the mandate; foreign key to Email table");

                entity.Property(e => e.UsrDownloaded).HasComment("User who downloaded the empty mandate; foreign key to Email table");

                entity.Property(e => e.UsrUploaded).HasComment("User who uploaded the completed mandate form; foreign key for Email table");

                entity.HasOne(d => d.BankBranchNavigation)
                    .WithMany(p => p.Ibans)
                    .HasForeignKey(d => d.BankBranch)
                    .HasConstraintName("FK_IBAN_Bn2");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.Ibans)
                    .HasForeignKey(d => d.ContactGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IBAN_CliGral");

                entity.HasOne(d => d.UsrApprovedNavigation)
                    .WithMany(p => p.IbanUsrApprovedNavigations)
                    .HasForeignKey(d => d.UsrApproved)
                    .HasConstraintName("FK_Iban_UsrApproved");

                entity.HasOne(d => d.UsrDownloadedNavigation)
                    .WithMany(p => p.IbanUsrDownloadedNavigations)
                    .HasForeignKey(d => d.UsrDownloaded)
                    .HasConstraintName("FK_Iban_UsrDownloaded");

                entity.HasOne(d => d.UsrUploadedNavigation)
                    .WithMany(p => p.IbanUsrUploadedNavigations)
                    .HasForeignKey(d => d.UsrUploaded)
                    .HasConstraintName("FK_Iban_UsrUploaded");
            });

            modelBuilder.Entity<IbanStructure>(entity =>
            {
                entity.HasKey(e => e.CountryIso);

                entity.ToTable("IbanStructure");

                entity.HasComment("Structure of Iban digits for each EU country");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166-1 code for the country");

                entity.Property(e => e.AccountFormat).HasComment("Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric");

                entity.Property(e => e.AccountLength).HasComment("Number of dogits of the account number");

                entity.Property(e => e.AccountPosition).HasComment("Index of the first character of the account number, starting with zero");

                entity.Property(e => e.BankFormat).HasComment("Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric");

                entity.Property(e => e.BankLength).HasComment("Length of the digits representing the bank entity");

                entity.Property(e => e.BankPosition).HasComment("Index of the first character of the bank code, starting with zero");

                entity.Property(e => e.BranchFormat).HasComment("Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric");

                entity.Property(e => e.BranchLength).HasComment("Length of the digits representing the bank branch");

                entity.Property(e => e.BranchPosition).HasComment("Index of the first character of the bank branch code, starting with zero");

                entity.Property(e => e.CheckDigitsFormat).HasComment("Enumerable DTOIbanStructure.Formats 0.Numeric, 1.Alfanumeric");

                entity.Property(e => e.CheckDigitsLength).HasComment("number of check control digits");

                entity.Property(e => e.CheckDigitsPosition).HasComment("Index of the first check control digit, starting with zero");

                entity.Property(e => e.OverallLength).HasComment("Total number of digits ");
            });

            modelBuilder.Entity<Immoble>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Immoble");

                entity.HasComment("Real Estate properties (buildings, offices, appartments...)");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Physical address (street, number, flat...)");

                entity.Property(e => e.Cadastre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Land registry reference");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company Id, Fopreign key for Emp table");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("Date it was acquired");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("Date it was sold");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Nickname for this record");

                entity.Property(e => e.Part).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Superficie).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.ZipGuid).HasComment("Postal code, foreign key to Zip table");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Immobles)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Immoble_Emp");

                entity.HasOne(d => d.ZipGu)
                    .WithMany(p => p.Immobles)
                    .HasForeignKey(d => d.ZipGuid)
                    .HasConstraintName("FK_Immoble_Zip");
            });

            modelBuilder.Entity<Impagat>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_IMPAGATS");

                entity.HasComment("Unpayments");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.AsnefAlta)
                    .HasColumnType("date")
                    .HasComment("Date the unpayment has been notified to credit  insurance company");

                entity.Property(e => e.AsnefBaixa)
                    .HasColumnType("date")
                    .HasComment("Date the unpayment has been removed from credit insurance company");

                entity.Property(e => e.CcaIncobrable).HasComment("Accounts entry passing the debt to unchargable debts account");

                entity.Property(e => e.CsbGuid).HasComment("Foreign key to Bank remittance Csb table");

                entity.Property(e => e.FchAfp)
                    .HasColumnType("datetime")
                    .HasColumnName("FchAFP")
                    .HasComment("Date the debtor has been notified about the unpayment");

                entity.Property(e => e.FchSdo)
                    .HasColumnType("datetime")
                    .HasComment("Date the debt has been canceled");

                entity.Property(e => e.Gastos)
                    .HasColumnType("money")
                    .HasComment("Expenses");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments, free text");

                entity.Property(e => e.PagatAcompte)
                    .HasColumnType("money")
                    .HasColumnName("PagatACompte")
                    .HasComment("Partial payments total amount");

                entity.Property(e => e.RefBanc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Unpayment bank reference");

                entity.Property(e => e.Status).HasComment("Unpaid status DTOImpagat.StatusCodes");

                entity.HasOne(d => d.CcaIncobrableNavigation)
                    .WithMany(p => p.Impagats)
                    .HasForeignKey(d => d.CcaIncobrable)
                    .HasConstraintName("FK_Impagats_Cca");

                entity.HasOne(d => d.CsbGu)
                    .WithMany(p => p.Impagats)
                    .HasForeignKey(d => d.CsbGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Impagats_Csb");
            });

            modelBuilder.Entity<ImportDtl>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_IMPORTDTL");

                entity.ToTable("ImportDtl");

                entity.HasComment("Import consignment documents");

                entity.HasIndex(e => e.HeaderGuid, "IX_ImportDtl_Parent");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Descripcio)
                    .HasColumnType("text")
                    .HasComment("Document description");

                entity.Property(e => e.Div)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Value in foreign currency, if applicable");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Value in Euros, if applicable");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Pdf document. Foreign key for DocFile table");

                entity.Property(e => e.HeaderGuid).HasComment("Foreign key for parent table ImportHdr");

                entity.Property(e => e.SrcCod).HasComment("Type of document. Enumerable DTOImportacioItem.SourceCodes");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.ImportDtls)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_ImportDtl_DocFile");

                entity.HasOne(d => d.HeaderGu)
                    .WithMany(p => p.ImportDtls)
                    .HasForeignKey(d => d.HeaderGuid)
                    .HasConstraintName("FK_ImportDtl_ImportHdr");
            });

            modelBuilder.Entity<ImportHdr>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_IMPORTHDR");

                entity.ToTable("ImportHdr");

                entity.HasComment("Import consignments");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Id }, "Ix_ImportHdr_EmpYeaId")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Arrived).HasComment("True if the goods have already arrived to our warehouse");

                entity.Property(e => e.Bultos).HasComment("Number of packages");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 4217 currency code");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Eur)
                    .HasColumnType("money")
                    .HasComment("Goods value, in Euros");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date of delivery");

                entity.Property(e => e.FchAvisTrp)
                    .HasColumnType("datetime")
                    .HasComment("Date the transport company has been notified in order to schedule the truck or vessel");

                entity.Property(e => e.FchEtd)
                    .HasColumnType("datetime")
                    .HasColumnName("FchETD")
                    .HasComment("Estimated delivery time");

                entity.Property(e => e.Id).HasComment("Import Id, sequential number unique for each Company/Year combination");

                entity.Property(e => e.IncoTerms)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("International Commerce Terms; foreign key for Incoterm table");

                entity.Property(e => e.Kg)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Load weigh, in Kg");

                entity.Property(e => e.M3)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Load volume, in m3");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasComment("Truck plate or vessel number");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.PaisOrigen)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ES')")
                    .IsFixedLength()
                    .HasComment("ISO 3166-1 country code");

                entity.Property(e => e.PlataformaDeCarga).HasComment("Platform where the goods have to be picked up, if different from the supplier main address; foreign key for CliGral table");

                entity.Property(e => e.PrvGuid).HasComment("Supplier shipping the imported goods");

                entity.Property(e => e.TrpGuid).HasComment("International transport");

                entity.Property(e => e.Val)
                    .HasColumnType("money")
                    .HasComment("Goods value, in foreign currency");

                entity.Property(e => e.Week).HasComment("Week number within the year, starting the first week with at least 4 days inside the new year");

                entity.Property(e => e.Yea).HasComment("Import year");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.ImportHdrs)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportHdr_Emp");

                entity.HasOne(d => d.PlataformaDeCargaNavigation)
                    .WithMany(p => p.ImportHdrPlataformaDeCargaNavigations)
                    .HasForeignKey(d => d.PlataformaDeCarga)
                    .HasConstraintName("FK_ImportHdr_Platf");

                entity.HasOne(d => d.PrvGu)
                    .WithMany(p => p.ImportHdrPrvGus)
                    .HasForeignKey(d => d.PrvGuid)
                    .HasConstraintName("FK_ImportHdr_Prv");

                entity.HasOne(d => d.TrpGu)
                    .WithMany(p => p.ImportHdrTrpGus)
                    .HasForeignKey(d => d.TrpGuid)
                    .HasConstraintName("FK_ImportHdr_Trp");
            });

            modelBuilder.Entity<ImportPrevisio>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ImportPrevisio");

                entity.HasComment("Products and quantities expected to arrive from each import consignment");

                entity.HasIndex(e => new { e.Importacio, e.Lin }, "Ix_ImportPrevisio")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Importacio).HasComment("Import consignment; foreign key to parent table ImportHdr");

                entity.Property(e => e.InvoiceReceivedItem).HasComment("Foreign key for InvoiceReceivedItem");

                entity.Property(e => e.Lin)
                    .HasColumnName("lin")
                    .HasComment("Line number");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Product Sku name");

                entity.Property(e => e.NumComandaProveidor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Our order number");

                entity.Property(e => e.PurchaseOrderItem).HasComment("Foreign key to Pnc table");

                entity.Property(e => e.Qty).HasComment("Units expected to arrive");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Our M+O product reference, unique per company. Stored in Art table Art field");

                entity.Property(e => e.Sku).HasComment("Product Sku. Foreign key for Art table");

                entity.HasOne(d => d.ImportacioNavigation)
                    .WithMany(p => p.ImportPrevisios)
                    .HasForeignKey(d => d.Importacio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportPrevisio_ImportHdr");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.ImportPrevisios)
                    .HasForeignKey(d => d.Sku)
                    .HasConstraintName("FK_ImportPrevisio_ART");
            });

            modelBuilder.Entity<Incentiu>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Incentiu");

                entity.HasComment("Commercial promotions");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.CliVisible)
                    .HasDefaultValueSql("((1))")
                    .HasComment("True if visible to customers");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOIncentiu.Cods (1.discount, 2.free units)");

                entity.Property(e => e.Evento).HasComment("In case the promotion is link to a registered event");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasComment("Start date");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Termination date");

                entity.Property(e => e.ManufacturerContribution)
                    .HasColumnType("text")
                    .HasComment("Free text to explain manufacturer contribution");

                entity.Property(e => e.OnlyInStk).HasComment("If true, the promotion is only b¡valid for product in stock");

                entity.Property(e => e.Product).HasComment("Product range, foreign key to either brand Tpa table, product category Stp table or product Sku Art table");

                entity.Property(e => e.RepVisible)
                    .HasDefaultValueSql("((1))")
                    .HasComment("True if visible to sales agents");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("image")
                    .HasComment("Thumbnail image to ilustrate it on a web page list");

                entity.HasOne(d => d.EventoNavigation)
                    .WithMany(p => p.Incentius)
                    .HasForeignKey(d => d.Evento)
                    .HasConstraintName("FK_Incentiu_Noticia");

                entity.HasMany(d => d.DistributionChannels)
                    .WithMany(p => p.Incentius)
                    .UsingEntity<Dictionary<string, object>>(
                        "IncentiuChannel",
                        l => l.HasOne<DistributionChannel>().WithMany().HasForeignKey("DistributionChannel").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Incentiu_Channels_DistributionChannel"),
                        r => r.HasOne<Incentiu>().WithMany().HasForeignKey("Incentiu").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Incentiu_Channels_Incentiu"),
                        j =>
                        {
                            j.HasKey("Incentiu", "DistributionChannel");

                            j.ToTable("Incentiu_Channels").HasComment("Channels where a promotion is valid");

                            j.IndexerProperty<Guid>("Incentiu").HasComment("Foreign key to parent Incentiu table");

                            j.IndexerProperty<Guid>("DistributionChannel").HasComment("Foreign key to ChannelDistribution table");
                        });
            });

            modelBuilder.Entity<IncentiuProduct>(entity =>
            {
                entity.HasKey(e => new { e.Incentiu, e.Product })
                    .HasName("PK_Inc_Product");

                entity.ToTable("Incentiu_Product");

                entity.HasComment("Product range where a promotion is valid");

                entity.Property(e => e.Incentiu).HasComment("Foreign key for parent table Incentiu");

                entity.Property(e => e.Product).HasComment("Product range; foreign key for either product brand table Tpa, product category table Stp or product sku table Art");

                entity.HasOne(d => d.IncentiuNavigation)
                    .WithMany(p => p.IncentiuProducts)
                    .HasForeignKey(d => d.Incentiu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incentiu_Product_Incentiu");
            });

            modelBuilder.Entity<IncentiuQtyDto>(entity =>
            {
                entity.HasKey(e => new { e.Incentiu, e.Qty, e.Dto, e.FreeUnits });

                entity.ToTable("Incentiu_QtyDto");

                entity.HasComment("Minimum units to reach the discount");

                entity.Property(e => e.Incentiu).HasComment("Foreign key for parent table Incentiu");

                entity.Property(e => e.Qty).HasComment("Units");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Discount, in case Incentiu.Cod sets this type of incentive");

                entity.Property(e => e.FreeUnits).HasComment("Free units to deliver, in case Incentiu.Cod sets this type of incentive");

                entity.HasOne(d => d.IncentiuNavigation)
                    .WithMany(p => p.IncentiuQtyDtos)
                    .HasForeignKey(d => d.Incentiu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incentiu_QtyDto_Incentiu");
            });

            modelBuilder.Entity<IncidenciaDocFile>(entity =>
            {
                entity.HasKey(e => new { e.Incidencia, e.Hash });

                entity.ToTable("Incidencia_DocFiles");

                entity.HasComment("Postsales incidence support documents");

                entity.Property(e => e.Incidencia).HasComment("Foreign key to parent table Incidencies");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Foreign key to documents table Docfile");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOIncidencia.AttachmentCods (0.ticket, 1.image, 2.video)");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.IncidenciaDocFiles)
                    .HasForeignKey(d => d.Hash)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Incidencia_DocFiles_Incidencia_DocFiles");
            });

            modelBuilder.Entity<IncidenciesCod>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_IncidenciesCods_1");

                entity.HasComment("Postsales incidence codes");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name in Catalan language");

                entity.Property(e => e.Cod).HasComment("Enumerable (0.averia 1.tancament)");

                entity.Property(e => e.Eng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Name in English language");

                entity.Property(e => e.Esp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Name in Spanish language");

                entity.Property(e => e.Por)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name in Portuguese language");

                entity.Property(e => e.ReposicionParcial).HasComment("True if it implies reposition of spares part of the product");

                entity.Property(e => e.ReposicionTotal).HasComment("True if it implies the full reposition of a new product");
            });

            modelBuilder.Entity<Incidency>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_Incidencies_1");

                entity.HasComment("Postsales incidence reports");

                entity.HasIndex(e => new { e.ContactGuid, e.Id }, "IX_Incidencies_ContactGuid");

                entity.HasIndex(e => e.Id, "IX_Incidencies_Id");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOIncidencia.Srcs (1.Product, 2.Transport)");

                entity.Property(e => e.CodiApertura).HasComment("Cause code; foreign key for IncidenciesCods table");

                entity.Property(e => e.CodiTancament).HasComment("Closure code; foreign key for IncidenciesCods table");

                entity.Property(e => e.ContactGuid).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.ContactType).HasComment("Enumerable DTOIncidencia.ContactTypes (1.Professional, 2.Consumer)");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Email of the person who notified the incidence");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date of acknowledgement");

                entity.Property(e => e.FchClose)
                    .HasColumnType("datetime")
                    .HasComment("Date of closure");

                entity.Property(e => e.FchCompra)
                    .HasColumnType("date")
                    .HasComment("Acquisition date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Id).HasComment("Sequential number per company");

                entity.Property(e => e.ManufactureDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Manufacture date of the product, if any");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Person)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the person who notified the incidence");

                entity.Property(e => e.Procedencia).HasComment("Enumerable DTOIncidencia.Procedencias (1.Purchased from my shop, 2.Purchased from other shops, 3.Not sold yet, it comes from my exposition or my warehouse)");

                entity.Property(e => e.ProductGuid).HasComment("Product object of the claim; foreign key to either the brand Tpa table, the category Stp table or the product Sku table");

                entity.Property(e => e.SRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sRef")
                    .HasComment("Customer reference, if any");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Serial number of the product, if applicable");

                entity.Property(e => e.SpvGuid).HasComment("Foreign key to Spv table, if the product is sent to repair");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Phone number of the person who notified the incidence");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key to Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.HasOne(d => d.CodiAperturaNavigation)
                    .WithMany(p => p.IncidencyCodiAperturaNavigations)
                    .HasForeignKey(d => d.CodiApertura)
                    .HasConstraintName("FK_Incidencies_IncidenciesCodsAperturas");

                entity.HasOne(d => d.CodiTancamentNavigation)
                    .WithMany(p => p.IncidencyCodiTancamentNavigations)
                    .HasForeignKey(d => d.CodiTancament)
                    .HasConstraintName("FK_Incidencies_IncidenciesTancaments");

                entity.HasOne(d => d.SpvGu)
                    .WithMany(p => p.Incidencies)
                    .HasForeignKey(d => d.SpvGuid)
                    .HasConstraintName("FK_Incidencies_Spv");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.IncidencyUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_Incidencies_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.IncidencyUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_Incidencies_UsrLastEdited");
            });

            modelBuilder.Entity<Incoterm>(entity =>
            {
                entity.ToTable("Incoterm");

                entity.HasComment("pre-defined commercial terms published by the International Chamber of Commerce (ICC)");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Primary key, Incoterm acronym");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Description");
            });

            modelBuilder.Entity<Insolvencia>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_Insolvencias_1");

                entity.HasComment("Bad debts");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Customer).HasComment("Customer, foreign key for CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date ");

                entity.Property(e => e.FchRehabilitacio).HasColumnType("date");

                entity.Property(e => e.Nominal)
                    .HasColumnType("money")
                    .HasComment("Amount");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Insolvencia)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insolvencias_CliGral");
            });

            modelBuilder.Entity<Intrastat>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Intrastat");

                entity.HasComment("Customs monthly declaration of statistics on the exchange of goods between Member States of the European Union ");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Mes, e.Flujo, e.Ord }, "IX_Intrastat_EmpYeaMes")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Csv)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("Signature code received as response when uploading the Declaration online");

                entity.Property(e => e.Emp).HasComment("Company. Foreign key for Emp table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Flujo).HasComment("Enumerable DTOIntrastat.Flujos: 0.Introduccion (input), 1.Expedicion (output)");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Pdf document, foreign key for Docfile table");

                entity.Property(e => e.Mes).HasComment("Declaration month");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Consecutive number for different declarations from same Company, Year, Month and Flujo");

                entity.Property(e => e.Yea).HasComment("Declaration year");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Intrastats)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Intrastat_Emp");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.Intrastats)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Intrastat_DocFile");
            });

            modelBuilder.Entity<IntrastatPartidum>(entity =>
            {
                entity.HasKey(e => new { e.Intrastat, e.Lin });

                entity.HasComment("Intrastat declaration items");

                entity.Property(e => e.Intrastat).HasComment("Foreign key to parent table Intrastat");

                entity.Property(e => e.Lin).HasComment("item line number");

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Customs code. Foreign key for CodisMercancia table");

                entity.Property(e => e.CodiTransport).HasComment("Enumerable DTOIntrastat.Partida.CodisTransport. 1.maritimo, 2.ferrocarril, 3.carretera....");

                entity.Property(e => e.Country).HasComment("Destination country or origin, foreign key to Country table");

                entity.Property(e => e.ImporteFacturado)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Total invoiced value");

                entity.Property(e => e.Incoterm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("pre-defined commercial terms published by the International Chamber of Commerce (ICC). Foreign key for Incoterms table");

                entity.Property(e => e.Kg)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Total weight of product");

                entity.Property(e => e.MadeIn).HasComment("Country of manufacture. Foreign key to Country table");

                entity.Property(e => e.NaturalezaTransaccion).HasComment("Enumerable DTOIntrastat.Partida.NaturalezasTransaccion. 11.Compra en firme");

                entity.Property(e => e.Provincia).HasComment("Foreign key to destination province");

                entity.Property(e => e.RegimenEstadistico).HasComment("Enumerable DTOIntrastat.Partida.RegimenesEstadisticos 1.destinoFinalEuropa");

                entity.Property(e => e.Tag).HasComment("Document id, foreign key to either an invoice or a delivery note");

                entity.Property(e => e.UnidadesSuplementarias).HasComment("Total number of units of product");

                entity.Property(e => e.ValorEstadistico)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Total products value");

                entity.HasOne(d => d.CodiMercanciaNavigation)
                    .WithMany(p => p.IntrastatPartida)
                    .HasForeignKey(d => d.CodiMercancia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntrastatPartida_CodisMercancia");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.IntrastatPartidumCountryNavigations)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntrastatPartida_Country");

                entity.HasOne(d => d.IncotermNavigation)
                    .WithMany(p => p.IntrastatPartida)
                    .HasForeignKey(d => d.Incoterm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntrastatPartida_Incoterm");

                entity.HasOne(d => d.IntrastatNavigation)
                    .WithMany(p => p.IntrastatPartida)
                    .HasForeignKey(d => d.Intrastat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IntrastatPartida_Intrastat");

                entity.HasOne(d => d.MadeInNavigation)
                    .WithMany(p => p.IntrastatPartidumMadeInNavigations)
                    .HasForeignKey(d => d.MadeIn)
                    .HasConstraintName("FK_IntrastatPartida_MadeIn");

                entity.HasOne(d => d.ProvinciaNavigation)
                    .WithMany(p => p.IntrastatPartida)
                    .HasForeignKey(d => d.Provincia)
                    .HasConstraintName("FK_IntrastatPartida_Provincia");
            });

            modelBuilder.Entity<InventariItem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Inventory items");

                entity.HasIndex(e => new { e.Immoble, e.Nom }, "IX_InventariItems_ImmobleNom")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Immoble).HasComment("Property Id, foreign key to Immoble table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Inventory item friendly name");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.HasOne(d => d.ImmobleNavigation)
                    .WithMany(p => p.InventariItems)
                    .HasForeignKey(d => d.Immoble)
                    .HasConstraintName("FK_InventariItems_Immoble");
            });

            modelBuilder.Entity<InvoiceReceivedHeader>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("InvoiceReceivedHeader");

                entity.HasComment("Invoices received from suppliers");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("ISO 4217 currency code");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Invoice number");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("invoice date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Importacio).HasComment("Import consignment; foreign key for ImportHdr");

                entity.Property(e => e.NetTotal)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Invoice payable amount");

                entity.Property(e => e.Proveidor).HasComment("Supplier");

                entity.Property(e => e.ProveidorEan)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Supplier EAN 13 code");

                entity.Property(e => e.ShipmentId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Shipment number assigned by the supplier; it may be a truck plate, a vessel name or a document Id");

                entity.Property(e => e.TaxBase)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Invoice amount before taxes");

                entity.HasOne(d => d.ImportacioNavigation)
                    .WithMany(p => p.InvoiceReceivedHeaders)
                    .HasForeignKey(d => d.Importacio)
                    .HasConstraintName("FK_InvoiceReceivedHeader_ImportHdr");

                entity.HasOne(d => d.ProveidorNavigation)
                    .WithMany(p => p.InvoiceReceivedHeaders)
                    .HasForeignKey(d => d.Proveidor)
                    .HasConstraintName("FK_InvoiceReceivedHeader_CliGral");
            });

            modelBuilder.Entity<InvoiceReceivedItem>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_InvoiceReceivedItem_1");

                entity.ToTable("InvoiceReceivedItem");

                entity.HasComment("Invoices line items");

                entity.HasIndex(e => new { e.Parent, e.Lin }, "Ix_InvoiceReceivedItem_ParentLin");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Line amount");

                entity.Property(e => e.DeliveryNote)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Packing list number");

                entity.Property(e => e.Dto)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Price discount");

                entity.Property(e => e.Lin).HasComment("Item line number");

                entity.Property(e => e.OrderConfirmation)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Order confirmation number");

                entity.Property(e => e.Parent).HasComment("Foreign key to parent table InvoiceReceivedHeader");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Unit price");

                entity.Property(e => e.PurchaseOrder).HasComment("Foreign key to Pdc table");

                entity.Property(e => e.PurchaseOrderId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Our purchase order Id");

                entity.Property(e => e.PurchaseOrderItem).HasComment("Foreign key to Pnc table");

                entity.Property(e => e.Qty).HasComment("Units invoiced");

                entity.Property(e => e.QtyConfirmed).HasComment("Units received");

                entity.Property(e => e.Sku).HasComment("Product; foreign key for Art table");

                entity.Property(e => e.SkuEan)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("EAN 13 product code");

                entity.Property(e => e.SkuNom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Manufacturer product name");

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Manufacturer product code");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InvoiceReceivedItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceReceivedItem_InvoiceReceivedHeader");

                entity.HasOne(d => d.PurchaseOrderNavigation)
                    .WithMany(p => p.InvoiceReceivedItems)
                    .HasForeignKey(d => d.PurchaseOrder)
                    .HasConstraintName("FK_InvoiceReceivedItem_Pdc");
            });

            modelBuilder.Entity<JornadaLaboral>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("JornadaLaboral");

                entity.HasIndex(e => new { e.Staff, e.FchFrom }, "IX_JornadaLaboral_UsrFch")
                    .IsUnique();

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.FchFrom).HasColumnType("datetime");

                entity.Property(e => e.FchTo).HasColumnType("datetime");
            });

            modelBuilder.Entity<JsonLog>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("JsonLog");

                entity.HasComment("Log of Json messages received in our Api  https://matiasmasso-api.azurewebsites.net/api/JsonLog/mailbox");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the message was logged");

                entity.Property(e => e.Json)
                    .HasColumnType("text")
                    .HasComment("Message Json code");

                entity.Property(e => e.Result).HasComment("Enumerable DTOJsonLog.Results (1.Success, 2.Failure)");

                entity.Property(e => e.ResultTarget).HasComment("Primary key to a relevant table for the message object (for example, in case of a customer sending a purchase order, the value would be the foreign key for Pdc table)");

                entity.Property(e => e.Schema).HasComment("Schema the message conforms to");

                entity.HasOne(d => d.SchemaNavigation)
                    .WithMany(p => p.JsonLogs)
                    .HasForeignKey(d => d.Schema)
                    .HasConstraintName("FK_JsonLog_JsonSchema");
            });

            modelBuilder.Entity<JsonSchema>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("JsonSchema");

                entity.HasComment("Json Schemas for partners iontegration with our Api");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was last edited");

                entity.Property(e => e.Json)
                    .HasColumnType("text")
                    .HasComment("Json code");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Schema friendly name");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time; foreign key for Email table");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.JsonSchemaUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JsonSchema_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.JsonSchemaUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_JsonSchema_Email");
            });

            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.HasKey(e => new { e.Target, e.Value });

                entity.ToTable("Keyword");

                entity.HasComment("Keywords from news or blog posts");

                entity.Property(e => e.Target).HasComment("Blog post or News post");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("keyword text");
            });

            modelBuilder.Entity<LangText>(entity =>
            {
                entity.HasKey(e => e.Pkey)
                    .HasName("PK_LangResource");

                entity.ToTable("LangText");

                entity.HasComment("Text resources in different languages");

                entity.HasIndex(e => new { e.Guid, e.Src, e.Lang }, "IX_GuidSrcLang")
                    .IsUnique();

                entity.HasIndex(e => e.Src, "Ix_LangText_Src");

                entity.Property(e => e.Pkey)
                    .HasColumnName("PKey")
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Fch2).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Creation date");

                entity.Property(e => e.Guid).HasComment("Target; foreign key to different target tables");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 639-2 Language code");

                entity.Property(e => e.Src).HasComment("Enumerable DTOLangText.Srcs");

                entity.Property(e => e.Text).IsUnicode(false);
            });

            modelBuilder.Entity<LiniaTelefon>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_LINIATELEFON");

                entity.ToTable("LiniaTelefon");

                entity.HasComment("Company phone lines");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Friendly name");

                entity.Property(e => e.Alta)
                    .HasColumnType("date")
                    .HasComment("Start date");

                entity.Property(e => e.Baixa)
                    .HasColumnType("date")
                    .HasComment("Termination date");

                entity.Property(e => e.Contact).HasComment("Line user");

                entity.Property(e => e.Dual)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasComment("True if double phone number");

                entity.Property(e => e.Emp).HasComment("Owner company");

                entity.Property(e => e.Icc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ICC")
                    .HasComment("SIM card Id on mobile phones");

                entity.Property(e => e.Imei)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IMEI")
                    .HasComment("IMEI mobile phone serial number");

                entity.Property(e => e.Num)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Phone number");

                entity.Property(e => e.Pin)
                    .HasMaxLength(4)
                    .HasColumnName("PIN")
                    .IsFixedLength()
                    .HasComment("Pin to unblock the phone device");

                entity.Property(e => e.Privat).HasComment("True if private number");

                entity.Property(e => e.Puk)
                    .HasMaxLength(12)
                    .HasColumnName("PUK")
                    .HasComment("Puk to unblock the phone when the Pin is no longer enabled");

                entity.Property(e => e.Serial)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Serial number");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Location");

                entity.HasComment("Zona locations (towns, cities...)");

                entity.HasIndex(e => new { e.Zona, e.Nom }, "IX_Location_Zona_Nom")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.Property(e => e.Comarca).HasComment("Foreign key to Comarca table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Location name");

                entity.Property(e => e.Zona).HasComment("Foreign key to Zona table");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.Zona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Zona");
            });

            modelBuilder.Entity<MailingLog>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Usuari, e.Fch });

                entity.ToTable("MailingLog");

                entity.HasComment("Logs when automated email has been sent to a subscriber");

                entity.Property(e => e.Guid).HasComment("Primary key; foreign key to different tables depending on the subscription. It may be for example Csb table for notifying payments close to due date");

                entity.Property(e => e.Usuari).HasComment("Recipient destination; foreign key for Email table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date ant¡d time the email was sent");

                entity.HasOne(d => d.UsuariNavigation)
                    .WithMany(p => p.MailingLogs)
                    .HasForeignKey(d => d.Usuari)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailingLog_Email");
            });

            modelBuilder.Entity<MarketPlace>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_MarketPlace_1");

                entity.ToTable("MarketPlace");

                entity.HasComment("Platforms through which we directly sell to consumers (for example Amazon seller)");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Commission)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Commission rate the platform earns for each conversion");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Platform name");

                entity.Property(e => e.Web)
                    .HasColumnType("text")
                    .HasComment("Url for market place website");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.MarketPlace)
                    .HasForeignKey<MarketPlace>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MarketPlace_CliGral");
            });

            modelBuilder.Entity<MarketplaceSku>(entity =>
            {
                entity.HasKey(e => new { e.Marketplace, e.Sku });

                entity.ToTable("MarketplaceSku");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MediaResource>(entity =>
            {
                entity.HasKey(e => e.Hash)
                    .HasName("PK_MediaResource_1");

                entity.ToTable("MediaResource");

                entity.HasComment("Product resources like images or videos stored on the server filesystem by its hash signature");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOMediaResource.Cods (1.Product, 2.Features, 3.Life style)");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the image was uploaded");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Height).HasComment("Image height");

                entity.Property(e => e.Hres)
                    .HasColumnName("HRes")
                    .HasComment("Horizontal resolution");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language cod");

                entity.Property(e => e.Mime).HasComment("Enumerable MatHelperStd.Mimecods");

                entity.Property(e => e.Nom)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Descriptive name");

                entity.Property(e => e.Obsoleto).HasComment("True if outdated");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Pags).HasComment("Number of pages");

                entity.Property(e => e.Product).HasComment("Foreign key for brand Tpa table, product category Stp table or product sku Art table");

                entity.Property(e => e.Size).HasComment("Length in bytes");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("image")
                    .HasComment("Thumbnail image 140 x 140 pixels");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.Property(e => e.Vres)
                    .HasColumnName("VRes")
                    .HasComment("Vertical resolution");

                entity.Property(e => e.Width).HasComment("Image width");
            });

            modelBuilder.Entity<MediaResourceTarget>(entity =>
            {
                entity.HasKey(e => new { e.Target, e.Hash });

                entity.ToTable("MediaResourceTarget");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.MediaResourceTargets)
                    .HasForeignKey(d => d.Hash)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MediaResourceTarget_MediaResource");
            });

            modelBuilder.Entity<Mem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Mem");

                entity.HasComment("Contact comments and visit reports");

                entity.HasIndex(e => new { e.Contact, e.Fch }, "IX_Mem_Contact");

                entity.HasIndex(e => e.FchCreated, "IX_Mem_FchCreated");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOMem.Cods");

                entity.Property(e => e.Contact).HasComment("Foreign key for CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Entry creation date");

                entity.Property(e => e.Mem1)
                    .HasColumnType("text")
                    .HasColumnName("Mem")
                    .HasComment("Comments, visit reports, etc");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.Mems)
                    .HasForeignKey(d => d.Contact)
                    .HasConstraintName("FK_Mem_CliGral");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.Mems)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_Mem_Email");
            });

            modelBuilder.Entity<Mgz>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_MGZ");

                entity.ToTable("Mgz");

                entity.HasComment("Warehouses, logistic centers");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Friendly name");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.Mgz)
                    .HasForeignKey<Mgz>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mgz_CliGral");
            });

            modelBuilder.Entity<Mr2>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_MR2");

                entity.ToTable("Mr2");

                entity.HasComment("Amortisation quotes, accounting depreciations");

                entity.HasIndex(e => e.Cca, "IX_Mr2_Cca");

                entity.HasIndex(e => new { e.Parent, e.Fch }, "IX_Mr2_ParentFch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cca).HasComment("Accounting entry");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOAmortizationItem.Cods: 0.Amortització, 1.Baixa");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Amortisation amount, in Euro");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date of this entry");

                entity.Property(e => e.Parent).HasComment("Amortisation item, foreign key for Mrt table");

                entity.Property(e => e.Pts)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Amortisation amount, in foreign currency");

                entity.Property(e => e.Tipus)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Amortisation rate");

                entity.HasOne(d => d.CcaNavigation)
                    .WithMany(p => p.Mr2s)
                    .HasForeignKey(d => d.Cca)
                    .HasConstraintName("FK_MR2_CCA");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.Mr2s)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MR2_MRT");
            });

            modelBuilder.Entity<Mrt>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_MRT");

                entity.ToTable("Mrt");

                entity.HasComment("Amortisable items");

                entity.HasIndex(e => e.AltaCca, "IX_Mrt_Alta");

                entity.HasIndex(e => e.BaixaCca, "IX_Mrt_Baixa");

                entity.HasIndex(e => new { e.Emp, e.Fch, e.Cta }, "IX_Mrt_EmpCtaFch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.AltaCca).HasComment("Accounting entry of acquisition");

                entity.Property(e => e.BaixaCca).HasComment("Accounting entry of removal from inventory");

                entity.Property(e => e.Cta).HasComment("Accounting account");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Item description");

                entity.Property(e => e.Emp).HasComment("Company, foreign key for Emp table");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Acquisition price in Euro");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Acquisition date");

                entity.Property(e => e.Pts)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Acquisition price, in foreign currency");

                entity.Property(e => e.Tipus)
                    .HasColumnType("decimal(5, 2)")
                    .HasComment("Amortisation rate");

                entity.HasOne(d => d.AltaCcaNavigation)
                    .WithMany(p => p.MrtAltaCcaNavigations)
                    .HasForeignKey(d => d.AltaCca)
                    .HasConstraintName("FK_MRT_CCA");

                entity.HasOne(d => d.BaixaCcaNavigation)
                    .WithMany(p => p.MrtBaixaCcaNavigations)
                    .HasForeignKey(d => d.BaixaCca)
                    .HasConstraintName("FK_MRT_CCA1");

                entity.HasOne(d => d.CtaNavigation)
                    .WithMany(p => p.Mrts)
                    .HasForeignKey(d => d.Cta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MRT_PgcCta");
            });

            modelBuilder.Entity<MrtTipu>(entity =>
            {
                entity.HasKey(e => e.Cod)
                    .HasName("PK_MRTTIPOS");

                entity.HasComment("Amortisation rates");

                entity.Property(e => e.Cod)
                    .ValueGeneratedNever()
                    .HasComment("Account code. Enumerable DTOPgcPlan.Ctas");

                entity.Property(e => e.Pct).HasComment("Rate");
            });

            modelBuilder.Entity<Msg>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Msg");

                entity.HasComment("Messages sent by App users");

                entity.HasIndex(e => e.Id, "IX_Msg_Id");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Creation date");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Message sequential number");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasComment("Message body");

                entity.Property(e => e.UsrCreated).HasComment("User semding the message");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.Msgs)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Msg_Email");
            });

            modelBuilder.Entity<Multum>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Penalty fees");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Emisor).HasComment("Penalty issuer, foreign key for CliGral table");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(18, 2)")
                    .HasComment("Penalty fee");

                entity.Property(e => e.Expedient)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Proceeding number, issuer reference");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Infringement date");

                entity.Property(e => e.Pagat)
                    .HasColumnType("date")
                    .HasComment("Payment date");

                entity.Property(e => e.Subjecte).HasComment("Penalty object, usually a car from the company fleet");

                entity.Property(e => e.Vto)
                    .HasColumnType("date")
                    .HasComment("Payment deadline");

                entity.HasOne(d => d.EmisorNavigation)
                    .WithMany(p => p.Multa)
                    .HasForeignKey(d => d.Emisor)
                    .HasConstraintName("FK_Multa_CliGral");
            });

            modelBuilder.Entity<Nav>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Nav");

                entity.HasIndex(e => new { e.Parent, e.Ord }, "Idx_NavParentOrd");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IcoBig)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IcoSmall)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("FK_Nav_Nav");
            });

            modelBuilder.Entity<NavEmp>(entity =>
            {
                entity.HasKey(e => new { e.Nav, e.Emp });

                entity.ToTable("NavEmp");
            });

            modelBuilder.Entity<NavRol>(entity =>
            {
                entity.HasKey(e => new { e.Nav, e.Rol });

                entity.ToTable("NavRol");

                entity.HasIndex(e => new { e.Rol, e.Nav }, "Idx_NavRol_RolNav")
                    .IsUnique();

                entity.HasOne(d => d.NavNavigation)
                    .WithMany(p => p.NavRols)
                    .HasForeignKey(d => d.Nav)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NavRol_Nav");
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Newsletter");

                entity.HasComment("Newsletters sent to subscribed leads");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Newsletter date");

                entity.Property(e => e.Id).HasComment("Sequential Id, to refer to each newsletter by a number");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Newsletter subject");
            });

            modelBuilder.Entity<NewsletterSource>(entity =>
            {
                entity.HasKey(e => new { e.Newsletter, e.Ord })
                    .HasName("PK_NewsletterSource_1");

                entity.ToTable("NewsletterSource");

                entity.HasComment("Posts referred to on each newsletter");

                entity.Property(e => e.Newsletter).HasComment("Foreign key to parent table Newsletter ");

                entity.Property(e => e.Ord).HasComment("Sort order of this post within same newsletter");

                entity.Property(e => e.SourceCod).HasComment("Enumerable DTONewsletterSource.Cods (blog, news, event, promo...)");

                entity.Property(e => e.SourceGuid).HasComment("Content; foreign key to either Noticia, BlogPost or Content table");

                entity.HasOne(d => d.NewsletterNavigation)
                    .WithMany(p => p.NewsletterSources)
                    .HasForeignKey(d => d.Newsletter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsletterSource_Newsletter");
            });

            modelBuilder.Entity<Nomina>(entity =>
            {
                entity.HasKey(e => e.CcaGuid);

                entity.ToTable("Nomina");

                entity.HasComment("Employee salary sheets");

                entity.Property(e => e.CcaGuid)
                    .ValueGeneratedNever()
                    .HasComment("Accounts registry; foreign key to accounts entry Cca table");

                entity.Property(e => e.Anticips)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Advanced amounts");

                entity.Property(e => e.Deutes)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Debts");

                entity.Property(e => e.Devengat)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Accrued amount");

                entity.Property(e => e.Dietes)
                    .HasColumnType("decimal(9, 0)")
                    .HasComment("Subsistence allowances");

                entity.Property(e => e.Embargos)
                    .HasColumnType("decimal(9, 0)")
                    .HasComment("Wage garnishment");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Salary date");

                entity.Property(e => e.Iban)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasComment("Bank account to transfer the salary");

                entity.Property(e => e.Irpf)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Tax withholdings amount");

                entity.Property(e => e.IrpfBase)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Tax base for withholdings");

                entity.Property(e => e.Liquid)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Payable cash");

                entity.Property(e => e.SegSocial)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Social security charges");

                entity.Property(e => e.Staff).HasComment("Employee; foreign key to CliGral table");

                entity.HasOne(d => d.CcaGu)
                    .WithOne(p => p.Nomina)
                    .HasForeignKey<Nomina>(d => d.CcaGuid)
                    .HasConstraintName("FK_Nomina_CCA");

                entity.HasOne(d => d.StaffNavigation)
                    .WithMany(p => p.Nominas)
                    .HasForeignKey(d => d.Staff)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nomina_CliGral");
            });

            modelBuilder.Entity<NominaConcepte>(entity =>
            {
                entity.ToTable("NominaConcepte");

                entity.HasComment("Employee salary concepts");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Concept cdefined by externalized salary management company");

                entity.Property(e => e.Concepte)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Concept description");
            });

            modelBuilder.Entity<NominaItem>(entity =>
            {
                entity.HasKey(e => new { e.CcaGuid, e.Lin })
                    .HasName("PK_NOMINA_ITEM_1");

                entity.ToTable("NominaItem");

                entity.HasComment("Employee salary items");

                entity.Property(e => e.CcaGuid).HasComment("Accounts entry; foreign key for Cca table");

                entity.Property(e => e.Lin).HasComment("Item line, sequential number to sort the items");

                entity.Property(e => e.CodiConcepte).HasComment("Concept code; foreign key for NominaConcepte table");

                entity.Property(e => e.Preu)
                    .HasColumnType("decimal(8, 3)")
                    .HasComment("Unit price");

                entity.Property(e => e.Qty).HasComment("U");

                entity.HasOne(d => d.CcaGu)
                    .WithMany(p => p.NominaItems)
                    .HasForeignKey(d => d.CcaGuid)
                    .HasConstraintName("FK_NominaItem_Nomina");

                entity.HasOne(d => d.CodiConcepteNavigation)
                    .WithMany(p => p.NominaItems)
                    .HasForeignKey(d => d.CodiConcepte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NominaItem_NominaConcepte");
            });

            modelBuilder.Entity<Noticium>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_NEWS");

                entity.HasComment("News posts");

                entity.HasIndex(e => e.Fch, "IX_Noticia_Fch");

                entity.HasIndex(e => e.UrlFriendlySegment, "IX_Noticia_Url");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Brand).HasComment("If the news relate to a specific brand, brand category or product, foreign key to either table Tpa, Stp or Art respectively");

                entity.Property(e => e.Cnap).HasComment("Product classification; foreign key for Cnap table");

                entity.Property(e => e.Cod).HasComment("Enumerable DTONoticia.Srcs (0.news, 1.events...)");

                entity.Property(e => e.DestacarFrom)
                    .HasColumnType("date")
                    .HasComment("Date this post should be highlited from");

                entity.Property(e => e.DestacarTo)
                    .HasColumnType("date")
                    .HasComment("Date this post should no longer be highlited");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("News date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this entry was created");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("In case of events, start date");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this entry was last edited");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("In case of events, end date");

                entity.Property(e => e.Image265x150)
                    .HasColumnType("image")
                    .HasComment("Featured image");

                entity.Property(e => e.Location).HasComment("Greographical area, if restricted to any");

                entity.Property(e => e.Priority).HasComment("Enumerable DTONoticia.PriorityLevels (0.low, 1.high)");

                entity.Property(e => e.Professional).HasComment("True if visibility limited to professionals");

                entity.Property(e => e.UrlFriendlySegment)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("Friendly segment of the landing page url");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time; foreign key for Email table");

                entity.Property(e => e.Visible)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Hidden to web visitors if false");

                entity.HasOne(d => d.CnapNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.Cnap)
                    .HasConstraintName("FK_Noticia_Cnap");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Noticia_Emp");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.NoticiumUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_Noticia_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.NoticiumUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_Noticia_UsrLastEdited");

                entity.HasMany(d => d.Categoria)
                    .WithMany(p => p.Noticia)
                    .UsingEntity<Dictionary<string, object>>(
                        "NoticiaCategorium",
                        l => l.HasOne<CategoriaDeNoticium>().WithMany().HasForeignKey("Categoria").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NoticiaCategoria_CategoriaDeNoticia"),
                        r => r.HasOne<Noticium>().WithMany().HasForeignKey("Noticia").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NoticiaCategoria_Noticia"),
                        j =>
                        {
                            j.HasKey("Noticia", "Categoria");

                            j.ToTable("NoticiaCategoria").HasComment("News posts per category");

                            j.HasIndex(new[] { "Categoria", "Noticia" }, "IX_NoticiaCategoria_CategoriaNoticia").IsUnique();

                            j.IndexerProperty<Guid>("Noticia").HasComment("News post; foreign key for Noticias table");

                            j.IndexerProperty<Guid>("Categoria").HasComment("News category; foreign key for CategoriaDeNoticias table");
                        });

                entity.HasMany(d => d.Channels)
                    .WithMany(p => p.Noticia)
                    .UsingEntity<Dictionary<string, object>>(
                        "NoticiaChannel",
                        l => l.HasOne<DistributionChannel>().WithMany().HasForeignKey("Channel").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NoticiaChannel_DistributionChannel"),
                        r => r.HasOne<Noticium>().WithMany().HasForeignKey("Noticia").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_NoticiaChannel_Noticia"),
                        j =>
                        {
                            j.HasKey("Noticia", "Channel");

                            j.ToTable("NoticiaChannel").HasComment("News posts per distribution channel");

                            j.IndexerProperty<Guid>("Noticia").HasComment("News post; foreign key for Noticias");

                            j.IndexerProperty<Guid>("Channel").HasComment("Distribution channel; foreign key for DistributionChannel table");
                        });
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Sku });

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PaymentGateway>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_SermepaConfig");

                entity.ToTable("PaymentGateway");

                entity.HasComment("Bank online gateways to manage secure payments from customers credit cards");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the gateway is active from");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("Date the gateway was closed");

                entity.Property(e => e.MerchantCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Code assigned by the gateway service (currently Redsys) to this account");

                entity.Property(e => e.MerchantUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Url from our website the gateway service will send logs of operations");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Friendly name");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Admin password to manage our account on gateway service");

                entity.Property(e => e.SermepaUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Url to address customers for new payments");

                entity.Property(e => e.SignatureKey)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Key to sign messages to and from the gateway");

                entity.Property(e => e.UrlKo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UrlKO")
                    .HasComment("Url the gateway will address our customers after a failed payment");

                entity.Property(e => e.UrlOk)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Url the gateway will address our customers after a successful payment");

                entity.Property(e => e.UserAdmin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Admin user name to manage our account on gateway service");
            });

            modelBuilder.Entity<Pdc>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PDC");

                entity.ToTable("Pdc");

                entity.HasComment("Purchase Order header details");

                entity.HasIndex(e => new { e.CliGuid, e.Yea, e.Pdc1 }, "IX_PDC_CliGuidYeaPdc");

                entity.HasIndex(e => new { e.Cod, e.Emp, e.Yea }, "IX_Pdc_CodEmpYea");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Pdc1 }, "IX_Pdc_EmpYeaPdc");

                entity.HasIndex(e => e.Fch, "IX_Pdc_Fch");

                entity.HasIndex(e => e.FchCreated, "IX_Pdc_FchCreated");

                entity.HasIndex(e => e.Promo, "IX_Pdc_Promo");

                entity.HasIndex(e => new { e.Cod, e.CliGuid }, "NonClusteredIndex-20211212-194016");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.BlockStock).HasComment("If true, stock should be reserved to make sure we can deliver on the right date");

                entity.Property(e => e.CliGuid).HasComment("Customer (or supplier). Foreign key for CliGral table.");

                entity.Property(e => e.Cod).HasComment("Enuimeration DTOPurchaseOrder.Codis: 1.To Suplier, 2.From Customer...");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency (ISO 4217)");

                entity.Property(e => e.CustomerDocUrl)
                    .HasColumnType("text")
                    .HasColumnName("CustomerDocURL")
                    .HasDefaultValueSql("('')")
                    .HasComment("A link for the warehouse to download customer specific documentation to include on the package, usually an consumer invoice from the e-commerce");

                entity.Property(e => e.Dpp).HasComment("Discount for early payment");

                entity.Property(e => e.Dt2).HasComment("Second level of discount for the whole order");

                entity.Property(e => e.Dto).HasComment("Global discount for the whole order");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company Id. Foreign key for Emp table");

                entity.Property(e => e.EtiquetesTransport)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Hash of labels to attach to the packaging. Foreign key to DocFile table");

                entity.Property(e => e.Eur)
                    .HasColumnType("money")
                    .HasComment("Order amount in Euro currency");

                entity.Property(e => e.FacturarA).HasComment("Account where to invoice this delivery, if different from default. Foreign key to CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Official order date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this order was registered");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Last date this order was edited");

                entity.Property(e => e.FchMax)
                    .HasColumnType("date")
                    .HasComment("Don't deliver after this date, if any");

                entity.Property(e => e.FchMin)
                    .HasColumnType("date")
                    .HasComment("Don't deliver before this date, if any");

                entity.Property(e => e.Fpg)
                    .HasColumnType("text")
                    .HasComment("Payment terms if different from customer default. XML or JSON coded");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Hash of the original Purchase Order Pdf document. Foreign key for Docfile table");

                entity.Property(e => e.Hide).HasComment("if true, this order should not be displayed to customer/supplier");

                entity.Property(e => e.Incoterm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("International Commerce Terms");

                entity.Property(e => e.Nadms)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.NoRep).HasComment("If true, no commission should be granted to any agent for this order");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments, customer special instructions");

                entity.Property(e => e.Pdc1)
                    .HasColumnName("Pdc")
                    .HasComment("Internal order number within same company and year");

                entity.Property(e => e.PdcPts)
                    .HasColumnType("money")
                    .HasComment("Amount in foreign currency");

                entity.Property(e => e.Pdd)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Customer order number or any text the customer may need to identify this order");

                entity.Property(e => e.Platform).HasComment("Platform where to deliver this order. Foreign key to CliGral");

                entity.Property(e => e.Pot).HasComment("If true, customer does not want it to be delivered yet and goods may be sold to alternative customers");

                entity.Property(e => e.Promo).HasComment("Promotion applied, if any. Foreign key to Incentius table");

                entity.Property(e => e.Src).HasComment("Where did this order came from. Enumeration DTOPurchaseOrder.Sources");

                entity.Property(e => e.TotJunt).HasComment("If true, the order should not be fractionated in different deliveries");

                entity.Property(e => e.UsrCreatedGuid).HasComment("User who registered the order. Foreign key to Email table");

                entity.Property(e => e.UsrLastEditedGuid).HasComment("Last user who edited this order. Foreign key to Email table.");

                entity.Property(e => e.Yea).HasComment("Year where the order number belongs to (each year order numbers start from zero)");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.PdcCliGus)
                    .HasForeignKey(d => d.CliGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pdc_Customer");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Pdcs)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pdc_Emp");

                entity.HasOne(d => d.EtiquetesTransportNavigation)
                    .WithMany(p => p.PdcEtiquetesTransportNavigations)
                    .HasForeignKey(d => d.EtiquetesTransport)
                    .HasConstraintName("FK_Pdc_TransportLabels");

                entity.HasOne(d => d.FacturarANavigation)
                    .WithMany(p => p.PdcFacturarANavigations)
                    .HasForeignKey(d => d.FacturarA)
                    .HasConstraintName("FK_Pdc_FacturarA");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.PdcHashNavigations)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_Pdc_DocFile");

                entity.HasOne(d => d.PlatformNavigation)
                    .WithMany(p => p.PdcPlatformNavigations)
                    .HasForeignKey(d => d.Platform)
                    .HasConstraintName("FK_Pdc_Platform");

                entity.HasOne(d => d.PromoNavigation)
                    .WithMany(p => p.Pdcs)
                    .HasForeignKey(d => d.Promo)
                    .HasConstraintName("FK_Pdc_Incentiu");

                entity.HasOne(d => d.UsrCreatedGu)
                    .WithMany(p => p.PdcUsrCreatedGus)
                    .HasForeignKey(d => d.UsrCreatedGuid)
                    .HasConstraintName("FK_Pdc_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedGu)
                    .WithMany(p => p.PdcUsrLastEditedGus)
                    .HasForeignKey(d => d.UsrLastEditedGuid)
                    .HasConstraintName("FK_Pdc_UsrLastEdited");
            });

            modelBuilder.Entity<Pdd>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PDD");

                entity.ToTable("Pdd");

                entity.HasComment("Shortcuts for purchase order concepts");

                entity.HasIndex(e => e.SearchKey, "Pdd_Idx_Searchkey")
                    .IsUnique();

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Concept in Catalan language");

                entity.Property(e => e.Eng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Concept in English language");

                entity.Property(e => e.Esp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Concept in Spanish language");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Shortcut. A short text the user enters which the system converts into an often used text");

                entity.Property(e => e.SearchKey)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Src).HasComment("Purchase order source. Enumerable DTOPurchaseOrder.Sources");
            });

            modelBuilder.Entity<PgcClass>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("PgcClass");

                entity.HasComment("Recursive account groups");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("DTOEnumerable DTOPgcClass.Cods, to programatically refer to it");

                entity.Property(e => e.HideFigures).HasComment("True if a report should not display amounts for this item");

                entity.Property(e => e.Level).HasComment("Depth level");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Name, catalan language");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Name, English language");

                entity.Property(e => e.NomEsp)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Name, Spanish language");

                entity.Property(e => e.Ord).HasComment("Sort order within same parent");

                entity.Property(e => e.Parent).HasComment("Parent accounts group, foreign key for self table PgcClass");

                entity.Property(e => e.Plan).HasComment("Accounts plan; foreign key for PgcPlan table");

                entity.Property(e => e.Sumandos)
                    .HasColumnType("text")
                    .HasComment("Comma separated Cod field values which this item value is the sum result, if any");

                entity.HasOne(d => d.PlanNavigation)
                    .WithMany(p => p.PgcClasses)
                    .HasForeignKey(d => d.Plan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PgcClass_PgcPlan");
            });

            modelBuilder.Entity<PgcCtum>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PGCCTA_1");

                entity.HasComment("Accounts accounts");

                entity.HasIndex(e => e.PgcClass, "IX_PgcCta_PgcClass");

                entity.HasIndex(e => new { e.Plan, e.Cod }, "IX_PgcCta_PlanGuidCod");

                entity.HasIndex(e => new { e.Plan, e.Id }, "IX_PgcCta_PlanGuidCta");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Act).HasComment("Enumerable DTOPgcCta.Acts (1.receivable account, 2.payable account)");

                entity.Property(e => e.Cat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Account name, Catalan language");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOPgcPlan.Ctas, used to programmatically refer to an account regardles of its Id");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Account description, free text");

                entity.Property(e => e.Eng)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Account name, English language");

                entity.Property(e => e.Esp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Account name, Spanish language");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Account Id, 5 digits");

                entity.Property(e => e.IsBaseImponibleIva)
                    .HasColumnName("IsBaseImponibleIVA")
                    .HasComment("True if it is the base to calculate VAT charges");

                entity.Property(e => e.IsQuotaIrpf)
                    .HasColumnName("isQuotaIrpf")
                    .HasComment("True it it is a Irpf charge (tax withholdings)");

                entity.Property(e => e.IsQuotaIva).HasComment("True if it is a VAT charge");

                entity.Property(e => e.NextCtaGuid).HasComment("Equivalent account on next plan");

                entity.Property(e => e.PgcClass).HasComment("Accounts group it belongs to; foreign key for PgcClass table");

                entity.Property(e => e.Plan).HasComment("Accounts plan; foreign key for PgcPlan table");

                entity.HasOne(d => d.NextCtaGu)
                    .WithMany(p => p.InverseNextCtaGu)
                    .HasForeignKey(d => d.NextCtaGuid)
                    .HasConstraintName("FK_PgcCta_NextCta");

                entity.HasOne(d => d.PgcClassNavigation)
                    .WithMany(p => p.PgcCta)
                    .HasForeignKey(d => d.PgcClass)
                    .HasConstraintName("FK_PGCCTA_PgcClass");

                entity.HasOne(d => d.PlanNavigation)
                    .WithMany(p => p.PgcCta)
                    .HasForeignKey(d => d.Plan)
                    .HasConstraintName("FK_PGCCTA_PgcPlan");
            });

            modelBuilder.Entity<PgcPlan>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("PgcPlan");

                entity.HasComment("Accounts plan");

                entity.HasIndex(e => e.YearFrom, "IX_PgcPlan_YearFrom");

                entity.HasIndex(e => e.YearTo, "IX_PgvPlan_YearTo");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.LastPlan).HasComment("Previous plan; foreign key for self table PgcPlan");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.YearFrom).HasComment("Year of activation");

                entity.Property(e => e.YearTo)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Last year of validity");

                entity.HasOne(d => d.LastPlanNavigation)
                    .WithMany(p => p.InverseLastPlanNavigation)
                    .HasForeignKey(d => d.LastPlan)
                    .HasConstraintName("FK_PgcPlan_PgcPlan");
            });

            modelBuilder.Entity<Plantilla>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CustomTemplate");

                entity.ToTable("Plantilla");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Pnc>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PNC");

                entity.ToTable("Pnc");

                entity.HasComment("Purchase order items");

                entity.HasIndex(e => e.ArtGuid, "IX_Pnc_Art");

                entity.HasIndex(e => new { e.PdcGuid, e.Lin }, "IX_Pnc_PdcGuid_Lin");

                entity.HasIndex(e => e.RepGuid, "Ix_Pnc_RepGuid");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.ArtGuid).HasComment("Product Sku. Foreign key to table Art");

                entity.Property(e => e.Bundle).HasComment("A bundle is a virtual product sku composed by an agregation of different product skus that are sold all together.\r\nIn order to identify it on an order, this field takes the same value of the primary key of the bundle purchase order item, which means it should be displayed but it does not affect inventory.\r\nThe following items list the components of the bundle and keep the parent value on the bundle field.\r\nThis means they should be display as components included in the bundle and they affect the inventory");

                entity.Property(e => e.Carrec).HasComment("if false, free of charge");

                entity.Property(e => e.Com)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Commission percentage for the commercial agent");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency as ISO 4217");

                entity.Property(e => e.Custom)
                    .HasColumnType("text")
                    .HasComment("Free text to explain why we set a custom rep rather than d¡the default one for this order");

                entity.Property(e => e.CustomLin).HasComment("Line number matching original customer purchase order");

                entity.Property(e => e.Dt2).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Dto)
                    .HasColumnType("numeric(5, 2)")
                    .HasComment("Discount on price, if any");

                entity.Property(e => e.ErrCod).HasComment("Enumerated in DTOPurchaseOrder.Errcodes");

                entity.Property(e => e.ErrDsc)
                    .HasColumnType("text")
                    .HasComment("Error description");

                entity.Property(e => e.Eur)
                    .HasColumnType("money")
                    .HasComment("Price in Eur");

                entity.Property(e => e.FchConfirm)
                    .HasColumnType("datetime")
                    .HasComment("Estimated Time of Delivery (ETD)");

                entity.Property(e => e.Lin).HasComment("Internal line number");

                entity.Property(e => e.PdcGuid).HasComment("Foreign key to parent table Pdc");

                entity.Property(e => e.Pn2).HasComment("Ordered units not delivered yet");

                entity.Property(e => e.Pts)
                    .HasColumnType("numeric(12, 2)")
                    .HasComment("Price in foreign currency");

                entity.Property(e => e.Qty).HasComment("Units ordered");

                entity.Property(e => e.RepCustom)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Sometimes there is the need to assign a commission to a rep different than the default one. Setting this value to true prevents the system from overriding the rep when validating the order");

                entity.Property(e => e.RepGuid).HasComment("Commercial agent when commission applies. Foreign key for CliRep table");

                entity.HasOne(d => d.ArtGu)
                    .WithMany(p => p.Pncs)
                    .HasForeignKey(d => d.ArtGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PNC_ART");

                entity.HasOne(d => d.BundleNavigation)
                    .WithMany(p => p.InverseBundleNavigation)
                    .HasForeignKey(d => d.Bundle)
                    .HasConstraintName("FK_Pnc_Bundle");

                entity.HasOne(d => d.PdcGu)
                    .WithMany(p => p.Pncs)
                    .HasForeignKey(d => d.PdcGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pnc_Pdc");
            });

            modelBuilder.Entity<Pnd>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Pnd");

                entity.HasComment("Pending debts and credits");

                entity.HasIndex(e => new { e.ContactGuid, e.Status, e.Vto, e.Fra }, "IX_PND_ContactGuid");

                entity.HasIndex(e => e.Guid, "IX_PND_Guid")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Ad)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('D')")
                    .IsFixedLength()
                    .HasComment("Enumerable DTOPnd.Codis (1.debit, 2.credit)");

                entity.Property(e => e.CcaGuid).HasComment("Accounts entry; key of Cca table");

                entity.Property(e => e.Cfp).HasComment("Payment way, enumerable DTOPnd.FormasDePagament");

                entity.Property(e => e.ContactGuid).HasComment("Debtor or creditor; foreign key for CliGral table");

                entity.Property(e => e.CsbGuid).HasComment("Remittance; key of  table");

                entity.Property(e => e.CtaGuid).HasComment("Accounts account, key of PgcCta table");

                entity.Property(e => e.Div)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("ISO 4217 Currency code");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company; key of Emp table");

                entity.Property(e => e.Eur)
                    .HasColumnType("money")
                    .HasComment("Amount, in Euros");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this entry was created");

                entity.Property(e => e.Fpg)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')")
                    .HasComment("Payment way, free text");

                entity.Property(e => e.Fra)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Invoice number, if any");

                entity.Property(e => e.FraGuid).HasComment("Invoice; key of Fra table");

                entity.Property(e => e.Pts)
                    .HasColumnType("money")
                    .HasComment("Amount, in foreign currency");

                entity.Property(e => e.Status).HasComment("Payment status, enumerable DTOPnd.StatusCod (0.Pending...)");

                entity.Property(e => e.StatusGuid).HasComment("Foreign key for the table relevant to this debt");

                entity.Property(e => e.Vto)
                    .HasColumnType("datetime")
                    .HasComment("Due date");

                entity.Property(e => e.Yef).HasComment("Year of the invoice, if any");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.Pnds)
                    .HasForeignKey(d => d.ContactGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PND_CliGral");
            });

            modelBuilder.Entity<PortadaImg>(entity =>
            {
                entity.ToTable("PortadaImg");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PortsCondicion>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Transport to customer conditions");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOPortsCondicio.Cods (0.paid transport, 1.chargeable....)");

                entity.Property(e => e.Fee)
                    .HasColumnType("numeric(4, 2)")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Forfait transport fee if order value does not reach the minimum");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((9999))")
                    .HasComment("Sort order");

                entity.Property(e => e.PdcMinVal)
                    .HasColumnType("numeric(8, 2)")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Minimum order value for free of charge transport");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("PostComment");

                entity.HasComment("Comments to website and blog posts");

                entity.HasIndex(e => new { e.Parent, e.Fch }, "IX_PostComment_ParentFch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.AnswerRoot).HasComment("The root comment who started the thred");

                entity.Property(e => e.Answering).HasComment("Previous comment in the thread to which this comment is answering; foreign key to self table PostComment");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Comment date and time");

                entity.Property(e => e.FchApproved)
                    .HasColumnType("datetime")
                    .HasComment("Date and time this comment was approved by the moderator");

                entity.Property(e => e.FchDeleted)
                    .HasColumnType("datetime")
                    .HasComment("Date and time the moderator removed this commenty from the thread");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.Parent).HasComment("Post where this comment was posted");

                entity.Property(e => e.ParentSource)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Enumerable DTOPostComment.ParentSources (news, blog...)");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasComment("Comment text");

                entity.Property(e => e.User).HasComment("User who wrote the comment");

                entity.HasOne(d => d.AnswerRootNavigation)
                    .WithMany(p => p.InverseAnswerRootNavigation)
                    .HasForeignKey(d => d.AnswerRoot)
                    .HasConstraintName("FK_PostComment_AnswerRoot");

                entity.HasOne(d => d.AnsweringNavigation)
                    .WithMany(p => p.InverseAnsweringNavigation)
                    .HasForeignKey(d => d.Answering)
                    .HasConstraintName("FK_PostComment_Answeering");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComment_Email");
            });

            modelBuilder.Entity<PremiumCustomer>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PremiumCustomer_1");

                entity.ToTable("PremiumCustomer");

                entity.HasComment("Customers included/excluded from Premium Line range distribution");

                entity.HasIndex(e => e.Customer, "IX_PremiumCustomer_Customer");

                entity.HasIndex(e => new { e.Customer, e.PremiumLine }, "IX_PremiumCustomer_PLCustomer")
                    .IsUnique();

                entity.HasIndex(e => e.PremiumLine, "IX_PremiumCustomer_PremiumLine");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Codi).HasComment("Enumerable DTOPremiumCustomer.Codis (1.Included, 2.Excluded)");

                entity.Property(e => e.Customer).HasComment("Foreign key to CliGral table");

                entity.Property(e => e.Docfile)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Pdf contract; foreign key to Docfile table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments explaining why this customer is included or excluded");

                entity.Property(e => e.PremiumLine).HasComment("Foreign key to parent PremiumLine table");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.Property(e => e.UsrLastEdited).HasComment("Last user who edited this entry");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.PremiumCustomers)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumCustomer_CliGral");

                entity.HasOne(d => d.DocfileNavigation)
                    .WithMany(p => p.PremiumCustomers)
                    .HasForeignKey(d => d.Docfile)
                    .HasConstraintName("FK_PremiumCustomer_DocFile");

                entity.HasOne(d => d.PremiumLineNavigation)
                    .WithMany(p => p.PremiumCustomers)
                    .HasForeignKey(d => d.PremiumLine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumCustomer_PremiumLine");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.PremiumCustomerUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumCustomer_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.PremiumCustomerUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumCustomer_UsrLastEdited");
            });

            modelBuilder.Entity<PremiumLine>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("PremiumLine");

                entity.HasComment("Product range with distribution limited to selected customers");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Codi).HasComment("DTOEnumerable (1.Include all customers not specifically excluded, 2.Disable all customers not specifically included) ");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Start date");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Premium line name");
            });

            modelBuilder.Entity<PremiumProduct>(entity =>
            {
                entity.HasKey(e => new { e.PremiumLine, e.Product });

                entity.ToTable("PremiumProduct");

                entity.HasComment("Products included on a PremiumLine range");

                entity.Property(e => e.PremiumLine).HasComment("Foreign key to parent table PremiumLine");

                entity.Property(e => e.Product).HasComment("Foreign key to product category Stp table");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((999))")
                    .HasComment("Sort order in which this product should be listed");

                entity.HasOne(d => d.PremiumLineNavigation)
                    .WithMany(p => p.PremiumProducts)
                    .HasForeignKey(d => d.PremiumLine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumProduct_PremiumLine");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.PremiumProducts)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PremiumProduct_STP");
            });

            modelBuilder.Entity<PriceListCustomer>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("PriceList_Customer");

                entity.HasComment("Retail price lists");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Concepte)
                    .HasColumnType("text")
                    .HasComment("Price list description");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("ISO 4217 currency code");

                entity.Property(e => e.Customer).HasComment("In case it is a customer specific price list. Foreign key for CliGral table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Price list date. Only the most recent price list will be taken in account");

                entity.Property(e => e.FchEnd)
                    .HasColumnType("date")
                    .HasComment("Termination date");

                entity.Property(e => e.Visible)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If true, visible to customers");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.PriceListCustomers)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("FK_PriceList_Customer_CliGral");
            });

            modelBuilder.Entity<PriceListItemCustomer>(entity =>
            {
                entity.HasKey(e => new { e.PriceList, e.Art });

                entity.ToTable("PriceListItem_Customer");

                entity.HasComment("Retail prices");

                entity.HasIndex(e => new { e.Art, e.PriceList }, "IX_PriceListItemCustomer_Art_PriceList")
                    .IsUnique();

                entity.Property(e => e.PriceList).HasComment("Foreign key to parent table PriceList_Customer");

                entity.Property(e => e.Art).HasComment("Product; foreign key to Art table");

                entity.Property(e => e.Retail)
                    .HasColumnType("decimal(10, 2)")
                    .HasComment("If Customer field value is null, retail price. Other else, the value is the specific cost for this customer");

                entity.HasOne(d => d.ArtNavigation)
                    .WithMany(p => p.PriceListItemCustomers)
                    .HasForeignKey(d => d.Art)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceListItem_Customer_ART");

                entity.HasOne(d => d.PriceListNavigation)
                    .WithMany(p => p.PriceListItemCustomers)
                    .HasForeignKey(d => d.PriceList)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceListItem_Customer_PriceList_Customer");
            });

            modelBuilder.Entity<PriceListItemSupplier>(entity =>
            {
                entity.HasKey(e => new { e.PriceList, e.Ref });

                entity.ToTable("PriceListItem_Supplier");

                entity.HasComment("Supplier price list items");

                entity.HasIndex(e => new { e.PriceList, e.Lin }, "IX_PriceListItem_Supplier_Lin")
                    .IsUnique();

                entity.Property(e => e.PriceList).HasComment("Foreign key for parent table PriceList_Supplier");

                entity.Property(e => e.Ref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Manufacturer product code");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product name");

                entity.Property(e => e.Ean)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("EAN")
                    .HasComment("EAN 13 product code");

                entity.Property(e => e.InnerPack).HasComment("Units per package");

                entity.Property(e => e.Lin).HasComment("Sort order");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(8, 2)")
                    .HasComment("Distributor price list");

                entity.Property(e => e.Retail)
                    .HasColumnType("decimal(8, 2)")
                    .HasComment("Retail price list");

                entity.HasOne(d => d.PriceListNavigation)
                    .WithMany(p => p.PriceListItemSuppliers)
                    .HasForeignKey(d => d.PriceList)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceListItem_Supplier_PriceList_Supplier");
            });

            modelBuilder.Entity<PriceListSupplier>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PriceList");

                entity.ToTable("PriceList_Supplier");

                entity.HasComment("Supplier price lists");

                entity.HasIndex(e => new { e.Proveidor, e.Fch }, "IX_PriceList_Supplier_Proveidor_Fch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Concepte)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Price list description");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("ISO 4217 currency code");

                entity.Property(e => e.DiscountOffInvoice)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("Discount_OffInvoice")
                    .HasComment("Discount out of invoice like rapples etc");

                entity.Property(e => e.DiscountOnInvoice)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("Discount_OnInvoice")
                    .HasComment("Discount applicable on invoice");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Price list date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Supplier document; foreign key for Docfile table");

                entity.Property(e => e.Proveidor).HasComment("Supplier, foreign key for CliGral table");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.PriceListSuppliers)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_PriceList_Supplier_DocFile");

                entity.HasOne(d => d.ProveidorNavigation)
                    .WithMany(p => p.PriceListSuppliers)
                    .HasForeignKey(d => d.Proveidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PriceList_Supplier_CliGral");
            });

            modelBuilder.Entity<ProductChannel>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ProductChannel");

                entity.HasComment("Product range per distribution channel");

                entity.HasIndex(e => new { e.DistributionChannel, e.Cod }, "IX_ProductChannel_Channel");

                entity.HasIndex(e => new { e.Product, e.DistributionChannel }, "IX_ProductChannel_ProductChannelCod")
                    .IsUnique();

                entity.HasIndex(e => new { e.Product, e.Cod }, "IX_ProductChannel_ProductCod");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Defines wheter the product range is included or excluded from this channel. Enumerable DTOProductChannel.Cods (0.Included, 1.Excluded)");

                entity.Property(e => e.DistributionChannel).HasComment("Foreign key for DistributionChannel table");

                entity.Property(e => e.Product).HasComment("Product range; foreign key for either product brand Tpa table, product category Stp table or product sku Art table");

                entity.HasOne(d => d.DistributionChannelNavigation)
                    .WithMany(p => p.ProductChannels)
                    .HasForeignKey(d => d.DistributionChannel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductChannel_DistributionChannel");
            });

            modelBuilder.Entity<ProductDownload>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ProductDownload");

                entity.HasComment("Product documents");

                entity.HasIndex(e => e.Hash, "IX:ProductDownload_Hash");

                entity.HasIndex(e => e.Product, "IX_ProductDownload_Product");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Foreign key for Pdf document DocFile table");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.Obsoleto).HasComment("True if outdated");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Product).HasComment("Target object owner of the document, mainly a product");

                entity.Property(e => e.PublicarAlConsumidor)
                    .HasDefaultValueSql("((1))")
                    .HasComment("True if visible to consumer");

                entity.Property(e => e.PublicarAlDistribuidor)
                    .HasDefaultValueSql("((1))")
                    .HasComment("True if visible to proffessional");

                entity.Property(e => e.Src).HasComment("Type of document; enumerable DTOProductDownload.Srcs");

                entity.Property(e => e.Target).HasComment("Enumerable DTOProductDownload.TargetCods (0.Product)");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.ProductDownloads)
                    .HasForeignKey(d => d.Hash)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDownload_DocFile");
            });

            modelBuilder.Entity<ProductPlugin>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ProductPlugins");

                entity.ToTable("ProductPlugin");

                entity.HasComment("Website product plugins to include in Html posts");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Plugin caption");

                entity.Property(e => e.Product).HasComment("Plugin product, if any. Foreign key to either product brand Tpa table, product category Styp table or product sku Art table");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.ProductPluginUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_ProductPlugin_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.ProductPluginUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_ProductPlugin_Email");
            });

            modelBuilder.Entity<ProductPluginItem>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_ProductPluginSku");

                entity.ToTable("ProductPluginItem");

                entity.HasComment("Products included on each product plugin");

                entity.HasIndex(e => new { e.Plugin, e.Lin }, "Ix_ProductPluginItem_PluginLin");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Lin).HasComment("Sort order");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product name, Catalan language");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product name, English language");

                entity.Property(e => e.NomEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product name, Spanish language");

                entity.Property(e => e.NomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product name, Portuguese language");

                entity.Property(e => e.Plugin).HasComment("Foreign key to parent table ProductPlugin");

                entity.Property(e => e.Product).HasComment("Foreign key to either product brand Tpa table, product category Stp table or product sku Art table");

                entity.HasOne(d => d.PluginNavigation)
                    .WithMany(p => p.ProductPluginItems)
                    .HasForeignKey(d => d.Plugin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPluginItems_ProductPlugins");
            });

            modelBuilder.Entity<Projecte>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Projecte");

                entity.HasComment("Projects to be referred from other tables");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Project description");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Start date");

                entity.Property(e => e.FchTo)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasComment("End date");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Project name");
            });

            modelBuilder.Entity<PromofarmaFeed>(entity =>
            {
                entity.HasKey(e => new { e.Customer, e.Sku })
                    .HasName("PK_PromofarmaFeed_1");

                entity.ToTable("PromofarmaFeed");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_PROVINCIA");

                entity.HasComment("Region provinces");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary Key");

                entity.Property(e => e.Intrastat)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Mod347)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Code for model 347 Spanish tax form");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Provincia name");

                entity.Property(e => e.Regio).HasComment("Foreign key for Region table");

                entity.HasOne(d => d.RegioNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.Regio)
                    .HasConstraintName("FK_Provincia_Regio");
            });

            modelBuilder.Entity<PrvCliNum>(entity =>
            {
                entity.HasKey(e => new { e.Proveidor, e.CliNum });

                entity.ToTable("PrvCliNum");

                entity.HasComment("Customer numbers assigned by our suppliers to our customers. Used to automate Excel files for example Mayborn requests to supply his Pharma channel distributors");

                entity.Property(e => e.Proveidor).HasComment("Supplier. Foreign key to CliGral table");

                entity.Property(e => e.CliNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Customer number assigned by our supplier to our customer");

                entity.Property(e => e.Customer).HasComment("Customer whom belongs this customer number assigned by this supplier. Foreign key for CliGral table");

                entity.HasOne(d => d.ProveidorNavigation)
                    .WithMany(p => p.PrvCliNums)
                    .HasForeignKey(d => d.Proveidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrvCliNum_CliGralCustomer");
            });

            modelBuilder.Entity<PwaMenuItem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("PwaMenuItem");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Recall>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Recall");

                entity.HasComment("Manufacturer requests to recall defectuous products");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Date of alert");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Friendly name");
            });

            modelBuilder.Entity<RecallCli>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("RecallCli");

                entity.HasComment("Customer recall of products");

                entity.HasIndex(e => e.Recall, "IX_RecallCli_Recall");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Customer pickup address");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Customer contact person email address");

                entity.Property(e => e.ContactNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer contact person name");

                entity.Property(e => e.ContactTel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer contact person phone number");

                entity.Property(e => e.Country).HasComment("Customer pickup country");

                entity.Property(e => e.Customer).HasComment("Customer; foreign key to CliGral table");

                entity.Property(e => e.Delivery).HasComment("Foreign key to Alb table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchVivace)
                    .HasColumnType("datetime")
                    .HasComment("Date received at the warehouse");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Customer pickup location");

                entity.Property(e => e.PurchaseOrder).HasComment("Foreign key to Pdc table");

                entity.Property(e => e.Recall).HasComment("Foreign key to parent table Recall");

                entity.Property(e => e.RegMuelle)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasComment("Warehouse port log number");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Customer pickup zip code");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.RecallClis)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_RecallCli_Country");

                entity.HasOne(d => d.DeliveryNavigation)
                    .WithMany(p => p.RecallClis)
                    .HasForeignKey(d => d.Delivery)
                    .HasConstraintName("FK_RecallCli_Alb");

                entity.HasOne(d => d.PurchaseOrderNavigation)
                    .WithMany(p => p.RecallClis)
                    .HasForeignKey(d => d.PurchaseOrder)
                    .HasConstraintName("FK_RecallCli_PDC");

                entity.HasOne(d => d.RecallNavigation)
                    .WithMany(p => p.RecallClis)
                    .HasForeignKey(d => d.Recall)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecallCli_Recall");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.RecallClis)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_RecallCli_Email");
            });

            modelBuilder.Entity<RecallProduct>(entity =>
            {
                entity.HasKey(e => new { e.RecallCli, e.SerialNumber })
                    .HasName("PK_RecallSerial");

                entity.ToTable("RecallProduct");

                entity.HasComment("Products recalled from customer");

                entity.Property(e => e.RecallCli).HasComment("Foreign key to parent table RecallCli");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product serial number");

                entity.Property(e => e.Sku).HasComment("Product; foreign key for either brand Tpa table, category Stp table or product Art table");

                entity.HasOne(d => d.RecallCliNavigation)
                    .WithMany(p => p.RecallProducts)
                    .HasForeignKey(d => d.RecallCli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecallProduct_RecallCli");
            });

            modelBuilder.Entity<RedsysErr>(entity =>
            {
                entity.ToTable("RedsysErr");

                entity.HasComment("Errors received from Redsys Api. Redsys is our bank payment gateway platform for customer payments through credit cards");

                entity.Property(e => e.Id)
                    .HasMaxLength(7)
                    .HasComment("Error code");

                entity.Property(e => e.ErrDsc)
                    .HasColumnType("text")
                    .HasComment("Error description");
            });

            modelBuilder.Entity<Regio>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Regio");

                entity.HasComment("Region details. A region belongs to a Country and is split on Provincias");

                entity.HasIndex(e => new { e.Country, e.Nom }, "IX_Regio_CountryNom");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Country).HasComment("Foreign key to parent table Country");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Region name");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Regios)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Regio_Country");
            });

            modelBuilder.Entity<RepCliCom>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("RepCliCom");

                entity.HasComment("Agent customers with specific commission terms ");

                entity.HasIndex(e => new { e.Rep, e.Cli, e.Fch }, "IX_RepCliCom_RepCliFch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cli).HasComment("Customer, foreign key to CliGral table");

                entity.Property(e => e.ComCod).HasComment("Enumerable DTORepCliCom.Cods: 0.Standard, 1.Reduced, 2.Excluded");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date of agreement");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date of entry creation");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments to explain the reason of special conditions");

                entity.Property(e => e.Rep).HasComment("Commercial agent, foreign key to CliRep table");

                entity.Property(e => e.UsrCreated).HasComment("User who registered this entry. Foreign key to Email table");

                entity.HasOne(d => d.CliNavigation)
                    .WithMany(p => p.RepCliComs)
                    .HasForeignKey(d => d.Cli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepCliCom_CliClient");

                entity.HasOne(d => d.RepNavigation)
                    .WithMany(p => p.RepCliComs)
                    .HasForeignKey(d => d.Rep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepCliCom_CliRep");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.RepCliComs)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepCliCom_Email");
            });

            modelBuilder.Entity<RepLiq>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("RepLiq");

                entity.HasComment("Commission statements");

                entity.HasIndex(e => new { e.RepGuid, e.Yea, e.Id }, "IX_RepGuidYeaId")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.BaseFras)
                    .HasColumnType("decimal(19, 2)")
                    .HasComment("Sum of customer invoices amounts");

                entity.Property(e => e.CcaGuid).HasComment("Accounts entry, foreign key for Cca table");

                entity.Property(e => e.ComisioDivisa)
                    .HasColumnType("decimal(19, 2)")
                    .HasComment("Commission in foreign currency, if any");

                entity.Property(e => e.ComisioEur)
                    .HasColumnType("decimal(19, 2)")
                    .HasComment("Commission in Euro");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Statement date");

                entity.Property(e => e.Id).HasComment("statement Id. It starts again each year");

                entity.Property(e => e.Irpfpct)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("IRPFpct")
                    .HasComment("Irpf percentage");

                entity.Property(e => e.Ivapct)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("IVApct")
                    .HasComment("VAT percentage");

                entity.Property(e => e.RepGuid).HasComment("Agent. Foreign key for CliGral table");

                entity.Property(e => e.Yea).HasComment("Year of the statement");
            });

            modelBuilder.Entity<RepProduct>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_RepProduct");

                entity.HasComment("Agent portfolio");

                entity.HasIndex(e => e.Area, "IX_RepProducts_Area");

                entity.HasIndex(e => e.DistributionChannel, "IX_RepProducts_Channel");

                entity.HasIndex(e => e.Rep, "IX_RepProducts_Rep");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Area).HasComment("Working area where agent customers are based");

                entity.Property(e => e.Cod).HasComment("Enumerable DTORepProduct.Cods: 0.NotSet, 1.Included, 2.Excluded");

                entity.Property(e => e.ComRed)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Reduced commission when agreed for specific operations");

                entity.Property(e => e.ComStd)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Standard commission for sales to customers of agreed channels within agreed areas");

                entity.Property(e => e.DistributionChannel).HasComment("Distribution channels granted to this agent within his working area");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Entry creation date");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("Date of agreement");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("Date of termination");

                entity.Property(e => e.Product).HasComment("Product. May be a brand, category or sku");

                entity.Property(e => e.Rep).HasComment("Agent. Foreign key to CliGral table");

                entity.HasOne(d => d.DistributionChannelNavigation)
                    .WithMany(p => p.RepProducts)
                    .HasForeignKey(d => d.DistributionChannel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepProducts_DistributionChannel1");

                entity.HasOne(d => d.RepNavigation)
                    .WithMany(p => p.RepProducts)
                    .HasForeignKey(d => d.Rep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepProducts_CliRep");
            });

            modelBuilder.Entity<Rp>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_RPS");

                entity.HasComment("Invoice item that generates a sale commission to a specific rep");

                entity.HasIndex(e => e.FraGuid, "IX_RPS_Fra");

                entity.HasIndex(e => e.RepGuid, "IX_RPS_Rep");

                entity.HasIndex(e => e.RepLiqGuid, "IX_RPS_RepLiq");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Bas)
                    .HasColumnType("numeric(12, 2)")
                    .HasComment("Base amount over which the commission applies");

                entity.Property(e => e.ComVal)
                    .HasColumnType("numeric(12, 2)")
                    .HasComment("Commission value");

                entity.Property(e => e.FraGuid).HasComment("Invoice. Foreign key to Fra table");

                entity.Property(e => e.Liquidable)
                    .HasDefaultValueSql("((1))")
                    .HasComment("if true, commission will be granted to the agent");

                entity.Property(e => e.Obs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Comments");

                entity.Property(e => e.RepGuid).HasComment("Agent. Foreign key for CliGral table");

                entity.Property(e => e.RepLiqGuid).HasComment("Commission statement. Foreign key to RepLiq table");

                entity.HasOne(d => d.FraGu)
                    .WithMany(p => p.Rps)
                    .HasForeignKey(d => d.FraGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RPS_FRA");

                entity.HasOne(d => d.RepLiqGu)
                    .WithMany(p => p.Rps)
                    .HasForeignKey(d => d.RepLiqGuid)
                    .HasConstraintName("FK_RPS_RepLiq");
            });

            modelBuilder.Entity<SalesManager>(entity =>
            {
                entity.HasKey(e => new { e.Contact, e.Channel, e.Brand, e.Area });

                entity.ToTable("SalesManager");

                entity.HasComment("Sales managers");

                entity.Property(e => e.Contact).HasComment("Sales manager Id; foreign key for CliGral");

                entity.Property(e => e.Channel).HasComment("Distribution channel Id; foreign key for CliGral table");

                entity.Property(e => e.Brand).HasComment("Product Id, foreign key for VwProductNom view");

                entity.Property(e => e.Area).HasComment("Area Id, foreign key for VwArea view");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasComment("Date of agreement start");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Date of agreement termination");
            });

            modelBuilder.Entity<SatRecall>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("SatRecall");

                entity.HasComment("Manufacturer repairs. The manufacturer picks up the product at customer location, repairs and returns it back");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasColumnType("text")
                    .HasComment("Pickup address");

                entity.Property(e => e.Carrec).HasComment("Charge amount, if any");

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Pickup contact person name");

                entity.Property(e => e.CreditFch)
                    .HasColumnType("datetime")
                    .HasComment("Credit date if any");

                entity.Property(e => e.CreditNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Credit number is case the product has to be credited");

                entity.Property(e => e.Defect)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Description of the problem");

                entity.Property(e => e.FchCustomer)
                    .HasColumnType("datetime")
                    .HasComment("Date we request the customer to prepare the goods for pickup");

                entity.Property(e => e.FchManufacturer)
                    .HasColumnType("datetime")
                    .HasComment("Date the manufacturer ships the product back");

                entity.Property(e => e.Incidencia).HasComment("Support incidence. Foreign key for Incidencies table");

                entity.Property(e => e.Mode).HasComment("Enumerable DTOSatRecall.Modes (0.Credited, 1.Repaired)");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.PickupFrom).HasComment("Enumerable DTOSatRecall.PickupFroms (1.Store, 2.Our waorshop, 3.Our warehouse)");

                entity.Property(e => e.PickupRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Manufacturer tracking number for pickup");

                entity.Property(e => e.ReturnFch)
                    .HasColumnType("date")
                    .HasComment("Date of return back to customer");

                entity.Property(e => e.ReturnRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Tracking number for shipment back from manufacturer to customer");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Pickup contact person phone number");

                entity.Property(e => e.Zip).HasComment("Pickup zip; foreign key for Zip table");

                entity.HasOne(d => d.IncidenciaNavigation)
                    .WithMany(p => p.SatRecalls)
                    .HasForeignKey(d => d.Incidencia)
                    .HasConstraintName("FK_SatRecall_Incidencies");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.SatRecalls)
                    .HasForeignKey(d => d.Zip)
                    .HasConstraintName("FK_SatRecall_Zip");
            });

            modelBuilder.Entity<SearchRequest>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_SearchBox");

                entity.ToTable("SearchRequest");

                entity.HasComment("Logs each time a visitor uses our website search box, registering the search key used");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Email).HasComment("User, if logged in. Foreign key for Email table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time of the request");

                entity.Property(e => e.SearchKey)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Text typed by the visitor on the search box");

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.SearchRequests)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK_SearchRequest_Email");
            });

            modelBuilder.Entity<SearchResult>(entity =>
            {
                entity.HasNoKey();

                entity.HasComment("Logs the results found when a user uses the searchbox of our website");

                entity.HasIndex(e => new { e.SearchRequest, e.Cod }, "IX_SearchResults_RequestCod");

                entity.HasIndex(e => e.Source, "IX_SearchResults_Source");

                entity.Property(e => e.Cod).HasComment("Type of result object. Enumerable DTOSearchRequest.Result.Cods (brand, category, contact, noticia...)");

                entity.Property(e => e.SearchRequest).HasComment("Foreign key to parent table SearchRequest");

                entity.Property(e => e.Source).HasComment("Result object found");

                entity.HasOne(d => d.SearchRequestNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SearchRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SearchResults_SearchRequest");
            });

            modelBuilder.Entity<SegSocialGrup>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CategoriasSegSocial");

                entity.HasComment("Social security employees category groups");

                entity.HasIndex(e => e.Id, "IX_SegSocialGrups_Id")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Id).HasComment("Sort order");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Group name");
            });

            modelBuilder.Entity<SepaMe>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_DTOSepaMe");

                entity.ToTable("SepaMe");

                entity.HasComment("Bank Sepa mandates signed by us to our creditors");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Banc).HasComment("Bank entity; foreign key for CliBnc table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this record was created");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("Date from which this mandate is valid");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasComment("Date this record was edited for last time");

                entity.Property(e => e.FchTo)
                    .HasColumnType("date")
                    .HasComment("Expiration date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Mandate pdf document; foreign key for Docfile table");

                entity.Property(e => e.Lliurador).HasComment("Creditor issuing the Sepa mandate; foreign key for CliGral table");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Mandate unique reference the creditor should mention on each bank charge under this mandate");

                entity.Property(e => e.UsrCreated).HasComment("User who registered this document; foreign key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this record for kast time");

                entity.HasOne(d => d.BancNavigation)
                    .WithMany(p => p.SepaMes)
                    .HasForeignKey(d => d.Banc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SepaMe_CliBnc");

                entity.HasOne(d => d.HashNavigation)
                    .WithMany(p => p.SepaMes)
                    .HasForeignKey(d => d.Hash)
                    .HasConstraintName("FK_SepaMe_DocFile");

                entity.HasOne(d => d.LliuradorNavigation)
                    .WithMany(p => p.SepaMes)
                    .HasForeignKey(d => d.Lliurador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SepaMe_Lliurador");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.SepaMeUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .HasConstraintName("FK_SepaMe_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.SepaMeUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .HasConstraintName("FK_SepaMe_UsrLastEdited");
            });

            modelBuilder.Entity<SepaText>(entity =>
            {
                entity.HasKey(e => new { e.Lang, e.Id });

                entity.ToTable("Sepa_Texts");

                entity.HasComment("Texts used to composed Sepa bank mandate in the different languages supported");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-3 language code");

                entity.Property(e => e.Id).HasComment("Code refers to text position within the mandate");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasComment("Translated text");
            });

            modelBuilder.Entity<Sheet1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sheet1$");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .HasColumnName("id");

                entity.Property(e => e.Marketplace)
                    .HasMaxLength(255)
                    .HasColumnName("marketplace");

                entity.Property(e => e.Sku)
                    .HasMaxLength(255)
                    .HasColumnName("sku");
            });

            modelBuilder.Entity<ShoppingBasket>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("ShoppingBasket");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Address).HasColumnType("text");

                entity.Property(e => e.Amount).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrpObs).HasColumnType("text");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingBasketItem>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Lin });

                entity.ToTable("ShoppingBasketItem");

                entity.Property(e => e.Dto).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.ShoppingBasketItems)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingBasketItem_ShoppingBasket");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.ShoppingBasketItems)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingBasketItem_Art");
            });

            modelBuilder.Entity<SkuBundle>(entity =>
            {
                entity.HasKey(e => new { e.Bundle, e.Sku });

                entity.ToTable("SkuBundle");

                entity.HasComment("We call bundle to a product agrupation composed of 2 or more sku product components. This table lists which components define each bundle.");

                entity.HasIndex(e => new { e.Bundle, e.Ord }, "Ix_SkuBundle_BundleOrd")
                    .IsUnique();

                entity.HasIndex(e => new { e.Sku, e.Bundle }, "Ix_SkuBundle_SkuBundle")
                    .IsUnique();

                entity.Property(e => e.Bundle).HasComment("Primary key; foreign key for Art table");

                entity.Property(e => e.Sku).HasComment("Product sku; foreign key for Art table");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Qty)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Units of this product sku included in the bundle");

                entity.HasOne(d => d.BundleNavigation)
                    .WithMany(p => p.SkuBundleBundleNavigations)
                    .HasForeignKey(d => d.Bundle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkuBundle_Bundle");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.SkuBundleSkuNavigations)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkuBundle_Sku");
            });

            modelBuilder.Entity<SkuMoqLock>(entity =>
            {
                entity.HasKey(e => new { e.Sku, e.Usr })
                    .IsClustered(false);

                entity.ToTable("SkuMoqLock");

                entity.HasComment("Specific products may be subjected to minimum quantities per order. In certain cases, it may be needed to skip this constraint during a short time to allow an authorised user to enter an order with a single unit, for example to attend a warranty reposition. This teble keeps track of such requests to revert the operation after a couple of minutes");

                entity.Property(e => e.Sku).HasComment("Product requested to release minimum order quantity. Foreign key for Art table");

                entity.Property(e => e.Usr).HasComment("User requesting to release product MOQ (Minimum Order Quantity). Foreingn key to Email table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the user requests to release the product minimum order quantity- It will only be released a couple of minutes, this is why the time is registered.");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.SkuMoqLocks)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Art_Bloqueig_Log_Art");

                entity.HasOne(d => d.UsrNavigation)
                    .WithMany(p => p.SkuMoqLocks)
                    .HasForeignKey(d => d.Usr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Art_Bloqueig_Log_Email");
            });

            modelBuilder.Entity<SkuWith>(entity =>
            {
                entity.HasKey(e => new { e.Parent, e.Child });

                entity.ToTable("SkuWith");

                entity.HasComment("Product skus that should also be added to the order when certain products are ordered");

                entity.Property(e => e.Parent).HasComment("Parent product sku that requires other products to be added to the purchase ; foreifgn key for Art table");

                entity.Property(e => e.Child).HasComment("Child product sku that is required to be added when parent is ordered; foreifgn key for Art table");

                entity.Property(e => e.Qty).HasComment("Units of child product required for each parent product");

                entity.HasOne(d => d.ChildNavigation)
                    .WithMany(p => p.SkuWithChildNavigations)
                    .HasForeignKey(d => d.Child)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkuWith_ChildSku");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.SkuWithParentNavigations)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SkuWith_ParentSku");
            });

            modelBuilder.Entity<SocialMediaWidget>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_SocialMediaPlatform");

                entity.ToTable("SocialMediaWidget");

                entity.HasComment("Widget details for social media platforms");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Brand).HasComment("Product brand; foreign key for Tpa table");

                entity.Property(e => e.Platform).HasComment("Enumerable DTOSocialMediaWidget.Platforms (1.Facebook, 2.Twitter...)");

                entity.Property(e => e.Titular)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Platform profile name");

                entity.Property(e => e.WidgetId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Platform profile id");

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.SocialMediaWidgets)
                    .HasForeignKey(d => d.Brand)
                    .HasConstraintName("FK_SocialMediaWidget_Tpa");
            });

            modelBuilder.Entity<Sorteo>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Raffles for consumers");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Answers)
                    .HasColumnType("text")
                    .HasComment("Multiline string, one answer each line");

                entity.Property(e => e.Art).HasComment("Product prize of the raffle. Foreign key for Art table");

                entity.Property(e => e.Bases)
                    .HasColumnType("text")
                    .HasComment("Free text with the raffles rules");

                entity.Property(e => e.CostPrize)
                    .HasColumnType("decimal(12, 2)")
                    .HasComment("Landed cost of the prize");

                entity.Property(e => e.CostPubli)
                    .HasColumnType("decimal(12, 2)")
                    .HasComment("Cost of the publicity on social media");

                entity.Property(e => e.Country).HasComment("Foreign key for Country table");

                entity.Property(e => e.Delivery).HasComment("Foreign key for Alb table");

                entity.Property(e => e.FchDelivery)
                    .HasColumnType("date")
                    .HasComment("Date the prize was delivered to the distributor");

                entity.Property(e => e.FchDistributorReaction)
                    .HasColumnType("date")
                    .HasComment("Date the distributor answered our message");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasComment("Initial date the consumers can start to participate");

                entity.Property(e => e.FchPicture)
                    .HasColumnType("date")
                    .HasComment("Date the distributor sent the picture of the winner");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Termination date");

                entity.Property(e => e.FchWinnerReaction)
                    .HasColumnType("date")
                    .HasComment("Date the winner contacted us");

                entity.Property(e => e.ImgBanner600)
                    .HasColumnType("image")
                    .HasComment("Banner 600 pixels width");

                entity.Property(e => e.ImgCallToAction500)
                    .HasColumnType("image")
                    .HasComment("Call to action image for social media 500 pixels width");

                entity.Property(e => e.ImgFbFeatured116)
                    .HasColumnType("image")
                    .HasComment("Image 116 pixels width");

                entity.Property(e => e.ImgWinner)
                    .HasColumnType("image")
                    .HasComment("Picture sent by the distributor with the winner receiving the prize");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-2 Raffle language code (we issue different raffles for Spain and Portugal)");

                entity.Property(e => e.Question)
                    .HasColumnType("text")
                    .HasComment("Free text. The users have to answer a question to get a valid participation");

                entity.Property(e => e.RightAnswer).HasComment("Index of the right answer within the string array from splitting the multiline Answers value into each line");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasComment("Raffle description");

                entity.Property(e => e.UrlExterna)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Friendly url to the raffle");

                entity.Property(e => e.Visible)
                    .HasDefaultValueSql("((1))")
                    .HasComment("If true, consumers have access to the raffle");

                entity.Property(e => e.Winner).HasComment("Foreign key for Email table");

                entity.Property(e => e.WinnerNom)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Name of the winner of the raffle");

                entity.HasOne(d => d.ArtNavigation)
                    .WithMany(p => p.Sorteos)
                    .HasForeignKey(d => d.Art)
                    .HasConstraintName("FK_Sorteos_Art");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Sorteos)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_Sorteos_Country");

                entity.HasOne(d => d.DeliveryNavigation)
                    .WithMany(p => p.Sorteos)
                    .HasForeignKey(d => d.Delivery)
                    .HasConstraintName("FK_Sorteos_Alb");

                entity.HasOne(d => d.WinnerNavigation)
                    .WithMany(p => p.Sorteos)
                    .HasForeignKey(d => d.Winner)
                    .HasConstraintName("FK_Sorteos_SorteoLeads");
            });

            modelBuilder.Entity<SorteoLead>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Raffle participants");

                entity.HasIndex(e => new { e.Sorteo, e.Lead }, "SorteoLeads.Sorteo.Lead");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Answer).HasComment("Choosen answer. Index of available answers, starting with zero");

                entity.Property(e => e.Distributor).HasComment("Retailer the participant selected to receive his prize in case he wins the raffle. Foreign key for CliGral");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date and time of participation");

                entity.Property(e => e.Lead).HasComment("User. Foreign key for Email table");

                entity.Property(e => e.Num)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Sequential number, unique to each Raffle. Each participant gets a unique number of participation");

                entity.Property(e => e.Sorteo).HasComment("Raffle. Foreign key for Sorteos table");

                entity.HasOne(d => d.DistributorNavigation)
                    .WithMany(p => p.SorteoLeads)
                    .HasForeignKey(d => d.Distributor)
                    .HasConstraintName("FK_SorteoLeads_CliGral");

                entity.HasOne(d => d.LeadNavigation)
                    .WithMany(p => p.SorteoLeads)
                    .HasForeignKey(d => d.Lead)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SorteoLeads_Email");

                entity.HasOne(d => d.SorteoNavigation)
                    .WithMany(p => p.SorteoLeads)
                    .HasForeignKey(d => d.Sorteo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SorteoLeads_Sorteos");
            });

            modelBuilder.Entity<Spv>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Spv");

                entity.HasComment("Repair reports from workshop");

                entity.HasIndex(e => e.AlbGuid, "IX_Spv_AlbGuid");

                entity.HasIndex(e => e.CliGuid, "IX_Spv_CliGuid");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Id }, "IX_Spv_EmpYeaId")
                    .IsUnique();

                entity.HasIndex(e => e.SpvIn, "IX_Spv_SpvIn");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasColumnType("text")
                    .HasComment("Destination address");

                entity.Property(e => e.AlbGuid).HasComment("Delivery; foreign key for Alb table");

                entity.Property(e => e.CliGuid).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Customer contact person");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('EUR')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.FchAvis)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date we ask the customer to pick upo the goods to repair");

                entity.Property(e => e.FchOutOfSpvIn)
                    .HasColumnType("datetime")
                    .HasComment("Date in which this entry was removed from pending to receive");

                entity.Property(e => e.FchOutOfSpvOut)
                    .HasColumnType("datetime")
                    .HasComment("Date in which this entry was removed from pending to repair");

                entity.Property(e => e.FchRead)
                    .HasColumnType("datetime")
                    .HasComment("Date the workshop has read the job order");

                entity.Property(e => e.FchRecepcio)
                    .HasColumnType("datetime")
                    .HasComment("Date the workshop received the product to repair");

                entity.Property(e => e.FchSortida)
                    .HasColumnType("datetime")
                    .HasComment("Date the workshop delivered the repaired product");

                entity.Property(e => e.Garantia).HasComment("True if the defect is covered by the warranty");

                entity.Property(e => e.Id).HasComment("Report Id, unique within Company/Year");

                entity.Property(e => e.Incidencia).HasComment("Incidence; foreign key to Incidencies table");

                entity.Property(e => e.LabelEmailedTo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Recipient to deliver the labels to pickup the goods");

                entity.Property(e => e.ManufactureDate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Product manufacture date, if available");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Customer destination name");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.ObsOutOfSpvIn)
                    .HasColumnType("text")
                    .HasComment("Comments why this entry was removed from pending to receive");

                entity.Property(e => e.ObsOutOfSpvOut)
                    .HasColumnType("text")
                    .HasComment("Comments why this entry was removed from pending to repair");

                entity.Property(e => e.ObsTecnic)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Technician comments");

                entity.Property(e => e.ProductGuid).HasComment("Product; foreign key for either the brand Tpa table, category Stp table or product sku table");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Product serial number, if applicable");

                entity.Property(e => e.SolicitaGarantia).HasComment("True if the customer requests to be repaired under warranty");

                entity.Property(e => e.SpvIn).HasComment("Product reception; foreign key for SpvIn table");

                entity.Property(e => e.Sref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SRef")
                    .HasDefaultValueSql("('')")
                    .HasComment("Customer reference, if any");

                entity.Property(e => e.Tel)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Customer contact phone");

                entity.Property(e => e.UsrOutOfSpvInGuid).HasComment("User who removed this entry from pending to receive if this is the case; foreign key for Email table");

                entity.Property(e => e.UsrOutOfSpvOutGuid).HasComment("User who removed this entry from pending to repair if this is the case; foreign key for Email table");

                entity.Property(e => e.UsrRegisterGuid).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.UsrTecnicGuid).HasComment("Technician who took care of this job; foreign key for Email table");

                entity.Property(e => e.ValEmbalatje)
                    .HasColumnType("money")
                    .HasComment("Value of packaging");

                entity.Property(e => e.ValJob)
                    .HasColumnType("money")
                    .HasComment("Job value");

                entity.Property(e => e.ValPorts)
                    .HasColumnType("money")
                    .HasComment("Transport value");

                entity.Property(e => e.ValSpares)
                    .HasColumnType("money")
                    .HasComment("Value of any spares replaced");

                entity.Property(e => e.Yea).HasComment("Year of the report");

                entity.Property(e => e.Zip).HasComment("Destination zip code id; foreign key to Zip table");

                entity.HasOne(d => d.AlbGu)
                    .WithMany(p => p.Spvs)
                    .HasForeignKey(d => d.AlbGuid)
                    .HasConstraintName("FK_Spv_Alb");

                entity.HasOne(d => d.CliGu)
                    .WithMany(p => p.Spvs)
                    .HasForeignKey(d => d.CliGuid)
                    .HasConstraintName("FK_Spv_CliGral");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Spvs)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spv_Emp");

                entity.HasOne(d => d.IncidenciaNavigation)
                    .WithMany(p => p.Spvs)
                    .HasForeignKey(d => d.Incidencia)
                    .HasConstraintName("FK_Spv_Incidencies");

                entity.HasOne(d => d.SpvInNavigation)
                    .WithMany(p => p.Spvs)
                    .HasForeignKey(d => d.SpvIn)
                    .HasConstraintName("FK_Spv_SpvIn");

                entity.HasOne(d => d.UsrOutOfSpvInGu)
                    .WithMany(p => p.SpvUsrOutOfSpvInGus)
                    .HasForeignKey(d => d.UsrOutOfSpvInGuid)
                    .HasConstraintName("FK_Spv_UsrOutOfSpvIn");

                entity.HasOne(d => d.UsrOutOfSpvOutGu)
                    .WithMany(p => p.SpvUsrOutOfSpvOutGus)
                    .HasForeignKey(d => d.UsrOutOfSpvOutGuid)
                    .HasConstraintName("FK_Spv_UsrOutOfSpvOut");

                entity.HasOne(d => d.UsrRegisterGu)
                    .WithMany(p => p.SpvUsrRegisterGus)
                    .HasForeignKey(d => d.UsrRegisterGuid)
                    .HasConstraintName("FK_Spv_UsrRegister");

                entity.HasOne(d => d.UsrTecnicGu)
                    .WithMany(p => p.SpvUsrTecnicGus)
                    .HasForeignKey(d => d.UsrTecnicGuid)
                    .HasConstraintName("FK_Spv_UsrTecnic");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.Spvs)
                    .HasForeignKey(d => d.Zip)
                    .HasConstraintName("FK_Spv_Zip");
            });

            modelBuilder.Entity<SpvIn>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("SpvIn");

                entity.HasComment("Workshop reception of products to repair");

                entity.HasIndex(e => new { e.Emp, e.Yea, e.Id }, "IX_SpvIn_EmpYeaId")
                    .IsUnique();

                entity.HasIndex(e => new { e.Emp, e.Fch, e.Expedicio }, "Ix_SpvIn_EmpFchExp");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Bultos).HasComment("Number of packages received");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Expedicio)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Forwarder reference number of the delivery");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date of reception");

                entity.Property(e => e.Id).HasComment("Sequential number unique per Company and year");

                entity.Property(e => e.Kg).HasComment("Weight in Kg");

                entity.Property(e => e.M3)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Volume in cubic meters");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')")
                    .HasComment("Comments to report any incidences on reception (lack of packages, packages received in bad conditions...)");

                entity.Property(e => e.UsrGuid).HasComment("User who created this entry; foreign key for Email table");

                entity.Property(e => e.Yea).HasComment("Year of reception");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.SpvIns)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpvIn_Emp");

                entity.HasOne(d => d.UsrGu)
                    .WithMany(p => p.SpvIns)
                    .HasForeignKey(d => d.UsrGuid)
                    .HasConstraintName("FK_SpvIn_Email");
            });

            modelBuilder.Entity<Ssc>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_SSC");

                entity.ToTable("Ssc");

                entity.HasComment("Subscriptions. Automated tasks from Windows service Matsched (table Task) send email notifications to subscribed users");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Ord).HasComment("Sort order in which this subscription should appear");

                entity.Property(e => e.Reverse).HasComment("True if all users are subscribed except for those listed, false if only users listed are subscribed");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Sscs)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ssc_Emp");
            });

            modelBuilder.Entity<SscEmail>(entity =>
            {
                entity.HasKey(e => new { e.SscGuid, e.Email })
                    .HasName("PK_SSCEMAIL");

                entity.ToTable("SscEmail");

                entity.HasComment("Subscriptors subscribed to subscriptions from Ssc table");

                entity.HasIndex(e => new { e.Email, e.SscGuid }, "IX_SSCEMAIL_EmailSsc")
                    .IsUnique();

                entity.Property(e => e.SscGuid).HasComment("Subscription; foreign key for Ssc table");

                entity.Property(e => e.Email).HasComment("User; foreign key for Email table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.SscEmails)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SscEmail_Email");

                entity.HasOne(d => d.SscGu)
                    .WithMany(p => p.SscEmails)
                    .HasForeignKey(d => d.SscGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SscEmail_Ssc");
            });

            modelBuilder.Entity<SscLog>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_SSCLOG");

                entity.ToTable("SscLog");

                entity.HasComment("Any subscription and unsubscription event is logged in this table");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Action).HasComment("1. Subscribed, 0.Unsubscribed");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Event date");

                entity.Property(e => e.Mail).HasComment("Subscriptor; foreign key for Email table");

                entity.Property(e => e.SscGuid).HasComment("Subscription; foreign key for Ssc table");

                entity.HasOne(d => d.MailNavigation)
                    .WithMany(p => p.SscLogs)
                    .HasForeignKey(d => d.Mail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SscLog_Email");

                entity.HasOne(d => d.SscGu)
                    .WithMany(p => p.SscLogs)
                    .HasForeignKey(d => d.SscGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SscLog_Ssc");
            });

            modelBuilder.Entity<SscRol>(entity =>
            {
                entity.HasKey(e => new { e.SscGuid, e.Rol })
                    .HasName("PK_SSCROL");

                entity.ToTable("SscRol");

                entity.HasComment("User rols available for each subscription");

                entity.Property(e => e.SscGuid).HasComment("Subscription; foreign key for Ssc table");

                entity.Property(e => e.Rol).HasComment("Enumerable DTORol.Ids");

                entity.HasOne(d => d.SscGu)
                    .WithMany(p => p.SscRols)
                    .HasForeignKey(d => d.SscGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SscRol_Ssc");
            });

            modelBuilder.Entity<StaffCategory>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_LaboralCategories");

                entity.ToTable("StaffCategory");

                entity.HasComment("Social security employee categories");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Category name");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((99))")
                    .HasComment("Sort order");

                entity.Property(e => e.SegSocialGrup).HasComment("Group; foreign key for SegSocialGrups table");

                entity.HasOne(d => d.SegSocialGrupNavigation)
                    .WithMany(p => p.StaffCategories)
                    .HasForeignKey(d => d.SegSocialGrup)
                    .HasConstraintName("FK_StaffCategory_SegSocialGrups");
            });

            modelBuilder.Entity<StaffHoliday>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("StaffHoliday");

                entity.HasComment("Employees holidays");

                entity.HasIndex(e => e.Staff, "IX_StaffHoliday_Staff");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Justification. Enumerable DTOStaff.Cods");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasComment("Date starting holidays");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Date ending holidays");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Staff).HasComment("Employee, if specific for certain employees; foreign key for CliGral table");

                entity.HasOne(d => d.StaffNavigation)
                    .WithMany(p => p.StaffHolidays)
                    .HasForeignKey(d => d.Staff)
                    .HasConstraintName("FK_StaffHoliday_CliStaff");
            });

            modelBuilder.Entity<StaffPo>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Employee position");

                entity.HasIndex(e => e.Ord, "IX_StaffPos_Ord");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Position name, Catalan language");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Position name, English language");

                entity.Property(e => e.NomEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Position name, Spanish language");

                entity.Property(e => e.Ord).HasComment("Sort order");
            });

            modelBuilder.Entity<StaffSched>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("StaffSched");

                entity.HasComment("Employees schedule");

                entity.HasIndex(e => e.Emp, "IX_StaffSched_Emp");

                entity.HasIndex(e => new { e.Staff, e.FchFrom }, "IX_StaffSched_Staff");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key; parent for StaffSchedItems table");

                entity.Property(e => e.Emp).HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasComment("Schedule start date");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("Schedule termination date");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Staff).HasComment("Employee; foreign key for CliGral table");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.StaffScheds)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffSched_Emp");

                entity.HasOne(d => d.StaffNavigation)
                    .WithMany(p => p.StaffScheds)
                    .HasForeignKey(d => d.Staff)
                    .HasConstraintName("FK_StaffSched_CliStaff");
            });

            modelBuilder.Entity<StaffSchedItem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("StaffSchedItem");

                entity.HasComment("Employees workday");

                entity.HasIndex(e => new { e.StaffSched, e.Weekday, e.FromHour, e.FromMinutes }, "IX_StaffSchedItem");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOStaffSched.Item.Cods (0.Standard, 1.Intensive)");

                entity.Property(e => e.FromHour).HasComment("Hour when the workday starts");

                entity.Property(e => e.FromMinutes).HasComment("Minute when the workday starts");

                entity.Property(e => e.StaffSched).HasComment("Foreign key for parent StaffSched table");

                entity.Property(e => e.ToHour).HasComment("Hour when the workday ends");

                entity.Property(e => e.ToMinutes).HasComment("Minute when the workday ends");

                entity.Property(e => e.Weekday).HasComment("Day of the week, starting with 0.Sunday");

                entity.HasOne(d => d.StaffSchedNavigation)
                    .WithMany(p => p.StaffSchedItems)
                    .HasForeignKey(d => d.StaffSched)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffSchedItem_StaffSched");
            });

            modelBuilder.Entity<Stp>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_STP");

                entity.ToTable("Stp");

                entity.HasComment("Product categories for each commercial brand");

                entity.HasIndex(e => new { e.Brand, e.Obsoleto, e.Ord }, "IX_STP_Brand_Obsoleto_Ord");

                entity.HasIndex(e => e.Codi, "IX_STP_Codi");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Brand).HasComment("Commercial brand to which the category belongs. Foreign key to Stp table");

                entity.Property(e => e.CnapGuid).HasComment("Product classification (Clasificación Normalizada de Articulos de Puericultura). Foreign key to Cnap table");

                entity.Property(e => e.Codi).HasComment("0.Standard product, 1.Accessory, 2.Spare part, 3.POS Marketing materials, 4.Others");

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('00000000')")
                    .IsFixedLength()
                    .HasComment("Default Customs code for products in this category");

                entity.Property(e => e.Color)
                    .HasDefaultValueSql("((-1))")
                    .HasComment("Color to graphically identify the category on charts");

                entity.Property(e => e.DimensionH)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Default height, if any, in mm (packaging included)");

                entity.Property(e => e.DimensionL)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Default length, if any, in mm (packaging included)");

                entity.Property(e => e.DimensionW)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Default width, if any, in mm (packaging included)");

                entity.Property(e => e.DscPropagateToChildren)
                    .HasColumnName("Dsc_PropagateToChildren")
                    .HasComment("True if new products from this category should inherit tha category excerpt and description");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time when this category was registered");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Last date and time when this category was edited");

                entity.Property(e => e.ForzarInnerPack).HasComment("If true, orders are required to be multiple of InnerPack field value");

                entity.Property(e => e.HideUntil)
                    .HasColumnType("date")
                    .HasComment("If not null, the category will remain hidden until disclosure date");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasComment("Category image (410x410 pixels)");

                entity.Property(e => e.InnerPack).HasComment("Quantity of items per package");

                entity.Property(e => e.IsBundle).HasComment("True if products under this category are just an administrative group of other products");

                entity.Property(e => e.Kg)
                    .HasColumnType("numeric(6, 3)")
                    .HasComment("Default gross weight, if any, in Kg (includes packaging)");

                entity.Property(e => e.KgNet)
                    .HasColumnType("numeric(6, 3)")
                    .HasComment("Default net weight, if any, in Kg");

                entity.Property(e => e.LaunchMonth).HasComment("Month when this category was launched to the market");

                entity.Property(e => e.LaunchYear).HasComment("Year when this category products were or are expected to be launched to the market");

                entity.Property(e => e.M3)
                    .HasColumnType("numeric(6, 3)")
                    .HasComment("Default volume of this category products, if any, in cubic meters, packaging included");

                entity.Property(e => e.MadeIn).HasComment("Default country of Origin. Foreign key to Country table");

                entity.Property(e => e.NoDimensions).HasComment("True when physical dimensions are not appliable to this category products (services...)");

                entity.Property(e => e.NoStk).HasComment("True if inventory not applicable (services...)");

                entity.Property(e => e.Obsoleto).HasComment("True if the category is no longer active");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((999))")
                    .HasComment("Index where to display the category when sorted order applies");

                entity.Property(e => e.OuterPack).HasComment("Quantity of items per master package");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("image")
                    .HasComment("Thumbnail to ilustrate the category (150px)");

                entity.Property(e => e.WebEnabledConsumer)
                    .HasColumnName("Web_Enabled_Consumer")
                    .HasComment("True if the category should be displayed to consumers (on web site...)");

                entity.Property(e => e.WebEnabledPro)
                    .HasColumnName("Web_Enabled_Pro")
                    .HasComment("True if the category should be visible to professionals");

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.Stps)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STP_TPA");

                entity.HasOne(d => d.CnapGu)
                    .WithMany(p => p.Stps)
                    .HasForeignKey(d => d.CnapGuid)
                    .HasConstraintName("FK_Stp_Cnap");

                entity.HasOne(d => d.CodiMercanciaNavigation)
                    .WithMany(p => p.Stps)
                    .HasForeignKey(d => d.CodiMercancia)
                    .HasConstraintName("FK_Stp_CodisMercancia");

                entity.HasOne(d => d.MadeInNavigation)
                    .WithMany(p => p.Stps)
                    .HasForeignKey(d => d.MadeIn)
                    .HasConstraintName("FK_Stp_Country");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Task");

                entity.HasComment("Automatic tasks run at specific schedules by MatSched Windows service");

                entity.HasIndex(e => e.Cod, "IX_Task_Cod");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOTask.Cods to call a task programmatically");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Task description");

                entity.Property(e => e.Enabled).HasComment("If false, the task will not run");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Task name");

                entity.Property(e => e.NotAfter).HasComment("If not null, the task will no longer run after this date");

                entity.Property(e => e.NotBefore).HasComment("If not null, the task won't run until this date");
            });

            modelBuilder.Entity<TaskLog>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("TaskLog");

                entity.HasComment("Logs event results from tasks run at Matsched Windows Service");

                entity.HasIndex(e => new { e.Task, e.Fch }, "IX_TaskLog_TaskFch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Fch)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Event date and time");

                entity.Property(e => e.ResultCod).HasComment("Enumerable DTOTask.ResultCods (0.Running, 1.Success, 2.Empty, 3.DoneWithErrors, 4.Failed)");

                entity.Property(e => e.ResultMsg)
                    .HasColumnType("text")
                    .HasComment("Optional text describing the result");

                entity.Property(e => e.Task).HasComment("Foreign key to parent table Task");

                entity.HasOne(d => d.TaskNavigation)
                    .WithMany(p => p.TaskLogs)
                    .HasForeignKey(d => d.Task)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskLog_Task");
            });

            modelBuilder.Entity<TaskSchedule>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("TaskSchedule");

                entity.HasComment("Schedules used by Matsched Windows Service to run automated tasks");

                entity.HasIndex(e => new { e.Task, e.Weekdays, e.Hour, e.Minute }, "IX_TaskSchedule_Task");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Enabled).HasComment("If false this scheduled will be skipped");

                entity.Property(e => e.Hour).HasComment("If mode is set to DTOTaskSchedule.Modes.GivenTime, the task will run each active date at time set at Hour and Minutes fields");

                entity.Property(e => e.Interval).HasComment("If mode is set to DTOTaskSchedules.Modes.Interval, the task will run every number of minutes set at this field");

                entity.Property(e => e.Minute).HasComment("If mode is set to DTOTaskSchedule.Modes.GivenTime, the task will run each active date at time set at Hour and Minutes fields");

                entity.Property(e => e.Mode).HasComment("Enumerable DTOTaskSchedules.Modes (0.GivenTime, 1.Interval)");

                entity.Property(e => e.Task).HasComment("Parent Task table");

                entity.Property(e => e.TimeInterval)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasComment("ISO8601 Duration time interval");

                entity.Property(e => e.Weekdays)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0000000')")
                    .IsFixedLength()
                    .HasComment("Seven digits one or zero to enable or disable weekdays starting with Sunday");

                entity.HasOne(d => d.TaskNavigation)
                    .WithMany(p => p.TaskSchedules)
                    .HasForeignKey(d => d.Task)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskSchedule_Task");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Tax");

                entity.HasComment("Taxes");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Codi).HasComment("Enumerable DTOTax.Codis (VAT, Irpf...)");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Start date");

                entity.Property(e => e.Tipus)
                    .HasColumnType("decimal(4, 2)")
                    .HasComment("Tax rate");
            });

            modelBuilder.Entity<Tmp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmp");

                entity.Property(e => e.PesBrut)
                    .HasMaxLength(255)
                    .HasColumnName("Pes Brut");

                entity.Property(e => e.PesNet)
                    .HasMaxLength(255)
                    .HasColumnName("Pes Net");

                entity.Property(e => e.Sku)
                    .HasMaxLength(255)
                    .HasColumnName("SKU");

                entity.Property(e => e.UnitatsEmbalatgeTransport)
                    .HasMaxLength(255)
                    .HasColumnName("Unitats/embalatge transport");
            });

            modelBuilder.Entity<TmpInspeccioConsum2022>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TmpInspeccioConsum2022");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<TmpInspeccioRef>(entity =>
            {
                entity.HasKey(e => e.Ref);

                entity.Property(e => e.Ref)
                    .ValueGeneratedNever()
                    .HasColumnName("REF");
            });

            modelBuilder.Entity<Tmpinspecciorefs2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tmpinspecciorefs2");

                entity.Property(e => e.Compra)
                    .HasColumnType("datetime")
                    .HasColumnName("COMPRA");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("COST");

                entity.Property(e => e.Ref).HasColumnName("REF");

                entity.Property(e => e.Stock).HasColumnName("STOCK");
            });

            modelBuilder.Entity<Tpa>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_TPA_1");

                entity.ToTable("Tpa");

                entity.HasComment("Commercial brands");

                entity.HasIndex(e => new { e.Emp, e.Obsoleto, e.Ord }, "IX_TPA_Emp_Ord");

                entity.HasIndex(e => e.Proveidor, "IX_Tpa_Proveidor");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CnapGuid).HasComment("Foreign key to Cnap product classification table (Codificación Normalizada para Articulos de Puericultura)");

                entity.Property(e => e.CodDist).HasComment("Distribution policy: 0.Free (any sale point is allowed to distribute) 1.Restricted (distribution is limited to selected sale points)");

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Default Customs code (NC, Nomenclatura Combinada) for products of this brand, if any");

                entity.Property(e => e.Color)
                    .HasDefaultValueSql("((-1))")
                    .HasComment("Color to graphically identify the brand on charts");

                entity.Property(e => e.Emp).HasComment("Company Id, Foreign key to Emp table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Last date and time this entry was edited");

                entity.Property(e => e.IncludeDeptOnUrl).HasComment("If true, url is built as brand/department/category/sku... else as brand/category/sku...");

                entity.Property(e => e.Logo)
                    .HasColumnType("image")
                    .HasComment("Brand logo 150x48px");

                entity.Property(e => e.LogoDistribuidorOficial)
                    .HasColumnType("image")
                    .HasComment("Official brand logo each distributor is allowed to publish on their website");

                entity.Property(e => e.MadeIn).HasComment("Country of Origin. Foreign key to Country table");

                entity.Property(e => e.Obsoleto).HasComment("true if the brand is no longer active");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((99))")
                    .HasComment("Sort order in which to display the brand among the rest of the brands");

                entity.Property(e => e.Proveidor).HasComment("Provider Id; foreign key to CliPrv table");

                entity.Property(e => e.ShowAtlas)
                    .HasDefaultValueSql("((1))")
                    .HasComment("True if allowed to display to consumers the list of official sale points for products of this brand");

                entity.Property(e => e.WebAtlasDeadline)
                    .HasDefaultValueSql("((60))")
                    .HasComment(" Max Days from last order to be published as distributor");

                entity.Property(e => e.WebAtlasRafflesDeadline).HasComment("Max Days from last order to be published as raffle sale point");

                entity.Property(e => e.WebEnabledConsumer)
                    .HasColumnName("Web_Enabled_Consumer")
                    .HasComment("True if allowed to be displayed to consumers (on web site...)");

                entity.Property(e => e.WebEnabledPro)
                    .HasColumnName("Web_Enabled_Pro")
                    .HasComment("True if allowed to be displayed to proffessionals (on price lists, order forms...)");

                entity.HasOne(d => d.CnapGu)
                    .WithMany(p => p.Tpas)
                    .HasForeignKey(d => d.CnapGuid)
                    .HasConstraintName("FK_Tpa_Cnap");

                entity.HasOne(d => d.CodiMercanciaNavigation)
                    .WithMany(p => p.Tpas)
                    .HasForeignKey(d => d.CodiMercancia)
                    .HasConstraintName("FK_Tpa_CodisMercancia");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Tpas)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tpa_Emp");

                entity.HasOne(d => d.MadeInNavigation)
                    .WithMany(p => p.Tpas)
                    .HasForeignKey(d => d.MadeIn)
                    .HasConstraintName("FK_Tpa_Country");

                entity.HasOne(d => d.ProveidorNavigation)
                    .WithMany(p => p.Tpas)
                    .HasForeignKey(d => d.Proveidor)
                    .HasConstraintName("FK_Tpa_CliPrv");
            });

            modelBuilder.Entity<TpvLog>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("TpvLog");

                entity.HasComment("Log message sent by bank online payments gateway when a customer tries to issue a payment. Info available at https://canales.redsys.es/canales/ayuda/documentacion/Manual%20integracion%20para%20conexion%20por%20Web%20Service.pdf");

                entity.HasIndex(e => e.DsOrder, "Ix_TpvLog_Ds_Order")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CcaGuid).HasComment("Accounts entry on payment success. Foreign key for Cca table");

                entity.Property(e => e.DsAmount)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Amount")
                    .HasComment("Payment amount with no decimal point (the amount should be devided by 100 to get Euros)");

                entity.Property(e => e.DsAuthorisationCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Ds_AuthorisationCode")
                    .HasComment("If authorisation is approved, gateway service returns a numerical code");

                entity.Property(e => e.DsCardCountry)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Card_Country")
                    .HasComment("Enumerable (724.Spain)");

                entity.Property(e => e.DsCardType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Card_Type")
                    .IsFixedLength()
                    .HasComment("Enumerable (C.Credit, D.Debit)");

                entity.Property(e => e.DsConsumerLanguage)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ds_ConsumerLanguage")
                    .HasComment("Enumerable (001.Spanish)");

                entity.Property(e => e.DsCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Currency")
                    .HasComment("Payment currency code (978.Euros)");

                entity.Property(e => e.DsDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Date")
                    .HasComment("Payment date");

                entity.Property(e => e.DsHour)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Hour")
                    .HasComment("Payment hour, format HH:mm");

                entity.Property(e => e.DsMerchantCode)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("Ds_MerchantCode")
                    .HasComment("We are the merchant; merchant code is the code assigned to our account by the gateway service");

                entity.Property(e => e.DsMerchantData)
                    .HasColumnType("text")
                    .HasColumnName("Ds_MerchantData")
                    .HasComment("Custom data we include on the request which is returned back on the gateway response to identify the request it refers to. It includes on an encoded string the user issuing the payment and the payment object (purchase order, delivery note, unpayment cancellation...)");

                entity.Property(e => e.DsMerchantParameters)
                    .HasColumnType("text")
                    .HasColumnName("Ds_MerchantParameters")
                    .HasComment("Encoded request data sent to the gateway");

                entity.Property(e => e.DsOrder)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Order")
                    .HasComment("Unique number identifying the operation on gateway database. 12 alphanumeric chars, the first 4 characters must be numeric. Since year 2016 all our Ds_Order numbers start with \"9999A\" followed by a consecutive number of 7 digits.");

                entity.Property(e => e.DsProductDescription)
                    .HasColumnType("text")
                    .HasColumnName("Ds_ProductDescription")
                    .HasComment("Free text payment concept");

                entity.Property(e => e.DsResponse)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Response")
                    .HasComment("Encrypted esponse sent by te gateway with the result of the operation");

                entity.Property(e => e.DsSecurePayment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ds_SecurePayment")
                    .HasComment("Enumerable (0. Non secure payment, 1.Secure payment)");

                entity.Property(e => e.DsSignature)
                    .HasColumnType("text")
                    .HasColumnName("Ds_Signature")
                    .HasComment("Signature of the payment request sent  to the gateway");

                entity.Property(e => e.DsSignatureReceived)
                    .HasColumnType("text")
                    .HasColumnName("Ds_SignatureReceived")
                    .HasComment("Encoded response received from the gateway upon operation completed");

                entity.Property(e => e.DsTerminal)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ds_Terminal")
                    .HasComment("We might operate with different terminal accounts for different purposes. This is not currently the case, so value for this field is always 1");

                entity.Property(e => e.DsTransactionType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Ds_TransactionType")
                    .HasComment("Type of transaction; always 0");

                entity.Property(e => e.Exceptions)
                    .HasColumnType("text")
                    .HasComment("Multiline exceptions descriptions, if any");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the log was created");

                entity.Property(e => e.Mode).HasComment("Payment object. Enumerable (1.Free, 2.Delivery note, 3.Purchase Order, 4.Unpayment cancellation)");

                entity.Property(e => e.ProcessedSuccessfully).HasComment("True if the full operation completed successfully, including target object updates");

                entity.Property(e => e.Request).HasComment("Depending on Mode field value, foreign key to the Delivery note Alb table, the Purchase Order Pdc table or the Unpayments Impagats table");

                entity.Property(e => e.SignatureValidated).HasComment("True if signature received could be validated using the PaymentGateway.SignatureKey");

                entity.Property(e => e.Titular).HasComment("Customer issuing the payment; foreign key to CliGral table");

                entity.Property(e => e.User).HasComment("User issuing the payment. Foreign key to Email table");
            });

            modelBuilder.Entity<Tracking>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Tracking");

                entity.HasComment("Events tracking for reports to customers for example about after sales service  status");

                entity.HasIndex(e => new { e.Target, e.FchCreated }, "IDX_Tracking_TargetFchCreated");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cod).HasComment("Foreign key for Cod table describing the event tracked");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Event date");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Target).HasComment("Target object, for example an after sales service tracking would be a foreign key for Incidencies table");

                entity.Property(e => e.UsrCreated).HasComment("User triggering the event");

                entity.HasOne(d => d.CodNavigation)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.Cod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tracking_Cod");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.Trackings)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tracking_UsrCreated");
            });

            modelBuilder.Entity<Transm>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_TRANSM");

                entity.ToTable("Transm");

                entity.HasComment("Sets of deliveries sent to the logistic center to prepare picking and packing");

                entity.HasIndex(e => new { e.Transm1, e.Fch }, "IX_Transm_Id_Fch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Albs).HasComment("Number of delivery notes included");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company; foreign key for Emp table");

                entity.Property(e => e.Eur)
                    .HasColumnType("money")
                    .HasComment("Total amount in Euros");

                entity.Property(e => e.Fch).HasComment("Date");

                entity.Property(e => e.MgzGuid).HasComment("Logistic center preparing the deliveries; foreign key for CliGral table");

                entity.Property(e => e.Transm1)
                    .HasColumnName("Transm")
                    .HasComment("Sequential number, unique for each Company/year combination");

                entity.Property(e => e.Val)
                    .HasColumnType("money")
                    .HasComment("Total amount, foreign curreny");

                entity.Property(e => e.Yea).HasComment("Creation year");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Transms)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transm_Transm");

                entity.HasOne(d => d.MgzGu)
                    .WithMany(p => p.Transms)
                    .HasForeignKey(d => d.MgzGuid)
                    .HasConstraintName("FK_Transm_CliGral");
            });

            modelBuilder.Entity<Trp>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_Trp_1");

                entity.ToTable("Trp");

                entity.HasComment("Transport companies");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key; foreign key for CliGral table");

                entity.Property(e => e.Abr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Cubicaje).HasComment("chargeable weight / m3");

                entity.Property(e => e.TrackingUrlTemplate)
                    .HasColumnType("text")
                    .HasComment("Template url for delivery tracking");

                entity.HasOne(d => d.Gu)
                    .WithOne(p => p.Trp)
                    .HasForeignKey<Trp>(d => d.Guid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trp_CliGral");
            });

            modelBuilder.Entity<Tutorial>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Tutorial");

                entity.HasComment("Tutorials to use certain processes");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Excerpt)
                    .HasColumnType("text")
                    .HasComment("Brief description about what is the tutorial about");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date the tutorial was launched");

                entity.Property(e => e.Parent).HasComment("Subject; foreign key to parent table TutorialSubject");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Tutorial title");

                entity.Property(e => e.YoutubeId)
                    .HasColumnType("text")
                    .HasComment("Id required to browse the video in youtube");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.Tutorials)
                    .HasForeignKey(d => d.Parent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tutorial_TutorialSubject");
            });

            modelBuilder.Entity<TutorialRol>(entity =>
            {
                entity.HasKey(e => new { e.Tutorial, e.Rol });

                entity.ToTable("TutorialRol");

                entity.HasComment("User rols target for this tutorial");

                entity.Property(e => e.Tutorial).HasComment("Foreign key for Tutorial table");

                entity.Property(e => e.Rol).HasComment("User rol; foreign key for UsrRols");

                entity.HasOne(d => d.TutorialNavigation)
                    .WithMany(p => p.TutorialRols)
                    .HasForeignKey(d => d.Tutorial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TutorialRol_Tutorial");
            });

            modelBuilder.Entity<TutorialSubject>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("TutorialSubject");

                entity.HasComment("Groups different tutorials into subjects");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Subject title");
            });

            modelBuilder.Entity<Txt>(entity =>
            {
                entity.ToTable("Txt");

                entity.HasComment("Text resources in 4 languages accessible programmatically by an integer key");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Enumerable DTOTxt.Ids to access the resource programmatically");

                entity.Property(e => e.Cat)
                    .HasColumnType("text")
                    .HasComment("Resource text in Catalan");

                entity.Property(e => e.Eng)
                    .HasColumnType("text")
                    .HasComment("Resource text in English");

                entity.Property(e => e.Esp)
                    .HasColumnType("text")
                    .HasComment("Resource text in Spanish");

                entity.Property(e => e.Por)
                    .HasColumnType("text")
                    .HasComment("Resource text in Portuguese");
            });

            modelBuilder.Entity<UrlSegment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UrlSegment");

                entity.HasComment("Product url segments");

                entity.HasIndex(e => e.Segment, "Ix_UrlAlt_UrlSegmenty")
                    .IsClustered();

                entity.HasIndex(e => e.Target, "Ix_UrlSegment_Target");

                entity.Property(e => e.Canonical).HasComment("True if this is the right segment for canonic Url");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code (3 letters)");

                entity.Property(e => e.Segment)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Url segment");

                entity.Property(e => e.Target)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Product; foreign key to either brand Tpa table, category Stp table, sku Art table");
            });

            modelBuilder.Entity<UserDefault>(entity =>
            {
                entity.HasKey(e => new { e.UserGuid, e.Cod });

                entity.ToTable("UserDefault");

                entity.HasComment("User default values");

                entity.Property(e => e.UserGuid).HasComment("User; foreign key for Email table");

                entity.Property(e => e.Cod).HasComment("Purpose of the value; Enumerable DTOUserDefault.Cods");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("User value for this purpose code");

                entity.HasOne(d => d.UserGu)
                    .WithMany(p => p.UserDefaults)
                    .HasForeignKey(d => d.UserGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDefault_Email");
            });

            modelBuilder.Entity<UsrRol>(entity =>
            {
                entity.HasKey(e => e.Rol)
                    .IsClustered(false);

                entity.HasComment("User rol names");

                entity.Property(e => e.Rol)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Dsc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Rol features description");

                entity.Property(e => e.Nom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Name, Spanish language");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nom_CAT")
                    .HasDefaultValueSql("('')")
                    .HasComment("Name, Catalan language");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nom_ENG")
                    .HasDefaultValueSql("('')")
                    .HasComment("Name, English language");

                entity.Property(e => e.NomPor)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nom_POR")
                    .HasComment("Name, Portuguese language");

                entity.HasMany(d => d.WebMenuItems)
                    .WithMany(p => p.Rols)
                    .UsingEntity<Dictionary<string, object>>(
                        "WebMenuItemsxRol",
                        l => l.HasOne<WebMenuItem>().WithMany().HasForeignKey("WebMenuItem").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_WebMenuItemsxRol_WebMenuItems"),
                        r => r.HasOne<UsrRol>().WithMany().HasForeignKey("Rol").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_WebMenuItemsxRol_UsrRols"),
                        j =>
                        {
                            j.HasKey("Rol", "WebMenuItem");

                            j.ToTable("WebMenuItemsxRol").HasComment("website menu items per user rol");

                            j.IndexerProperty<int>("Rol").HasComment("Foreign key for UsrRols table");

                            j.IndexerProperty<Guid>("WebMenuItem").HasComment("Foreign key for WebMenuItems table");
                        });
            });

            modelBuilder.Entity<UsrSession>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("UsrSession");

                entity.HasComment("User sessions on our corporate Apps and websites");

                entity.HasIndex(e => new { e.Guid, e.Usuari }, "IX_UsrSession_FchFrom");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.AppType).HasComment("Enumerable DTOApp.AppTypes");

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Application version the user has initiated session in");

                entity.Property(e => e.Culture)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("System.Globalization.CultureInfo.name");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Init session date and time");

                entity.Property(e => e.FchTo)
                    .HasColumnType("datetime")
                    .HasComment("End session date and time");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("ISO 639-1 user language code");

                entity.Property(e => e.Usuari).HasComment("Session user; foreign key for Email table");

                entity.HasOne(d => d.UsuariNavigation)
                    .WithMany(p => p.UsrSessions)
                    .HasForeignKey(d => d.Usuari)
                    .HasConstraintName("FK_UsrSession_Email");
            });

            modelBuilder.Entity<VehicleFlotum>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_VEHICLE_FLOTA_1");

                entity.ToTable("Vehicle_Flota");

                entity.HasComment("Vehicles owned by the company");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Alta)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date of acquisition");

                entity.Property(e => e.Baixa)
                    .HasColumnType("datetime")
                    .HasComment("Date of removal from inventory");

                entity.Property(e => e.Bastidor)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasComment("Vehicle frame number");

                entity.Property(e => e.ConductorGuid).HasComment("Driver, fhoreign key for CliGral table");

                entity.Property(e => e.Contracte).HasComment("Acquisition contract, foreign key from Contracts table");

                entity.Property(e => e.Emp).HasComment("Company, foreign key for Emp table");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasComment("Picture of the vehicle");

                entity.Property(e => e.Insurance).HasComment("Insurance policy, foreign key to Contracts table");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Vehicle plate number");

                entity.Property(e => e.ModelGuid).HasComment("Vehicle model, foreign key for Vehicle_Models table");

                entity.Property(e => e.Privat).HasComment("True if private owned");

                entity.Property(e => e.VenedorGuid).HasComment("Company the vehicle was purchased to, foreign key to CliGral table");

                entity.HasOne(d => d.ConductorGu)
                    .WithMany(p => p.VehicleFlotumConductorGus)
                    .HasForeignKey(d => d.ConductorGuid)
                    .HasConstraintName("FK_Vehicle_Flota_Driver");

                entity.HasOne(d => d.ContracteNavigation)
                    .WithMany(p => p.VehicleFlotumContracteNavigations)
                    .HasForeignKey(d => d.Contracte)
                    .HasConstraintName("FK_Vehicle_Flota_Contracte");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.VehicleFlota)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_Flota_Emp");

                entity.HasOne(d => d.InsuranceNavigation)
                    .WithMany(p => p.VehicleFlotumInsuranceNavigations)
                    .HasForeignKey(d => d.Insurance)
                    .HasConstraintName("FK_Vehicle_Flota_Insurance");

                entity.HasOne(d => d.ModelGu)
                    .WithMany(p => p.VehicleFlota)
                    .HasForeignKey(d => d.ModelGuid)
                    .HasConstraintName("FK_Vehicle_Flota_Vehicle_Models");

                entity.HasOne(d => d.VenedorGu)
                    .WithMany(p => p.VehicleFlotumVenedorGus)
                    .HasForeignKey(d => d.VenedorGuid)
                    .HasConstraintName("FK_Vehicle_Flota_Venedor");
            });

            modelBuilder.Entity<VehicleMarca>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_VEHICLE_MARCAS");

                entity.ToTable("Vehicle_Marcas");

                entity.HasComment("Vehicle commercial brands");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary kley");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("''");
            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_Vehicle_Models_1");

                entity.ToTable("Vehicle_Models");

                entity.HasComment("Vehicle brand commercial models");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.MarcaGuid).HasComment("Vehicle brand, foreign key to Vehicle_Marcas");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Brand model name");

                entity.HasOne(d => d.MarcaGu)
                    .WithMany(p => p.VehicleModels)
                    .HasForeignKey(d => d.MarcaGuid)
                    .HasConstraintName("FK_Vehicle_Models_Vehicle_Marcas");
            });

            modelBuilder.Entity<VisaCard>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_CliVisa");

                entity.ToTable("VisaCard");

                entity.HasComment("Employees credit cards");

                entity.HasIndex(e => new { e.Titular, e.Caduca, e.FchCanceled }, "IX_CliVisa_Contact");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Friendly name");

                entity.Property(e => e.Banc).HasComment("Bank issuer; foreign key for CliBnc table");

                entity.Property(e => e.Caduca)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0000')")
                    .IsFixedLength()
                    .HasComment("Expiration date");

                entity.Property(e => e.Ccid)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CCID")
                    .HasComment("Safety code");

                entity.Property(e => e.Digits)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1111222233334444')")
                    .IsFixedLength()
                    .HasComment("Credit card number");

                entity.Property(e => e.Emisor).HasComment("Issuer company");

                entity.Property(e => e.FchCanceled)
                    .HasColumnType("date")
                    .HasComment("Cancelation date, if any");

                entity.Property(e => e.Limit)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Credit limit");

                entity.Property(e => e.Titular).HasComment("Employee. Foreign key for CliGral table");

                entity.HasOne(d => d.BancNavigation)
                    .WithMany(p => p.VisaCards)
                    .HasForeignKey(d => d.Banc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisaCard_CliBnc");

                entity.HasOne(d => d.EmisorNavigation)
                    .WithMany(p => p.VisaCards)
                    .HasForeignKey(d => d.Emisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CliVisa_VISA");

                entity.HasOne(d => d.TitularNavigation)
                    .WithMany(p => p.VisaCards)
                    .HasForeignKey(d => d.Titular)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VisaCard_CliGral");
            });

            modelBuilder.Entity<VisaEmisor>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_VISA");

                entity.ToTable("VisaEmisor");

                entity.HasComment("Credit card issuers");

                entity.HasIndex(e => e.Nom, "IX_VisaEmisor_Nom")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Img)
                    .HasColumnType("image")
                    .HasComment("Logo");

                entity.Property(e => e.Nom)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Friendly name");
            });

            modelBuilder.Entity<VwAddress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAddress");

                entity.HasComment("Filters view VwAddressBase by official corporate address for each contact");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("adr")
                    .HasComment("Address purpose: Fiscal (1), Correspondencia (2), Entregas (3), Fra.Consumidor (4)");

                entity.Property(e => e.AdrNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adrNum")
                    .HasComment("Street number or any detail to identify the location within the street");

                entity.Property(e => e.AdrPis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adrPis")
                    .HasComment("Floor number or any detail to identify the location within the building");

                entity.Property(e => e.AdrViaNom)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adrViaNom")
                    .HasComment("Name of the street or road");

                entity.Property(e => e.Cee)
                    .HasColumnName("CEE")
                    .HasComment("True if the location belongs to European Community");

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasComment("Always value 1 meaning official corporate address");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Catalan country name");

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("English country name");

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Spanish country name");

                entity.Property(e => e.CountryGuid).HasComment("Foreign key to Country table");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code (2 letters)");

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Portuguese country name");

                entity.Property(e => e.ExportCod).HasComment("Code for Custom purposes: 1:National, 2:CEE, 3:Rest of the world");

                entity.Property(e => e.Geo).HasComment("Geographical data to compute distances from other points");

                entity.Property(e => e.Latitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.LocationGuid).HasComment("Foreign key to Location table");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the city or town");

                entity.Property(e => e.Longitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("International Dilaing Code");

                entity.Property(e => e.ProvinciaGuid).HasComment("Foreign key to Provincia table");

                entity.Property(e => e.ProvinciaIntrastat)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the province");

                entity.Property(e => e.RegioGuid).HasComment("Foreign key to Region table");

                entity.Property(e => e.RegioNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the Region or Comunidad Autónoma");

                entity.Property(e => e.SplitByComarcas).HasComment("Wether Zones may be split into Comarcas");

                entity.Property(e => e.SrcGuid).HasComment("Contact identifier whom the address belongs");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Postal code");

                entity.Property(e => e.ZipGuid).HasComment("Foreign key to Zip table (postal codes)");

                entity.Property(e => e.ZonaGuid).HasComment("Foreign key to Zona table");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Zona name");
            });

            modelBuilder.Entity<VwAddressBase>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAddressBase");

                entity.HasComment("Returns full address joining all geographical area related tables");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("adr")
                    .HasComment("Street name and number, the way it should be written locally");

                entity.Property(e => e.AdrNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adrNum")
                    .HasComment("Street number or any detail to identify the building within the street");

                entity.Property(e => e.AdrPis)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adrPis")
                    .HasComment("Floor number or any detail not relevant to Google maps to identify the location within the building");

                entity.Property(e => e.AdrViaNom)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("adrViaNom")
                    .HasComment("Name of the street or road");

                entity.Property(e => e.Cee)
                    .HasColumnName("CEE")
                    .HasComment("True if the country belongs to European Community");

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasComment("Address purpose: Fiscal (1), Correspondencia (2), Entregas (3), Fra.Consumidor (4)");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Catalan country name");

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("English country name");

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Spanish country name");

                entity.Property(e => e.CountryGuid).HasComment("Foreign key to Country table");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code");

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Portuguese country name");

                entity.Property(e => e.ExportCod).HasComment("For Customs purposes: 1:National, 2:Europe, 3:Rest of the world");

                entity.Property(e => e.Geo).HasComment("Geographic data to compute distances from other points");

                entity.Property(e => e.Latitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.LocationGuid).HasComment("Foreign key to Location table");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the city or town");

                entity.Property(e => e.Longitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("International dialing code");

                entity.Property(e => e.ProvinciaGuid).HasComment("Foreign key to Provincia table, if any");

                entity.Property(e => e.ProvinciaIntrastat)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the province, if any");

                entity.Property(e => e.RegioGuid).HasComment("Foreign key to Region table, if any");

                entity.Property(e => e.RegioNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Region name. A country is split into regions, a region is split into provinces");

                entity.Property(e => e.SplitByComarcas).HasComment("A country zone may be split into Comarcas; each location may belong to a Comarca");

                entity.Property(e => e.SrcGuid).HasComment("Contact identifier whom the address belongs");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Postal code");

                entity.Property(e => e.ZipGuid).HasComment("Foreign key to Zip table");

                entity.Property(e => e.ZonaGuid).HasComment("Foreign key to Zona table");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the Zona or area inside a country");
            });

            modelBuilder.Entity<VwAppUsr>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAppUsrs");

                entity.HasComment("For each combination of user, device and App, shows app version and interval it has been logged in");

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceModel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstFch).HasColumnType("datetime");

                entity.Property(e => e.LastFch).HasColumnType("datetime");

                entity.Property(e => e.Usr)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAppUsrLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAppUsrLog");

                entity.Property(e => e.Adr)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AppVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceId).HasColumnType("text");

                entity.Property(e => e.DeviceModel).HasColumnType("text");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.FchTo).HasColumnType("datetime");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Os)
                    .HasColumnType("text")
                    .HasColumnName("OS");
            });

            modelBuilder.Entity<VwArea>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwArea");

                entity.HasComment("Returns area name from its Id regardless it's a Country, Zone or Location");

                entity.Property(e => e.Cod).HasComment("Area code: 1:Country, 2:Zona, 3:Location");

                entity.Property(e => e.Guid).HasComment("Primary key, either from a Country, Zona or Location table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the area");
            });

            modelBuilder.Entity<VwAreaNom>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAreaNom");

                entity.HasComment("Returns full details for each area, regardless it is a Country, Zona, Location or Zip");

                entity.Property(e => e.AreaCod).HasComment("1:Country, 2:Zona, 3:Location, 4:Zip");

                entity.Property(e => e.CountryGuid).HasComment("Foreign key for Country table");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code");

                entity.Property(e => e.CountryNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Catalan country name");

                entity.Property(e => e.CountryNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("English country name");

                entity.Property(e => e.CountryNomEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Spanish country name");

                entity.Property(e => e.CountryNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Portuguese country name");

                entity.Property(e => e.Guid).HasComment("Primary key");

                entity.Property(e => e.LocationGuid).HasComment("Foreign key to Location table");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the city or town");

                entity.Property(e => e.Provincia).HasComment("Foreign key to Provincia table");

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the province");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Postal code");

                entity.Property(e => e.ZipGuid).HasComment("Foreign key to Zip table");

                entity.Property(e => e.ZonaGuid).HasComment("Foreign key to Zona table");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Zona name");
            });

            modelBuilder.Entity<VwAreaParent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAreaParent");

                entity.HasComment("Relates areas with its parent areas and children areas");

                entity.Property(e => e.ChildCod).HasComment("Area code for child element: 1:Country, 2:Zona, 3:Location, 4:Zip");

                entity.Property(e => e.ChildGuid).HasComment("Foreign key for child element to an area table depending on Child code");

                entity.Property(e => e.ParentCod).HasComment("Area code for parent element: 1:Country, 2:Zona, 3:Location, 4:Zip");

                entity.Property(e => e.ParentGuid).HasComment("Foreign key for parent element to an area table depending on ParentCod field value");
            });

            modelBuilder.Entity<VwAtla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAtlas");

                entity.HasComment("All contact addresses");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAtlasRep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAtlasRep");

                entity.HasComment("Customer list for each commercial agent");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.User).HasColumnName("user");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAtlasSalesManager>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAtlasSalesManager");

                entity.HasComment("Customer list for each sales manager");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.User).HasColumnName("user");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBalance");

                entity.HasComment("Account balance and Profit&loss reports");

                entity.Property(e => e.ClassNomCat)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ClassNomEng)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ClassNomEsp)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CtaCat)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaEng)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaEsp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Emp).HasColumnName("emp");

                entity.Property(e => e.Eur).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.YearMonth)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwBancsSdo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBancsSdos");

                entity.Property(e => e.Abr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.IbanCcc)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Sdo).HasColumnType("decimal(12, 2)");
            });

            modelBuilder.Entity<VwBank>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBank");

                entity.HasComment("List of bank entities");

                entity.Property(e => e.BankAlias).HasMaxLength(20);

                entity.Property(e => e.BankBranchAdr).HasMaxLength(50);

                entity.Property(e => e.BankBranchCountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("BankBranchCountryISO")
                    .IsFixedLength();

                entity.Property(e => e.BankBranchLocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankBranchZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankNom).HasMaxLength(60);

                entity.Property(e => e.BankSwift)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwBrand>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBrands");

                entity.Property(e => e.Cat).IsUnicode(false);

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Eng).IsUnicode(false);

                entity.Property(e => e.Esp).IsUnicode(false);

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FchLastEdited).HasColumnType("datetime");

                entity.Property(e => e.Logo).HasColumnType("image");

                entity.Property(e => e.Por).IsUnicode(false);

                entity.Property(e => e.WebEnabledConsumer).HasColumnName("Web_Enabled_Consumer");

                entity.Property(e => e.WebEnabledPro).HasColumnName("Web_Enabled_Pro");
            });

            modelBuilder.Entity<VwBrandCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBrandCategories");

                entity.HasComment("List of all brands with their own product categories");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.BrandNomCat).IsUnicode(false);

                entity.Property(e => e.BrandNomEng).IsUnicode(false);

                entity.Property(e => e.BrandNomPor).IsUnicode(false);

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.Emp).HasColumnName("EMP");

                entity.Property(e => e.WebEnabledConsumer).HasColumnName("Web_Enabled_Consumer");
            });

            modelBuilder.Entity<VwBundleRetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBundleRetail");

                entity.HasComment("Retail price for product packs (bundles) of different components");

                entity.Property(e => e.Retail).HasColumnType("decimal(38, 14)");
            });

            modelBuilder.Entity<VwBundleStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwBundleStocks");

                entity.HasComment("Stocks available for product bundles");
            });

            modelBuilder.Entity<VwCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCategories");

                entity.Property(e => e.Cat).IsUnicode(false);

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DscPropagateToChildren).HasColumnName("Dsc_PropagateToChildren");

                entity.Property(e => e.Eng).IsUnicode(false);

                entity.Property(e => e.Esp).IsUnicode(false);

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FchLastEdited).HasColumnType("datetime");

                entity.Property(e => e.HideUntil).HasColumnType("date");

                entity.Property(e => e.Kg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.KgNet).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.M3).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.Por).IsUnicode(false);

                entity.Property(e => e.WebEnabledConsumer).HasColumnName("Web_Enabled_Consumer");

                entity.Property(e => e.WebEnabledPro).HasColumnName("Web_Enabled_Pro");
            });

            modelBuilder.Entity<VwCategoryNom>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCategoryNom");

                entity.HasComment("Product categories names");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.BrandNomCat).IsUnicode(false);

                entity.Property(e => e.BrandNomEng).IsUnicode(false);

                entity.Property(e => e.BrandNomPor).IsUnicode(false);

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.Emp).HasColumnName("EMP");

                entity.Property(e => e.WebEnabledConsumer).HasColumnName("Web_Enabled_Consumer");
            });

            modelBuilder.Entity<VwCca>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCca");

                entity.HasComment("Account entries");

                entity.Property(e => e.CtaCat)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaEng)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaEsp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Eur).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FullNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Pts).HasColumnType("money");

                entity.Property(e => e.Txt).HasMaxLength(60);

                entity.Property(e => e.UsrCreatedAdr)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsrCreatedNickname)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCca1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCcas");

                entity.Property(e => e.Eur).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Txt).HasMaxLength(60);

                entity.Property(e => e.UsrNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCcasList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCcasList");

                entity.Property(e => e.Eur).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Txt).HasMaxLength(60);

                entity.Property(e => e.UsrNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCcxOrMe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCcxOrMe");

                entity.HasComment("Customers whom to invoice deliveries to other destinations");
            });

            modelBuilder.Entity<VwChannelDto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwChannelDto");

                entity.HasComment("Default product discounts on each channel to calculate distributors price list");

                entity.Property(e => e.Dto).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Obs).HasColumnType("text");
            });

            modelBuilder.Entity<VwChannelOpenSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwChannelOpenSkus");

                entity.HasComment("Product range available per distribution channel");

                entity.Property(e => e.BrandNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwChannelSkusExcluded>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwChannelSkusExcluded");

                entity.HasComment("Product ranges excluded from distribution per channel");
            });

            modelBuilder.Entity<VwCliTpaExcludedCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCliTpaExcludedCustomers");

                entity.HasComment("Product ranges included/excluded per customer");
            });

            modelBuilder.Entity<VwCnap>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCnap");

                entity.HasComment("Product classification by functionality");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LongNomCat).IsUnicode(false);

                entity.Property(e => e.LongNomEng).IsUnicode(false);

                entity.Property(e => e.LongNomEsp).IsUnicode(false);

                entity.Property(e => e.LongNomPor).IsUnicode(false);

                entity.Property(e => e.ShortNomCat).IsUnicode(false);

                entity.Property(e => e.ShortNomEng).IsUnicode(false);

                entity.Property(e => e.ShortNomEsp).IsUnicode(false);

                entity.Property(e => e.ShortNomPor).IsUnicode(false);

                entity.Property(e => e.Tags).HasColumnType("text");
            });

            modelBuilder.Entity<VwCnapParent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCnapParent");

                entity.HasComment("Hierarchical product classification by functionality");
            });

            modelBuilder.Entity<VwCondicion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCondicions");

                entity.HasComment("Terms and conditions contents");

                entity.Property(e => e.CapitolCat)
                    .IsUnicode(false)
                    .HasComment("Chapter title, Catalan language");

                entity.Property(e => e.CapitolEng)
                    .IsUnicode(false)
                    .HasComment("Chapter title, English language");

                entity.Property(e => e.CapitolEsp)
                    .IsUnicode(false)
                    .HasComment("Chapter title, Spanish language");

                entity.Property(e => e.CapitolGuid).HasComment("Chapter id, foreign key for CondCapitol table");

                entity.Property(e => e.CapitolPor)
                    .IsUnicode(false)
                    .HasComment("Chapter title, Portuguese language");

                entity.Property(e => e.CondGuid).HasComment("Terms id, foreign key for Cond table");

                entity.Property(e => e.ExcerptCat)
                    .IsUnicode(false)
                    .HasComment("Terms excerpt, Catalan language");

                entity.Property(e => e.ExcerptEng)
                    .IsUnicode(false)
                    .HasComment("Terms excerpt, English language");

                entity.Property(e => e.ExcerptEsp)
                    .IsUnicode(false)
                    .HasComment("Terms excerpt, Spanish language");

                entity.Property(e => e.ExcerptPor)
                    .IsUnicode(false)
                    .HasComment("Terms excerpt, Portuguese language");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasComment("Creation date");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasComment("Date this record was last edited");

                entity.Property(e => e.Ord).HasComment("Chapter order");

                entity.Property(e => e.TitleCat)
                    .IsUnicode(false)
                    .HasComment("Terms title, Catalan language");

                entity.Property(e => e.TitleEng)
                    .IsUnicode(false)
                    .HasComment("Terms title, English language");

                entity.Property(e => e.TitleEsp)
                    .IsUnicode(false)
                    .HasComment("Terms title, Spanish language");

                entity.Property(e => e.TitlePor)
                    .IsUnicode(false)
                    .HasComment("Terms title, Portuguese language");

                entity.Property(e => e.TxtCat)
                    .IsUnicode(false)
                    .HasComment("Chapter content, Catalan language");

                entity.Property(e => e.TxtEng)
                    .IsUnicode(false)
                    .HasComment("Chapter content, English language");

                entity.Property(e => e.TxtEsp)
                    .IsUnicode(false)
                    .HasComment("Chapter content, Spanish language");

                entity.Property(e => e.TxtPor)
                    .IsUnicode(false)
                    .HasComment("Chapter content, Portuguese language");

                entity.Property(e => e.UsrCreated).HasComment("User who created these terms");

                entity.Property(e => e.UsrCreatedNickname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Creation user nickname");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this record for last time");

                entity.Property(e => e.UsrLastEditedNickname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Nickname of the user who edited this record for last time");
            });

            modelBuilder.Entity<VwContact>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwContact");

                entity.HasComment("Contact list");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("adr");

                entity.Property(e => e.Cee).HasColumnName("CEE");

                entity.Property(e => e.ChannelNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactClassNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength();

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longitud).HasColumnType("decimal(20, 16)");

                entity.Property(e => e.Nif)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NIF");

                entity.Property(e => e.NomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RaoSocialyNomCom)
                    .HasMaxLength(103)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwContactChannel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwContactChannel");

                entity.HasComment("Contacts per distribuition channel");
            });

            modelBuilder.Entity<VwContactMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwContactMenu");

                entity.HasComment("Contact tabs");
            });

            modelBuilder.Entity<VwContentUrl>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwContentUrl");

                entity.Property(e => e.Cat).IsUnicode(false);

                entity.Property(e => e.Eng).IsUnicode(false);

                entity.Property(e => e.Esp).IsUnicode(false);

                entity.Property(e => e.Por).IsUnicode(false);
            });

            modelBuilder.Entity<VwCtaMe>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCtaMes");

                entity.HasComment("Monthly balances per year each account");

                entity.Property(e => e.Cat)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.D01).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D02).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D03).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D04).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D05).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D06).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D07).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D08).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D09).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D10).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D11).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.D12).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Eng)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Esp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.H01).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H02).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H03).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H04).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H05).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H06).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H07).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H08).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H09).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H10).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H11).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.H12).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCurExchange>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCurExchange");

                entity.HasComment("Currency exchange rates");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.Rate).HasColumnType("numeric(12, 6)");

                entity.Property(e => e.Tag)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VwCustomerChannelOpenSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerChannelOpenSkus");

                entity.HasComment("Product range per customer");
            });

            modelBuilder.Entity<VwCustomerChannelSkusExcluded>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerChannelSkusExcluded");

                entity.HasComment("Excluded product ranges per customer");
            });

            modelBuilder.Entity<VwCustomerCredit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerCredit");

                entity.HasComment("Credit limit per customer");

                entity.Property(e => e.Eur).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.Obs).HasColumnType("text");
            });

            modelBuilder.Entity<VwCustomerDto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerDto");

                entity.HasComment("Product discount over retail price to calculate customer cost price list");

                entity.Property(e => e.Dto).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Obs).HasColumnType("text");
            });

            modelBuilder.Entity<VwCustomerIban>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerIban");

                entity.HasComment("Active customer bank accounts");

                entity.Property(e => e.BankAlias).HasMaxLength(20);

                entity.Property(e => e.BankBranchAdr).HasMaxLength(50);

                entity.Property(e => e.BankNom).HasMaxLength(60);

                entity.Property(e => e.CaducaFch)
                    .HasColumnType("datetime")
                    .HasColumnName("Caduca_Fch");

                entity.Property(e => e.Ccc)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CCC");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MandatoFch)
                    .HasColumnType("datetime")
                    .HasColumnName("Mandato_Fch");
            });

            modelBuilder.Entity<VwCustomerProductGuidsIncluded>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerProductGuidsIncluded");
            });

            modelBuilder.Entity<VwCustomerSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerSkus");

                entity.HasComment("Customer result product range");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryHideUntil).HasColumnType("date");

                entity.Property(e => e.CategoryKg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.PackageEan)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.SkuHideUntil).HasColumnType("date");

                entity.Property(e => e.SkuKg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.SkuNom).IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomLlarg).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargCat).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargEng).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargPor).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCustomerSkusExcluded>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerSkusExcluded");

                entity.HasComment("Customer excluded product range");
            });

            modelBuilder.Entity<VwCustomerSkusIncluded>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerSkusIncluded");

                entity.HasComment("Customer included product range");
            });

            modelBuilder.Entity<VwCustomerSkusLite>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerSkusLite");

                entity.HasComment("Customer product range, light weight");
            });

            modelBuilder.Entity<VwDelivery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDeliveries");

                entity.HasComment("Delivery notes");

                entity.Property(e => e.AlbCobro).HasColumnType("datetime");

                entity.Property(e => e.AlbEur).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.AlbFch).HasColumnType("datetime");

                entity.Property(e => e.AlbImportAdicional).HasColumnType("money");

                entity.Property(e => e.ClientRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LangId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TicketCognom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TicketNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Tracking)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNif)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrCreatedNickname)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwDeliveryShipment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDeliveryShipment");

                entity.HasComment("Feedback from warehouse about logistic details");

                entity.Property(e => e.Cost).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.CubicKg).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Expedition)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.Packaging)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Pallet)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SkuEan)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sscc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SSCC");

                entity.Property(e => e.Tracking)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingUrlTemplate).IsUnicode(false);

                entity.Property(e => e.TrpNif)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Volume).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Weight).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<VwDeliveryTracking>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDeliveryTracking");

                entity.HasComment("Shipment tracking details, used on VwDeliveryShipment");

                entity.Property(e => e.Cost).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.CubicKg).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.Delivery)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.Forwarder)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Packaging)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Sender)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Sscc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SSCC");

                entity.Property(e => e.Tracking)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrackingUrlTemplate).HasColumnType("text");

                entity.Property(e => e.TrpNif)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Volume).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Weight).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<VwDeliveryTrackingTrp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDeliveryTrackingTrp");

                entity.HasComment("Shipment forwarder details");

                entity.Property(e => e.Expr1).IsUnicode(false);

                entity.Property(e => e.Tracking)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNif)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwDept>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDepts");

                entity.Property(e => e.Cat).IsUnicode(false);

                entity.Property(e => e.Eng).IsUnicode(false);

                entity.Property(e => e.Esp).IsUnicode(false);

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FchLastEdited).HasColumnType("datetime");

                entity.Property(e => e.Por).IsUnicode(false);
            });

            modelBuilder.Entity<VwDeptCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDeptCategories");

                entity.HasComment("Product categories per department");
            });

            modelBuilder.Entity<VwDocfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDocfile");

                entity.HasComment("Document details");

                entity.Property(e => e.DocFileFchCreated).HasColumnType("datetime");

                entity.Property(e => e.DocfileFch).HasColumnType("datetime");

                entity.Property(e => e.DocfileHash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DocfileHres).HasColumnName("DocfileHRes");

                entity.Property(e => e.DocfileNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocfileVres).HasColumnName("DocfileVRes");
            });

            modelBuilder.Entity<VwDocfileThumbnail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDocfileThumbnail");

                entity.HasComment("Documents thumbnails");

                entity.Property(e => e.DocfileFch).HasColumnType("datetime");

                entity.Property(e => e.DocfileHash)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.DocfileHres).HasColumnName("DocfileHRes");

                entity.Property(e => e.DocfileNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocfileThumbnail).HasColumnType("image");

                entity.Property(e => e.DocfileVres).HasColumnName("DocfileVRes");
            });

            modelBuilder.Entity<VwDownloadTarget>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwDownloadTarget");

                entity.Property(e => e.DocFileFchCreated).HasColumnType("datetime");

                entity.Property(e => e.DocfileFch).HasColumnType("datetime");

                entity.Property(e => e.DocfileHash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DocfileHres).HasColumnName("DocfileHRes");

                entity.Property(e => e.DocfileNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocfileVres).HasColumnName("DocfileVRes");
            });

            modelBuilder.Entity<VwEciPlantillaModLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwEciPlantillaModLog");

                entity.Property(e => e.Descatalogado)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Rotura)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomLlargEsp).IsUnicode(false);
            });

            modelBuilder.Entity<VwEciSalesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwEciSalesReport");

                entity.HasComment("El Corte Ingles sales reports received via Edi");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Eur).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProveidorNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNom).IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwEdiInvRpt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwEdiInvRpt");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.Nadgy)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADGY")
                    .IsFixedLength();

                entity.Property(e => e.Nadgyguid).HasColumnName("NADGYGuid");

                entity.Property(e => e.Nadgyref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NADGYRef");

                entity.Property(e => e.Nadms)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADMS")
                    .IsFixedLength();

                entity.Property(e => e.Nadmsguid).HasColumnName("NADMSGuid");
            });

            modelBuilder.Entity<VwEdiInvrptHeader>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwEdiInvrptHeader");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.Nadgy)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADGY")
                    .IsFixedLength();

                entity.Property(e => e.Nadgyguid).HasColumnName("NADGYGuid");

                entity.Property(e => e.Nadgyref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NADGYRef");

                entity.Property(e => e.Nadms)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("NADMS")
                    .IsFixedLength();

                entity.Property(e => e.Nadmsguid).HasColumnName("NADMSGuid");
            });

            modelBuilder.Entity<VwEdiSalesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwEdiSalesReport");

                entity.HasComment("Edi received sales reports from customers");

                entity.Property(e => e.BrandNom).HasMaxLength(50);

                entity.Property(e => e.CategoryNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Centro)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength();

                entity.Property(e => e.CustomerRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dept)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Eur).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwEmailDefault>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwEmailDefault");

                entity.Property(e => e.Adr)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwFeedback>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwFeedback");

                entity.HasComment("Likes and shares an object (like a blog post, etc) has achieved per user");
            });

            modelBuilder.Entity<VwFeedbackSum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwFeedbackSum");

                entity.HasComment("Summary of total of Likes and shares an object (like a blog post, etc) has achieved");
            });

            modelBuilder.Entity<VwFilter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwFilter");

                entity.HasComment("Product filter names");

                entity.Property(e => e.FilterCat)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterEng)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterEsp)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterItemCat)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterItemEng)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterItemEsp)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterItemPor)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FilterPor)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwFilterTarget>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwFilterTarget");

                entity.HasComment("Product filter targets");
            });

            modelBuilder.Entity<VwForecast>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwForecast");

                entity.HasComment("Product units expected to be sold each month");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwHoldingCustomRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwHoldingCustomRefs");

                entity.HasComment("Cuistom references assigned by certain customers to our products");

                entity.Property(e => e.CustomRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwIban>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwIban");

                entity.HasComment("All bank accounts");

                entity.Property(e => e.BankAlias).HasMaxLength(20);

                entity.Property(e => e.BankBranchAdr).HasMaxLength(50);

                entity.Property(e => e.BankBranchCountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("BankBranchCountryISO")
                    .IsFixedLength();

                entity.Property(e => e.BankBranchLocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankBranchZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankNom).HasMaxLength(60);

                entity.Property(e => e.BankSwift)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.IbanCaducaFch).HasColumnType("datetime");

                entity.Property(e => e.IbanCcc)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IbanMandatoFch).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwImpagat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwImpagats");

                entity.HasComment("Unpayments");

                entity.Property(e => e.Expr1).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwImpagatsOinsolvent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwImpagatsOInsolvents");

                entity.HasComment("unpayments plus definitive insolvencies");
            });

            modelBuilder.Entity<VwInsolvent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwInsolvents");

                entity.HasComment("Insolvencies");
            });

            modelBuilder.Entity<VwInvRpt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwInvRpt");

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CustomerNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Retail).HasColumnType("decimal(38, 14)");

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);
            });

            modelBuilder.Entity<VwInvRptException>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwInvRptExceptions");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwInvoicesSent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwInvoicesSent");

                entity.Property(e => e.EurLiq).HasColumnType("money");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Fpg).HasMaxLength(50);

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegimenEspecialOtrascendencia)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("RegimenEspecialOTrascendencia")
                    .IsFixedLength();

                entity.Property(e => e.SiiL9)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TipoFactura)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VwLangText>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwLangText");

                entity.HasComment("Text resources per language and target");

                entity.Property(e => e.Cat).IsUnicode(false);

                entity.Property(e => e.Eng).IsUnicode(false);

                entity.Property(e => e.Esp).IsUnicode(false);

                entity.Property(e => e.FchCat).HasColumnType("datetime");

                entity.Property(e => e.FchEng).HasColumnType("datetime");

                entity.Property(e => e.FchEsp).HasColumnType("datetime");

                entity.Property(e => e.FchPor).HasColumnType("datetime");

                entity.Property(e => e.Por).IsUnicode(false);
            });

            modelBuilder.Entity<VwLastRetailPrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwLastRetailPrice");

                entity.HasComment("Most recent retail price registered for each product");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.Retail).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<VwLocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwLocation");

                entity.HasComment("Joins Location table with the area tables it belongs to (Country, Region, Provincia, Zona)");

                entity.Property(e => e.Cee)
                    .HasColumnName("CEE")
                    .HasComment("True if the country belongs to EEC");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Catalan country name");

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("English country name");

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Spanish country name");

                entity.Property(e => e.CountryGuid).HasComment("Foreign key to Country table");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code (2 letters)");

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Portuguese country name");

                entity.Property(e => e.ExportCod).HasComment("For Customs purposes: 1:National, 2:EEC; 3:Rest of the world");

                entity.Property(e => e.LocationGuid).HasComment("Primary key");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the city or town");

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("International call prefix");

                entity.Property(e => e.ProvinciaGuid).HasComment("Foreign key to Provincia table, if any");

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the province, if any");

                entity.Property(e => e.RegioGuid).HasComment("Foreign key to Region table");

                entity.Property(e => e.RegioNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the region or Comunidad Autónoma");

                entity.Property(e => e.SplitByComarcas).HasComment("True if the zone may be split by comarcas");

                entity.Property(e => e.ZonaGuid).HasComment("Foreign key to Zona table");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the zone within the country");
            });

            modelBuilder.Entity<VwMgzInventoryCost>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwMgzInventoryCost");

                entity.Property(e => e.Dt2).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Dto).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Eur).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Fch).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwMgzInventoryIo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwMgzInventoryIO");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Io).HasColumnName("IO");
            });

            modelBuilder.Entity<VwMgzInventoryMovement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwMgzInventoryMovement");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Io).HasColumnName("IO");
            });

            modelBuilder.Entity<VwNav>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwNav");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cat).IsUnicode(false);

                entity.Property(e => e.Eng).IsUnicode(false);

                entity.Property(e => e.Esp).IsUnicode(false);

                entity.Property(e => e.IcoBig)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IcoSmall)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Por).IsUnicode(false);
            });

            modelBuilder.Entity<VwNoticia>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwNoticias");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.TitleCat).IsUnicode(false);

                entity.Property(e => e.TitleEng).IsUnicode(false);

                entity.Property(e => e.TitleEsp).IsUnicode(false);

                entity.Property(e => e.TitlePor).IsUnicode(false);

                entity.Property(e => e.UrlCat).IsUnicode(false);

                entity.Property(e => e.UrlEng).IsUnicode(false);

                entity.Property(e => e.UrlEsp).IsUnicode(false);

                entity.Property(e => e.UrlPor).IsUnicode(false);
            });

            modelBuilder.Entity<VwPdc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPdc");

                entity.HasComment("Purchase orders");

                entity.Property(e => e.Dt2).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.Pdd)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PncDto).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.PncEur).HasColumnType("money");
            });

            modelBuilder.Entity<VwPgcCtaSdo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPgcCtaSdo");

                entity.Property(e => e.Cat)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CtaId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Deb).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.Eng)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Esp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Hab).HasColumnType("numeric(38, 2)");
            });

            modelBuilder.Entity<VwPgcCtum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPgcCta");

                entity.HasComment("Accounts names and codes");

                entity.Property(e => e.Cat)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Eng)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Esp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwPncSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPncSku");

                entity.HasComment("Pending purchase orders");

                entity.Property(e => e.Com).HasColumnType("numeric(4, 2)");

                entity.Property(e => e.Dt2).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Dto).HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Eur).HasColumnType("money");

                entity.Property(e => e.SkuNomLlarg).IsUnicode(false);
            });

            modelBuilder.Entity<VwPostalAddress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPostalAddress");

                entity.HasComment("Contact address details");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("adr");

                entity.Property(e => e.Cee).HasColumnName("CEE");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength();

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SrcGuid).HasComment("Target; usually foreign key for CliGral table");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwPremiumLineExcludedCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPremiumLineExcludedCustomers");

                entity.HasComment("Customers excluded from exclusive distribution product ranges");
            });

            modelBuilder.Entity<VwPremiumLineExclusiveCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwPremiumLineExclusiveCustomers");

                entity.HasComment("Customers included on exclusive distribution product ranges");
            });

            modelBuilder.Entity<VwProductBreadcrumb>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductBreadcrumbs");

                entity.Property(e => e.BrandNomCat).IsUnicode(false);

                entity.Property(e => e.BrandNomEng).IsUnicode(false);

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.BrandNomPor).IsUnicode(false);

                entity.Property(e => e.BrandUrlCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrandUrlEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrandUrlEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrandUrlPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.CategoryUrlCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryUrlEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryUrlEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryUrlPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptNomCat).IsUnicode(false);

                entity.Property(e => e.DeptNomEng).IsUnicode(false);

                entity.Property(e => e.DeptNomEsp).IsUnicode(false);

                entity.Property(e => e.DeptNomPor).IsUnicode(false);

                entity.Property(e => e.DeptUrlCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptUrlEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptUrlEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptUrlPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuUrlCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuUrlEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuUrlEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuUrlPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProductCanonicalUrl>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductCanonicalUrl");

                entity.HasComment("Product canonical url");

                entity.Property(e => e.BrandCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrandEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrandEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BrandPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProductCnap>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductCnap");

                entity.HasComment("Product classification per functionality");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.NomLongCat)
                    .IsUnicode(false)
                    .HasColumnName("NomLong_CAT");

                entity.Property(e => e.NomLongEng)
                    .IsUnicode(false)
                    .HasColumnName("NomLong_ENG");

                entity.Property(e => e.NomLongEsp)
                    .IsUnicode(false)
                    .HasColumnName("NomLong_ESP");

                entity.Property(e => e.NomLongPor)
                    .IsUnicode(false)
                    .HasColumnName("NomLong_POR");

                entity.Property(e => e.NomShortCat)
                    .IsUnicode(false)
                    .HasColumnName("NomShort_CAT");

                entity.Property(e => e.NomShortEng)
                    .IsUnicode(false)
                    .HasColumnName("NomShort_ENG");

                entity.Property(e => e.NomShortEsp)
                    .IsUnicode(false)
                    .HasColumnName("NomShort_ESP");

                entity.Property(e => e.NomShortPor)
                    .IsUnicode(false)
                    .HasColumnName("NomShort_POR");

                entity.Property(e => e.SkuNom).IsUnicode(false);

                entity.Property(e => e.Tags).HasColumnType("text");
            });

            modelBuilder.Entity<VwProductDefaultUrl>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductDefaultUrl");

                entity.HasComment("Product default urls");

                entity.Property(e => e.BrandLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BrandSegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CategorySegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DeptSegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SkuSegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProductGuid>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductGuid");

                entity.HasComment("Products, either if product brand, product category or product sku");
            });

            modelBuilder.Entity<VwProductLandingPage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductLandingPage");

                entity.HasComment("Landing page per product, with all variants and languages a consumer is expected  to call a product with");

                entity.Property(e => e.Url)
                    .HasMaxLength(403)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProductLangText>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductLangText");

                entity.HasComment("product text resources (name, excerpt and Html description)");

                entity.Property(e => e.BrandText).HasColumnType("text");

                entity.Property(e => e.CategoryText).HasColumnType("text");

                entity.Property(e => e.DeptText).HasColumnType("text");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SkuText).HasColumnType("text");
            });

            modelBuilder.Entity<VwProductNom>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductNom");

                entity.HasComment("product names");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.DeptNom).IsUnicode(false);

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FullNom).IsUnicode(false);

                entity.Property(e => e.SkuNom).IsUnicode(false);

                entity.Property(e => e.SkuNomLlarg).IsUnicode(false);
            });

            modelBuilder.Entity<VwProductParent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductParent");

                entity.HasComment("In product  hierarchy (brand/category/sku), relates children with parents");
            });

            modelBuilder.Entity<VwProductText>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductText");

                entity.HasComment("Name, excerpt and description content by product");

                entity.Property(e => e.ContentCat).IsUnicode(false);

                entity.Property(e => e.ContentEng).IsUnicode(false);

                entity.Property(e => e.ContentEsp).IsUnicode(false);

                entity.Property(e => e.ContentPor).IsUnicode(false);

                entity.Property(e => e.ExcerptCat).IsUnicode(false);

                entity.Property(e => e.ExcerptEng).IsUnicode(false);

                entity.Property(e => e.ExcerptEsp).IsUnicode(false);

                entity.Property(e => e.ExcerptPor).IsUnicode(false);

                entity.Property(e => e.NomCat).IsUnicode(false);

                entity.Property(e => e.NomEng).IsUnicode(false);

                entity.Property(e => e.NomEsp).IsUnicode(false);

                entity.Property(e => e.NomLlargCat).IsUnicode(false);

                entity.Property(e => e.NomLlargEng).IsUnicode(false);

                entity.Property(e => e.NomLlargEsp).IsUnicode(false);

                entity.Property(e => e.NomLlargPor).IsUnicode(false);

                entity.Property(e => e.NomPor).IsUnicode(false);

                entity.Property(e => e.UrlCat).IsUnicode(false);

                entity.Property(e => e.UrlEng).IsUnicode(false);

                entity.Property(e => e.UrlEsp).IsUnicode(false);

                entity.Property(e => e.UrlPor).IsUnicode(false);
            });

            modelBuilder.Entity<VwProductUrl>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductUrl");

                entity.HasComment("Url segments per product");

                entity.Property(e => e.BrandLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BrandSegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CategorySegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DeptLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DeptSegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SkuLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SkuSegment)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProductUrlCanonical>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwProductUrlCanonical");

                entity.HasComment("Canonical Url segments per product");

                entity.Property(e => e.UrlBrandCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlBrandEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlBrandEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlBrandPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlCategoryCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlCategoryEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlCategoryEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlCategoryPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlDeptCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlDeptEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlDeptEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlDeptPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlSkuCat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlSkuEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlSkuEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlSkuPor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRepCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwRepCustomers");

                entity.HasComment("Customers per agent and start date");

                entity.Property(e => e.Customer).HasComment("Foreign key to CliGral table");

                entity.Property(e => e.FchFrom)
                    .HasColumnType("date")
                    .HasComment("Date the customer (indeed his area and channel) was assigned to this agent");

                entity.Property(e => e.Rep).HasComment("Foreign key to CliGral table");
            });

            modelBuilder.Entity<VwRepPncsLiqPending>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwRepPncsLiqPending");

                entity.HasComment("Customer orrders which have not been liquidated yet to the agent");

                entity.Property(e => e.Abr)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Alias (short name) for the agent");

                entity.Property(e => e.CcxGuid).HasComment("Customer to invoice, if different from default. Foreign key to CliGral");

                entity.Property(e => e.CliGuid).HasComment("Customer. Foreign key to CliGral table");

                entity.Property(e => e.Com)
                    .HasColumnType("numeric(4, 2)")
                    .HasComment("Commission percentage");

                entity.Property(e => e.ContactClass).HasComment("Contact classification upon activity");

                entity.Property(e => e.CountryGuid).HasComment("Customer Country, foreign key to Country table");

                entity.Property(e => e.DistributionChannel).HasComment("Distribution channel (hypermarket, chain, independant, e-commerce...)");

                entity.Property(e => e.Emp).HasComment("Company. Foreign key to Emp table");

                entity.Property(e => e.Fch)
                    .HasColumnType("date")
                    .HasComment("Order date");

                entity.Property(e => e.FullNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Guid).HasComment("Purchase order item. Foreign key to Pnc table");

                entity.Property(e => e.Lin).HasComment("Purchase order item line");

                entity.Property(e => e.LocationGuid).HasComment("Customer Location, foreign key to Location table");

                entity.Property(e => e.NoRep).HasComment("If true, no rep should earn a commission for this item");

                entity.Property(e => e.PdcGuid).HasComment("Purchase order. Foreign key to Pdc table");

                entity.Property(e => e.PdcNum).HasComment("Order number");

                entity.Property(e => e.RepCustom).HasComment("If true, conditions (rep or commission) are different from default and should not be overriden during validation process");

                entity.Property(e => e.RepGuid).HasComment("Agent. Foreign key to CliGral table");

                entity.Property(e => e.SkuNomLlarg).IsUnicode(false);

                entity.Property(e => e.ZipGuid).HasComment("Customer Postal code, foreign key to Zip table");

                entity.Property(e => e.ZonaGuid).HasComment("Customer Zone, foerign key to Zona table");
            });

            modelBuilder.Entity<VwRepProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwRepProducts");

                entity.HasComment("Agent product range");

                entity.Property(e => e.Rep).HasComment("Commercial agent. Foreign key to CliGral table");

                entity.Property(e => e.SkuGuid).HasComment("Product sku. Foreign key to Art table");
            });

            modelBuilder.Entity<VwRepSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwRepSkus");

                entity.HasComment("Agent product sku range, with sku names, excluding obsolets, accessories and spares");

                entity.Property(e => e.BrandGuid).HasComment("Product brand id; foreign key for Tpa table");

                entity.Property(e => e.BrandNom)
                    .HasMaxLength(50)
                    .HasComment("Product brand name");

                entity.Property(e => e.BrandOrd).HasComment("Product brand sort order");

                entity.Property(e => e.CategoryCodi).HasComment("Product category code (product, accessory, spare, Point of Sale materials...)");

                entity.Property(e => e.CategoryGuid).HasComment("Product category id; foreign key for Stp table");

                entity.Property(e => e.CategoryNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Category name");

                entity.Property(e => e.CategoryOrd).HasComment("Product category sort order");

                entity.Property(e => e.Rep).HasComment("Rep id; foreign key for CliGral table");

                entity.Property(e => e.SkuGuid).HasComment("Product sku Id; foreign key for Art table");

                entity.Property(e => e.SkuNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product sku name (Spanish)");

                entity.Property(e => e.SkuNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product sku name (Catalan, if different from Spanish)");

                entity.Property(e => e.SkuNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product sku name (English, if different from Spanish)");

                entity.Property(e => e.SkuNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Product sku name (Portuguese, if different from Spanish)");
            });

            modelBuilder.Entity<VwRetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwRetail");

                entity.HasComment("Complete product retail price list, including product bundles");

                entity.Property(e => e.Retail).HasColumnType("decimal(38, 8)");
            });

            modelBuilder.Entity<VwRetailPrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwRetailPrice");

                entity.HasComment("Product price lists, both active and outdated");

                entity.Property(e => e.Fch).HasColumnType("date");

                entity.Property(e => e.FchEnd).HasColumnType("date");

                entity.Property(e => e.Retail).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<VwSalesManagerCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSalesManagerCustomers");

                entity.HasComment("Customers on each sales manager portfolio");
            });

            modelBuilder.Entity<VwSalesManagerSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSalesManagerSkus");

                entity.HasComment("Product range per sales manager");

                entity.Property(e => e.BrandNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSellout>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSellout");

                entity.HasComment("Product units ordered each month per customer");

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.ChannelNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.ContactClassNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Eur).HasColumnType("numeric(38, 13)");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProveidorNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepNom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSellout2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSellout2");

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.ChannelNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.ContactClassNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Eur).HasColumnType("numeric(38, 13)");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProveidorNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepNom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSelloutCli>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSelloutCli");

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.ChannelNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.ContactClassNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Eur).HasColumnType("numeric(30, 10)");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProveidorNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepNom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSelloutProveidor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSelloutProveidor");

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.ChannelNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.ContactClassNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Eur).HasColumnType("numeric(30, 10)");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProveidorNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepNom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSelloutRep>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSelloutRep");

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.ChannelNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelNomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.ContactClassNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNomCom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Eur).HasColumnType("numeric(30, 10)");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProveidorNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RepNom)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkus");

                entity.Property(e => e.Availability).HasColumnType("date");

                entity.Property(e => e.BundleDto).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Cbar)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CBar");

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FchLastEdited).HasColumnType("datetime");

                entity.Property(e => e.FchObsoleto).HasColumnType("datetime");

                entity.Property(e => e.HideUntil).HasColumnType("date");

                entity.Property(e => e.ImgFch).HasColumnType("datetime");

                entity.Property(e => e.Kg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.KgNet).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.M3).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.NomCurtCat).IsUnicode(false);

                entity.Property(e => e.NomCurtEng).IsUnicode(false);

                entity.Property(e => e.NomCurtEsp).IsUnicode(false);

                entity.Property(e => e.NomCurtPor).IsUnicode(false);

                entity.Property(e => e.NomLlargCat).IsUnicode(false);

                entity.Property(e => e.NomLlargEng).IsUnicode(false);

                entity.Property(e => e.NomLlargEsp).IsUnicode(false);

                entity.Property(e => e.NomLlargPor).IsUnicode(false);

                entity.Property(e => e.ObsoletoConfirmed).HasColumnType("datetime");

                entity.Property(e => e.PackageEan)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.Pmc).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefPrv)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSkuAndBundlePnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuAndBundlePncs");

                entity.HasComment("Product units pending from purchase orders, including  product bundles");

                entity.Property(e => e.Xcod).HasColumnName("xcod");
            });

            modelBuilder.Entity<VwSkuAndBundleStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuAndBundleStocks");

                entity.HasComment("Stocks, including product bundles");
            });

            modelBuilder.Entity<VwSkuBundleRetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuBundleRetail");

                entity.HasComment("Product bundle retail price list");

                entity.Property(e => e.Retail).HasColumnType("decimal(38, 8)");
            });

            modelBuilder.Entity<VwSkuCost>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuCost");

                entity.HasComment("Product cost from supplier current price list");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiscountOffInvoice)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("Discount_OffInvoice");

                entity.Property(e => e.DiscountOnInvoice)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("Discount_OnInvoice");

                entity.Property(e => e.Hash)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PriceListFch).HasColumnType("date");
            });

            modelBuilder.Entity<VwSkuLastCost>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuLastCost");

                entity.HasComment("Current price list date for each product");

                entity.Property(e => e.LastFch).HasColumnType("date");

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSkuNom>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuNom");

                entity.HasComment("Product names");

                entity.Property(e => e.BrandNom).IsUnicode(false);

                entity.Property(e => e.BrandNomEsp).IsUnicode(false);

                entity.Property(e => e.BundleDto).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.CategoryHideUntil).HasColumnType("date");

                entity.Property(e => e.CategoryKg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.CategoryKgNet).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.CategoryNom).IsUnicode(false);

                entity.Property(e => e.CategoryNomCat).IsUnicode(false);

                entity.Property(e => e.CategoryNomEng).IsUnicode(false);

                entity.Property(e => e.CategoryNomEsp).IsUnicode(false);

                entity.Property(e => e.CategoryNomPor).IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).IsUnicode(false);

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.FchObsoleto).HasColumnType("datetime");

                entity.Property(e => e.ObsoletoConfirmed).HasColumnType("datetime");

                entity.Property(e => e.PackageEan)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.SkuHideUntil).HasColumnType("date");

                entity.Property(e => e.SkuKg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.SkuKgNet).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.SkuNom).IsUnicode(false);

                entity.Property(e => e.SkuNomCat).IsUnicode(false);

                entity.Property(e => e.SkuNomEng).IsUnicode(false);

                entity.Property(e => e.SkuNomEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomLlarg).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargCat).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargEng).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargEsp).IsUnicode(false);

                entity.Property(e => e.SkuNomLlargPor).IsUnicode(false);

                entity.Property(e => e.SkuNomPor).IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSkuPnc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuPncs");

                entity.HasComment("Product pending units from purchase orders");
            });

            modelBuilder.Entity<VwSkuRetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuRetail");

                entity.HasComment("Retail price list");

                entity.Property(e => e.Retail).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<VwSkuStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSkuStocks");

                entity.HasComment("Stocks per warehouse");
            });

            modelBuilder.Entity<VwStaff>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStaff");

                entity.Property(e => e.Abr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Alta).HasColumnType("datetime");

                entity.Property(e => e.Baja).HasColumnType("datetime");

                entity.Property(e => e.Nac).HasColumnType("datetime");

                entity.Property(e => e.Nif)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NIF");

                entity.Property(e => e.NumSs)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NumSS")
                    .IsFixedLength();

                entity.Property(e => e.RaoSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StaffPosNomCat).IsUnicode(false);

                entity.Property(e => e.StaffPosNomEng).IsUnicode(false);

                entity.Property(e => e.StaffPosNomEsp).IsUnicode(false);

                entity.Property(e => e.StaffPosNomPor).IsUnicode(false);
            });

            modelBuilder.Entity<VwStaffIban>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStaffIban");

                entity.HasComment("Employees bank accounts");

                entity.Property(e => e.BankAlias).HasMaxLength(20);

                entity.Property(e => e.BankBranchAdr).HasMaxLength(50);

                entity.Property(e => e.BankNom).HasMaxLength(60);

                entity.Property(e => e.Ccc)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CCC");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwStat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStat");

                entity.Property(e => e.Eur).HasColumnType("numeric(38, 13)");
            });

            modelBuilder.Entity<VwStoreLocator>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStoreLocator");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AreaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tel");

                entity.Property(e => e.Val).HasColumnType("decimal(38, 13)");
            });

            modelBuilder.Entity<VwTaskLastLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwTaskLastLog");

                entity.HasComment("Registers result of automated task executions");

                entity.Property(e => e.ResultMsg).HasColumnType("text");
            });

            modelBuilder.Entity<VwTel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwTel");

                entity.HasComment("Contact phone numbers");

                entity.Property(e => e.TelNum)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwTelDefault>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwTelDefault");

                entity.HasComment("Contact default phone number");

                entity.Property(e => e.TelNum)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwTelsyEmail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwTelsyEmails");

                entity.HasComment("Contact phone numbers and email addresses");

                entity.Property(e => e.Num)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Obs)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwTrp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwTrp");

                entity.HasComment("Transport companies");

                entity.Property(e => e.TrackingUrlTemplate).HasColumnType("text");

                entity.Property(e => e.TrpNif)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrpNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwUsrArtCustRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwUsrArtCustRef");

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwUsrLog>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwUsrLog");

                entity.HasComment("For each target shows creation date and user, and last edition date and user");

                entity.Property(e => e.UsrCreatedEmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsrCreatedNickname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UsrLastEditedEmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsrLastEditedNickname)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwUsrNickname>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwUsrNickname");

                entity.HasComment("Returns a name for each user, either if name, nickname or email address");

                entity.Property(e => e.Guid).HasColumnName("guid");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwVehicle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwVehicles");

                entity.Property(e => e.Alta).HasColumnType("datetime");

                entity.Property(e => e.Baixa).HasColumnType("datetime");

                entity.Property(e => e.Bastidor)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.CompraNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ConductorNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContracteNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.InsuranceNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsuranceNum)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Matricula)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VenedorNom)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwWebAtla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwWebAtlas");

                entity.HasComment("Used to calculate recommended sale points per product on our website");

                entity.Property(e => e.LastFch).HasColumnType("datetime");

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Val).HasColumnType("money");

                entity.Property(e => e.ValHistoric).HasColumnType("money");
            });

            modelBuilder.Entity<VwWtbolBasket>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwWtbolBasket");

                entity.HasComment("Result of customer conversion pixels");

                entity.Property(e => e.BrandNom).HasMaxLength(50);

                entity.Property(e => e.CategoryHideUntil).HasColumnType("date");

                entity.Property(e => e.CategoryKg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.CategoryKgNet).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.CategoryNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CnapId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CnapNom).HasMaxLength(100);

                entity.Property(e => e.CodiMercancia)
                    .HasMaxLength(8)
                    .IsFixedLength();

                entity.Property(e => e.Ean13)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("EAN13");

                entity.Property(e => e.Emp).HasColumnName("EMP");

                entity.Property(e => e.Fch).HasColumnType("datetime");

                entity.Property(e => e.Noweb).HasColumnName("noweb");

                entity.Property(e => e.PackageEan)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.SkuHideUntil).HasColumnType("date");

                entity.Property(e => e.SkuKg).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.SkuKgNet).HasColumnType("numeric(6, 3)");

                entity.Property(e => e.SkuNom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkuNomLlarg)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuPrvNom)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Web)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwWtbolDisplay>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwWtbolDisplay");

                entity.Property(e => e.FchLastStocks).HasColumnType("datetime");

                entity.Property(e => e.FchStatus).HasColumnType("datetime");

                entity.Property(e => e.Inventory).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);

                entity.Property(e => e.Web)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwWtbolInventory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwWtbolInventory");

                entity.HasComment("Stock value per product reported by e-commerce customers");

                entity.Property(e => e.Inventory).HasColumnType("decimal(38, 2)");
            });

            modelBuilder.Entity<VwWtbolLandingPage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwWtbolLandingPage");

                entity.HasComment("Product landing pages to forward visitors to purchase on customer e-commerces");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.FchStatus).HasColumnType("datetime");

                entity.Property(e => e.LandingPageFchCreated).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.StockFchCreated).HasColumnType("datetime");

                entity.Property(e => e.Url).HasColumnType("text");
            });

            modelBuilder.Entity<VwWtbolStock>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwWtbolStock");

                entity.HasComment("Stock units available per product reported by customer e-commerces");

                entity.Property(e => e.FchCreated).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<VwZip>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwZip");

                entity.HasComment("Joins Zip table with the area tables it belongs to (Country, Region, Provincia, Zona, Location)");

                entity.Property(e => e.Cee)
                    .HasColumnName("CEE")
                    .HasComment("True if the country is member of EEC");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Catalan country name");

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("English country name");

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Spanish country name");

                entity.Property(e => e.CountryGuid).HasComment("Foreign key to Country table");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code");

                entity.Property(e => e.CountryLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasComment("Language to display to country residents");

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Portuguese country name");

                entity.Property(e => e.ExportCod).HasComment("For Customs purposes: 1:National, 2:EEC; 3:rest of the world");

                entity.Property(e => e.LocationGuid).HasComment("Foreign key to Location table");

                entity.Property(e => e.LocationNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the city or town");

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("International call code");

                entity.Property(e => e.ProvinciaGuid).HasComment("Fotreign key to Provincia table, if any");

                entity.Property(e => e.ProvinciaIntrastat)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the province, if any");

                entity.Property(e => e.RegioGuid).HasComment("Foreign key to Regio table, if any");

                entity.Property(e => e.RegioNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the region or Comunidad Autónoma where the province belongs, if any");

                entity.Property(e => e.SplitByComarcas).HasComment("True if the Zona may be split into Comarcas");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Postal code");

                entity.Property(e => e.ZipGuid).HasComment("Primary Key");

                entity.Property(e => e.ZonaGuid).HasComment("Foreign key to Zona table");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the Zona where the address belongs");
            });

            modelBuilder.Entity<VwZona>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwZona");

                entity.HasComment("Joins Zona table with the area tables it belongs to (Country, Region, Provincia)");

                entity.Property(e => e.Cee)
                    .HasColumnName("CEE")
                    .HasComment("For Customs purposes, true if it belongs to EEC");

                entity.Property(e => e.CountryCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Catalan country name");

                entity.Property(e => e.CountryEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("English country name");

                entity.Property(e => e.CountryEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Spanish country name");

                entity.Property(e => e.CountryGuid).HasComment("Foreign key to Country table");

                entity.Property(e => e.CountryIso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CountryISO")
                    .IsFixedLength()
                    .HasComment("ISO 3166 country code (2 letters)");

                entity.Property(e => e.CountryLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasComment("Language to display for the country, regardless of the zona language");

                entity.Property(e => e.CountryPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Portuguese country name");

                entity.Property(e => e.ExportCod).HasComment("Zona export cod: 1:National, 2:EEC, 3:Rest of the world");

                entity.Property(e => e.PrefixeTelefonic)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasComment("International call prefix");

                entity.Property(e => e.ProvinciaGuid).HasComment("Foreign key for Provincia table");

                entity.Property(e => e.ProvinciaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the province, if any");

                entity.Property(e => e.RegioGuid).HasComment("Foreign key to Region table");

                entity.Property(e => e.RegioNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the region or Comunidad Autonoma, if any");

                entity.Property(e => e.SplitByComarcas).HasComment("True if the zone may be split into Comarcas");

                entity.Property(e => e.ZonaGuid).HasComment("Foreign key to Zona table");

                entity.Property(e => e.ZonaLang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Language we should display for this zona residents. ISO 639-2");

                entity.Property(e => e.ZonaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Zona name");
            });

            modelBuilder.Entity<Web>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Web");

                entity.HasComment("Distributors per product to be published as recommended sale points. They are published depending on recent purchases of each product and availability of sale points on same area. A Windows service updates this table on a daily base");

                entity.HasIndex(e => new { e.Impagat, e.Blocked, e.Obsoleto }, "IX_Web_Suggested");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Adr)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Retailer sale point address");

                entity.Property(e => e.AreaGuid).HasComment("Either the province or the zone; foreign key for Provincia or Zona tables");

                entity.Property(e => e.AreaNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Either the province or the zone name");

                entity.Property(e => e.Blocked).HasComment("True if this retailer is not convenient to publish to consumers");

                entity.Property(e => e.Brand).HasComment("Product brand; foreign key to Tpa table");

                entity.Property(e => e.Category).HasComment("Product category; foreign key for Stp table");

                entity.Property(e => e.Ccx).HasComment("Account where invoices to this retailer are invoiced, if different from retailer. Used to totalize purchases and divide them among the number of outlets a particular customer owns.");

                entity.Property(e => e.Cit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Retailer sale point location name");

                entity.Property(e => e.Client).HasComment("Customer; foreign key for CliGral table");

                entity.Property(e => e.ConsumerPriority).HasComment("If true, it is displayed regardless its turnover");

                entity.Property(e => e.Country).HasComment("Foreign key for Country table");

                entity.Property(e => e.Dept).HasComment("Product department; foreign key for Dept table");

                entity.Property(e => e.Impagat).HasComment("True if the customer has unpaid invoices");

                entity.Property(e => e.LastFch)
                    .HasColumnType("date")
                    .HasComment("Most recent purchase order date");

                entity.Property(e => e.Location).HasComment("Retailer location; foreign key for Location table");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Retailer commerccial name");

                entity.Property(e => e.Obsoleto).HasComment("True if outdated");

                entity.Property(e => e.PremiumLine).HasComment("True if Premium Line enabled");

                entity.Property(e => e.Proveidor).HasComment("Supplier; foreign key for CliGral table");

                entity.Property(e => e.Raffles).HasComment("True if the sale point is enabled to participate as a raffles prize pickup point");

                entity.Property(e => e.SalePointsCount).HasComment("Number of outlets");

                entity.Property(e => e.Sku).HasComment("Product Sku; foreign key for Art table");

                entity.Property(e => e.SkuRef)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Manufacturer product code. Useful for integrations with suppliers so they can also recomend same retailers for same products");

                entity.Property(e => e.SumCcxVal)
                    .HasColumnType("decimal(12, 2)")
                    .HasComment("Total turnover of all the outlets owned by same customer for the last X months");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tel")
                    .HasDefaultValueSql("('')")
                    .HasComment("retailer sale point phone number");

                entity.Property(e => e.Val)
                    .HasColumnType("decimal(12, 2)")
                    .HasComment("Turnover for the last X months");

                entity.Property(e => e.ValHistoric)
                    .HasColumnType("decimal(12, 2)")
                    .HasComment("Total turnover ever");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Retailer sale point zip code");

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Web_Tpa");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Web_Stp");

                entity.HasOne(d => d.CcxNavigation)
                    .WithMany(p => p.WebCcxNavigations)
                    .HasForeignKey(d => d.Ccx)
                    .HasConstraintName("FK_Web_Ccx");

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.WebClientNavigations)
                    .HasForeignKey(d => d.Client)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Web_Client");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_Web_Country");

                entity.HasOne(d => d.DeptNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.Dept)
                    .HasConstraintName("FK_Web_Dept1");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.Location)
                    .HasConstraintName("FK_Web_Location");

                entity.HasOne(d => d.PremiumLineNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.PremiumLine)
                    .HasConstraintName("FK_Web_PremiumLine");

                entity.HasOne(d => d.ProveidorNavigation)
                    .WithMany(p => p.WebProveidorNavigations)
                    .HasForeignKey(d => d.Proveidor)
                    .HasConstraintName("FK_Web_Proveidor");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.Webs)
                    .HasForeignKey(d => d.Sku)
                    .HasConstraintName("FK_Web_Art");
            });

            modelBuilder.Entity<WebErr>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("WebErr");

                entity.HasComment("Error events on website");

                entity.HasIndex(e => e.Fch, "Ix_WebErr_Fch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Browser)
                    .HasColumnType("text")
                    .HasComment("User browser");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOWebErr.Cods");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date");

                entity.Property(e => e.Ip)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasComment("User Ip address");

                entity.Property(e => e.Msg)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.Referrer)
                    .HasColumnType("text")
                    .HasComment("Where did the user linked to this page from");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasComment("Url page causing the error");

                entity.Property(e => e.Usr).HasComment("User, if any. Foreign key for Email table");

                entity.HasOne(d => d.UsrNavigation)
                    .WithMany(p => p.WebErrs)
                    .HasForeignKey(d => d.Usr)
                    .HasConstraintName("FK_WebErr_Email");
            });

            modelBuilder.Entity<WebLogBrowse>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_WebLog3");

                entity.ToTable("WebLogBrowse");

                entity.HasComment("Logs when visitor browses content on the website like bloog or news");

                entity.HasIndex(e => new { e.Doc, e.Fch }, "IX_WebLogBrowse_DocFch");

                entity.HasIndex(e => new { e.Doc, e.User, e.Fch }, "IX_WebLogBrowse_DocUserFch");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Doc).HasComment("Target browsed; foreign key to LangText table");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time of the event");

                entity.Property(e => e.User).HasComment("Visitor, if logged in; foreign key for Email table");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.WebLogBrowses)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK_WebLogBrowse_Email");
            });

            modelBuilder.Entity<WebMenuGroup>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Menu groups under which are display the different menu items");

                entity.HasIndex(e => new { e.Ord, e.Guid }, "IX_WebMenuGroups");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Cat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Catalan language");

                entity.Property(e => e.Eng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, English language");

                entity.Property(e => e.Esp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Spanish language");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Por)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Portuguese language");
            });

            modelBuilder.Entity<WebMenuItem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.HasComment("Website menu items");

                entity.HasIndex(e => new { e.WebMenuGroup, e.Ord }, "IX_WebMenuItems_GroupOrd");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Actiu)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Displayed if true");

                entity.Property(e => e.Cat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Catalan language");

                entity.Property(e => e.Eng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, English language");

                entity.Property(e => e.Esp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Spanish language");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Por)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Portuguese language");

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Link to navigate when the item is clicked");

                entity.Property(e => e.WebMenuGroup).HasComment("Menu group under which this item is displayed; foreign key for WebMenuGroups table");

                entity.HasOne(d => d.WebMenuGroupNavigation)
                    .WithMany(p => p.WebMenuItems)
                    .HasForeignKey(d => d.WebMenuGroup)
                    .HasConstraintName("FK_WebMenuItems_WebMenuGroups");
            });

            modelBuilder.Entity<WebPageAlias>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_WebPageAlias_1");

                entity.ToTable("WebPageAlias");

                entity.HasComment("Website url redirections");

                entity.HasIndex(e => e.UrlFrom, "IX_WebPageAlias_UrlFrom")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Domain).HasComment("Enumerable (0.all domains, 1.only es domain, 2.omly pt domain)");

                entity.Property(e => e.UrlFrom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("Old url to be redirected");

                entity.Property(e => e.UrlTo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasComment("New landing page to display when the old url is requested");
            });

            modelBuilder.Entity<WinBug>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("WinBug");

                entity.HasComment("Alert events");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Event date");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.User).HasComment("foreign key for Email table, if any");
            });

            modelBuilder.Entity<WinMenuItem>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("WinMenuItem");

                entity.HasComment("Windows desktop ERP menu items");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.ActionProcedure)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Function name from main form Frm__Idx.vb to launch when clicked");

                entity.Property(e => e.Cod).HasComment("Enumerable DTOWinMenuItem.Cods (1.folder, has children menu items 2.item, launches action)");

                entity.Property(e => e.CustomTarget).HasComment("Enumerable DTOWinMenuItem.CustomTargets to display custom items when clicked (1.banks, 2.Staff, 3.Reps...)");

                entity.Property(e => e.Emps)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Hyphen divided string with Company ids (foreign key to Emp table) where this menuitem should be used");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Icon)
                    .HasColumnType("image")
                    .HasComment("489x48px image");

                entity.Property(e => e.Mime).HasDefaultValueSql("((1))");

                entity.Property(e => e.NomCat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Catalan language");

                entity.Property(e => e.NomEng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, English language");

                entity.Property(e => e.NomEsp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Spanish language");

                entity.Property(e => e.NomPor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Caption, Portuguese language");

                entity.Property(e => e.Ord).HasComment("Sort order");

                entity.Property(e => e.Parent).HasComment("Parent menu item (or nothing for root items)");

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("FK_WinMenuItem_Parent");
            });

            modelBuilder.Entity<WinMenuItemRol>(entity =>
            {
                entity.HasKey(e => new { e.MenuItem, e.Rol });

                entity.ToTable("WinMenuItemRol");

                entity.HasComment("Menu items per user rol");

                entity.HasIndex(e => new { e.Rol, e.MenuItem }, "IX_WinMenuitemRol_RolMenuItem")
                    .IsUnique();

                entity.Property(e => e.MenuItem).HasComment("foreign key for parent table WinMenuItem");

                entity.Property(e => e.Rol).HasComment("Rol id authorized to activate this menu item");

                entity.HasOne(d => d.MenuItemNavigation)
                    .WithMany(p => p.WinMenuItemRols)
                    .HasForeignKey(d => d.MenuItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WinMenuItemRol_WinMenuItem");
            });

            modelBuilder.Entity<Worten>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("worten");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Marketplace)
                    .HasMaxLength(50)
                    .HasColumnName("marketplace");

                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasColumnName("sku");
            });

            modelBuilder.Entity<WortenCatalog>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WortenCatalog");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .HasColumnName("id");

                entity.Property(e => e.Marketplace).HasColumnName("marketplace");

                entity.Property(e => e.Sku).HasColumnName("sku");
            });

            modelBuilder.Entity<WortenEan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("wortenEan");

                entity.Property(e => e.Ean)
                    .HasMaxLength(13)
                    .HasColumnName("EAN");

                entity.Property(e => e.Sku)
                    .HasMaxLength(20)
                    .HasColumnName("sku");
            });

            modelBuilder.Entity<WtbolBasket>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("WtbolBasket");

                entity.HasComment("Consumer baskets reported by Wtbol affiliates");

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.SiteNavigation)
                    .WithMany(p => p.WtbolBaskets)
                    .HasForeignKey(d => d.Site)
                    .HasConstraintName("FK_WtbolBasket_WtbolSite");
            });

            modelBuilder.Entity<WtbolBasketItem>(entity =>
            {
                entity.HasKey(e => new { e.Basket, e.Lin });

                entity.ToTable("WtbolBasketItem");

                entity.HasComment("Consumer basket items");

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.BasketNavigation)
                    .WithMany(p => p.WtbolBasketItems)
                    .HasForeignKey(d => d.Basket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolBasketItem_WtbolBasket");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.WtbolBasketItems)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolBasketItem_ART");
            });

            modelBuilder.Entity<WtbolCtr>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("WtbolCtr");

                entity.HasComment("Each time a visitor on our website selects an online retailer to purchase the product he was browsing, we log the event in this table so we can calculate the Ctr and build a retailer ranking to prioritize display order");

                entity.HasIndex(e => e.Fch, "IX_WtbolCtr_Fch");

                entity.HasIndex(e => new { e.Product, e.Site, e.Fch }, "IX_WtbolCtr_ProductSite");

                entity.HasIndex(e => new { e.Site, e.Product, e.Fch }, "IX_WtbolCtr_SiteProduct");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the visitor clicked the affiliate link");

                entity.Property(e => e.Ip)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Visitor Ip");

                entity.Property(e => e.Product).HasComment("Product he was browsing; foreign key for Art table");

                entity.Property(e => e.Site).HasComment("Affiliate site; foreign key for Wtbol site");

                entity.HasOne(d => d.SiteNavigation)
                    .WithMany(p => p.WtbolCtrs)
                    .HasForeignKey(d => d.Site)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolCtr_WtbolSite");
            });

            modelBuilder.Entity<WtbolLandingPage>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_WtbolLandingPage_1");

                entity.ToTable("WtbolLandingPage");

                entity.HasComment("Landing page retailers declare where they display each of our products so we can display their link under \"Purchase now\" to consumers browsing our catalog (Wtbol project)");

                entity.HasIndex(e => e.Product, "IX_WtbolLandingPage_Product");

                entity.HasIndex(e => e.Site, "IX_WtbolLandingPage_Site");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchStatus)
                    .HasColumnType("datetime")
                    .HasComment("Date and time the last status was set");

                entity.Property(e => e.Product).HasComment("Foreign key to Art table");

                entity.Property(e => e.Site).HasComment("Affiliated eCommerce selling our products; foreign key to WtbolSite table");

                entity.Property(e => e.Status).HasComment("DTOWtboEnumerable DTOWtbolLandingPage.StatusEnum (0.Pending, 1.Approved, 2.Denied)lLandingPage");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasComment("Landing page addressof this priduct on affiliated eCommerce site");

                entity.Property(e => e.UsrCreated).HasComment("User who uploaded this landing page");

                entity.Property(e => e.UsrStatus).HasComment("User who set last status; foreign  key for Email table");

                entity.HasOne(d => d.SiteNavigation)
                    .WithMany(p => p.WtbolLandingPages)
                    .HasForeignKey(d => d.Site)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolLandingPage_WtbolSite1");
            });

            modelBuilder.Entity<WtbolLog>(entity =>
            {
                entity.HasKey(e => new { e.Site, e.Fch });

                entity.ToTable("WtbolLog");

                entity.HasComment("Logs each time Wtbol external service (Britax through Hash) connects to our website to download affiliated customer stocks");

                entity.Property(e => e.Site).HasComment("Affiliated customer site; foreign key for WtbolSite");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time Hatch connects");

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Ip through which Hatch connects");
            });

            modelBuilder.Entity<WtbolSerp>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_WtbolSerp_1");

                entity.ToTable("WtbolSerp");

                entity.HasComment("Each time a visitor browsers info from one of our products, a list of limited recommended online retailers is displayed so the consumer can quickly purchase it. These online retailers are affiliated to our WTBOL project (Where to buy Online) and criteria to be displayed depends on whether the retailer has registered a landing page for this product, has declared stock availability in the last 24 hours, and which is his ranking on CTR.\r\nThis table registers each time the retailer is displayed so we can calculate the CTR");

                entity.HasIndex(e => e.Fch, "IX_WtbolSerp_Fch");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Website visitor ISO 3166-1 country code");

                entity.Property(e => e.Fch)
                    .HasColumnType("datetime")
                    .HasComment("Date and time");

                entity.Property(e => e.Ip)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Website visitor Ip");

                entity.Property(e => e.Product).HasComment("Product the consumer was interested in");

                entity.Property(e => e.Session).HasComment("User session; foreign key for UsrSession");

                entity.Property(e => e.UserAgent)
                    .HasColumnType("text")
                    .HasComment("Website visitor browser details");
            });

            modelBuilder.Entity<WtbolSerpItem>(entity =>
            {
                entity.HasKey(e => new { e.Serp, e.Pos });

                entity.ToTable("WtbolSerpItem");

                entity.HasComment("Affiliated online commerces and the position they are displayed when recommended to our website visitors");

                entity.Property(e => e.Serp).HasComment("Foreign key for parent table WtbolSerp");

                entity.Property(e => e.Pos).HasComment("Order in which the affiliated site is displayed");

                entity.Property(e => e.Site).HasComment("Affiliated site offered to our website visitor");

                entity.HasOne(d => d.SerpNavigation)
                    .WithMany(p => p.WtbolSerpItems)
                    .HasForeignKey(d => d.Serp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolSerpItem_WtbolSerp");

                entity.HasOne(d => d.SiteNavigation)
                    .WithMany(p => p.WtbolSerpItems)
                    .HasForeignKey(d => d.Site)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolSerpItem_WtbolSite");
            });

            modelBuilder.Entity<WtbolSite>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("WtbolSite");

                entity.HasComment("Wtbol project is the algorithm that recommends a limited number of online retailers to quickly convert sales when visitors browse our products in our website or the manufacturer website. Hatch is an external service manufacturers are subscribed to apply same functionality on their websites. Hatch gets retailers data from our Api. WtbolSite table stores the properties of affiliated online retailers");

                entity.HasIndex(e => e.MerchantId, "IX_WtbolSite_MerchantId");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Active).HasComment("True if currently active");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Contact person email");

                entity.Property(e => e.ContactNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Contact person name");

                entity.Property(e => e.ContactTel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Contact person phone");

                entity.Property(e => e.Customer).HasComment("Foreign key for CliGral table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.FchLastEdited)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was edited for last time");

                entity.Property(e => e.Logo)
                    .HasColumnType("image")
                    .HasComment("Retailer logo, 150x48 pixels");

                entity.Property(e => e.MerchantId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasComment("Id assigned by Hatch to refer to this affiliated");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Retailer name");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasComment("Comments");

                entity.Property(e => e.UsrCreated).HasComment("User who created this entry, foreignn key for Email table");

                entity.Property(e => e.UsrLastEdited).HasComment("User who edited this entry for last time");

                entity.Property(e => e.Web)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Retailer website");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.WtbolSites)
                    .HasForeignKey(d => d.Customer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolSite_CliGral");

                entity.HasOne(d => d.UsrCreatedNavigation)
                    .WithMany(p => p.WtbolSiteUsrCreatedNavigations)
                    .HasForeignKey(d => d.UsrCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolSite_UsrCreated");

                entity.HasOne(d => d.UsrLastEditedNavigation)
                    .WithMany(p => p.WtbolSiteUsrLastEditedNavigations)
                    .HasForeignKey(d => d.UsrLastEdited)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolSite_UsrLastEdited");
            });

            modelBuilder.Entity<WtbolStock>(entity =>
            {
                entity.HasKey(e => new { e.Site, e.Sku, e.FchCreated });

                entity.ToTable("WtbolStock");

                entity.HasComment("Stocks declared daily by affiliated online retailers");

                entity.HasIndex(e => e.FchCreated, "WtbolStock_Fchcreated");

                entity.Property(e => e.Site).HasComment("Affiliated site; foreign key for WtbolSite table");

                entity.Property(e => e.Sku).HasComment("Product; foreign key for Art table");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Retail price he is offering the product for");

                entity.Property(e => e.Stock).HasComment("Stock available the affiliate declares ");

                entity.HasOne(d => d.SiteNavigation)
                    .WithMany(p => p.WtbolStocks)
                    .HasForeignKey(d => d.Site)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolStock_WtbolSite");

                entity.HasOne(d => d.SkuNavigation)
                    .WithMany(p => p.WtbolStocks)
                    .HasForeignKey(d => d.Sku)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WtbolStock_ART");
            });

            modelBuilder.Entity<Xec>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_TLN")
                    .IsClustered(false);

                entity.ToTable("Xec");

                entity.HasComment("Bank checks from debtors");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.CcaPresentacio).HasComment("The accounts entry reflecting the deposit of the x¡check in our bank");

                entity.Property(e => e.CcaRebut).HasComment("The accounts entry reflecting check reception");

                entity.Property(e => e.CcaVto).HasComment("The accounts entry reflecting the date it was due in case of deferred amounts");

                entity.Property(e => e.CodPresentacio).HasComment("Enumerable DTOXec.ModalitatsPresentacio (a la vista, al cobro, al descuento)");

                entity.Property(e => e.ContactGuid).HasComment("Debtor; foreign key for CliGral table");

                entity.Property(e => e.Cur)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ESP')")
                    .IsFixedLength()
                    .HasComment("Currency");

                entity.Property(e => e.Eur)
                    .HasColumnType("decimal(9, 2)")
                    .HasComment("Amount, in Euro");

                entity.Property(e => e.FchRecepcio)
                    .HasColumnType("date")
                    .HasComment("Date the check was received");

                entity.Property(e => e.Iban)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Check account number");

                entity.Property(e => e.NbancGuid)
                    .HasColumnName("NBancGuid")
                    .HasComment("The bank account where the xec was entered, if any; foreign key to CliBnc");

                entity.Property(e => e.Pts)
                    .HasColumnType("money")
                    .HasComment("Amount, in foreign currency");

                entity.Property(e => e.SbankBranch)
                    .HasColumnName("SBankBranch")
                    .HasComment("Bank branch issuer, foreign key for Bn2 table");

                entity.Property(e => e.StatusCod).HasComment("Enumerable DTOXec.StatusCods (En carftera, en circulació, vençut...)");

                entity.Property(e => e.Vto)
                    .HasColumnType("date")
                    .HasComment("Due date, in case of deferred amount");

                entity.Property(e => e.XecNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Check number");

                entity.HasOne(d => d.CcaPresentacioNavigation)
                    .WithMany(p => p.XecCcaPresentacioNavigations)
                    .HasForeignKey(d => d.CcaPresentacio)
                    .HasConstraintName("FK_Xec_CcaPresentacio");

                entity.HasOne(d => d.CcaRebutNavigation)
                    .WithMany(p => p.XecCcaRebutNavigations)
                    .HasForeignKey(d => d.CcaRebut)
                    .HasConstraintName("FK_Xec_CcaRebut");

                entity.HasOne(d => d.CcaVtoNavigation)
                    .WithMany(p => p.XecCcaVtoNavigations)
                    .HasForeignKey(d => d.CcaVto)
                    .HasConstraintName("FK_Xec_CcaVto");

                entity.HasOne(d => d.ContactGu)
                    .WithMany(p => p.Xecs)
                    .HasForeignKey(d => d.ContactGuid)
                    .HasConstraintName("FK_XecHeader_Lliurador");

                entity.HasOne(d => d.NbancGu)
                    .WithMany(p => p.Xecs)
                    .HasForeignKey(d => d.NbancGuid)
                    .HasConstraintName("FK_Xec_NBanc");

                entity.HasOne(d => d.SbankBranchNavigation)
                    .WithMany(p => p.Xecs)
                    .HasForeignKey(d => d.SbankBranch)
                    .HasConstraintName("FK_Xec_SBankBranch");
            });

            modelBuilder.Entity<XecDetail>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK_XecDetail_1");

                entity.ToTable("XecDetail");

                entity.HasComment("Debtor debts paid by check");

                entity.HasIndex(e => e.Xec, "IX_XecDetail_Parent");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.PndGuid).HasComment("Debt details; foreign key to Pnd table");

                entity.Property(e => e.Xec).HasComment("Foreign key to parent Xec table");

                entity.HasOne(d => d.PndGu)
                    .WithMany(p => p.XecDetails)
                    .HasForeignKey(d => d.PndGuid)
                    .HasConstraintName("FK_XecDetail_Pnd");

                entity.HasOne(d => d.XecNavigation)
                    .WithMany(p => p.XecDetails)
                    .HasForeignKey(d => d.Xec)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_XecDetail_Xec");
            });

            modelBuilder.Entity<Yea>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Yea");

                entity.HasComment("Assigns a Guid number to each combination of Company and year");

                entity.HasIndex(e => new { e.Emp, e.Yea1 }, "IX_Yea_EmpYear")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Emp)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Company. Foreign key for Emp table");

                entity.Property(e => e.Yea1)
                    .HasColumnName("Yea")
                    .HasComment("Year");

                entity.HasOne(d => d.EmpNavigation)
                    .WithMany(p => p.Yeas)
                    .HasForeignKey(d => d.Emp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Yea_Emp");
            });

            modelBuilder.Entity<YouTube>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("YouTube");

                entity.HasComment("Product videos stored on Youtube");

                entity.Property(e => e.Guid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("Primary key");

                entity.Property(e => e.Dsc)
                    .HasColumnType("text")
                    .HasComment("Video description");

                entity.Property(e => e.FchCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date this entry was created");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("ISO 639-2 language code");

                entity.Property(e => e.Nom)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Video name");

                entity.Property(e => e.Obsoleto).HasComment("True if the video is outdated");

                entity.Property(e => e.YoutubeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("Youtube Id of the video");
            });

            modelBuilder.Entity<YouTubeProduct>(entity =>
            {
                entity.HasKey(e => new { e.YouTubeGuid, e.ProductGuid });

                entity.HasComment("Videos per product");

                entity.HasIndex(e => new { e.ProductGuid, e.Ord }, "IX_YouTubeProducts");

                entity.Property(e => e.YouTubeGuid).HasComment("Foreign key to parent table YouTube with video details");

                entity.Property(e => e.ProductGuid).HasComment("Product; foreign key either brand Tpa table, category Stp table or Sku Art table depending on video purpose");

                entity.Property(e => e.Ord)
                    .HasDefaultValueSql("((99))")
                    .HasComment("Sort order this video should appear on this product list");

                entity.HasOne(d => d.YouTubeGu)
                    .WithMany(p => p.YouTubeProducts)
                    .HasForeignKey(d => d.YouTubeGuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_YouTubeProducts_YouTube");
            });

            modelBuilder.Entity<Zip>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Zip");

                entity.HasComment("Postal codes");

                entity.HasIndex(e => new { e.Location, e.ZipCod }, "IX_Zip_Location");

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Location).HasComment("Foreign key to Location table");

                entity.Property(e => e.ZipCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Postal code");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Zips)
                    .HasForeignKey(d => d.Location)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zip_Location");
            });

            modelBuilder.Entity<Zona>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("Zona");

                entity.HasComment("Country Areas");

                entity.HasIndex(e => new { e.Country, e.Nom }, "IX_Zona_Country")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .ValueGeneratedNever()
                    .HasComment("Primary key");

                entity.Property(e => e.Country).HasComment("Foreign key to Country table");

                entity.Property(e => e.ExportCod).HasComment("Wether Customs considers it National (1), European (2) or rest of the world (3)");

                entity.Property(e => e.Img)
                    .HasColumnType("image")
                    .HasComment("Map");

                entity.Property(e => e.Iso)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ISO")
                    .IsFixedLength()
                    .HasComment("ISO code for the zone");

                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Official lang to write there, if different from the country it belongs");

                entity.Property(e => e.Mod347).HasComment("Wether operations with this zone should be declared on Spanish Model 347 form");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of the zone");

                entity.Property(e => e.PortsCondicions).HasComment("Foreign key for PortsCondicions table");

                entity.Property(e => e.Provincia).HasComment("Foreign key to Province table");

                entity.Property(e => e.SplitByComarcas).HasComment("Whether the zone may be divided in comarcas");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Zonas)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zona_Country");

                entity.HasOne(d => d.PortsCondicionsNavigation)
                    .WithMany(p => p.Zonas)
                    .HasForeignKey(d => d.PortsCondicions)
                    .HasConstraintName("FK_Zona_PortsCondicions");

                entity.HasOne(d => d.ProvinciaNavigation)
                    .WithMany(p => p.Zonas)
                    .HasForeignKey(d => d.Provincia)
                    .HasConstraintName("FK_Zona_Provincia");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
