Imports System.Web.Http
Imports System.Web.Optimization

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)

        If System.Environment.MachineName = "WIN-21VN07SBVSF" Then DAL.Defaults.serverName = System.Environment.MachineName
        Dim oMatUser = BEBL.User.Find(DTOUser.Wellknown(DTOUser.Wellknowns.matias).Guid)
        GlobalVariables.UsersCache = New Dictionary(Of Guid, DTOUser)
        GlobalVariables.CachedImages = New Models.CachedImages()

        GlobalVariables.Emps = BEBL.Emps.All(oMatUser)
        Dim oEmp = GlobalVariables.Emps.FirstOrDefault(Function(x) x.Id = DTOEmp.Ids.MatiasMasso)

        BEBL.ServerCache.Initialize(GlobalVariables.Emps)

        DTOApp.Current = New DTOApp(DTOApp.AppTypes.webApi)
        With DTOApp.Current
            .Curs = BEBL.Curs.All()
            .Taxes = BEBL.Taxes.All
            .Cur = .Curs.FirstOrDefault(Function(x) x.Tag = DTOCur.Ids.EUR.ToString())
            .PgcPlan = BEBL.PgcPlan.FromYear(DTO.GlobalVariables.Today().Year)
        End With

        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

    End Sub


End Class
