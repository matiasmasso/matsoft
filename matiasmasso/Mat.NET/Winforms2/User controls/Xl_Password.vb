Public Class Xl_Password
    Inherits Xl_LookupTextboxButton


    Public Sub New()
        MyBase.New()
        MyBase.PasswordChar = "*"
    End Sub

    Private Sub Xl_Password_onLookUpRequest(sender As Object, e As EventArgs) Handles Me.onLookUpRequest
        Clipboard.SetDataObject(MyBase.Text, True)
    End Sub
End Class
