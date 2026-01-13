Public Class NavViewModel
    Property Logo As ImageBoxViewModel
    Property Filters As DTOFilter.Collection
    Property CheckedFilterItems As DTOFilter.Item.Collection
    Property SpecificMenu As BoxViewModel.Collection
    Property GlobalMenu As BoxViewModel.Collection

    Public Sub New()
        _Logo = New ImageBoxViewModel
        _Filters = New DTOFilter.Collection
        _CheckedFilterItems = New DTOFilter.Item.Collection
        _SpecificMenu = New BoxViewModel.Collection
        _GlobalMenu = New BoxViewModel.Collection
    End Sub



    Shared Function Factory(value As DTOProductBrand, oTab As DTOProduct.Tabs, oLang As DTOLang) As NavViewModel
        Dim retval As New NavViewModel
        With retval
            Dim oBrand As DTOProductBrand = value
            With .Logo
                .ImageUrl = oBrand.LogoUrl
                .ImageWidth = DTOProductBrand.LogoWidth
                .ImageHeight = DTOProductBrand.LogoHeight
                .NavigateTo = oBrand.getUrl()
                .Title = String.Format("logo {0}", oBrand.Nom.tradueix(oLang))
            End With

            With .SpecificMenu
                .Add(BoxViewModel.Factory(oLang.tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar"), value.getUrl(DTOProduct.Tabs.distribuidores)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Galería de imágenes", "Galeria de imatges", "Image gallery", "Galeria de Imagens"), value.getUrl(DTOProduct.Tabs.galeria)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Descargas"), value.getUrl(DTOProduct.Tabs.descargas)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Videos"), value.getUrl(DTOProduct.Tabs.videos)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Publicaciones", "Publicacions", "Related posts", "Publicações"), tag:=DTOProduct.Tabs.bloggerposts))
            End With

        End With
        Return retval
    End Function

    Shared Function Factory(value As DTODept, oTab As DTOProduct.Tabs, oLang As DTOLang) As NavViewModel
        Dim retval As New NavViewModel
        With retval
            With .Logo
                .ImageUrl = DTOProduct.Brand(value).LogoUrl
                .ImageWidth = DTOProductBrand.LogoWidth
                .ImageHeight = DTOProductBrand.LogoHeight
            End With

        End With
        Return retval
    End Function

    Shared Function Factory(value As DTOProductCategory, oTab As DTOProduct.Tabs, oLang As DTOLang, oGlobalMenu As BoxViewModel.Collection) As NavViewModel
        Dim retval As New NavViewModel
        With retval
            With .Logo
                .ImageUrl = DTOProduct.Brand(value).LogoUrl
                .ImageWidth = DTOProductBrand.LogoWidth
                .ImageHeight = DTOProductBrand.LogoHeight
            End With

            With .SpecificMenu
                .Add(BoxViewModel.Factory(oLang.tradueix("Colección", "Col·lecció", "Designs", "Coleçao"), "#", tag:=DTOProduct.Tabs.coleccion))
                .Add(BoxViewModel.Factory(oLang.tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar"), value.getUrl(DTOProduct.Tabs.distribuidores)))
                If value.Brand.unEquals(DTOProductBrand.wellknown(DTOProductBrand.wellknowns.TommeeTippee)) Then
                    .Add(BoxViewModel.Factory(oLang.tradueix("Accesorios", "Accessoris", "Accessories", "Acessórios"), "#", tag:=DTOProduct.Tabs.accesorios))
                End If
                .Add(BoxViewModel.Factory(oLang.tradueix("Galería de imágenes", "Galeria de imatges", "Image gallery", "Galeria de Imagens"), value.getUrl(DTOProduct.Tabs.galeria)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Descargas"), value.getUrl(DTOProduct.Tabs.descargas)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Videos"), value.getUrl(DTOProduct.Tabs.videos)))
                If value.Brand.unEquals(DTOProductBrand.wellknown(DTOProductBrand.wellknowns.TommeeTippee)) Then
                    .Add(BoxViewModel.Factory(oLang.tradueix("Publicaciones", "Publicacions", "Related posts", "Publicações"), tag:=DTOProduct.Tabs.bloggerposts))
                End If
            End With

            .GlobalMenu = oGlobalMenu
        End With
        Return retval
    End Function
    Shared Function Factory(value As DTOProductSku, oTab As DTOProduct.Tabs, oLang As DTOLang, oGlobalMenu As BoxViewModel.Collection) As NavViewModel
        Dim retval As New NavViewModel
        With retval
            With .Logo
                .ImageUrl = DTOProduct.Brand(value).LogoUrl
                .ImageWidth = DTOProductBrand.LogoWidth
                .ImageHeight = DTOProductBrand.LogoHeight
            End With

            With .SpecificMenu
                .Add(BoxViewModel.Factory(oLang.tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar"), value.getUrl(DTOProduct.Tabs.distribuidores)))
                If DTOProduct.Brand(value).unEquals(DTOProductBrand.wellknown(DTOProductBrand.wellknowns.TommeeTippee)) Then
                    .Add(BoxViewModel.Factory(oLang.tradueix("Accesorios", "Accessoris", "Accessories", "Acessórios"), "#", tag:=DTOProduct.Tabs.accesorios))
                End If
                .Add(BoxViewModel.Factory(oLang.tradueix("Galería de imágenes", "Galeria de imatges", "Image gallery", "Galeria de Imagens"), value.getUrl(DTOProduct.Tabs.galeria)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Descargas"), value.getUrl(DTOProduct.Tabs.descargas)))
                .Add(BoxViewModel.Factory(oLang.tradueix("Videos"), value.getUrl(DTOProduct.Tabs.videos)))
                If DTOProduct.Brand(value).unEquals(DTOProductBrand.wellknown(DTOProductBrand.wellknowns.TommeeTippee)) Then
                    .Add(BoxViewModel.Factory(oLang.tradueix("Publicaciones", "Publicacions", "Related posts", "Publicações"), tag:=DTOProduct.Tabs.bloggerposts))
                End If
            End With

            .GlobalMenu = oGlobalMenu
        End With
        Return retval
    End Function

End Class
