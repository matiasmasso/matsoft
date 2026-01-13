Public Class Xl_Contact_Add
    Private _Contact As DTOContact

    Public Event RequestToAdd(sender As Object, e As MatEventArgs)

    Private Sub Xl_Contact21_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact21.AfterUpdate
        _Contact = e.Argument
        If _Contact IsNot Nothing Then
            ButtonAdd.Enabled = True
        End If
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        RaiseEvent RequestToAdd(Me, New MatEventArgs(_Contact))
        ButtonAdd.Enabled = False
    End Sub
End Class
