Public Class Frm_FreeText
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(sTitle As String)
        MyBase.New
        InitializeComponent()
        Me.Text = sTitle
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ButtonOk.Enabled = TextBox1.Text > ""
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        RaiseEvent AfterUpdate(Me, New MatEventArgs(TextBox1.Text))
        Me.Close()
    End Sub

End Class