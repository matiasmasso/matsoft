Public Class DTOWebMenu
    Property Groups As List(Of DTOWebMenuGroup)
End Class

Public Class DTOWebMenuGroup
    Inherits DTOBaseGuid

    Property Ord As Integer
    Property LangText As DTOLangText

    Property [Private] As Boolean
    Property Items As List(Of DTOWebMenuItem)

    Property Url As String

    Public Enum wellknowns
        NotSet
        Langs
        Products
    End Enum

    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOWebMenuItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOWebMenuItem)
    End Sub

    Shared Function wellknown(value As DTOWebMenuGroup.wellknowns) As Guid
        Dim retval As Guid = Nothing
        Select Case value
            Case DTOWebMenuGroup.wellknowns.Langs
                retval = New Guid("69D3D691-8F8E-41C9-8711-3E8A189EF113")
            Case DTOWebMenuGroup.wellknowns.Products
                retval = New Guid("81F161B9-4513-4388-8CB6-329E6005ECE4")
        End Select
        Return retval
    End Function

    Shared Function Nom(oWebMenuGroup As DTOWebMenuGroup, oLang As DTOLang) As String
        Dim retval As String = oWebMenuGroup.LangText.tradueix(oLang)
        Return retval
    End Function

    Shared Function LangsGroup(oLang As DTOLang) As DTOWebMenuGroup
        Dim retval As New DTOWebMenuGroup(DTOWebMenuGroup.wellknown(DTOWebMenuGroup.wellknowns.Langs))
        retval.LangText = New DTOLangText("Idioma", "Llengua", "Language", "idioma")


        DTOWebMenuGroup.AddItem(retval, "Español", "Espanyol", "Spanish", "Espanhol", "/esp", oLang.Equals(DTOLang.ESP))
        DTOWebMenuGroup.AddItem(retval, "Catalán", "Català", "Catalan", "Catalão", "/cat", oLang.Equals(DTOLang.CAT))
        DTOWebMenuGroup.AddItem(retval, "Inglés", "Anglés", "English", "Inglês", "/eng", oLang.Equals(DTOLang.ENG))
        DTOWebMenuGroup.AddItem(retval, "Portugués", "Portuguès", "Portuguese", "Português", "/por", oLang.Equals(DTOLang.POR))
        Return retval
    End Function


    Shared Function SorteosGroup() As DTOWebMenuGroup
        Dim retval As New DTOWebMenuGroup
        retval.LangText = New DTOLangText("Sorteos", "Sortejos", "Raffles", "Sorteios")
        retval.Url = "/sorteos"
        Return retval
    End Function

    Shared Function NoticiasGroup() As DTOWebMenuGroup
        Dim retval As New DTOWebMenuGroup
        retval.LangText = New DTOLangText("Noticias", "Noticies", "News", "Notícias")
        retval.Url = "/news"
        Return retval
    End Function

    Shared Function SignUpMenuGroup() As DTOWebMenuGroup
        Dim retval As New DTOWebMenuGroup
        retval.LangText = New DTOLangText("Registrarse", "Registrar-se", "Sign up", "Inscrever-se")
        retval.Url = "/registro"
        Return retval
    End Function

    Shared Function LoginGroup() As DTOWebMenuGroup
        Dim retval As New DTOWebMenuGroup()
        retval.LangText = New DTOLangText("Acceder", "Accedir", "Log in", "Entrar")
        retval.Url = "/pro"
        Return retval
    End Function

    Shared Function AddItem(oParent As DTOWebMenuGroup, sEsp As String, sCat As String, sEng As String, sPor As String, Optional sNavigateTo As String = "#", Optional BlSelected As Boolean = False) As DTOWebMenuItem
        Dim retval As New DTOWebMenuItem()
        With retval
            .LangText = New DTOLangText(sEsp, sCat, sEng, sPor)
            .Url = sNavigateTo
            .Actiu = BlSelected
        End With
        oParent.Items.Add(retval)
        Return retval
    End Function

End Class

Public Class DTOWebMenuItem
    Inherits DTOBaseGuid

    Property LangText As DTOLangText
    Property Ord As String
    Property Url As String
    Property Actiu As Boolean
    Property Group As DTOWebMenuGroup
    Property Rols As List(Of DTORol)


    Public Sub New()
        MyBase.New()
        _Rols = New List(Of DTORol)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Rols = New List(Of DTORol)
    End Sub

    Shared Function Nom(oWebMenuItem As DTOWebMenuItem, oLang As DTOLang) As String
        Dim retval As String = oWebMenuItem.LangText.tradueix(oLang)
        Return retval
    End Function
End Class

