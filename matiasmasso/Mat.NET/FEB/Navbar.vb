Public Class Navbar
    Shared Function Html(oEmp As DTOEmp, oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As String
        Dim exs As New List(Of Exception)
        User.Load(exs, oUser)
        Dim oNavBar As DTONavbar = TopNavBar2(oEmp, oUser, oLang, oProduct)
        Dim retval As String = oNavBar.Html()
        Return retval
    End Function

    Shared Function TopNavBar2(oEmp As DTOEmp, oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTONavbar
        Dim retval = DTONavbar.Factory(DTONavbar.Formats.horizontal)
        Dim oWebMenuGroups = WebMenuGroups.ForWebSiteSync(oEmp, oUser, oLang, oProduct)
        For Each oWebMenuGroup As DTOWebMenuGroup In oWebMenuGroups
            Dim sText As String = oWebMenuGroup.LangText.Tradueix(oLang)
            Dim url As String = If(oWebMenuGroup.LangUrl Is Nothing, "", oWebMenuGroup.LangUrl.Tradueix(oLang))
            Dim oParent = retval.AddItem(sText, url)

            For Each oWebMenuItem As DTOWebMenuItem In oWebMenuGroup.Items
                sText = oWebMenuItem.LangText.Tradueix(oLang)
                oParent.AddItem(sText, oWebMenuItem.LangUrl.Tradueix(oLang), oWebMenuItem.Actiu)
            Next
        Next
        Return retval
    End Function

End Class
