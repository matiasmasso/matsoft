Public Class Xl_Lookup_Website
    Inherits Xl_LookupTextboxButton

    Public Sub New()
    End Sub

    Private Sub Xl_Lookup_Website_onLookUpRequest(sender As Object, e As EventArgs) Handles Me.onLookUpRequest
        Dim sUrl As String = MyBase.Text
        UIHelper.ShowHtml(sUrl)
    End Sub
End Class
