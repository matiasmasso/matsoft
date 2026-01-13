Public Class Menu

    Shared Function Fetch(oUser As DTOUser) As DTOMenu.Collection
        Dim retval As New DTOMenu.Collection
        Dim oBrandDeptsMenuItems = BEBL.Depts.BrandDeptsMenuItems()
        'retval.AddRange(oBrandDeptsMenuItems)

        If oUser Is Nothing Then
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Noticias", "Notícies", "News", "Notícias"), DTONoticia.urlAllNoticias()))
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Blog"), DTOBlogPost.BlogHomeUrl()))
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Sorteos", "Sortejos", "Raffles", "Sorteios"), DTORaffle.Collection.Url().RelativeUrl()))
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Contacto", "Contacte", "Contact", "Contacto"), "/contactMessage"))
        ElseIf oUser.Rol.id = DTORol.Ids.lead Then
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Noticias", "Notícies", "News", "Notícias"), DTONoticia.urlAllNoticias()))
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Blog"), DTOBlogPost.BlogHomeUrl()))
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Sorteos", "Sortejos", "Raffles", "Sorteios"), DTORaffle.Collection.Url().RelativeUrl()))
            'retval.Add(DTOMenu.Factory(DTOLangText.Factory("Contacto", "Contacte", "Contact", "Contacto"), "/contactMessage"))
            Dim oWebMenuGroups = BEBL.WebMenuGroups.All(oUser, JustActiveItems:=True)
            Dim oWellknownPerfil = DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Perfil)
            Dim oPerfil = oWebMenuGroups.FirstOrDefault(Function(x) x.Guid.Equals(oWellknownPerfil))
            If oPerfil IsNot Nothing Then
                Dim oParentMenu = DTOMenu.Factory(oPerfil.LangText, oPerfil.LangUrl)
                For Each item In oPerfil.Items
                    Dim oMenu = DTOMenu.Factory(item.LangText, item.LangUrl)
                    oParentMenu.Items.Add(oMenu)
                Next
                retval.Add(oParentMenu)
            End If

        Else
            Dim oWebMenuGroups = BEBL.WebMenuGroups.All(oUser, JustActiveItems:=True)
            For Each oGroup In oWebMenuGroups
                Dim oParentMenu = DTOMenu.Factory(oGroup.LangText, oGroup.LangUrl)
                For Each item In oGroup.Items
                    Dim oMenu = DTOMenu.Factory(item.LangText, item.LangUrl)
                    oParentMenu.Items.Add(oMenu)
                Next
                retval.Add(oParentMenu)
            Next
        End If

        Return retval
    End Function


End Class
