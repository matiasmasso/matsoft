Public Class Xl_IbanTextbox
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Property Value As String
        Get
            Dim retval As String = BLL.BLLIbanStructure.CleanCcc(TextBox1.Text)
            Return retval
        End Get
        Set(value As String)
            Dim formated As String = BLL.BLLIban.Formated(value)
            TextBox1.Text = formated
        End Set
    End Property

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim src As String = BLL.BLLIbanStructure.CleanCcc(TextBox1.Text)
        Dim validated As Boolean = BLL.BLLIban.Validated(src)
        If validated Then
            TextBox1.BackColor = Color.LightBlue
        Else
            TextBox1.BackColor = Color.LightYellow
        End If
        RaiseEvent ValueChanged(Me, New MatEventArgs(src))
    End Sub
End Class
