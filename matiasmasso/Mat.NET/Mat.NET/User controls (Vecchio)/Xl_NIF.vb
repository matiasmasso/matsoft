

Public Class Xl_NIF
    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Nif() As DTONif
        Get
            Return New DTONif(TextBox1.Text)
        End Get
        Set(ByVal value As DTONif)
            TextBox1.Text = value.Value
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            If value <> Nothing Then
                TextBox1.Text = New DTONif(value).Value
            End If
        End Set
    End Property

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        Dim oNIF As New DTONif(TextBox1.Text)
        MsgBox(BLL.BLLNif.ValidationResultString(oNIF, BLL.BLLApp.Lang))
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim oNIF As New DTONif(TextBox1.Text)
        PictureBoxWarn.Visible = (BLL.BLLNif.ValidationResult(oNIF) <> DTONif.Errors.Ok)
        RaiseEvent Changed(Me, New EventArgs)
    End Sub
End Class
