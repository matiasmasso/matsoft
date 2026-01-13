Imports DocumentFormat.OpenXml.Office.Drawing

Public Class HomeModel
    Property Lang As DTOLang
    Property Banners As List(Of BoxModel)
    Property BrandsV As List(Of BoxModel)
    Property BrandsH As List(Of BoxModel)
    Property LastNewsPost As BoxModel
    Property LastBlogPost As BoxModel
    Property ActiveRaffle As BoxModel

    Public Sub New()
        _Banners = New List(Of BoxModel)
        LoadBrands()
    End Sub

    Shared Function Factory(oLang As DTOLang, oBanners As List(Of DTOBanner), oLastNewsPost As DTONoticia, oLastBlogPost As DTOBlog2PostModel, oActiveRaffle As DTORaffle)
        Dim retval As New HomeModel
        With retval
            .Lang = oLang
            .LoadBanners(oBanners)
            .LoadBrands()
            .LoadLastNewsPost(oLastNewsPost)
            .LoadLastBlogPost(oLastBlogPost)
            .loadActiveRaffle(oActiveRaffle)
        End With
        Return retval
    End Function

    Private Sub LoadBanners(oBanners As List(Of DTOBanner))
        _Banners = New List(Of BoxModel)
        For Each value In oBanners
            Dim oBox = BoxModel.Factory(value.Nom, value.NavigateTo, value.ImageUrl)
            _Banners.Add(oBox)
        Next
    End Sub

    Private Sub LoadBrands()
        _BrandsV = New List(Of BoxModel)
        With _BrandsV
            .Add(BoxModel.Factory("Britax Römer", "/britax-roemer", "/Media/Img/Portada/BrandsV/br-home-200x242.jpg"))
            .Add(BoxModel.Factory("Tommee Tippee", "/Tommee_Tippee", "/Media/Img/Portada/BrandsV/tt-home-200x242.jpg"))
            .Add(BoxModel.Factory("4moms", "/4moms", "/Media/Img/Portada/BrandsV/4moms-home-200x242.jpg"))
        End With

        BrandsH = New List(Of BoxModel)
        With _BrandsH
            .Add(BoxModel.Factory("Britax Römer", "/britax-roemer", "/Media/Img/Portada/BrandsH/BR-home-600.jpg"))
            .Add(BoxModel.Factory("Tommee Tippee", "/Tommee_Tippee", "/Media/Img/Portada/BrandsH/TT-home-600.jpg"))
            .Add(BoxModel.Factory("4moms", "/4moms", "/Media/Img/Portada/BrandsH/4moms-home-600.jpg"))
        End With
    End Sub

    Private Sub LoadLastNewsPost(oNoticia As DTONoticia)
        Dim domain = DTOWebDomain.Factory(False)
        _LastNewsPost = New BoxModel
        With _LastNewsPost
            .Title = oNoticia.Title.tradueix(ContextHelper.Lang)
            .NavigateTo = oNoticia.GetUrl(domain)
            .ImageUrl = oNoticia.ThumbnailUrl()
        End With
    End Sub

    Public Sub LoadLastBlogPost(value As DTOBlog2PostModel)
        _LastBlogPost = New BoxModel
        With _LastBlogPost
            .Title = value.Title
            .NavigateTo = value.Url()
            .ImageUrl = value.ThumbnailUrl
        End With
    End Sub

    Public Sub loadActiveRaffle(value As DTORaffle)
        _ActiveRaffle = New BoxModel
        With _ActiveRaffle
            .Title = value.Title
            .NavigateTo = DTORaffle.rafflesUrl()
            .ImageUrl = value.BannerUrl
        End With
    End Sub

End Class
