Public Class Frm_Servidor
    Private _AllowEvents As Boolean

    Private Sub Frm_Servidor_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = BLLApp.SqlServerName
        _AllowEvents = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        BLLApp.SqlServerName = TextBox1.Text
        Me.Close()
    End Sub
End Class