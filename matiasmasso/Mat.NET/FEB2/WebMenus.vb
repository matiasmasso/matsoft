Public Class WebMenus

    Shared Function Factory(oUser As DTOUser) As List(Of DTOMenuItem)
        Dim retval As New List(Of DTOMenuItem)
        AddProductsMenu(retval, oUser)
        AddRafflesMenu(retval, oUser)
        AddNewsMenu(retval, oUser)
        AddLangsMenu(retval, oUser)
        AddSignUpMenu(retval, oUser)
        AddSignInMenu(retval, oUser)
        AddQueriesMenu(retval, oUser)
        AddFormsMenu(retval, oUser)
        AddProfileMenu(retval, oUser)
        Return retval
    End Function

    Shared Sub AddProductsMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Productos", "Productes", "Products"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddRafflesMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Sorteos", "Sortejos", "Raffles"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddNewsMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Noticias", "Notícies", "News"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddLangsMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Idioma", "Idioma", "Language"))
        item.AddChild(oUser.Lang.Tradueix("Español", "Espanyol", "Spanish"))
        item.AddChild(oUser.Lang.Tradueix("Catalán", "Català", "Catalan"))
        item.AddChild(oUser.Lang.Tradueix("Inglés", "Anglès", "English"))
        item.AddChild(oUser.Lang.Tradueix("Portugués", "Portuguès", "Portuguese"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddSignUpMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Registrarse", "Registrar-se", "Sign up"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddSignInMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Acceder", "Accedir", "Sign in"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddQueriesMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Consultas", "Consultes", "Queries"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddFormsMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Formularios", "Formularis", "Forms"))
        oMenus.Add(item)
    End Sub
    Shared Sub AddProfileMenu(oMenus As List(Of DTOMenuItem), oUser As DTOUser)
        Dim item As New DTOMenuItem(oUser.Lang.Tradueix("Mi perfil", "El meu perfil", "My profile"))
        oMenus.Add(item)
    End Sub
End Class
