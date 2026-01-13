Public Class DTOSocialMediaWidget
    Inherits DTOBaseGuid

    Property Platform As Platforms
    Property Titular As String
    Property WidgetId As String
    Property Brand As DTOProductBrand

    Public Enum Platforms
        NotSet
        Facebook
        Twitter
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Url(oSocialMediaWidget As DTOSocialMediaWidget) As String
        Dim retval As String = ""
        Select Case oSocialMediaWidget.Platform
            Case DTOSocialMediaWidget.Platforms.Facebook
                retval = "https://www.facebook.com/" & oSocialMediaWidget.Titular
            Case DTOSocialMediaWidget.Platforms.Twitter
                retval = "https://twitter.com/" & oSocialMediaWidget.Titular
        End Select
        Return retval
    End Function

    Shared Function BrandNom(oSocialMediaWidget As DTOSocialMediaWidget) As String
        Dim retval As String = "M+O"
        If oSocialMediaWidget.Brand IsNot Nothing Then
            retval = oSocialMediaWidget.Brand.nom.Esp
        End If
        Return retval
    End Function

End Class
