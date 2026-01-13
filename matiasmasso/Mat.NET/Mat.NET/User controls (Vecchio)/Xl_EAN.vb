Public Class Xl_EAN
    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Ean13() As maxisrvr.ean13
        Get
            Return New maxisrvr.ean13(TextBox1.Text)
        End Get
        Set(ByVal value As maxisrvr.ean13)
            If value Is Nothing Then
                TextBox1.Clear()
            Else
                TextBox1.Text = value.Value
            End If
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            If value <> Nothing Then
                TextBox1.Text = value
            End If
        End Set
    End Property

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        Dim oEan13 As New maxisrvr.ean13(TextBox1.Text)
        MsgBox(oEan13.ValidationResultString(BLL.BLLApp.Lang))
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim oEan13 As New maxisrvr.ean13(TextBox1.Text)
        PictureBoxWarn.Visible = (oEan13.ValidationResult <> maxisrvr.ean13.Errors.Ok)
        RaiseEvent Changed(Me, New EventArgs)
    End Sub
End Class
