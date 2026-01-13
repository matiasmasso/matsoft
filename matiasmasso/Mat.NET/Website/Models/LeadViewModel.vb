Imports System.Web.WebPages.Html

Public Class LeadViewModel
    Property EmailAddress As String
    Property Lang As DTOLang
    Property Nom As String
    Property Cognoms As String
    Property sex As DTOUser.Sexs
    Property BirthYear As String
    Property CountryCod As String
    Property tel As String
    Property Password As String
    Property Fase As Fases

    Property LangTag As String


    Public Enum Fases
        Email
        ExistingUser
        FillDetails
        Edit
    End Enum

    ReadOnly Property Langs As List(Of SelectListItem)
        Get
            Dim retval As New List(Of SelectListItem)
            For Each oLang In DTOLang.Collection.All
                Dim item As New SelectListItem
                item.Value = oLang.Tag
                item.Text = oLang.NomEsp
                retval.Add(item)
            Next
            Return retval
        End Get
    End Property

End Class
