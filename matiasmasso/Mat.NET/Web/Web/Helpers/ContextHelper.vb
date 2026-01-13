Imports System.Globalization
Imports System.Threading.Tasks

Public Class ContextHelper

    Public Enum Cookies
        None
        User
        UserPersist
        Lang
        LastProductBrowsed
    End Enum

    ''' <summary>
    ''' Returns current language from 1.Url segment 2.Session 3.TopLevelDomain 4.Cookie 5.User 6.Browser 7.OS 8.Default Esp 
    ''' </summary>
    ''' <returns></returns>
    ''' 
    Shared Function Lang() As DTOLang
        Dim retval As DTOLang = Nothing
        If TopLevelDomain() = "pt" Then
            retval = DTOLang.POR
        Else
            If ContextHelper.UrlSegmentLang Is Nothing Then
                retval = CookieLang()
                If retval Is Nothing Then
                    retval = DTOLang.FromBrowserLanguages(BrowserLanguages())
                    If retval Is Nothing Then
                        retval = DTOLang.FromISO639(OSLanguage)
                        If retval Is Nothing Then
                            retval = DTOLang.ESP()
                        End If
                    End If
                End If
            Else
                retval = ContextHelper.UrlSegmentLang
            End If
        End If
        Return retval
    End Function

    Shared Function Lang_CurrentlyCheckingOldVersion() As DTOLang
        Dim retval As DTOLang = Nothing
        Try
            retval = UrlSegmentLang()
            If retval Is Nothing Then
                retval = Context.Session("Lang")
                If retval Is Nothing Then
                    retval = If(TopLevelDomain() = "pt", DTOLang.POR, Nothing)
                    If retval Is Nothing Then
                        retval = CookieLang()
                        If retval Is Nothing Then
                            If Context.Session("User") Is Nothing Then
                                retval = DTOLang.FromBrowserLanguages(BrowserLanguages())
                                If retval Is Nothing Then
                                    retval = DTOLang.FromISO639(OSLanguage)
                                    If retval Is Nothing Then
                                        retval = DTOLang.ESP() 'per crawlers
                                    End If
                                End If
                            Else
                                retval = CType(Context.Session("User"), DTOUser).Lang
                            End If
                        End If
                    End If
                    Context.Session("Lang") = retval
                End If
            End If

        Catch ex As Exception
            retval = DTOLang.ESP
        End Try
        Return retval
    End Function


    Shared Function UrlSegmentLang() As DTOLang
        Dim retval As DTOLang = Nothing
        Dim oURI = Context().Request.Url
        Dim absolutePath = oURI.AbsolutePath.Trim("/")
        Dim segments = absolutePath.Split("/").ToList()
        Dim langSegment = segments.FirstOrDefault(Function(x) DTOLang.Collection.ISO6391Array.Any(Function(y) y = x))
        If langSegment.isNotEmpty Then
            retval = DTOLang.Factory(langSegment)
        End If
        Return retval
    End Function

    Shared Function isTrimmed() As Boolean
        Return Not String.IsNullOrEmpty(Context().Request.QueryString("trim"))
    End Function

    Shared Function CookieLang() As DTOLang
        Dim retval As DTOLang = Nothing
        Dim langTag = GetCookieValue(Mvc.ContextHelper.Cookies.Lang)
        If langTag > "" Then retval = DTOLang.Factory(langTag)
        Return retval
    End Function

    Shared Function BrowserLanguages() As List(Of String)
        Dim retval As New List(Of String)
        If Context() IsNot Nothing AndAlso Context.Request IsNot Nothing Then
            Dim ul = Context.Request.UserLanguages()
            If ul IsNot Nothing Then retval = ul.ToList()
        End If
        Return retval
    End Function

    Shared Function OSLanguage() As String
        Dim retval = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
        Return retval
    End Function

    Shared Function BrowserAgent() As String
        Return Context.Request.Browser.Browser
    End Function

    Shared Function BrowserVersion() As String
        Return Context.Request.Browser.Version
    End Function
    '--------------------------------------------------------------------------------- from current browser
    Shared Function resource(ByVal stringKey As String) As String
        Dim retval As String = ""

        If Not String.IsNullOrEmpty(stringKey) Then
            Dim cultureInfo As CultureInfo = GetCultureInfo()
            retval = DTO.GlobalStrings.ResourceManager.GetString(stringKey, cultureInfo)
        End If

        Return (If(retval, ""))
    End Function




    Shared Function ISO639() As String
        Dim retval As String = GetCultureInfo().TwoLetterISOLanguageName
        Return retval
    End Function

    Shared Function GetCultureInfo() As CultureInfo
        Dim userLanguages = HttpContext.Current.Request.UserLanguages
        Dim retval As CultureInfo

        If userLanguages.Count() > 0 Then

            Try
                retval = System.Globalization.CultureInfo.CurrentCulture
            Catch __unusedCultureNotFoundException1__ As CultureNotFoundException
                retval = Globalization.CultureInfo.InvariantCulture
            End Try
        Else
            retval = Globalization.CultureInfo.InvariantCulture
        End If

        Return retval
    End Function

    '--------------------------------------------------------------------------------------------------------------------------------------

    Private Shared Function Context() As HttpContext
        Dim retval = Web.HttpContext.Current
        Return retval
    End Function

    Shared Function Culture() As String
        Dim retval As String = ""
        Dim cultures() As String = Context.Request.UserLanguages
        If cultures IsNot Nothing Then
            If cultures.Length > 0 Then
                retval = cultures(0)
            End If
        End If
        Return retval
    End Function

    Shared Function Tradueix(ByVal Esp As String, Optional ByVal Cat As String = "", Optional ByVal Eng As String = "", Optional ByVal Por As String = "") As String
        Return Lang().Tradueix(Esp, Cat, Eng, Por)
    End Function

    Shared Function isAuthenticated() As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing Then
            If FEB2.User.Load(exs, oUser) Then
                Dim oRol As DTORol = oUser.Rol
                retval = oRol.isAuthenticated
            End If
        End If
        Return retval
    End Function

    Shared Function GetDistribuidorFromCookie() As DTOContact
        Dim retval As DTOContact = Nothing

        If Not Context.Request.Cookies("distribuidor") Is Nothing Then
            Dim sValue As String = Context.Request.Cookies("distribuidor").Value
            Dim oGuid As New Guid(sValue)
            retval = New DTOContact(oGuid)
        End If
        Return retval
    End Function

    Shared Sub SetDistribuidorCookie(oCustomerGuid As Guid)
        If GetDistribuidorFromCookie() IsNot Nothing Then
            Context.Response.Cookies.Remove("distribuidor")
        End If
        Dim oCookie As New HttpCookie("distribuidor", oCustomerGuid.ToString())
        oCookie.Expires() = Now.AddHours(48)
        Context.Response.Cookies.Add(oCookie)
    End Sub

    Shared Function IsUserPersisted() As Boolean
        Dim retval = HasCookie(Cookies.UserPersist)
        Return retval
    End Function

    Shared Sub SetUserCookie(oUser As DTOUser, persist As Boolean)
        SetCookieValue(Cookies.User, oUser)
        If persist Then
            SetCookieValue(Cookies.UserPersist)
        Else
            RemoveCookie(Cookies.UserPersist)
        End If
    End Sub

    Shared Sub SetLangCookie(oLang As DTOLang)
        Context.Session("Lang") = oLang
        SetCookieValue(Cookies.Lang, oLang.Tag)
    End Sub

    Shared Function IsIOS() As Boolean
        Dim userAgent = Context().Request.UserAgent.ToLower()
        Dim retval = userAgent.contains("ios")
        Return retval
    End Function

    Shared Function GetUser() As DTOUser
        Dim exs As New List(Of Exception)
        Dim retval As DTOUser = Context.Session("User")
        If retval Is Nothing Then
            'check si havia caducat la sessio
            If IsUserPersisted() AndAlso GuidHelper.IsGuid(GetCookieValue(Cookies.User)) Then
                Dim oUserGuid = New Guid(GetCookieValue(Cookies.User))
                retval = FEB2.User.FindSync(oUserGuid, exs)
                If exs.Count = 0 AndAlso retval IsNot Nothing Then
                    Context.Session("User") = retval
                End If
            End If

        End If
        Return retval
    End Function

    Shared Async Function FindUser(exs As List(Of Exception)) As Threading.Tasks.Task(Of DTOUser)
        Dim retval As DTOUser = Nothing
        Dim sGuid = GetCookieValue(Cookies.User)
        Dim oGuid = GuidHelper.GetGuid(sGuid)
        If Not oGuid.Equals(Guid.Empty) Then
            retval = Await FEB2.User.Find(oGuid, exs)
        End If
        Return retval
    End Function

    Shared Function FindUserSync(Optional exs As List(Of Exception) = Nothing) As DTOUser
        Dim retval As DTOUser = Nothing
        If exs Is Nothing Then exs = New List(Of Exception)
        Dim sGuid = GetCookieValue(Cookies.User)
        Dim oGuid = GuidHelper.GetGuid(sGuid)
        If Not oGuid.Equals(Guid.Empty) Then
            retval = FEB2.User.FindSync(oGuid, exs)
        End If
        Return retval
    End Function

    Shared Async Function Contact(exs As List(Of Exception)) As Task(Of DTOContact)
        Dim retval As DTOContact = Nothing
        Dim oUser = GetUser()

        If exs.Count = 0 AndAlso oUser IsNot Nothing Then
            If oUser.contact Is Nothing Then
                Dim oContacts = Await FEB2.Contacts.All(exs, oUser)
                If exs.Count = 0 AndAlso oContacts.Count > 0 Then
                    retval = oContacts.First
                End If
            Else
                retval = oUser.contact
            End If
        End If
        Return retval
    End Function

    Shared Function GetLang() As DTOLang
        Dim sLang = GetCookieValue(Cookies.Lang)
        Dim retval = DTOLang.Factory(sLang)
        If retval.Tag <> sLang Then
            SetLangCookie(retval)
        End If
        Return retval
    End Function

    Shared Function GetCookieValue(oCookieId As Cookies) As String
        Dim retval As String = ""
        If Context() IsNot Nothing Then
            If Context.Request IsNot Nothing Then
                Dim oCookie As HttpCookie = Context.Request.Cookies(oCookieId.ToString())
                If oCookie IsNot Nothing Then
                    retval = oCookie.Value
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function HasCookie(oCookieId As Cookies) As Boolean
        Dim retval As Boolean
        If Context() IsNot Nothing Then
            If Context.Request IsNot Nothing Then
                Dim oCookie As HttpCookie = Context.Request.Cookies(oCookieId.ToString())
                If oCookie IsNot Nothing Then
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function


    Shared Function IsMissingCookie(oCookieId As Cookies) As Boolean
        Dim oCookie As HttpCookie = Context.Request.Cookies(oCookieId.ToString())
        Return (oCookie Is Nothing)
    End Function

    Shared Sub SetCookieValue(oCookieId As DTOSession.CookieIds, oBaseGuid As DTOBaseGuid)
        If oBaseGuid IsNot Nothing Then
            SetCookieValue(oCookieId, oBaseGuid.Guid)
        End If
    End Sub

    Shared Sub SetCookieValue(oCookieId As DTOSession.CookieIds, oGuid As Guid)
        SetCookieValue(oCookieId, oGuid.ToString())
    End Sub

    Shared Sub SetCookieValue(oCookieId As Cookies, Optional sValue As String = "")
        Dim oContext As HttpContext = Context()
        If oContext IsNot Nothing Then
            If oContext.Request IsNot Nothing Then
                If oContext.Request.Cookies IsNot Nothing Then
                    Dim cookieName As String = oCookieId.ToString()
                    Dim oCookie As HttpCookie = If(HttpContext.Current.Response.Cookies.AllKeys.Contains(cookieName), HttpContext.Current.Response.Cookies(cookieName), HttpContext.Current.Request.Cookies(cookieName))
                    'Dim oCookie As HttpCookie = oContext.Request.Cookies(oCookieId.ToString())
                    If oCookie Is Nothing Then oCookie = New HttpCookie(oCookieId.ToString())
                    oCookie.Value = sValue
                    oCookie.Expires = Today.AddYears(50)
                    HttpContext.Current.Response.Cookies.[Set](oCookie)
                End If
            End If
        End If
    End Sub


    Shared Sub RemoveCookie(oCookieId As Cookies)
        If Context.Request.Cookies(oCookieId.ToString()) IsNot Nothing Then
            Context.Request.Cookies.Remove(oCookieId.ToString())
            Context.Response.Cookies(oCookieId.ToString()).Expires = DateTime.Now.AddDays(-1)
        End If
    End Sub


    Shared Function DefaultLang() As DTOLang
        Dim retval As DTOLang = DTOLang.ESP
        Try
            retval = LangFromTopLevelDomain()
            If Not retval.Equals(DTOLang.POR) Then
                retval = LangFromUserCulture()
            End If
        Catch ex As Exception

        End Try
        Return retval
    End Function

    Shared Function LangFromTopLevelDomain() As DTOLang
        Dim retval As DTOLang = Nothing
        Dim tld As String = TopLevelDomain()
        Select Case tld
            Case "pt"
                retval = DTOLang.POR
            Case Else
                retval = DTOLang.ESP
        End Select
        Return retval
    End Function

    Shared Function LangFromUserCulture() As DTOLang
        Dim retval As DTOLang = DTOLang.ESP
        Dim userLangs As String() = Context.Request.UserLanguages
        If userLangs IsNot Nothing Then
            If userLangs.Count > 0 Then
                Dim sFirstLang As String = userLangs.First
                If sFirstLang.Length > 2 Then sFirstLang = sFirstLang.Substring(0, 2)
                Select Case sFirstLang
                    Case "ca"
                        retval = DTOLang.CAT
                    Case "pt"
                        retval = DTOLang.POR
                    Case "es"
                        retval = DTOLang.ESP
                    Case Else
                        retval = DTOLang.ENG
                End Select
            End If
        End If
        Return retval
    End Function

    Shared Function Host() As String
        'farma.matiasmasso.es
        Dim retval As String = ""
        If Context() IsNot Nothing Then
            If Context.Request IsNot Nothing Then
                If Context.Request.Url IsNot Nothing Then
                    retval = Context.Request.Url.Host.ToLower()
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function IsLocalHost() As Boolean
        Dim localhost = Host()
        Return localhost = "localhost"
    End Function

    Shared Function Domain() As DTOWebDomain
        Dim retval = DTOWebDomain.Factory(TopLevelDomain)
        Return retval
    End Function

    Shared Function TopLevelDomain() As String
        Dim retval As String = ""
        Dim sHost As String = Host()
        If sHost > "" Then
            Dim segments As String() = sHost.Split(".")
            retval = segments.Last
        End If
        Return retval
    End Function

    Shared Function LogErrorSync(Optional sObs As String = "") As Boolean
        Dim exs As New List(Of Exception)
        Dim oContext = Context()
        Dim oUriReferrer As System.Uri = oContext.Request.UrlReferrer
        Dim sReferrer As String = oContext.Request.RawUrl
        If oUriReferrer IsNot Nothing Then
            sReferrer = oContext.Request.UrlReferrer.AbsolutePath
        End If
        'Dim retval = ContextHelper.LogErrorSync(sObs)

        Return True
    End Function

    Shared Sub MailErr(oUser As DTOUser, ByVal StErr As String, Optional oContext As System.Web.HttpContext = Nothing)
        Dim exs As New List(Of Exception)
        Dim sbSubject As New System.Text.StringBuilder
        Dim sbBody As New System.Text.StringBuilder
        Try
            sbSubject.Append("MAT ERR ")
            sbSubject.Append(DTOApp.Current.Id.ToString())
            If oContext IsNot Nothing Then
                sbSubject.Append(". Ip=" & oContext.Request.UserHostAddress)
            End If
        Catch ex As Exception

        End Try

        sbBody.AppendLine("User: " & System.Environment.UserName())
        sbBody.AppendLine("Maquina: " & System.Environment.MachineName)

        If oContext Is Nothing Then
            sbBody.AppendLine("IP: (no HttpContext)")
        Else

            Dim ipaddress As String = oContext.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ipaddress = "" Then
                ipaddress = oContext.Request.ServerVariables("REMOTE_ADDR")
            End If
            sbBody.AppendLine("IP: " & ipaddress)

        End If

        sbBody.AppendLine(StErr)
        sbBody.AppendLine("StackTrace: " & System.Environment.StackTrace())

        FEB2.MailMessage.MailAdminSync(sbSubject.ToString, sbBody.ToString())
    End Sub

    Shared Sub UnPersist(oContext As HttpContext)
        If Not oContext.Request.Cookies(DTOSession.CookiePersistName) Is Nothing Then
            Dim oCookie As HttpCookie
            oCookie = New HttpCookie(DTOSession.CookiePersistName)
            oCookie.Expires = Today.AddMonths(-1)
            oContext.Response.Cookies.Add(oCookie)
        End If
    End Sub

    Shared Async Function SignUpNewLead(exs As List(Of Exception), oContext As Web.HttpContext, oLang As DTOLang, oEmp As DTOEmp, sEmail As String, sPwd As String) As Task(Of DTOUser)
        Dim retval = DTOUser.Factory(oEmp)
        With retval
            .lang = oLang
            .emailAddress = sEmail
            .password = sPwd
            .source = DTOUser.Sources.webComment
            .rol = New DTORol(DTORol.Ids.lead)
            .fchCreated = Now
            .fchActivated = .fchCreated
        End With

        Await FEB2.User.Update(exs, retval)
        Return retval
    End Function



    Shared Function GetLastProductBrowsed() As DTOProduct
        Dim exs As New List(Of Exception)
        Dim retval As DTOProduct = Nothing
        Dim oGuid As Guid = GetCookieGuid(DTOSession.CookieIds.LastProductBrowsed)
        If oGuid <> Nothing Then
            retval = FEB2.Product.FindSync(exs, oGuid)
            'FEB2.Product.Load(retval, exs) (find ja fa les vegades de Load des de que el vaig actualitzar) 
        End If
        Return retval
    End Function

    Shared Function GetCookieGuid(oCookieId As DTOSession.CookieIds) As Guid
        Dim retval As Guid = Nothing
        Dim sCookieValue As String = GetCookieValue(oCookieId)
        If GuidHelper.IsGuid(sCookieValue) Then
            retval = New Guid(sCookieValue)
        End If
        Return retval
    End Function

    Shared Function GetCookiesAccepted() As Boolean
        Dim oCookie As HttpCookie = Context().Request.Cookies(DTOSession.CookiesAccepted)
        Dim retval As Boolean = Not oCookie Is Nothing
        Return retval
    End Function

    Shared Sub RemoveCookie(oCookieId As DTOSession.CookieIds)
        Dim aCookie As HttpCookie = Context().Request.Cookies(oCookieId)
        aCookie.Values.Remove(oCookieId)
        aCookie.Expires = DateTime.Now.AddDays(-1)
        Context().Response.Cookies.Add(aCookie)
    End Sub

    Shared Function BoxMenu(oMenus As DTOMenu.Collection, oLang As DTOLang) As BoxNodeModel.Collection
        Dim retval = BoxNodeModel.Collection.Factory(oLang)
        For Each oMenuGroup In oMenus
            Dim oBox = BoxNodeModel.Factory(oMenuGroup.Caption.Tradueix(oLang), oMenuGroup.Url)
            retval.Add(oBox)

            For Each item In oMenuGroup.Items
                Dim oChild = BoxNodeModel.Factory(item.Caption.Tradueix(oLang), item.Url)
                oBox.Children.Add(oChild)
            Next
        Next
        Return retval
    End Function


    Shared Function NavViewModel() As NavViewModel
        Dim exs As New List(Of Exception)
        Dim retval As NavViewModel = Nothing
        If Context.Session("Menu") Is Nothing Then
            RestoreSession(exs)
        End If
        retval = Context.Session("Menu")
        FEB2.MenuItems.attachDeveloperMenu(retval.GlobalMenu)
        Return retval
    End Function


    Shared Async Function SetNavViewModel(Optional oUser As DTOUser = Nothing) As Task
        Dim exs As New List(Of Exception)
        Dim oMenuGroups = Await FEB2.MenuItems.Fetch(exs, oUser)
        Dim retval As NavViewModel = DTO.NavViewModel.Factory(oUser, oMenuGroups)
        Context.Session("Menu") = retval
    End Function

    Shared Sub SetNavViewModelSync(Optional oUser As DTOUser = Nothing)
        Dim exs As New List(Of Exception)
        If oUser Is Nothing Then oUser = GetUser()
        Dim oMenuGroups = FEB2.MenuItems.FetchSync(exs, oUser)


        If oMenuGroups Is Nothing Then
            'no s'ha trobat l'usuari amb aquesta cookie
            ContextHelper.RemoveCookie(ContextHelper.Cookies.User)
            oUser = Nothing
            Context.Session("User") = oUser
            RemoveCookie(Cookies.User)
            oMenuGroups = FEB2.MenuItems.FetchSync(exs, oUser)
        End If
        Dim retval As NavViewModel = DTO.NavViewModel.Factory(oUser, oMenuGroups)
        Context.Session("Menu") = retval
    End Sub

    Shared Function RestoreSession(exs As List(Of Exception)) As Boolean
        Try
            Dim oUser = ContextHelper.GetUser()
            If oUser IsNot Nothing Then
                If FEB2.User.Load(exs, oUser) Then
                    Context.Session("User") = oUser
                End If
            End If

            Dim oLang = ContextHelper.Lang()
            If ContextHelper.IsMissingCookie(ContextHelper.Cookies.Lang) Then
                ContextHelper.SetLangCookie(oLang)
            End If

            Mvc.ContextHelper.SetNavViewModelSync()

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return exs.Count = 0
    End Function

    Shared Sub SetLang(oLang As DTOLang)
        ContextHelper.SetLangCookie(oLang)
        Mvc.ContextHelper.SetNavViewModelSync()
    End Sub

End Class
