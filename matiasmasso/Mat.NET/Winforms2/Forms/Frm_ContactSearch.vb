Public Class Frm_ContactSearch
    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        RaiseEvent itemSelected(Me, New MatEventArgs(Xl_Contact21.Contact))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_Contact21_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact21.AfterUpdate
        ButtonOk.Enabled = True
    End Sub
End Class