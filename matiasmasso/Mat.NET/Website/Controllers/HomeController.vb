Public Class HomeController
    Inherits _MatController

    Async Function Index(Optional CatchAll As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang
        Dim model = Await FEB.MvcHome.Model(exs, Website.GlobalVariables.Emp, oLang, MyBase.User)


        If exs.Count = 0 Then
            ViewBag.Title = "MATIAS MASSO, S.A."
            ViewBag.MetaDescription = oLang.Tradueix("MATIAS MASSO, S.A. importa marcas lideres en puericultura para su distribución en exclusiva en España, Portugal y Andorra",
                                                 "MATIAS MASSO, S.A. importa marques liders en puericultura per la seva distribució en exclusiva a Espanya, Portugal i Andorra",
                                                 "MATIAS MASSO, S.A. builds local markets for child care leader brands in Spain, Portugal and Andorra",
                                                 "MATIAS MASSO, S.A. importa marcas líderes em puericultura para a sua distribuição nos mercados de Espanha, Portugal e de Andorra")
            ViewBag.Canonical = DTOUrl.Factory()
            Return View("Index", model)
        Else
            Return Await MyBase.ErrorResult(exs)
        End If
    End Function

    Async Function Catchall() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oLang = ContextHelper.Lang
        Dim sUrlTo As String = ""
        Dim QueryString As NameValueCollection = Me.HttpContext.Request.QueryString
        Dim sAlias As String = HttpContext.Request.RawUrl
        Dim hostName As String = HttpContext.Request.Url.Host
        If sAlias.StartsWith("/") Then sAlias = sAlias.Substring(1)

        If sAlias.ToLower = "localapi" Or sAlias.ToLower = "uselocalapi/1" Then
            FEB.UseLocalApi(True)
            retval = Redirect("/")
        ElseIf sAlias.ToLower = "remoteapi" Or sAlias.ToLower = "uselocalapi/0" Then
            FEB.UseLocalApi(False)
            retval = Redirect("/")
        ElseIf sAlias.EndsWith("/esp") Or sAlias.EndsWith("/cat") Or sAlias.EndsWith("/eng") Or sAlias.EndsWith("/por") Then
            Dim sTag As String = sAlias.Substring(sAlias.Length - 3, 3)
            Dim sUrl = HttpContext.Request.RawUrl.Substring(0, sAlias.Length - 3)
            ContextHelper.SetLangCookie(DTOLang.Factory(sTag))
            retval = Redirect(sUrl)
        Else
            'Dim oWebPageAlias As DTOWebPageAlias = BLLWebPageAlias.FromUrl(sAlias, domain)
            Dim oWebPageAlias = Await FEB.WebPageAlias.FromUrl(exs, sAlias, DTOWebPageAlias.Domains.All)
            If oWebPageAlias Is Nothing Then
                retval = Await ErrorNotFoundResult()
            Else
                sUrlTo = oWebPageAlias.UrlTo
                If oWebPageAlias.UrlTo.StartsWith("http") Then
                    'pagina externa
                ElseIf oWebPageAlias.UrlTo.StartsWith("/") Then
                    'url relativa
                Else
                    'url relativa; afegir barra al principi
                    sUrlTo = "/" & sUrlTo
                End If
                retval = Redirect(sUrlTo)
            End If
        End If
        Return retval
    End Function


    Async Function Privacidad() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oTxt = Await FEB.Txt.Find(DTOTxt.Ids.Privacitat, exs)

        ViewBag.Canonical = DTOUrl.Factory("Privacidad")
        ViewBag.Title = MatHelperStd.UrlHelper.Title(ContextHelper.Tradueix("Privacidad", "Privacitat", "Privacity", "Política de Privacidade"))
        ViewBag.MetaDescription = MyBase.DealerDescription
        Return View("FreeText", oTxt)
    End Function

    Async Function PoliticaDeCookies() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oTxt = Await FEB.Txt.Find(DTOTxt.Ids.PoliticaDeCookies, exs)

        ViewBag.Canonical = DTOUrl.Factory(True, "politicadecookies")
        ViewBag.Title = MatHelperStd.UrlHelper.Title(ContextHelper.Tradueix("Política de Cookies", "Politica de Cookies", "Cookies policy", "Como usamos os cookies"))
        ViewBag.MetaDescription = "Para qué utilizamos las cookies en este sitio web"
        Return View("FreeText", oTxt)
    End Function

    Async Function About() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oTxt = Await FEB.Txt.Find(DTOTxt.Ids.About, exs)

        ViewBag.Canonical = DTOUrl.Factory(True, "about")
        ViewBag.Title = MatHelperStd.UrlHelper.Title(ContextHelper.Tradueix("Quienes somos", "Qui som", "About us", "Sobre nós"))
        ViewBag.MetaDescription = MyBase.DealerDescription
        Return View("FreeText", oTxt)
    End Function

    Function Navegador() As ActionResult
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim Model = DTOBrowser.Factory(Me.Request.UserHostAddress, Me.Request.Browser.Browser, Me.Request.Browser.Version, oUser)
        Model.Cookies = New Dictionary(Of String, String)
        For Each sCookieName In Request.Cookies
            Model.Cookies.Add(sCookieName, Request.Cookies(sCookieName).Value)
        Next
        Model.Headers = New Dictionary(Of String, String)
        For i As Integer = 0 To Request.Headers.Count - 1
            Dim name = Request.Headers.GetKey(i)
            Dim value = Request.Headers(i)
            Model.Headers.Add(name, value)
        Next
        Return View(Model)
    End Function
End Class
