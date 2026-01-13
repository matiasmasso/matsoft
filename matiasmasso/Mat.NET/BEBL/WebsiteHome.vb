Public Class WebsiteHome

    Shared Function Factory(oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTO.WebsiteModels.Home
        Dim oAboutUs = BEBL.Content.Find(DTO.DTOContent.Wellknowns.aboutUs)
        Dim retval = New WebsiteModels.Home()
        With retval
            .Banners = Banners(oLang)
            .BrandsV = BrandsV()
            .BrandsH = BrandsH()

            .News = LastNoticia(oUser, oLang, oProduct)
            .Blog = LastBlogPost(oLang)
            .Raffle = CurrentOrNextRaffle(oLang)

            .Menu = BEBL.Menu.fromUser(oUser, oLang)
            .Footer = DTO.WebsiteModels.Footer.Factory(oAboutUs, oLang)
        End With

        Return retval
    End Function

    Private Shared Function LastNoticia(oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTOBox
        Dim oNoticia = BEBL.Noticias.LastNoticia(oUser, oProduct)
        Dim retval = DTOBox.Factory(oNoticia.title.tradueix(oLang), oNoticia.ThumbnailUrl())
        Return retval
    End Function

    Private Shared Function LastBlogPost(oLang As DTOLang) As DTOBox
        Dim oBlogPost = BEBL.BlogPosts.LastPost(oLang)
        Dim retval = DTOBox.Factory(oBlogPost.Title, oBlogPost.FeaturedImageUrl())
        Return retval
    End Function

    Private Shared Function CurrentOrNextRaffle(oLang As DTOLang) As DTOBox
        Dim oRaffle = BEBL.Raffles.CurrentOrNextRaffle(oLang)
        Dim retval = DTOBox.Factory(oRaffle.title, oRaffle.BannerUrl())
        Return retval
    End Function

    Private Shared Function Banners(oLang As DTOLang) As List(Of DTOBox)
        Dim retval As New List(Of DTOBox)
        Dim oBanners = BEBL.Banners.Active(oLang)
        For Each item In oBanners
            retval.Add(DTOBox.Factory(item.nom, item.navigateTo, item.imageUrl))
        Next
        Return retval
    End Function

    Private Shared Function BrandsV() As List(Of DTOBox)
        Dim retval As New List(Of DTOBox)
        With retval
            .Add(DTOBox.Factory("Britax Römer", "/britax-roemer", "/content/images/BrandVertical/britax.jpg"))
            .Add(DTOBox.Factory("Tommee Tippee", "/Tommee_Tippee", "/content/images/BrandVertical/TommeeTippee.jpg"))
            .Add(DTOBox.Factory("4moms", "/4moms", "/content/images/BrandVertical/4moms.jpg"))
        End With
        Return retval
    End Function

    Private Shared Function BrandsH() As List(Of DTOBox)
        Dim retval As New List(Of DTOBox)
        With retval
            .Add(DTOBox.Factory("Britax Römer", "/britax-roemer", "/content/images/BrandHorizontal/BR-home-600.jpg"))
            .Add(DTOBox.Factory("Tommee Tippee", "/Tommee_Tippee", "/content/images/BrandHorizontal/TT-home-600.jpg"))
            .Add(DTOBox.Factory("4moms", "/4moms", "/content/images/BrandHorizontal/4moms-home-600.jpg"))
        End With
        Return retval
    End Function
End Class
