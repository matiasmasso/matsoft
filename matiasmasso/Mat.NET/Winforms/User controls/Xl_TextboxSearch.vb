Public Class Xl_TextboxSearch

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public ReadOnly Property Value As String
        Get
            Return TextBox1.Text
        End Get
    End Property

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Raise()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Raise()
    End Sub

    Private Sub Raise()
        Dim src As String = TextBox1.Text
        RaiseEvent AfterUpdate(Me, New MatEventArgs(src))
    End Sub
End Class
