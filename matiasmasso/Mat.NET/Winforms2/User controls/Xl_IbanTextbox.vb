Public Class Xl_IbanTextbox
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Property Value As String
        Get
            Dim retval As String = DTOIban.CleanCcc(TextBox1.Text)
            Return retval
        End Get
        Set(value As String)
            Dim formated As String = DTOIban.Formated(value)
            TextBox1.Text = formated
        End Set
    End Property

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim src As String = DTOIban.CleanCcc(TextBox1.Text)
        If DTOIban.ValidateDigits(src) Then
            TextBox1.BackColor = Color.LightBlue
        Else
            TextBox1.BackColor = Color.LightYellow
        End If
        RaiseEvent ValueChanged(Me, New MatEventArgs(src))
    End Sub
End Class
