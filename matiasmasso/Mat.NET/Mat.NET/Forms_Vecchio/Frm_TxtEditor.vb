Public Class Frm_TxtEditor

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(value As String)
        MyBase.New()
        Me.InitializeComponent()
        TextBox1.Text = value
    End Sub


    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        Dim retval As String = TextBox1.Text
        RaiseEvent AfterUpdate(retval, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox1.TextChanged
        ButtonOk.Enabled = True
    End Sub
End Class