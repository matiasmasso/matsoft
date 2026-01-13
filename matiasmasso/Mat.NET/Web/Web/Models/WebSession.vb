Public Class WebSession
    Inherits DTOSession

    Private _TopNavBar As DTONavbar

    Public Property PurchaseOrder As PurchaseOrder

    Public Enum SideConfigs
        LastNewsBlogVideo
        Social
        LastNews
        LastEvents
    End Enum

    Public Function Title(Esp As String, Optional Cat As String = "", Optional Eng As String = "") As String
        Dim retval As String = String.Format("MATIAS MASSO, S.A. - {0}", Tradueix(Esp, Cat, Eng))
        Return retval
    End Function

    Shared Function FromHttpContext(oContext As HttpContext) As Mvc.WebSession

        Dim oMatSession As DTOSession = BLLSession.FromHttpContext(oContext)
        Dim retval As Mvc.WebSession = FromMatSession(oMatSession)
        Return retval
    End Function

    Shared Function FromNewUsuari(oContext As HttpContext, oUser As DTOUser) As Mvc.WebSession
        Dim oMatSession As DTOSession = BLLSession.GetNewSession(oContext, oUser)
        Dim retval As Mvc.WebSession = FromMatSession(oMatSession)
        Return retval
    End Function

    Shared Function GetCookiesAccepted(oContext As HttpContext) As Boolean
        Dim retval As Boolean = BLLSession.GetCookiesAccepted(oContext)
        Return retval
    End Function

    Shared Sub SetCookiesAccepted(oContext As HttpContext)
        BLLSession.SetCookiesAccepted(oContext)
    End Sub

    Shared Function FromMatSession(oMatSession As DTOSession) As Mvc.WebSession
        Dim retval As New Mvc.WebSession(oMatSession.Guid)
        With retval
            .FchFrom = oMatSession.FchFrom
            .User = oMatSession.User
            .Contact = oMatSession.Contact
            .IsAuthenticated = oMatSession.IsAuthenticated
            .Lang = oMatSession.Lang
        End With
        If retval.User.Country Is Nothing Then
            retval.User.Country = BLL.BLLCountry.DefaultCountry
        End If
        Return retval
    End Function



    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub New(oAppType As DTOSession.AppTypes)
        MyBase.New(oAppType)
    End Sub

    Public Sub Close()
        BLLSession.Close(Me)
    End Sub

    Public Sub Logoff(oContext As HttpContext)
        BLLSession.LogOff(oContext, Me)
        _TopNavBar = Nothing
    End Sub

    Shared Function SetUser(oContext As HttpContext, oUser As DTOUser) As Mvc.WebSession
        Dim oMatSession As DTOSession = BLLSession.SetUser(oContext, oUser)
        Dim retval As Mvc.WebSession = Mvc.WebSession.FromMatSession(oMatSession)
        Return retval
    End Function

    Public Shadows Property Lang As DTOLang
        Get
            Return MyBase.Lang
        End Get
        Set(value As DTOLang)
            MyBase.Lang = value
            _TopNavBar = Nothing
        End Set
    End Property

    Public Function PasswordEdit(oldPassword As String, newPassword As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BLLUser.PasswordEdit(MyBase.User, oldPassword, newPassword, exs)
        Return retval
    End Function


    Public Function Countries() As List(Of DTOCountry)
        Dim retval As List(Of DTOCountry) = BLL.BLLCountries.All(Me.Lang)
        Return retval
    End Function

    Public Function CountryListItems() As List(Of SelectListItem)
        Dim retval As New List(Of SelectListItem)
        For Each oCountry As DTOCountry In Countries()
            retval.Add(New SelectListItem() With {.Value = oCountry.ISO, .Text = MyBase.Lang.Tradueix(oCountry.Nom_Esp, oCountry.Nom_Cat, oCountry.Nom_Eng)})
        Next
        Return retval
    End Function

    Public Function Tradueix(sEsp As String, sCat As String, sEng As String) As String
        Dim retval As String = MyBase.Lang.Tradueix(sEsp, sCat, sEng)
        Return retval
    End Function

    Public Function Noticias(Optional iMaxLen As Integer = 1) As Noticias
        Dim oProduct As DTOProduct = Nothing

        If MyBase.User.Rol.Id = DTORol.Ids.Manufacturer Then
            Dim oBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.All(MyBase.User)
            If oBrands.Count > 0 Then
                oProduct = oBrands(0)
            End If
        Else
            oProduct = BLLSession.GetLastProductBrowsed(HttpContext.Current)
        End If

        Dim retval As Noticias = NoticiasLoader.LastNewsFromProduct(Me, DTONoticia.Srcs.News, oProduct, iMaxLen, True)
        If retval.Count = 0 Then
            retval = NoticiasLoader.LastNewsFromProduct(Me, DTONoticia.Srcs.News, Nothing, iMaxLen, True)
        End If
        Return retval
    End Function

    Public Function Eventos(Optional iMaxLen As Integer = 1) As Noticias
        Dim oProduct As DTOProduct = BLLSession.GetLastProductBrowsed(HttpContext.Current)
        Dim retval As Noticias = NoticiasLoader.NextEventsFromProduct(Me, oProduct, iMaxLen, True)
        If retval.Count = 0 Then
            retval = NoticiasLoader.NextEventsFromProduct(Me, Nothing, iMaxLen, True)
        End If
        Return retval
    End Function

    Public Function BlogPosts(Optional iMaxLen As Integer = 0) As List(Of DTOBlogPost)
        Static retval As List(Of DTOBlogPost)
        If retval Is Nothing Then
            retval = BLL.BLLWpPosts.Last(2)
        End If
        Return retval
    End Function

    Public Function YouTubeMovies(Optional iMaxLen As Integer = 0) As List(Of DTOYouTubeMovie)
        Dim retval As List(Of DTOYouTubeMovie) = Nothing
        Dim oProduct As DTOProduct = BLLSession.GetLastProductBrowsed(HttpContext.Current)
        If oProduct Is Nothing Then
            retval = BLLYouTubeMovies.Last(2)
        Else
            retval = BLLYouTubeMovies.FromProductGuid(oProduct.Guid)
            If retval.Count = 0 Then
                retval = BLLYouTubeMovies.Last(2)
            End If
        End If
        Return retval
    End Function


    Shared Function is_IOS_Browser(oContext As HttpContext) As Boolean
        Dim retval As Boolean = False
        Dim sUserAgent As String = oContext.Request.Headers("User-Agent").ToLower
        If sUserAgent.Contains("iphone") Or sUserAgent.Contains("ipad") Then
            retval = True
        End If
        Return retval
    End Function

End Class
