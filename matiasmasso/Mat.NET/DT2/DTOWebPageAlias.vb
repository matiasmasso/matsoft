Public Class DTOWebPageAlias
    Inherits DTOBaseGuid
    Property UrlFrom As String
    Property UrlTo As String
    Property domain As domains

    Public Enum domains
        webabsolute
        matiasmasso_es
        matiasmasso_pt
        webrelative
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function GetUrlFrom(oWebPageAlias As DTOWebPageAlias) As String
        Dim retval As String = ""
        Select Case oWebPageAlias.domain
            Case DTOWebPageAlias.domains.matiasmasso_pt
                retval = String.Format("https://www.matiasmasso.pt/{0}", oWebPageAlias.UrlFrom)
            Case Else
                retval = String.Format("https://www.matiasmasso.es/{0}", oWebPageAlias.UrlFrom)
        End Select
        Return retval
    End Function
End Class
