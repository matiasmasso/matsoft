Public Class _MatController
    Inherits System.Web.Mvc.Controller

    Private _Session As DTOSession

    Public Enum AuthResults
        success
        login
        denied
    End Enum

    Public Enum CopyTo
        None
        Info
        Matias
    End Enum

    Protected Function Lang() As DTOLang
        Return ContextHelper.Lang()
    End Function

    Protected Function Tradueix(esp As String, Optional cat As String = "", Optional eng As String = "", Optional por As String = "") As String
        Return Lang.Tradueix(esp, cat, eng, por)
    End Function

    Protected Function Success() As JsonResult
        Dim oResult = DTOApiResult.Succeeded
        Dim retval = Json(oResult, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Protected Function Fail(exs As List(Of Exception)) As JsonResult
        Dim oResult = DTOApiResult.Failed(exs)
        Dim retval = Json(oResult, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Protected Function Fail(Optional msg As String = "") As JsonResult
        Dim exs As New List(Of Exception)
        If msg.isNotEmpty Then
            exs.Add(New Exception(msg))
        End If
        Dim oResult = DTOApiResult.Failed(exs)
        Dim retval = Json(oResult, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Protected Function Fail(ex As Exception) As JsonResult
        Dim exs As New List(Of Exception)
        exs.Add(ex)
        Dim oResult = DTOApiResult.Failed(exs)
        Dim retval = Json(oResult, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Protected Function Authorize(RolIds() As DTORol.Ids) As AuthResults
        Dim exs As New List(Of Exception)
        Dim retval As AuthResults
        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = AuthResults.login
        Else
            If FEB2.User.Load(exs, oUser) Then
                Dim oRol As DTORol = oUser.Rol
                If RolIds.Contains(oRol.id) Then
                    retval = AuthResults.success
                Else
                    retval = AuthResults.denied
                End If
            Else
                retval = AuthResults.denied
            End If
        End If
        Return retval
    End Function

    Protected Function AuthorizeStaff() As AuthResults
        Dim exs As New List(Of Exception)
        Dim retval As AuthResults
        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = AuthResults.login
        Else
            If FEB2.User.Load(exs, oUser) Then
                Dim oRol As DTORol = oUser.Rol
                If oRol.isStaff() Then
                    retval = AuthResults.success
                Else
                    retval = AuthResults.denied
                End If
            Else
                retval = AuthResults.denied
            End If
        End If
        Return retval
    End Function


    Protected Function Authorize(oUser As DTOUser, RolIds() As DTORol.Ids) As AuthResults
        Dim exs As New List(Of Exception)
        Dim retval As AuthResults
        If oUser Is Nothing Then
            retval = AuthResults.login
        Else
            Dim oRol As DTORol = oUser.Rol
            If RolIds.Contains(oRol.id) Then
                retval = AuthResults.success
            Else
                retval = AuthResults.denied
            End If
        End If
        Return retval
    End Function



    Protected Function LoginOrView(Optional sView As String = "", Optional oModel As Object = Nothing, Optional oRols As DTORol.Ids() = Nothing) As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing Then
            If FEB2.User.Load(exs, oUser) Then
                Dim oRol As DTORol = oUser.Rol
                If oRol.isAuthenticated Then
                    If oRols Is Nothing Then
                        retval = View(sView, oModel)
                    ElseIf oRols.Contains(oRol.id) Then
                        retval = View(sView, oModel)
                    Else
                        retval = UnauthorizedView()
                    End If
                Else
                    Dim oContext As ControllerContext = Me.ControllerContext
                    Dim actionName As String = oContext.RouteData.Values("action")
                    'Dim s = oContext.RouteData.Values("guid")
                    Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
                    Dim SelfUrl As String = Request.RawUrl ' u.Action(actionName)

                    retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
                End If
            Else
                retval = UnauthorizedView()
            End If
        Else
            Dim SelfUrl As String = Request.RawUrl ' u.Action(actionName)
            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Protected Function LoginOrDeny(oAuthResult As AuthResults) As ActionResult
        Dim retval As ActionResult = Nothing
        Select Case oAuthResult
            Case AuthResults.login
                retval = Login()
            Case AuthResults.denied
                retval = UnauthorizedView()
        End Select
        Return retval
    End Function

    Protected Shadows Function User() As DTOUser
        Return Session("User")
    End Function

    Protected Function SideMenuItems() As List(Of DTOMenuItem)
        Dim lang = ContextHelper.Lang
        Dim retval As New List(Of DTOMenuItem)
        With retval
            .Add(New DTOMenuItem(lang.Tradueix("Catalogo", "Catàleg", "Catalogue"), "/pro/proCatalog"))
            .Add(New DTOMenuItem(lang.Tradueix("Contactos", "Contactes", "Contacts"), "/pro/proAtlas"))
            .Add(New DTOMenuItem(lang.Tradueix("Sorteos", "Sortejos", "Raffles", "Sorteios"), "/pro/proRaffles"))
            .Add(New DTOMenuItem(lang.Tradueix("Wtbol"), "/pro/proWtBolSites"))
            .Add(New DTOMenuItem(lang.Tradueix("Credenciales", "Credencials", "Credentiales"), "/pro/proCredencials"))
        End With
        Return retval
    End Function

    Protected Function UnauthorizedView() As ActionResult
        ViewBag.Title = ContextHelper.Tradueix("Usuario no autorizado", "Usuari no autoritzat", "Unauthorized user")
        Dim retval As ActionResult = View("Unauthorized")
        Return retval
    End Function


    Protected Async Function SignInUser(oUser As DTOUser, persist As Boolean) As System.Threading.Tasks.Task
        Dim exs As New List(Of Exception)
        Session("User") = oUser
        ContextHelper.SetUserCookie(oUser, persist)
        Await ContextHelper.SetNavViewModel(oUser)
    End Function

    Protected Function IsPortugal() As Boolean
        Dim retval As Boolean = WebHelper.IsPortugal(Web.HttpContext.Current.Request)
        Return retval
    End Function

    Protected Function Channel() As DTODistributionChannel
        Dim retval As DTODistributionChannel = DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.botiga)
        Return retval
    End Function

    Protected Function DealerDescription() As String
        Dim retval As String = "Importador exclusivo de Britax Römer, Bob, 4moms y Tommee Tippee para España, Portugal y Andorra. Distribuidor exclusivo de Fisher-Price para el canal especializado en puericultura"
        Return retval
    End Function

    Protected Async Function LogOff() As Threading.Tasks.Task
        Dim exs As New List(Of Exception)
        Session("User") = Nothing
        ContextHelper.RemoveCookie(ContextHelper.Cookies.User)
        Await Mvc.ContextHelper.SetNavViewModel()
    End Function

    Protected Function CsvResult(oCsv As DTOCsv) As FileResult
        Dim bytes As Byte() = oCsv.toByteArray()
        Dim retval As FileResult = New FileContentResult(bytes, "text/csv")
        If oCsv.Filename > "" Then
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename='" & oCsv.Filename & "'")
        End If
        Return retval
    End Function

    Protected Function ExcelResult(oBook As ExcelHelper.Book) As FileResult
        Dim bytes As Byte() = Nothing
        LegacyHelper.ExcelHelper.Stream(oBook, bytes)
        Dim retval As FileResult = New FileContentResult(bytes, "application/vnd.openxmlformats-officedocument." + "spreadsheetml.sheet")
        If oBook.Filename > "" Then
            If Not oBook.Filename.EndsWith(".xlsx") Then oBook.Filename = oBook.Filename & ".xlsx"
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" & oBook.Filename & "")
        End If
        Return retval
    End Function

    Protected Function ExcelResult(oSheet As ExcelHelper.Sheet) As FileResult
        Dim bytes As Byte() = Nothing
        LegacyHelper.ExcelHelper.Stream(oSheet, bytes)

        Dim retval As FileResult = New FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        If oSheet.Filename = "" Then
            HttpContext.Response.AddHeader("content-disposition", "inline")
        Else
            If Not oSheet.Filename.EndsWith(".xlsx") Then oSheet.Filename = oSheet.Filename & ".xlsx"
            HttpContext.Response.AddHeader("content-disposition", "inline; filename=" & oSheet.Filename & "")
        End If
        Return retval
    End Function




    Function LoginOrFile(oBook As ExcelHelper.Book) As ActionResult
        Dim bytes As Byte() = Nothing
        LegacyHelper.ExcelHelper.Stream(oBook, bytes)
        Dim retval As FileResult = New FileContentResult(bytes, "application/vnd.openxmlformats-officedocument." + "spreadsheetml.sheet")
        If oBook.Filename > "" Then
            If Not oBook.Filename.EndsWith(".xlsx") Then oBook.Filename = oBook.Filename & ".xlsx"
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" & oBook.Filename & "")
        End If
        Return LoginOrFile(retval)
    End Function


    Function LoginOrFile(oSheet As ExcelHelper.Sheet) As ActionResult
        Dim bytes As Byte() = Nothing
        LegacyHelper.ExcelHelper.Stream(oSheet, bytes)
        Dim retval As FileResult = New FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        If oSheet.Filename > "" Then
            If Not oSheet.Filename.EndsWith(".xlsx") Then oSheet.Filename = oSheet.Filename & ".xlsx"
            HttpContext.Response.AddHeader("Content-Disposition", "inline; filename=" & oSheet.Filename & "")
            HttpContext.Response.AddHeader("Content-Length", bytes.Length.ToString())
        End If
        Return LoginOrFile(retval)
    End Function

    Function FileResult(oStream As Byte(), sFilename As String) As FileResult
        Dim retval As FileResult = Nothing
        Dim oMime As MimeCods = MimeHelper.GetMimeFromExtension(sFilename)
        Dim sContentType As String = MediaHelper.ContentType(oMime)
        retval = New FileContentResult(oStream, sContentType) ' "application/pdf")
        retval.FileDownloadName = sFilename ' Server.UrlEncode(sFilename)
        Return retval
    End Function

    Function FileResult(oDocFile As DTODocFile, Optional sFilename As String = "") As FileResult
        Dim retval As FileResult = Nothing
        If sFilename = "" Then sFilename = "Descargado de M+O.pdf"
        Dim oStream As Byte() = oDocFile.Stream
        Dim sContentType As String = MediaHelper.ContentType(oDocFile.Mime)
        retval = New FileContentResult(oStream, sContentType) ' "application/pdf")
        retval.FileDownloadName = sFilename ' Server.UrlEncode(sFilename)
        Return retval
    End Function

    Function LoginOrFile(oDocFile As DTODocFile, sFilename As String) As ActionResult
        Dim retval As FileResult = Nothing
        Dim oStream As Byte() = oDocFile.Stream
        Dim sContentType As String = MediaHelper.ContentType(oDocFile.Mime)
        retval = New FileContentResult(oStream, "application/pdf")
        retval.FileDownloadName = sFilename ' Server.UrlEncode(sFilename)
        Return retval
    End Function

    Public Function LoginOrFile(oCsv As DTOCsv) As ActionResult
        Dim retval As ActionResult = Nothing
        If ContextHelper.isAuthenticated Then
            retval = CsvResult(oCsv)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Public Function Login() As ActionResult
        Dim oContext As ControllerContext = Me.ControllerContext

        Dim actionName As String = oContext.RouteData.Values("action")
        Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
        'Dim SelfUrl As String = u.Action(actionName)
        Dim SelfUrl As String = oContext.HttpContext.Request.Path

        Dim retval As ActionResult = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        Return retval
    End Function


    Public Function LoginOrFile(oFileResult As FileContentResult) As ActionResult
        Dim retval As ActionResult = Nothing
        If ContextHelper.isAuthenticated Then
            retval = oFileResult
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Public Function PostedDocFiles(exs As List(Of Exception)) As List(Of DTODocFile)
        Dim retval As New List(Of DTODocFile)
        Dim oRequest As HttpRequest = Web.HttpContext.Current.Request
        For Each sFileKey As String In oRequest.Files
            Dim oFile As HttpPostedFile = oRequest.Files(sFileKey)
            If oFile.ContentLength > 0 Then
                Dim iLength As Integer = oFile.ContentLength
                Dim oBytes(iLength) As Byte
                Dim oStream As System.IO.Stream = oFile.InputStream()
                oStream.Read(oBytes, 0, iLength)
                Dim oDocfile = New DTODocFile()
                LegacyHelper.DocfileHelper.LoadFromStream(oBytes, oDocfile, exs, sFileKey)
                retval.Add(oDocfile)
            End If
        Next
        Return retval
    End Function

    Public Function PostedFiles() As List(Of Byte())
        Dim retval As New List(Of Byte())
        Dim oRequest As HttpRequest = Web.HttpContext.Current.Request
        For Each sFileKey As String In oRequest.Files
            Dim oFile As HttpPostedFile = oRequest.Files(sFileKey)
            If oFile.ContentLength > 0 Then
                Dim iLength As Integer = oFile.ContentLength
                Dim oBytes(iLength) As Byte
                Dim oStream As System.IO.Stream = oFile.InputStream()
                oStream.Read(oBytes, 0, iLength)
                retval.Add(oBytes)
            End If
        Next
        Return retval
    End Function

    Public Function PostedFile() As Byte()
        Dim retval As Byte() = Nothing
        Dim oFiles As List(Of Byte()) = PostedFiles()
        If oFiles.Count > 0 Then
            retval = oFiles.First
        End If
        Return retval
    End Function


    Protected Async Function PluginErrorResult(exs As List(Of Exception)) As Threading.Tasks.Task(Of ActionResult)
        Dim msg As String = ""
        If exs.Count > 0 Then msg = ExceptionsHelper.ToFlatString(exs)
        Dim oWebErr = Await LogWebErr(DTOWebErr.Cods.ManagedErr, msg)
        Return View("_Error")
    End Function

    Protected Async Function ErrorResult(exs As List(Of Exception)) As Threading.Tasks.Task(Of ActionResult)
        Dim msg As String = ""
        If exs.Count > 0 Then msg = ExceptionsHelper.ToFlatString(exs)
        Return Await ErrorResult(msg)
    End Function

    Protected Async Function ErrorResult(ex As Exception) As Threading.Tasks.Task(Of ActionResult)
        Return Await ErrorResult(ex.Message)
    End Function

    Protected Async Function ErrorResult(msg As String) As Threading.Tasks.Task(Of ActionResult)
        Dim oWebErr = Await LogWebErr(DTOWebErr.Cods.ManagedErr, msg)
        Return RedirectToAction("Index", "Error", New With {.Url = oWebErr.Url})
    End Function

    Protected Async Function ErrorBlogNotFoundResult() As Threading.Tasks.Task(Of ActionResult)
        Dim oWebErr = Await LogWebErr(DTOWebErr.Cods.PageNotFound, "Page Not Found")
        Return RedirectToAction("BlogPageNotFound", "Error", New With {.Url = oWebErr.Url})
    End Function

    Protected Async Function ErrorNotFoundResult() As Threading.Tasks.Task(Of ActionResult)
        Dim oWebErr = Await LogWebErr(DTOWebErr.Cods.PageNotFound, "Page Not Found")
        Return RedirectToAction("PageNotFound", "Error", New With {.Url = oWebErr.Url})
    End Function

    Protected Async Function LogWebErr(cod As DTOWebErr.Cods, msg As String) As Threading.Tasks.Task(Of DTOWebErr)
        Dim exs As New List(Of Exception)
        Dim oReferrer As Uri = Request.UrlReferrer()

        Dim url = Request.Url.ToString
        If url.LastIndexOf("http") > 0 Then
            url = url.Substring(url.LastIndexOf("http"))
            url = HttpUtility.UrlDecode(url)
        End If
        If url.IndexOf("aspxerrorpath") > 0 Then
            url = Regex.Replace(url, "\/Error\/Err[0-9]{3}\?aspxerrorpath=", "")
        End If

        Dim oWebErr = DTOWebErr.Factory(cod, User)
        With oWebErr
            .Url = url
            If oReferrer IsNot Nothing Then
                .Referrer = oReferrer.AbsoluteUri
            End If
            .Ip = Request.UserHostAddress
            If Request.Headers("User-Agent") IsNot Nothing Then
                .Browser = Request.Headers("User-Agent").ToString()
            End If
            .Msg = msg
        End With

        Await FEB2.WebErr.Update(exs, oWebErr)
        Return oWebErr
    End Function

    Function UrlLang(oUri As Uri) As DTOLang
        Dim host = oUri.Host
        Dim hostSegments = host.Split(".").ToList()
        Dim topLevelDomain = hostSegments.Last.Trim("/")

        Dim absolutePath = oUri.AbsolutePath.Trim("/")
        Dim segments = absolutePath.Split("/").ToList()
        Dim langSegment = segments.FirstOrDefault(Function(x) DTOLang.Collection.ISO6391Array.Any(Function(y) y = x))

        Dim retval = ContextHelper.Lang
        If langSegment.isNotEmpty() Then
            retval = DTOLang.FromISO639(langSegment)
        End If
        Return retval
    End Function

    Function UrlSplit(oUri As Uri, oMainSegment As DTOLangText, sFileExtension As String, ByRef oDomain As DTOWebDomain, ByRef oLang As DTOLang, ByRef sFriendlySegment As String) As Boolean
        Dim host = oUri.Host
        Dim hostSegments = host.Split(".").ToList()
        Dim topLevelDomain = hostSegments.Last.Trim("/")
        oDomain = DTOWebDomain.Factory(topLevelDomain)

        Dim absolutePath = oUri.AbsolutePath.Trim("/").ToLower()
        Dim segments = absolutePath.Split("/").ToList()
        Dim langSegment = segments.FirstOrDefault(Function(x) DTOLang.Collection.ISO6391Array.Any(Function(y) y = x))

        Dim cleanSegments = segments

        If oMainSegment IsNot Nothing Then
            Dim mainSegment = segments.FirstOrDefault(Function(x) oMainSegment.Matches(x.ToLower()))
            cleanSegments.Remove(mainSegment)
        End If

        If String.IsNullOrEmpty(langSegment) Then
            oLang = ContextHelper.Lang
        Else
            oLang = DTOLang.FromISO639(langSegment)
            cleanSegments.Remove(langSegment)
        End If

        'per no perdre links del antic blog de Wordpress
        If cleanSegments.Count > 0 AndAlso cleanSegments.Last.StartsWith("comment-page-") Then
            cleanSegments.Remove(cleanSegments.Last)
        End If

        sFriendlySegment = String.Join("/", cleanSegments)
        If sFileExtension.isNotEmpty And sFriendlySegment.EndsWith(sFileExtension) Then
            sFriendlySegment = sFriendlySegment.Substring(0, sFriendlySegment.Length - (sFileExtension).Length)
        End If
        Return True
    End Function
End Class
